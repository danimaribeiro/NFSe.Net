using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFSE.Net.Layouts.Betha
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcLoteRps
    {

        private string numeroLoteField;

        private string cnpjField;

        private string inscricaoMunicipalField;

        private int quantidadeRpsField;

        private tcRps[] listaRpsField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string NumeroLote
        {
            get
            {
                return this.numeroLoteField;
            }
            set
            {
                this.numeroLoteField = value;
            }
        }

        /// <remarks/>
        public string Cnpj
        {
            get
            {
                return this.cnpjField;
            }
            set
            {
                this.cnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }

        /// <remarks/>
        public int QuantidadeRps
        {
            get
            {
                return this.quantidadeRpsField;
            }
            set
            {
                this.quantidadeRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Rps", IsNullable = false)]
        public tcRps[] ListaRps
        {
            get
            {
                return this.listaRpsField;
            }
            set
            {
                this.listaRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcRps
    {

        private tcInfRps infRpsField;

        /// <remarks/>
        public tcInfRps InfRps
        {
            get
            {
                return this.infRpsField;
            }
            set
            {
                this.infRpsField = value;
            }
        }
              
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcInfRps
    {

        private tcIdentificacaoRps identificacaoRpsField;

        private System.DateTime dataEmissaoField;

        private sbyte naturezaOperacaoField;

        private sbyte regimeEspecialTributacaoField;

        private bool regimeEspecialTributacaoFieldSpecified;

        private sbyte optanteSimplesNacionalField;

        private sbyte incentivadorCulturalField;

        private sbyte statusField;

        private tcIdentificacaoRps rpsSubstituidoField;

        private tcDadosServico servicoField;

        private tcIdentificacaoPrestador prestadorField;

        private tcDadosTomador tomadorField;

        private tcIdentificacaoIntermediarioServico intermediarioServicoField;

        private tcDadosConstrucaoCivil contrucaoCivilField;

        private string idField;

        /// <remarks/>
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
                
        [System.Xml.Serialization.XmlIgnore]
        public System.DateTime DataEmissao
        {
            get
            {
                return this.dataEmissaoField;
            }
            set
            {
                this.dataEmissaoField = value;
            }
        }

        [XmlElement("DataEmissao")]
        public string DataEmissaoString
        {
            get { return this.DataEmissao.ToString("yyyy-MM-ddTHH:mm:ss"); }
            set { this.DataEmissao = DateTime.Parse(value); }
        }

        /// <remarks/>
        public sbyte NaturezaOperacao
        {
            get
            {
                return this.naturezaOperacaoField;
            }
            set
            {
                this.naturezaOperacaoField = value;
            }
        }

        /// <remarks/>
        public sbyte RegimeEspecialTributacao
        {
            get
            {
                return this.regimeEspecialTributacaoField;
            }
            set
            {
                this.regimeEspecialTributacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RegimeEspecialTributacaoSpecified
        {
            get
            {
                return this.regimeEspecialTributacaoFieldSpecified;
            }
            set
            {
                this.regimeEspecialTributacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte OptanteSimplesNacional
        {
            get
            {
                return this.optanteSimplesNacionalField;
            }
            set
            {
                this.optanteSimplesNacionalField = value;
            }
        }

        /// <remarks/>
        public sbyte IncentivadorCultural
        {
            get
            {
                return this.incentivadorCulturalField;
            }
            set
            {
                this.incentivadorCulturalField = value;
            }
        }

        /// <remarks/>
        public sbyte Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoRps RpsSubstituido
        {
            get
            {
                return this.rpsSubstituidoField;
            }
            set
            {
                this.rpsSubstituidoField = value;
            }
        }

        /// <remarks/>
        public tcDadosServico Servico
        {
            get
            {
                return this.servicoField;
            }
            set
            {
                this.servicoField = value;
            }
        }

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
        public tcDadosTomador Tomador
        {
            get
            {
                return this.tomadorField;
            }
            set
            {
                this.tomadorField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoIntermediarioServico IntermediarioServico
        {
            get
            {
                return this.intermediarioServicoField;
            }
            set
            {
                this.intermediarioServicoField = value;
            }
        }

        /// <remarks/>
        public tcDadosConstrucaoCivil ContrucaoCivil
        {
            get
            {
                return this.contrucaoCivilField;
            }
            set
            {
                this.contrucaoCivilField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("ComplNfse", IsNullable = false)]
    public class tcCompNfse
    {

        private tcNfse nfseField;

        private tcCancelamentoNfse nfseCancelamentoField;

        private tcSubstituicaoNfse nfseSubstituicaoField;

        /// <remarks/>
        public tcNfse Nfse
        {
            get
            {
                return this.nfseField;
            }
            set
            {
                this.nfseField = value;
            }
        }

        /// <remarks/>
        public tcCancelamentoNfse NfseCancelamento
        {
            get
            {
                return this.nfseCancelamentoField;
            }
            set
            {
                this.nfseCancelamentoField = value;
            }
        }

        /// <remarks/>
        public tcSubstituicaoNfse NfseSubstituicao
        {
            get
            {
                return this.nfseSubstituicaoField;
            }
            set
            {
                this.nfseSubstituicaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcNfse
    {

        private tcInfNfse infNfseField;   

        /// <remarks/>
        public tcInfNfse InfNfse
        {
            get
            {
                return this.infNfseField;
            }
            set
            {
                this.infNfseField = value;
            }
        }        
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcInfNfse
    {

        private string numeroField;

        private string codigoVerificacaoField;

        private System.DateTime dataEmissaoField;

        private tcIdentificacaoRps identificacaoRpsField;

        private System.DateTime dataEmissaoRpsField;

        private bool dataEmissaoRpsFieldSpecified;

        private sbyte naturezaOperacaoField;

        private sbyte regimeEspecialTributacaoField;

        private bool regimeEspecialTributacaoFieldSpecified;

        private sbyte optanteSimplesNacionalField;

        private sbyte incentivadorCulturalField;

        private System.DateTime competenciaField;

        private string nfseSubstituidaField;

        private string outrasInformacoesField;

        private tcDadosServico servicoField;

        private decimal valorCreditoField;

        private bool valorCreditoFieldSpecified;

        private tcDadosPrestador prestadorServicoField;

        private tcDadosTomador tomadorServicoField;

        private tcIdentificacaoIntermediarioServico intermediarioServicoField;

        private tcIdentificacaoOrgaoGerador orgaoGeradorField;

        private tcDadosConstrucaoCivil contrucaoCivilField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string CodigoVerificacao
        {
            get
            {
                return this.codigoVerificacaoField;
            }
            set
            {
                this.codigoVerificacaoField = value;
            }
        }

        /// <remarks/>
        public System.DateTime DataEmissao
        {
            get
            {
                return this.dataEmissaoField;
            }
            set
            {
                this.dataEmissaoField = value;
            }
        }

        /// <remarks/>
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
                        
        public System.DateTime DataEmissaoRps
        {
            get
            {
                return this.dataEmissaoRpsField;
            }
            set
            {
                this.dataEmissaoRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataEmissaoRpsSpecified
        {
            get
            {
                return this.dataEmissaoRpsFieldSpecified;
            }
            set
            {
                this.dataEmissaoRpsFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte NaturezaOperacao
        {
            get
            {
                return this.naturezaOperacaoField;
            }
            set
            {
                this.naturezaOperacaoField = value;
            }
        }

        /// <remarks/>
        public sbyte RegimeEspecialTributacao
        {
            get
            {
                return this.regimeEspecialTributacaoField;
            }
            set
            {
                this.regimeEspecialTributacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RegimeEspecialTributacaoSpecified
        {
            get
            {
                return this.regimeEspecialTributacaoFieldSpecified;
            }
            set
            {
                this.regimeEspecialTributacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte OptanteSimplesNacional
        {
            get
            {
                return this.optanteSimplesNacionalField;
            }
            set
            {
                this.optanteSimplesNacionalField = value;
            }
        }

        /// <remarks/>
        public sbyte IncentivadorCultural
        {
            get
            {
                return this.incentivadorCulturalField;
            }
            set
            {
                this.incentivadorCulturalField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Competencia
        {
            get
            {
                return this.competenciaField;
            }
            set
            {
                this.competenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string NfseSubstituida
        {
            get
            {
                return this.nfseSubstituidaField;
            }
            set
            {
                this.nfseSubstituidaField = value;
            }
        }

        /// <remarks/>
        public string OutrasInformacoes
        {
            get
            {
                return this.outrasInformacoesField;
            }
            set
            {
                this.outrasInformacoesField = value;
            }
        }

        /// <remarks/>
        public tcDadosServico Servico
        {
            get
            {
                return this.servicoField;
            }
            set
            {
                this.servicoField = value;
            }
        }

        /// <remarks/>
        public decimal ValorCredito
        {
            get
            {
                return this.valorCreditoField;
            }
            set
            {
                this.valorCreditoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCreditoSpecified
        {
            get
            {
                return this.valorCreditoFieldSpecified;
            }
            set
            {
                this.valorCreditoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public tcDadosPrestador PrestadorServico
        {
            get
            {
                return this.prestadorServicoField;
            }
            set
            {
                this.prestadorServicoField = value;
            }
        }

        /// <remarks/>
        public tcDadosTomador TomadorServico
        {
            get
            {
                return this.tomadorServicoField;
            }
            set
            {
                this.tomadorServicoField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoIntermediarioServico IntermediarioServico
        {
            get
            {
                return this.intermediarioServicoField;
            }
            set
            {
                this.intermediarioServicoField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoOrgaoGerador OrgaoGerador
        {
            get
            {
                return this.orgaoGeradorField;
            }
            set
            {
                this.orgaoGeradorField = value;
            }
        }

        /// <remarks/>
        public tcDadosConstrucaoCivil ContrucaoCivil
        {
            get
            {
                return this.contrucaoCivilField;
            }
            set
            {
                this.contrucaoCivilField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcIdentificacaoRps
    {

        private string numeroField;

        private string serieField;

        private sbyte tipoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string Serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        public sbyte Tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcDadosServico
    {

        private tcValores valoresField;

        private string itemListaServicoField;

        private int codigoCnaeField;

        private bool codigoCnaeFieldSpecified;

        private string codigoTributacaoMunicipioField;

        private string discriminacaoField;

        private int codigoMunicipioField;

        /// <remarks/>
        public tcValores Valores
        {
            get
            {
                return this.valoresField;
            }
            set
            {
                this.valoresField = value;
            }
        }

        /// <remarks/>
        public string ItemListaServico
        {
            get
            {
                return this.itemListaServicoField;
            }
            set
            {
                this.itemListaServicoField = value;
            }
        }

        /// <remarks/>
        public int CodigoCnae
        {
            get
            {
                return this.codigoCnaeField;
            }
            set
            {
                this.codigoCnaeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CodigoCnaeSpecified
        {
            get
            {
                return this.codigoCnaeFieldSpecified;
            }
            set
            {
                this.codigoCnaeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string CodigoTributacaoMunicipio
        {
            get
            {
                return this.codigoTributacaoMunicipioField;
            }
            set
            {
                this.codigoTributacaoMunicipioField = value;
            }
        }

        /// <remarks/>
        public string Discriminacao
        {
            get
            {
                return this.discriminacaoField;
            }
            set
            {
                this.discriminacaoField = value;
            }
        }

        /// <remarks/>
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcValores
    {

        private decimal valorServicosField;

        private decimal valorDeducoesField;

        private bool valorDeducoesFieldSpecified;

        private decimal valorPisField;

        private bool valorPisFieldSpecified;

        private decimal valorCofinsField;

        private bool valorCofinsFieldSpecified;

        private decimal valorInssField;

        private bool valorInssFieldSpecified;

        private decimal valorIrField;

        private bool valorIrFieldSpecified;

        private decimal valorCsllField;

        private bool valorCsllFieldSpecified;

        private sbyte issRetidoField;

        private decimal valorIssField;

        private bool valorIssFieldSpecified;

        private decimal valorIssRetidoField;

        private bool valorIssRetidoFieldSpecified;

        private decimal outrasRetencoesField;

        private bool outrasRetencoesFieldSpecified;

        private decimal baseCalculoField;

        private bool baseCalculoFieldSpecified;

        private decimal aliquotaField;

        private bool aliquotaFieldSpecified;

        private decimal valorLiquidoNfseField;

        private bool valorLiquidoNfseFieldSpecified;

        private decimal descontoIncondicionadoField;

        private bool descontoIncondicionadoFieldSpecified;

        private decimal descontoCondicionadoField;

        private bool descontoCondicionadoFieldSpecified;

        /// <remarks/>
        public decimal ValorServicos
        {
            get
            {
                return this.valorServicosField;
            }
            set
            {
                this.valorServicosField = value;
            }
        }

        /// <remarks/>
        public decimal ValorDeducoes
        {
            get
            {
                return this.valorDeducoesField;
            }
            set
            {
                this.valorDeducoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorDeducoesSpecified
        {
            get
            {
                return this.valorDeducoesFieldSpecified;
            }
            set
            {
                this.valorDeducoesFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorPis
        {
            get
            {
                return this.valorPisField;
            }
            set
            {
                this.valorPisField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorPisSpecified
        {
            get
            {
                return this.valorPisFieldSpecified;
            }
            set
            {
                this.valorPisFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorCofins
        {
            get
            {
                return this.valorCofinsField;
            }
            set
            {
                this.valorCofinsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCofinsSpecified
        {
            get
            {
                return this.valorCofinsFieldSpecified;
            }
            set
            {
                this.valorCofinsFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorInss
        {
            get
            {
                return this.valorInssField;
            }
            set
            {
                this.valorInssField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorInssSpecified
        {
            get
            {
                return this.valorInssFieldSpecified;
            }
            set
            {
                this.valorInssFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorIr
        {
            get
            {
                return this.valorIrField;
            }
            set
            {
                this.valorIrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIrSpecified
        {
            get
            {
                return this.valorIrFieldSpecified;
            }
            set
            {
                this.valorIrFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorCsll
        {
            get
            {
                return this.valorCsllField;
            }
            set
            {
                this.valorCsllField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCsllSpecified
        {
            get
            {
                return this.valorCsllFieldSpecified;
            }
            set
            {
                this.valorCsllFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte IssRetido
        {
            get
            {
                return this.issRetidoField;
            }
            set
            {
                this.issRetidoField = value;
            }
        }

        /// <remarks/>
        public decimal ValorIss
        {
            get
            {
                return this.valorIssField;
            }
            set
            {
                this.valorIssField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIssSpecified
        {
            get
            {
                return this.valorIssFieldSpecified;
            }
            set
            {
                this.valorIssFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorIssRetido
        {
            get
            {
                return this.valorIssRetidoField;
            }
            set
            {
                this.valorIssRetidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIssRetidoSpecified
        {
            get
            {
                return this.valorIssRetidoFieldSpecified;
            }
            set
            {
                this.valorIssRetidoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal OutrasRetencoes
        {
            get
            {
                return this.outrasRetencoesField;
            }
            set
            {
                this.outrasRetencoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OutrasRetencoesSpecified
        {
            get
            {
                return this.outrasRetencoesFieldSpecified;
            }
            set
            {
                this.outrasRetencoesFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal BaseCalculo
        {
            get
            {
                return this.baseCalculoField;
            }
            set
            {
                this.baseCalculoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BaseCalculoSpecified
        {
            get
            {
                return this.baseCalculoFieldSpecified;
            }
            set
            {
                this.baseCalculoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal Aliquota
        {
            get
            {
                return this.aliquotaField;
            }
            set
            {
                this.aliquotaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaSpecified
        {
            get
            {
                return this.aliquotaFieldSpecified;
            }
            set
            {
                this.aliquotaFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorLiquidoNfse
        {
            get
            {
                return this.valorLiquidoNfseField;
            }
            set
            {
                this.valorLiquidoNfseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorLiquidoNfseSpecified
        {
            get
            {
                return this.valorLiquidoNfseFieldSpecified;
            }
            set
            {
                this.valorLiquidoNfseFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal DescontoIncondicionado
        {
            get
            {
                return this.descontoIncondicionadoField;
            }
            set
            {
                this.descontoIncondicionadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DescontoIncondicionadoSpecified
        {
            get
            {
                return this.descontoIncondicionadoFieldSpecified;
            }
            set
            {
                this.descontoIncondicionadoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal DescontoCondicionado
        {
            get
            {
                return this.descontoCondicionadoField;
            }
            set
            {
                this.descontoCondicionadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DescontoCondicionadoSpecified
        {
            get
            {
                return this.descontoCondicionadoFieldSpecified;
            }
            set
            {
                this.descontoCondicionadoFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcDadosPrestador
    {

        private tcIdentificacaoPrestador identificacaoPrestadorField;

        private string razaoSocialField;

        private string nomeFantasiaField;

        private tcEndereco enderecoField;

        private tcContato contatoField;

        /// <remarks/>
        public tcIdentificacaoPrestador IdentificacaoPrestador
        {
            get
            {
                return this.identificacaoPrestadorField;
            }
            set
            {
                this.identificacaoPrestadorField = value;
            }
        }

        /// <remarks/>
        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public string NomeFantasia
        {
            get
            {
                return this.nomeFantasiaField;
            }
            set
            {
                this.nomeFantasiaField = value;
            }
        }

        /// <remarks/>
        public tcEndereco Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public tcContato Contato
        {
            get
            {
                return this.contatoField;
            }
            set
            {
                this.contatoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcIdentificacaoPrestador
    {

        private string cnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public string Cnpj
        {
            get
            {
                return this.cnpjField;
            }
            set
            {
                this.cnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcEndereco
    {

        private string enderecoField;

        private string numeroField;

        private string complementoField;

        private string bairroField;

        private int codigoMunicipioField;

        private bool codigoMunicipioFieldSpecified;

        private string ufField;

        private int cepField;

        private bool cepFieldSpecified;

        /// <remarks/>
        public string Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string Complemento
        {
            get
            {
                return this.complementoField;
            }
            set
            {
                this.complementoField = value;
            }
        }

        /// <remarks/>
        public string Bairro
        {
            get
            {
                return this.bairroField;
            }
            set
            {
                this.bairroField = value;
            }
        }

        /// <remarks/>
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CodigoMunicipioSpecified
        {
            get
            {
                return this.codigoMunicipioFieldSpecified;
            }
            set
            {
                this.codigoMunicipioFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public int Cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CepSpecified
        {
            get
            {
                return this.cepFieldSpecified;
            }
            set
            {
                this.cepFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcContato
    {

        private string telefoneField;

        private string emailField;

        /// <remarks/>
        public string Telefone
        {
            get
            {
                return this.telefoneField;
            }
            set
            {
                this.telefoneField = value;
            }
        }

        /// <remarks/>
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcDadosTomador
    {

        private tcIdentificacaoTomador identificacaoTomadorField;

        private string razaoSocialField;

        private tcEndereco enderecoField;

        private tcContato contatoField;

        /// <remarks/>
        public tcIdentificacaoTomador IdentificacaoTomador
        {
            get
            {
                return this.identificacaoTomadorField;
            }
            set
            {
                this.identificacaoTomadorField = value;
            }
        }

        /// <remarks/>
        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public tcEndereco Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public tcContato Contato
        {
            get
            {
                return this.contatoField;
            }
            set
            {
                this.contatoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcIdentificacaoTomador
    {

        private tcCpfCnpj cpfCnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public tcCpfCnpj CpfCnpj
        {
            get
            {
                return this.cpfCnpjField;
            }
            set
            {
                this.cpfCnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcCpfCnpj
    {

        private string itemField;

        private ItemChoiceType itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cnpj", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Cpf", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcIdentificacaoIntermediarioServico
    {

        private string razaoSocialField;

        private tcCpfCnpj cpfCnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public tcCpfCnpj CpfCnpj
        {
            get
            {
                return this.cpfCnpjField;
            }
            set
            {
                this.cpfCnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcIdentificacaoOrgaoGerador
    {

        private int codigoMunicipioField;

        private string ufField;

        /// <remarks/>
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }

        /// <remarks/>
        public string Uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class tcDadosConstrucaoCivil
    {

        private string codigoObraField;

        private string artField;

        /// <remarks/>
        public string CodigoObra
        {
            get
            {
                return this.codigoObraField;
            }
            set
            {
                this.codigoObraField = value;
            }
        }

        /// <remarks/>
        public string Art
        {
            get
            {
                return this.artField;
            }
            set
            {
                this.artField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcCancelamentoNfse
    {

        private tcConfirmacaoCancelamento confirmacaoField;      

        /// <remarks/>
        public tcConfirmacaoCancelamento Confirmacao
        {
            get
            {
                return this.confirmacaoField;
            }
            set
            {
                this.confirmacaoField = value;
            }
        }
       
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcConfirmacaoCancelamento
    {

        private tcPedidoCancelamento pedidoField;

        private tcInfConfirmacaoCancelamento infConfirmacaoCancelamentoField;

        private string idField;

        /// <remarks/>
        public tcPedidoCancelamento Pedido
        {
            get
            {
                return this.pedidoField;
            }
            set
            {
                this.pedidoField = value;
            }
        }

        /// <remarks/>
        public tcInfConfirmacaoCancelamento InfConfirmacaoCancelamento
        {
            get
            {
                return this.infConfirmacaoCancelamentoField;
            }
            set
            {
                this.infConfirmacaoCancelamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcPedidoCancelamento
    {

        private tcInfPedidoCancelamento infPedidoCancelamentoField;

      
        /// <remarks/>
        public tcInfPedidoCancelamento InfPedidoCancelamento
        {
            get
            {
                return this.infPedidoCancelamentoField;
            }
            set
            {
                this.infPedidoCancelamentoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcInfPedidoCancelamento
    {

        private tcIdentificacaoNfse identificacaoNfseField;

        private string codigoCancelamentoField;

        private string idField;

        /// <remarks/>
        public tcIdentificacaoNfse IdentificacaoNfse
        {
            get
            {
                return this.identificacaoNfseField;
            }
            set
            {
                this.identificacaoNfseField = value;
            }
        }

        /// <remarks/>
        public string CodigoCancelamento
        {
            get
            {
                return this.codigoCancelamentoField;
            }
            set
            {
                this.codigoCancelamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcIdentificacaoNfse
    {

        private string numeroField;

        private string cnpjField;

        private string inscricaoMunicipalField;

        private int codigoMunicipioField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string Cnpj
        {
            get
            {
                return this.cnpjField;
            }
            set
            {
                this.cnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }

        /// <remarks/>
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcInfConfirmacaoCancelamento
    {

        private bool sucessoField;

        private System.DateTime dataHoraField;

        /// <remarks/>
        public bool Sucesso
        {
            get
            {
                return this.sucessoField;
            }
            set
            {
                this.sucessoField = value;
            }
        }

        /// <remarks/>
        public System.DateTime DataHora
        {
            get
            {
                return this.dataHoraField;
            }
            set
            {
                this.dataHoraField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcSubstituicaoNfse
    {

        private tcInfSubstituicaoNfse substituicaoNfseField;

           /// <remarks/>
        public tcInfSubstituicaoNfse SubstituicaoNfse
        {
            get
            {
                return this.substituicaoNfseField;
            }
            set
            {
                this.substituicaoNfseField = value;
            }
        }
    
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/tipos_complexos.xsd")]
    public class tcInfSubstituicaoNfse
    {

        private string nfseSubstituidoraField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string NfseSubstituidora
        {
            get
            {
                return this.nfseSubstituidoraField;
            }
            set
            {
                this.nfseSubstituidoraField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class ListaMensagemRetorno
    {

        private tcMensagemRetorno[] mensagemRetornoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MensagemRetorno")]
        public tcMensagemRetorno[] MensagemRetorno
        {
            get
            {
                return this.mensagemRetornoField;
            }
            set
            {
                this.mensagemRetornoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "")]
    public class tcMensagemRetorno
    {

        private string codigoField;

        private string mensagemField;

        private string correcaoField;

        /// <remarks/>
        public string Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        public string Mensagem
        {
            get
            {
                return this.mensagemField;
            }
            set
            {
                this.mensagemField = value;
            }
        }

        /// <remarks/>
        public string Correcao
        {
            get
            {
                return this.correcaoField;
            }
            set
            {
                this.correcaoField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        Cnpj,

        /// <remarks/>
        Cpf,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "", IncludeInSchema = false)]
    public enum ItemsChoiceType3
    {

        /// <remarks/>
        DataRecebimento,

        /// <remarks/>
        NumeroLote,

        /// <remarks/>
        Protocolo,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("http://tempuri.org/tipos_complexos.xsd:ListaMensagemRetorno")]
        ListaMensagemRetorno,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    public class ConsultarLoteRpsRespostaListaNfse
    {

        private tcCompNfse[] compNfseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ComplNfse")]
        public tcCompNfse[] ComplNfse
        {
            get
            {
                return this.compNfseField;
            }
            set
            {
                this.compNfseField = value;
            }
        }
    }

}
