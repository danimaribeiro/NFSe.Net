using NFSE.Net.Certificado;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net.Core
{

    /// <summary>
    /// Classe contém os dados da empresa e suas configurações
    /// </summary>
    /// <remarks>
    /// Autor: Wandrey Mundin Ferreira
    /// Data: 28/07/2010
    /// </remarks>
    public class Empresa
    {
        #region Propriedades

        #region Propriedades das pastas configuradas para utilização pelo UniNFe
        #region Ticket: #110
        /* 
         * Marcelo
         * 03/06/2013
         */
        /// <summary>
        /// Pasta base onde todas as outras estão configuradas. 
        /// <para>Diretório Root</para>
        /// </summary>
        /// <remarks>Esta propriedade tem como base a pasta envio</remarks>
        public string PastaBase
        {
            get
            {
                string result = "";

                if (!String.IsNullOrEmpty(PastaEnvio))
                {
                    string[] dirs = PastaEnvio.Split('\\');

                    for (int i = 0; i < dirs.Length - 1; i++)
                    {
                        result += dirs[i] + "\\";
                    }

                    result = result.Substring(0, result.Length - 1);
                }

                return result;
            }
        }
        #endregion

        /// <summary>
        /// Pasta onde deve ser gravado os XML´s a serem enviados
        /// </summary>
        public string PastaEnvio { get; set; }
        /// <summary>
        /// Pasta onde será gravado os XML´s de retorno para o ERP
        /// </summary>
        public string PastaRetorno { get; set; }
        /// <summary>
        /// Pasta onde será gravado os XML´s enviados
        /// </summary>
        public string PastaEnviado { get; set; }
        /// <summary>
        /// Pasta onde será gravado os XML´s que apresentaram algum tipo de erro em sua estrutura
        /// </summary>
        public string PastaErro { get; set; }
        /// <summary>
        /// Pasta onde será gravado como forma de backup os XML´s enviados
        /// </summary>
        public string PastaBackup { get; set; }
        /// <summary>
        /// Pasta onde deve ser gravado os XML´s de notas fiscais a serem enviadas em lote
        /// </summary>
        public string PastaEnvioEmLote { get; set; }
        /// <summary>
        /// Pasta onde é gravado os XML´s da NFE somente para validação
        /// </summary>
        public string PastaValidar { get; set; }
        /// <summary>
        /// Pasta para onde será gravado os XML´s de NFe para o DANFEMon fazer a impressão do DANFe
        /// </summary>
        public string PastaDanfeMon { get; set; }
        /// <summary>
        /// Pasta para onde será gravado os XML´s de download das NFe de destinatarios
        /// </summary>
        public string PastaDownloadNFeDest { get; set; }

        #endregion

        #region Propriedades diversas
        /// <summary>
        /// CNPJ da Empresa
        /// </summary>
        public string CNPJ { get; set; }
        /// <summary>
        /// Nome da Empresa
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Código da unidade Federativa da Empresa
        /// </summary>
        public int UFCod { get; set; }
        /// <summary>
        /// Ambiente a ser utilizado para a emissão da nota fiscal eletrônica
        /// </summary>
        public int tpAmb { get; set; }
        /// <summary>
        /// Tipo de emissão a ser utilizado para a emissão da nota fiscal eletrônica
        /// </summary>
        public int tpEmis { get; set; }
        /// <summary>
        /// Define a utilização do certficado instalado no windows ou através de arquivo
        /// </summary>
        public bool CertificadoInstalado { get; set; }
        /// <summary>
        /// Quando utilizar o certificado através de arquivo será necessário informar o local de armazenamento do certificado digital
        /// </summary>
        public string CertificadoArquivo { get; set; }
        /// <summary>
        /// Quando utilizar o certificado através de arquivo será necessário informar a senha do certificado
        /// </summary>
        public string CertificadoSenha { get; set; }
        /// <summary>
        /// Certificado digital - Subject
        /// </summary>
        public string Certificado { get; set; }
        /// <summary>
        /// Certificado digital - ThumbPrint
        /// </summary>
        public string CertificadoThumbPrint { get; set; }
        /// <summary>
        /// Certificado digital
        /// </summary>
        public X509Certificate2 X509Certificado { get; set; }
        /// <summary>
        /// Gravar o retorno da NFe também em TXT
        /// </summary>
        public bool GravarRetornoTXTNFe { get; set; }
        /// <summary>
        /// dias em que se deve manter os arquivos nas pastas retorno e temporario
        /// <para>coloque 0 para infinito</para>
        /// </summary>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public int DiasLimpeza { get; set; }
        /// <summary>
        /// Tempo para execução da consulta do recibo após o envio do lote
        /// </summary>
        public int TempoConsulta { get; set; }
        /// <summary>
        /// Caminho das pastas com erro no caminho dos diretorios
        /// </summary>
        public static string ErroCaminhoDiretorio { get; set; }
        /// <summary>
        /// Propriedade para exibição  de mensagem de erro referente ao erro no caminho das pastas informadas
        /// </summary>
        public static bool ExisteErroDiretorio { get; set; }
        /// <summary>
        /// Usuário de acesso ao webservice (Utilizado pelo UniNFS-e para algumas prefeituras)
        /// </summary>
        public string UsuarioWS { get; set; }
        /// <summary>
        /// Senha de acesso ao webservice (Utilizado pelo UniNFS-e para algumas prefeituras)
        /// </summary>
        public string SenhaWS { get; set; }
        /// <summary>
        /// Serviço que está sendo monitorado na configuração da empresa
        /// </summary>
        public TipoAplicativo Servico { get; set; }
        #endregion

        #region Propriedades da parte das configurações por empresa


        /// <summary>
        /// Nome da pasta onde é gravado as configurações e informações da Empresa
        /// </summary>
        public string PastaEmpresa { get; set; }
        /// <summary>
        /// Nome do arquivo XML das configurações da empresa
        /// </summary>
        public string NomeArquivoConfig { get; set; }

        public bool CriaPastasAutomaticamente { get; set; }
        public bool GravarEventosNaPastaEnviadosNFe { get; set; }
        public bool GravarEventosCancelamentoNaPastaEnviadosNFe { get; set; }
        public bool GravarEventosDeTerceiros { get; set; }
        #endregion

        #region Propriedades para controle da impressão do DANFE
        /// <summary>
        /// Pasta do executável do UniDanfe
        /// </summary>
        public string PastaExeUniDanfe { get; set; }
        /// <summary>
        /// Pasta do arquivo de configurações do UniDanfe (Tem que ser sem o \dados)
        /// </summary>
        public string PastaConfigUniDanfe { get; set; }
        /// <summary>
        /// Copiar o XML da NFe (-nfe.xml) para a pasta do danfemon? 
        /// </summary>
        public bool XMLDanfeMonNFe { get; set; }
        /// <summary>
        /// Copiar o XML de Distribuição da NFe (-procNfe.xml) para a pasta do danfemon?
        /// </summary>
        public bool XMLDanfeMonProcNFe { get; set; }
        /// <summary>
        /// Copiar o XML de denegacao da NFe (-procNfe.xml) para a pasta do danfemon?
        /// </summary>
        public bool XMLDanfeMonDenegadaNFe { get; set; }//danasa 11-4-2012
        #endregion

        #region Propriedade para controle do nome da pasta a serem salvos os XML´s enviados
        private DiretorioSalvarComo mDiretorioSalvarComo = "";
        /// <summary>
        /// Define como devem ser salvos os diretórios dentro do Uninfe.
        /// <para>por enqto apenas usa a data e os valores possíveis para definir são:</para>
        /// <para>    A para ANO</para>
        /// <para>    M para MES</para>
        /// <para>    D para DIA</para>
        /// <para>    pode se passar como desejar</para>
        /// <para>    Ex:</para>
        /// <para>        AMD   para criar a pasta como ..\Enviados\Autorizados\ANOMESDIA\nfe.xml</para>
        /// <para>        AM    para criar a pasta como ..\Enviados\Autorizados\ANOMES\nfe.xml</para>
        /// <para>        AD    para criar a pasta como ..\Enviados\Autorizados\ANODIA\nfe.xml</para>
        /// <para>        A\M\D para criar a pasta como ..\Enviados\Autorizados\ANO\MES\DIA\nfe.xml</para>
        /// <para>        podem ser criadas outras combinações, ficando a critério do usuário</para>
        /// </summary>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public DiretorioSalvarComo DiretorioSalvarComo
        {
            get
            {
                if (string.IsNullOrEmpty(mDiretorioSalvarComo))
                    mDiretorioSalvarComo = "AM";//padrão

                return mDiretorioSalvarComo;
            }

            set { mDiretorioSalvarComo = value; }
        }
        #endregion

        #endregion

        #region Coleções
        /// <summary>
        /// Configurações por empresa
        /// </summary>
        public static List<Empresa> Configuracoes = new List<Empresa>();
        /// <summary>
        /// Objetos dos serviços da NFe
        /// </summary>
        public Dictionary<string, WebServiceProxy> WSProxy = new Dictionary<string, WebServiceProxy>();
        /// <summary>
        /// Lista das threads que estão sendo executadas e o Index da empresa da thread
        /// </summary>
        //public static Dictionary<Thread, int> threads = new Dictionary<Thread, int>();
        #endregion

        /// <summary>
        /// Empresa
        /// danasa 20-9-2010
        /// </summary>
        public Empresa()
        {
            this.GravarEventosNaPastaEnviadosNFe = false;
            this.GravarEventosCancelamentoNaPastaEnviadosNFe = false;
            this.GravarEventosDeTerceiros = false;
            this.CriaPastasAutomaticamente = false;
            this.DiasLimpeza = 0;
            this.TempoConsulta = 2;
        }

        #region CriarPasta()
        /// <summary>
        /// Criar as pastas para todas as empresas cadastradas e configuradas no sistema se as mesmas não existirem
        /// </summary>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>29/09/2009</date>
        public static void CriarPasta()
        {
            if (!Directory.Exists(Propriedade.PastaGeral))
                Directory.CreateDirectory(Propriedade.PastaGeral);

            if (!Directory.Exists(Propriedade.PastaGeralRetorno))
                Directory.CreateDirectory(Propriedade.PastaGeralRetorno);

            if (!Directory.Exists(Propriedade.PastaGeralTemporaria))
                Directory.CreateDirectory(Propriedade.PastaGeralTemporaria);

            if (!Directory.Exists(Propriedade.PastaLog))
                Directory.CreateDirectory(Propriedade.PastaLog);

            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                //Criar pasta de envio
                if (!string.IsNullOrEmpty(empresa.PastaEnvio))
                {
                    if (!Directory.Exists(empresa.PastaEnvio))
                    {
                        Directory.CreateDirectory(empresa.PastaEnvio);
                    }

                    //Criar a pasta Temp dentro da pasta de envio. Wandrey 03/08/2011
                    if (!Directory.Exists(empresa.PastaEnvio.Trim() + "\\Temp"))
                    {
                        Directory.CreateDirectory(empresa.PastaEnvio.Trim() + "\\Temp");
                    }
                }

                //Criar pasta de Envio em Lote
                if (!string.IsNullOrEmpty(empresa.PastaEnvioEmLote))
                {
                    if (!Directory.Exists(empresa.PastaEnvioEmLote))
                    {
                        Directory.CreateDirectory(empresa.PastaEnvioEmLote);
                    }

                    //Criar a pasta Temp dentro da pasta de envio em lote. Wandrey 05/10/2011
                    if (!Directory.Exists(empresa.PastaEnvioEmLote.Trim() + "\\Temp"))
                    {
                        Directory.CreateDirectory(empresa.PastaEnvioEmLote.Trim() + "\\Temp");
                    }
                }

                //Criar pasta de Retorno
                if (!string.IsNullOrEmpty(empresa.PastaRetorno))
                {
                    if (!Directory.Exists(empresa.PastaRetorno))
                    {
                        Directory.CreateDirectory(empresa.PastaRetorno);
                    }
                }

                //Criar pasta Enviado
                if (!string.IsNullOrEmpty(empresa.PastaEnviado))
                {
                    if (!Directory.Exists(empresa.PastaEnviado))
                    {
                        Directory.CreateDirectory(empresa.PastaEnviado);
                    }
                }

                //Criar pasta de XML´s com erro
                if (!string.IsNullOrEmpty(empresa.PastaErro))
                {
                    if (!Directory.Exists(empresa.PastaErro))
                    {
                        Directory.CreateDirectory(empresa.PastaErro);
                    }
                }

                //Criar pasta de Backup
                if (!string.IsNullOrEmpty(empresa.PastaBackup))
                {
                    if (!Directory.Exists(empresa.PastaBackup))
                    {
                        Directory.CreateDirectory(empresa.PastaBackup);
                    }
                }

                //Criar pasta para somente validação de XML´s
                if (!string.IsNullOrEmpty(empresa.PastaValidar))
                {
                    if (!Directory.Exists(empresa.PastaValidar))
                    {
                        Directory.CreateDirectory(empresa.PastaValidar);
                    }

                    //Criar a pasta Temp dentro da pasta de envio em lote. Wandrey 05/10/2011
                    if (!Directory.Exists(empresa.PastaValidar.Trim() + "\\Temp"))
                    {
                        Directory.CreateDirectory(empresa.PastaValidar.Trim() + "\\Temp");
                    }
                }

                //Criar subpasta Assinado na pasta de envio individual de nfe
                if (!string.IsNullOrEmpty(empresa.PastaEnvio))
                {
                    if (!Directory.Exists(empresa.PastaEnvio + Propriedade.NomePastaXMLAssinado))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaEnvio + Propriedade.NomePastaXMLAssinado);
                    }
                }

                //Criar subpasta Assinado na pasta de envio em lote de nfe
                if (!string.IsNullOrEmpty(empresa.PastaEnvioEmLote))
                {
                    if (!Directory.Exists(empresa.PastaEnvioEmLote + Propriedade.NomePastaXMLAssinado))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaEnvioEmLote + Propriedade.NomePastaXMLAssinado);
                    }
                }

                //Criar pasta para monitoramento do DANFEMon e impressão do DANFE
                if (!string.IsNullOrEmpty(empresa.PastaDanfeMon))
                {
                    if (!Directory.Exists(empresa.PastaDanfeMon))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaDanfeMon);
                    }
                }

                //Criar pasta para gravar as nfe de destinatarios
                if (!string.IsNullOrEmpty(empresa.PastaDownloadNFeDest))
                {
                    if (!Directory.Exists(empresa.PastaDownloadNFeDest))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaDownloadNFeDest);
                    }
                }
            }

            Empresa.CriarSubPastaEnviado();
        }
        #endregion

        #region CriarSubPastaEnviado()
        /// <summary>
        /// Criar as subpastas (Autorizados/Denegados/EmProcessamento) dentro da pasta dos XML´s enviados para todas as empresas cadastradas e configuradas
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Date: 20/04/2010
        /// </remarks>
        private static void CriarSubPastaEnviado()
        {
            for (int i = 0; i < Empresa.Configuracoes.Count; i++)
            {
                Empresa.CriarSubPastaEnviado(i);
            }
        }
        #endregion

        #region CriarSubPastaEnviado()
        /// <summary>
        /// Criar as subpastas (Autorizados/Denegados/EmProcessamento) dentro da pasta dos XML´s enviados para a empresa passada por parâmetro
        /// </summary>
        /// <param name="indexEmpresa">Index da Empresa a ser pesquisado na coleção de configurações das empresas cadastradas</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Date: 20/04/2010
        /// </remarks>
        public static void CriarSubPastaEnviado(int indexEmpresa)
        {
            Empresa empresa = Empresa.Configuracoes[indexEmpresa];

            if (!string.IsNullOrEmpty(empresa.PastaEnviado))
            {
                //Criar a pasta EmProcessamento
                if (!Directory.Exists(empresa.PastaEnviado + "\\" + PastaEnviados.EmProcessamento.ToString()))
                {
                    System.IO.Directory.CreateDirectory(empresa.PastaEnviado + "\\" + PastaEnviados.EmProcessamento.ToString());
                }

                //Criar a Pasta Autorizado
                if (!Directory.Exists(empresa.PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString()))
                {
                    System.IO.Directory.CreateDirectory(empresa.PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString());
                }

                //Criar a Pasta Denegado
                if (!Directory.Exists(empresa.PastaEnviado + "\\" + PastaEnviados.Denegados.ToString()))
                {
                    System.IO.Directory.CreateDirectory(empresa.PastaEnviado + "\\" + PastaEnviados.Denegados.ToString());
                }
            }
        }
        #endregion

        #region CarregaConfiguracao()
        /// <summary>
        /// Carregar as configurações de todas as empresas na coleção "Configuracoes" 
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 29/07/2010
        /// </remarks>
        public static void CarregaConfiguracao()
        {
            Empresa.Configuracoes.Clear();

            if (File.Exists(Propriedade.NomeArqEmpresa))
            {
                FileStream arqXml = null;

                try
                {
                    arqXml = new FileStream(Propriedade.NomeArqEmpresa, FileMode.Open, FileAccess.Read, FileShare.Read); //Abrir um arquivo XML usando FileStream

                    var xml = new XmlDocument();
                    xml.Load(arqXml);

                    var empresaList = xml.GetElementsByTagName("Empresa");

                    foreach (XmlNode empresaNode in empresaList)
                    {
                        var empresaElemento = (XmlElement)empresaNode;

                        var registroList = xml.GetElementsByTagName("Registro");

                        for (int i = 0; i < registroList.Count; i++)
                        {
                            Empresa empresa = new Empresa();

                            var registroNode = registroList[i];
                            var registroElemento = (XmlElement)registroNode;

                            empresa.CNPJ = registroElemento.GetAttribute("CNPJ").Trim();
                            empresa.Nome = registroElemento.GetElementsByTagName("Nome")[0].InnerText.Trim();

                            empresa.Servico = TipoAplicativo.Nfe;
                            if (registroElemento.GetAttribute("Servico") != "")
                                empresa.Servico = (TipoAplicativo)Convert.ToInt16(registroElemento.GetAttribute("Servico").Trim());

                            #region Definir a pasta das configurações da empresa
                            empresa.PastaEmpresa = Propriedade.PastaExecutavel + "\\" + empresa.CNPJ.Trim();

                            switch (empresa.Servico)
                            {
                                case TipoAplicativo.Nfe:
                                    break;

                                default:
                                    empresa.PastaEmpresa += "\\" + empresa.Servico.ToString().ToLower();
                                    break;
                            }
                            #endregion

                            empresa.NomeArquivoConfig = empresa.PastaEmpresa + "\\" + Propriedade.NomeArqConfig;

                            try
                            {
                                BuscaConfiguracao(empresa);
                            }
                            catch (Exception ex)
                            {
                                ///
                                /// nao acessar o metodo Auxiliar.GravarArqErroERP(string Arquivo, string Erro) já que nela tem a pesquisa da empresa
                                /// com base em "int emp = Functions.FindEmpresaByThread();" e neste ponto ainda não foi criada
                                /// as thread's
                                string cArqErro;
                                if (string.IsNullOrEmpty(empresa.PastaRetorno))
                                    cArqErro = Path.Combine(Propriedade.PastaExecutavel, string.Format(Propriedade.NomeArqERRUniNFe, DateTime.Now.ToString("yyyyMMddTHHmmss")));
                                else
                                    cArqErro = Path.Combine(empresa.PastaRetorno, string.Format(Propriedade.NomeArqERRUniNFe, DateTime.Now.ToString("yyyyMMddTHHmmss")));

                                try
                                {
                                    //Grava arquivo de ERRO para o ERP
                                    File.WriteAllText(cArqErro, ex.Message, Encoding.Default);
                                }
                                catch { }
                            }
                            ///
                            /// mesmo com erro, adicionar a lista para que o usuário possa altera-la
                            ChecaCaminhoDiretorio(empresa);
                            Configuracoes.Add(empresa);
                        }
                    }

                    arqXml.Close();
                    arqXml = null;
                }
                finally
                {
                    if (arqXml != null)
                        arqXml.Close();
                }
            }

            if (!ExisteErroDiretorio)
                Empresa.CriarPasta();
        }
        #endregion

        #region BuscaConfiguracao()
        /// <summary>
        /// Busca as configurações da empresa dentro de sua pasta gravadas em um XML chamado UniNfeConfig.Xml
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 28/07/2010
        /// </remarks>
        private static void BuscaConfiguracao(Empresa empresa)
        {
            #region Criar diretório das configurações e dados da empresa
            if (!Directory.Exists(empresa.PastaEmpresa))
            {
                Directory.CreateDirectory(empresa.PastaEmpresa);
            }
            #endregion

            #region Limpar conteúdo dos atributos de configurações da empresa
            empresa.PastaEnvio =
                empresa.PastaRetorno =
                empresa.PastaEnviado =
                empresa.PastaErro =
                empresa.PastaBackup =
                empresa.PastaEnvioEmLote =
                empresa.PastaValidar =
                empresa.PastaDanfeMon =
                empresa.PastaExeUniDanfe =
                empresa.PastaConfigUniDanfe =
                empresa.PastaDownloadNFeDest =
                empresa.Certificado =
                empresa.CertificadoThumbPrint = string.Empty;
            empresa.X509Certificado = null;

            empresa.UFCod = 0;
            empresa.DiasLimpeza = 0;
            empresa.TempoConsulta = 2;

            empresa.UsuarioWS = string.Empty;
            empresa.SenhaWS = string.Empty;

            empresa.tpAmb = Propriedade.TipoAmbiente.taHomologacao; //2
            empresa.tpEmis = Propriedade.TipoEmissao.teNormal; //1

            empresa.GravarRetornoTXTNFe =
                empresa.GravarEventosNaPastaEnviadosNFe =
                empresa.GravarEventosCancelamentoNaPastaEnviadosNFe =
                empresa.GravarEventosDeTerceiros =
                empresa.XMLDanfeMonNFe =
                empresa.XMLDanfeMonProcNFe =
                empresa.XMLDanfeMonDenegadaNFe = false;
            empresa.DiretorioSalvarComo = "AM";

            empresa.CertificadoInstalado = true;
            empresa.CertificadoArquivo = string.Empty;
            empresa.CertificadoSenha = string.Empty;
            #endregion

            #region Carregar as configurações do XML UniNFeConfig da Empresa
            FileStream arqXml = null;

            if (File.Exists(empresa.NomeArquivoConfig))
            {
                try
                {
                    arqXml = new FileStream(empresa.NomeArquivoConfig, FileMode.Open, FileAccess.Read, FileShare.Read); //Abrir um arquivo XML usando FileStrem
                    var xml = new XmlDocument();
                    xml.Load(arqXml);

                    var configList = xml.GetElementsByTagName(NFeStrConstants.nfe_configuracoes);
                    foreach (XmlNode configNode in configList)
                    {
                        var configElemento = (XmlElement)configNode;

                        empresa.UFCod = Convert.ToInt32(Functions.LerTag(configElemento, NFeStrConstants.UnidadeFederativaCodigo, false));
                        empresa.tpAmb = Convert.ToInt32(Functions.LerTag(configElemento, NFeStrConstants.AmbienteCodigo, false));
                        empresa.tpEmis = Convert.ToInt32(Functions.LerTag(configElemento, NFeStrConstants.tpEmis, false));
                        empresa.GravarRetornoTXTNFe = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.GravarRetornoTXTNFe, "False"));
                        empresa.GravarEventosNaPastaEnviadosNFe = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.GravarEventosNaPastaEnviadosNFe, "False"));
                        empresa.GravarEventosCancelamentoNaPastaEnviadosNFe = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.GravarEventosCancelamentoNaPastaEnviadosNFe, "False"));
                        empresa.GravarEventosDeTerceiros = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.GravarEventosDeTerceiros, "False"));
                        empresa.DiretorioSalvarComo = Functions.LerTag(configElemento, NFeStrConstants.DiretorioSalvarComo, "AM");
                        empresa.DiasLimpeza = Convert.ToInt32("0" + Functions.LerTag(configElemento, NFeStrConstants.DiasLimpeza, false));
                        empresa.TempoConsulta = Convert.ToInt32("0" + Functions.LerTag(configElemento, NFeStrConstants.TempoConsulta, false));

                        empresa.PastaEnvio = Functions.LerTag(configElemento, NFeStrConstants.PastaXmlEnvio, false);
                        empresa.PastaRetorno = Functions.LerTag(configElemento, NFeStrConstants.PastaXmlRetorno, false);
                        empresa.PastaEnviado = Functions.LerTag(configElemento, NFeStrConstants.PastaXmlEnviado, false);
                        empresa.PastaErro = Functions.LerTag(configElemento, NFeStrConstants.PastaXmlErro, false);
                        empresa.PastaBackup = Functions.LerTag(configElemento, NFeStrConstants.PastaBackup, false);
                        empresa.PastaEnvioEmLote = Functions.LerTag(configElemento, NFeStrConstants.PastaXmlEmLote, false);
                        empresa.PastaValidar = Functions.LerTag(configElemento, NFeStrConstants.PastaValidar, false);
                        empresa.PastaDownloadNFeDest = Functions.LerTag(configElemento, NFeStrConstants.PastaDownloadNFeDest, false);

                        empresa.PastaExeUniDanfe = Functions.LerTag(configElemento, NFeStrConstants.PastaExeUniDanfe, false);
                        empresa.PastaConfigUniDanfe = Functions.LerTag(configElemento, NFeStrConstants.PastaConfigUniDanfe, false);
                        empresa.PastaDanfeMon = Functions.LerTag(configElemento, NFeStrConstants.PastaDanfeMon, false);
                        empresa.XMLDanfeMonNFe = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.XMLDanfeMonNFe, "False"));
                        empresa.XMLDanfeMonProcNFe = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.XMLDanfeMonProcNFe, "False"));
                        empresa.XMLDanfeMonDenegadaNFe = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.XMLDanfeMonDenegadaNFe, "False"));

                        empresa.Certificado = Functions.LerTag(configElemento, NFeStrConstants.CertificadoDigital, false);
                        empresa.CertificadoArquivo = Functions.LerTag(configElemento, NFeStrConstants.CertificadoArquivo, false);
                        empresa.CertificadoThumbPrint = Functions.LerTag(configElemento, NFeStrConstants.CertificadoDigitalThumbPrint, false);
                        empresa.CertificadoInstalado = Convert.ToBoolean(Functions.LerTag(configElemento, NFeStrConstants.CertificadoInstalado, (!string.IsNullOrEmpty(empresa.CertificadoThumbPrint) || !string.IsNullOrEmpty(empresa.Certificado)).ToString()));

                        if (!empresa.CertificadoInstalado)
                            if (configElemento.GetElementsByTagName(NFeStrConstants.CertificadoSenha)[0] != null)
                                if (!string.IsNullOrEmpty(configElemento.GetElementsByTagName(NFeStrConstants.CertificadoSenha)[0].InnerText.Trim()))
                                    empresa.CertificadoSenha = Criptografia.descriptografaSenha(configElemento.GetElementsByTagName(NFeStrConstants.CertificadoSenha)[0].InnerText.Trim());

                        empresa.UsuarioWS = Functions.LerTag(configElemento, NFeStrConstants.UsuarioWS, false);
                        empresa.SenhaWS = Functions.LerTag(configElemento, NFeStrConstants.SenhaWS, false);
                    }

                    empresa.X509Certificado = null;
                    if (empresa.CertificadoInstalado ||
                        (!empresa.CertificadoInstalado &&
                        string.IsNullOrEmpty(empresa.CertificadoArquivo) &&
                        (!string.IsNullOrEmpty(empresa.CertificadoThumbPrint) || !string.IsNullOrEmpty(empresa.Certificado))))
                    {
                        empresa.X509Certificado = BuscaConfiguracaoCertificado(empresa);

                        //Não vou mais fazer isso pois estava gerando problemas com Certificados A3 - Renan 18/06/2013
                        //empresa.CertificadoInstalado = empresa.X509Certificado != null;

                        if (!empresa.CertificadoInstalado)
                            if (!Propriedade.ExecutandoPeloUniNFe)
                                throw new Exception("Não pode acessar a lista de certificados instalados\r\nAcesse as propriedades do serviço 'UniNFeServico' e altere para ser executado com uma conta especifica");
                            else
                                throw new Exception("Não pode acessar a lista de certificados instalados");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(empresa.CertificadoArquivo) || (!string.IsNullOrEmpty(empresa.CertificadoArquivo) && !File.Exists(empresa.CertificadoArquivo)))
                            if (string.IsNullOrEmpty(empresa.CertificadoArquivo))
                                throw new Exception("Nome do certificado não definido");
                            else
                                throw new Exception(string.Format("Certificado \"{0}\" não encontrado", empresa.CertificadoArquivo));

                        //Leandro Souza - http://leonelfraga.com/neomatrixtech/?p=486
                        //Utilizar o certificado sem instalação
                        using (FileStream fs = new FileStream(empresa.CertificadoArquivo, FileMode.Open))
                        {
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);
                            empresa.X509Certificado = new X509Certificate2(buffer, empresa.CertificadoSenha);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Não vou mais fazer isso pois estava gerando problemas com Certificados A3 - Renan 18/06/2013
                    //empresa.Certificado = string.Empty;
                    //empresa.CertificadoThumbPrint = string.Empty;
                    throw new Exception("Ocorreu um erro ao efetuar a leitura das configurações da empresa " + empresa.Nome.Trim() + ". Por favor entre na tela de configurações desta empresa e reconfigure.\r\n\r\nErro: " + ex.Message);
                }
                finally
                {
                    if (arqXml != null)
                        arqXml.Close();
                }
            }
            #endregion
        }
        #endregion

        #region Reset certificado
        /// <summary>
        /// Reseta o certificado da empresa e recria o mesmo
        /// </summary>
        /// <param name="index">identificador da empresa</param>
        /// <returns></returns>
        public static X509Certificate2 ResetCertificado(int index)
        {
            Empresa empresa = Empresa.Configuracoes[index];

            empresa.X509Certificado.Reset();


            empresa.X509Certificado = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Ajustar o certificado digital de String para o tipo X509Certificate2
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = null;
            if (!string.IsNullOrEmpty(empresa.CertificadoThumbPrint))
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByThumbprint, empresa.CertificadoThumbPrint, false);
            else
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, empresa.Certificado, false);

            for (int i = 0; i < collection1.Count; i++)
            {
                //Verificar a validade do certificado
                if (DateTime.Compare(DateTime.Now, collection1[i].NotAfter) == -1)
                {
                    empresa.X509Certificado = collection1[i];
                    break;
                }
            }

            //Se não encontrou nenhum certificado com validade correta, vou pegar o primeiro certificado, porem vai travar na hora de tentar enviar a nota fiscal, por conta da validade. Wandrey 06/04/2011
            if (empresa.X509Certificado == null && collection1.Count > 0)
                empresa.X509Certificado = collection1[0];

            return empresa.X509Certificado;

        }
        #endregion

        #region BuscaConfiguracaoCertificado
        public static X509Certificate2 BuscaConfiguracaoCertificado(Empresa empresa)
        {
            //Ajustar o certificado digital de String para o tipo X509Certificate2
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = null;
            if (!string.IsNullOrEmpty(empresa.CertificadoThumbPrint))
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByThumbprint, empresa.CertificadoThumbPrint, false);
            else
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, empresa.Certificado, false);

            for (int i = 0; i < collection1.Count; i++)
            {
                //Verificar a validade do certificado
                if (DateTime.Compare(DateTime.Now, collection1[i].NotAfter) == -1)
                {
                    empresa.X509Certificado = collection1[i];
                    break;
                }
            }

            //Se não encontrou nenhum certificado com validade correta, vou pegar o primeiro certificado, porem vai travar na hora de tentar enviar a nota fiscal, por conta da validade. Wandrey 06/04/2011
            if (empresa.X509Certificado == null && collection1.Count > 0)
                empresa.X509Certificado = collection1[0];

            return empresa.X509Certificado;

        }

        #endregion

        #region FindConfEmpresa()
        /// <summary>
        /// Procurar o cnpj na coleção das empresas
        /// </summary>
        /// <param name="cnpj">CNPJ a ser pesquisado</param>
        /// <param param name="servico">Serviço a ser pesquisado</param>
        /// <returns>objeto empresa localizado, null se nada for localizado</returns>
        public static Empresa FindConfEmpresa(string cnpj, TipoAplicativo servico)
        {
            Empresa retorna = null;
            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                if (empresa.CNPJ.Equals(cnpj) && empresa.Servico.Equals(servico))
                {
                    retorna = empresa;
                    break;
                }
            }

            return retorna;
        }
        #endregion

        #region FindConfEmpresaIndex()
        /// <summary>
        /// Procurar o cnpj na coleção das empresas
        /// </summary>
        /// <param name="cnpj">CNPJ a ser pesquisado</param>
        /// <param name="servico">Serviço a ser pesquisado</param>
        /// <returns>Retorna o index do objeto localizado ou null se nada for localizado</returns>
        public static int FindConfEmpresaIndex(string cnpj, TipoAplicativo servico)
        {
            int retorna = -1;

            for (int i = 0; i < Empresa.Configuracoes.Count; i++)
            {
                Empresa empresa = Empresa.Configuracoes[i];

                if (empresa.CNPJ.Equals(cnpj) && empresa.Servico.Equals(servico))
                {
                    retorna = i;
                    break;
                }
            }

            return retorna;
        }
        #endregion

        #region ChecaCaminhoDiretorio
        /// <summary>
        /// Método para checagem dos caminhos e sua existencia ou não no pc
        /// </summary>
        /// <param name="empresa">Empresa a ser validado os caminhos das pastas</param>
        private static void ChecaCaminhoDiretorio(Empresa empresa)
        {
            FileStream arqXml = null;

            if (File.Exists(empresa.NomeArquivoConfig))
            {
                try
                {
                    arqXml = new FileStream(empresa.NomeArquivoConfig, FileMode.Open, FileAccess.Read, FileShare.Read); //Abrir um arquivo XML usando FileStrem
                    var xml = new XmlDocument();
                    xml.Load(arqXml);
                    var configList = xml.GetElementsByTagName(NFeStrConstants.nfe_configuracoes);
                    foreach (XmlNode configNode in configList)
                    {
                        var configElemento = (XmlElement)configNode;

                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaXmlEnvio, "Pasta onde serão gravados os arquivos XML´s a serem enviados individualmente para os WebServices", true);
                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaXmlEmLote, "Pasta onde serão gravados os arquivos XML´s de NF-e a serem enviadas em lote para os WebServices", false);
                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaXmlRetorno, "Pasta onde serão gravados os arquivos XML´s de retorno dos WebServices", true);
                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaXmlEnviado, "Pasta onde serão gravados os arquivos XML´s enviados", true);
                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaXmlErro, "Pasta para arquivamento temporário dos XML´s que apresentaram erro na tentativa do envio", true);
                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaBackup, "Pasta para Backup dos XML´s enviados", false);
                        verificaPasta(empresa, configElemento, NFeStrConstants.PastaValidar, "Pasta onde serão gravados os arquivos XML´s a serem somente validados", true);
                        //verificaPasta(empresa, configElemento, NFeStrConstants.PastaDownloadNFeDest, "Pasta onde serão gravados os arquivos XML´s de download de NFe de destinatários e eventos de terceiros", true);
                    }
                }
                catch
                {
                }
                finally
                {
                    if (arqXml != null)
                        arqXml.Close();
                }
            }
        }

        private static void verificaPasta(Empresa empresa, XmlElement configElemento, string tagName, string descricao, bool isObrigatoria)
        {
            XmlNode node = configElemento.GetElementsByTagName(tagName)[0];
            if (node != null)
            {
                if (!isObrigatoria && node.InnerText.Trim() == "")
                    return;

                if (isObrigatoria && node.InnerText.Trim() == "")
                {
                    Empresa.ExisteErroDiretorio = true;
                    ErroCaminhoDiretorio += "Empresa: " + empresa.Nome + "   : \"" + descricao + "\"\r\n";
                }
                else
                    if (!Directory.Exists(node.InnerText.Trim()) && node.InnerText.Trim() != "")
                    {
                        Empresa.ExisteErroDiretorio = true;
                        ErroCaminhoDiretorio += "Empresa: " + empresa.Nome + "   Pasta: " + node.InnerText.Trim() + "\r\n";
                    }
            }
            else
            {
                if (isObrigatoria)
                {
                    Empresa.ExisteErroDiretorio = true;
                    ErroCaminhoDiretorio += "Empresa: " + empresa.Nome + "   : \"" + descricao + "\"\r\n";
                }
            }
        }
        #endregion

        #region Ticket: #110
        /* Validação do arquivo de lock
         * Marcelo
         * 03/06/2013
         */
        /// <summary>
        /// Exclui o arquivo de lock associado a esta empresa/ instancia
        /// </summary>
        public void DeleteLockFile()
        {
            string file = String.Format("{0}\\{1}-{2}.lock", PastaBase, Propriedade.NomeAplicacao, Environment.MachineName);
            FileInfo fi = new FileInfo(file);

            if (fi.Exists)
                fi.Delete();
        }


        /// <summary>
        /// Cria os arquivos de lock para os diretórios de envio que esta instância vai atender.
        /// <param name="clearIfExist">Se verdadeiro, irá excluir os arquivos existentes antes de recriar</param>
        /// </summary>
        public static void CreateLockFile(bool clearIfExist = false)
        {
            if (Empresa.Configuracoes == null || Empresa.Configuracoes.Count == 0) return;

            if (clearIfExist) ClearLockFiles(false);

            IEnumerable<string> diretorios = (from d in Empresa.Configuracoes
                                              select d.PastaBase);

            foreach (string dir in diretorios)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    string file = String.Format("{0}\\{1}-{2}.lock", dir, Propriedade.NomeAplicacao, Environment.MachineName);
                    FileInfo fi = new FileInfo(file);

                    using (StreamWriter sw = new StreamWriter(file, false)
                    {
                        AutoFlush = true
                    })
                    {
                        sw.WriteLine("Iniciado em: {0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);
                        sw.WriteLine("Estação: {0}", Environment.MachineName);
                        sw.WriteLine("IP: {0}", Functions.GetIPAddress());
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Exclui todos os arquivos de lock existentes nas configurações de pasta das empresas
        /// <param name="confirm">Se verdadeiro confirma antes de apagar os arquivos</param>
        /// </summary>
        public static bool ClearLockFiles(bool confirm = true)
        {
            if (Empresa.Configuracoes == null || Empresa.Configuracoes.Count == 0) return true;

            bool result = false;

            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                empresa.DeleteLockFile();
            }
            return true;
        }
        #endregion

    }
}
