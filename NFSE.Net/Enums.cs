using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    #region SubPastas da pasta de enviados
    /// <summary>
    /// SubPastas da pasta de XML´s enviados para os webservices
    /// </summary>
    public enum PastaEnviados
    {
        EmProcessamento,
        Autorizados,
        Denegados
    }
    #endregion
    
    #region Servicos
    /// <summary>
    /// Serviços executados pelo Aplicativo
    /// </summary>
    public enum Servicos
    {
        #region NFSe
        /// <summary>
        /// Enviar Lote RPS NFS-e 
        /// </summary>
        RecepcionarLoteRps,
        /// <summary>
        /// Consultar Situação do lote RPS NFS-e
        /// </summary>
        ConsultarSituacaoLoteRps,
        /// <summary>
        /// Consultar NFS-e por RPS
        /// </summary>
        ConsultarNfsePorRps,
        /// <summary>
        /// Consultar NFS-e por NFS-e
        /// </summary>
        ConsultarNfse,
        /// <summary>
        /// Consultar lote RPS
        /// </summary>
        ConsultarLoteRps,
        /// <summary>
        /// Cancelar NFS-e
        /// </summary>
        CancelarNfse,
        /// <summary>
        /// Consultar a URL de visualização da NFSe
        /// </summary>
        ConsultarURLNfse,
        #endregion

        /// <summary>
        /// Nulo / Nenhum serviço em execução
        /// </summary>        
        Nulo
    }
    #endregion

    #region NF
    /// <summary>
    /// Tipo de ambiente
    /// </summary>
    public enum TpAmb
    {
        /// <summary>
        /// Ambiente de produção
        /// </summary>
        [Description("Produção")]
        Producao = 1,

        /// <summary>
        /// Ambiente de homologação
        /// </summary>
        [Description("Homologação")]
        Homologacao = 2
    }
    #endregion

    #region TipoAplicativo
    public enum TipoAplicativo
    {
        /// <summary>
        /// Aplicativo ou serviços para processamento dos XMLs da NF-e
        /// </summary>
        Nfe = 0,
        /// <summary>
        /// Aplicativo ou serviços para processamento dos XMLs do CT-e
        /// </summary>
        Cte = 1,
        /// <summary>
        /// Aplicativo ou servicos para processamento dos XMLs da NFS-e
        /// </summary>
        Nfse = 2,
        /// <summary>
        /// Aplicativo ou serviços para processamento dos XMLs do MDF-e
        /// </summary>
        MDFe = 3,
        Nulo = 100
    }
    #endregion

    #region Padrão NFSe
    public enum PadroesNFSe
    {
        /// <summary>
        /// Não Identificado
        /// </summary>
        NaoIdentificado,
        /// <summary>
        /// Padrão GINFES
        /// </summary>
        GINFES,
        /// <summary>
        /// Padrão da BETHA Sistemas
        /// </summary>
        BETHA,
        /// <summary>
        /// Padrão da THEMA Informática
        /// </summary>
        THEMA,
        /// <summary>
        /// Padrão da prefeitura de Salvador-BA
        /// </summary>
        SALVADOR_BA,
        /// <summary>
        /// Padrão da prefeitura de Canoas-RS
        /// </summary>
        CANOAS_RS,
        /// <summary>
        /// Padrão da ISS Net
        /// </summary>    
        ISSNET,
        /// <summary>
        /// Padrão da prefeitura de Apucarana-PR
        /// Padrão da prefeitura de Aracatuba-SP
        /// </summary>
        ISSONLINE,
        /// <summary>
        /// Padrão da prefeitura de Blumenau-SC
        /// </summary>
        BLUMENAU_SC,
        /// <summary>
        /// Padrão da prefeitura de Juiz de Fora-MG
        /// </summary>
        BHISS,
        /// <summary>
        /// Padrao GIF
        /// Prefeitura de Campo Bom-RS
        /// </summary>
        GIF,
        /// <summary>
        /// Padrão IPM
        /// <para>Prefeitura de Campo Mourão.</para>
        /// </summary>
        IPM,
        /// <summary>
        /// Padrão DUETO
        /// Prefeitura de Nova Santa Rita - RS
        /// </summary>
        DUETO,
        /// <summary>
        /// Padrão WEB ISS
        /// Prefeitura de Feira de Santana - BA
        /// </summary>
        WEBISS,
        /// <summary>
        /// Padrão Nota Fiscal Eletrônica Paulistana -
        /// Prefeitura São Paulo - SP
        /// </summary>
        PAULISTANA,
        /// <summary>
        /// Padrão Nota Fiscal Eletrônica Porto Velhense
        /// Prefeitura de Porto Velho - RO
        /// </summary>
        PORTOVELHENSE,
        /// <summary>
        /// Padrão Nota Fiscal Eletrônica da PRONIN (GovBR)
        /// Prefeitura de Mirassol - SP
        /// </summary>
        PRONIN

        ///Atencao Wandrey.
        ///o nome deste enum tem que coincidir com o nome da url, pq faço um "IndexOf" deste enum para pegar o padrao
    }
    #endregion

    #region Erros Padrões
    public enum ErroPadrao
    {
        ErroNaoDetectado = 0,
        FalhaInternet = 1,
        FalhaEnvioXmlWS = 2,
        CertificadoVencido = 3,
        FalhaEnvioXmlWSDPEC = 4, //danasa 21/10/2010
        FalhaEnvioXmlNFeWS = 5
    }
    #endregion
}
