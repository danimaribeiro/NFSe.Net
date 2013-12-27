using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net
{
    public class Functions
    {
        #region MemoryStream
        /// <summary>
        /// Método responsável por converter uma String contendo a estrutura de um XML em uma Stream para
        /// ser lida pela XMLDocument
        /// </summary>
        /// <returns>String convertida em Stream</returns>
        /// <remarks>Conteúdo do método foi fornecido pelo Marcelo da desenvolvedores.net</remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public static MemoryStream StringXmlToStream(string strXml)
        {
            byte[] byteArray = new byte[strXml.Length];
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byteArray = encoding.GetBytes(strXml);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        public static MemoryStream StringXmlToStreamUTF8(string strXml)
        {
            byte[] byteArray = new byte[strXml.Length];
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byteArray = encoding.GetBytes(strXml);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        #endregion

        #region Move()
        /// <summary>
        /// Mover arquivo para uma determinada pasta
        /// </summary>
        /// <param name="arquivoOrigem">Arquivo de origem (arquivo a ser movido)</param>
        /// <param name="arquivoDestino">Arquivo de destino (destino do arquivo)</param>
        public static void Move(string arquivoOrigem, string arquivoDestino)
        {
            if (File.Exists(arquivoDestino))
                File.Delete(arquivoDestino);

            File.Copy(arquivoOrigem, arquivoDestino);
            //File.Delete(arquivoOrigem);
        }
        #endregion

        #region DeletarArquivo()
        /// <summary>
        /// Excluir arquivos do HD
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser excluido.</param>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public static void DeletarArquivo(string arquivo)
        {
            if (File.Exists(arquivo))
            {
                //File.Delete(arquivo);
            }
        }
        #endregion

        #region CodigoParaUF()
        public static string CodigoParaUF(int codigo)
        {
            try
            {
                for (int v = 0; v < Propriedade.CodigosEstados.Length / 2; ++v)
                    if (Propriedade.CodigosEstados[v, 0] == codigo.ToString())
                        return Propriedade.CodigosEstados[v, 1];

                return "";
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region PadraoNFe()
        public static PadroesNFSe PadraoNFSe(int municipio)
        {
            foreach (Municipio mun in Propriedade.Municipios)
                if (mun.CodigoMunicipio == municipio)
                    return mun.Padrao;

            return PadroesNFSe.NaoIdentificado;
        }
        #endregion

        #region Gerar MD5
        public static string GerarMD5(string valor)
        {
            // Cria uma nova intância do objeto que implementa o algoritmo para
            // criptografia MD5
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Criptografa o valor passado
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(valor));

            // Cria um StringBuilder para passarmos os bytes gerados para ele
            StringBuilder strBuilder = new StringBuilder();

            // Converte cada byte em um valor hexadecimal e adiciona ao
            // string builder
            // and format each one as a hexadecimal string.
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            // retorna o valor criptografado como string
            return strBuilder.ToString();
        }
        #endregion

        #region LerArquivo()
        /// <summary>
        /// Le arquivos no formato TXT
        /// Retorna uma lista do conteudo do arquivo
        /// </summary>
        /// <param name="cArquivo"></param>
        /// <returns></returns>
        public static List<string> LerArquivo(string cArquivo)
        {
            List<string> lstRetorno = new List<string>();
            if (File.Exists(cArquivo))
            {
                using (System.IO.StreamReader txt = new StreamReader(cArquivo, Encoding.Default, true))
                {
                    try
                    {
                        string cLinhaTXT = txt.ReadLine();
                        while (cLinhaTXT != null)
                        {
                            string[] dados = cLinhaTXT.Split('|');
                            if (dados.GetLength(0) > 1)
                            {
                                lstRetorno.Add(cLinhaTXT);
                            }
                            cLinhaTXT = txt.ReadLine();
                        }
                    }
                    finally
                    {
                        txt.Close();
                    }
                    if (lstRetorno.Count == 0)
                        throw new Exception("Arquivo: " + cArquivo + " vazio");
                }
            }
            return lstRetorno;
        }
        #endregion

        #region ExtrairNomeArq()
        /// <summary>
        /// Extrai somente o nome do arquivo de uma string; para ser utilizado na situação desejada. Veja os exemplos na documentação do código.
        /// </summary>
        /// <param name="pPastaArq">String contendo o caminho e nome do arquivo que é para ser extraido o nome.</param>
        /// <param name="pFinalArq">String contendo o final do nome do arquivo até onde é para ser extraído.</param>
        /// <returns>Retorna somente o nome do arquivo de acordo com os parâmetros passados - veja exemplos.</returns>
        /// <example>
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", "-ped-sta.xml"));
        /// //Será demonstrado no message a string "ArqSituacao"
        /// 
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", ".xml"));
        /// //Será demonstrado no message a string "ArqSituacao-ped-sta"
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>19/06/2008</date>
        public static string ExtrairNomeArq(string pPastaArq, string pFinalArq)
        {
            FileInfo fi = new FileInfo(pPastaArq);
            string ret = fi.Name;
            ret = ret.Length > pFinalArq.Length ? ret.Substring(0, ret.Length - pFinalArq.Length) : ret;
            return ret;
        }
        #endregion

        #region IsConnectedToInternet()
        //Creating the extern function...
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
        #endregion

        #region FileInUse()
        /// <summary>
        /// detectar se o arquivo está em uso
        /// </summary>
        /// <param name="file">caminho do arquivo</param>
        /// <returns>true se estiver em uso</returns>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public static bool FileInUse(string file)
        {
            bool ret = false;

            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fs.Close();//fechar o arquivo para nao dar erro em outras aplicações
                }
            }
            catch
            {
                ret = true;
            }

            return ret;
        }
        #endregion

        #region LerTag()
        /// <summary>
        /// Busca o nome de uma determinada TAG em um Elemento do XML para ver se existe, se existir retorna seu conteúdo com um ponto e vírgula no final do conteúdo.
        /// </summary>
        /// <param name="Elemento">Elemento a ser pesquisado o Nome da TAG</param>
        /// <param name="NomeTag">Nome da Tag</param>
        /// <returns>Conteúdo da tag</returns>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public static string LerTag(XmlElement Elemento, string NomeTag)
        {
            return LerTag(Elemento, NomeTag, true);
        }
        #endregion

        #region LerTag()
        /// <summary>
        /// Busca o nome de uma determinada TAG em um Elemento do XML para ver se existe, se existir retorna seu conteúdo, com ou sem um ponto e vírgula no final do conteúdo.
        /// </summary>
        /// <param name="Elemento">Elemento a ser pesquisado o Nome da TAG</param>
        /// <param name="NomeTag">Nome da Tag</param>
        /// <param name="RetornaPontoVirgula">Retorna com ponto e vírgula no final do conteúdo da tag</param>
        /// <returns>Conteúdo da tag</returns>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public static string LerTag(XmlElement Elemento, string NomeTag, bool RetornaPontoVirgula)
        {
            string Retorno = string.Empty;

            if (Elemento.GetElementsByTagName(NomeTag).Count != 0)
            {
                if (RetornaPontoVirgula)
                {
                    Retorno = Elemento.GetElementsByTagName(NomeTag)[0].InnerText.Replace(";", " ");  //danasa 19-9-2009
                    Retorno += ";";
                }
                else
                {
                    Retorno = Elemento.GetElementsByTagName(NomeTag)[0].InnerText;  //Wandrey 07/10/2009
                }
            }
            return Retorno;
        }

        public static string LerTag(XmlElement Elemento, string NomeTag, string defaultValue)
        {
            string result = LerTag(Elemento, NomeTag, false);
            if (string.IsNullOrEmpty(result))
                result = defaultValue;
            return result;
        }
        #endregion

        #region getMD5Hash
        /// <summary>
        /// Criptografar conteúdo com MD5
        /// </summary>
        /// <param name="input">Conteúdo a ser criptografado</param>
        /// <returns>Conteúdo criptografado com MD5</returns>
        public static string GetMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion

        #region GetIpAddress
        /*
         * Marcelo
         * 03/06/2013
         */
        /// <summary>
        /// Retorna o endereço IP desta estação
        /// </summary>
        /// <returns>Endereço ip da estação</returns>
        public static string GetIPAddress()
        {
            var hostEntry = Dns.GetHostEntry(Environment.MachineName);
            string ip = (
                       from addr in hostEntry.AddressList
                       where addr.AddressFamily.ToString() == "InterNetwork"
                       select addr.ToString()
                ).FirstOrDefault();

            return ip;
        }

        #endregion
    }
}
