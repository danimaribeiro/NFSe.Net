using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net
{
    public class TipoArquivoXML
    {
        public int nRetornoTipoArq { get; private set; }
        public string cRetornoTipoArq { get; private set; }
        /// <summary>
        /// Tag que deve ser assinada no XML, se o conteúdo estiver em branco é por que o XML não deve ser assinado
        /// </summary>
        public string TagAssinatura { get; private set; }
        /// <summary>
        /// Tag que tem o atributo ID no XML
        /// </summary>
        public string TagAtributoId { get; private set; }
        /// <summary>
        /// Tag que deve ser assinada no XML, se o conteúdo estiver em branco é por que o XML não deve ser assinado
        /// </summary>
        public string TagLoteAssinatura { get; private set; }
        /// <summary>
        /// Tag que tem o atributo ID no XML
        /// </summary>
        public string TagLoteAtributoId { get; private set; }
        public string cArquivoSchema { get; private set; }
        public string TargetNameSpace { get; private set; }

        public TipoArquivoXML(string rotaArqXML, int UFCod)
        {
            DefinirTipoArq(rotaArqXML, UFCod);
        }

        private void DefinirTipoArq(string cRotaArqXML, int UFCod)
        {
            nRetornoTipoArq = 0;
            cRetornoTipoArq = string.Empty;
            cArquivoSchema = string.Empty;
            TagAssinatura = string.Empty;
            TagAtributoId = string.Empty;
            TagLoteAssinatura = string.Empty;
            TagLoteAtributoId = string.Empty;
            TargetNameSpace = string.Empty;

            string padraoNFSe = string.Empty;
            if (Propriedade.TipoAplicativo == TipoAplicativo.Nfse)
                padraoNFSe = Functions.PadraoNFSe(UFCod).ToString() + "-";
            else
                padraoNFSe = string.Empty;

            try
            {
                if (File.Exists(cRotaArqXML))
                {
                    //Carregar os dados do arquivo XML de configurações do UniNfe
                    XmlTextReader oLerXml = null;

                    try
                    {
                        oLerXml = new XmlTextReader(cRotaArqXML);

                        while (oLerXml.Read())
                        {
                            if (oLerXml.NodeType == XmlNodeType.Element)
                            {
                                InfSchema schema = null;
                                try
                                {
                                    string nome = oLerXml.Name;
                                    if (Propriedade.TipoAplicativo == TipoAplicativo.Nfe)
                                    {
                                        string name = oLerXml.Name;

                                        if (name.Equals("envEvento") || name.Equals("eventoCTe"))
                                        {
                                            XmlDocument xml = new XmlDocument();
                                            xml.Load(oLerXml);

                                            XmlElement cl = (XmlElement)xml.GetElementsByTagName("tpEvento")[0];
                                            if (cl != null)
                                            {
                                                string evento = cl.InnerText;

                                                switch (evento)
                                                {
                                                    case "110110": //XML de Evento da CCe
                                                        nome = name + evento;
                                                        break;

                                                    case "110111": //XML de Envio de evento de cancelamento
                                                        nome = name + evento;
                                                        break;

                                                    case "110113": //XML de Envio do evento de contingencia EPEC, CTe
                                                        nome = name + evento;
                                                        break;

                                                    case "110160": //XML de Envio do evento de Registro Multimodal, CTe
                                                        nome = name + evento;
                                                        break;

                                                    case "210200": //XML Evento de manifestação do destinatário
                                                    case "210210": //XML Evento de manifestação do destinatário
                                                    case "210220": //XML Evento de manifestação do destinatário
                                                    case "210240": //XML Evento de manifestação do destinatário
                                                        nome = "envConfRecebto";
                                                        break;
                                                }
                                            }
                                        }
                                        else if (oLerXml.Name.Equals("eventoMDFe"))
                                        {
                                            XmlDocument xml = new XmlDocument();
                                            xml.Load(oLerXml);

                                            XmlElement cl = (XmlElement)xml.GetElementsByTagName("tpEvento")[0];
                                            if (cl != null)
                                            {
                                                nome = "eventoMDFe" + cl.InnerText;
                                            }
                                        }
                                    }
                                    schema = SchemaXML.InfSchemas[Propriedade.TipoAplicativo.ToString().ToUpper() + "-" + padraoNFSe + nome];
                                }
                                catch
                                {
                                    throw new Exception("Não foi possível identificar o tipo do XML para ser validado, ou seja, o sistema não sabe se é um XML de NFe, consulta, etc. Por favor verifique se não existe algum erro de estrutura do XML que impede sua identificação.");
                                }

                                nRetornoTipoArq = schema.ID;
                                cRetornoTipoArq = schema.Descricao;
                                cArquivoSchema = schema.ArquivoXSD;
                                TagAssinatura = schema.TagAssinatura;
                                TagAtributoId = schema.TagAtributoId;
                                TagLoteAssinatura = schema.TagLoteAssinatura;
                                TagLoteAtributoId = schema.TagLoteAtributoId;
                                TargetNameSpace = schema.TargetNameSpace;

                                if (this.nRetornoTipoArq != 0) //Arquivo XML já foi identificado
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.nRetornoTipoArq = 102;
                        this.cRetornoTipoArq = ex.Message;
                    }
                    finally
                    {
                        if (oLerXml != null)
                        {
                            if (oLerXml.ReadState != ReadState.Closed)
                            {
                                oLerXml.Close();
                            }
                        }
                    }
                }
                else
                {
                    this.nRetornoTipoArq = 100;
                    this.cRetornoTipoArq = "Arquivo XML não foi encontrado";
                }
            }
            catch (Exception ex)
            {
                this.nRetornoTipoArq = 103;
                this.cRetornoTipoArq = ex.Message;
            }

            if (this.nRetornoTipoArq == 0)
            {
                this.nRetornoTipoArq = 101;
                this.cRetornoTipoArq = "Não foi possível identificar o arquivo XML";
            }
        }

    }
}
