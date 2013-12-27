using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net
{
    public class WebServiceNFSe
    {
        private static List<string> _Padroes = null;

        /// <summary>
        /// lista de padrões usados para preencher o datagrid e pesquisas internas
        /// </summary>
        public static string[] PadroesNFSeList
        {
            get
            {
                if (_Padroes == null)
                {
                    Array arr = Enum.GetValues(typeof(PadroesNFSe));
                    _Padroes = new List<string>();
                    foreach (PadroesNFSe type in arr)
                        _Padroes.Add(type.ToString());
                }
                return _Padroes.ToArray();
            }
        }

        public static string WebServicesHomologacao(PadroesNFSe padrao, int idMunicipio = 0)
        {
            switch (padrao)
            {
                #region THEMA
                case PadroesNFSe.THEMA:
                    switch (idMunicipio)
                    {
                        case 4312401: //Monte Negro - RS 
                            return "<LocalHomologacao>" +
                                   @"<RecepcionarLoteRps>wsdl\homologacao\HMonteNegroRSRemessaNFSE.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HMonteNegroRSConsultarNFSE.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\homologacao\HMonteNegroRSConsultarNFSE.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\homologacao\HMonteNegroRSConsultarNFSE.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\homologacao\HMonteNegroRSConsultarNFSE.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\homologacao\HMonteNegroRSCancelarNFSE.wsdl</CancelarNfse>" +
                                   "</LocalHomologacao>";

                        case 4303103: // Cachoeirinha - RS
                            return "<LocalHomologacao>" +
                                   @"<RecepcionarLoteRps>wsdl\homologacao\HThemaCachoerinhaRSRemessa.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HThemaCachoerinhaRSConsulta.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\homologacao\HThemaCachoerinhaRSConsulta.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\homologacao\HThemaCachoerinhaRSConsulta.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\homologacao\HThemaCachoerinhaRSConsulta.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\homologacao\HThemaCachoerinhaRSCancelamento.wsdl</CancelarNfse>" +
                                   "</LocalHomologacao>";

                        case 4311403: //Lajeado - RS 
                            return "<LocalHomologacao>" +
                                   @"<RecepcionarLoteRps>wsdl\homologacao\HLajeadoRSRemessaNFSE.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HLajeadoRSConsultarNFSE.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\homologacao\HLajeadoRSConsultarNFSE.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\homologacao\HLajeadoRSConsultarNFSE.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\homologacao\HLajeadoRSConsultarNFSE.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\homologacao\HLajeadoRSCancelarNFSE.wsdl</CancelarNfse>" +
                                   "</LocalHomologacao>";


                        default:
                            return "<LocalHomologacao>" +
                                   @"<RecepcionarLoteRps>wsdl\homologacao\HThemaRemessa.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HThemaConsulta.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\homologacao\HThemaConsulta.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\homologacao\HThemaConsulta.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\homologacao\HThemaConsulta.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\homologacao\HThemaCancelamento.wsdl</CancelarNfse>" +
                                   "</LocalHomologacao>";
                    }

                #endregion

                #region GINFES
                case PadroesNFSe.GINFES:
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\hginfes.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\hginfes.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\hginfes.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\hginfes.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\hginfes.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\hginfes.wsdl</CancelarNfse>" +
                            "</LocalHomologacao>";
                #endregion

                #region BETHA
                case PadroesNFSe.BETHA:
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HBethaRecepcionarLoteRps.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HBethaConsultarSituacaoLoteRPS.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HBethaConsultarNFSePorRPS.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HBethaConsultarNFSe.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HBethaConsultarLoteRPS.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HBethaCancelarNFSe.wsdl</CancelarNfse>" +
                            "</LocalHomologacao>";
                #endregion

                #region SALVADOR_BA
                case PadroesNFSe.SALVADOR_BA:
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HSalvadorBAEnvioLoteRPS.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HSalvadorBAConsultaSituacaoLoteRPS.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HSalvadorBAConsultaNfseRPS.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HSalvadorBAConsultaNfse.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HSalvadorBAConsultaLoteRPS.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HSalvadorBA.wsdl</CancelarNfse>" +
                            "</LocalHomologacao>";
                #endregion

                #region CANOAS_RS
                case PadroesNFSe.CANOAS_RS:
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HCanoasRSRecepcionarLoteRps.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HCanoasRSConsultarSituacaoLoteRps.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HCanoasRSConsultarNfsePorRps.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HCanoasRSConsultarNfse.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HCanoasRSConsultarLoteRps.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HCanoasRSCancelarNfse.wsdl</CancelarNfse>" +
                            "</LocalHomologacao>";
                #endregion

                #region ISSNET
                case PadroesNFSe.ISSNET:
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HISSNet.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HISSNet.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HISSNet.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HISSNet.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HISSNet.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HISSNet.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\homologacao\HISSNet.wsdl</ConsultarURLNfse>" +
                            "</LocalHomologacao>";
                #endregion

                #region ISSONLINE
                case PadroesNFSe.ISSONLINE:
                    switch (idMunicipio)
                    {
                        case 3502804: //Aracatuba - SP
                            return "<LocalHomologacao>" +
                                    @"<RecepcionarLoteRps>wsdl\homologacao\HISSOnLineAracatubaSP.wsdl</RecepcionarLoteRps>" +
                                    @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HISSOnLineAracatubaSP.wsdl</ConsultarSituacaoLoteRps>" +
                                    @"<ConsultarNfsePorRps>wsdl\homologacao\HISSOnLineAracatubaSP.wsdl</ConsultarNfsePorRps>" +
                                    @"<ConsultarNfse>wsdl\homologacao\HISSOnLineAracatubaSP.wsdl</ConsultarNfse>" +
                                    @"<ConsultarLoteRps>wsdl\homologacao\HISSOnLineAracatubaSP.wsdl</ConsultarLoteRps>" +
                                    @"<CancelarNfse>wsdl\homologacao\HISSOnLineAracatubaSP.wsdl</CancelarNfse>" +
                                    "</LocalHomologacao>";

                        default: //Apucarana - PR
                            return "<LocalHomologacao>" +
                                    @"<RecepcionarLoteRps>wsdl\homologacao\HISSOnLineApucarana.wsdl</RecepcionarLoteRps>" +
                                    @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HISSOnLineApucarana.wsdl</ConsultarSituacaoLoteRps>" +
                                    @"<ConsultarNfsePorRps>wsdl\homologacao\HISSOnLineApucarana.wsdl</ConsultarNfsePorRps>" +
                                    @"<ConsultarNfse>wsdl\homologacao\HISSOnLineApucarana.wsdl</ConsultarNfse>" +
                                    @"<ConsultarLoteRps>wsdl\homologacao\HISSOnLineApucarana.wsdl</ConsultarLoteRps>" +
                                    @"<CancelarNfse>wsdl\homologacao\HISSOnLineApucarana.wsdl</CancelarNfse>" +
                                    "</LocalHomologacao>";
                    }
                #endregion

                #region BLUMENAU_SC
                case PadroesNFSe.BLUMENAU_SC:
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HBlumenauSC.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HBlumenauSC.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HBlumenauSC.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HBlumenauSC.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HBlumenauSC.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HBlumenauSC.wsdl</CancelarNfse>" +
                            "</LocalHomologacao>";
                #endregion

                #region BHISS
                case PadroesNFSe.BHISS:
                    if (idMunicipio == 3106200) //Belo Horizonte - MG
                    {
                        return "<LocalHomologacao>" +
                                @"<RecepcionarLoteRps>wsdl\homologacao\HBeloHorizonteMG-BHISS.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HBeloHorizonteMG-BHISS.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\homologacao\HBeloHorizonteMG-BHISS.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\homologacao\HBeloHorizonteMG-BHISS.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\homologacao\HBeloHorizonteMG-BHISS.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\homologacao\HBeloHorizonteMG-BHISS.wsdl</CancelarNfse>" +
                                "</LocalHomologacao>";
                    }
                    else //Juiz de Fora - MG
                    {
                        return "<LocalHomologacao>" +
                                @"<RecepcionarLoteRps>wsdl\homologacao\HJuizdeForaMG.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HJuizdeForaMG.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\homologacao\HJuizdeForaMG.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\homologacao\HJuizdeForaMG.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\homologacao\HJuizdeForaMG.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\homologacao\HJuizdeForaMG.wsdl</CancelarNfse>" +
                                "</LocalHomologacao>";
                    }
                #endregion

                #region GIF
                case PadroesNFSe.GIF:
                    if (idMunicipio == 4314050) // Parobé - RS
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\HParobéRSGIFServicos.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\HParobéRSGIFServicos.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\HParobéRSGIFServicos.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\HParobéRSGIFServicos.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\HParobéRSGIFServicos.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\HParobéRSGIFServicos.wsdl</CancelarNfse>" +
                                @"<ConsultarURLNfse>wsdl\producao\HParobéRSGIFServicos.wsdl</ConsultarURLNfse>" +
                                @"</LocalProducao>";
                    }
                    else
                    {
                        return "<LocalHomologacao>" +
                                @"<RecepcionarLoteRps>wsdl\homologacao\HCampoBomRS.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HCampoBomRS.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\homologacao\HCampoBomRS.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\homologacao\HCampoBomRS.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\homologacao\HCampoBomRS.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\homologacao\HCampoBomRS.wsdl</CancelarNfse>" +
                                @"<ConsultarURLNfse>wsdl\homologacao\HCampoBomRS.wsdl</ConsultarURLNfse>" +
                                @"</LocalHomologacao>";
                    }
                #endregion

                #region DUETO
                case PadroesNFSe.DUETO:
                    if (idMunicipio == 4310207) // Ijuí - RS
                    {
                        return "<LocalHomologacao>" +
                                @"<RecepcionarLoteRps>wsdl\homologacao\HIjuiRS-Dueto.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HIjuiRS-Dueto.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\homologacao\HIjuiRS-Dueto.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\homologacao\HIjuiRS-Dueto.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\homologacao\HIjuiRS-Dueto.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\homologacao\HIjuiRS-Dueto.wsdl</CancelarNfse>" +
                                @"</LocalHomologacao>";
                    }
                    else // Nova Santa Rita - RS
                    {
                        return "<LocalHomologacao>" +
                                @"<RecepcionarLoteRps>wsdl\homologacao\HNovaSantaRitaRS-Dueto.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HNovaSantaRitaRS-Dueto.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\homologacao\HNovaSantaRitaRS-Dueto.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\homologacao\HNovaSantaRitaRS-Dueto.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\homologacao\HNovaSantaRitaRS-Dueto.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\homologacao\HNovaSantaRitaRS-Dueto.wsdl</CancelarNfse>" +
                                @"</LocalHomologacao>";
                    }
                #endregion

                #region WEBISS
                case PadroesNFSe.WEBISS: // Feira de Santana - BA
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\homologacao\HFeiradeSantanaBA_WebISS.wsdl</ConsultarURLNfse>" +
                            @"</LocalHomologacao>";
                #endregion

                #region PAULISTANA
                case PadroesNFSe.PAULISTANA: // São Paulo - SP
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\homologacao\HSaoPauloSP-PAULISTANA.wsdl</ConsultarURLNfse>" +
                            @"</LocalHomologacao>";
                #endregion

                #region PORTOVELHENSE
                case PadroesNFSe.PORTOVELHENSE: // Porto Velho - RO
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HPortoVelhoPO.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\homologacao\HPortoVelhoPO.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HPortoVelhoPO.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HPortoVelhoPO.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HPortoVelhoPO.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HPortoVelhoPO.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\homologacao\HPortoVelhoPO.wsdl</ConsultarURLNfse>" +
                            @"</LocalHomologacao>";
                #endregion

                #region PRONIN
                case PadroesNFSe.PRONIN: // Mirassol - SP
                    return "<LocalHomologacao>" +
                            @"<RecepcionarLoteRps>wsdl\homologacao\HMirassolSP.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\homologacao\HMirassolSP.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\homologacao\HMirassolSP.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\homologacao\HMirassolSP.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\homologacao\HMirassolSP.wsdl</CancelarNfse>" +
                            @"</LocalHomologacao>";
                #endregion

                default:
                    return "<LocalHomologacao></LocalHomologacao>";
            }
        }

        public static string WebServicesProducao(PadroesNFSe padrao, int idMunicipio = 0)
        {
            switch (padrao)
            {
                #region THEMA
                case PadroesNFSe.THEMA:
                    switch (idMunicipio)
                    {
                        case 4312401: // Monte Negro - RS
                            return "<LocalProducao>" +
                                   @"<RecepcionarLoteRps>wsdl\producao\PMonteNegroRSRemessaNFSE.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\producao\PMonteNegroRSConsultarNFSE.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\producao\PMonteNegroRSConsultarNFSE.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\producao\PMonteNegroRSConsultarNFSE.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\producao\PMonteNegroRSConsultarNFSE.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\producao\PMonteNegroRSCancelarNFSE.wsdl</CancelarNfse>" +
                                   "</LocalProducao>";

                        case 4303103: // Cachoeirinha - RS
                            return "<LocalProducao>" +
                                   @"<RecepcionarLoteRps>wsdl\producao\PThemaCachoerinhaRSRemessa.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\producao\PThemaCachoerinhaRSConsulta.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\producao\PThemaCachoerinhaRSConsulta.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\producao\PThemaCachoerinhaRSConsulta.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\producao\PThemaCachoerinhaRSConsulta.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\producao\PThemaCachoerinhaRSCancelamento.wsdl</CancelarNfse>" +
                                   "</LocalProducao>";

                        case 4311403: // Lajeado - RS
                            return "<LocalProducao>" +
                                   @"<RecepcionarLoteRps>wsdl\producao\PLajeadoRSRemessaNFSE.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\producao\PLajeadoRSConsultarNFSE.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\producao\PLajeadoRSConsultarNFSE.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\producao\PLajeadoRSConsultarNFSE.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\producao\PLajeadoRSConsultarNFSE.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\producao\PLajeadoRSCancelarNFSE.wsdl</CancelarNfse>" +
                                   "</LocalProducao>";

                        default:
                            return "<LocalProducao>" +
                                   @"<RecepcionarLoteRps>wsdl\producao\PThemaRemessa.wsdl</RecepcionarLoteRps>" +
                                   @"<ConsultarSituacaoLoteRps>wsdl\producao\PThemaConsulta.wsdl</ConsultarSituacaoLoteRps>" +
                                   @"<ConsultarNfsePorRps>wsdl\producao\PThemaConsulta.wsdl</ConsultarNfsePorRps>" +
                                   @"<ConsultarNfse>wsdl\producao\PThemaConsulta.wsdl</ConsultarNfse>" +
                                   @"<ConsultarLoteRps>wsdl\producao\PThemaConsulta.wsdl</ConsultarLoteRps>" +
                                   @"<CancelarNfse>wsdl\producao\PThemaCancelamento.wsdl</CancelarNfse>" +
                                   "</LocalProducao>";
                    }
                #endregion

                #region GINFES
                case PadroesNFSe.GINFES:
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\pginfes.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\pginfes.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\pginfes.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\pginfes.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\pginfes.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\pginfes.wsdl</CancelarNfse>" +
                            "</LocalProducao>";
                #endregion

                #region BETHA
                case PadroesNFSe.BETHA:
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PBethaRecepcionarLoteRPS.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PBethaConsultarSituacaoLoteRPS.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PBethaConsultarNFSePorRPS.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PBethaConsultarNFSe.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PBethaConsultarLoteRPS.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PBethaCancelarNFSe.wsdl</CancelarNfse>" +
                            "</LocalProducao>";
                #endregion

                #region SALVADOR_BA
                case PadroesNFSe.SALVADOR_BA:
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PSalvadorBAEnvioLoteRPS.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PSalvadorBAConsultaSituacaoLoteRPS.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PSalvadorBAConsultaNfseRPS.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PSalvadorBAConsultaNfse.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PSalvadorBAConsultaLoteRPS.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PSalvadorBA.wsdl</CancelarNfse>" +
                            "</LocalProducao>";
                #endregion

                #region CANOAS_RS
                case PadroesNFSe.CANOAS_RS:
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PCanoasRSRecepcionarLoteRps.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PCanoasRSConsultarSituacaoLoteRps.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PCanoasRSConsultarNfsePorRps.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PCanoasRSConsultarNfse.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PCanoasRSConsultarLoteRps.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PCanoasRSCancelarNfse.wsdl</CancelarNfse>" +
                            "</LocalProducao>";
                #endregion

                #region ISSNET
                case PadroesNFSe.ISSNET:
                    if (idMunicipio == 5201108) //Anapolis - GO
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PISSNetAnapolis.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PISSNetAnapolis.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PISSNetAnapolis.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PISSNetAnapolis.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PISSNetAnapolis.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PISSNetAnapolis.wsdl</CancelarNfse>" +
                                @"<ConsultarURLNfse>wsdl\producao\PISSNetAnapolis.wsdl</ConsultarURLNfse>" +
                                "</LocalProducao>";
                    }
                    else
                    { //Novo Hamburgo - RS (Default)
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PISSNetNovoHamburgo.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PISSNetNovoHamburgo.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PISSNetNovoHamburgo.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PISSNetNovoHamburgo.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PISSNetNovoHamburgo.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PISSNetNovoHamburgo.wsdl</CancelarNfse>" +
                                @"<ConsultarURLNfse>wsdl\producao\PISSNetNovoHamburgo.wsdl</ConsultarURLNfse>" +
                                "</LocalProducao>";
                    }
                #endregion

                #region ISSONLINE
                case PadroesNFSe.ISSONLINE:
                    switch (idMunicipio)
                    {
                        case 3502804: //Aracatuba - SP
                            return "<LocalProducao>" +
                                    @"<RecepcionarLoteRps>wsdl\producao\PISSOnLineAracatubaSP.wsdl</RecepcionarLoteRps>" +
                                    @"<ConsultarSituacaoLoteRps>wsdl\producao\PISSOnLineAracatubaSP.wsdl</ConsultarSituacaoLoteRps>" +
                                    @"<ConsultarNfsePorRps>wsdl\producao\PISSOnLineAracatubaSP.wsdl</ConsultarNfsePorRps>" +
                                    @"<ConsultarNfse>wsdl\producao\PISSOnLineAracatubaSP.wsdl</ConsultarNfse>" +
                                    @"<ConsultarLoteRps>wsdl\producao\PISSOnLineAracatubaSP.wsdl</ConsultarLoteRps>" +
                                    @"<CancelarNfse>wsdl\producao\PISSOnLineAracatubaSP.wsdl</CancelarNfse>" +
                                    "</LocalProducao>";

                        case 3537305: //Penapolis - SP
                            return "<LocalProducao>" +
                                    @"<RecepcionarLoteRps>wsdl\producao\PPenapoisSPIssOnLine.wsdl</RecepcionarLoteRps>" +
                                    @"<ConsultarSituacaoLoteRps>wsdl\producao\PPenapoisSPIssOnLine.wsdl</ConsultarSituacaoLoteRps>" +
                                    @"<ConsultarNfsePorRps>wsdl\producao\PPenapoisSPIssOnLine.wsdl</ConsultarNfsePorRps>" +
                                    @"<ConsultarNfse>wsdl\producao\PPenapoisSPIssOnLine.wsdl</ConsultarNfse>" +
                                    @"<ConsultarLoteRps>wsdl\producao\PPenapoisSPIssOnLine.wsdl</ConsultarLoteRps>" +
                                    @"<CancelarNfse>wsdl\producao\PPenapoisSPIssOnLine.wsdl</CancelarNfse>" +
                                    "</LocalProducao>";

                        default: //Apucarana - PR
                            return "<LocalProducao>" +
                                    @"<RecepcionarLoteRps>wsdl\producao\PISSOnLineApucarana.wsdl</RecepcionarLoteRps>" +
                                    @"<ConsultarSituacaoLoteRps>wsdl\producao\PISSOnLineApucarana.wsdl</ConsultarSituacaoLoteRps>" +
                                    @"<ConsultarNfsePorRps>wsdl\producao\PISSOnLineApucarana.wsdl</ConsultarNfsePorRps>" +
                                    @"<ConsultarNfse>wsdl\producao\PISSOnLineApucarana.wsdl</ConsultarNfse>" +
                                    @"<ConsultarLoteRps>wsdl\producao\PISSOnLineApucarana.wsdl</ConsultarLoteRps>" +
                                    @"<CancelarNfse>wsdl\producao\PISSOnLineApucarana.wsdl</CancelarNfse>" +
                                    "</LocalProducao>";

                    }


                #endregion

                #region BLUMENAU_SC
                case PadroesNFSe.BLUMENAU_SC:
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PBlumenauSC.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PBlumenauSC.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PBlumenauSC.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PBlumenauSC.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PBlumenauSC.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PBlumenauSC.wsdl</CancelarNfse>" +
                            "</LocalProducao>";
                #endregion

                #region BHISS
                case PadroesNFSe.BHISS:
                    if (idMunicipio == 3106200) //Belo Horizonte - MG
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PBeloHorizonteMG-BHISS.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PBeloHorizonteMG-BHISS.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PBeloHorizonteMG-BHISS.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PBeloHorizonteMG-BHISS.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PBeloHorizonteMG-BHISS.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PBeloHorizonteMG-BHISS.wsdl</CancelarNfse>" +
                                "</LocalProducao>";
                    }
                    else //Juiz de Fora - MG
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PJuizdeForaMG.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PJuizdeForaMG.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PJuizdeForaMG.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PJuizdeForaMG.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PJuizdeForaMG.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PJuizdeForaMG.wsdl</CancelarNfse>" +
                                "</LocalProducao>";
                    }
                #endregion

                #region GIF
                case PadroesNFSe.GIF:
                    if (idMunicipio == 4314050) // Parobé - RS
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PParobéRSGIFServicos.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PParobéRSGIFServicos.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PParobéRSGIFServicos.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PParobéRSGIFServicos.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PParobéRSGIFServicos.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PParobéRSGIFServicos.wsdl</CancelarNfse>" +
                                @"<ConsultarURLNfse>wsdl\producao\PParobéRSGIFServicos.wsdl</ConsultarURLNfse>" +
                                @"</LocalProducao>";
                    }
                    else
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PCampoBomRS.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PCampoBomRS.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PCampoBomRS.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PCampoBomRS.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PCampoBomRS.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PCampoBomRS.wsdl</CancelarNfse>" +
                                @"<ConsultarURLNfse>wsdl\producao\PCampoBomRS.wsdl</ConsultarURLNfse>" +
                                @"</LocalProducao>";
                    }
                #endregion

                #region DUETO
                case PadroesNFSe.DUETO:
                    if (idMunicipio == 4310207) // Ijuí - RS
                    {
                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PIjuiRS-Dueto.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PIjuiRS-Dueto.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PIjuiRS-Dueto.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PIjuiRS-Dueto.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PIjuiRS-Dueto.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PIjuiRS-Dueto.wsdl</CancelarNfse>" +
                                @"</LocalProducao>";    //danasa 9-2013
                    }
                    else // Nova Santa Rita - RS
                    {

                        return "<LocalProducao>" +
                                @"<RecepcionarLoteRps>wsdl\producao\PNovaSantaRitaRS-Dueto.wsdl</RecepcionarLoteRps>" +
                                @"<ConsultarSituacaoLoteRps>wsdl\producao\PNovaSantaRitaRS-Dueto.wsdl</ConsultarSituacaoLoteRps>" +
                                @"<ConsultarNfsePorRps>wsdl\producao\PNovaSantaRitaRS-Dueto.wsdl</ConsultarNfsePorRps>" +
                                @"<ConsultarNfse>wsdl\producao\PNovaSantaRitaRS-Dueto.wsdl</ConsultarNfse>" +
                                @"<ConsultarLoteRps>wsdl\producao\PNovaSantaRitaRS-Dueto.wsdl</ConsultarLoteRps>" +
                                @"<CancelarNfse>wsdl\producao\PNovaSantaRitaRS-Dueto.wsdl</CancelarNfse>" +
                                @"</LocalProducao>";
                    }
                #endregion

                #region WEBISS
                case PadroesNFSe.WEBISS: // Feira de Santana - BA
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\producao\PFeiradeSantanaBA_WebISS.wsdl</ConsultarURLNfse>" +
                            @"</LocalProducao>";
                #endregion

                #region PAULISTANA
                case PadroesNFSe.PAULISTANA: // São Paulo - SP
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\producao\PSaoPauloSP-PAULISTANA.wsdl</ConsultarURLNfse>" +
                            @"</LocalProducao>";
                #endregion

                #region PORTOVELHENSE
                case PadroesNFSe.PORTOVELHENSE: // Porto Velho - RO
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PPortoVelhoPO.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarSituacaoLoteRps>wsdl\producao\PPortoVelhoPO.wsdl</ConsultarSituacaoLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PPortoVelhoPO.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PPortoVelhoPO.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PPortoVelhoPO.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PPortoVelhoPO.wsdl</CancelarNfse>" +
                            @"<ConsultarURLNfse>wsdl\producao\PPortoVelhoPO.wsdl</ConsultarURLNfse>" +
                            @"</LocalProducao>";
                #endregion

                #region PRONIN
                case PadroesNFSe.PRONIN: // Mirassol - SP
                    return "<LocalProducao>" +
                            @"<RecepcionarLoteRps>wsdl\producao\PMirassolSP.wsdl</RecepcionarLoteRps>" +
                            @"<ConsultarNfsePorRps>wsdl\producao\PMirassolSP.wsdl</ConsultarNfsePorRps>" +
                            @"<ConsultarNfse>wsdl\producao\PMirassolSP.wsdl</ConsultarNfse>" +
                            @"<ConsultarLoteRps>wsdl\producao\PMirassolSP.wsdl</ConsultarLoteRps>" +
                            @"<CancelarNfse>wsdl\producao\PMirassolSP.wsdl</CancelarNfse>" +
                            @"</LocalProducao>";
                #endregion

                default:
                    return "<LocalProducao></LocalProducao>";
            }
        }

        public static PadroesNFSe GetPadraoFromString(string padrao)
        {
            Array arr = Enum.GetValues(typeof(PadroesNFSe));
            foreach (PadroesNFSe type in arr)
                if (padrao.ToLower() == type.ToString().ToLower())
                    return type;

            return PadroesNFSe.NaoIdentificado;
        }

        public static void SavePadrao(string uf, string cidade, int codigomunicipio, string padrao, bool forcaAtualizacao)
        {
            try
            {
                Municipio mun = null;
                for (int i = 0; i < Propriedade.Municipios.Count; ++i)
                    if (Propriedade.Municipios[i].CodigoMunicipio == codigomunicipio)
                    {
                        mun = Propriedade.Municipios[i];
                        break;
                    }

                if (padrao == PadroesNFSe.NaoIdentificado.ToString() && mun != null)
                    Propriedade.Municipios.Remove(mun);

                if (padrao != PadroesNFSe.NaoIdentificado.ToString())
                {
                    if (mun != null)
                    {
                        ///
                        /// é o mesmo padrão definido?
                        /// o parametro "forcaAtualizacao" é "true" somente quando vier da aba "Municipios definidos"
                        /// desde que o datagrid atualiza automaticamente o membro "padrao" da classe "Municipio" quando ele é alterado.
                        if (mun.PadraoStr == padrao && !forcaAtualizacao)
                            return;

                        mun.Padrao = GetPadraoFromString(padrao);
                        mun.PadraoStr = padrao;
                    }
                    else
                        Propriedade.Municipios.Add(new Municipio(codigomunicipio, uf, cidade.Trim(), GetPadraoFromString(padrao)));
                }

                if (System.IO.File.Exists(Propriedade.NomeArqXMLMunicipios))
                {
                    ///
                    /// faz uma copia por segurança
                    if (System.IO.File.Exists(Propriedade.NomeArqXMLMunicipios + ".bck"))
                        System.IO.File.Delete(Propriedade.NomeArqXMLMunicipios + ".bck");
                    System.IO.File.Copy(Propriedade.NomeArqXMLMunicipios, Propriedade.NomeArqXMLMunicipios + ".bck");
                }

                /*
                <nfes_municipios>
                    <Registro ID="4125506" Nome="São José dos Pinais - PR" Padrao="GINFES" />
                </nfes_municipios>
                 */
                XmlWriter oXmlGravar = null;
                XmlWriterSettings oSettings = new XmlWriterSettings();
                UTF8Encoding c = new UTF8Encoding(false);
                oSettings.Encoding = c;
                oSettings.Indent = true;
                oSettings.IndentChars = "";
                oSettings.NewLineOnAttributes = false;
                oSettings.OmitXmlDeclaration = false;
                oXmlGravar = XmlWriter.Create(Propriedade.NomeArqXMLMunicipios, oSettings);

                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("nfes_municipios");
                {
                    foreach (Municipio item in Propriedade.Municipios)
                    {
                        oXmlGravar.WriteStartElement("Registro");
                        {
                            oXmlGravar.WriteStartAttribute("ID");
                            oXmlGravar.WriteString(item.CodigoMunicipio.ToString());
                            oXmlGravar.WriteEndAttribute();

                            oXmlGravar.WriteStartAttribute("Nome");
                            oXmlGravar.WriteString(item.Nome);
                            oXmlGravar.WriteEndAttribute();

                            oXmlGravar.WriteStartAttribute("Padrao");
                            oXmlGravar.WriteString(item.PadraoStr);
                            oXmlGravar.WriteEndAttribute();
                        }
                        oXmlGravar.WriteEndElement();   //Registro
                    }
                }
                oXmlGravar.WriteEndElement(); //nfes_municipios
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
                oXmlGravar.Close();
            }
            catch
            {
                //recupera a copia feita se houve erro na criacao do XML de municipios
                if (System.IO.File.Exists(Propriedade.NomeArqXMLMunicipios + ".bck"))
                    Functions.Move(Propriedade.NomeArqXMLMunicipios + ".bck", Propriedade.NomeArqXMLMunicipios);
                throw;
            }
        }

        /// <summary>
        /// Responsavel pela gravacao do arquivo de muncipios, caso nao exista
        /// </summary>
        public static void Start()
        {
            if (!System.IO.File.Exists(Propriedade.NomeArqXMLMunicipios) && System.IO.File.Exists(Propriedade.NomeArqXMLWebService))
            {
                XmlWriter oXmlGravar = null;
                XmlWriterSettings oSettings = new XmlWriterSettings();
                UTF8Encoding c = new UTF8Encoding(false);
                oSettings.Encoding = c;
                oSettings.Indent = true;
                oSettings.IndentChars = "";
                oSettings.NewLineOnAttributes = false;
                oSettings.OmitXmlDeclaration = false;
                oXmlGravar = XmlWriter.Create(Propriedade.NomeArqXMLMunicipios, oSettings);
                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("nfes_municipios");

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(Propriedade.NomeArqXMLWebService);
                    XmlNodeList estadoList = doc.GetElementsByTagName("Estado");
                    foreach (XmlNode estadoNode in estadoList)
                    {
                        XmlElement estadoElemento = (XmlElement)estadoNode;
                        if (estadoElemento.Attributes.Count > 0)
                        {
                            if (estadoElemento.Attributes[2].Value != "XX")
                            {
                                int ID = Convert.ToInt32(estadoElemento.Attributes[0].Value);
                                string Nome = estadoElemento.Attributes[1].Value;
                                string UF = estadoElemento.Attributes[2].Value;

                                string padrao = PadroesNFSe.NaoIdentificado.ToString();
                                XmlNodeList urlList = estadoElemento.GetElementsByTagName("LocalHomologacao");
                                if (urlList.Count > 0)
                                    ///
                                    /// verifica qual o padrao com base nas url's
                                    foreach (string p0 in PadroesNFSeList)
                                        if (p0.ToLower() != PadroesNFSe.NaoIdentificado.ToString().ToLower())
                                            if (urlList[0].ChildNodes[0].InnerText.ToLower().IndexOf(p0.ToLower()) >= 0)
                                            {
                                                padrao = p0;
                                                break;
                                            }

                                if (padrao != PadroesNFSe.NaoIdentificado.ToString())
                                {
                                    oXmlGravar.WriteStartElement("Registro");
                                    {
                                        oXmlGravar.WriteStartAttribute("ID");
                                        oXmlGravar.WriteString(ID.ToString());
                                        oXmlGravar.WriteEndAttribute();

                                        oXmlGravar.WriteStartAttribute("Nome");
                                        oXmlGravar.WriteString(Nome);
                                        oXmlGravar.WriteEndAttribute();

                                        oXmlGravar.WriteStartAttribute("Padrao");
                                        oXmlGravar.WriteString(padrao);
                                        oXmlGravar.WriteEndAttribute();
                                    }
                                    oXmlGravar.WriteEndElement();   //Registro
                                }
                            }
                        }
                    }
                    oXmlGravar.WriteEndElement(); //nfes_municipios
                    oXmlGravar.WriteEndDocument();
                    oXmlGravar.Flush();
                }                
                finally
                {
                    if (oXmlGravar != null)
                        oXmlGravar.Close();
                }
            }
        }
    }
}
