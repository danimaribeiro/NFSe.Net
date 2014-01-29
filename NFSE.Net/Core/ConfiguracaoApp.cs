using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
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

        #region Propriedades para controle de servidor proxy
        public static bool Proxy { get; set; }
        public static string ProxyServidor { get; set; }
        public static string ProxyUsuario { get; set; }
        public static string ProxySenha { get; set; }
        public static int ProxyPorta { get; set; }
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
            public void load(Core.Empresa empresa)
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
                    Auxiliar.WriteLog(ex.ToString());
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
            if (File.Exists(vArquivoConfig))
            {
                //TODO Carregar as configurações do arquivo xml na classe ConfiguracaoApp   
            }

            if (WebServiceProxy.CarregaWebServicesList())
                ConfiguracaoApp.AtualizaWSDL = true;
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
        public static WebServiceProxy DefinirWS(Servicos servico, Core.Empresa emp, int cUF, int tpAmb, int tpEmis)
        {
            return DefinirWS(servico, emp, cUF, tpAmb, tpEmis, PadroesNFSe.NaoIdentificado);
        }

        public static WebServiceProxy DefinirWS(Servicos servico, Core.Empresa emp, int cUF, int tpAmb, int tpEmis, PadroesNFSe padraoNFSe)
        {
            WebServiceProxy wsProxy = null;
            string key = servico + " " + cUF + " " + tpAmb + " " + tpEmis;
            while (true)
            {
                if (emp.WSProxy.ContainsKey(key))
                {
                    wsProxy = emp.WSProxy[key];
                }
                else
                {
                    //Definir a URI para conexão com o Webservice
                    string Url = ConfiguracaoApp.DefLocalWSDL(cUF, tpAmb, tpEmis, servico);

                    wsProxy = new WebServiceProxy(Url, emp.X509Certificado, padraoNFSe);
                    emp.WSProxy.Add(key, wsProxy);
                }
                break;
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

        #region ValidarConfig()
        /// <summary>
        /// Verifica se algumas das informações das configurações tem algum problema ou falha
        /// </summary>
        /// <param name="validarCertificado">Se valida se tem certificado informado ou não nas configurações</param>
        public static void ValidarConfig(Empresa empresaValidar)
        {            
            empresaValidar.ErrosValidacao.Clear();

            #region Verificar se o certificado foi informado
            string aplicacao = "NF-e";
            switch (Propriedade.TipoAplicativo)
            {
                case TipoAplicativo.Nfse:
                    aplicacao = "NFS-e";
                    break;
            }
            if (empresaValidar.CertificadoInstalado && empresaValidar.CertificadoThumbPrint.Equals(string.Empty))
            {
                empresaValidar.ErrosValidacao.Add("Selecione o certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + ".\r\n" + empresaValidar.Nome + "\r\n" + empresaValidar.CNPJ);                
            }
            if (!empresaValidar.CertificadoInstalado)
            {
                if (empresaValidar.CertificadoArquivo.Equals(string.Empty))
                {
                    empresaValidar.ErrosValidacao.Add("Informe o local de armazenamento do certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + ".\r\n" + empresaValidar.Nome + "\r\n" + empresaValidar.CNPJ);                    
                }
                else if (!File.Exists(empresaValidar.CertificadoArquivo))
                {
                    empresaValidar.ErrosValidacao.Add("Arquivo do certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + " não foi encontrado.\r\n" + empresaValidar.Nome + "\r\n" + empresaValidar.CNPJ);                    
                }
                else if (empresaValidar.CertificadoSenha.Equals(string.Empty))
                {
                    empresaValidar.ErrosValidacao.Add("Informe a senha do certificado digital a ser utilizado na autenticação dos serviços da " + aplicacao + ".\r\n" + empresaValidar.Nome + "\r\n" + empresaValidar.CNPJ);                    
                }
                else
                {
                    try
                    {
                        using (FileStream fs = new FileStream(empresaValidar.CertificadoArquivo, FileMode.Open))
                        {
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);
                            empresaValidar.X509Certificado = new X509Certificate2(buffer, empresaValidar.CertificadoSenha, X509KeyStorageFlags.MachineKeySet);
                        }
                    }
                    catch (System.Security.Cryptography.CryptographicException ex)
                    {
                        empresaValidar.ErrosValidacao.Add(ex.Message + ".\r\n" + empresaValidar.Nome + "\r\n" + empresaValidar.CNPJ);                        
                    }
                    catch (Exception ex)
                    {
                        empresaValidar.ErrosValidacao.Add(ex.Message + ".\r\n" + empresaValidar.Nome + "\r\n" + empresaValidar.CNPJ);                     
                    }
                }
            }

            #endregion

            if (empresaValidar.ErrosValidacao.Count > 0)
            {
                string mensagemCompleta = "Erros de validação. Consulta a lista de erros:\r\n";
                foreach (var item in empresaValidar.ErrosValidacao)
                {
                    mensagemCompleta += "\u2022 " + item;
                }
                throw new Exception(mensagemCompleta);
            }
        }
        #endregion


        #endregion
    }
        #endregion
}
