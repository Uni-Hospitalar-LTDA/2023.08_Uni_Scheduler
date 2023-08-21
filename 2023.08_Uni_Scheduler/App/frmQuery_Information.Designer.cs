namespace _2023._08_Uni_Scheduler.App
{
    partial class frmQuery_Information
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuery_Information));
            this.txtQueryDescription = new System.Windows.Forms.TextBox();
            this.txtQueryId = new System.Windows.Forms.TextBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.lblObservation = new System.Windows.Forms.Label();
            this.txtObservation = new System.Windows.Forms.TextBox();
            this.btnRemoveConnection = new System.Windows.Forms.Button();
            this.btnAddConnection = new System.Windows.Forms.Button();
            this.lsbConnections = new System.Windows.Forms.ListBox();
            this.lblConnections = new System.Windows.Forms.Label();
            this.rtxtQuery = new System.Windows.Forms.RichTextBox();
            this.btnXlsxFormat = new System.Windows.Forms.Button();
            this.btnJsonFormat = new System.Windows.Forms.Button();
            this.btnXmlFormat = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtActive = new System.Windows.Forms.TextBox();
            this.lblActive = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPdfFormat = new System.Windows.Forms.Button();
            this.pcbCharge = new System.Windows.Forms.ProgressBar();
            this.chkFree = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQueryDescription
            // 
            this.txtQueryDescription.Location = new System.Drawing.Point(62, 25);
            this.txtQueryDescription.Name = "txtQueryDescription";
            this.txtQueryDescription.Size = new System.Drawing.Size(458, 20);
            this.txtQueryDescription.TabIndex = 0;
            // 
            // txtQueryId
            // 
            this.txtQueryId.Location = new System.Drawing.Point(11, 25);
            this.txtQueryId.Name = "txtQueryId";
            this.txtQueryId.Size = new System.Drawing.Size(45, 20);
            this.txtQueryId.TabIndex = 4;
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(12, 9);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(40, 13);
            this.lblQuery.TabIndex = 3;
            this.lblQuery.Text = "Querys";
            // 
            // lblObservation
            // 
            this.lblObservation.AutoSize = true;
            this.lblObservation.Location = new System.Drawing.Point(11, 47);
            this.lblObservation.Name = "lblObservation";
            this.lblObservation.Size = new System.Drawing.Size(65, 13);
            this.lblObservation.TabIndex = 7;
            this.lblObservation.Text = "Observação";
            // 
            // txtObservation
            // 
            this.txtObservation.Location = new System.Drawing.Point(11, 63);
            this.txtObservation.Multiline = true;
            this.txtObservation.Name = "txtObservation";
            this.txtObservation.Size = new System.Drawing.Size(509, 76);
            this.txtObservation.TabIndex = 1;
            // 
            // btnRemoveConnection
            // 
            this.btnRemoveConnection.Location = new System.Drawing.Point(831, 41);
            this.btnRemoveConnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveConnection.Name = "btnRemoveConnection";
            this.btnRemoveConnection.Size = new System.Drawing.Size(62, 19);
            this.btnRemoveConnection.TabIndex = 3;
            this.btnRemoveConnection.Text = "Remover";
            this.btnRemoveConnection.UseVisualStyleBackColor = true;
            // 
            // btnAddConnection
            // 
            this.btnAddConnection.Location = new System.Drawing.Point(831, 18);
            this.btnAddConnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddConnection.Name = "btnAddConnection";
            this.btnAddConnection.Size = new System.Drawing.Size(62, 19);
            this.btnAddConnection.TabIndex = 2;
            this.btnAddConnection.Text = "Adicionar";
            this.btnAddConnection.UseVisualStyleBackColor = true;
            // 
            // lsbConnections
            // 
            this.lsbConnections.FormattingEnabled = true;
            this.lsbConnections.Location = new System.Drawing.Point(525, 18);
            this.lsbConnections.Margin = new System.Windows.Forms.Padding(2);
            this.lsbConnections.Name = "lsbConnections";
            this.lsbConnections.Size = new System.Drawing.Size(302, 121);
            this.lsbConnections.TabIndex = 27;
            // 
            // lblConnections
            // 
            this.lblConnections.AutoSize = true;
            this.lblConnections.Location = new System.Drawing.Point(522, 2);
            this.lblConnections.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConnections.Name = "lblConnections";
            this.lblConnections.Size = new System.Drawing.Size(54, 13);
            this.lblConnections.TabIndex = 26;
            this.lblConnections.Text = "Conexões";
            // 
            // rtxtQuery
            // 
            this.rtxtQuery.Location = new System.Drawing.Point(11, 180);
            this.rtxtQuery.Name = "rtxtQuery";
            this.rtxtQuery.Size = new System.Drawing.Size(559, 328);
            this.rtxtQuery.TabIndex = 4;
            this.rtxtQuery.Text = "";
            // 
            // btnXlsxFormat
            // 
            this.btnXlsxFormat.Location = new System.Drawing.Point(463, 151);
            this.btnXlsxFormat.Name = "btnXlsxFormat";
            this.btnXlsxFormat.Size = new System.Drawing.Size(107, 23);
            this.btnXlsxFormat.TabIndex = 9;
            this.btnXlsxFormat.Text = "Visualizar XLSX";
            this.btnXlsxFormat.UseVisualStyleBackColor = true;
            // 
            // btnJsonFormat
            // 
            this.btnJsonFormat.Location = new System.Drawing.Point(124, 151);
            this.btnJsonFormat.Name = "btnJsonFormat";
            this.btnJsonFormat.Size = new System.Drawing.Size(107, 23);
            this.btnJsonFormat.TabIndex = 6;
            this.btnJsonFormat.Text = "Visualizar JSON";
            this.btnJsonFormat.UseVisualStyleBackColor = true;
            // 
            // btnXmlFormat
            // 
            this.btnXmlFormat.Location = new System.Drawing.Point(237, 151);
            this.btnXmlFormat.Name = "btnXmlFormat";
            this.btnXmlFormat.Size = new System.Drawing.Size(107, 23);
            this.btnXmlFormat.TabIndex = 7;
            this.btnXmlFormat.Text = "Visualizar XML";
            this.btnXmlFormat.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(11, 151);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(107, 23);
            this.btnExecute.TabIndex = 5;
            this.btnExecute.Text = "Executar (F5)";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(11, 514);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(559, 101);
            this.txtOutput.TabIndex = 34;
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(576, 180);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(848, 435);
            this.dgvData.TabIndex = 35;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1204, 621);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1317, 619);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtActive
            // 
            this.txtActive.Location = new System.Drawing.Point(576, 157);
            this.txtActive.Name = "txtActive";
            this.txtActive.Size = new System.Drawing.Size(251, 20);
            this.txtActive.TabIndex = 39;
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Location = new System.Drawing.Point(573, 141);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(37, 13);
            this.lblActive.TabIndex = 40;
            this.lblActive.Text = "Active";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(899, 219);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 19);
            this.button1.TabIndex = 24;
            this.button1.Text = "Adicionar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(1296, 158);
            this.btnPreviousPage.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(62, 19);
            this.btnPreviousPage.TabIndex = 41;
            this.btnPreviousPage.Text = "Previous";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(1362, 158);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(2);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(62, 19);
            this.btnNextPage.TabIndex = 42;
            this.btnNextPage.Text = "Next";
            this.btnNextPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(867, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 39);
            this.label1.TabIndex = 43;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnPdfFormat
            // 
            this.btnPdfFormat.Location = new System.Drawing.Point(350, 151);
            this.btnPdfFormat.Name = "btnPdfFormat";
            this.btnPdfFormat.Size = new System.Drawing.Size(107, 23);
            this.btnPdfFormat.TabIndex = 8;
            this.btnPdfFormat.Text = "Visualizar PDF";
            this.btnPdfFormat.UseVisualStyleBackColor = true;
            // 
            // pcbCharge
            // 
            this.pcbCharge.Location = new System.Drawing.Point(12, 621);
            this.pcbCharge.Name = "pcbCharge";
            this.pcbCharge.Size = new System.Drawing.Size(1187, 23);
            this.pcbCharge.TabIndex = 44;
            // 
            // chkFree
            // 
            this.chkFree.AutoSize = true;
            this.chkFree.Location = new System.Drawing.Point(831, 159);
            this.chkFree.Name = "chkFree";
            this.chkFree.Size = new System.Drawing.Size(120, 17);
            this.chkFree.TabIndex = 45;
            this.chkFree.Text = "Consulta sem limites";
            this.chkFree.UseVisualStyleBackColor = true;
            // 
            // frmQuery_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 646);
            this.Controls.Add(this.chkFree);
            this.Controls.Add(this.pcbCharge);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.txtActive);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnXmlFormat);
            this.Controls.Add(this.btnJsonFormat);
            this.Controls.Add(this.btnXlsxFormat);
            this.Controls.Add(this.btnPdfFormat);
            this.Controls.Add(this.rtxtQuery);
            this.Controls.Add(this.btnRemoveConnection);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddConnection);
            this.Controls.Add(this.lsbConnections);
            this.Controls.Add(this.lblConnections);
            this.Controls.Add(this.lblObservation);
            this.Controls.Add(this.txtObservation);
            this.Controls.Add(this.txtQueryDescription);
            this.Controls.Add(this.txtQueryId);
            this.Controls.Add(this.lblQuery);
            this.Name = "frmQuery_Information";
            this.Text = "frmQuerys_Information";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQueryDescription;
        private System.Windows.Forms.TextBox txtQueryId;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.Label lblObservation;
        private System.Windows.Forms.TextBox txtObservation;
        private System.Windows.Forms.Button btnRemoveConnection;
        private System.Windows.Forms.Button btnAddConnection;
        private System.Windows.Forms.ListBox lsbConnections;
        private System.Windows.Forms.Label lblConnections;
        private System.Windows.Forms.RichTextBox rtxtQuery;
        private System.Windows.Forms.Button btnXlsxFormat;
        private System.Windows.Forms.Button btnJsonFormat;
        private System.Windows.Forms.Button btnXmlFormat;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtActive;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPdfFormat;
        private System.Windows.Forms.ProgressBar pcbCharge;
        private System.Windows.Forms.CheckBox chkFree;
    }
}