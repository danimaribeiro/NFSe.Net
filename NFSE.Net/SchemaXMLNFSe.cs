using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class SchemaXMLNFSe
    {
        public static void CriarListaIDXML()
        {
            SchemaXML.InfSchemas.Clear();

            #region Schemas padrão GINFES

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-GINFES-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 22,
                ArquivoXSD = "NFSe\\GINFES\\servico_consultar_nfse_envio_v03.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "ConsultarNfseEnvio",
                TagAtributoId = "NumeroNfse",
                TargetNameSpace = "http://www.ginfes.com.br/servico_consultar_nfse_envio_v03.xsd"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-GINFES-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfsePorRpsEnvio",
                ID = 23,
                ArquivoXSD = "NFSe\\GINFES\\servico_consultar_nfse_rps_envio_v03.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "ConsultarNfseRpsEnvio",
                TagAtributoId = "Prestador",
                TargetNameSpace = "http://www.ginfes.com.br/servico_consultar_nfse_rps_envio_v03.xsd"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-GINFES-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 1,
                ArquivoXSD = "NFSe\\GINFES\\servico_consultar_lote_rps_envio_v03.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "ConsultarLoteRpsEnvio",
                TagAtributoId = "Protocolo",
                TargetNameSpace = "http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-GINFES-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 2,
                ArquivoXSD = "NFSe\\GINFES\\servico_cancelar_nfse_envio_v02.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "CancelarNfseEnvio",
                TagAtributoId = "NumeroNfse",
                TargetNameSpace = "http://www.ginfes.com.br/servico_cancelar_nfse_envio"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-GINFES-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 3,
                ArquivoXSD = "NFSe\\GINFES\\servico_consultar_situacao_lote_rps_envio_v03.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "ConsultarSituacaoLoteRpsEnvio",
                TagAtributoId = "Protocolo",
                TargetNameSpace = "http://www.ginfes.com.br/servico_consultar_situacao_lote_rps_envio_v03.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-GINFES-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 4,
                ArquivoXSD = "NFSe\\GINFES\\servico_enviar_lote_rps_envio_v03.xsd",
                Descricao = "XML de Lote RPS",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd"
            });
            #endregion

            #endregion

            #region Schemas padrão BETHA

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-BETHA-e:ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "e:ConsultarNfseEnvio",
                ID = 22,
                ArquivoXSD = "NFSe\\BETHA\\servico_consultar_nfse_envio_v01.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-BETHA-e:ConsultarNfsePorRpsEnvio", new InfSchema()
            {
                Tag = "e:ConsultarNfsePorRpsEnvio",
                ID = 23,
                ArquivoXSD = "NFSe\\BETHA\\servico_consultar_nfse_rps_envio_v01.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BETHA-e:ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "e:ConsultarLoteRpsEnvio",
                ID = 5,
                ArquivoXSD = "NFSe\\BETHA\\servico_consultar_lote_rps_envio_v01.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-BETHA-e:CancelarNfseEnvio", new InfSchema()
            {
                Tag = "e:CancelarNfseEnvio",
                ID = 6,
                ArquivoXSD = "NFSe\\BETHA\\servico_cancelar_nfse_envio_v01.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-BETHA-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 7,
                ArquivoXSD = "NFSe\\BETHA\\servico_cancelar_nfse_envio_v01.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BETHA-e:ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "e:ConsultarSituacaoLoteRpsEnvio",
                ID = 8,
                ArquivoXSD = "NFSe\\BETHA\\servico_consultar_situacao_lote_rps_envio_v01.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BETHA-e:EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "e:EnviarLoteRpsEnvio",
                ID = 9,
                ArquivoXSD = "NFSe\\BETHA\\servico_enviar_lote_rps_envio_v01.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "e:EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.betha.com.br/e-nota-contribuinte-ws"
            });
            #endregion

            #endregion

            #region Schemas padrão THEMA

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-THEMA-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 10,
                ArquivoXSD = "NFSe\\ABRASF\\nfse.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-THEMA-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 11,
                ArquivoXSD = "NFSe\\ABRASF\\nfse.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-THEMA-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 12,
                ArquivoXSD = "NFSe\\ABRASF\\nfse.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-THEMA-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 13,
                ArquivoXSD = "NFSe\\ABRASF\\nfse.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region Consulta NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-THEMA-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 14,
                ArquivoXSD = "NFSe\\ABRASF\\nfse.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region Consulta NFSe por período
            SchemaXML.InfSchemas.Add("NFSE-THEMA-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 15,
                ArquivoXSD = "NFSe\\ABRASF\\nfse.xsd",
                Descricao = "XML de Consulta da NFSe por período",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #endregion

            #region Schemas padrão ABACO

            #region CANOAS_RS

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-CANOAS_RS-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 16,
                ArquivoXSD = "NFSe\\ABACO\\nfse_v2010.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-CANOAS_RS-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 17,
                ArquivoXSD = "NFSe\\ABACO\\nfse_v2010.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-CANOAS_RS-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 18,
                ArquivoXSD = "NFSe\\ABACO\\nfse_v2010.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-CANOAS_RS-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 19,
                ArquivoXSD = "NFSe\\ABACO\\nfse_v2010.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region Consulta NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-CANOAS_RS-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 20,
                ArquivoXSD = "NFSe\\ABACO\\nfse_v2010.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region Consulta NFSe por período
            SchemaXML.InfSchemas.Add("NFSE-CANOAS_RS-ConsultarNfsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfsEnvio",
                ID = 21,
                ArquivoXSD = "NFSe\\ABACO\\nfse_v2010.xsd",
                Descricao = "XML de Consulta da NFSe por período",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #endregion

            #endregion

            #region Schemas padrão ISSNet

            #region NOVOHAMBURGO_RS

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 24,
                ArquivoXSD = "NFSe\\ISSNET\\servico_consultar_nfse_envio.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_consultar_nfse_envio.xsd"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 25,
                ArquivoXSD = "NFSe\\ISSNET\\servico_consultar_nfse_rps_envio.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_consultar_nfse_rps_envio.xsd"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 26,
                ArquivoXSD = "NFSe\\ISSNET\\servico_consultar_lote_rps_envio.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_consultar_lote_rps_envio.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-p1:CancelarNfseEnvio", new InfSchema()
            {
                Tag = "p1:CancelarNfseEnvio",
                ID = 27,
                ArquivoXSD = "NFSe\\ISSNET\\servico_cancelar_nfse_envio.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "tc:InfPedidoCancelamento",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_cancelar_nfse_envio.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 29,
                ArquivoXSD = "NFSe\\ISSNET\\servico_consultar_situacao_lote_rps_envio.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_consultar_situacao_lote_rps_envio.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 30,
                ArquivoXSD = "NFSe\\ISSNET\\servico_enviar_lote_rps_envio.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "tc:Rps",
                TagAtributoId = "tc:InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_enviar_lote_rps_envio.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-ISSNET-ConsultarUrlVisualizacaoNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarUrlVisualizacaoNfseEnvio",
                ID = 31,
                ArquivoXSD = "NFSe\\ISSNET\\servico_consultar_url_visualizacao_nfse_envio.xsd",
                Descricao = "XML de Consulta da URL de Visualização da NFSe",
                TagAssinatura = "",
                TargetNameSpace = "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_consultar_url_visualizacao_nfse_envio.xsd"
            });
            #endregion

            #endregion

            #endregion

            #region Schemas padrão ISSOnLine

            #region APUCARANA_PR/ARACATUBA_SP/PENAPOLIS_SP

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-ISSONLINE-NFSE", new InfSchema()
            {
                Tag = "NFSE",
                ID = 32,
                ArquivoXSD = "",
                Descricao = "XML de NFSe padrão ISSOnLine",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = ""
            });
            #endregion

            #endregion

            #endregion

            #region Schemas BLUMENAU_SC

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-BLUMENAU_SC-p1:PedidoConsultaNFePeriodo", new InfSchema()
            {
                Tag = "p1:PedidoConsultaNFePeriodo",
                ID = 33,
                ArquivoXSD = "NFSe\\BLUMENAUSC\\PedidoConsultaNFePeriodo_v01.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "p1:PedidoConsultaNFePeriodo",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://nfse.blumenau.sc.gov.br"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-BLUMENAU_SC-p1:PedidoConsultaNFe", new InfSchema()
            {
                Tag = "p1:PedidoConsultaNFe",
                ID = 34,
                ArquivoXSD = "NFSe\\BLUMENAUSC\\PedidoConsultaNFe_v01.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "p1:PedidoConsultaNFe",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://nfse.blumenau.sc.gov.br"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BLUMENAU_SC-p1:PedidoConsultaLote", new InfSchema()
            {
                Tag = "p1:PedidoConsultaLote",
                ID = 35,
                ArquivoXSD = "NFSe\\BLUMENAUSC\\PedidoConsultaLote_v01.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "p1:PedidoConsultaLote",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://nfse.blumenau.sc.gov.br"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-BLUMENAU_SC-PedidoCancelamentoNFe", new InfSchema()
            {
                Tag = "PedidoCancelamentoNFe",
                ID = 36,
                ArquivoXSD = "NFSe\\BLUMENAUSC\\PedidoCancelamentoNFe_v01.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "PedidoCancelamentoNFe",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://nfse.blumenau.sc.gov.br"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BLUMENAU_SC-p1:PedidoInformacoesLote", new InfSchema()
            {
                Tag = "p1:PedidoInformacoesLote",
                ID = 37,
                ArquivoXSD = "NFSe\\BLUMENAUSC\\PedidoInformacoesLote_v01.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "p1:PedidoInformacoesLote",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://nfse.blumenau.sc.gov.br"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BLUMENAU_SC-PedidoEnvioLoteRPS", new InfSchema()
            {
                Tag = "PedidoEnvioLoteRPS",
                ID = 38,
                ArquivoXSD = "NFSe\\BLUMENAUSC\\PedidoEnvioLoteRPS_v01.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "PedidoEnvioLoteRPS",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://nfse.blumenau.sc.gov.br"
            });
            #endregion

            #endregion

            #region Schemas BHISS

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-BHISS-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 39,
                ArquivoXSD = "NFSe\\BHISS\\nfse.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-BHISS-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 40,
                ArquivoXSD = "NFSe\\BHISS\\nfse.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BHISS-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 41,
                ArquivoXSD = "NFSe\\BHISS\\nfse.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-BHISS-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 42,
                ArquivoXSD = "NFSe\\BHISS\\nfse.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BHISS-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 43,
                ArquivoXSD = "NFSe\\BHISS\\nfse.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-BHISS-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 44,
                ArquivoXSD = "NFSe\\BHISS\\nfse.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #endregion

            #region Schemas GIF

            // testo se o UniNFS-e tem alguma empresa configurada para Parobé-RS
            bool usaParobeRS = false;
            foreach (var item in Empresa.Configuracoes)
            {
                if (item.UFCod == 4314050)
                    usaParobeRS = true;
            }

            if (usaParobeRS) // Unico Municipio da GIF que usa um XSD especifico
            {
                #region XML de Consulta de NFSe por Rps
                SchemaXML.InfSchemas.Add("NFSE-GIF-NFSE", new InfSchema()
                {
                    Tag = "NFSE",
                    ID = 45,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de Consulta de NFSe por Data",
                    TagAssinatura = "pedidoLoteNFSe",
                    TagAtributoId = "pedidoLoteNFSe",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Consulta de NFSe por Data
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoLoteNFSe", new InfSchema()
                {
                    Tag = "pedidoLoteNFSe",
                    ID = 45,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de NFSe padrão GIF",
                    TagAssinatura = "",
                    TagAtributoId = "",
                    TargetNameSpace = ""
                });
                #endregion

                #region XML de Consulta de NFSe por Rps
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedConsultaTrans", new InfSchema()
                {
                    Tag = "pedConsultaTrans",
                    ID = 46,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de Consulta de NFSe por Rps",
                    TagAssinatura = "pedConsultaTrans",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Consulta de Lote RPS
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoNFSe", new InfSchema()
                {
                    Tag = "pedidoNFSe",
                    ID = 47,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de Consulta de Lote RPS",
                    TagAssinatura = "pedidoNFSe",
                    TagAtributoId = "pedidoNFSe",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Cancelamento de NFS-e
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoCancelamentoLote", new InfSchema()
                {
                    Tag = "pedidoCancelamentoLote",
                    ID = 48,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de Cancelamento da NFS-e",
                    TagAssinatura = "pedidoCancelamentoLote",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Consulta Situação do Lote RPS
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoStatusLote", new InfSchema()
                {
                    Tag = "pedidoStatusLote",
                    ID = 49,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de Consulta da Situacao do Lote RPS",
                    TagAssinatura = "pedidoStatusLote",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de lote RPS
                SchemaXML.InfSchemas.Add("NFSE-GIF-envioLote", new InfSchema()
                {
                    Tag = "envioLote",
                    ID = 50,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Lote RPS",
                    TagAssinatura = "envioLote",
                    TagAtributoId = "NFS-e",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de consulta URL NFSe
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoNFSePNG", new InfSchema()
                {
                    Tag = "pedidoNFSePNG",
                    ID = 51,
                    ArquivoXSD = "NFSe\\GIF\\nfse-parobe-v1-1.xsd",
                    Descricao = "XML de consulta URL NFe padrão GIF",
                    TagAssinatura = "pedidoLoteNFSePNG",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

            }
            else
            {
                #region XML de Consulta de NFSe por Rps
                SchemaXML.InfSchemas.Add("NFSE-GIF-NFSE", new InfSchema()
                {
                    Tag = "NFSE",
                    ID = 45,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Consulta de NFSe por Data",
                    TagAssinatura = "pedidoLoteNFSe",
                    TagAtributoId = "pedidoLoteNFSe",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Consulta de NFSe por Data
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoLoteNFSe", new InfSchema()
                {
                    Tag = "pedidoLoteNFSe",
                    ID = 45,
                    ArquivoXSD = "",
                    Descricao = "XML de NFSe padrão GIF",
                    TagAssinatura = "",
                    TagAtributoId = "",
                    TargetNameSpace = ""
                });
                #endregion

                #region XML de Consulta de NFSe por Rps
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedConsultaTrans", new InfSchema()
                {
                    Tag = "pedConsultaTrans",
                    ID = 46,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Consulta de NFSe por Rps",
                    TagAssinatura = "pedConsultaTrans",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Consulta de Lote RPS
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoNFSe", new InfSchema()
                {
                    Tag = "pedidoNFSe",
                    ID = 47,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Consulta de Lote RPS",
                    TagAssinatura = "pedidoNFSe",
                    TagAtributoId = "pedidoNFSe",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Cancelamento de NFS-e
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedAnulaNFSe", new InfSchema()
                {
                    Tag = "pedAnulaNFSe",
                    ID = 48,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Cancelamento da NFS-e",
                    TagAssinatura = "pedAnulaNFSe",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de Consulta Situação do Lote RPS
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoStatusLote", new InfSchema()
                {
                    Tag = "pedidoStatusLote",
                    ID = 49,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Consulta da Situacao do Lote RPS",
                    TagAssinatura = "pedidoStatusLote",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de lote RPS
                SchemaXML.InfSchemas.Add("NFSE-GIF-envioLote", new InfSchema()
                {
                    Tag = "envioLote",
                    ID = 50,
                    ArquivoXSD = "NFSe\\GIF\\NFSe-Infisc-v1.xsd",
                    Descricao = "XML de Lote RPS",
                    TagAssinatura = "envioLote",
                    TagAtributoId = "NFS-e",
                    TargetNameSpace = "http://ws.pc.gif.com.br/"
                });
                #endregion

                #region XML de consulta URL NFSe
                SchemaXML.InfSchemas.Add("NFSE-GIF-pedidoLoteNFSePNG", new InfSchema()
                {
                    Tag = "pedidoLoteNFSePNG",
                    ID = 51,
                    ArquivoXSD = "",
                    Descricao = "XML de consulta URL NFe padrão GIF",
                    TagAssinatura = "pedidoLoteNFSePNG",
                    TagAtributoId = "CNPJ",
                    TargetNameSpace = ""
                });
                #endregion

            }



            #endregion

            #region Schemas padrão DUETO
            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-DUETO-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 49,
                ArquivoXSD = "NFSe\\DUETO\\servico_enviar_lote_rps_envio.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://tempuri.org/servico_enviar_lote_rps_envio.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-DUETO-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 50,
                ArquivoXSD = "NFSe\\DUETO\\servico_cancelar_nfse_envio.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://tempuri.org/servico_enviar_lote_rps_envio.xsd"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-DUETO-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 51,
                ArquivoXSD = "NFSe\\DUETO\\servico_consultar_lote_rps_envio.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://tempuri.org/servico_consultar_lote_rps_envio.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-DUETO-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 52,
                ArquivoXSD = "NFSe\\DUETO\\servico_consultar_situacao_lote_rps_envio.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://tempuri.org/servico_consultar_situacao_lote_rps_envio.xsd"
            });
            #endregion

            #region Consulta NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-DUETO-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 53,
                ArquivoXSD = "NFSe\\DUETO\\servico_consultar_nfse_rps_envio.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://tempuri.org/servico_consultar_nfse_rps_envio.xsd"
            });
            #endregion

            #region Consulta NFSe por período
            SchemaXML.InfSchemas.Add("NFSE-DUETO-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 54,
                ArquivoXSD = "NFSe\\DUETO\\servico_consultar_nfse_envio.xsd",
                Descricao = "XML de Consulta da NFSe por período",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://tempuri.org/servico_consultar_nfse_envio.xsd"
            });
            #endregion
            #endregion

            #region Schemas padrão WebISS

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-WEBISS-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 55,
                ArquivoXSD = "NFSe\\WEBISS\\servico_enviar_lote_rps_envio.xsd",
                Descricao = "XML de Lote RPS",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/nfse"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-WEBISS-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 56,
                ArquivoXSD = "NFSe\\WEBISS\\servico_cancelar_nfse_envio.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse"

            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-WEBISS-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 57,
                ArquivoXSD = "NFSe\\WEBISS\\servico_consultar_lote_rps_envio.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-WEBISS-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 58,
                ArquivoXSD = "NFSe\\WEBISS\\servico_consultar_situacao_lote_rps_envio.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse"
            });
            #endregion

            #region Consulta NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-WEBISS-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 59,
                ArquivoXSD = "NFSe\\WEBISS\\servico_consultar_nfse_rps_envio.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse"
            });
            #endregion

            #region Consulta NFSe por período
            SchemaXML.InfSchemas.Add("NFSE-WEBISS-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 60,
                ArquivoXSD = "NFSe\\WEBISS\\servico_consultar_nfse_envio.xsd",
                Descricao = "XML de Consulta da NFSe por período",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse"
            });
            #endregion

            #endregion

            #region Schemas padrão Paulistana

            #region XML de lote RPS

            SchemaXML.InfSchemas.Add("NFSE-PAULISTANA-PedidoEnvioLoteRPS", new InfSchema()
            {
                Tag = "PedidoEnvioLoteRPS",
                ID = 61,
                ArquivoXSD = "NFSe\\PAULISTANA\\PedidoEnvioLoteRPS_v01.xsd",
                Descricao = "XML de Lote RPS",
                TagLoteAssinatura = "PedidoEnvioLoteRPS",
                TagLoteAtributoId = "Cabecalho",
                TargetNameSpace = "http://www.prefeitura.sp.gov.br/nfe"
            });

            #endregion

            #region XML de Cancelamento de NFS-e

            SchemaXML.InfSchemas.Add("NFSE-PAULISTANA-PedidoCancelamentoNFe", new InfSchema()
            {
                Tag = "PedidoCancelamentoNFe",
                ID = 62,
                ArquivoXSD = "NFSe\\PAULISTANA\\PedidoCancelamentoNFe_v01.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "PedidoCancelamentoNFe",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://www.prefeitura.sp.gov.br/nfe"

            });

            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PAULISTANA-p1:PedidoConsultaLote", new InfSchema()
            {
                Tag = "p1:PedidoConsultaLote",
                ID = 63,
                ArquivoXSD = "NFSe\\PAULISTANA\\PedidoConsultaLote_v01.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "p1:PedidoConsultaLote",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://www.prefeitura.sp.gov.br/nfe"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PAULISTANA-p1:PedidoInformacoesLote", new InfSchema()
            {
                Tag = "p1:PedidoInformacoesLote",
                ID = 64,
                ArquivoXSD = "NFSe\\PAULISTANA\\PedidoInformacoesLote_v01.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "p1:PedidoInformacoesLote",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://www.prefeitura.sp.gov.br/nfe"
            });
            #endregion

            #region Consulta NFSe por período
            SchemaXML.InfSchemas.Add("NFSE-PAULISTANA-p1:PedidoConsultaNFePeriodo", new InfSchema()
            {
                Tag = "p1:PedidoConsultaNFePeriodo",
                ID = 65,
                ArquivoXSD = "NFSe\\PAULISTANA\\PedidoConsultaNFePeriodo_v01.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "p1:PedidoConsultaNFePeriodo",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://www.prefeitura.sp.gov.br/nfe"
            });
            #endregion

            #region Consulta NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-PAULISTANA-p1:PedidoConsultaNFe", new InfSchema()
            {
                Tag = "p1:PedidoConsultaNFe",
                ID = 66,
                ArquivoXSD = "NFSe\\PAULISTANA\\PedidoConsultaNFe_v01.xsd",
                Descricao = "XML de Consulta da NFSe por período",
                TagAssinatura = "p1:PedidoConsultaNFe",
                TagAtributoId = "Cabecalho",
                TargetNameSpace = "http://www.prefeitura.sp.gov.br/nfe"
            });

            #endregion

            #endregion

            #region Schemas SALVADOR_BA

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-SALVADOR_BA-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 67,
                ArquivoXSD = "NFSe\\SALVADORBA\\nfse_salvador.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-SALVADOR_BA-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 68,
                ArquivoXSD = "NFSe\\SALVADORBA\\nfse_salvador.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-SALVADOR_BA-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 69,
                ArquivoXSD = "NFSe\\SALVADORBA\\nfse_salvador.xsd",
                Descricao = "XML de Consulta de Situação do Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-SALVADOR_BA-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 70,
                ArquivoXSD = "NFSe\\SALVADORBA\\nfse_salvador.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-SALVADOR_BA-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 71,
                ArquivoXSD = "NFSe\\SALVADORBA\\nfse_salvador.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-SALVADOR_BA-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 72,
                ArquivoXSD = "NFSe\\SALVADORBA\\nfse_salvador.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #endregion

            #region Schemas padrão PORTOVELHENSE

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PORTOVELHENSE-p:ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "p:ConsultarLoteRpsEnvio",
                ID = 73,
                ArquivoXSD = "NFSe\\PORTOVELHENSE\\nfse_v2.0.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-PORTOVELHENSE-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 74,
                ArquivoXSD = "NFSe\\PORTOVELHENSE\\nfse_v2.0.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PORTOVELHENSE-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 75,
                ArquivoXSD = "NFSe\\PORTOVELHENSE\\nfse_v2.0.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PORTOVELHENSE-GerarNfseEnvio", new InfSchema()
            {
                Tag = "GerarNfseEnvio",
                ID = 76,
                ArquivoXSD = "NFSe\\PORTOVELHENSE\\nfse_v2.0.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfDeclaracaoPrestacaoServico",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region Consulta NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-PORTOVELHENSE-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 77,
                ArquivoXSD = "NFSe\\PORTOVELHENSE\\nfse_v2.0.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #region Consulta NFSe por Faixa
            SchemaXML.InfSchemas.Add("NFSE-PORTOVELHENSE-ConsultarNfseFaixaEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseFaixaEnvio",
                ID = 78,
                ArquivoXSD = "NFSe\\PORTOVELHENSE\\nfse_v2.0.xsd",
                Descricao = "XML de Consulta da NFSe por Faixa",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/nfse.xsd"
            });
            #endregion

            #endregion

            #region Schemas PRONIN

            #region XML de Consulta de NFSe por Data
            SchemaXML.InfSchemas.Add("NFSE-PRONIN-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = 78,
                ArquivoXSD = "NFSe\\PRONIN\\nfse.xsd",
                Descricao = "XML de Consulta de NFSe por Data",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta de NFSe por Rps
            SchemaXML.InfSchemas.Add("NFSE-PRONIN-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = 79,
                ArquivoXSD = "NFSe\\PRONIN\\nfse.xsd",
                Descricao = "XML de Consulta de NFSe por Rps",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta de Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PRONIN-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = 80,
                ArquivoXSD = "NFSe\\PRONIN\\nfse.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Cancelamento de NFS-e
            SchemaXML.InfSchemas.Add("NFSE-PRONIN-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = 81,
                ArquivoXSD = "NFSe\\PRONIN\\nfse.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "Pedido",
                TagAtributoId = "InfPedidoCancelamento",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de Consulta Situação do Lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PRONIN-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = 82,
                ArquivoXSD = "NFSe\\PRONIN\\nfse.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #region XML de lote RPS
            SchemaXML.InfSchemas.Add("NFSE-PRONIN-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = 83,
                ArquivoXSD = "NFSe\\PRONIN\\nfse.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });
            #endregion

            #endregion

            #region Determinar a propriedade MaxID
            SchemaXML.MaxID = 0;
            foreach (InfSchema item in SchemaXML.InfSchemas.Values)
            {
                if (item.ID > SchemaXML.MaxID)
                    SchemaXML.MaxID = item.ID;
            }
            #endregion

        }
    }
}
