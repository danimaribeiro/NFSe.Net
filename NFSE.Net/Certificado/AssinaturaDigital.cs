using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net.Certificado
{
    public delegate void CryptographicExceptionHandler(object sender, EventArgs args);


    public class AssinaturaDigital
    {
        /// <summary>
        /// O método assina digitalmente o arquivo XML passado por parâmetro e 
        /// grava o XML assinado com o mesmo nome, sobreponto o XML informado por parâmetro.
        /// Disponibiliza também uma propriedade com uma string do xml assinado (this.vXmlStringAssinado)
        /// </summary>
        /// <param name="arqXMLAssinar">Nome do arquivo XML a ser assinado</param>
        /// <param name="tagAssinatura">Nome da tag onde é para ficar a assinatura</param>
        /// <param name="tagAtributoId">Nome da tag que tem o atributo ID, tag que vai ser assinada</param>
        /// <param name="x509Cert">Certificado a ser utilizado na assinatura</param>
        /// <param name="empresa">Índice da empresa que está solicitando a assinatura</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 04/06/2008
        /// </remarks>
        private void Assinar(string arqXMLAssinar,
            string tagAssinatura,
            string tagAtributoId,
            X509Certificate2 x509Cert,
            int empresa)
        {

            StreamReader SR = null;

            try
            {
                //Abrir o arquivo XML a ser assinado e ler o seu conteúdo
                SR = File.OpenText(arqXMLAssinar);
                string xmlString = SR.ReadToEnd();
                SR.Close();
                SR = null;

                // Create a new XML document.
                XmlDocument doc = new XmlDocument();

                // Format the document to ignore white spaces.
                doc.PreserveWhitespace = false;

                // Load the passed XML file using it’s name.
                doc.LoadXml(xmlString);

                if (doc.GetElementsByTagName(tagAssinatura).Count == 0)
                {
                    throw new Exception("A tag de assinatura " + tagAssinatura.Trim() + " não existe no XML. (Código do Erro: 5)");
                }
                else if (doc.GetElementsByTagName(tagAtributoId).Count == 0)
                {
                    throw new Exception("A tag de assinatura " + tagAtributoId.Trim() + " não existe no XML. (Código do Erro: 4)");
                }
                // Existe mais de uma tag a ser assinada
                else
                {
                    XmlDocument XMLDoc;

                    XmlNodeList lists = doc.GetElementsByTagName(tagAssinatura);
                    foreach (XmlNode nodes in lists)
                    {
                        foreach (XmlNode childNodes in nodes.ChildNodes)
                        {
                            if (!childNodes.Name.Equals(tagAtributoId))
                                continue;

                            if (childNodes.NextSibling != null && childNodes.NextSibling.Name.Equals("Signature"))
                                continue;

                            // Create a reference to be signed
                            Reference reference = new Reference();
                            reference.Uri = "";

                            // pega o uri que deve ser assinada                                       
                            XmlElement childElemen = (XmlElement)childNodes;
                            if (childElemen.GetAttributeNode("Id") != null)
                            {
                                reference.Uri = "#" + childElemen.GetAttributeNode("Id").Value;
                            }
                            else if (childElemen.GetAttributeNode("id") != null)
                            {
                                reference.Uri = "#" + childElemen.GetAttributeNode("id").Value;
                            }

                            // Create a SignedXml object.
                            SignedXml signedXml = new SignedXml(doc);

                            // Add the key to the SignedXml document
                            signedXml.SigningKey = x509Cert.PrivateKey;

                            // Add an enveloped transformation to the reference.
                            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                            reference.AddTransform(env);

                            XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                            reference.AddTransform(c14);

                            // Add the reference to the SignedXml object.
                            signedXml.AddReference(reference);

                            // Create a new KeyInfo object
                            KeyInfo keyInfo = new KeyInfo();

                            // Load the certificate into a KeyInfoX509Data object
                            // and add it to the KeyInfo object.
                            keyInfo.AddClause(new KeyInfoX509Data(x509Cert));

                            // Add the KeyInfo object to the SignedXml object.
                            signedXml.KeyInfo = keyInfo;
                            signedXml.ComputeSignature();

                            // Get the XML representation of the signature and save
                            // it to an XmlElement object.
                            XmlElement xmlDigitalSignature = signedXml.GetXml();

                            // Gravar o elemento no documento XML
                            nodes.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                        }
                    }

                    XMLDoc = new XmlDocument();
                    XMLDoc.PreserveWhitespace = false;
                    XMLDoc = doc;

                    // Atualizar a string do XML já assinada
                    string StringXMLAssinado = XMLDoc.OuterXml;

                    // Gravar o XML Assinado no HD
                    StreamWriter SW_2 = File.CreateText(arqXMLAssinar);
                    SW_2.Write(StringXMLAssinado);
                    SW_2.Close();
                }
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                #region #10316
                /*
                 * Solução para o problema do certificado do tipo A3
                 * Marcelo
                 * 29/07/2013
                 */

#if DEBUG
                Debug.WriteLine("O erro CryptographicException foi lançado");
#endif
                x509Cert = Empresa.ResetCertificado(empresa);

                throw new Exception("O certificado deverá ser reiniciado.\r\n Retire o certificado.\r\nAguarde o LED terminar de piscar.\r\n Recoloque o certificado e informe o PIN novamente.\r\n" + ex.ToString());// #12342 concatenar com a mensagem original
                #endregion
            }
            finally
            {
                if (SR != null)
                    SR.Close();
            }
        }

        /// <summary>
        /// Assina o XML sobrepondo-o
        /// </summary>
        /// <param name="arqXMLAssinar">Nome do arquivo XML a ser assinado</param>
        /// <param name="x509Certificado">Certificado a ser utilizado na assinatura</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>16/04/2009</date>
        public void Assinar(string arqXMLAssinar, int emp, int UFCod)
        {
            TipoArquivoXML v = new TipoArquivoXML(arqXMLAssinar, UFCod);

            if (!String.IsNullOrEmpty(v.TagAssinatura))
                this.Assinar(arqXMLAssinar, v.TagAssinatura, v.TagAtributoId, Empresa.Configuracoes[emp].X509Certificado, emp);

            //Assinar o lote
            if (!String.IsNullOrEmpty(v.TagLoteAssinatura))
                this.Assinar(arqXMLAssinar, v.TagLoteAssinatura, v.TagLoteAtributoId, Empresa.Configuracoes[emp].X509Certificado, emp);
        }
    }
}
