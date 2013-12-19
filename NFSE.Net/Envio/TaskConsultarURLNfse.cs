using NFSE.Net.Certificado;
using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Envio
{
    public class TaskConsultarURLNfse : TaskAbst
    {
        #region Objeto com os dados do XML da consulta nfse
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da consulta nfse
        /// </summary>
        private DadosPedSitNfse oDadosPedURLNfse;
        #endregion

        #region Execute
        public override void Execute()
        {
            int emp = Functions.FindEmpresaByThread();

            //Definir o serviço que será executado para a classe
            Servico = Servicos.ConsultarURLNfse;

            try
            {
                oDadosPedURLNfse = new DadosPedSitNfse(emp);
                //Ler o XML para pegar parâmetros de envio
                PedURLNfse(NomeArquivoXML);

                //Criar objetos das classes dos serviços dos webservices do SEFAZ
                WebServiceProxy wsProxy = null;
                object pedURLNfse = null;
                string cabecMsg = "";
                PadroesNFSe padraoNFSe = Functions.PadraoNFSe(/*ler.*/oDadosPedURLNfse.cMunicipio);
                switch (padraoNFSe)
                {
                    case PadroesNFSe.ISSNET:
                    case PadroesNFSe.GIF:
                        wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, oDadosPedURLNfse.cMunicipio, oDadosPedURLNfse.tpAmb, oDadosPedURLNfse.tpEmis);
                        pedURLNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedURLNfse.cMunicipio));
                        break;

                    default:
                        throw new Exception("Não foi possível detectar o padrão da NFS-e.");
                }

                //Assinar o XML
                AssinaturaDigital ad = new AssinaturaDigital();
                ad.Assinar(NomeArquivoXML, emp, Convert.ToInt32(oDadosPedURLNfse.cMunicipio));

                //Invocar o método que envia o XML para o SEFAZ
                oInvocarObj.InvocarNFSe(wsProxy, pedURLNfse, NomeMetodoWS(Servico, oDadosPedURLNfse.cMunicipio), cabecMsg, this, "-ped-urlnfse", "-urlnfse", padraoNFSe, Servico);
            }
            catch (Exception ex)
            {
                try
                {
                    //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                    TFunctions.GravarArqErroServico(NomeArquivoXML, Propriedade.ExtEnvio.PedURLNfse, Propriedade.ExtRetorno.Urlnfse_ERR, ex);
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

        #region PedURLNfse()
        /// <summary>
        /// Fazer a leitura do conteúdo do XML de consulta nfse por numero e disponibiliza conteúdo em um objeto para analise
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML que é para efetuar a leitura</param>
        private void PedURLNfse(string arquivoXML)
        {
            int emp = Functions.FindEmpresaByThread();
        }
        #endregion
    }
}
