using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using NFSE.Net.Core;
using NFSE.Net.Certificado;

namespace NFSE.Net.Validacoes
{
    /// <summary>
    /// Classe de validação dos XML´s
    /// </summary>
    public class ValidarXML
    {
        #region Construtores
        public ValidarXML(string arquivoXML, int UFCod)
        {
            TipoArqXml = new TipoArquivoXML(arquivoXML, UFCod);
        }
        #endregion

        public TipoArquivoXML TipoArqXml = null;

        public int Retorno { get; private set; }
        public string RetornoString { get; private set; }
        /// <summary>
        /// Pasta dos schemas para validação do XML
        /// </summary>
        private string PastaSchema = Propriedade.PastaSchemas;

        private string cErro;

        /// <summary>
        /// Método responsável por validar a estrutura do XML de acordo com o schema passado por parâmetro
        /// </summary>
        /// <param name="cRotaArqXML">XML a ser validado</param>
        /// <param name="cRotaArqSchema">Schema a ser utilizado na validação</param>
        /// <param name="nsSchema">Namespace contendo a URL do schema</param>
        public void Validar(string cRotaArqXML)
        {
            bool lArqXML = File.Exists(cRotaArqXML);
            var caminhoDoSchema = this.PastaSchema + "\\" + TipoArqXml.cArquivoSchema;
            bool lArqXSD = File.Exists(caminhoDoSchema);
            bool temXSD = !string.IsNullOrEmpty(TipoArqXml.cArquivoSchema);

            Retorno = 0;
            RetornoString = "";

            if (lArqXML && lArqXSD)
            {
                XmlReader xmlReader = null;

                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;

                    XmlSchemaSet schemas = new XmlSchemaSet();
                    settings.Schemas = schemas;

                    if (TipoArqXml.TargetNameSpace != string.Empty)
                        schemas.Add(TipoArqXml.TargetNameSpace, caminhoDoSchema);
                    else
                        schemas.Add(Propriedade.nsURI, caminhoDoSchema);

                    settings.ValidationEventHandler += new ValidationEventHandler(reader_ValidationEventHandler);

                    xmlReader = XmlReader.Create(cRotaArqXML, settings);

                    this.cErro = "";
                    try
                    {
                        while (xmlReader.Read()) { }
                    }
                    catch (Exception ex)
                    {
                        this.cErro = ex.Message;
                    }

                    xmlReader.Close();
                }
                catch (Exception ex)
                {
                    if (xmlReader != null)
                        xmlReader.Close();

                    cErro = ex.Message + "\r\n";
                }

                this.Retorno = 0;
                this.RetornoString = "";
                if (cErro != "")
                {
                    this.Retorno = 1;
                    this.RetornoString = "Início da validação...\r\n\r\n";
                    this.RetornoString += "Arquivo XML: " + cRotaArqXML + "\r\n";
                    this.RetornoString += "Arquivo SCHEMA: " + caminhoDoSchema + "\r\n\r\n";
                    this.RetornoString += this.cErro;
                    this.RetornoString += "\r\n...Final da validação";
                }
            }
            else
            {
                if (lArqXML == false)
                {
                    this.Retorno = 2;
                    this.RetornoString = "Arquivo XML não foi encontrato";
                }
                else if (lArqXSD == false && temXSD)
                {
                    this.Retorno = 3;
                    this.RetornoString = "Arquivo XSD (schema) não foi encontrado em " + caminhoDoSchema;
                }
            }
        }

        private void reader_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            this.cErro += "Linha: " + e.Exception.LineNumber + " Coluna: " + e.Exception.LinePosition + " Erro: " + e.Exception.Message + "\r\n";
        }


        #region ValidarArqXML()
        /// <summary>
        /// Valida o arquivo XML 
        /// </summary>
        /// <param name="arquivo">Nome do arquivo XML a ser validado</param>
        /// <param name="cUFCod">Código da UF/Municipio</param>
        /// <returns>
        /// Se retornar uma string em branco, significa que o XML foi 
        /// validado com sucesso, ou seja, não tem nenhum erro. Se o retorno
        /// tiver algo, algum erro ocorreu na validação.
        /// </returns>
        public string ValidarArqXML(string arquivo)
        {
            string cRetorna = "";

            if (TipoArqXml.nRetornoTipoArq >= 1 && TipoArqXml.nRetornoTipoArq <= SchemaXML.MaxID)
            {
                Validar(arquivo);
                if (Retorno != 0)
                {
                    cRetorna = "XML INCONSISTENTE!\r\n\r\n" + RetornoString;
                }
            }
            else
            {
                cRetorna = "XML INCONSISTENTE!\r\n\r\n" + TipoArqXml.cRetornoTipoArq;
            }

            return cRetorna;
        }
        #endregion

        #region ValidarAssinarXML()
        /// <summary>
        /// Efetua a validação de qualquer XML, NFE, Cancelamento, Inutilização, etc..., e retorna se está ok ou não
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML a ser validado e assinado</param>
        /// <param name="PastaValidar">Nome da pasta onde fica os arquivos a serem validados</param>
        /// <param name="PastaXMLErro">Nome da pasta onde é para gravar os XML´s validados que apresentaram erro.</param>
        /// <param name="PastaXMLRetorno">Nome da pasta de retorno onde será gravado o XML com o status da validação</param>
        /// <param name="Certificado">Certificado digital a ser utilizado na validação</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>28/05/2009</date>
        public void ValidarAssinarXML(string Arquivo)
        {
            int emp = Functions.FindEmpresaByThread();

            Boolean Assinou = true;

            //Assinar o XML se tiver tag para assinar
            AssinaturaDigital oAD = new AssinaturaDigital();

            try
            {
                oAD.Assinar(Arquivo, emp, Empresa.Configuracoes[emp].UFCod);

                Assinou = true;
            }
            catch (Exception ex)
            {
                Assinou = false;
                try
                {
                    GravarXMLRetornoValidacao(Arquivo, "2", "Ocorreu um erro ao assinar o XML: " + ex.Message);
                    new Auxiliar().MoveArqErro(Arquivo);
                }
                catch
                {
                    //Se deu algum erro na hora de gravar o retorno do erro para o ERP, infelizmente não posso fazer nada.
                    //Isso pode acontecer se falhar rede, hd, problema de permissão de pastas, etc... Wandrey 23/03/2010
                }
            }

            if (Assinou)
            {
                // Validar o Arquivo XML
                if (TipoArqXml.nRetornoTipoArq >= 1 && TipoArqXml.nRetornoTipoArq <= SchemaXML.MaxID)
                {
                    try
                    {
                        Validar(Arquivo);
                        if (Retorno != 0)
                        {
                            this.GravarXMLRetornoValidacao(Arquivo, "3", "Ocorreu um erro ao validar o XML: " + RetornoString);
                            new Auxiliar().MoveArqErro(Arquivo);
                        }
                        else
                        {
                            if (!Directory.Exists(Empresa.Configuracoes[emp].PastaValidar + "\\Validado"))
                            {
                                Directory.CreateDirectory(Empresa.Configuracoes[emp].PastaValidar + "\\Validado");
                            }

                            string ArquivoNovo = Empresa.Configuracoes[emp].PastaValidar + "\\Validado\\" + Functions.ExtrairNomeArq(Arquivo, ".xml") + ".xml";

                            Functions.Move(Arquivo, ArquivoNovo);
                            /*
                            if (File.Exists(ArquivoNovo))
                            {
                                FileInfo oArqNovo = new FileInfo(ArquivoNovo);
                                oArqNovo.Delete();
                            }

                            FileInfo oArquivo = new FileInfo(Arquivo);
                            oArquivo.MoveTo(ArquivoNovo);
                            */

                            this.GravarXMLRetornoValidacao(Arquivo, "1", "XML assinado e validado com sucesso.");
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            this.GravarXMLRetornoValidacao(Arquivo, "4", "Ocorreu um erro ao validar o XML: " + ex.Message);
                            new Auxiliar().MoveArqErro(Arquivo);
                        }
                        catch
                        {
                            //Se deu algum erro na hora de gravar o retorno do erro para o ERP, infelizmente não posso fazer nada.
                            //Isso pode acontecer se falhar rede, hd, problema de permissão de pastas, etc... Wandrey 23/03/2010
                        }
                    }
                }
                else
                {
                    try
                    {
                        this.GravarXMLRetornoValidacao(Arquivo, "5", "Ocorreu um erro ao validar o XML: " + TipoArqXml.cRetornoTipoArq);
                        new Auxiliar().MoveArqErro(Arquivo);
                    }
                    catch
                    {
                        //Se deu algum erro na hora de gravar o retorno do erro para o ERP, infelizmente não posso fazer nada.
                        //Isso pode acontecer se falhar rede, hd, problema de permissão de pastas, etc... Wandrey 23/03/2010
                    }
                }
            }
        }
        #endregion

        #region GravarXMLRetornoValidacao()
        /// <summary>
        /// Na tentativa de somente validar ou assinar o XML se encontrar um erro vai ser retornado um XML para o ERP
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML validado</param>
        /// <param name="PastaXMLRetorno">Pasta de retorno para ser gravado o XML</param>
        /// <param name="cStat">Status da validação</param>
        /// <param name="xMotivo">Status descritivo da validação</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>28/05/2009</date>
        private void GravarXMLRetornoValidacao(string Arquivo, string cStat, string xMotivo)
        {
            int emp = Functions.FindEmpresaByThread();

            //Definir o nome do arquivo de retorno
            string ArquivoRetorno = Functions.ExtrairNomeArq(Arquivo, ".xml") + "-ret.xml";

            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            XmlWriter oXmlGravar = null;

            try
            {
                //Agora vamos criar um XML Writer
                oXmlGravar = XmlWriter.Create(Empresa.Configuracoes[emp].PastaRetorno + "\\" + ArquivoRetorno);

                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("Validacao");
                oXmlGravar.WriteElementString("cStat", cStat);
                oXmlGravar.WriteElementString("xMotivo", xMotivo);
                oXmlGravar.WriteEndElement(); //nfe_configuracoes
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
            }
            finally
            {
                if (oXmlGravar != null)
                    oXmlGravar.Close();
            }
        }
        #endregion
    }
}
