using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NFSE.Net.Envio
{
    public abstract class TaskAbst
    {
        #region Objetos
        protected Auxiliar oAux = new Auxiliar();
        protected InvocarObjeto oInvocarObj = new InvocarObjeto();
        protected GerarXML oGerarXML = new GerarXML();
        #endregion

        #region Propriedades

        /// <summary>
        /// Conteúdo do XML de retorno do serviço, ou seja, para cada serviço invocado a classe seta neste atributo a string do XML Retornado pelo serviço
        /// </summary>
        public string vStrXmlRetorno { get; set; }

        /// <summary>
        /// Pasta/Nome do arquivo XML contendo os dados a serem enviados (Nota Fiscal, Pedido de Status, Cancelamento, etc...)
        /// </summary>
        private string mNomeArquivoXML;
        public string NomeArquivoXML
        {
            get
            {
                return this.mNomeArquivoXML;
            }
            set
            {
                this.mNomeArquivoXML = value;
            }
        }

        /// <summary>
        /// Pasta onde será salvo o arquivo de retorno do webservice.
        /// </summary>
        public string SalvarXmlRetornoEm { get; set; }

        /// <summary>
        /// Serviço que está sendo executado (Envio de Nota, Cancelamento, Consulta, etc...)
        /// </summary>
        private Servicos mServico;
        public Servicos Servico
        {
            get
            {
                return this.mServico;
            }
            protected set
            {
                this.mServico = value;
            }
        }

        /// <summary>
        /// Se o vXmlNFeDadosMsg é um XML
        /// </summary>
        public bool vXmlNfeDadosMsgEhXML    //danasa 12-9-2009
        {
            get { return Path.GetExtension(NomeArquivoXML).ToLower() == ".xml"; }
        }
        #endregion

        public abstract void Execute(Empresa empresa);

        #region Métodos para definição dos nomes das classes e métodos da NFe, CTe, NFSe e MDFe

        #region NomeClasseWS()
        /// <summary>
        /// Retorna o nome da classe do serviço passado por parâmetro do WebService do SEFAZ - CTe
        /// </summary>
        /// <param name="servico">Servico</param>
        /// <param name="cUF">Código da UF</param>
        /// <returns>Nome da classe</returns>
        protected string NomeClasseWS(Servicos servico, int cUF)
        {
            string retorna = string.Empty;

            switch (Propriedade.TipoAplicativo)
            {
                case TipoAplicativo.Nfse:
                    retorna = NomeClasseWSNFSe(servico, cUF);
                    break;
            }

            return retorna;
        }
        #endregion


        #region NomeClasseWSNFSe()
        /// <summary>
        /// Retorna o nome da classe do serviço passado por parâmetro do WebService do SEFAZ - CTe
        /// </summary>
        /// <param name="servico">Servico</param>
        /// <param name="cMunicipio">Código do Municipio UF</param>
        /// <returns>Nome da classe</returns>
        private string NomeClasseWSNFSe(Servicos servico, int cMunicipio)
        {
            string retorna = string.Empty;

            switch (Functions.PadraoNFSe(cMunicipio))
            {
                #region GINFES
                case PadroesNFSe.GINFES:
                    retorna = "ServiceGinfesImplService";
                    break;
                #endregion

                #region THEMA
                case PadroesNFSe.THEMA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "NFSEconsulta";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "NFSEconsulta";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "NFSEconsulta";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "NFSEconsulta";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "NFSEcancelamento";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "NFSEremessa";
                            break;
                    }
                    break;
                #endregion

                #region BETHA
                case PadroesNFSe.BETHA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                    }
                    break;
                #endregion

                #region CANOAS-RS (ABACO)
                case PadroesNFSe.CANOAS_RS:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRPS";
                            break;
                    }
                    break;
                #endregion

                #region ISSNet
                case PadroesNFSe.ISSNET:
                    retorna = "Servicos";
                    break;
                #endregion

                #region ISSNet
                case PadroesNFSe.ISSONLINE:
                    retorna = "Nfse";
                    break;
                #endregion

                #region Blumenau-SC
                case PadroesNFSe.BLUMENAU_SC:
                    retorna = "LoteNFe";
                    break;
                #endregion

                #region BHISS
                case PadroesNFSe.BHISS:
                    retorna = "NfseWSService";
                    break;

                #endregion

                #region GIF
                case PadroesNFSe.GIF:
                    retorna = "ServicosService";
                    break;

                #endregion

                #region DUETO
                case PadroesNFSe.DUETO:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "basic_INFSEConsultas";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "basic_INFSEConsultas";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "basic_INFSEConsultas";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "basic_INFSEConsultas";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "basic_INFSEGeracao";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "basic_INFSEGeracao";
                            break;
                    }
                    break;
                #endregion

                #region WEBISS
                case PadroesNFSe.WEBISS:
                    retorna = "NfseServices";
                    break;

                #endregion

                #region PAULISTANA
                case PadroesNFSe.PAULISTANA:
                    retorna = "LoteNFe";
                    break;

                #endregion

                #region SALVADOR_BA
                case PadroesNFSe.SALVADOR_BA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultaLoteRPS";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultaNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultaNfseRPS";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultaSituacaoLoteRPS";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "EnvioLoteRPS";
                            break;
                    }
                    break;
                #endregion

                #region PORTOVELHENSE
                case PadroesNFSe.PORTOVELHENSE:
                    retorna = "NfseWSService";
                    break;

                #endregion

                #region PRONIN
                case PadroesNFSe.PRONIN:
                    switch (servico)
                    {
                        case Servicos.CancelarNfse:
                            retorna = "basic_INFSEGeracao";
                            break;

                        case Servicos.RecepcionarLoteRps:
                            retorna = "basic_INFSEGeracao";
                            break;

                        default:
                            retorna = "basic_INFSEConsultas";
                            break;
                    }
                    break;
                #endregion

            }

            return retorna;
        }
        #endregion

        #region NomeMetodoWS()
        /// <summary>
        /// Retorna o nome do método da classe de serviço
        /// </summary>
        /// <param name="servico">Servico</param>
        /// <param name="cUF">Código da UF</param>
        /// <returns>nome do método da classe de serviço</returns>
        protected string NomeMetodoWS(Servicos servico, int cUF, int tipoAmb)
        {
            string retorna = string.Empty;

            switch (Propriedade.TipoAplicativo)
            {
                case TipoAplicativo.Nfse:
                    retorna = NomeMetodoWSNFSe(servico, cUF, tipoAmb);
                    break;
            }

            return retorna;
        }
        #endregion


        #region NomeMetodoWSNFSe()
        /// <summary>
        /// Retorna o nome da classe do serviço passado por parâmetro do WebService do SEFAZ - CTe
        /// </summary>
        /// <param name="servico">Servico</param>
        /// <param name="cMunicipio">Código do Municipio UF</param>
        /// <returns>Nome da classe</returns>
        private string NomeMetodoWSNFSe(Servicos servico, int cMunicipio, int tipoAmb)
        {
            string retorna = string.Empty;

            switch (Functions.PadraoNFSe(cMunicipio))
            {
                #region GINFES
                case PadroesNFSe.GINFES:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRpsV3";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfseV3";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRpsV3";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRpsV3";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRpsV3";
                            break;
                    }
                    break;
                #endregion

                #region THEMA
                case PadroesNFSe.THEMA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "consultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "consultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "consultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "consultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "cancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "recepcionarLoteRpsLimitado"; //"recepcionarLoteRps";
                            break;
                    }
                    break;
                #endregion

                #region BETHA
                case PadroesNFSe.BETHA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                    }
                    break;
                #endregion

                #region CANOAS - RS (ABACO)
                case PadroesNFSe.CANOAS_RS:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "Execute";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "Execute";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "Execute";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "Execute";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "Execute";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "Execute";
                            break;
                    }
                    break;
                #endregion

                #region ISSNET
                case PadroesNFSe.ISSNET:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultaNFSePorRPS";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultaSituacaoLoteRPS";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                        case Servicos.ConsultarURLNfse:
                            retorna = "ConsultarUrlVisualizacaoNfse";
                            break;
                    }
                    break;
                #endregion

                #region ISSONLINE
                case PadroesNFSe.ISSONLINE:
                    retorna = "Execute";
                    break;
                #endregion

                #region Blumenau-SC
                case PadroesNFSe.BLUMENAU_SC:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultaLote";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultaNFeEmitidas";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultaNFe";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultaInformacoesLote";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelamentoNFe";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            if (tipoAmb == Propriedade.TipoAmbiente.taHomologacao)
                                retorna = "TesteEnvioLoteRPS";
                            else
                                retorna = "EnvioLoteRPS";
                            break;
                    }
                    break;
                #endregion

                #region BHISS
                case PadroesNFSe.BHISS:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                    }
                    break;
                #endregion

                #region GIF
                case PadroesNFSe.GIF:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "consultarNotaFiscal";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "obterCriticaLote";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "anularNotaFiscal";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "enviarLoteNotas";
                            break;
                        case Servicos.ConsultarURLNfse:
                            retorna = "obterNotasEmPNG";
                            break;
                    }
                    break;
                #endregion

                #region DUETO
                case PadroesNFSe.DUETO:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                        case Servicos.ConsultarURLNfse:
                            retorna = "";
                            break;
                    }
                    break;
                #endregion

                #region WEBISS
                case PadroesNFSe.WEBISS:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRps";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                    }
                    break;
                #endregion

                #region PAULISTANA
                case PadroesNFSe.PAULISTANA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultaLote";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultaNFeEmitidas";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultaNFe";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultaInformacoesLote";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "CancelamentoNFe";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            if (tipoAmb == Propriedade.TipoAmbiente.taHomologacao)
                                retorna = "TesteEnvioLoteRPS";
                            else
                                retorna = "EnvioLoteRPS";
                            break;
                    }
                    break;
                #endregion

                #region SALVADOR_BA
                case PadroesNFSe.SALVADOR_BA:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRPS";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfseRPS";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "ConsultarSituacaoLoteRPS";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = "";
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "EnviarLoteRPS";
                            break;
                    }
                    break;
                #endregion

                #region PORTOVELHENSE
                case PadroesNFSe.PORTOVELHENSE:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;
                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfsePorFaixa";
                            break;
                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;
                        case Servicos.ConsultarSituacaoLoteRps:
                            retorna = "";
                            break;
                        case Servicos.CancelarNfse:
                            retorna = ""; //Ainda não implmentado pelo municipio somenete pelo Site - Renan 
                            break;
                        case Servicos.RecepcionarLoteRps:
                            retorna = "GerarNfse";
                            break;
                    }
                    break;


                #endregion

                #region PRONIN
                case PadroesNFSe.PRONIN:
                    switch (servico)
                    {
                        case Servicos.ConsultarLoteRps:
                            retorna = "ConsultarLoteRps";
                            break;

                        case Servicos.ConsultarNfse:
                            retorna = "ConsultarNfse";
                            break;

                        case Servicos.ConsultarNfsePorRps:
                            retorna = "ConsultarNfsePorRps";
                            break;

                        case Servicos.CancelarNfse:
                            retorna = "CancelarNfse";
                            break;

                        case Servicos.RecepcionarLoteRps:
                            retorna = "RecepcionarLoteRps";
                            break;
                    }
                    break;
                #endregion
            }

            return retorna;
        }
        #endregion

        #endregion


        #region XmlRetorno()
        /// <summary>
        /// Auxiliar na geração do arquivo XML de retorno para o ERP quando estivermos utilizando o InvokeMember para chamar o método
        /// </summary>
        /// <param name="arquivoEnvio">Final do nome do arquivo de solicitação do serviço.</param>
        /// <param name="pFinalArqRetorno">Final do nome do arquivo que é para ser gravado o retorno.</param>
        /// <date>07/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void XmlRetorno(string arquivoEnvio, string salvarArquivoRetornoEm)
        {
            oGerarXML.XmlRetorno(arquivoEnvio, salvarArquivoRetornoEm, this.vStrXmlRetorno);
        }
        #endregion
    }
}
