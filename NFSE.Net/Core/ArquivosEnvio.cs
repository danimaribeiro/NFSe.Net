using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSE.Net.Core
{
    public class ArquivosEnvio
    {
        public string SalvarEnvioLoteEm { get; set; }
        public string SalvarRetornoEnvioLoteEm { get; set; }

        public string SalvarConsultaSituacaoLoteEm { get; set; }
        public string SalvarRetornoConsultaSituacaoLoteEm { get; set; }

        public string SalvarConsultaLoteRpsEnvioEm { get; set; }
        public string SalvarConsultaLoteRpsRespostaEm { get; set; }

        public string SalvarCancelarNfseEnvioEm { get; set; }
        public string SalvarCancelarNfseRespostaEm { get; set; }

        public static ArquivosEnvio GerarCaminhos(string lote, string pastaBase)
        {
            return new ArquivosEnvio()
            {
                SalvarEnvioLoteEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtEnvio.EnvLoteRps),
                SalvarRetornoEnvioLoteEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtRetorno.RetLoteRps),

                SalvarConsultaSituacaoLoteEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtEnvio.PedSitLoteRps),
                SalvarRetornoConsultaSituacaoLoteEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtRetorno.SitLoteRps),

                SalvarConsultaLoteRpsEnvioEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtEnvio.PedLoteRps),
                SalvarConsultaLoteRpsRespostaEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtRetorno.LoteRps),

                SalvarCancelarNfseEnvioEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtEnvio.PedCanNfse),
                SalvarCancelarNfseRespostaEm = System.IO.Path.Combine(pastaBase, lote + Propriedade.ExtRetorno.CanNfse)
            };
        }
    }
}
