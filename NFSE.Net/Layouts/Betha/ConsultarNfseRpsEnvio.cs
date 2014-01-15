using System.Xml.Serialization;


namespace NFSE.Net.Layouts.Betha
{
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.betha.com.br/e-nota-contribuinte-ws", IsNullable = false)]
    public class ConsultarNfsePorRpsEnvio
    {

        private tcIdentificacaoRps identificacaoRpsField;

        private tcIdentificacaoPrestador prestadorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tcIdentificacaoRps IdentificacaoRps
        {
            get
            {
                return this.identificacaoRpsField;
            }
            set
            {
                this.identificacaoRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tcIdentificacaoPrestador Prestador
        {
            get
            {
                return this.prestadorField;
            }
            set
            {
                this.prestadorField = value;
            }
        }
    }    
}
