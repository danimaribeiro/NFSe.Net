using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFSE.Net.Tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SchemaXMLNFSe.CriarListaIDXML();
            System.Net.ServicePointManager.Expect100Continue = false;
            Propriedade.TipoAplicativo = TipoAplicativo.Nfse;
            NFSE.Net.Core.Empresa.CarregaConfiguracao();
            NFSE.Net.Core.ConfiguracaoApp.CarregarDados();
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(0, @"C:\Users\danimaribeiro\Documents\Nota_Servico\Ger\960-env-loterps.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SchemaXMLNFSe.CriarListaIDXML();
            System.Net.ServicePointManager.Expect100Continue = false;
            Propriedade.TipoAplicativo = TipoAplicativo.Nfse;
            NFSE.Net.Core.Empresa.CarregaConfiguracao();
            NFSE.Net.Core.ConfiguracaoApp.CarregarDados();
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(0, @"C:\Users\danimaribeiro\Documents\Nota_Servico\Ger\960-ped-sitloterps.xml");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SchemaXMLNFSe.CriarListaIDXML();
            System.Net.ServicePointManager.Expect100Continue = false;
            Propriedade.TipoAplicativo = TipoAplicativo.Nfse;
            NFSE.Net.Core.Empresa.CarregaConfiguracao();
            NFSE.Net.Core.ConfiguracaoApp.CarregarDados();
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(0, @"C:\Users\danimaribeiro\Documents\Nota_Servico\Ger\960-ped-loterps.xml");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string caminhoXml = @"C:\Users\danimaribeiro\Documents\Nota_Servico\NOVOS_RPS\1-env.xml";

            Layouts.Betha.EnviarLoteRpsEnvio envio = new Layouts.Betha.EnviarLoteRpsEnvio();
            envio.LoteRps = new Layouts.Betha.tcLoteRps();
            envio.LoteRps.Cnpj = "05456564";
            envio.LoteRps.id = "1";
            envio.LoteRps.InscricaoMunicipal = "12313";
            envio.LoteRps.NumeroLote = "1";
            envio.LoteRps.QuantidadeRps = 2;
            envio.LoteRps.ListaRps = new Layouts.Betha.tcRps[2] { new Layouts.Betha.tcRps(), new Layouts.Betha.tcRps() };
            envio.LoteRps.ListaRps[0].InfRps = new Layouts.Betha.tcInfRps();
            envio.LoteRps.ListaRps[0].InfRps.id = "12345";
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps = new Layouts.Betha.tcIdentificacaoRps();
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Numero = "1";
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Serie = "AA";
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Tipo = 1;
            envio.LoteRps.ListaRps[0].InfRps.DataEmissao = DateTime.Now;
            envio.LoteRps.ListaRps[0].InfRps.NaturezaOperacao = 1;
            envio.LoteRps.ListaRps[0].InfRps.RegimeEspecialTributacao = 1;
            envio.LoteRps.ListaRps[0].InfRps.OptanteSimplesNacional = 1;
            envio.LoteRps.ListaRps[0].InfRps.IncentivadorCultural = 2;
            envio.LoteRps.ListaRps[0].InfRps.Status = 1;

            envio.LoteRps.ListaRps[0].InfRps.Servico = new Layouts.Betha.tcDadosServico();
            envio.LoteRps.ListaRps[0].InfRps.Servico.ItemListaServico = "1105";
            envio.LoteRps.ListaRps[0].InfRps.Servico.Discriminacao = "Serviço de venda";
            envio.LoteRps.ListaRps[0].InfRps.Servico.CodigoMunicipio = 4204202;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores = new Layouts.Betha.tcValores();
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.ValorServicos = 1;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.IssRetido = 2;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.ValorIss = 0.04M;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.BaseCalculo = 1;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.Aliquota = 4;

            envio.LoteRps.ListaRps[0].InfRps.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            envio.LoteRps.ListaRps[0].InfRps.Tomador = new Layouts.Betha.tcDadosTomador();


            envio.LoteRps.ListaRps[1].InfRps = new Layouts.Betha.tcInfRps();
            envio.LoteRps.ListaRps[1].InfRps.id = "12345";
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps = new Layouts.Betha.tcIdentificacaoRps();
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps.Numero = "2";
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps.Serie = "AA";
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps.Tipo = 1;
            envio.LoteRps.ListaRps[1].InfRps.DataEmissao = DateTime.Now;
            envio.LoteRps.ListaRps[1].InfRps.NaturezaOperacao = 1;
            envio.LoteRps.ListaRps[1].InfRps.RegimeEspecialTributacao = 1;
            envio.LoteRps.ListaRps[1].InfRps.OptanteSimplesNacional = 1;
            envio.LoteRps.ListaRps[1].InfRps.IncentivadorCultural = 2;
            envio.LoteRps.ListaRps[1].InfRps.Status = 1;
                                   
            envio.LoteRps.ListaRps[1].InfRps.Servico = new Layouts.Betha.tcDadosServico();
            envio.LoteRps.ListaRps[1].InfRps.Servico.ItemListaServico = "1105";
            envio.LoteRps.ListaRps[1].InfRps.Servico.Discriminacao = "Serviço de venda";
            envio.LoteRps.ListaRps[1].InfRps.Servico.CodigoMunicipio = 4204202;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores = new Layouts.Betha.tcValores();
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.ValorServicos = 1;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.IssRetido = 2;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.ValorIss = 0.04M;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.BaseCalculo = 1;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.Aliquota = 4;

            envio.LoteRps.ListaRps[1].InfRps.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            envio.LoteRps.ListaRps[1].InfRps.Tomador = new Layouts.Betha.tcDadosTomador();


            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.EnviarLoteRpsEnvio>(envio, caminhoXml);
        }
    }
}
