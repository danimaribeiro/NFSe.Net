using NFSE.Net.Layouts.Betha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Envio
{
    public class EnvioCompleto
    {
        public void EnviarLoteRps(Core.Empresa empresa, Layouts.Betha.EnviarLoteRpsEnvio lote)
        {
            try
            {
                empresa.CriarPastas();
                string caminhoXml = System.IO.Path.Combine(empresa.PastaEnvioRps, lote.LoteRps.NumeroLote + Propriedade.ExtEnvio.EnvLoteRps);

                var serializar = new Layouts.Serializador();
                serializar.SalvarXml<Layouts.Betha.EnviarLoteRpsEnvio>(lote, caminhoXml);

                var envio = new NFSE.Net.Envio.Processar();
                envio.ProcessaArquivo(empresa, caminhoXml, Servicos.RecepcionarLoteRps);

                caminhoXml = System.IO.Path.Combine(empresa.PastaRetornoNFse, lote.LoteRps.NumeroLote + Propriedade.ExtRetorno.RetLoteRps);
                bool erro = false;
                var respostaEnvioLote = serializar.TryLerXml<Layouts.Betha.EnviarLoteRpsResposta>(caminhoXml, out erro);
                var respostaSituacao = ConsultarSituacaoLote(empresa, respostaEnvioLote);
                var respostaConsultaLote = ConsultarLote(empresa, respostaEnvioLote);
                if (respostaConsultaLote.ListaNfse.CompNfse != null)
                {
                    //TODO Deu certo
                }
                else if (respostaConsultaLote.ListaMensagemRetorno.MensagemRetorno != null)
                {
                    //TODO Erros;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Layouts.Betha.ConsultarSituacaoLoteRpsResposta ConsultarSituacaoLote(Core.Empresa empresa, EnviarLoteRpsResposta protocolo)
        {
            string caminhoXml = System.IO.Path.Combine(empresa.PastaXmlConsultas, protocolo.Items[2].ToString() + Propriedade.ExtEnvio.PedSitLoteRps);
            var consultaSituacaoLote = new Layouts.Betha.ConsultarSituacaoLoteRpsEnvio();
            consultaSituacaoLote.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            consultaSituacaoLote.Prestador.Cnpj = empresa.CNPJ;
            consultaSituacaoLote.Prestador.InscricaoMunicipal = empresa.InscricaoMunicipal;
            consultaSituacaoLote.Protocolo = protocolo.Items[2].ToString();

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.ConsultarSituacaoLoteRpsEnvio>(consultaSituacaoLote, caminhoXml);

            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(empresa, caminhoXml, Servicos.ConsultarSituacaoLoteRps);

            caminhoXml = System.IO.Path.Combine(empresa.PastaRetornoNFse, protocolo.Items[2].ToString() + Propriedade.ExtRetorno.SitLoteRps);
            bool erro = false;
            var resposta = serializar.TryLerXml<Layouts.Betha.ConsultarSituacaoLoteRpsResposta>(caminhoXml, out erro);
            return resposta;
        }

        private Layouts.Betha.ConsultarLoteRpsResposta ConsultarLote(Core.Empresa empresa, EnviarLoteRpsResposta protocolo)
        {
            string caminhoXml = System.IO.Path.Combine(empresa.PastaXmlConsultas, protocolo.Items[2].ToString() + Propriedade.ExtEnvio.PedLoteRps);
            var consultaSituacaoLote = new Layouts.Betha.ConsultarLoteRpsEnvio();
            consultaSituacaoLote.Prestador = new tcIdentificacaoPrestador();
            consultaSituacaoLote.Prestador.Cnpj = empresa.CNPJ;
            consultaSituacaoLote.Prestador.InscricaoMunicipal = empresa.InscricaoMunicipal;
            consultaSituacaoLote.Protocolo = protocolo.Items[2].ToString();

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.ConsultarLoteRpsEnvio>(consultaSituacaoLote, caminhoXml);

            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(empresa, caminhoXml, Servicos.ConsultarLoteRps);

            caminhoXml = System.IO.Path.Combine(empresa.PastaRetornoNFse, protocolo.Items[2].ToString() + Propriedade.ExtRetorno.LoteRps);
            bool erro = false;
            var resposta = serializar.TryLerXml<Layouts.Betha.ConsultarLoteRpsResposta>(caminhoXml, out erro);
            return resposta;

        }

    }
}
