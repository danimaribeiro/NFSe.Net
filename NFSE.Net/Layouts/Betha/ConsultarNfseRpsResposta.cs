using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSE.Net.Layouts.Betha
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "")]
    public class ConsultarNfseRpsResposta
    {

        private tcCompNfse complNfseField;

        private tcMensagemRetorno[] listaMensagemRetornoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tcCompNfse ComplNfse
        {
            get
            {
                return this.complNfseField;
            }
            set
            {
                this.complNfseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("MensagemRetorno", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tcMensagemRetorno[] ListaMensagemRetorno
        {
            get
            {
                return this.listaMensagemRetornoField;
            }
            set
            {
                this.listaMensagemRetornoField = value;
            }
        }
    }
    
}
