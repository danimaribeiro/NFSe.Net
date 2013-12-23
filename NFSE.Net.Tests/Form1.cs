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
            try
            {
                string caminhoXml = @"C:\Users\danimaribeiro\Documents\Nota_Servico\NOVOS_RPS\1-env.xml";
                System.Net.ServicePointManager.Expect100Continue = false;

                NFSE.Net.Core.ConfiguracaoApp.CarregarDados();
                var envio = new NFSE.Net.Envio.Processar();
                envio.ProcessaArquivo(0, caminhoXml, Servicos.RecepcionarLoteRps);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string caminhoXml = @"C:\Users\danimaribeiro\Documents\Nota_Servico\NOVOS_RPS\1-consulta-situacao-lote.xml";

            var consultaSituacaoLote = new Layouts.Betha.ConsultarSituacaoLoteRpsEnvio();
            consultaSituacaoLote.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            consultaSituacaoLote.Prestador.Cnpj = "03657739000169";
            consultaSituacaoLote.Prestador.InscricaoMunicipal = "24082-6";
            consultaSituacaoLote.Protocolo = "855426049227311";

            if (System.IO.File.Exists(caminhoXml))
                System.IO.File.Delete(caminhoXml);

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.ConsultarSituacaoLoteRpsEnvio>(consultaSituacaoLote, caminhoXml);
            
            SchemaXMLNFSe.CriarListaIDXML();
            System.Net.ServicePointManager.Expect100Continue = false;
            Propriedade.TipoAplicativo = TipoAplicativo.Nfse;
            NFSE.Net.Core.Empresa.CarregaConfiguracao();
            NFSE.Net.Core.ConfiguracaoApp.CarregarDados();
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(0, caminhoXml, Servicos.ConsultarSituacaoLoteRps);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SchemaXMLNFSe.CriarListaIDXML();
            System.Net.ServicePointManager.Expect100Continue = false;
            Propriedade.TipoAplicativo = TipoAplicativo.Nfse;
            NFSE.Net.Core.Empresa.CarregaConfiguracao();
            NFSE.Net.Core.ConfiguracaoApp.CarregarDados();
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(0, @"C:\Users\danimaribeiro\Documents\Nota_Servico\Ger\960-ped-loterps.xml", Servicos.ConsultarLoteRps);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string caminhoXml = @"C:\Users\danimaribeiro\Documents\Nota_Servico\NOVOS_RPS\1-env.xml";

            Layouts.Betha.EnviarLoteRpsEnvio envio = new Layouts.Betha.EnviarLoteRpsEnvio();
            envio.LoteRps = new Layouts.Betha.tcLoteRps();
            envio.LoteRps.Cnpj = "03657739000169";
            envio.LoteRps.Id = "1400";
            envio.LoteRps.InscricaoMunicipal = "24082-6";
            envio.LoteRps.NumeroLote = "1400";
            envio.LoteRps.QuantidadeRps = 2;
            envio.LoteRps.ListaRps = new Layouts.Betha.tcRps[2] { new Layouts.Betha.tcRps(), new Layouts.Betha.tcRps() };
            envio.LoteRps.ListaRps[0].InfRps = new Layouts.Betha.tcInfRps();
            envio.LoteRps.ListaRps[0].InfRps.Id = "rps1AA";
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps = new Layouts.Betha.tcIdentificacaoRps();
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Numero = "1";
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Serie = "AA";
            envio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Tipo = 1;
            envio.LoteRps.ListaRps[0].InfRps.DataEmissao = DateTime.Now;
            envio.LoteRps.ListaRps[0].InfRps.NaturezaOperacao = 1;
            envio.LoteRps.ListaRps[0].InfRps.RegimeEspecialTributacao = 1;
            envio.LoteRps.ListaRps[0].InfRps.RegimeEspecialTributacaoSpecified = true;
            envio.LoteRps.ListaRps[0].InfRps.OptanteSimplesNacional = 1;
            envio.LoteRps.ListaRps[0].InfRps.IncentivadorCultural = 2;
            envio.LoteRps.ListaRps[0].InfRps.Status = 1;

            envio.LoteRps.ListaRps[0].InfRps.Servico = new Layouts.Betha.tcDadosServico();
            envio.LoteRps.ListaRps[0].InfRps.Servico.ItemListaServico = "0105";
            envio.LoteRps.ListaRps[0].InfRps.Servico.Discriminacao = "Serviço de venda";
            envio.LoteRps.ListaRps[0].InfRps.Servico.CodigoMunicipio = 4204202;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores = new Layouts.Betha.tcValores();
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.ValorServicos = 1;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.IssRetido = 2;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.ValorIss = 0.04M;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.ValorIssSpecified = true;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.BaseCalculo = 1;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.BaseCalculoSpecified = true;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.Aliquota = 4;
            envio.LoteRps.ListaRps[0].InfRps.Servico.Valores.AliquotaSpecified = true;

            envio.LoteRps.ListaRps[0].InfRps.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            envio.LoteRps.ListaRps[0].InfRps.Prestador.Cnpj = "03657739000169";
            envio.LoteRps.ListaRps[0].InfRps.Prestador.InscricaoMunicipal = "24082-6";

            envio.LoteRps.ListaRps[0].InfRps.Tomador = new Layouts.Betha.tcDadosTomador();
            envio.LoteRps.ListaRps[0].InfRps.Tomador.IdentificacaoTomador = new Layouts.Betha.tcIdentificacaoTomador();
            envio.LoteRps.ListaRps[0].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new Layouts.Betha.tcCpfCnpj() { ItemElementName = Layouts.Betha.ItemChoiceType.Cnpj, Item = "09072780000150" };
            envio.LoteRps.ListaRps[0].InfRps.Tomador.RazaoSocial = "Mecanica Boa Viagem";
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco = new Layouts.Betha.tcEndereco();
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.Endereco = "Rua do comercio";
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.Numero = "15";
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.Bairro = "Centro";
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.CodigoMunicipio = 4204350;
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.CodigoMunicipioSpecified = true;
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.Uf = "SC";
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.Cep = 88032050;
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Endereco.CepSpecified = true;

            envio.LoteRps.ListaRps[0].InfRps.Tomador.Contato = new Layouts.Betha.tcContato();
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Contato.Email = "email@email.com.br";
            envio.LoteRps.ListaRps[0].InfRps.Tomador.Contato.Telefone = "32386621";

            envio.LoteRps.ListaRps[1].InfRps = new Layouts.Betha.tcInfRps();
            envio.LoteRps.ListaRps[1].InfRps.Id = "rps2AA";
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps = new Layouts.Betha.tcIdentificacaoRps();
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps.Numero = "2";
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps.Serie = "AA";
            envio.LoteRps.ListaRps[1].InfRps.IdentificacaoRps.Tipo = 1;
            envio.LoteRps.ListaRps[1].InfRps.DataEmissao = DateTime.Now;
            envio.LoteRps.ListaRps[1].InfRps.NaturezaOperacao = 1;
            envio.LoteRps.ListaRps[1].InfRps.RegimeEspecialTributacao = 1;
            envio.LoteRps.ListaRps[1].InfRps.RegimeEspecialTributacaoSpecified = true;
            envio.LoteRps.ListaRps[1].InfRps.OptanteSimplesNacional = 1;
            envio.LoteRps.ListaRps[1].InfRps.IncentivadorCultural = 2;
            envio.LoteRps.ListaRps[1].InfRps.Status = 1;

            envio.LoteRps.ListaRps[1].InfRps.Servico = new Layouts.Betha.tcDadosServico();
            envio.LoteRps.ListaRps[1].InfRps.Servico.ItemListaServico = "0105";
            envio.LoteRps.ListaRps[1].InfRps.Servico.Discriminacao = "Serviço de venda";
            envio.LoteRps.ListaRps[1].InfRps.Servico.CodigoMunicipio = 4204202;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores = new Layouts.Betha.tcValores();
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.ValorServicos = 1;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.IssRetido = 2;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.ValorIss = 0.04M;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.BaseCalculo = 1;
            envio.LoteRps.ListaRps[1].InfRps.Servico.Valores.Aliquota = 4;

            envio.LoteRps.ListaRps[1].InfRps.Prestador = new Layouts.Betha.tcIdentificacaoPrestador();
            envio.LoteRps.ListaRps[1].InfRps.Prestador.Cnpj = "03657739000169";
            envio.LoteRps.ListaRps[1].InfRps.Prestador.InscricaoMunicipal = "24082-6";

            envio.LoteRps.ListaRps[1].InfRps.Tomador = new Layouts.Betha.tcDadosTomador();
            envio.LoteRps.ListaRps[1].InfRps.Tomador.IdentificacaoTomador = new Layouts.Betha.tcIdentificacaoTomador();
            envio.LoteRps.ListaRps[1].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new Layouts.Betha.tcCpfCnpj() { ItemElementName = Layouts.Betha.ItemChoiceType.Cnpj, Item = "09072780000150" };
            envio.LoteRps.ListaRps[1].InfRps.Tomador.RazaoSocial = "Mecanica Boa Viagem";
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco = new Layouts.Betha.tcEndereco();
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.Endereco = "Rua do comercio";
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.Numero = "15";
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.Bairro = "Centro";
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.CodigoMunicipio = 4204350;
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.CodigoMunicipioSpecified = true;
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.Uf = "SC";
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.Cep = 88032050;
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Endereco.CepSpecified = true;

            envio.LoteRps.ListaRps[1].InfRps.Tomador.Contato = new Layouts.Betha.tcContato();
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Contato.Email = "email@email.com.br";
            envio.LoteRps.ListaRps[1].InfRps.Tomador.Contato.Telefone = "32386621";

            if (System.IO.File.Exists(caminhoXml))
                System.IO.File.Delete(caminhoXml);

            var serializar = new Layouts.Serializador();
            serializar.SalvarXml<Layouts.Betha.EnviarLoteRpsEnvio>(envio, caminhoXml);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var empresa = new Core.Empresa();
            empresa.CertificadoArquivo = @"C:\a.txt";
            empresa.tpAmb = 2;
            empresa.tpEmis = 1;
            empresa.UFCod = 42;
            Empresas.SalvarNovaEmpresa(empresa, "03657739000169", "Infoger Sistemas");
        }
    }
}
