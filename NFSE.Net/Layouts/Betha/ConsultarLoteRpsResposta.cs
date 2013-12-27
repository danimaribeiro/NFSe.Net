using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Layouts.Betha
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class ConsultarLoteRpsResposta
    {

        private ConsultarLoteRpsRespostaListaNfse listaNfseField;

        private ListaMensagemRetorno listaMensagemRetornoField;

        /// <remarks/>
        public ConsultarLoteRpsRespostaListaNfse ListaNfse
        {
            get
            {
                return this.listaNfseField;
            }
            set
            {
                this.listaNfseField = value;
            }
        }

        /// <remarks/>
        public ListaMensagemRetorno ListaMensagemRetorno
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
