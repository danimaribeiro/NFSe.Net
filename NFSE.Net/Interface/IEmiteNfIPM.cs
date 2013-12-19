using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    /// <summary>
    /// Interface para assinaturas de classe que emitem a NFe
    /// </summary>
    public interface IEmiteNfIPM
    {
        /// <summary>
        /// Emite uma nota fiscal.
        /// </summary>
        /// <param name="file">Arquivo a ser enviado ao servidor NF-e</param>
        /// <param name="tpAmb">Tipo de ambiente</param>
        /// <param name="cancelamento">Define se está se tratando de um Cancelamento pois a URL utilizada é a mesma do envio - Renan</param>
        /// <returns></returns>
        string EmitirNF(string file, TpAmb tpAmb, bool cancelamento);
        /// <summary>
        /// Usuário para emissão da NF
        /// </summary>
        string Usuario { get; set; }

        /// <summary>
        /// Senha do usuário para emissão da NF
        /// </summary>
        string Senha { get; set; }
    }
}
