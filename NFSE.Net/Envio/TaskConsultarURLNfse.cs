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
        public override void Execute(Empresa empresa)
        {
            //Definir o serviço que será executado para a classe
            Servico = Servicos.ConsultarURLNfse;

            oDadosPedURLNfse = new DadosPedSitNfse(empresa);
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
                    wsProxy = ConfiguracaoApp.DefinirWS(Servico, empresa, oDadosPedURLNfse.cMunicipio, oDadosPedURLNfse.tpAmb, oDadosPedURLNfse.tpEmis);
                    pedURLNfse = wsProxy.CriarObjeto(NomeClasseWS(Servico, oDadosPedURLNfse.cMunicipio));
                    break;

                default:
                    throw new Exception("Não foi possível detectar o padrão da NFS-e.");
            }

            //Assinar o XML
            AssinaturaDigital ad = new AssinaturaDigital();
            ad.Assinar(NomeArquivoXML, empresa, Convert.ToInt32(oDadosPedURLNfse.cMunicipio));

            //Invocar o método que envia o XML para o SEFAZ
            oInvocarObj.InvocarNFSe(wsProxy, pedURLNfse, NomeMetodoWS(Servico, oDadosPedURLNfse.cMunicipio, empresa.tpAmb), cabecMsg, this, "-ped-urlnfse", "-urlnfse", padraoNFSe, Servico, empresa);
        }
        #endregion

        #region PedURLNfse()
        /// <summary>
        /// Fazer a leitura do conteúdo do XML de consulta nfse por numero e disponibiliza conteúdo em um objeto para analise
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML que é para efetuar a leitura</param>
        private void PedURLNfse(string arquivoXML)
        {
            //TODO Fazer a leitura
        }
        #endregion
    }
}
