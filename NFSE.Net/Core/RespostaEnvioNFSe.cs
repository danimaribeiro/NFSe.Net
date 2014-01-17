using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSE.Net.Core
{
    public class RespostaEnvioNFSe : List<ItemResposta>
    {
        
    }

    public class ItemResposta
    {
        #region "Identificacao RPS"
        
        public string LoteEnvio { get; set; }

        public string NumeroRps { get; set; }

        public string Serie { get; set; }

        public string Identificacao { get; set; }

        public bool Sucesso { get; set; }

        #endregion

        #region "Identificacao NFS-e"

        public string IdentificacaoRetorno { get; set; }

        public string UrlConsulta { get; set; }

        #endregion

        #region "Identifiacação de erros"

        public string CodigoErro { get; set; }

        public string MensagemErro { get; set; }

        public string Correcao { get; set; }

        #endregion
    }
}
