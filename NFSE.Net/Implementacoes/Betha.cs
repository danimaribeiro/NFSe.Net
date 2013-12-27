using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace NFSE.Net.Implementacoes
{
    public class Betha : IBetha
    {
        public IWebProxy Proxy { get; set; }

        #region CancelarNfse
        /// <summary>
        /// Cancelar Nfse
        /// </summary>
        /// <param name="xml">XML de cancelamento da Nfse</param>
        /// <returns>XML Retornado pela prefeitura</returns>
        public string CancelarNfse(XmlNode xml, int tpAmb)
        {
            StringBuilder retornar = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            string url;
            if (tpAmb == Propriedade.TipoAmbiente.taProducao)
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-ws/cancelarNfse";
            else
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-test-ws/cancelarNfse";

            string XMLRetorno = RequestWS(xml, url, "#CancelarNEV01Service");

            MemoryStream stream = Functions.StringXmlToStream(XMLRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlList = doc.GetElementsByTagName("CancelarNfseReposta");
            retornar.Append(xmlList[0].OuterXml);

            return retornar.ToString();
        }
        #endregion

        #region ConsultarLoteRps
        /// <summary>
        /// Consultar lote Rps
        /// </summary>
        /// <param name="xml">XML de consulta de lote Rps</param>
        /// <returns>XML Retornado pela prefeitura</returns>
        public string ConsultarLoteRps(XmlNode xml, int tpAmb)
        {
            StringBuilder retornar = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            string url;
            if (tpAmb == Propriedade.TipoAmbiente.taProducao)
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-ws/consultarLoteRps";
            else
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-test-ws/consultarLoteRps";

            string XMLRetorno = RequestWS(xml, url, "#ConsultarLoteRpsService");

            MemoryStream stream = Functions.StringXmlToStream(XMLRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlList = doc.GetElementsByTagName("ConsultarLoteRpsResposta");
            retornar.Append(xmlList[0].OuterXml);

            return retornar.ToString();
        }
        #endregion

        #region ConsultarSituacaoLoteRps
        /// <summary>
        /// Consultar a situação do lote Rps
        /// </summary>
        /// <param name="xml">XML de consulta da situação do lote Rps</param>
        /// <returns>XML Retornado pela prefeitura</returns>
        public string ConsultarSituacaoLoteRps(XmlNode xml, int tpAmb)
        {
            StringBuilder retornar = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            string url;
            if (tpAmb == Propriedade.TipoAmbiente.taProducao)
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-ws/consultarSituacaoLoteRps";
            else
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-test-ws/consultarSituacaoLoteRps";

            string XMLRetorno = RequestWS(xml, url, "#ConsultarSituacaoLoteRpsService");

            MemoryStream stream = Functions.StringXmlToStream(XMLRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlList = doc.GetElementsByTagName("ConsultarSituacaoLoteRpsResposta");
            retornar.Append(xmlList[0].OuterXml);

            return retornar.ToString();
        }
        #endregion

        #region RecepcionarLoteRps
        /// <summary>
        /// Consultar a situação do lote Rps
        /// </summary>
        /// <param name="xml">XML de consulta da situação do lote Rps</param>
        /// <returns>XML Retornado pela prefeitura</returns>
        public string RecepcionarLoteRps(XmlNode xml, int tpAmb)
        {
            StringBuilder retornar = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            string url;

            if (tpAmb == Propriedade.TipoAmbiente.taProducao)
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-ws/recepcionarLoteRps";
            else
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-test-ws/recepcionarLoteRps";

            string XMLRetorno = RequestWS(xml, url, "#RecepcionarLoteRpsService");

            MemoryStream stream = Functions.StringXmlToStream(XMLRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlList = doc.GetElementsByTagName("EnviarLoteRpsResposta");
            retornar.Append(xmlList[0].OuterXml);

            return retornar.ToString();
        }
        #endregion

        #region RequestWS()
        /// <summary>
        /// Conecta com o Webservice, envia o XML e obtém o retorno
        /// </summary>
        /// <param name="xml">XML a ser enviado</param>
        /// <param name="url">URL do serviço para onde deve ser enviado o XML</param>
        /// <param name="metodo">Metodo do Webservice que é para ser executado</param>
        /// <returns>Conteúdo retornado pela prefeitura</returns>
        private string RequestWS(XmlNode xml, String url, String metodo)
        {
            string XMLRetorno = string.Empty;
            string xmlSoap = Envelopar(xml);
            
            Uri uri = new Uri(url);

            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpWR = (HttpWebRequest)webRequest;
            httpWR.ContentType = "text/xml; charset=ISO-8859-1";
            httpWR.Headers.Add("SOAPAction", uri + "" + metodo);
            httpWR.Method = "POST";

            httpWR.Proxy = Proxy;

            Stream reqStream = httpWR.GetRequestStream();
            StreamWriter streamWriter = new StreamWriter(reqStream);
            streamWriter.Write(xmlSoap);
            streamWriter.Close();

            WebResponse webResponse = httpWR.GetResponse();
            Stream respStream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(respStream);

            XMLRetorno = streamReader.ReadToEnd();

            return XMLRetorno;
        }
        #endregion

        #region Envelopar()
        /// <summary>
        /// Envelopa o XML (Soap)
        /// </summary>
        /// <param name="xml">XML a ser envelopado</param>
        /// <returns>Retorna o xml envelopado</returns>
        private string Envelopar(XmlNode xml)
        {
            StringBuilder env = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            env.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:e=\""
                    + "http://www.betha.com.br/e-nota-contribuinte-ws" + "\">");
            env.Append("<soapenv:Header/>");
            env.Append("<soapenv:Body>");

            env.Append(xml.LastChild.OuterXml.ToString());

            env.Append("</soapenv:Body>");
            env.Append("</soapenv:Envelope>");

            return env.ToString();
        }
        #endregion

        #region ConsultarNfse
        /// <summary>
        /// Consultar Nfse
        /// </summary>
        /// <param name="xml">XML de consulta da Nfse</param>
        /// <returns>XML Retornado pela prefeitura</returns>
        public string ConsultarNfse(XmlNode xml, int tpAmb)
        {
            StringBuilder retornar = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            string url;
            if (tpAmb == Propriedade.TipoAmbiente.taProducao)
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-ws/consultarNfse";
            else
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-test-ws/consultarNfse";

            string XMLRetorno = RequestWS(xml, url, "#ConsultarNfseService");

            MemoryStream stream = Functions.StringXmlToStream(XMLRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlList = doc.GetElementsByTagName("ConsultarNfseResposta");
            retornar.Append(xmlList[0].OuterXml);

            return retornar.ToString();
        }
        #endregion

        #region ConsultarNfsePorRps
        /// <summary>
        /// Consultar Nfse por RPS
        /// </summary>
        /// <param name="xml">XML de consulta da Nfse por RPS</param>
        /// <returns>XML Retornado pela prefeitura</returns>
        public string ConsultarNfsePorRps(XmlNode xml, int tpAmb)
        {
            StringBuilder retornar = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            string url;
            if (tpAmb == Propriedade.TipoAmbiente.taProducao)
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-ws/consultarNfsePorRps";
            else
                url = @"http://e-gov.betha.com.br/e-nota-contribuinte-test-ws/consultarNfsePorRps";

            string XMLRetorno = RequestWS(xml, url, "#ConsultarNfsePorRpsService");

            MemoryStream stream = Functions.StringXmlToStream(XMLRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlList = doc.GetElementsByTagName("ConsultarNfseRpsResposta");
            retornar.Append(xmlList[0].OuterXml);

            return retornar.ToString();
        }
        #endregion
    }
}
