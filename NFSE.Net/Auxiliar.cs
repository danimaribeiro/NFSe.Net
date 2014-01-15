using NFSE.Net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net
{
    public class Auxiliar
    {
        #region ExtrairNomeArq()
        /// <summary>
        /// Extrai somente o nome do arquivo de uma string; para ser utilizado na situação desejada. Veja os exemplos na documentação do código.
        /// </summary>
        /// <param name="pPastaArq">String contendo o caminho e nome do arquivo que é para ser extraido o nome.</param>
        /// <param name="pFinalArq">String contendo o final do nome do arquivo até onde é para ser extraído.</param>
        /// <returns>Retorna somente o nome do arquivo de acordo com os parâmetros passados - veja exemplos.</returns>
        /// <example>
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", "-ped-sta.xml"));
        /// //Será demonstrado no message a string "ArqSituacao"
        /// 
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", ".xml"));
        /// //Será demonstrado no message a string "ArqSituacao-ped-sta"
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>19/06/2008</date>
        /// 
#if tirada
        public string xExtrairNomeArq(string pPastaArq, string pFinalArq)
        {
            FileInfo fi = new FileInfo(pPastaArq);
            string ret = fi.Name;
            ret = ret.Substring(0, ret.Length - pFinalArq.Length);
            return ret;
        }
#endif
        #endregion


        #region WriteLog()
        public static void WriteLog(string msg)
        {
            bool geraLog = ConfiguracaoApp.GravarLogOperacoesRealizadas;
            if (geraLog)
                Auxiliar.WriteLog(msg, false);
        }
        #endregion

        #region WriteLog()
        public static void WriteLog(string msg, bool gravarStackTrace)
        {
            bool geraLog = ConfiguracaoApp.GravarLogOperacoesRealizadas;

            if (geraLog)
            {
                string fileName = Propriedade.PastaLog + "\\uninfe_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".log";

                DateTime startTime;
                DateTime stopTime;
                TimeSpan elapsedTime;

                long elapsedMillieconds;
                startTime = DateTime.Now;

                while (true)
                {
                    stopTime = DateTime.Now;
                    elapsedTime = stopTime.Subtract(startTime);
                    elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                    StreamWriter arquivoWS = null;
                    try
                    {
                        //Se for para gravar ot race
                        if (gravarStackTrace)
                        {
                            msg += "\r\nSTACK TRACE:";
                            msg += "\r\n" + Environment.StackTrace;

                            /*
                            StackTrace stackTrace = new StackTrace();
                            StackFrame[] stackFrames = stackTrace.GetFrames();
                            foreach (StackFrame s in stackFrames)
                            {
                                msg += "\r\nModule: " + s.GetMethod().ReflectedType.Module.Name + " Class: " + s.GetMethod().ReflectedType.FullName + " Method: " + s.GetMethod().Name;
                                msg += " line: " + s.GetFileLineNumber();
                                
                            }*/
                        }

                        arquivoWS = new StreamWriter(fileName, true);
                        arquivoWS.WriteLine(DateTime.Now.ToLongTimeString() + "  " + msg);
                        arquivoWS.Flush();
                        arquivoWS.Close();
                        break;
                    }
                    catch
                    {
                        if (arquivoWS != null)
                        {
                            arquivoWS.Close();
                        }

                        if (elapsedMillieconds >= 60000) //60.000 ms que corresponde á 60 segundos que corresponde a 1 minuto
                        {
                            break;
                        }
                    }

                    Thread.Sleep(2);
                }
            }
        }
        #endregion

            

        #region ConversaoNovaVersao()
        /// <summary>
        /// Conversões que são executadas quando atualizado o aplicativo.
        /// Alguns ajustes que são necessários serem executados automaticamente
        /// para evitar falhas no aplicativo
        /// </summary>
        public static string ConversaoNovaVersao(string cnpjEmpresa)    //danasa 20-9-2010
        {
            #region Estamos sem nenhuma conversão no momento
            return "";
            #endregion
        }
        #endregion

        #region DefinirTipoServico()
        /// <summary>
        /// Definir o tipo do servico a ser executado a partir da extensão do arquivo
        /// </summary>
        /// <param name="fullPath">Nome do arquivo completo do qual é para definir o tipo de serviço a ser executado</param>
        /// <returns>Retorna o tipo do serviço que deve ser executado</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 27/04/2011
        /// </remarks>
#if movido_para_processar_cs
        public static Servicos xDefinirTipoServico(int empresa, string fullPath)
        {
            Servicos tipoServico = Servicos.Nulo;

            try
            {
                string arq = fullPath.ToLower().Trim();

                if (arq.IndexOf(Empresa.Configuracoes[empresa].PastaValidar.ToLower()) >= 0)
                {
                    tipoServico = Servicos.AssinarValidar;
                }
                else
                {
                    if (arq.IndexOf(Propriedade.ExtEnvio.PedSit_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.PedSit_TXT) >= 0)
                    {
                        tipoServico = Servicos.PedidoConsultaSituacaoNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedSta_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.PedSta_TXT) >= 0)
                    {
                        tipoServico = Servicos.PedidoConsultaStatusServicoNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.ConsCad_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.ConsCad_TXT) >= 0)
                    {
                        tipoServico = Servicos.ConsultaCadastroContribuinte;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedCan_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.PedCan_TXT) >= 0)
                    {
                        tipoServico = Servicos.CancelarNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedInu_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.PedInu_TXT) >= 0)
                    {
                        tipoServico = Servicos.InutilizarNumerosNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedRec_XML) >= 0)
                    {
                        tipoServico = Servicos.PedidoSituacaoLoteNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.Nfe) >= 0)
                    {
                        FileInfo infArq = new FileInfo(arq);
                        string pastaArq = ConfiguracaoApp.RemoveEndSlash(infArq.DirectoryName).ToLower().Trim();
                        string pastaLote = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[empresa].PastaEnvioEmLote).ToLower().Trim();
                        string pastaEnvio = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[empresa].PastaEnvio).ToLower().Trim();

                        //Remover a subpasta temp
                        if (pastaArq.EndsWith("\\temp"))
                            pastaArq = Path.GetDirectoryName(pastaArq);

                        //Definir o serviço
                        if (pastaArq == pastaLote)
                            tipoServico = Servicos.AssinarValidarNFe;
                        else if (pastaArq == pastaEnvio)
                            tipoServico = Servicos.MontarLoteUmaNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.Nfe_TXT) >= 0)
                    {
                        tipoServico = Servicos.ConverterTXTparaXML;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvLot) >= 0)
                    {
                        tipoServico = Servicos.EnviarLoteNfe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.GerarChaveNFe_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.GerarChaveNFe_TXT) >= 0)
                    {
                        tipoServico = Servicos.GerarChaveNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvWSExiste_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvWSExiste_TXT) >= 0)
                    {
                        tipoServico = Servicos.WSExiste;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvDPEC_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvDPEC_TXT) >= 0)
                    {
                        tipoServico = Servicos.EnviarDPEC;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.ConsDPEC_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.ConsDPEC_TXT) >= 0)
                    {
                        tipoServico = Servicos.ConsultarDPEC;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.AltCon_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.AltCon_TXT) >= 0)
                    {
                        tipoServico = Servicos.AlterarConfiguracoesUniNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.ConsInf_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.ConsInf_TXT) >= 0)
                    {
                        tipoServico = Servicos.ConsultaInformacoesUniNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvCCe_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvCCe_TXT) >= 0)
                    {
                        tipoServico = Servicos.EnviarCCe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvCancelamento_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvCancelamento_TXT) >= 0)
                    {
                        tipoServico = Servicos.EnviarEventoCancelamento;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvManifestacao_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvManifestacao_TXT) >= 0)
                    {
                        tipoServico = Servicos.EnviarManifestacao;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.ConsNFeDest_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.ConsNFeDest_TXT) >= 0)
                    {
                        tipoServico = Servicos.ConsultaNFDest;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvDownload_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvDownload_TXT) >= 0)
                    {
                        tipoServico = Servicos.DownloadNFe;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvRegistroDeSaida_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvRegistroDeSaida_TXT) >= 0)
                    {
                        tipoServico = Servicos.RegistroDeSaida;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvCancRegistroDeSaida_XML) >= 0 || arq.IndexOf(Propriedade.ExtEnvio.EnvCancRegistroDeSaida_TXT) >= 0)
                    {
                        tipoServico = Servicos.RegistroDeSaidaCancelamento;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.MontarLote) >= 0)
                    {
                        if (arq.IndexOf(Empresa.Configuracoes[empresa].PastaEnvioEmLote.ToLower().Trim()) >= 0)
                        {
                            tipoServico = Servicos.MontarLoteVariasNFe;
                        }
                    }
        #region NFS-e
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedLoteRps) >= 0)
                    {
                        tipoServico = Servicos.ConsultarLoteRps;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedCanNfse) >= 0)
                    {
                        tipoServico = Servicos.CancelarNfse;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedSitLoteRps) >= 0)
                    {
                        tipoServico = Servicos.ConsultarSituacaoLoteRps;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.EnvLoteRps) >= 0)
                    {
                        tipoServico = Servicos.RecepcionarLoteRps;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedSitNfse) >= 0)
                    {
                        tipoServico = Servicos.ConsultarNfse;
                    }
                    else if (arq.IndexOf(Propriedade.ExtEnvio.PedSitNfseRps) >= 0)
                    {
                        tipoServico = Servicos.ConsultarNfsePorRps;
                    }
        #endregion
                }
            }
            catch
            {
            }

            return tipoServico;
        }
#endif
        #endregion

        #region CarregaEmpresa()
        /// <summary>
        /// Carrega as Empresas que foram cadastradas e estão gravadas no XML
        /// </summary>
        /// <returns>Retorna uma ArrayList das empresas cadastradas</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 28/07/2010
        /// </remarks>
        public static ArrayList CarregaEmpresa()
        {
            ArrayList empresa = new ArrayList();

            string arqXML = Propriedade.NomeArqEmpresa;

            if (File.Exists(arqXML))
            {
                XmlTextReader oLerXml = null;
                try
                {
                    //Carregar os dados do arquivo XML de configurações da Aplicação
                    oLerXml = new XmlTextReader(arqXML);
                    int codEmp = 0;

                    while (oLerXml.Read())
                    {
                        if (oLerXml.NodeType == XmlNodeType.Element)
                        {
                            if (oLerXml.Name.Equals("Registro"))
                            {
                                string cnpj = oLerXml.GetAttribute("CNPJ");

                                while (oLerXml.Read())
                                {
                                    if (oLerXml.NodeType == XmlNodeType.Element && oLerXml.Name.Equals("Nome"))
                                    {
                                        oLerXml.Read();
                                        string nome = oLerXml.Value;
                                        empresa.Add(new ComboElem(cnpj, codEmp, nome));
                                        codEmp++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    if (oLerXml != null)
                        oLerXml.Close();
                }
            }

            empresa.Sort(new OrdenacaoPorNome());

            return empresa;
        }
        #endregion
    }
}
