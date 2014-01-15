using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class GerarXML
    {

        #region XmlRetorno()
        /// <summary>
        /// Grava o XML com os dados do retorno dos webservices e deleta o XML de solicitação do serviço.
        /// </summary>
        /// <param name="arquivoEnvio">Final do nome do arquivo de solicitação do serviço.</param>
        /// <param name="finalArqRetorno">Final do nome do arquivo que é para ser gravado o retorno.</param>
        /// <param name="conteudoXMLRetorno">Conteúdo do XML a ser gerado</param>
        /// <example>
        /// // Arquivo de envio: 20080619T19113320-ped-sta.xml
        /// // Arquivo de retorno que vai ser gravado: 20080619T19113320-sta.xml
        /// this.GravarXmlRetorno("-ped-sta.xml", "-sta.xml");
        /// </example>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// </remarks>        
        public void XmlRetorno(string arquivoEnvio, string salvarArquivoRetornoEm, string conteudoXMLRetorno)
        {
            StreamWriter SW = null;

            try
            {   
                SW = File.CreateText(salvarArquivoRetornoEm);
                SW.Write(conteudoXMLRetorno);
                SW.Close();
                SW = null;
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                    SW = null;
                }
            }
        }
        #endregion

      
    }
}
