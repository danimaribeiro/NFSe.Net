using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class Proxy
    {
        #region DefinirProxy()
        /// <summary>
        /// Efetua as definições do proxy
        /// </summary>
        /// <returns>Retorna as definições do Proxy</returns>
        /// <param name="servidor">Endereço do servidor de proxy</param>
        /// <param name="usuario">Usuário para autenticação no servidor de proxy</param>
        /// <param name="senha">Senha do usuário para autenticação no servidor de proxy</param>
        /// <param name="porta">Porta de comunicação do servidor proxy</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 29/09/2009
        /// </remarks>
        public static System.Net.IWebProxy DefinirProxy(string servidor, string usuario, string senha, int porta)
        {
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(usuario, senha);
            System.Net.IWebProxy proxy;
            proxy = new System.Net.WebProxy(servidor, porta);

            if (!String.IsNullOrEmpty(usuario.Trim()) && usuario.Trim().Length > 0)
            {
                proxy.Credentials = credencial;
            }

            return proxy;
        }
        #endregion
    }
}
