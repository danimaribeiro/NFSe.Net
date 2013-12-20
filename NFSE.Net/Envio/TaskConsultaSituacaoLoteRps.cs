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
    public class TaskConsultaSituacaoLoteRps : TaskAbst
    {
        #region Objeto com os dados do XML de consulta situação do lote rps
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do pedido de consulta da situação do lote rps
        /// </summary>
        private DadosPedSitLoteRps oDadosPedSitLoteRps;
        #endregion

        #region Execute
        public override void Execute(Empresa empresa)
        {
            int emp = Functions.FindEmpresaByThread();

            //Definir o serviço que será executado para a classe
            Servico = Servicos.ConsultarSituacaoLoteRps;

            try
            {
                oDadosPedSitLoteRps = new DadosPedSitLoteRps(emp);
                //Ler o XML para pegar parâmetros de envio
                //LerXML ler = new LerXML();
                /*ler.*/
                PedSitLoteRps(NomeArquivoXML);

                //Definir o objeto do WebService

                //Criar objetos das classes dos serviços dos webservices do SEFAZ
                WebServiceProxy wsProxy = null;
                object pedSitLoteRps = null;
                string cabecMsg = "";
                PadroesNFSe padraoNFSe = Functions.PadraoNFSe(/*ler.*/oDadosPedSitLoteRps.cMunicipio);
                switch (padraoNFSe)
                {
                    case PadroesNFSe.GINFES:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, /*ler.*/oDadosPedSitLoteRps.cMunicipio, /*ler.*/oDadosPedSitLoteRps.tpAmb, /*ler.*/oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, /*ler.*/oDadosPedSitLoteRps.cMunicipio));
                        cabecMsg = "<ns2:cabecalho versao=\"3\" xmlns:ns2=\"http://www.ginfes.com.br/cabecalho_v03.xsd\"><versaoDados>3</versaoDados></ns2:cabecalho>";
                        break;

                    case PadroesNFSe.BETHA:
                        wsProxy = new WebServiceProxy(Empresa.Configuracoes[emp].X509Certificado);
                        wsProxy.Betha = new Betha();
                        break;

                    case PadroesNFSe.THEMA:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, /*ler.*/oDadosPedSitLoteRps.cMunicipio, /*ler.*/oDadosPedSitLoteRps.tpAmb, /*ler.*/oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, /*ler.*/oDadosPedSitLoteRps.cMunicipio));
                        break;

                    case PadroesNFSe.CANOAS_RS:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, /*ler.*/oDadosPedSitLoteRps.cMunicipio, /*ler.*/oDadosPedSitLoteRps.tpAmb, /*ler.*/oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, /*ler.*/oDadosPedSitLoteRps.cMunicipio));
                        cabecMsg = "<cabecalho versao=\"201001\"><versaoDados>V2010</versaoDados></cabecalho>";
                        break;

                    case PadroesNFSe.ISSNET:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        break;

                    case PadroesNFSe.BLUMENAU_SC:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        break;

                    case PadroesNFSe.BHISS:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        cabecMsg = "<cabecalho xmlns=\"http://www.abrasf.org.br/nfse.xsd\" versao=\"1.00\"><versaoDados >1.00</versaoDados ></cabecalho>";
                        break;

                    case PadroesNFSe.GIF:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        break;

                    case PadroesNFSe.DUETO:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis, padraoNFSe);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        break;

                    case PadroesNFSe.WEBISS:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis, padraoNFSe);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        cabecMsg = "<cabecalho xmlns=\"http://www.abrasf.org.br/nfse.xsd\" versao=\"1.00\"><versaoDados >1.00</versaoDados ></cabecalho>";
                        break;

                    case PadroesNFSe.PAULISTANA:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        break;

                    case PadroesNFSe.SALVADOR_BA:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedSitLoteRps.cMunicipio, oDadosPedSitLoteRps.tpAmb, oDadosPedSitLoteRps.tpEmis, padraoNFSe);
                        pedSitLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedSitLoteRps.cMunicipio));
                        break;

                    default:
                        throw new Exception("Não foi possível detectar o padrão da NFS-e.");
                }
                if (padraoNFSe != PadroesNFSe.IPM)
                {
                    //Assinar o XML
                    AssinaturaDigital ad = new AssinaturaDigital();
                    ad.Assinar(NomeArquivoXML, emp, Convert.ToInt32(/*ler.*/oDadosPedSitLoteRps.cMunicipio));

                    //Invocar o método que envia o XML para o SEFAZ
                    oInvocarObj.InvocarNFSe(wsProxy, pedSitLoteRps, NomeMetodoWS(Servico, /*ler.*/oDadosPedSitLoteRps.cMunicipio), cabecMsg, this, "-ped-sitloterps", "-sitloterps", padraoNFSe, Servico);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                    TFunctions.GravarArqErroServico(NomeArquivoXML, Propriedade.ExtEnvio.PedSitLoteRps, Propriedade.ExtRetorno.SitLoteRps_ERR, ex);
                }
                catch
                {
                    //Se falhou algo na hora de gravar o retorno .ERR (de erro) para o ERP, infelizmente não posso fazer mais nada.
                    //Wandrey 31/08/2011
                }
            }
            finally
            {
                try
                {
                    Functions.DeletarArquivo(NomeArquivoXML);
                }
                catch
                {
                    //Se falhou algo na hora de deletar o XML de cancelamento de NFe, infelizmente
                    //não posso fazer mais nada, o UniNFe vai tentar mandar o arquivo novamente para o webservice, pois ainda não foi excluido.
                    //Wandrey 31/08/2011
                }
            }
        }
        #endregion

        #region PedSitLoteRps()
        /// <summary>
        /// Fazer a leitura do conteúdo do XML de consulta situação do lote rps e disponibilizar conteúdo em um objeto para analise
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML que é para efetuar a leitura</param>
        private void PedSitLoteRps(string arquivoXML)
        {
            int emp = Functions.FindEmpresaByThread();

            XmlDocument doc = new XmlDocument();
            doc.Load(arquivoXML);

            XmlNodeList infConsList = doc.GetElementsByTagName("ConsultarSituacaoLoteRpsEnvio");

            foreach (XmlNode infConsNode in infConsList)
            {
                XmlElement infConsElemento = (XmlElement)infConsNode;
            }
        }
        #endregion

    }
}
