using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSE.Net.Core
{
    public class RespostaCancelamentoNfse
    {
        public string NumeroNfse { get; set; }

        public bool Sucesso { get; set; }

        public DateTime DataHoraCancelamento { get; set; }


        #region "Identificação de erros"

        public string CodigoErro { get; set; }

        public string MensagemErro { get; set; }

        public string Correcao { get; set; }

        #endregion
    }
}
