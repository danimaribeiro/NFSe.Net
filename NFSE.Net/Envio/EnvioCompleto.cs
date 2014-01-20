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
        public void SalvarLoteRps(Layouts.Betha.EnviarLoteRpsEnvio lote, Core.ArquivosEnvio localArquivos)
        {
            if (string.IsNullOrWhiteSpace(localArquivos.SalvarEnvioLoteEm))
                throw new ArgumentNullException("localArquivos.SalvarEnvioLoteEm");
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(localArquivos.SalvarEnvioLoteEm)))
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(localArquivos.SalvarEnvioLoteEm));

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.EnviarLoteRpsEnvio>(lote, localArquivos.SalvarEnvioLoteEm);
        }

        public Core.RespostaEnvioNFSe EnviarLoteRps(Core.Empresa empresa, Core.ArquivosEnvio localArquivos)
        {
            var serializar = new Layouts.Serializador();
            var envio = new NFSE.Net.Envio.Processar();
            var lote = serializar.LerXml<Layouts.Betha.EnviarLoteRpsEnvio>(localArquivos.SalvarEnvioLoteEm);
            envio.ProcessaArquivo(empresa, localArquivos.SalvarEnvioLoteEm, localArquivos.SalvarRetornoEnvioLoteEm, Servicos.RecepcionarLoteRps);

            bool erro = false;
            var respostaEnvioLote = serializar.TryLerXml<Layouts.Betha.EnviarLoteRpsResposta>(localArquivos.SalvarRetornoEnvioLoteEm, out erro);
            while (true)
            {
                System.Threading.Thread.Sleep(500);
                var respostaSituacao = ConsultarSituacaoLote(empresa, respostaEnvioLote, localArquivos);
                if (respostaSituacao.Items[0] is ListaMensagemRetorno)
                {
                    if (((ListaMensagemRetorno)respostaSituacao.Items[0]).MensagemRetorno[0].Codigo == "E92")  //Lote ainda em processamento, tentando denovo.
                        continue;
                    return MontarResposta(lote, (ListaMensagemRetorno)respostaSituacao.Items[0], null);
                }
                else
                    break;
            }
            var respostaLote = ConsultarLote(empresa, respostaEnvioLote, localArquivos);
            return MontarResposta(lote, null, respostaLote.ListaNfse);
        }

        private Layouts.Betha.ConsultarSituacaoLoteRpsResposta ConsultarSituacaoLote(Core.Empresa empresa, EnviarLoteRpsResposta protocolo, Core.ArquivosEnvio localArquivos)
        {
            var consultaSituacaoLote = new Layouts.Betha.ConsultarSituacaoLoteRpsEnvio();
            consultaSituacaoLote.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            consultaSituacaoLote.Prestador.Cnpj = empresa.CNPJ;
            consultaSituacaoLote.Prestador.InscricaoMunicipal = empresa.InscricaoMunicipal;
            consultaSituacaoLote.Protocolo = protocolo.Items[2].ToString();

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.ConsultarSituacaoLoteRpsEnvio>(consultaSituacaoLote, localArquivos.SalvarConsultaSituacaoLoteEm);

            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(empresa, localArquivos.SalvarConsultaSituacaoLoteEm, localArquivos.SalvarRetornoConsultaSituacaoLoteEm, Servicos.ConsultarSituacaoLoteRps);

            bool erro = false;
            var resposta = serializar.TryLerXml<Layouts.Betha.ConsultarSituacaoLoteRpsResposta>(localArquivos.SalvarRetornoConsultaSituacaoLoteEm, out erro);
            return resposta;
        }

        private Layouts.Betha.ConsultarLoteRpsResposta ConsultarLote(Core.Empresa empresa, EnviarLoteRpsResposta protocolo, Core.ArquivosEnvio localArquivos)
        {
            var consultaSituacaoLote = new Layouts.Betha.ConsultarLoteRpsEnvio();
            consultaSituacaoLote.Prestador = new tcIdentificacaoPrestador();
            consultaSituacaoLote.Prestador.Cnpj = empresa.CNPJ;
            consultaSituacaoLote.Prestador.InscricaoMunicipal = empresa.InscricaoMunicipal;
            consultaSituacaoLote.Protocolo = protocolo.Items[2].ToString();

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.ConsultarLoteRpsEnvio>(consultaSituacaoLote, localArquivos.SalvarConsultaLoteRpsEnvioEm);

            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(empresa, localArquivos.SalvarConsultaLoteRpsEnvioEm, localArquivos.SalvarConsultaLoteRpsRespostaEm, Servicos.ConsultarLoteRps);

            bool erro = false;
            var resposta = serializar.TryLerXml<Layouts.Betha.ConsultarLoteRpsResposta>(localArquivos.SalvarConsultaLoteRpsRespostaEm, out erro);
            return resposta;

        }


        private Core.RespostaEnvioNFSe MontarResposta(Layouts.Betha.EnviarLoteRpsEnvio lote, ListaMensagemRetorno listaRetorno, ConsultarLoteRpsRespostaListaNfse respostaConsulta)
        {
            var resposta = new Core.RespostaEnvioNFSe();
            int indice = 0;
            foreach (var item in lote.LoteRps.ListaRps)
            {
                var resp = new Core.ItemResposta();
                resp.LoteEnvio = lote.LoteRps.NumeroLote;
                resp.NumeroRps = item.InfRps.IdentificacaoRps.Numero;
                resp.Serie = item.InfRps.IdentificacaoRps.Serie;
                resp.Identificacao = item.InfRps.Id;

                if (listaRetorno != null)
                {
                    resp.Sucesso = false;
                    if (indice > 0 && listaRetorno.MensagemRetorno.Length > 1)
                    {
                        resp.CodigoErro = listaRetorno.MensagemRetorno[indice].Codigo;
                        resp.MensagemErro = listaRetorno.MensagemRetorno[indice].Mensagem;
                        resp.Correcao = listaRetorno.MensagemRetorno[indice].Correcao;
                    }
                    else
                    {
                        resp.CodigoErro = listaRetorno.MensagemRetorno[0].Codigo;
                        resp.MensagemErro = listaRetorno.MensagemRetorno[0].Mensagem;
                        resp.Correcao = listaRetorno.MensagemRetorno[0].Correcao;
                    }
                }
                else if (respostaConsulta != null)
                {
                    resp.Sucesso = true;
                    resp.IdentificacaoRetorno = respostaConsulta.CompNfse[indice].Nfse.InfNfse.CodigoVerificacao;
                    resp.UrlConsulta = "https://e-gov.betha.com.br/";
                }
                resposta.Add(resp);
                indice++;
            }
            return resposta;
        }
    }
}
