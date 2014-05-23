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
    public partial class BarcodeTest : Form
    {
        public BarcodeTest()
        {
            InitializeComponent();
        }

        private void BarcodeTest_Load(object sender, EventArgs e)
        {
            Font f = new Font("Code 128", 80);
            this.Font = f;

            Label l = new Label();
            l.Text = "1234567890";
            l.Size = new System.Drawing.Size(800, 600);
            this.Controls.Add(l);

            this.Size = new Size(800, 600);
        }
    }
}
