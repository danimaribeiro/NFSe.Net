using NFSE.Net.Core;
using NFSE.Net.Exceptions;
using NFSE.Net.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NFSE.Net
{
    /// <summary>
    /// Classe para invocar os métodos e propriedades das classes dos webservices da NFE
    /// </summary>
    public class InvocarObjeto
    {
        #region Objetos
        private Auxiliar oAux = new Auxiliar();
        #endregion

        #region Métodos

        #region InvocarNFSe()
        /// <summary>
        /// Metodo responsável por invocar o serviço do WebService do SEFAZ
        /// </summary>
        /// <param name="oWSProxy">Objeto da classe construida do WSDL</param>
        /// <param name="oServicoWS">Objeto da classe de envio do XML</param>
        /// <param name="cMetodo">Método da classe de envio do XML que faz o envio</param>
        /// <param name="cabecMsg">Objeto da classe de cabecalho do serviço</param>
        /// <param name="oServicoNFe">Objeto do Serviço de envio da NFE do UniNFe</param>
        /// <param name="cFinalArqEnvio">string do final do arquivo a ser enviado. Sem a extensão ".xml"</param>
        /// <param name="cFinalArqRetorno">string do final do arquivo a ser gravado com o conteúdo do retorno. Sem a extensão ".xml"</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 17/03/2010
        /// </remarks>
        public void InvocarNFSe(WebServiceProxy oWSProxy,
                            object oServicoWS,
                            string cMetodo,
                            string cabecMsg,
                            object oServicoNFe,
                            string cFinalArqEnvio,
                            string cFinalArqRetorno,
                            PadroesNFSe padraoNFSe,
                            Servicos servicoNFSe,
                            Core.Empresa empresa)
        {            
            XmlDocument docXML = new XmlDocument();

            // Definir o tipo de serviço da NFe
            Type typeServicoNFe = oServicoNFe.GetType();

            // Resgatar o nome do arquivo XML a ser enviado para o webservice
            string XmlNfeDadosMsg = (string)(typeServicoNFe.InvokeMember("NomeArquivoXML", System.Reflection.BindingFlags.GetProperty, null, oServicoNFe, null));

            // Exclui o Arquivo de Erro
            Functions.DeletarArquivo(empresa.PastaRetornoNFse + "\\" + Functions/*oAux*/.ExtrairNomeArq(XmlNfeDadosMsg, cFinalArqEnvio + ".xml") + cFinalArqRetorno + ".err");

            // Validar o Arquivo XML
            ValidarXML validar = new ValidarXML(XmlNfeDadosMsg, empresa.UFCod);
            string cResultadoValidacao = validar.ValidarArqXML(XmlNfeDadosMsg);
            if (cResultadoValidacao != "")
            {
                throw new Exception(cResultadoValidacao);
            }

            // Montar o XML de Lote de envio de Notas fiscais
            docXML.Load(XmlNfeDadosMsg);

            // Definir Proxy
            if (ConfiguracaoApp.Proxy)
                if (padraoNFSe != PadroesNFSe.BETHA)
                {
                    oWSProxy.SetProp(oServicoWS, "Proxy", Proxy.DefinirProxy(ConfiguracaoApp.ProxyServidor, ConfiguracaoApp.ProxyUsuario, ConfiguracaoApp.ProxySenha, ConfiguracaoApp.ProxyPorta));
                }
                else
                {
                    oWSProxy.Betha.Proxy = Proxy.DefinirProxy(ConfiguracaoApp.ProxyServidor, ConfiguracaoApp.ProxyUsuario, ConfiguracaoApp.ProxySenha, ConfiguracaoApp.ProxyPorta);
                }

            // Limpa a variável de retorno
            string strRetorno = string.Empty;

            //Vou mudar o timeout para evitar que demore a resposta e o uninfe aborte antes de recebe-la. Wandrey 17/09/2009
            //Isso talvez evite de não conseguir o número do recibo se o serviço do SEFAZ estiver lento.
            if (padraoNFSe != PadroesNFSe.BETHA)
                oWSProxy.SetProp(oServicoWS, "Timeout", 60000);
                     
            //Invocar o membro
            switch (padraoNFSe)
            {
                #region Padrão BETHA
                case PadroesNFSe.BETHA:
                    switch (cMetodo)
                    {
                        case "ConsultarSituacaoLoteRps":
                            strRetorno = oWSProxy.Betha.ConsultarSituacaoLoteRps(docXML, empresa.tpAmb);
                            break;

                        case "ConsultarLoteRps":
                            strRetorno = oWSProxy.Betha.ConsultarLoteRps(docXML, empresa.tpAmb);
                            break;

                        case "CancelarNfse":
                            strRetorno = oWSProxy.Betha.CancelarNfse(docXML, empresa.tpAmb);
                            break;

                        case "ConsultarNfse":
                            strRetorno = oWSProxy.Betha.ConsultarNfse(docXML, empresa.tpAmb);
                            break;

                        case "ConsultarNfsePorRps":
                            strRetorno = oWSProxy.Betha.ConsultarNfsePorRps(docXML, empresa.tpAmb);
                            break;

                        case "RecepcionarLoteRps":
                            strRetorno = oWSProxy.Betha.RecepcionarLoteRps(docXML, empresa.tpAmb);
                            break;
                    }
                    break;
                #endregion

                #region Padrão ISSONLINE
                case PadroesNFSe.ISSONLINE:
                    int operacao;
                    string senhaWs = Functions.GetMD5Hash(empresa.SenhaWS);

                    switch (servicoNFSe)
                    {
                        case Servicos.RecepcionarLoteRps:
                            operacao = 1;
                            break;
                        case Servicos.CancelarNfse:
                            operacao = 2;
                            break;
                        default:
                            operacao = 3;
                            break;
                    }

                    strRetorno = oWSProxy.InvokeStr(oServicoWS, cMetodo, new object[] { Convert.ToSByte(operacao), empresa.UsuarioWS, senhaWs, docXML.OuterXml });
                    break;
                #endregion

                #region Padrão Blumenau-SC
                case PadroesNFSe.BLUMENAU_SC:
                    strRetorno = oWSProxy.InvokeStr(oServicoWS, cMetodo, new object[] { 1, docXML.OuterXml });
                    break;
                #endregion

                #region Padrão Paulistana
                case PadroesNFSe.PAULISTANA:
                    strRetorno = oWSProxy.InvokeStr(oServicoWS, cMetodo, new object[] { 1, docXML.OuterXml });
                    break;
                #endregion

                #region Demais padrões
                case PadroesNFSe.GINFES:
                case PadroesNFSe.THEMA:
                case PadroesNFSe.SALVADOR_BA:
                case PadroesNFSe.CANOAS_RS:
                case PadroesNFSe.ISSNET:
                case PadroesNFSe.DUETO:
                default:
                    if (string.IsNullOrEmpty(cabecMsg))
                        strRetorno = oWSProxy.InvokeStr(oServicoWS, cMetodo, new object[] { docXML.OuterXml });
                    else
                        strRetorno = oWSProxy.InvokeStr(oServicoWS, cMetodo, new object[] { cabecMsg.ToString(), docXML.OuterXml });

                    break;
                #endregion
            }

            //Atualizar o atributo do serviço da Nfe com o conteúdo retornado do webservice do sefaz                  
            typeServicoNFe.InvokeMember("vStrXmlRetorno", System.Reflection.BindingFlags.SetProperty, null, oServicoNFe, new object[] { strRetorno });

            // Registra o retorno de acordo com o status obtido
            if (cFinalArqEnvio != string.Empty && cFinalArqRetorno != string.Empty)
            {
                typeServicoNFe.InvokeMember("XmlRetorno", System.Reflection.BindingFlags.InvokeMethod, null, oServicoNFe, new Object[] { cFinalArqEnvio + ".xml", cFinalArqRetorno + ".xml", empresa });
            }
        }
        #endregion

        #endregion
    }
}
