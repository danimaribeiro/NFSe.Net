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

        public DadosPedLoteRps(Core.Empresa empresa)
        {
            tpEmis = empresa.tpEmis;
            tpAmb = empresa.tpAmb;
            cMunicipio = empresa.UFCod;
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

        public DadosPedSitNfse(Core.Empresa empresa)
        {
            tpEmis = empresa.tpEmis;
            tpAmb = empresa.tpAmb;
            cMunicipio = empresa.UFCod;
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

        public DadosPedSitNfseRps(Core.Empresa empresa)
        {
            tpEmis = empresa.tpEmis;
            tpAmb = empresa.tpAmb;
            cMunicipio = empresa.UFCod;
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

        public DadosPedCanNfse(Core.Empresa empresa)
        {
            tpEmis = empresa.tpEmis;
            tpAmb = empresa.tpAmb;
            cMunicipio = empresa.UFCod;
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

        public DadosPedSitLoteRps(Core.Empresa empresa)
        {
            tpEmis = empresa.tpEmis;
            tpAmb = empresa.tpAmb;
            cMunicipio = empresa.UFCod;
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

        public DadosEnvLoteRps(Core.Empresa emp)
        {
            tpEmis = emp.tpEmis;
            tpAmb = emp.tpAmb;
            cMunicipio = emp.UFCod;
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

        public DadosPedURLNfse(Core.Empresa empresa)
        {
            tpEmis = empresa.tpEmis;
            tpAmb = empresa.tpAmb;
            cMunicipio = empresa.UFCod;
        }
    }
    #endregion
}
