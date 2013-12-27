using NFSE.Net.Certificado;
using NFSE.Net.Core;
using NFSE.Net.Implementacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net.Envio
{
    public class TaskCancelarNfse : TaskAbst
    {
        #region Objeto com os dados do XML de cancelamento de NFS-e
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do pedido de cancelamento
        /// </summary>
        private DadosPedCanNfse oDadosPedCanNfse;
        #endregion

        #region Execute
        public override void Execute(Empresa empresa)
        {
            //Definir o serviço que será executado para a classe
            Servico = Servicos.CancelarNfse;

            oDadosPedCanNfse = new DadosPedCanNfse(empresa);
            //Ler o XML para pegar parâmetros de envio
            //LerXML ler = new LerXML();
            /*ler.*/
            PedCanNfse(empresa, NomeArquivoXML);

            //Criar objetos das classes dos serviços dos webservices do SEFAZ
            WebServiceProxy wsProxy = null;
            object pedCanNfse = null;
            string cabecMsg = "";
            //PadroesNFSe padraoNFSe = Functions.PadraoNFSe(/*ler.*/oDadosPedSitLoteRps.cMunicipio);
            PadroesNFSe padraoNFSe = Functions.PadraoNFSe(/*ler.*/oDadosPedCanNfse.cMunicipio);
            switch (padraoNFSe)
            {
                case PadroesNFSe.IPM:
                    //código da cidade da receita federal, este arquivo pode ser encontrado em ~\uninfe\doc\Codigos_Cidades_Receita_Federal.xls</para>
                    //O código da cidade está hardcoded pois ainda está sendo usado apenas para campo mourão
                    IPM ipm = new IPM(empresa.UsuarioWS, empresa.SenhaWS, 7483, empresa.PastaRetornoNFse);
                    ipm.EmitirNF(NomeArquivoXML, (TpAmb)empresa.tpAmb, true);
                    break;

                case PadroesNFSe.GINFES:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, /*ler.*/oDadosPedCanNfse.cMunicipio, /*ler.*/oDadosPedCanNfse.tpAmb, /*ler.*/oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, /*ler.*/oDadosPedCanNfse.cMunicipio));
                    cabecMsg = ""; //Cancelamento ainda tá na versão 2.0 então não tem o cabecMsg
                    break;

                case PadroesNFSe.BETHA:
                    wsProxy = new WebServiceProxy(empresa.X509Certificado);
                    wsProxy.Betha = new Betha();
                    break;

                case PadroesNFSe.THEMA:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, /*ler.*/oDadosPedCanNfse.cMunicipio, /*ler.*/oDadosPedCanNfse.tpAmb, /*ler.*/oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, /*ler.*/oDadosPedCanNfse.cMunicipio));
                    break;

                case PadroesNFSe.CANOAS_RS:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, /*ler.*/oDadosPedCanNfse.cMunicipio, /*ler.*/oDadosPedCanNfse.tpAmb, /*ler.*/oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, /*ler.*/oDadosPedCanNfse.cMunicipio));
                    cabecMsg = "<cabecalho versao=\"201001\"><versaoDados>V2010</versaoDados></cabecalho>";
                    break;

                case PadroesNFSe.ISSNET:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    break;

                case PadroesNFSe.ISSONLINE:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    break;

                case PadroesNFSe.BLUMENAU_SC:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));

                    #region Encriptar tag <Assinatura>
                    EncryptAssinatura(empresa);
                    #endregion

                    break;

                case PadroesNFSe.BHISS:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    cabecMsg = "<cabecalho xmlns=\"http://www.abrasf.org.br/nfse.xsd\" versao=\"1.00\"><versaoDados >1.00</versaoDados ></cabecalho>";
                    break;

                case PadroesNFSe.GIF:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    break;

                case PadroesNFSe.DUETO:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis, padraoNFSe);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    break;

                case PadroesNFSe.WEBISS:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis, padraoNFSe);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    cabecMsg = "<cabecalho xmlns=\"http://www.abrasf.org.br/nfse.xsd\" versao=\"1.00\"><versaoDados >1.00</versaoDados ></cabecalho>";
                    break;

                case PadroesNFSe.PAULISTANA:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));

                    #region Encriptar tag <Assinatura>
                    EncryptAssinatura(empresa);
                    #endregion
                    break;

                case PadroesNFSe.SALVADOR_BA:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    break;

                case PadroesNFSe.PRONIN:
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedCanNfse.cMunicipio, oDadosPedCanNfse.tpAmb, oDadosPedCanNfse.tpEmis, padraoNFSe);
                    pedCanNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedCanNfse.cMunicipio));
                    break;
                default:
                    throw new Exception("Não foi possível detectar o padrão da NFS-e.");
            }

            if (padraoNFSe != PadroesNFSe.IPM)
            {
                //Assinar o XML
                AssinaturaDigital ad = new AssinaturaDigital();
                ad.Assinar(NomeArquivoXML, empresa, Convert.ToInt32(/*ler.*/oDadosPedCanNfse.cMunicipio));

                //Invocar o método que envia o XML para o SEFAZ
                oInvocarObj.InvocarNFSe(wsProxy, pedCanNfse, NomeMetodoWS(Servico, /*ler.*/oDadosPedCanNfse.cMunicipio, empresa.tpAmb), cabecMsg, this, "-ped-cannfse", "-cannfse", padraoNFSe, Servico, empresa);
            }
        }
        #endregion

        #region PedCanNfse()
        /// <summary>
        /// Fazer a leitura do conteúdo do XML de cancelamento de NFS-e e disponibilizar conteúdo em um objeto para analise
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML que é para efetuar a leitura</param>
        private void PedCanNfse(Core.Empresa empresa, string arquivoXML)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(arquivoXML);

            XmlNodeList infCancList = doc.GetElementsByTagName("CancelarNfseEnvio");

            foreach (XmlNode infCancNode in infCancList)
            {
                XmlElement infCancElemento = (XmlElement)infCancNode;
            }
        }
        #endregion

        #region EncryptAssinatura()
        /// <summary>
        /// Encriptar a tag Assinatura quando for município de Blumenau - SC
        /// </summary>
        private void EncryptAssinatura(Core.Empresa empresa)
        {
            string arquivoXML = NomeArquivoXML;

            XmlDocument doc = new XmlDocument();
            doc.Load(arquivoXML);

            XmlNodeList pedidoCancelamentoNFeList = doc.GetElementsByTagName("PedidoCancelamentoNFe");

            foreach (XmlNode pedidoCancelamentoNFeNode in pedidoCancelamentoNFeList)
            {
                XmlElement pedidoCancelamentoNFeElemento = (XmlElement)pedidoCancelamentoNFeNode;

                XmlNodeList detalheList = doc.GetElementsByTagName("Detalhe");

                foreach (XmlNode detalheNode in detalheList)
                {
                    XmlElement detalheElement = (XmlElement)detalheNode;


                    if (detalheElement.GetElementsByTagName("AssinaturaCancelamento").Count != 0)
                    {
                        //Encryptar a tag Assinatura
                        detalheElement.GetElementsByTagName("AssinaturaCancelamento")[0].InnerText = Criptografia.SignWithRSASHA1(empresa.X509Certificado,
                            detalheElement.GetElementsByTagName("AssinaturaCancelamento")[0].InnerText);
                    }
                }
            }

            //Salvar o XML com as alterações efetuadas
            doc.Save(arquivoXML);
        }
        #endregion
    }
}
