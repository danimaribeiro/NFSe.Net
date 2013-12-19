using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    /// <summary>
    /// Propriedades publicas
    /// </summary>
    public class Propriedade
    {
        public static Assembly AssemblyEXE;

        public static bool ExecutandoPeloUniNFe = true;

        public static string ServiceName = "UniNFeServico";

        public static string[] tpEmissao = 
            { 
                "", 
                "Normal", 
                "Contingência com formulário de segurança (FS)", 
                "Contingência com SCAN do Ambiente Nacional", 
                "Contingência com DPEC", 
                "Contingência com formulário de segurança (FS-DA)",
                "",
                "Contingência com SVC-RS",
                "Contingência com SVC-SP"
            };

        /// <summary>
        /// Nome para a pasta dos XML assinados
        /// </summary>
        public const string NomePastaXMLAssinado = "\\Assinado";
        public const string NomeArqERRUniNFe = "UniNFeErro_{0}.err";
        /// <summary>
        /// Nome do arquivo XML de configurações
        /// </summary>
        public const string NomeArqConfig = "UniNfeConfig.xml";
        /// <summary>
        /// Nome do arquivo XML de configurações da tela de sobre
        /// </summary>
        public const string NomeArqConfigSobre = "UniNfeSobre.xml";
        /// <summary>
        /// Nome do arquivo XML que é gravado as empresas cadastradas
        /// </summary>
        public static readonly string NomeArqEmpresa = Propriedade.PastaExecutavel + "\\UniNfeEmpresa.xml";
        /// <summary>     
        /// Nome do arquivo para controle da numeração sequencial do lote.
        /// </summary>
        public const string NomeArqXmlLote = "UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo 1 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        public const string NomeArqXmlLoteBkp1 = "Bkp1_UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo 2 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        public const string NomeArqXmlLoteBkp2 = "Bkp2_UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo 3 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        public const string NomeArqXmlLoteBkp3 = "Bkp3_UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo que grava as notas fiscais em fluxo de envio
        /// </summary>
        public const string NomeArqXmlFluxoNfe = "fluxonfe.xml";

        /// <summary>
        /// Retorna o nome do XML dos municipios
        /// </summary>
        public static string NomeArqXMLMunicipios
        {
            get { return Propriedade.PastaExecutavel + "\\UniNFeMunic.xml"; }
        }

        /// <summary>
        /// Retorna a pasta do executável
        /// </summary>
        /// <returns>Retorna a pasta onde está o executável</returns>
        public static string PastaExecutavel
        {
            get { return System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory); }
        }

        #region Pastas de comunicação geral do ERP com o UniNFe
        /// <summary>
        /// Pasta de comunicação geral do ERP com o UniNFe (Envio)
        /// </summary>
        public static string PastaGeral
        {
            get { return Propriedade.PastaExecutavel + "\\Geral"; }
        }

        /// <summary>
        /// Pasta de comunicação geral do ERP com o UniNFe (Temporária)
        /// </summary>
        public static string PastaGeralTemporaria
        {
            get { return Propriedade.PastaGeral + "\\Temp"; }
        }

        /// <summary>
        /// Pasta de comunicação geral do ERP com o UniNFe (Retornos)
        /// </summary>
        public static string PastaGeralRetorno
        {
            get { return Propriedade.PastaGeral + "\\Retorno"; }
        }
        #endregion

        /// <summary>
        /// Retorna o XML contendos as definicoes dos webservices
        /// </summary>
        public static string NomeArqXMLWebService
        {
            get { return Propriedade.PastaExecutavel + "\\WSDL\\Webservice.xml"; }
        }


        public static TipoAplicativo TipoAplicativo { get; set; }

        public static List<Municipio> Municipios { get; set; }

        public static string[,] CodigosEstados
        {
            get
            {
                return new string[,] 
                {
                    { "12", "AC - Acre" },
                    { "27", "AL - Alagoas" },
                    { "16", "AP - Amapá" },
                    { "13", "AM - Amazonas" },
                    { "29", "BA - Bahia" },
                    { "23", "CE - Ceará" },
                    { "53", "DF - Distrito Federal" },
                    { "32", "ES - Espirito Santo" },
                    { "52", "GO - Goiás" },
                    { "21", "MA - Maranhão" },
                    { "51", "MT - Mato Grosso" },
                    { "50", "MS - Mato Grosso do Sul" },
                    { "31", "MG - Minas Gerais" },
                    { "15", "PA - Pará" },
                    { "25", "PB - Paraiba" },
                    { "41", "PR - Paraná" },
                    { "26", "PE - Pernambuco" },
                    { "22", "PI - Piauí" },
                    { "33", "RJ - Rio de Janeiro" },
                    { "24", "RN - Rio Grande do Norte" },
                    { "43", "RS - Rio Grande do Sul" },
                    { "11", "RO - Rondonia" },
                    { "14", "RR - Roraima" },
                    { "42", "SC - Santa Catarina" },
                    { "35", "SP - São Paulo" },
                    { "28", "SE - Sergipe" },
                    { "17", "TO - Tocantins" }
                };
            }
        }

        /// <summary>
        /// Retorna a pasta onde são gravados os log´s do UniNFe
        /// </summary>
        /// <returns>Pasta de log</returns>
        public static string PastaLog
        {
            get { return PastaExecutavel + "\\log"; }
        }

        /// <summary>
        /// Retorna a pasta dos schemas para validar os XML´s
        /// </summary>
        /// <returns></returns>
        public static string PastaSchemas
        {
            get { return PastaExecutavel + "\\schemas"; }
        }

        /// <summary>
        /// Namespace URI associado (Endereço http dos schemas de XML)
        /// </summary>
        public static string nsURI { get; set; }

        #region Propriedades com as extensões dos XML ou TXT de envio
        /// <summary>
        /// Classe com as propriedades com as extensões dos XML ou TXT de envio
        /// </summary>
        public class ExtEnvio
        {
            #region Extensões gerais
            public const string ConsCertificado = "-cons-certificado.xml";
            /// <summary>
            /// -alt-con.xml
            /// </summary>
            public const string AltCon_XML = "-alt-con.xml";
            /// <summary>
            /// -alt-con.txt
            /// </summary>
            public const string AltCon_TXT = "-alt-con.txt";
            /// <summary>
            /// -cons-inf.xml
            /// </summary>
            public const string ConsInf_XML = "-cons-inf.xml";
            /// <summary>
            /// -cons-inf.txt
            /// </summary>
            public const string ConsInf_TXT = "-cons-inf.txt";
            #endregion

            #region Extensões da NFS-e
            /// <summary>
            /// -env-loterps.xml X -ret-loterps.xml
            /// </summary>
            public const string EnvLoteRps = "-env-loterps.xml";
            /// <summary>
            /// -ped-cannfse.xml
            /// </summary>
            public const string PedCanNfse = "-ped-cannfse.xml";
            /// <summary>
            /// -ped-loterps.xml
            /// </summary>
            public const string PedLoteRps = "-ped-loterps.xml";
            /// <summary>
            /// -ped-sitloterps.xml
            /// </summary>
            public const string PedSitLoteRps = "-ped-sitloterps.xml";
            /// <summary>
            /// -ped-sitnfserps.xml
            /// </summary>
            public const string PedSitNfseRps = "-ped-sitnfserps.xml";
            /// <summary>
            /// -ped-sitnfse.xml
            /// </summary>
            public const string PedSitNfse = "-ped-sitnfse.xml";
            /// <summary>
            /// -ped-urlnfse.xml x -urlnfse.xml
            /// </summary>
            public const string PedURLNfse = "-ped-urlnfse.xml";
            /// <summary>
            /// -env-canc.xml
            /// </summary>
            public const string EnvCancelamento_XML = "-env-canc.xml";
            #endregion
        }
        #endregion

        #region Propriedades com as extensões dos XML ou TXT de retorno
        /// <summary>
        /// Classe com as propriedades com as extensões dos XML ou TXT de retorno
        /// </summary>
        public class ExtRetorno
        {
            #region Extensões NFSe
            /// <summary>
            /// -ret-loterps.xml X -env-loterps.xml
            /// </summary>
            public const string RetLoteRps = "-ret-loterps.xml";
            /// <summary>
            /// -ret-loterps.err X -env-loterps.xml
            /// </summary>
            public const string RetLoteRps_ERR = "-ret-loterps.err";
            /// <summary>
            /// -cannfse.xml
            /// </summary>
            public const string CanNfse = "-cannfse.xml";
            /// <summary>
            /// -cannfse.err
            /// </summary>
            public const string CanNfse_ERR = "-cannfse.err";
            /// <summary>
            /// -loterps.xml
            /// </summary>
            public const string LoteRps = "-loterps.xml";
            /// <summary>
            /// -lotrps.err
            /// </summary>
            public const string LoteRps_ERR = "-loterps.err";
            /// <summary>
            /// -sitloterps.xml
            /// </summary>
            public const string SitLoteRps = "-sitloterps.xml";
            /// <summary>
            /// -sitloterps.err
            /// </summary>
            public const string SitLoteRps_ERR = "-sitloterps.err";
            /// <summary>
            /// -sitnfse.xml
            /// </summary>
            public const string SitNfse = "-sitnfse.xml";
            /// <summary>
            /// -sitnfse.err
            /// </summary>
            public const string SitNfse_ERR = "-sitnfse.err";
            /// <summary>
            /// -sitnfserps.xml
            /// </summary>
            public const string SitNfseRps = "-sitnfserps.xml";
            /// <summary>
            /// -sitnfserps.err
            /// </summary>
            public const string SitNfseRps_ERR = "-sitnfserps.err";
            /// <summary>
            /// -urlnfse.xml x -ped-urlnfse.xml
            /// </summary>
            public const string Urlnfse = "-urlnfse.xml";
            /// <summary>
            /// -urlnfse.xml x -ped-urlnfse.xml x -urlnfse.err
            /// </summary>
            public const string Urlnfse_ERR = "-urlnfse.err";
            #endregion         
        }
        #endregion

        #region Classe dos tipos de emissão da NFe
        /// <summary>
        /// Tipo de emissão da NFe - danasa 8-2009
        /// </summary>
        public class TipoEmissao
        {
            /// <summary>
            /// Tipo de emissão Normal
            /// </summary>
            public const int teNormal = 1;
            /// <summary>
            /// Tipo de emissão Contingência FS (Formulário de Segurança)
            /// </summary>
            public const int teContingencia = 2;
            /// <summary>
            /// Tipo de emissão Contingência SCAN (Sistema de contingência Ambiente Nacional)
            /// </summary>
            public const int teSCAN = 3;
            /// <summary>
            /// Tipo de emissão Contingência DPEC (Declaração Prévia de Emissão em Contingência)
            /// </summary>
            public const int teDPEC = 4;
            /// <summary>
            /// Tipo de emissão Contingência FSDA (Formulário de Segurança ... )
            /// </summary>
            public const int teFSDA = 5;
            /// <summary>
            /// Sistema virtual de contingência do CTe do Rio Grande do Sul
            /// </summary>
            public const int teSVCRS = 7;
            /// <summary>
            /// Sistema virtual de contingência do CTe de São Paulo
            /// </summary>
            public const int teSVCSP = 8;
        }
        #endregion

        #region Classe dos tipos de ambiente da NFe
        /// <summary>
        /// Tipos de ambientes da NFe - danasa 8-2009
        /// </summary>
        public class TipoAmbiente
        {
            public const int taProducao = 1;
            public const int taHomologacao = 2;
        }
        #endregion

        #region NomeAplicacao
        /// <summary>
        /// Retorna o nome do aplicativo 
        /// </summary>
        /// <param name="oAssembly">Passar sempre: Assembly.GetExecutingAssembly() pois ele vai pegar o Assembly do EXE ou DLL de onde está sendo chamado o método</param>
        /// <returns>string contendo o nome do Aplicativo</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>31/07/2009</date>
        public static string NomeAplicacao
        {
            get
            {
                //Montar o nome da aplicação
                string Produto = string.Empty;

                foreach (Attribute attr in Attribute.GetCustomAttributes(AssemblyEXE))
                {
                    if (attr.GetType() == typeof(AssemblyProductAttribute))
                    {
                        Produto = ((AssemblyProductAttribute)attr).Product;
                        break;
                    }
                }

                return Produto;
            }
        }
        #endregion

        #region Versao
        /// <summary>
        /// Retorna a versão do aplicativo 
        /// </summary>
        public static string Versao
        {
            get
            {
                //Montar a versão do programa
                string versao;

                Assembly _assembly = AssemblyEXE;
                if (!ExecutandoPeloUniNFe)
                    //danasa 22/7/2011
                    //se o servico está sendo executado, pega a versão do 'uninfe.exe'
                    _assembly = System.Reflection.Assembly.LoadFile(Path.Combine(PastaExecutavel, "uninfe.exe"));

                foreach (Attribute attr in Attribute.GetCustomAttributes(_assembly))
                {
                    if (attr.GetType() == typeof(AssemblyVersionAttribute))
                    {
                        versao = ((AssemblyVersionAttribute)attr).Version;
                        break;
                    }
                }
                string delimStr = ",=";
                char[] delimiter = delimStr.ToCharArray();
                string[] strAssembly = _assembly.ToString().Split(delimiter);
                versao = strAssembly[2];

                return versao;
            }
        }
        #endregion
             
        /// <summary>
        /// Retorna o XML para salvar os parametros das telas
        /// </summary>
        public static string NomeArqXMLParams
        {
            get
            {
                return PastaExecutavel + "\\UniNFeParams.xml";
            }
        }

        #region Atributos
        /// <summary>
        /// Se pode encerrar a aplicação ou não
        /// </summary>
        public static bool EncerrarApp = false;
        #endregion

        #region DescricaoAplicacao
        /// <summary>
        /// Retorna a descrição da aplicação
        /// </summary>
        /// <returns>Descrição da aplicação</returns>
        public static string DescricaoAplicacao
        {
            get
            {
                //Montar o nome da aplicação
                string descricao = string.Empty;

                foreach (Attribute attr in Attribute.GetCustomAttributes(AssemblyEXE))
                {
                    if (attr.GetType() == typeof(AssemblyDescriptionAttribute))
                    {
                        descricao = ((AssemblyDescriptionAttribute)attr).Description;
                        break;
                    }
                }

                return descricao;
            }
        }
        #endregion
    }

    public class NFeStrConstants
    {
        public static string nfe_configuracoes = "nfe_configuracoes";
        public static string PastaXmlAssinado = "PastaXmlAssinado";
        public static string PastaXmlEnvio = "PastaXmlEnvio";
        public static string PastaXmlEmLote = "PastaXmlEmLote";
        public static string PastaXmlRetorno = "PastaXmlRetorno";
        public static string PastaXmlEnviado = "PastaXmlEnviado";
        public static string PastaXmlErro = "PastaXmlErro";
        public static string PastaBackup = "PastaBackup";
        public static string PastaValidar = "PastaValidar";
        public static string PastaDownloadNFeDest = "PastaDownloadNFeDest";
        public static string PastaExeUniDanfe = "PastaExeUniDanfe";
        public static string PastaConfigUniDanfe = "PastaConfigUniDanfe";
        public static string PastaDanfeMon = "PastaDanfeMon";
        public static string XMLDanfeMonNFe = "XMLDanfeMonNFe";
        public static string XMLDanfeMonProcNFe = "XMLDanfeMonProcNFe";
        public static string XMLDanfeMonDenegadaNFe = "XMLDanfeMonDenegadaNFe";

        public static string UsuarioWS = "UsuarioWS";
        public static string SenhaWS = "SenhaWS";

        public static string FTPAtivo = "FTPAtivo";
        public static string FTPGravaXMLPastaUnica = "FTPGravaXMLPastaUnica";
        public static string FTPSenha = "FTPSenha";
        public static string FTPPastaAutorizados = "FTPPastaAutorizados";
        public static string FTPPastaRetornos = "FTPPastaRetornos";
        public static string FTPNomeDoUsuario = "FTPNomeDoUsuario";
        public static string FTPNomeDoServidor = "FTPNomeDoServidor";
        public static string FTPPorta = "FTPPorta";

        public static string CertificadoDigital = "CertificadoDigital";
        public static string CertificadoDigitalThumbPrint = "CertificadoDigitalThumbPrint";
        public static string CertificadoInstalado = "CertificadoInstalado";
        public static string CertificadoArquivo = "CertificadoArquivo";
        public static string CertificadoSenha = "CertificadoSenha";

        public static string AmbienteCodigo = "AmbienteCodigo";
        public static string DiasLimpeza = "DiasLimpeza";
        public static string DiretorioSalvarComo = "DiretorioSalvarComo";
        public static string GravarRetornoTXTNFe = "GravarRetornoTXTNFe";
        public static string GravarEventosNaPastaEnviadosNFe = "GravarEventosNaPastaEnviadosNFe";
        public static string GravarEventosCancelamentoNaPastaEnviadosNFe = "GravarEventosCancelamentoNaPastaEnviadosNFe";
        public static string GravarEventosDeTerceiros = "GravarEventosDeTerceiros";
        public static string TempoConsulta = "TempoConsulta";
        public static string tpEmis = "tpEmis";
        public static string tpAmb = "tpAmb";
        public static string UnidadeFederativaCodigo = "UnidadeFederativaCodigo";
    }
}
