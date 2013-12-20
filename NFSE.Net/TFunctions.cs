using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class TFunctions
    {
        #region GravarArqErroServico()
        /// <summary>
        /// Grava um arquivo texto com um erros ocorridos durante as operações para que o ERP possa tratá-los        
        /// </summary>
        /// <param name="arquivo">Nome do arquivo que está sendo processado</param>
        /// <param name="finalArqEnvio">string final do nome do arquivo que é para ser substituida na gravação do arquivo de Erro</param>
        /// <param name="finalArqErro">string final do nome do arquivo que é para ser utilizado no nome do arquivo de erro</param>
        /// <param name="exception">Exception gerada</param>
        /// <param name="errorCode">Código do erro</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 02/06/2011
        /// </remarks>
        public static void GravarArqErroServico(string arquivo, string finalArqEnvio, string finalArqErro, Exception exception)
        {
            GravarArqErroServico(arquivo, finalArqEnvio, finalArqErro, exception, ErroPadrao.ErroNaoDetectado, true);
        }
        #endregion

        #region GravarArqErroServico()
        /// <summary>
        /// Grava um arquivo texto com um erros ocorridos durante as operações para que o ERP possa tratá-los        
        /// </summary>
        /// <param name="arquivo">Nome do arquivo que está sendo processado</param>
        /// <param name="finalArqEnvio">string final do nome do arquivo que é para ser substituida na gravação do arquivo de Erro</param>
        /// <param name="finalArqErro">string final do nome do arquivo que é para ser utilizado no nome do arquivo de erro</param>
        /// <param name="exception">Exception gerada</param>
        /// <param name="errorCode">Código do erro</param>
        /// <param name="moveArqErro">Move o arquivo informado no parametro "arquivo" para a pasta de XML com ERRO</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 02/06/2011
        /// </remarks>
        public static void GravarArqErroServico(string arquivo, string finalArqEnvio, string finalArqErro, Exception exception, bool moveArqErro)
        {
            GravarArqErroServico(arquivo, finalArqEnvio, finalArqErro, exception, ErroPadrao.ErroNaoDetectado, moveArqErro);
        }
        #endregion

        #region GravarArqErroServico()
        /// <summary>
        /// Grava um arquivo texto com um erros ocorridos durante as operações para que o ERP possa tratá-los        
        /// </summary>
        /// <param name="arquivo">Nome do arquivo que está sendo processado</param>
        /// <param name="finalArqEnvio">string final do nome do arquivo que é para ser substituida na gravação do arquivo de Erro</param>
        /// <param name="finalArqErro">string final do nome do arquivo que é para ser utilizado no nome do arquivo de erro</param>
        /// <param name="exception">Exception gerada</param>
        /// <param name="errorCode">Código do erro</param>
        /// <param name="moveArqErro">Move o arquivo informado no parametro "arquivo" para a pasta de XML com ERRO</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 02/06/2011
        /// </remarks>
        public static void GravarArqErroServico(string arquivo, string finalArqEnvio, string finalArqErro, Exception exception, ErroPadrao erroPadrao, bool moveArqErro)
        {
            int emp = Functions.FindEmpresaByThread();

            //Qualquer erro ocorrido o aplicativo vai mover o XML com falha da pasta de envio
            //para a pasta de XML´s com erros. Futuramente ele é excluido quando outro igual
            //for gerado corretamente.
            if (moveArqErro)
                MoveArqErro(arquivo);

            //Grava arquivo de ERRO para o ERP
            string arqErro = Empresa.Configuracoes[emp].PastaRetorno + "\\" +
                              Functions.ExtrairNomeArq(arquivo, finalArqEnvio) +
                              finalArqErro;

            string erroMessage = exception.ToString();
           
            try
            {
                // Gerar log do erro
                Auxiliar.WriteLog(erroMessage, true);
                //TODO: (Marcelo) Este tratamento de erro não poderia ser feito diretamente no método?
            }
            catch
            {
            }

            File.WriteAllText(arqErro, erroMessage, Encoding.Default);
        }
        
        
        #endregion

        #region MoveArqErro
        /// <summary>
        /// Move arquivos XML com erro para uma pasta de xml´s com erro configurados no UniNFe.
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo a ser movido para a pasta de XML´s com erro</param>
        /// <example>this.MoveArqErro(this.vXmlNfeDadosMsg)</example>
        public static void MoveArqErro(string Arquivo)
        {
            MoveArqErro(Arquivo, Path.GetExtension(Arquivo));
        }
        #endregion

        #region MoveArqErro()
        /// <summary>
        /// Move arquivos com a extensão informada e que está com erro para uma pasta de xml´s/arquivos com erro configurados no UniNFe.
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo a ser movido para a pasta de XML´s com erro</param>
        /// <param name="ExtensaoArq">Extensão do arquivo que vai ser movido. Ex: .xml</param>
        /// <example>this.MoveArqErro(this.vXmlNfeDadosMsg, ".xml")</example>
        private static void MoveArqErro(string Arquivo, string ExtensaoArq)
        {
            int emp = Functions.FindEmpresaByThread();

            if (File.Exists(Arquivo))
            {
                FileInfo oArquivo = new FileInfo(Arquivo);

                if (Directory.Exists(Empresa.Configuracoes[emp].PastaErro))
                {
                    string vNomeArquivo = Empresa.Configuracoes[emp].PastaErro + "\\" + Functions.ExtrairNomeArq(Arquivo, ExtensaoArq) + ExtensaoArq;

                    Functions.Move(Arquivo, vNomeArquivo);

                    Auxiliar.WriteLog("O arquivo " + Arquivo + " foi movido para a pasta de XML com problemas.", true);

                    /*
                    //Deletar o arquivo da pasta de XML com erro se o mesmo existir lá para evitar erros na hora de mover. Wandrey
                    if (File.Exists(vNomeArquivo))
                        this.DeletarArquivo(vNomeArquivo);

                    //Mover o arquivo da nota fiscal para a pasta do XML com erro
                    oArquivo.MoveTo(vNomeArquivo);
                    */
                }
                else
                {
                    //Antes estava deletando o arquivo, agora vou retornar uma mensagem de erro
                    //pois não podemos excluir, pode ser coisa importante. Wandrey 25/02/2011
                    throw new Exception("A pasta de XML´s com erro informada nas configurações não existe, por favor verifique.");
                    //oArquivo.Delete();
                }
            }
        }
        #endregion

        #region MoverArquivo()
        /// <summary>
        /// Move arquivos da nota fiscal eletrônica para suas respectivas pastas
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser movido</param>
        /// <param name="PastaXMLEnviado">Pasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="SubPastaXMLEnviado">SubPasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="PastaBackup">Pasta para Backup dos XML´s enviados</param>
        /// <param name="Emissao">Data de emissão da Nota Fiscal ou Data Atual do envio do XML para separação dos XML´s em subpastas por Ano e Mês</param>
        /// <date>16/07/2008</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public static void MoverArquivo(string arquivo, PastaEnviados subPastaXMLEnviado, DateTime emissao)
        {
            int emp = Functions.FindEmpresaByThread();

            #region Criar pastas que receberão os arquivos
            //Criar subpastas da pasta dos XML´s enviados
            Empresa.CriarSubPastaEnviado(emp);

            //Criar Pasta do Mês para gravar arquivos enviados autorizados ou denegados
            string nomePastaEnviado = string.Empty;
            string destinoArquivo = string.Empty;
            switch (subPastaXMLEnviado)
            {
                case PastaEnviados.EmProcessamento:
                    nomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.EmProcessamento.ToString();
                    destinoArquivo = nomePastaEnviado + "\\" + Functions.ExtrairNomeArq(arquivo, ".xml") + ".xml";
                    break;

                case PastaEnviados.Autorizados:
                    nomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(emissao);
                    destinoArquivo = nomePastaEnviado + "\\" + Functions.ExtrairNomeArq(arquivo, ".xml") + ".xml";
                    goto default;

                case PastaEnviados.Denegados:
                    nomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Denegados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(emissao);
                    if (arquivo.ToLower().EndsWith("-den.xml"))//danasa 11-4-2012
                        destinoArquivo = Path.Combine(nomePastaEnviado, Path.GetFileName(arquivo));
                    else
                        destinoArquivo = Path.Combine(nomePastaEnviado, Functions.ExtrairNomeArq(arquivo, "-nfe.xml") + "-den.xml");
                    goto default;

                default:
                    if (!Directory.Exists(nomePastaEnviado))
                    {
                        System.IO.Directory.CreateDirectory(nomePastaEnviado);
                    }
                    break;
            }
            #endregion

            //Se conseguiu criar a pasta ele move o arquivo, caso contrário
            if (Directory.Exists(nomePastaEnviado))
            {
                #region Mover o XML para a pasta de XML´s enviados
                //Se for para mover para a Pasta EmProcessamento
                if (subPastaXMLEnviado == PastaEnviados.EmProcessamento)
                {
                    //Se já existir o arquivo na pasta EmProcessamento vamos mover 
                    //ele para a pasta com erro antes para evitar exceção. Wandrey 05/07/2011
                    if (File.Exists(destinoArquivo))
                    {
                        string destinoErro = Empresa.Configuracoes[emp].PastaErro + "\\" + Functions.ExtrairNomeArq(arquivo, ".xml") + ".xml";
                        File.Move(destinoArquivo, destinoErro);

                        //danasa 11-4-2012
                        Auxiliar.WriteLog("Arquivo \"" + destinoArquivo + "\" movido para a pasta \"" + Empresa.Configuracoes[emp].PastaErro + "\".", true);
                    }
                    File.Move(arquivo, destinoArquivo);
                }
                else
                {
                    //Se já existir o arquivo na pasta autorizados ou denegado, não vou mover o novo arquivo para lá, pois posso estar sobrepondo algum arquivo importante
                    //Sendo assim se o usuário quiser forçar mover, tem que deletar da pasta autorizados ou denegados manualmente, com isso evitamos perder um XML importante.
                    //Wandrey 05/07/2011
                    if (!File.Exists(destinoArquivo))
                    {
                        File.Move(arquivo, destinoArquivo);
                    }
                    else
                    {
                        string destinoErro = Empresa.Configuracoes[emp].PastaErro + "\\" + Functions.ExtrairNomeArq(arquivo, ".xml") + ".xml";
                        File.Move(arquivo, destinoErro);

                        //danasa 11-4-2012
                        Auxiliar.WriteLog("Arquivo \"" + arquivo + "\" movido para a pasta \"" + Empresa.Configuracoes[emp].PastaErro + "\".", true);
                    }
                }
                #endregion

                if (subPastaXMLEnviado == PastaEnviados.Autorizados || subPastaXMLEnviado == PastaEnviados.Denegados)
                {
                    #region Copiar XML para a pasta de BACKUP
                    //Fazer um backup do XML que foi copiado para a pasta de enviados
                    //para uma outra pasta para termos uma maior segurança no arquivamento
                    //Normalmente esta pasta é em um outro computador ou HD
                    if (Empresa.Configuracoes[emp].PastaBackup.Trim() != "")
                    {
                        //Criar Pasta do Mês para gravar arquivos enviados
                        string nomePastaBackup = string.Empty;
                        switch (subPastaXMLEnviado)
                        {
                            case PastaEnviados.Autorizados:
                                nomePastaBackup = Empresa.Configuracoes[emp].PastaBackup + "\\" + PastaEnviados.Autorizados + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(emissao);
                                goto default;

                            case PastaEnviados.Denegados:
                                nomePastaBackup = Empresa.Configuracoes[emp].PastaBackup + "\\" + PastaEnviados.Denegados + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(emissao);
                                goto default;

                            default:
                                if (!Directory.Exists(nomePastaBackup))
                                {
                                    System.IO.Directory.CreateDirectory(nomePastaBackup);
                                }
                                break;
                        }

                        //Se conseguiu criar a pasta ele move o arquivo, caso contrário
                        if (Directory.Exists(nomePastaBackup))
                        {
                            //Mover o arquivo da nota fiscal para a pasta de backup
                            string destinoBackup = nomePastaBackup + "\\" + Functions.ExtrairNomeArq(arquivo, ".xml") + ".xml";
                            if (File.Exists(destinoBackup))
                            {
                                File.Delete(destinoBackup);
                            }
                            File.Copy(destinoArquivo, destinoBackup);
                        }
                        else
                        {
                            throw new Exception("Pasta de backup informada nas configurações não existe. (Pasta: " + nomePastaBackup + ")");
                        }
                    }
                    #endregion               
                  
                }
            }
            else
            {
                throw new Exception("Pasta para arquivamento dos XML´s enviados não existe. (Pasta: " + nomePastaEnviado + ")");
            }
        }
        #endregion

        #region MoverArquivo()
        /// <summary>
        /// Move arquivos da nota fiscal eletrônica para suas respectivas pastas
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser movido</param>
        /// <param name="PastaXMLEnviado">Pasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="SubPastaXMLEnviado">SubPasta de XML´s enviados para onde será movido o arquivo</param>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public static void MoverArquivo(string Arquivo, PastaEnviados SubPastaXMLEnviado)
        {
            MoverArquivo(Arquivo, SubPastaXMLEnviado, DateTime.Now);
        }
        #endregion

        

        #region ExecutaUniDanfe()

        #region RetornarConteudoEntre()
        /// <summary>
        /// Executa o aplicativo UniDanfe para gerar/imprimir o DANFE
        /// </summary>
        /// <param name="NomeArqXMLNFe">Nome do arquivo XML da NFe (final -nfe.xml)</param>
        /// <param name="DataEmissaoNFe">Data de emissão da NFe</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 03/02/2010
        /// </remarks>
        private static string RetornarConteudoEntre(string Conteudo, string Inicio, string Fim)
        {
            int i;
            i = Conteudo.IndexOf(Inicio);
            if (i == -1)
                return "";

            string s = Conteudo.Substring(i + Inicio.Length);
            i = s.IndexOf(Fim);
            if (i == -1)
                return "";
            return s.Substring(0, i);
        }
        #endregion

        #region ExcluirArqAuxiliar()
        private static void ExcluirArqAuxiliar(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (e.Cancel)
                return;

            System.Threading.Thread.Sleep(1000);
            while (!(sender as System.ComponentModel.BackgroundWorker).CancellationPending)
            {
                if (File.Exists((string)e.Argument))
                {
                    if (!Functions.FileInUse((string)e.Argument))
                    {
                        File.Delete((string)e.Argument);
                        e.Cancel = true;
                        break;
                    }
                }
            }
        }
        #endregion


        #endregion
        
        #region RemoveSomenteLeitura()
        /// <summary>
        /// Metodo que remove atributo de Somente Leitura do Arquivo caso o mesmo estiver marcado, evitando problemas no acesso do arquivo.
        /// Renan - 26/11/13
        /// </summary>
        /// <param name="file">Arquivo a remover o atributo</param>
        public static void RemoveSomenteLeitura(string file)
        {
            FileAttributes attributes = File.GetAttributes(file);

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                // Show the file.
                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                File.SetAttributes(file, attributes);
            }
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
        #endregion
        
    }
}

