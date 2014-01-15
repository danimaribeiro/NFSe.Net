namespace NFSE.Net.Tests
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.tb_conta_demonstrativo_resultadoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.desenvolvimentoDataSet = new NFSE.Net.Tests.desenvolvimentoDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tb_conta_demonstrativo_resultadoTableAdapter = new NFSE.Net.Tests.desenvolvimentoDataSetTableAdapters.tb_conta_demonstrativo_resultadoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tb_conta_demonstrativo_resultadoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desenvolvimentoDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_conta_demonstrativo_resultadoBindingSource
            // 
            this.tb_conta_demonstrativo_resultadoBindingSource.DataMember = "tb_conta_demonstrativo_resultado";
            this.tb_conta_demonstrativo_resultadoBindingSource.DataSource = this.desenvolvimentoDataSet;
            // 
            // desenvolvimentoDataSet
            // 
            this.desenvolvimentoDataSet.DataSetName = "desenvolvimentoDataSet";
            this.desenvolvimentoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.tb_conta_demonstrativo_resultadoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "NFSE.Net.Tests.Report4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(988, 487);
            this.reportViewer1.TabIndex = 0;
            // 
            // tb_conta_demonstrativo_resultadoTableAdapter
            // 
            this.tb_conta_demonstrativo_resultadoTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 502);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_conta_demonstrativo_resultadoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desenvolvimentoDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource tb_conta_demonstrativo_resultadoBindingSource;
        private desenvolvimentoDataSet desenvolvimentoDataSet;
        private desenvolvimentoDataSetTableAdapters.tb_conta_demonstrativo_resultadoTableAdapter tb_conta_demonstrativo_resultadoTableAdapter;
    }
}