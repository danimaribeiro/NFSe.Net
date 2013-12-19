using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Certificado
{
    public static class Criptografia
    {
        private static string _chave = "unimake_uninfe";

        public static string criptografaSenha(string senhaCripto)
        {
            try
            {
                return criptografaSenha(senhaCripto, _chave);
            }
            catch (Exception ex)
            {
                return "String errada. " + ex.Message;
            }

        }

        public static string descriptografaSenha(string senhaDescripto)
        {
            try
            {
                return descriptografaSenha(senhaDescripto, _chave);
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string criptografaSenha(string senhaCripto, string chave)
        {
            try
            {
                TripleDESCryptoServiceProvider objcriptografaSenha = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objcriptoMd5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = chave;

                byteHash = objcriptoMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objcriptoMd5 = null;
                objcriptografaSenha.Key = byteHash;
                objcriptografaSenha.Mode = CipherMode.ECB;

                byteBuff = ASCIIEncoding.ASCII.GetBytes(senhaCripto);
                return Convert.ToBase64String(objcriptografaSenha.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Digite os valores Corretamente." + ex.Message;
            }
        }

        public static string descriptografaSenha(string strCriptografada, string chave)
        {
            try
            {
                TripleDESCryptoServiceProvider objdescriptografaSenha = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objcriptoMd5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = chave;

                byteHash = objcriptoMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objcriptoMd5 = null;
                objdescriptografaSenha.Key = byteHash;
                objdescriptografaSenha.Mode = CipherMode.ECB;

                byteBuff = Convert.FromBase64String(strCriptografada);
                string strDecrypted = ASCIIEncoding.ASCII.GetString(objdescriptografaSenha.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objdescriptografaSenha = null;

                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Digite os valores Corretamente." + ex.Message;
            }
        }

        public static bool compararStrings(string num01, string num02)
        {
            bool stringValor;
            if (num01.Equals(num02))
            {
                stringValor = true;
            }
            else
            {
                stringValor = false;
            }
            return stringValor;
        }

        /// <summary>
        /// Assina a string utilizando RSA-SHA1
        /// </summary>
        /// <param name="cert">certificado utilizado para assinar a string</param>
        /// <param name="value">Valor a ser assinado</param>
        /// <returns></returns>
        public static string SignWithRSASHA1(X509Certificate2 cert, String value)
        {
            //Regras retiradas da página 39 do manual da Prefeitura Municipal de Blumenau
            // Converta a cadeia de caracteres ASCII para bytes. 
            ASCIIEncoding asciiEncoding = new ASCIIEncoding();
            byte[] asciiBytes = asciiEncoding.GetBytes(value);

            // Gere o HASH (array de bytes) utilizando SHA1
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] sha1Hash = sha1.ComputeHash(asciiBytes);

            //- Assine o HASH (array de bytes) utilizando RSA-SHA1.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa = cert.PrivateKey as RSACryptoServiceProvider;
            asciiBytes = rsa.SignHash(sha1Hash, "SHA1");
            string result = Convert.ToBase64String(asciiBytes);
            return result;
        }
    }
}
