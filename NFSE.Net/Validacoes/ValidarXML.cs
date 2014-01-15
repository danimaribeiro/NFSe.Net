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
        public void ValidarAssinarXML(string Arquivo, Core.Empresa empresa)
        {
            //Assinar o XML se tiver tag para assinar
            AssinaturaDigital oAD = new AssinaturaDigital();
            oAD.Assinar(Arquivo, empresa, empresa.CodigoMunicipio);
        }
        #endregion

    }
}
