using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    /// <summary>
    /// Esta classe utiliza métodos POST para fazer requisições
    /// </summary>
    public class POSTRequest : IDisposable
    {
        /// <summary>
        /// Proxy para ser utilizado na requisição, pode ser nulo
        /// </summary>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// Faz o post e retorna uma string  com o resultado
        /// </summary>
        /// <param name="url">url base para utilizar dentro do post</param>
        /// <param name="postData">dados a serem enviados junto com o post</param>
        /// <returns></returns>
        public string PostForm(string url, IDictionary<string, string> postData)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            string file = postData["f1"];

            #region Preparar a requisição
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials =
            System.Net.CredentialCache.DefaultCredentials;

            if (Proxy != null)
                request.Proxy = Proxy;
            #endregion

            #region Crar o stream da solicitação
            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            foreach (KeyValuePair<string, string> keyValue in postData)
            {
                string formitem = string.Format(formdataTemplate, keyValue.Key, keyValue.Value);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }

            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

            string header = string.Format(headerTemplate, "f1", file);

            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            memStream.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open,
            FileAccess.Read);
            byte[] buffer = new byte[1024];

            int bytesRead = 0;

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }

            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            fileStream.Close();

            request.ContentLength = memStream.Length;
            #endregion

            #region Escrever na requisição
            Stream requestStream = request.GetRequestStream();

            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();
            #endregion

            #region Resposta do servidor
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(response.ContentType.Substring(response.ContentType.IndexOf("charset=") + 8)));
            string result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;
            #endregion
        }

        public void Dispose()
        {
            Proxy = null;
        }
    }
}
