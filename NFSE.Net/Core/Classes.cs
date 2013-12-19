using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Core
{
    #region DadosPedLoteRps
    /// <summary>
    /// Classe com os dados do XML da consulta do lote de rps
    /// </summary>
    public class DadosPedLoteRps
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }
        public string Protocolo { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoMunicipal { get; set; }

        public DadosPedLoteRps(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion

    #region DadosPedSitNfse
    /// <summary>
    /// Classe com os dados do XML da consulta da nfse por numero da nfse
    /// </summary>
    public class DadosPedSitNfse
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }

        public DadosPedSitNfse(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion

    #region DadosPedSitNfseRps
    /// <summary>
    /// Classe com os dados do XML da consulta da nfse por rps
    /// </summary>
    public class DadosPedSitNfseRps
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }

        public DadosPedSitNfseRps(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion

    #region Classe com os dados do XML da consulta do lote de rps
    /// <summary>
    /// Classe com os dados do XML da consulta do lote de rps
    /// </summary>
    public class DadosPedCanNfse
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }

        public DadosPedCanNfse(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion

    #region Classe com os dados do XML da consulta situacao do lote de rps
    /// <summary>
    /// Classe com os dados do XML da consulta do lote de rps
    /// </summary>
    public class DadosPedSitLoteRps
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }

        public DadosPedSitLoteRps(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion

    #region Classe com os dados do XML do Lote RPS
    /// <summary>
    /// Classe com os dados do XML do Lote RPS
    /// </summary>
    public class DadosEnvLoteRps
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }

        public DadosEnvLoteRps(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion

    #region DadosPedURLNfse
    /// <summary>
    /// Classe com os dados do XML da consulta da URL da Nfse
    /// </summary>
    public class DadosPedURLNfse
    {
        public int cMunicipio { get; set; }
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }

        public DadosPedURLNfse(int emp)
        {
            tpEmis = Empresa.Configuracoes[emp].tpEmis;
            tpAmb = Empresa.Configuracoes[emp].tpAmb;
            cMunicipio = Empresa.Configuracoes[emp].UFCod;
        }
    }
    #endregion
}
