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
    }
}
