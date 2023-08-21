namespace _2023._08_Uni_Scheduler.App
{
    partial class frmGenerator
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.rtxtQuery = new System.Windows.Forms.RichTextBox();
            this.chkSeparateDashboards = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lsbReportList = new System.Windows.Forms.ListBox();
            this.txtSheetName = new System.Windows.Forms.TextBox();
            this.txtEmailTitle = new System.Windows.Forms.TextBox();
            this.lblSheetName = new System.Windows.Forms.Label();
            this.lblEmailTitle = new System.Windows.Forms.Label();
            this.lblQuery = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lsbConnections = new System.Windows.Forms.ListBox();
            this.lblConnections = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.txtActive = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkExcel = new System.Windows.Forms.CheckBox();
            this.chkJson = new System.Windows.Forms.CheckBox();
            this.chkXml = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(2, 449);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(1411, 191);
            this.dgvData.TabIndex = 38;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(352, 323);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(294, 37);
            this.txtOutput.TabIndex = 37;
            // 
            // rtxtQuery
            // 
            this.rtxtQuery.Location = new System.Drawing.Point(11, 12);
            this.rtxtQuery.Name = "rtxtQuery";
            this.rtxtQuery.Size = new System.Drawing.Size(1411, 292);
            this.rtxtQuery.TabIndex = 0;
            this.rtxtQuery.Text = "";
            // 
            // chkSeparateDashboards
            // 
            this.chkSeparateDashboards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSeparateDashboards.AutoSize = true;
            this.chkSeparateDashboards.BackColor = System.Drawing.Color.Transparent;
            this.chkSeparateDashboards.Location = new System.Drawing.Point(727, 306);
            this.chkSeparateDashboards.Name = "chkSeparateDashboards";
            this.chkSeparateDashboards.Size = new System.Drawing.Size(98, 17);
            this.chkSeparateDashboards.TabIndex = 45;
            this.chkSeparateDashboards.Text = "Envio Conjunto";
            this.chkSeparateDashboards.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(831, 323);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(34, 20);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Ok";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // lsbReportList
            // 
            this.lsbReportList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbReportList.FormattingEnabled = true;
            this.lsbReportList.Location = new System.Drawing.Point(652, 347);
            this.lsbReportList.Margin = new System.Windows.Forms.Padding(2);
            this.lsbReportList.Name = "lsbReportList";
            this.lsbReportList.Size = new System.Drawing.Size(213, 69);
            this.lsbReportList.TabIndex = 46;
            // 
            // txtSheetName
            // 
            this.txtSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSheetName.Location = new System.Drawing.Point(651, 323);
            this.txtSheetName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSheetName.Name = "txtSheetName";
            this.txtSheetName.Size = new System.Drawing.Size(173, 20);
            this.txtSheetName.TabIndex = 7;
            // 
            // txtEmailTitle
            // 
            this.txtEmailTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailTitle.Location = new System.Drawing.Point(869, 323);
            this.txtEmailTitle.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmailTitle.Name = "txtEmailTitle";
            this.txtEmailTitle.Size = new System.Drawing.Size(335, 20);
            this.txtEmailTitle.TabIndex = 9;
            // 
            // lblSheetName
            // 
            this.lblSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSheetName.AutoSize = true;
            this.lblSheetName.Location = new System.Drawing.Point(649, 307);
            this.lblSheetName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSheetName.Name = "lblSheetName";
            this.lblSheetName.Size = new System.Drawing.Size(72, 13);
            this.lblSheetName.TabIndex = 41;
            this.lblSheetName.Text = "Nome da Aba";
            // 
            // lblEmailTitle
            // 
            this.lblEmailTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmailTitle.AutoSize = true;
            this.lblEmailTitle.Location = new System.Drawing.Point(865, 307);
            this.lblEmailTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmailTitle.Name = "lblEmailTitle";
            this.lblEmailTitle.Size = new System.Drawing.Size(81, 13);
            this.lblEmailTitle.TabIndex = 39;
            this.lblEmailTitle.Text = "Título do E-mail";
            // 
            // lblQuery
            // 
            this.lblQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(8, -41);
            this.lblQuery.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(48, 13);
            this.lblQuery.TabIndex = 49;
            this.lblQuery.Text = "Consulta";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(655, 418);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(200, 26);
            this.lblInfo.TabIndex = 50;
            this.lblInfo.Text = "Para retirar um item da seleção pressione\r\n\'delete\' para o item selecionado.";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendEmail.Location = new System.Drawing.Point(1095, 395);
            this.btnSendEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(108, 35);
            this.btnSendEmail.TabIndex = 11;
            this.btnSendEmail.Text = "Enviar e-mail";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            // 
            // txtTo
            // 
            this.txtTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTo.Location = new System.Drawing.Point(869, 361);
            this.txtTo.Margin = new System.Windows.Forms.Padding(2);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(336, 20);
            this.txtTo.TabIndex = 10;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(867, 345);
            this.lblTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(35, 13);
            this.lblTo.TabIndex = 52;
            this.lblTo.Text = "E-mail";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(352, 366);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Executar";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // lsbConnections
            // 
            this.lsbConnections.FormattingEnabled = true;
            this.lsbConnections.Location = new System.Drawing.Point(11, 323);
            this.lsbConnections.Margin = new System.Windows.Forms.Padding(2);
            this.lsbConnections.Name = "lsbConnections";
            this.lsbConnections.Size = new System.Drawing.Size(336, 82);
            this.lsbConnections.TabIndex = 1;
            // 
            // lblConnections
            // 
            this.lblConnections.AutoSize = true;
            this.lblConnections.Location = new System.Drawing.Point(8, 307);
            this.lblConnections.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConnections.Name = "lblConnections";
            this.lblConnections.Size = new System.Drawing.Size(54, 13);
            this.lblConnections.TabIndex = 57;
            this.lblConnections.Text = "Conexões";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Location = new System.Drawing.Point(12, 413);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(37, 13);
            this.lblActive.TabIndex = 60;
            this.lblActive.Text = "Active";
            // 
            // txtActive
            // 
            this.txtActive.Location = new System.Drawing.Point(55, 410);
            this.txtActive.Name = "txtActive";
            this.txtActive.Size = new System.Drawing.Size(292, 20);
            this.txtActive.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(868, 395);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(222, 35);
            this.progressBar1.TabIndex = 61;
            // 
            // chkExcel
            // 
            this.chkExcel.AutoSize = true;
            this.chkExcel.Location = new System.Drawing.Point(593, 363);
            this.chkExcel.Name = "chkExcel";
            this.chkExcel.Size = new System.Drawing.Size(52, 17);
            this.chkExcel.TabIndex = 4;
            this.chkExcel.Text = "Excel";
            this.chkExcel.UseVisualStyleBackColor = true;
            // 
            // chkJson
            // 
            this.chkJson.AutoSize = true;
            this.chkJson.Location = new System.Drawing.Point(593, 382);
            this.chkJson.Name = "chkJson";
            this.chkJson.Size = new System.Drawing.Size(54, 17);
            this.chkJson.TabIndex = 5;
            this.chkJson.Text = "JSON";
            this.chkJson.UseVisualStyleBackColor = true;
            // 
            // chkXml
            // 
            this.chkXml.AutoSize = true;
            this.chkXml.Location = new System.Drawing.Point(593, 402);
            this.chkXml.Name = "chkXml";
            this.chkXml.Size = new System.Drawing.Size(48, 17);
            this.chkXml.TabIndex = 6;
            this.chkXml.Text = "XML";
            this.chkXml.UseVisualStyleBackColor = true;
            // 
            // frmGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 653);
            this.Controls.Add(this.chkXml);
            this.Controls.Add(this.chkJson);
            this.Controls.Add(this.chkExcel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.txtActive);
            this.Controls.Add(this.lsbConnections);
            this.Controls.Add(this.lblConnections);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtSheetName);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.chkSeparateDashboards);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lsbReportList);
            this.Controls.Add(this.txtEmailTitle);
            this.Controls.Add(this.lblSheetName);
            this.Controls.Add(this.lblEmailTitle);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.rtxtQuery);
            this.Name = "frmGenerator";
            this.Text = "Gerador";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.RichTextBox rtxtQuery;
        private System.Windows.Forms.CheckBox chkSeparateDashboards;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lsbReportList;
        private System.Windows.Forms.TextBox txtSheetName;
        private System.Windows.Forms.TextBox txtEmailTitle;
        private System.Windows.Forms.Label lblSheetName;
        private System.Windows.Forms.Label lblEmailTitle;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ListBox lsbConnections;
        private System.Windows.Forms.Label lblConnections;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.TextBox txtActive;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkExcel;
        private System.Windows.Forms.CheckBox chkJson;
        private System.Windows.Forms.CheckBox chkXml;
    }
}