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

        public Layouts.Betha.ConsultarLoteRpsResposta EnviarLoteRps(Core.Empresa empresa, Core.ArquivosEnvio localArquivos)
        {
            var serializar = new Layouts.Serializador();
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(empresa, localArquivos.SalvarEnvioLoteEm, localArquivos.SalvarRetornoEnvioLoteEm, Servicos.RecepcionarLoteRps);

            bool erro = false;
            var respostaEnvioLote = serializar.TryLerXml<Layouts.Betha.EnviarLoteRpsResposta>(localArquivos.SalvarRetornoEnvioLoteEm, out erro);
            var respostaSituacao = ConsultarSituacaoLote(empresa, respostaEnvioLote, localArquivos);
            if (respostaSituacao.Items[0] is ListaMensagemRetorno)
                return new ConsultarLoteRpsResposta() { ListaMensagemRetorno = (ListaMensagemRetorno)respostaSituacao.Items[0] }; 

            return ConsultarLote(empresa, respostaEnvioLote, localArquivos);
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

    }
}
