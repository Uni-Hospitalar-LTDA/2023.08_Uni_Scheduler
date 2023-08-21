namespace _2023._08_Uni_Scheduler.App
{
    partial class frmReport_Information
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
            this.txtReportDescription = new System.Windows.Forms.TextBox();
            this.txtReportId = new System.Windows.Forms.TextBox();
            this.lblReport = new System.Windows.Forms.Label();
            this.lblObservation = new System.Windows.Forms.Label();
            this.txtObservation = new System.Windows.Forms.TextBox();
            this.lblArchives = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnRemoveQuery = new System.Windows.Forms.Button();
            this.btnAddQuery = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gpbFormats = new System.Windows.Forms.GroupBox();
            this.chkJsonFormat = new System.Windows.Forms.CheckBox();
            this.chkXmlFormat = new System.Windows.Forms.CheckBox();
            this.chkPdfFormat = new System.Windows.Forms.CheckBox();
            this.chkXlsxFormat = new System.Windows.Forms.CheckBox();
            this.chkWithSheets = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gpbFormats.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReportDescription
            // 
            this.txtReportDescription.Location = new System.Drawing.Point(62, 25);
            this.txtReportDescription.Name = "txtReportDescription";
            this.txtReportDescription.Size = new System.Drawing.Size(387, 20);
            this.txtReportDescription.TabIndex = 0;
            // 
            // txtReportId
            // 
            this.txtReportId.Location = new System.Drawing.Point(11, 25);
            this.txtReportId.Name = "txtReportId";
            this.txtReportId.Size = new System.Drawing.Size(45, 20);
            this.txtReportId.TabIndex = 4;
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Location = new System.Drawing.Point(12, 9);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(49, 13);
            this.lblReport.TabIndex = 3;
            this.lblReport.Text = "Relatório";
            // 
            // lblObservation
            // 
            this.lblObservation.AutoSize = true;
            this.lblObservation.Location = new System.Drawing.Point(12, 48);
            this.lblObservation.Name = "lblObservation";
            this.lblObservation.Size = new System.Drawing.Size(65, 13);
            this.lblObservation.TabIndex = 7;
            this.lblObservation.Text = "Observação";
            // 
            // txtObservation
            // 
            this.txtObservation.Location = new System.Drawing.Point(12, 64);
            this.txtObservation.Multiline = true;
            this.txtObservation.Name = "txtObservation";
            this.txtObservation.Size = new System.Drawing.Size(437, 76);
            this.txtObservation.TabIndex = 1;
            // 
            // lblArchives
            // 
            this.lblArchives.AutoSize = true;
            this.lblArchives.Location = new System.Drawing.Point(11, 143);
            this.lblArchives.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblArchives.Name = "lblArchives";
            this.lblArchives.Size = new System.Drawing.Size(48, 13);
            this.lblArchives.TabIndex = 24;
            this.lblArchives.Text = "Arquivos";
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(15, 162);
            this.dgvData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(511, 274);
            this.dgvData.TabIndex = 23;
            // 
            // btnRemoveQuery
            // 
            this.btnRemoveQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveQuery.Location = new System.Drawing.Point(81, 443);
            this.btnRemoveQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveQuery.Name = "btnRemoveQuery";
            this.btnRemoveQuery.Size = new System.Drawing.Size(66, 19);
            this.btnRemoveQuery.TabIndex = 5;
            this.btnRemoveQuery.Text = "Remover";
            this.btnRemoveQuery.UseVisualStyleBackColor = true;
            // 
            // btnAddQuery
            // 
            this.btnAddQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddQuery.Location = new System.Drawing.Point(13, 443);
            this.btnAddQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddQuery.Name = "btnAddQuery";
            this.btnAddQuery.Size = new System.Drawing.Size(66, 19);
            this.btnAddQuery.TabIndex = 4;
            this.btnAddQuery.Text = "Adicionar";
            this.btnAddQuery.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(451, 441);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(370, 441);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // gpbFormats
            // 
            this.gpbFormats.Controls.Add(this.chkJsonFormat);
            this.gpbFormats.Controls.Add(this.chkXmlFormat);
            this.gpbFormats.Controls.Add(this.chkPdfFormat);
            this.gpbFormats.Controls.Add(this.chkXlsxFormat);
            this.gpbFormats.Location = new System.Drawing.Point(454, 26);
            this.gpbFormats.Margin = new System.Windows.Forms.Padding(2);
            this.gpbFormats.Name = "gpbFormats";
            this.gpbFormats.Padding = new System.Windows.Forms.Padding(2);
            this.gpbFormats.Size = new System.Drawing.Size(72, 114);
            this.gpbFormats.TabIndex = 2;
            this.gpbFormats.TabStop = false;
            this.gpbFormats.Text = "Formatos";
            // 
            // chkJsonFormat
            // 
            this.chkJsonFormat.AutoSize = true;
            this.chkJsonFormat.Location = new System.Drawing.Point(4, 84);
            this.chkJsonFormat.Margin = new System.Windows.Forms.Padding(2);
            this.chkJsonFormat.Name = "chkJsonFormat";
            this.chkJsonFormat.Size = new System.Drawing.Size(48, 17);
            this.chkJsonFormat.TabIndex = 9;
            this.chkJsonFormat.Text = ".json";
            this.chkJsonFormat.UseVisualStyleBackColor = true;
            // 
            // chkXmlFormat
            // 
            this.chkXmlFormat.AutoSize = true;
            this.chkXmlFormat.Location = new System.Drawing.Point(4, 63);
            this.chkXmlFormat.Margin = new System.Windows.Forms.Padding(2);
            this.chkXmlFormat.Name = "chkXmlFormat";
            this.chkXmlFormat.Size = new System.Drawing.Size(44, 17);
            this.chkXmlFormat.TabIndex = 3;
            this.chkXmlFormat.Text = ".xml";
            this.chkXmlFormat.UseVisualStyleBackColor = true;
            // 
            // chkPdfFormat
            // 
            this.chkPdfFormat.AutoSize = true;
            this.chkPdfFormat.Location = new System.Drawing.Point(4, 42);
            this.chkPdfFormat.Margin = new System.Windows.Forms.Padding(2);
            this.chkPdfFormat.Name = "chkPdfFormat";
            this.chkPdfFormat.Size = new System.Drawing.Size(44, 17);
            this.chkPdfFormat.TabIndex = 2;
            this.chkPdfFormat.Text = ".pdf";
            this.chkPdfFormat.UseVisualStyleBackColor = true;
            // 
            // chkXlsxFormat
            // 
            this.chkXlsxFormat.AutoSize = true;
            this.chkXlsxFormat.Location = new System.Drawing.Point(4, 21);
            this.chkXlsxFormat.Margin = new System.Windows.Forms.Padding(2);
            this.chkXlsxFormat.Name = "chkXlsxFormat";
            this.chkXlsxFormat.Size = new System.Drawing.Size(46, 17);
            this.chkXlsxFormat.TabIndex = 1;
            this.chkXlsxFormat.Text = ".xlsx";
            this.chkXlsxFormat.UseVisualStyleBackColor = true;
            // 
            // chkWithSheets
            // 
            this.chkWithSheets.AutoSize = true;
            this.chkWithSheets.Location = new System.Drawing.Point(63, 143);
            this.chkWithSheets.Margin = new System.Windows.Forms.Padding(2);
            this.chkWithSheets.Name = "chkWithSheets";
            this.chkWithSheets.Size = new System.Drawing.Size(73, 17);
            this.chkWithSheets.TabIndex = 3;
            this.chkWithSheets.Text = "Com abas";
            this.chkWithSheets.UseVisualStyleBackColor = true;
            // 
            // frmReport_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 468);
            this.Controls.Add(this.chkWithSheets);
            this.Controls.Add(this.gpbFormats);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemoveQuery);
            this.Controls.Add(this.btnAddQuery);
            this.Controls.Add(this.lblArchives);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.lblObservation);
            this.Controls.Add(this.txtObservation);
            this.Controls.Add(this.txtReportDescription);
            this.Controls.Add(this.txtReportId);
            this.Controls.Add(this.lblReport);
            this.Name = "frmReport_Information";
            this.Text = "frmReport_Information";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gpbFormats.ResumeLayout(false);
            this.gpbFormats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReportDescription;
        private System.Windows.Forms.TextBox txtReportId;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblObservation;
        private System.Windows.Forms.TextBox txtObservation;
        private System.Windows.Forms.Label lblArchives;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnRemoveQuery;
        private System.Windows.Forms.Button btnAddQuery;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gpbFormats;
        private System.Windows.Forms.CheckBox chkJsonFormat;
        private System.Windows.Forms.CheckBox chkXmlFormat;
        private System.Windows.Forms.CheckBox chkPdfFormat;
        private System.Windows.Forms.CheckBox chkXlsxFormat;
        private System.Windows.Forms.CheckBox chkWithSheets;
    }
}