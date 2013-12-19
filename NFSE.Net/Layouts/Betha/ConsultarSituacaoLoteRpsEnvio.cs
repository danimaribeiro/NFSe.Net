﻿using System;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/servico_consultar_situacao_lote_rps_envio.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/servico_consultar_situacao_lote_rps_envio.xsd", IsNullable = false)]
    public class ConsultarSituacaoLoteRpsEnvio
    {

        private tcIdentificacaoPrestador prestadorField;

        private string protocoloField;

        /// <remarks/>
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

        /// <remarks/>
        public string Protocolo
        {
            get
            {
                return this.protocoloField;
            }
            set
            {
                this.protocoloField = value;
            }
        }
    }
}
