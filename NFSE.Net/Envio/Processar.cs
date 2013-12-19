using NFSE.Net.Certificado;
using NFSE.Net.Core;
using NFSE.Net.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net.Envio
{
    public class Processar
    {
        #region Métodos gerais

        #region ProcessaArquivo()
        public void ProcessaArquivo(int emp, string arquivo)//, Servicos servico)
        {
            try
            {
                Servicos servico = DefinirTipoServico(emp, arquivo);
                try
                {
                    if (servico == Servicos.Nulo)
                        throw new Exception("Não pode identificar o tipo de serviço baseado no arquivo " + arquivo);

                    if (Propriedade.TipoAplicativo == TipoAplicativo.Nfse)
                    {
                        #region Executar o serviço da NFS-e
                        switch (servico)
                        {
                            case Servicos.ConsultarLoteRps:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskConsultarLoteRps());
                                break;

                            case Servicos.CancelarNfse:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskCancelarNfse());
                                break;

                            case Servicos.ConsultarSituacaoLoteRps:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskConsultaSituacaoLoteRps());
                                break;

                            case Servicos.ConsultarNfse:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskConsultarNfse());
                                break;

                            case Servicos.ConsultarNfsePorRps:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskConsultarNfsePorRps());
                                break;

                            case Servicos.RecepcionarLoteRps:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskRecepcionarLoteRps());
                                break;

                            case Servicos.ConsultarURLNfse:
                                CertVencido(emp);
                                IsConnectedToInternet();
                                this.DirecionarArquivo(arquivo, new TaskConsultarURLNfse());
                                break;
                        }
                        #endregion
                    }

                }
                catch (ExceptionSemInternet ex)
                {
                    GravaErroERP(arquivo, servico, ex, ex.ErrorCode);
                }
                catch (ExceptionCertificadoDigital ex)
                {
                    GravaErroERP(arquivo, servico, ex, ex.ErrorCode);
                }
                catch (Exception ex)
                {
                    GravaErroERP(arquivo, servico, ex, ErroPadrao.ErroNaoDetectado);
                }
            }
            catch { }
        }
        #endregion

        private Servicos DefinirTipoServico(int empresa, string fullPath)
        {
            Servicos tipoServico = Servicos.Nulo;

            try
            {
                string arq = fullPath.ToLower().Trim();

                FileInfo infArq = new FileInfo(arq);
                string pastaArq = ConfiguracaoApp.RemoveEndSlash(infArq.DirectoryName).ToLower().Trim();
                string pastaLote = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[empresa].PastaEnvioEmLote).ToLower().Trim();
                string pastaEnvio = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[empresa].PastaEnvio).ToLower().Trim();
                if (pastaArq.EndsWith("\\temp"))
                    pastaArq = Path.GetDirectoryName(pastaArq);

                XmlDocument doc = new XmlDocument();
                doc.Load(fullPath);

                switch (doc.DocumentElement.Name)
                {
                    default:
                        #region NFS-e
                        if (arq.IndexOf(Propriedade.ExtEnvio.PedLoteRps) >= 0)
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
                        else if (arq.IndexOf(Propriedade.ExtEnvio.PedURLNfse) >= 0)
                        {
                            tipoServico = Servicos.ConsultarURLNfse;
                        }
                        #endregion

                        break;
                }
            }
            catch
            {
            }

            return tipoServico;
        }

        #endregion

        #region DirecionarArquivo()
        /// <summary>
        /// Direcionar os arquivos encontrados na pasta de envico corretamente
        /// </summary>
        /// <param name="arquivos">Lista de arquivos</param>
        /// <param name="metodo">Método a ser executado do serviço da NFe</param>
        /// <param name="nfe">Objeto do serviço da NFe a ser executado</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 18/04/2011
        /// </remarks>
        private void DirecionarArquivo(List<string> arquivos, object nfe, string metodo)
        {
            for (int i = 0; i < arquivos.Count; i++)
            {
                DirecionarArquivo(arquivos[i], nfe, metodo);
            }
        }
        #endregion

        #region DirecionarArquivo()
        /// <summary>
        /// Direcionar o arquivo
        /// </summary>
        /// <param name="arquivos">Arquivo</param>
        /// <param name="metodo">Método a ser executado do serviço da NFe</param>
        /// <param name="nfe">Objeto do serviço da NFe a ser executado</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 18/04/2011
        /// </remarks>
        private void DirecionarArquivo(string arquivo, object taskClass)
        {
            //Processa ou envia o XML
            EnviarArquivo(arquivo, taskClass, "Execute");
        }

        private void DirecionarArquivo(string arquivo, object nfe, string metodo)
        {
            //Processa ou envia o XML
            EnviarArquivo(arquivo, nfe, metodo);
        }
        #endregion

        #region EnviarArquivo()
        /// <summary>
        /// Analisa o tipo do XML que está na pasta de envio e executa a operação necessária. Exemplo: Envia ao SEFAZ, reconfigura o UniNFE, etc... 
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo XML a ser enviado ou analisado</param>
        /// <param name="oNfe">Objeto da classe UniNfeClass a ser utilizado nas operações</param>
        private void EnviarArquivo(string arquivo, Object nfe, string metodo)
        {
            int emp = Functions.FindEmpresaByThread();

            //Definir o tipo do serviço
            Type tipoServico = nfe.GetType();

            //Definir o arquivo XML para a classe UniNfeClass
            tipoServico.InvokeMember("NomeArquivoXML", System.Reflection.BindingFlags.SetProperty, null, nfe, new object[] { arquivo });

            if (Empresa.Configuracoes[emp].tpEmis != Propriedade.TipoEmissao.teContingencia &&
                Empresa.Configuracoes[emp].tpEmis != Propriedade.TipoEmissao.teFSDA &&
                Empresa.Configuracoes[emp].tpEmis != Propriedade.TipoEmissao.teDPEC) //Confingência em formulário de segurança e DPEC não envia na hora, tem que aguardar voltar para normal.
            {
                tipoServico.InvokeMember(metodo, System.Reflection.BindingFlags.InvokeMethod, null, nfe, null);
            }
            else
            {
                throw new Exception("Não sei o que fazer aqui");
                //if (nfe is TaskRetRecepcao ||
                //    nfe is TaskConsultaStatus ||
                //    nfe is TaskRecepcaoDPEC ||
                //    nfe is TaskConsultaSituacaoNFe ||
                //    nfe is TaskConsultaDPEC ||
                //    nfe is TaskCadastroContribuinte)
                //{
                //    tipoServico.InvokeMember(metodo, System.Reflection.BindingFlags.InvokeMethod, null, nfe, null);
                //}
            }
        }
        #endregion



        #region CertVencido
        /// <summary>
        /// Verificar se o certificado digital está vencido
        /// </summary>
        /// <param name="emp">Empresa que é para ser verificado o certificado digital</param>
        /// <remarks>
        /// Retorna uma exceção ExceptionCertificadoDigital caso o certificado esteja vencido
        /// </remarks>
        protected void CertVencido(int emp)
        {
            CertificadoDigital CertDig = new CertificadoDigital();
            if (CertDig.Vencido(emp))
            {
                throw new ExceptionCertificadoDigital(ErroPadrao.CertificadoVencido, "(" + CertDig.dValidadeInicial.ToString() + " a " + CertDig.dValidadeFinal.ToString() + ")");
            }
        }
        #endregion

        #region IsConnectedToInternet()
        /// <summary>
        /// Verifica se a conexão com a internet está OK
        /// </summary>
        /// <remarks>
        /// Retorna uma exceção ExceptionSemInternet caso a internet não esteja OK
        /// </remarks>
        protected void IsConnectedToInternet()
        {
            //Verificar antes se tem conexão com a internet, se não tiver já gera uma exceção no padrão já esperado pelo ERP
            if (ConfiguracaoApp.ChecarConexaoInternet)
                if (!Functions.IsConnectedToInternet())
                {
                    throw new ExceptionSemInternet(ErroPadrao.FalhaInternet);
                }
        }
        #endregion

        #region GravaErroERP()
        /// <summary>
        /// Gravar o erro ocorrido para o ERP
        /// </summary>
        /// <param name="arquivo">Nome do arquivo que seria processado</param>
        /// <param name="extRet">Extensão do arquivo de erro a ser gravado</param>
        /// <param name="servico">Serviço que está sendo executado</param>
        /// <param name="ex">Exception gerada</param>
        protected void GravaErroERP(string arquivo, Servicos servico, Exception ex, ErroPadrao erroPadrao)
        {
            string extRetERR = string.Empty;
            string extRet = string.Empty;

            switch (servico)
            {
                case Servicos.RecepcionarLoteRps:
                    extRet = Propriedade.ExtEnvio.EnvLoteRps;
                    extRetERR = Propriedade.ExtRetorno.RetLoteRps_ERR;
                    goto default;

                case Servicos.ConsultarSituacaoLoteRps:
                    extRet = Propriedade.ExtEnvio.PedSitLoteRps;
                    extRetERR = Propriedade.ExtRetorno.SitLoteRps_ERR;
                    goto default;

                case Servicos.ConsultarNfsePorRps:
                    extRet = Propriedade.ExtEnvio.PedSitNfseRps;
                    extRetERR = Propriedade.ExtRetorno.SitNfseRps_ERR;
                    goto default;

                case Servicos.ConsultarNfse:
                    extRet = Propriedade.ExtEnvio.PedSitNfse;
                    extRetERR = Propriedade.ExtRetorno.SitNfse_ERR;
                    goto default;

                case Servicos.ConsultarLoteRps:
                    extRet = Propriedade.ExtEnvio.PedLoteRps;
                    extRetERR = Propriedade.ExtRetorno.LoteRps_ERR;
                    goto default;

                case Servicos.CancelarNfse:
                    extRet = Propriedade.ExtEnvio.PedCanNfse;
                    extRetERR = Propriedade.ExtRetorno.CanNfse_ERR;
                    goto default;

                default:
                    try
                    {
                        //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                        TFunctions.GravarArqErroServico(arquivo, extRet, extRetERR, ex, erroPadrao, true);

                        //new Task().GravarArqErroServico(arquivo, extRet, extRetERR, ex, erroPadrao, true);
                    }
                    catch
                    {
                        //Se falhou algo na hora de gravar o retorno .ERR (de erro) para o ERP, infelizmente não posso fazer mais nada.
                        //Wandrey 02/06/2011
                    }
                    break;
            }
        }
        #endregion

        #region ConsultaGeral()
        protected void ConsultarGeral(string arquivo)
        {
            string arq = arquivo.ToLower().Trim();

            if (arq.EndsWith(Propriedade.ExtEnvio.ConsCertificado))
            {
                ConsultaCertificados(arquivo);
            }


        }

        protected void ConsultaCertificados(string arquivo)
        {
            ConfiguracaoApp oConfig = new ConfiguracaoApp();
            oConfig.CertificadosInstalados(arquivo);
        }

        #endregion
    }
}
