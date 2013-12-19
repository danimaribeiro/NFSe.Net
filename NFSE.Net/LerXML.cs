using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net
{
    /// <summary>
    /// Classe responsável por ler os diversos XML´s utilizados na nota fiscal eletrônica
    /// e dispor as informações em propriedades para facilitar a leitura.
    /// </summary>
    public class LerXML
    {
        #region Classes
        #region Objetos relacionados a NFS-e

        #region Objeto com os dados do XML da consulta lote rps
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da consulta lote rps
        /// </summary>
        public DadosPedLoteRps oDadosPedLoteRps = new DadosPedLoteRps(Functions.FindEmpresaByThread());
        #endregion

        #region Objeto com os dados do XML da consulta nfse por RPS
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da consulta nfse por rps
        /// </summary>
        public DadosPedSitNfseRps oDadosPedSitNfseRps = new DadosPedSitNfseRps(Functions.FindEmpresaByThread());
        #endregion

        #endregion

        #endregion

        #region Metodos

        #endregion

        #region Métodos para leitura dos XML´s da NFS-e (Nota Fiscal de Serviços Eletrônica)

        #region PedLoteRps()
        /// <summary>
        /// Fazer a leitura do conteúdo do XML de consulta lote rps e disponibilizar conteúdo em um objeto para analise
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML que é para efetuar a leitura</param>
        public void PedLoteRps(string arquivoXML)
        {
            int emp = Functions.FindEmpresaByThread();

            XmlDocument doc = new XmlDocument();
            doc.Load(arquivoXML);

            XmlNodeList infConsList = doc.GetElementsByTagName("ConsultarLoteRpsEnvio");

            foreach (XmlNode infConsNode in infConsList)
            {
                XmlElement infConsElemento = (XmlElement)infConsNode;
                oDadosPedLoteRps.Protocolo = infConsElemento.GetElementsByTagName("Protocolo")[0].InnerText;

                XmlElement infPrestadorElemento = (XmlElement)infConsElemento.GetElementsByTagName("Prestador").Item(0);
                if (infPrestadorElemento.GetElementsByTagName("tipos:Cnpj")[0] != null)
                    oDadosPedLoteRps.Cnpj = infPrestadorElemento.GetElementsByTagName("tipos:Cnpj")[0].InnerText;
                else if (infPrestadorElemento.GetElementsByTagName("Cnpj")[0] != null)
                    oDadosPedLoteRps.Cnpj = infPrestadorElemento.GetElementsByTagName("Cnpj")[0].InnerText;

                if (infPrestadorElemento.GetElementsByTagName("tipos:InscricaoMunicipal")[0] != null)
                    oDadosPedLoteRps.InscricaoMunicipal = infPrestadorElemento.GetElementsByTagName("tipos:InscricaoMunicipal")[0].InnerText;
                else if (infPrestadorElemento.GetElementsByTagName("InscricaoMunicipal")[0] != null)
                    oDadosPedLoteRps.InscricaoMunicipal = infPrestadorElemento.GetElementsByTagName("InscricaoMunicipal")[0].InnerText;
            }
        }
        #endregion


        #region PedSitNfseRps()
        /// <summary>
        /// Fazer a leitura do conteúdo do XML de consulta nfse por rps e disponibiliza conteúdo em um objeto para analise
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML que é para efetuar a leitura</param>
        public void PedSitNfseRps(string arquivoXML)
        {
            int emp = Functions.FindEmpresaByThread();
        }
        #endregion

        #endregion
    }
}
