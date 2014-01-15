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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'desenvolvimentoDataSet.tb_conta_demonstrativo_resultado' table. You can move, or remove it, as needed.
            this.tb_conta_demonstrativo_resultadoTableAdapter.Fill(this.desenvolvimentoDataSet.tb_conta_demonstrativo_resultado);

            this.reportViewer1.RefreshReport();
        }
    }
}
