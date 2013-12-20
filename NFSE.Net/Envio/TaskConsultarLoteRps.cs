using NFSE.Net.Certificado;
using NFSE.Net.Core;
using NFSE.Net.Implementacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Envio
{
    /// <summary>
    /// Consultar uma NFS-e por RPS
    /// </summary>
    public class TaskConsultarLoteRps : TaskAbst
    {
        public override void Execute(Empresa empresa)
        {
            int emp = Functions.FindEmpresaByThread();

            //Definir o serviço que será executado para a classe
            Servico = Servicos.ConsultarLoteRps;

            try
            {
                //Ler o XML para pegar parâmetros de envio
                LerXML ler = new LerXML();
                ler.PedSitNfseRps(NomeArquivoXML);

                //Criar objetos das classes dos serviços dos webservices do SEFAZ
                WebServiceProxy wsProxy = null;
                object pedLoteRps = null;
                string cabecMsg = "";
                PadroesNFSe padraoNFSe = Functions.PadraoNFSe(ler.oDadosPedSitNfseRps.cMunicipio);
                switch (padraoNFSe)
                {
                    case PadroesNFSe.GINFES:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        cabecMsg = "<ns2:cabecalho versao=\"3\" xmlns:ns2=\"http://www.ginfes.com.br/cabecalho_v03.xsd\"><versaoDados>3</versaoDados></ns2:cabecalho>";
                        break;

                    case PadroesNFSe.BETHA:
                        wsProxy = new WebServiceProxy(Empresa.Configuracoes[emp].X509Certificado);
                        wsProxy.Betha = new Betha();
                        break;

                    case PadroesNFSe.THEMA:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.CANOAS_RS:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        cabecMsg = "<cabecalho versao=\"201001\"><versaoDados>V2010</versaoDados></cabecalho>";
                        break;

                    case PadroesNFSe.ISSNET:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.ISSONLINE:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.BLUMENAU_SC:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.BHISS:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        cabecMsg = "<cabecalho xmlns=\"http://www.abrasf.org.br/nfse.xsd\" versao=\"1.00\"><versaoDados >1.00</versaoDados ></cabecalho>";
                        break;

                    case PadroesNFSe.GIF:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.DUETO:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis, padraoNFSe);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.WEBISS:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis, padraoNFSe);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        cabecMsg = "<cabecalho xmlns=\"http://www.abrasf.org.br/nfse.xsd\" versao=\"1.00\"><versaoDados >1.00</versaoDados ></cabecalho>";
                        break;

                    case PadroesNFSe.PAULISTANA:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        break;

                    case PadroesNFSe.SALVADOR_BA:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis, padraoNFSe);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));

                        break;

                    case PadroesNFSe.PORTOVELHENSE:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));
                        cabecMsg = "<cabecalho versao=\"2.00\" xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" xmlns=\"http://www.abrasf.org.br/nfse.xsd\"><versaoDados>2.00</versaoDados></cabecalho>";
                        break;

                    case PadroesNFSe.PRONIN:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, ler.oDadosPedSitNfseRps.cMunicipio, ler.oDadosPedSitNfseRps.tpAmb, ler.oDadosPedSitNfseRps.tpEmis, padraoNFSe);
                        pedLoteRps = wsProxy.CriarObjeto(NomeClasseWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio));

                        break;

                    default:
                        throw new Exception("Não foi possível detectar o padrão da NFS-e.");
                }
                if (padraoNFSe != PadroesNFSe.IPM)
                {
                    //Assinar o XML
                    AssinaturaDigital ad = new AssinaturaDigital();
                    ad.Assinar(NomeArquivoXML, emp, Convert.ToInt32(ler.oDadosPedSitNfseRps.cMunicipio));

                    //Invocar o método que envia o XML para o SEFAZ
                    oInvocarObj.InvocarNFSe(wsProxy, pedLoteRps, NomeMetodoWS(Servico, ler.oDadosPedSitNfseRps.cMunicipio), cabecMsg, this, "-ped-loterps", "-loterps", padraoNFSe, Servico);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                    TFunctions.GravarArqErroServico(NomeArquivoXML, Propriedade.ExtEnvio.PedLoteRps, Propriedade.ExtRetorno.LoteRps_ERR, ex);
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
    }
}
