using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class MsgErro
    {
        public static string ErroPreDefinido(ErroPadrao Erro)
        {
            return MsgErro.ErroPreDefinido(Erro, string.Empty);
        }

        public static string ErroPreDefinido(ErroPadrao Erro, string ComplementoMensagem)
        {
            string Mensagem = string.Empty;

            switch (Erro)
            {
                case ErroPadrao.ErroNaoDetectado:
                    goto default;

                case ErroPadrao.FalhaInternet:
                    Mensagem = "Sem conexão com a internet.";
                    break;

                case ErroPadrao.FalhaEnvioXmlNFeWS:
                    Mensagem = "Não foi possível recuperar o número do recibo retornado pelo sefaz, pois ocorreu uma falha no exato momento que o XML foi enviado. " +
                        "Esta falha pode ter sido ocasionada por falha na internet ou erro no servidor do SEFAZ. " +
                        "Não tendo o número do recibo, a única forma de finalizar a nota fiscal é através da consulta situação da NF-e (-ped-sit.xml).";
                    break;

                case ErroPadrao.FalhaEnvioXmlWS:
                    Mensagem = "Não foi possível obter o retorno do sefaz, pois ocorreu uma falha no exato momento que o XML foi enviado. " +
                        "Esta falha pode ter sido ocasionada por falha na internet ou erro no servidor do SEFAZ. ";
                    break;

                //danasa 21/10/2010
                case ErroPadrao.FalhaEnvioXmlWSDPEC:
                    Mensagem = "Não foi possível processar o DPEC, pois ocorreu uma falha no exato momento que o XML foi enviado. " +
                        "Esta falha pode ter sido ocasionada por falha na internet ou erro no servidor do SEFAZ. " +
                        "A única forma de finalizar é através da consulta situação do DPEC (-consDPEC.xml).";
                    break;

                case ErroPadrao.CertificadoVencido:
                    Mensagem = "Validade do certificado digital está vencida.";
                    break;

                default:
                    Mensagem = "Não foi possível identificar o erro.";
                    break;
            }

            if (ComplementoMensagem != string.Empty)
            {
                Mensagem = Mensagem + " " + ComplementoMensagem;
            }

            return Mensagem;
        }
    }
}
