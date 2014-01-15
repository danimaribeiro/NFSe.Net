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

