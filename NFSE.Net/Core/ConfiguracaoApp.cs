using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using NFSE.Net.Certificado;

namespace NFSE.Net.Core
{
    /// <summary>
    /// Classe responsável por realizar algumas tarefas na parte de configurações da aplicação.
    /// Arquivo de configurações: UniNfeConfig.xml
    /// </summary>
    public class ConfiguracaoApp
    {
        internal class ArquivoItem
        {
            public string Arquivo;
            public DateTime Data;
            public bool Manual;
        }

        #region NfeConfiguracoes
        /// <summary>
        /// Enumerador com as tags do xml nfe_Configuracoes
        /// </summary>
        private enum NfeConfiguracoes
        {
            Proxy = 0,
            ProxyServidor,
            ProxyUsuario,
            ProxySenha,
            ProxyPorta,
            SenhaConfig,
            ChecarConexaoInternet,
            GravarLogOperacaoRealizada
        }
        #endregion

        #region Propriedades

        #region ChecarConexaoInternet
        public static bool ChecarConexaoInternet { get; set; }
        #endregion

        #region GravarLogOperacoesRealizadas
        public static bool GravarLogOperacoesRealizadas { get; set; }
        #endregion

        #region Propriedades das versões dos XML´s

        #region NFe
        public static string VersaoXMLStatusServico { get; set; }
        public static string VersaoXMLNFe { get; set; }
        public static string VersaoXMLPedRec { get; set; }
        public static string VersaoXMLCanc { get; set; }
        public static string VersaoXMLInut { get; set; }
        public static string VersaoXMLPedSit { get; set; }
        public static string VersaoXMLConsCad { get; set; }
        public static string VersaoXMLCabecMsg { get; set; }
        public static string VersaoXMLEnvDPEC { get; set; }
        public static string VersaoXMLConsDPEC { get; set; }
        public static string VersaoXMLEnvDownload { get; set; }
        public static string VersaoXMLEnvConsultaNFeDest { get; set; }
        public static string VersaoXMLEvento { get; set; }
        #endregion

        #region MDFe
        public static string VersaoXMLMDFeCabecMsg { get; set; }
        public static string VersaoXMLMDFeStatusServico { get; set; }
        public static string VersaoXMLMDFe { get; set; }
        public static string VersaoXMLMDFePedRec { get; set; }
        public static string VersaoXMLMDFePedSit { get; set; }
        public static string VersaoXMLMDFeEvento { get; set; }
        #endregion

        #region CTe
        public static string VersaoXMLCTeCabecMsg { get; set; }
        public static string VersaoXMLCTeStatusServico { get; set; }
        public static string VersaoXMLCTe { get; set; }
        public static string VersaoXMLCTePedRec { get; set; }
        public static string VersaoXMLCTePedSit { get; set; }
        public static string VersaoXMLCTeInut { get; set; }
        public static string VersaoXMLCTeEvento { get; set; }
        #endregion

        #endregion

        #region Propriedades para controle de servidor proxy
        public static bool Proxy { get; set; }
        public static string ProxyServidor { get; set; }
        public static string ProxyUsuario { get; set; }
        public static string ProxySenha { get; set; }
        public static int ProxyPorta { get; set; }
        #endregion

        #region Propriedades para tela de sobre
        public static string NomeEmpresa { get; set; }
        public static string Site { get; set; }
        public static string SiteProduto { get; set; }
        public static string Email { get; set; }
        #endregion

        #region SenhaConfig
        private static bool mSenhaConfigAlterada = false;
        private static string mSenhaConfig;
        public static string SenhaConfig
        {
            get
            {
                return mSenhaConfig;
            }
            set
            {
                if (value != mSenhaConfig)
                {
                    mSenhaConfigAlterada = true;
                    mSenhaConfig = value;
                }
                else
                    mSenhaConfigAlterada = false;
            }
        }
        #endregion

        #region Prorpiedades utilizadas no inicio do sistema
        public static bool AtualizaWSDL { get; set; }
        #endregion

        #endregion

        #region Métodos gerais

        #region Extrae os arquivos necessarios a executacao
        internal class loadResources
        {
            private static string XMLVersoesWSDL = Propriedade.PastaExecutavel + "\\VersoesWSDLs.xml";

            #region load()
            /// <summary>
            /// Exporta os WSDLs e Schemas da DLL para as pastas do UniNFe
            /// </summary>
            public void load()
            {
                List<ArquivoItem> ListArqsAtualizar = new List<ArquivoItem>();
                UpdateWSDL(ListArqsAtualizar);

                try
                {
                    System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFrom("NFe.Components.Wsdl.dll");
                    string[] x = ass.GetManifestResourceNames();
                    if (x.GetLength(0) > 0)
                    {
                        foreach (string s in x)
                        {
                            string fileoutput = null;
                            switch (Propriedade.TipoAplicativo)
                            {                               
                                case TipoAplicativo.Nfse:
                                    if (s.StartsWith("NFe.Components.Wsdl.NFse."))
                                        fileoutput = s.Replace("NFe.Components.Wsdl.NFse.", Propriedade.PastaExecutavel + "\\");
                                    break;
                            }

                            if (fileoutput == null)
                                continue;

                            if (fileoutput.ToLower().EndsWith(".xsd"))
                            {
                                /// Ex: NFe.Components.Wsdl.NFe.NFe.xmldsig-core-schema_v1.01.xsd
                                ///
                                /// pesquisa pelo nome do XSD
                                int plast = fileoutput.ToLower().LastIndexOf("_v");
                                if (plast == -1)
                                    plast = fileoutput.IndexOf(".xsd") - 1;

                                while (fileoutput[plast] != '.')
                                    --plast;

                                string fn = fileoutput.Substring(plast + 1);
                                fileoutput = fileoutput.Substring(0, plast).Replace(".", "\\") + "\\" + fn;
                            }
                            else
                                fileoutput = (fileoutput.Substring(0, fileoutput.LastIndexOf('.')) + "#" + fileoutput.Substring(fileoutput.LastIndexOf('.') + 1)).Replace(".", "\\").Replace("#", ".");

                            //fileoutput = fileoutput.Replace(NFe.Components.Propriedade.PastaExecutavel, "e:\\temp");
                            //System.Windows.Forms.MessageBox.Show(s + "\r\n"+fileoutput+"\r\n"+Path.GetFileName(fileoutput));
                            //continue;

                            FileInfo fi = new FileInfo(fileoutput);
                            ArquivoItem item = new ArquivoItem();
                            item = null;

                            if (fi.Exists)  //danasa 9-2013
                            {
                                if (ListArqsAtualizar.Count > 0)
                                {
                                    item = ListArqsAtualizar.FirstOrDefault(f => f.Arquivo == fi.Name);
                                }
                            }
                            // A comparação é feita (fi.LastWriteTime != item.Data)
                            // Pois intende-se que se a data do arquivo que esta na pasta do UniNFe for superior a data
                            // de quando foi feita a ultima atualizacao do UniNfe, significa que ele foi atualizado manualmente e não devemos
                            // sobrepor o WSDL ou SCHEMA do Usuario - Renan 26/03/2013
                            if (item == null || !(fi.LastWriteTime.ToString("dd/MM/yyyy") != item.Data.ToString("dd/MM/yyyy")))
                            {
                                if (item == null || !item.Manual)
                                {
                                    using (StreamReader FileReader = new StreamReader(ass.GetManifestResourceStream(s)))
                                    {
                                        if (!Directory.Exists(Path.GetDirectoryName(fileoutput)))
                                            Directory.CreateDirectory(Path.GetDirectoryName(fileoutput));

                                        using (StreamWriter FileWriter = new StreamWriter(fileoutput))
                                        {
                                            FileWriter.Write(FileReader.ReadToEnd());
                                            FileWriter.Close();
                                        }
                                    }

                                }

                            }
                            else if (item != null)
                            {
                                item.Manual = true;
                            }
                        }
                    }

                    GravarVersoesWSDLs(ListArqsAtualizar);
                }


                catch (Exception ex)
                {
                    int emp = Functions.FindEmpresaByThread();
                    string xMotivo = "Não foi possível atualizar pacotes de Schemas/WSDLs.";

                    Auxiliar.WriteLog(ex.Message);
                    Auxiliar oAux = new Auxiliar();
                    oAux.GravarArqErroERP(Empresa.Configuracoes[emp].CNPJ + ".err", xMotivo + Environment.NewLine + ex.Message);

                }
            }

            #endregion

            #region UpdateWSDL()
            /// <summary>
            /// Metodo responsavel em definir se as WSDLS e Schemas serão atualizados e quais serão atualizados pois o usuario pode ter atualizado manualmente.
            /// <author>
            /// Renan Borges - 25/06/2013 
            /// </author>
            /// </summary>
            /// <param name="ListArquivosVerificar">Lista a ser montada para ser comparada no momento da Atualizacao dos WSDLs</param>
            public void UpdateWSDL(List<ArquivoItem> ListArquivosVerificar)
            {
                if (File.Exists(XMLVersoesWSDL))
                {
                    LerXmlWSDLs(ListArquivosVerificar);

                }
                else
                {
                    //Não faz nada só vai atualizar e no final gravar o XML
                }
            }
            #endregion

            #region GravarVersoesWSDLs()
            /// <summary>
            /// Metodo responsavel em Ler os Arquivos disponiveis nas pastas e Gravas as Informações no XML que contem as informacoes sobre o mesmo
            /// </summary>
            /// <author>
            /// Renan Borges - 25/06/2013 
            /// </author>
            /// <param name="ListaAnterior"></param>
            public void GravarVersoesWSDLs(List<ArquivoItem> ListaAnterior)
            {
                string pastaExecutavel = Propriedade.PastaExecutavel;
                string pastaWSDLProducao = pastaExecutavel + "\\WSDL\\Producao\\";
                string pastaWSDLHomologacao = pastaExecutavel + "\\WSDL\\Homologacao\\";
                string pastaXSD = pastaExecutavel + "\\schemas\\";

                string[] ArquivosWSDLProducao = Directory.GetFiles(pastaWSDLProducao, "*.wsdl", SearchOption.AllDirectories);
                string[] ArquivosWSDLHomologacao = Directory.GetFiles(pastaWSDLHomologacao, "*.wsdl", SearchOption.AllDirectories);
                string[] ArquivosXSD = Directory.GetFiles(pastaXSD, "*.xsd", SearchOption.AllDirectories);

                List<ArquivoItem> ArquivosXML = new List<ArquivoItem>();

                PreparaDadosWSDLs(ArquivosWSDLProducao, ArquivosXML);
                PreparaDadosWSDLs(ArquivosWSDLHomologacao, ArquivosXML);
                PreparaDadosWSDLs(ArquivosXSD, ArquivosXML);

                foreach (ArquivoItem item in ArquivosXML)
                {
                    ArquivoItem itemAntigo = ListaAnterior.FirstOrDefault(a => a.Arquivo == item.Arquivo);
                    item.Manual = itemAntigo == null ? false : itemAntigo.Manual;
                }

                if (File.Exists(XMLVersoesWSDL))
                {
                    File.Delete(XMLVersoesWSDL);
                }

                EscreverXmlWSDLs(ArquivosXML);

            }
            #endregion

            #region PreparaDadosWSDLs()
            /// <summary>
            /// Metodo responsavel em pegar o Array contendo o caminho dos arquivos encontrado nos diretorios e Montar uma 
            /// Lista Contendo os Dados dos Arquivos que serao gravados no VersoesWSDLs.xml
            /// </summary>
            /// <author>
            /// Renan Borges - 25/06/2013 
            /// </author>
            /// <param name="arquivos">Array com o Caminho dos Arquivos encontrados nos diretorios</param>
            /// <param name="ListArquivos">A Lista onde as informações serão armazenadas para serem gravadas posteriormente</param>
            private void PreparaDadosWSDLs(string[] arquivos, List<ArquivoItem> ListArquivos)
            {

                foreach (string arquivo in arquivos)
                {
                    FileInfo infArquivo = new FileInfo(arquivo);

                    DateTime dataModif = infArquivo.LastWriteTime;
                    string nomearquivo = infArquivo.Name;

                    ListArquivos.Add(new ArquivoItem
                    {
                        Arquivo = nomearquivo,
                        Data = dataModif,
                        Manual = false

                    });
                }


            }
            #endregion

            #region EscreverXmlWSDLs()
            /// <summary>
            /// Metodo responsavel em escrever o XML VersoesWSDLs.xml na pasta do executavel
            /// </summary>
            /// <author>
            /// Renan Borges - 25/06/2013
            /// </author>
            /// <param name="ListArquivosGerar">Lista com os informacoes a serem gravados pertinentes aos arquivos</param>
            private void EscreverXmlWSDLs(List<ArquivoItem> ListArquivosGerar)
            {

                if (ListArquivosGerar.Count > 0)
                {

                    XmlTextWriter arqXML = new XmlTextWriter(XMLVersoesWSDL, Encoding.UTF8);
                    arqXML.WriteStartDocument();

                    arqXML.Formatting = Formatting.Indented;
                    arqXML.WriteStartElement("arquivos");

                    foreach (ArquivoItem item in ListArquivosGerar)
                    {
                        arqXML.WriteStartElement("wsdl");

                        arqXML.WriteElementString("arquivo", item.Arquivo);
                        arqXML.WriteElementString("data", item.Data.ToShortDateString());
                        arqXML.WriteElementString("manual", item.Manual.ToString());

                        arqXML.WriteEndElement();
                    }

                    arqXML.WriteFullEndElement();

                    arqXML.Close();
                }

            }
            #endregion

            #region LerXmlWSDLs()
            /// <summary>
            /// Metodo responsavel em ler as informacoes dos XML WSDLsVersoes e preparar a lista dos arquivos
            /// </summary>
            /// <author>
            /// Renan Borges - 25/06/2013
            /// </author>
            /// <param name="ListArqInstalados">Lista onde vai ser adcionada os arquivos do encontrados no XML</param>
            private void LerXmlWSDLs(List<ArquivoItem> ListArqInstalados)
            {
                XmlDocument oXmlWSDLs = new XmlDocument();
                oXmlWSDLs.Load(XMLVersoesWSDL);
                XmlNodeList xnListXml = oXmlWSDLs.GetElementsByTagName("wsdl");

                foreach (XmlNode item in xnListXml)
                {
                    string _arquivo = item["arquivo"].InnerText;
                    string _data = item["data"].InnerText;
                    string _manual = item["manual"].InnerText;

                    ListArqInstalados.Add(new ArquivoItem
                    {
                        Arquivo = _arquivo,
                        Data = Convert.ToDateTime(_data),
                        Manual = Convert.ToBoolean(_manual)
                    });
                }
            }
            #endregion

        }


        public static void loadResouces()
        {
            new loadResources().load();
        }

        #region StartVersoes
        public static void StartVersoes()
        {
            if (ConfiguracaoApp.AtualizaWSDL)
                new loadResources().load();

            ConfiguracaoApp.CarregarDados();

            switch (Propriedade.TipoAplicativo)
            {             
                case TipoAplicativo.Nfse:
                    ConfiguracaoApp.VersaoXMLCanc = "1.03";
                    ConfiguracaoApp.VersaoXMLConsCad = "1.03";
                    ConfiguracaoApp.VersaoXMLInut = "1.03";
                    ConfiguracaoApp.VersaoXMLNFe = "1.03";
                    ConfiguracaoApp.VersaoXMLPedRec = "1.03";
                    ConfiguracaoApp.VersaoXMLPedSit = "1.03";
                    ConfiguracaoApp.VersaoXMLStatusServico = "1.03";
                    ConfiguracaoApp.VersaoXMLCabecMsg = "1.03";
                    Propriedade.nsURI = "http://www.abrasf.org.br/";
                    break;
            }
        }
        #endregion

        #region CarregarDados()
        /// <summary>
        /// Carrega as configurações realizadas na Aplicação gravadas no XML UniNfeConfig.xml para
        /// propriedades, para facilitar a leitura das informações necessárias para as transações da NF-e.
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// </remarks>
        public static void CarregarDados()
        {
            string vArquivoConfig = Propriedade.PastaExecutavel + "\\" + Propriedade.NomeArqConfig;
            XmlDocument doc = null;
            if (File.Exists(vArquivoConfig))
            {
                try
                {
                    doc = new XmlDocument();
                    doc.Load(vArquivoConfig);

                    XmlNodeList configList = null;

                    configList = doc.GetElementsByTagName(NFeStrConstants.nfe_configuracoes);

                    foreach (XmlNode nodeConfig in configList)
                    {
                        XmlElement elementConfig = (XmlElement)nodeConfig;

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.Proxy.ToString())[0] != null)
                            ConfiguracaoApp.Proxy = Convert.ToBoolean(elementConfig[NfeConfiguracoes.Proxy.ToString()].InnerText);

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.ProxyServidor.ToString())[0] != null)
                            ConfiguracaoApp.ProxyServidor = elementConfig[NfeConfiguracoes.ProxyServidor.ToString()].InnerText.Trim();

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.ProxyUsuario.ToString())[0] != null)
                            ConfiguracaoApp.ProxyUsuario = elementConfig[NfeConfiguracoes.ProxyUsuario.ToString()].InnerText.Trim();

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.ProxySenha.ToString())[0] != null)
                            ConfiguracaoApp.ProxySenha = elementConfig[NfeConfiguracoes.ProxySenha.ToString()].InnerText.Trim();

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.ProxyPorta.ToString())[0] != null)
                            ConfiguracaoApp.ProxyPorta = Convert.ToInt32(elementConfig[NfeConfiguracoes.ProxyPorta.ToString()].InnerText.Trim());

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.SenhaConfig.ToString())[0] != null)
                            ConfiguracaoApp.SenhaConfig = elementConfig[NfeConfiguracoes.SenhaConfig.ToString()].InnerText.Trim();

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.ChecarConexaoInternet.ToString())[0] != null)
                            ConfiguracaoApp.ChecarConexaoInternet = Convert.ToBoolean(elementConfig[NfeConfiguracoes.ChecarConexaoInternet.ToString()].InnerText);
                        else
                            ConfiguracaoApp.ChecarConexaoInternet = true;

                        if (elementConfig.GetElementsByTagName(NfeConfiguracoes.GravarLogOperacaoRealizada.ToString())[0] != null)
                            ConfiguracaoApp.GravarLogOperacoesRealizadas = Convert.ToBoolean(elementConfig[NfeConfiguracoes.GravarLogOperacaoRealizada.ToString()].InnerText);

                    }
                }                
                finally
                {
                    if (doc != null)
                        doc = null;

                }
            }
            else
            {
                ChecarConexaoInternet = true;
            }
            //Carregar a lista de webservices disponíveis
            try
            {
                if (WebServiceProxy.CarregaWebServicesList())
                    ///
                    /// danasa 9-2013
                    /// força a atualizacao dos wsdl's pois pode ser que tenha sido criado um novo padrao
                    ConfiguracaoApp.AtualizaWSDL = true;
            }
            catch (Exception ex)
            {
                Auxiliar.WriteLog(ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// Definir o webservice que será utilizado para o envio do XML
        /// </summary>
        /// <param name="servico">Serviço que será executado</param>
        /// <param name="emp">Index da empresa que será executado o serviço</param>
        /// <param name="cUF">Código da UF</param>
        /// <param name="tpAmb">Código do ambiente que será acessado</param>
        /// <param name="tpEmis">Tipo de emissão do XML</param>
        /// <param name="versaoNFe">Versão da NFe (1 ou 2)</param>
        /// <returns>Retorna o objeto do serviço</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 04/04/2011
        /// </remarks>
        public static WebServiceProxy DefinirWS(Servicos servico, int emp, int cUF, int tpAmb, int tpEmis)
        {
            return DefinirWS(servico, emp, cUF, tpAmb, tpEmis, PadroesNFSe.NaoIdentificado);
        }

        public static WebServiceProxy DefinirWS(Servicos servico, int emp, int cUF, int tpAmb, int tpEmis, PadroesNFSe padraoNFSe)
        {
            WebServiceProxy wsProxy = null;
            string key = servico + " " + cUF + " " + tpAmb + " " + tpEmis;
            while (true)
            {
                lock (Smf.WebProxy)
                {
                    if (Empresa.Configuracoes[emp].WSProxy.ContainsKey(key))
                    {
                        wsProxy = Empresa.Configuracoes[emp].WSProxy[key];
                    }
                    else
                    {
                        //Definir a URI para conexão com o Webservice
                        string Url = ConfiguracaoApp.DefLocalWSDL(cUF, tpAmb, tpEmis, servico);

                        wsProxy = new WebServiceProxy(Url, Empresa.Configuracoes[emp].X509Certificado, padraoNFSe);

                        Empresa.Configuracoes[emp].WSProxy.Add(key, wsProxy);
                    }

                    break;
                }
            }

            return wsProxy;
        }


        #region DefLocalWSDL
        /// <summary>
        /// Definir o local do WSDL do webservice
        /// </summary>
        /// <param name="CodigoUF">Código da UF que é para pesquisar a URL do WSDL</param>
        /// <param name="tipoAmbiente">Tipo de ambiente da NFe</param>
        /// <param name="tipoEmissao">Tipo de Emissao da NFe</param>
        /// <param name="servico">Serviço da NFe que está sendo executado</param>
        /// <returns>Retorna a URL</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 22/03/2011
        /// </remarks>
        private static string DefLocalWSDL(int CodigoUF, int tipoAmbiente, int tipoEmissao, Servicos servico)
        {
            string WSDL = string.Empty;
            switch (tipoEmissao)
            {
                case Propriedade.TipoEmissao.teSVCRS:
                    CodigoUF = 902;
                    break;

                case Propriedade.TipoEmissao.teSVCSP:
                    CodigoUF = 903;
                    break;

                case Propriedade.TipoEmissao.teSCAN:
                    CodigoUF = 900;
                    break;              
                default:
                    break;
            }
            string ufNome = CodigoUF.ToString();  //danasa 20-9-2010            

            #region --varre a lista de webservices baseado no codigo da UF

            if (WebServiceProxy.webServicesList.Count == 0)
                throw new Exception("Lista de webservices não foi processada verifique se o arquivo 'WebService.xml' existe na pasta WSDL do UniNFe");

            foreach (webServices list in WebServiceProxy.webServicesList)
            {
                if (list.ID == CodigoUF)
                {
                    switch (servico)
                    {
                        #region NFS-e
                        case Servicos.RecepcionarLoteRps:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.RecepcionarLoteRps : list.LocalProducao.RecepcionarLoteRps);
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.ConsultarSituacaoLoteRps : list.LocalProducao.ConsultarSituacaoLoteRps);
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.ConsultarNfsePorRps : list.LocalProducao.ConsultarNfsePorRps);
                            break;
                        case Servicos.ConsultarNfse:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.ConsultarNfse : list.LocalProducao.ConsultarNfse);
                            break;
                        case Servicos.ConsultarLoteRps:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.ConsultarLoteRps : list.LocalProducao.ConsultarLoteRps);
                            break;
                        case Servicos.CancelarNfse:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.CancelarNfse : list.LocalProducao.CancelarNfse);
                            break;
                        case Servicos.ConsultarURLNfse:
                            WSDL = (tipoAmbiente == Propriedade.TipoAmbiente.taHomologacao ? list.LocalHomologacao.ConsultarURLNfse : list.LocalProducao.ConsultarURLNfse);
                            break;
                        #endregion
                    }
                    if (tipoEmissao == Propriedade.TipoEmissao.teDPEC)
                        ufNome = "DPEC";
                    else
                        ufNome = "de " + list.Nome;

                    break;
                }
            }
            #endregion

            if (string.IsNullOrEmpty(WSDL) || !File.Exists(WSDL))
            {
                if (!File.Exists(WSDL) && !string.IsNullOrEmpty(WSDL))
                    switch (Propriedade.TipoAplicativo)
                    {                        
                        case TipoAplicativo.Nfse:
                            throw new Exception("O arquivo \"" + WSDL + "\" não existe. Aconselhamos a reinstalação do UniNFSe.");
                    }

                string Ambiente = string.Empty;
                switch (tipoAmbiente)
                {
                    case Propriedade.TipoAmbiente.taProducao:
                        Ambiente = "produção";
                        break;

                    case Propriedade.TipoAmbiente.taHomologacao:
                        Ambiente = "homologação";
                        break;
                }
                string errorStr = "O Estado " + ufNome + " ainda não dispõe do serviço {0} para o ambiente de " + Ambiente + ".";
                switch (servico)
                {
                    case Servicos.CancelarNfse:
                    case Servicos.ConsultarURLNfse:
                    case Servicos.ConsultarLoteRps:
                    case Servicos.ConsultarNfse:
                    case Servicos.ConsultarNfsePorRps:
                    case Servicos.ConsultarSituacaoLoteRps:
                    case Servicos.RecepcionarLoteRps:
                        throw new Exception(string.Format(errorStr, "da NFS-e"));
                }
                throw new Exception(string.Format(errorStr, "da NF-e"));
            }

            return WSDL;
        }
        #endregion

        #region GravarConfig()
        /// <summary>
        /// Método responsável por gravar as configurações da Aplicação no arquivo "UniNfeConfig.xml"
        /// </summary>
        /// <returns>Retorna true se conseguiu gravar corretamente as configurações ou false se não conseguiu</returns>
        public void GravarConfig(bool gravaArqEmpresa, bool validaCertificado)  //<<<<<<danasa 1-5-2011
        {
            ValidarConfig(validaCertificado);
            GravarConfigGeral();
            GravarConfigEmpresa();
            if (gravaArqEmpresa)        //<<<<<<danasa 1-5-2011
            {                           //<<<<<<danasa 1-5-2011
                GravarArqEmpresas();    //<<<<<<danasa 1-5-2011
            }                           //<<<<<<danasa 1-5-2011
        }
        #endregion

        #region Gravar XML com as empresas cadastradas

        private void GravarArqEmpresas()
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            //Agora vamos criar um XML Writer
            XmlWriter oXmlGravar = XmlWriter.Create(Propriedade.NomeArqEmpresa, oSettings);

            //Agora vamos gravar os dados
            oXmlGravar.WriteStartDocument();
            oXmlGravar.WriteStartElement("Empresa");

            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                //Abrir a tag <Registro>
                oXmlGravar.WriteStartElement("Registro");

                //Criar o atributo CNPJ dentro da tag Registro
                oXmlGravar.WriteStartAttribute("CNPJ");
                oXmlGravar.WriteString(empresa.CNPJ);

                //Criar o atributo Servico dentro da tag Registro
                oXmlGravar.WriteStartAttribute("Servico");
                oXmlGravar.WriteString(((int)empresa.Servico).ToString());

                //Encerrar o atributo CNPJ
                oXmlGravar.WriteEndAttribute(); // Encerrar o atributo CNPJ

                //Criar a tag <Nome> com seu conteúdo </Nome>
                oXmlGravar.WriteElementString("Nome", empresa.Nome);

                //Encerrar a tag </Registro>
                oXmlGravar.WriteEndElement();
            }
            oXmlGravar.WriteEndElement(); //Encerrar o elemento Empresa
            oXmlGravar.WriteEndDocument();
            oXmlGravar.Flush();
            oXmlGravar.Close();
        }
        #endregion

        #region GravarConfigEmpresa()
        /// <summary>
        /// Gravar as configurações das empresas
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        private void GravarConfigEmpresa()
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                XmlWriter oXmlGravar = null;

                try
                {
                    //Criar pasta das configurações das empresas
                    string pasta = Propriedade.PastaExecutavel + "\\" + empresa.CNPJ.Trim();
                    switch (empresa.Servico)
                    {
                        default:
                            pasta += "\\" + empresa.Servico.ToString().ToLower();
                            break;
                    }
                    if (!Directory.Exists(pasta))
                        Directory.CreateDirectory(pasta);

                    //Agora vamos criar um XML Writer
                    oXmlGravar = XmlWriter.Create(pasta + "\\" + Propriedade.NomeArqConfig, oSettings);

                    //Agora vamos gravar os dados
                    oXmlGravar.WriteStartDocument();
                    oXmlGravar.WriteStartElement(NFeStrConstants.nfe_configuracoes);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaXmlEnvio, empresa.PastaEnvio);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaXmlRetorno, empresa.PastaRetorno);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaXmlEnviado, empresa.PastaEnviado);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaXmlErro, empresa.PastaErro);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaBackup, empresa.PastaBackup);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaXmlEmLote, empresa.PastaEnvioEmLote);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaValidar, empresa.PastaValidar);
                    oXmlGravar.WriteElementString(NFeStrConstants.PastaDownloadNFeDest, empresa.PastaDownloadNFeDest);
                    oXmlGravar.WriteElementString(NFeStrConstants.UnidadeFederativaCodigo, empresa.UFCod.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.AmbienteCodigo, empresa.tpAmb.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.CertificadoDigital, empresa.Certificado);
                    oXmlGravar.WriteElementString(NFeStrConstants.CertificadoInstalado, empresa.CertificadoInstalado.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.CertificadoArquivo, empresa.CertificadoArquivo.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.CertificadoSenha, Criptografia.criptografaSenha(empresa.CertificadoSenha));
                    oXmlGravar.WriteElementString(NFeStrConstants.tpEmis, empresa.tpEmis.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.GravarRetornoTXTNFe, empresa.GravarRetornoTXTNFe.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.GravarEventosDeTerceiros, empresa.GravarEventosDeTerceiros.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.GravarEventosNaPastaEnviadosNFe, empresa.GravarEventosNaPastaEnviadosNFe.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.GravarEventosCancelamentoNaPastaEnviadosNFe, empresa.GravarEventosCancelamentoNaPastaEnviadosNFe.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.DiretorioSalvarComo, empresa.DiretorioSalvarComo.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.DiasLimpeza, empresa.DiasLimpeza.ToString());
                    oXmlGravar.WriteElementString(NFeStrConstants.TempoConsulta, empresa.TempoConsulta.ToString());
                
                    if (empresa.CertificadoInstalado)
                    {                    
                        oXmlGravar.WriteElementString(NFeStrConstants.CertificadoDigitalThumbPrint, empresa.CertificadoThumbPrint);
                    }

                    oXmlGravar.WriteElementString(NFeStrConstants.UsuarioWS, (empresa.UsuarioWS == null ? "" : empresa.UsuarioWS.ToString()));
                    oXmlGravar.WriteElementString(NFeStrConstants.SenhaWS, (empresa.SenhaWS == null ? "" : empresa.SenhaWS.ToString()));

                    oXmlGravar.WriteEndElement(); //nfe_configuracoes
                    oXmlGravar.WriteEndDocument();
                    oXmlGravar.Flush();
                }
                finally
                {
                    if (oXmlGravar != null)
                        oXmlGravar.Close();
                }
            }
        }
        #endregion

        #region GravarConfigGeral()
        /// <summary>
        /// Gravar as configurações gerais
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        private void GravarConfigGeral()
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            XmlWriter oXmlGravar = null;

            try
            {
                //Agora vamos criar um XML Writer
                oXmlGravar = XmlWriter.Create(Propriedade.PastaExecutavel + "\\" + Propriedade.NomeArqConfig, oSettings);

                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement(NFeStrConstants.nfe_configuracoes);
                oXmlGravar.WriteElementString(NfeConfiguracoes.Proxy.ToString(), ConfiguracaoApp.Proxy.ToString());
                oXmlGravar.WriteElementString(NfeConfiguracoes.ProxyServidor.ToString(), ConfiguracaoApp.ProxyServidor);
                oXmlGravar.WriteElementString(NfeConfiguracoes.ProxyUsuario.ToString(), ConfiguracaoApp.ProxyUsuario);
                oXmlGravar.WriteElementString(NfeConfiguracoes.ProxySenha.ToString(), ConfiguracaoApp.ProxySenha);
                oXmlGravar.WriteElementString(NfeConfiguracoes.ProxyPorta.ToString(), ConfiguracaoApp.ProxyPorta.ToString());
                oXmlGravar.WriteElementString(NfeConfiguracoes.ChecarConexaoInternet.ToString(), ConfiguracaoApp.ChecarConexaoInternet.ToString());
                oXmlGravar.WriteElementString(NfeConfiguracoes.GravarLogOperacaoRealizada.ToString(), ConfiguracaoApp.GravarLogOperacoesRealizadas.ToString());
                if (!string.IsNullOrEmpty(ConfiguracaoApp.SenhaConfig))
                {
                    if (ConfiguracaoApp.mSenhaConfigAlterada)
                    {
                        ConfiguracaoApp.SenhaConfig = Functions.GerarMD5(ConfiguracaoApp.SenhaConfig);
                    }

                    oXmlGravar.WriteElementString(NfeConfiguracoes.SenhaConfig.ToString(), ConfiguracaoApp.SenhaConfig);
                    ConfiguracaoApp.mSenhaConfigAlterada = false;
                }

                oXmlGravar.WriteEndElement(); //nfe_configuracoes
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
            }
            finally
            {
                if (oXmlGravar != null)
                    oXmlGravar.Close();
            }
        }
        #endregion

        #region ValidarConfig()
        /// <summary>
        /// Verifica se algumas das informações das configurações tem algum problema ou falha
        /// </summary>
        /// <param name="validarCertificado">Se valida se tem certificado informado ou não nas configurações</param>
        private void ValidarConfig(bool validarCertificado)
        {
            string erro = string.Empty;
            bool validou = true;

            #region Remover End Slash
            for (int i = 0; i < Empresa.Configuracoes.Count; i++)
            {
                Empresa.Configuracoes[i].PastaEnvio = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaEnvio);
                Empresa.Configuracoes[i].PastaEnviado = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaEnviado);
                Empresa.Configuracoes[i].PastaErro = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaErro);
                Empresa.Configuracoes[i].PastaRetorno = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaRetorno);
                Empresa.Configuracoes[i].PastaBackup = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaBackup);
                Empresa.Configuracoes[i].PastaEnvioEmLote = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaEnvioEmLote);
                Empresa.Configuracoes[i].PastaDownloadNFeDest = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaDownloadNFeDest);
                Empresa.Configuracoes[i].PastaValidar = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaValidar);                
            }
            #endregion

            #region Verificar a duplicação de nome de pastas que não pode existir
            if (validou)
            {
                List<FolderCompare> fc = new List<FolderCompare>();

                for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                {
                    fc.Add(new FolderCompare(i, Empresa.Configuracoes[i].PastaEnvio));
                    fc.Add(new FolderCompare(i + 1, Empresa.Configuracoes[i].PastaEnvioEmLote));
                    fc.Add(new FolderCompare(i + 2, Empresa.Configuracoes[i].PastaRetorno));
                    fc.Add(new FolderCompare(i + 3, Empresa.Configuracoes[i].PastaEnviado));
                    fc.Add(new FolderCompare(i + 4, Empresa.Configuracoes[i].PastaErro));
                    fc.Add(new FolderCompare(i + 5, Empresa.Configuracoes[i].PastaBackup));
                    fc.Add(new FolderCompare(i + 6, Empresa.Configuracoes[i].PastaValidar));
                    fc.Add(new FolderCompare(i + 6, Empresa.Configuracoes[i].PastaDownloadNFeDest));
                }

                foreach (FolderCompare fc1 in fc)
                {
                    if (string.IsNullOrEmpty(fc1.folder))
                        continue;

                    foreach (FolderCompare fc2 in fc)
                    {
                        if (fc1.id != fc2.id)
                        {
                            if (fc1.folder.ToLower().Equals(fc2.folder.ToLower()))
                            {
                                erro = "Não é permitido a utilização de pasta idênticas na mesma ou entre as empresas..";
                                validou = false;
                                break;
                            }
                        }
                    }
                    if (!validou)
                        break;
                }
            }
            #endregion

            if (validou)
            {
                for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                {
                    Empresa empresa = Empresa.Configuracoes[i];
                    List<string> diretorios = new List<string>();
                    List<string> mensagens = new List<string>();

                    #region Verificar se tem alguma pasta em branco
                    diretorios.Clear(); mensagens.Clear();
                    diretorios.Add(empresa.PastaEnviado); mensagens.Add("Informe a pasta para arquivamento dos arquivos XML enviados.");
                    diretorios.Add(empresa.PastaEnvio); mensagens.Add("Informe a pasta de envio dos arquivos XML.");
                    diretorios.Add(empresa.PastaRetorno); mensagens.Add("Informe a pasta de retorno dos arquivos XML.");
                    diretorios.Add(empresa.PastaErro); mensagens.Add("Informe a pasta para arquivamento temporário dos arquivos XML que apresentaram erros.");
                    diretorios.Add(empresa.PastaBackup); mensagens.Add("Informe a pasta para o Backup dos arquivos XML enviados.");
                    diretorios.Add(empresa.PastaValidar); mensagens.Add("Informe a pasta onde será gravado os arquivos XML somente para ser validado pela Aplicação.");
                    //diretorios.Add(empresa.PastaDownloadNFeDest); mensagens.Add("Informe a pasta onde será gravado os arquivos XML de download de destinatário.");

                    for (int b = 0; b < diretorios.Count; b++)
                    {
                        if (diretorios[b].Equals(string.Empty))
                        {
                            erro = mensagens[b].Trim() + "\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                            validou = false;
                            break;
                        }
                    }                  
                    #endregion

                    #region Verificar se o certificado foi informado
                    if (validarCertificado)
                    {
                        if (validou)
                        {
                            string aplicacao = "NF-e";
                            switch (Propriedade.TipoAplicativo)
                            {
                                case TipoAplicativo.Nfse:
                                    aplicacao = "NFS-e";
                                    break;
                            }
                            if (empresa.CertificadoInstalado && empresa.CertificadoThumbPrint.Equals(string.Empty))
                            {
                                erro = "Selecione o certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + ".\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                validou = false;
                            }
                            if (!empresa.CertificadoInstalado)
                            {
                                if (empresa.CertificadoArquivo.Equals(string.Empty))
                                {
                                    erro = "Informe o local de armazenamento do certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + ".\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                    validou = false;
                                }
                                else if (!File.Exists(empresa.CertificadoArquivo))
                                {
                                    erro = "Arquivo do certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + " não foi encontrado.\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                    validou = false;
                                }
                                else if (empresa.CertificadoSenha.Equals(string.Empty))
                                {
                                    erro = "Informe a senha do certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + ".\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                    validou = false;
                                }
                                else
                                {
                                    try
                                    {
                                        using (FileStream fs = new FileStream(empresa.CertificadoArquivo, FileMode.Open))
                                        {
                                            byte[] buffer = new byte[fs.Length];
                                            fs.Read(buffer, 0, buffer.Length);
                                            empresa.X509Certificado = new X509Certificate2(buffer, empresa.CertificadoSenha);
                                        }
                                    }
                                    catch (System.Security.Cryptography.CryptographicException ex)
                                    {
                                        erro = ex.Message + ".\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                                        validou = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        erro = ex.Message + ".\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                                        validou = false;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Verificar se as pastas informadas existem
                    if (validou)
                    {
                       
                        if (empresa.PastaEnvio.ToLower().EndsWith("temp"))
                        {
                            erro = "Pasta de envio não pode terminar com a subpasta 'temp'.\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                            validou = false;
                        }
                        else
                            if (empresa.PastaEnvioEmLote.ToLower().EndsWith("temp"))
                            {
                                erro = "Pasta de envio em lote não pode terminar com a subpasta 'temp'.\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                                validou = false;
                            }
                            else
                                if (empresa.PastaValidar.ToLower().EndsWith("temp"))
                                {
                                    erro = "Pasta de validação não pode terminar com a subpasta 'temp'.\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                                    validou = false;
                                }
                                else
                                    if (empresa.PastaErro.ToLower().EndsWith("temp"))
                                    {
                                        erro = "Pasta de XML's com erro na tentativa de envio não pode terminar com a subpasta 'temp'.\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                                        validou = false;
                                    }

                        if (validou)
                        {
                            diretorios.Clear(); mensagens.Clear();
                            diretorios.Add(empresa.PastaEnvio.Trim()); mensagens.Add("A pasta de envio dos arquivos XML informada não existe.");
                            diretorios.Add(empresa.PastaRetorno.Trim()); mensagens.Add("A pasta de retorno dos arquivos XML informada não existe.");
                            diretorios.Add(empresa.PastaEnviado.Trim()); mensagens.Add("A pasta para arquivamento dos arquivos XML enviados informada não existe.");
                            diretorios.Add(empresa.PastaErro.Trim()); mensagens.Add("A pasta para arquivamento temporário dos arquivos XML com erro informada não existe.");
                            diretorios.Add(empresa.PastaBackup.Trim()); mensagens.Add("A pasta para backup dos XML enviados informada não existe.");
                            diretorios.Add(empresa.PastaValidar.Trim()); mensagens.Add("A pasta para validação de XML´s informada não existe.");
                            diretorios.Add(empresa.PastaDownloadNFeDest.Trim()); mensagens.Add("A pasta para arquivamento das NFe de destinatários informada não existe.");
                            diretorios.Add(empresa.PastaEnvioEmLote.Trim()); mensagens.Add("A pasta de envio das notas fiscais eletrônicas em lote informada não existe.");
                        
                            for (int b = 0; b < diretorios.Count; b++)
                            {
                                if (!string.IsNullOrEmpty(diretorios[b]))
                                {
                                    if (!Directory.Exists(diretorios[b]))
                                    {
                                        if (empresa.CriaPastasAutomaticamente)
                                        {
                                            Directory.CreateDirectory(diretorios[b]);
                                        }
                                        else
                                        {
                                            erro = mensagens[b].Trim() + "\r\n" + empresa.Nome + "\r\n" + empresa.CNPJ;
                                            validou = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        #region Criar pasta Temp dentro da pasta de envio, envio em lote e validar
                        //Criar pasta Temp dentro da pasta de envio, envio em Lote e Validar. Wandrey 03/08/2011
                        if (validou)
                        {
                            if (Directory.Exists(empresa.PastaEnvio.Trim()))
                            {
                                if (!Directory.Exists(empresa.PastaEnvio.Trim() + "\\Temp"))
                                {
                                    Directory.CreateDirectory(empresa.PastaEnvio.Trim() + "\\Temp");
                                }
                            }

                            if (Directory.Exists(empresa.PastaEnvioEmLote.Trim()))
                            {
                                if (!Directory.Exists(empresa.PastaEnvioEmLote.Trim() + "\\Temp"))
                                {
                                    Directory.CreateDirectory(empresa.PastaEnvioEmLote.Trim() + "\\Temp");
                                }
                            }

                            if (Directory.Exists(empresa.PastaValidar.Trim()))
                            {
                                if (!Directory.Exists(empresa.PastaValidar.Trim() + "\\Temp"))
                                {
                                    Directory.CreateDirectory(empresa.PastaValidar.Trim() + "\\Temp");
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
       
                }
            }

            #region Ticket: #110
            /* Validar se já existe uma instancia utilizando estes diretórios
             * Marcelo
             * 03/06/2013
             */
            if (validou)
            {
                //Se encontrar algum arquivo de lock nos diretórios, não permtir que seja executado                
                validou = String.IsNullOrEmpty(erro);
            }
            #endregion


            if (!validou)
            {
                throw new Exception(erro);
            }
        }
        #endregion
                

        #region RemoveEndSlash
        public static string RemoveEndSlash(string value)
        {
            return RemoveEndSlash(value, false);
        }
        #endregion

        #region RemoveEndSlash
        /// <summary>
        /// danasa 8-2009
        /// </summary>
        /// <param name="value"></param>
        /// <param name="acertarNomeCurto"></param>
        /// <returns></returns>
        public static string RemoveEndSlash(string value, bool acertarNomeCurto)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (acertarNomeCurto)
                {
                    DirectoryInfo dir = new DirectoryInfo(value);
                    value = dir.FullName;
                }

                while (value.Substring(value.Length - 1, 1) == @"\" && !string.IsNullOrEmpty(value))
                    value = value.Substring(0, value.Length - 1);
            }
            else
            {
                value = string.Empty;
            }

            return value.Replace("\r\n", "").Trim();
        }
        #endregion

    
        #endregion
    }
        #endregion
}
