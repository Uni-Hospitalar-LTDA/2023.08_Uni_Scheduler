namespace _2023._08_Uni_Scheduler.App
{
    partial class frmSchedule_Information
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
            this.lblSchedule = new System.Windows.Forms.Label();
            this.txtScheduleId = new System.Windows.Forms.TextBox();
            this.txtScheduleDescription = new System.Windows.Forms.TextBox();
            this.txtObservation = new System.Windows.Forms.TextBox();
            this.lblObservation = new System.Windows.Forms.Label();
            this.mtxHour = new System.Windows.Forms.MaskedTextBox();
            this.lblHour = new System.Windows.Forms.Label();
            this.txtDaysOfMonth = new System.Windows.Forms.TextBox();
            this.lblDaysOfMonth = new System.Windows.Forms.Label();
            this.gpbDaysOfWeek = new System.Windows.Forms.GroupBox();
            this.chkDaysOfWeek = new System.Windows.Forms.CheckBox();
            this.chkSunday = new System.Windows.Forms.CheckBox();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.chkThursday = new System.Windows.Forms.CheckBox();
            this.chkWednesday = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.lblReports = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.lsbEmails = new System.Windows.Forms.ListBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRemoveReport = new System.Windows.Forms.Button();
            this.btnAddReport = new System.Windows.Forms.Button();
            this.btnRemoverEmail = new System.Windows.Forms.Button();
            this.btnAddEmail = new System.Windows.Forms.Button();
            this.lblValidationResult = new System.Windows.Forms.Label();
            this.gpbDaysOfWeek.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Location = new System.Drawing.Point(13, 9);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(48, 13);
            this.lblSchedule.TabIndex = 0;
            this.lblSchedule.Text = "Agenda*";
            // 
            // txtScheduleId
            // 
            this.txtScheduleId.Location = new System.Drawing.Point(12, 25);
            this.txtScheduleId.Name = "txtScheduleId";
            this.txtScheduleId.Size = new System.Drawing.Size(45, 20);
            this.txtScheduleId.TabIndex = 1;
            // 
            // txtScheduleDescription
            // 
            this.txtScheduleDescription.Location = new System.Drawing.Point(63, 25);
            this.txtScheduleDescription.Name = "txtScheduleDescription";
            this.txtScheduleDescription.Size = new System.Drawing.Size(345, 20);
            this.txtScheduleDescription.TabIndex = 0;
            // 
            // txtObservation
            // 
            this.txtObservation.Location = new System.Drawing.Point(12, 67);
            this.txtObservation.Multiline = true;
            this.txtObservation.Name = "txtObservation";
            this.txtObservation.Size = new System.Drawing.Size(437, 76);
            this.txtObservation.TabIndex = 3;
            // 
            // lblObservation
            // 
            this.lblObservation.AutoSize = true;
            this.lblObservation.Location = new System.Drawing.Point(12, 51);
            this.lblObservation.Name = "lblObservation";
            this.lblObservation.Size = new System.Drawing.Size(65, 13);
            this.lblObservation.TabIndex = 4;
            this.lblObservation.Text = "Observação";
            // 
            // mtxHour
            // 
            this.mtxHour.Location = new System.Drawing.Point(414, 25);
            this.mtxHour.Mask = "00:00";
            this.mtxHour.Name = "mtxHour";
            this.mtxHour.Size = new System.Drawing.Size(35, 20);
            this.mtxHour.TabIndex = 1;
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(411, 9);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(30, 13);
            this.lblHour.TabIndex = 6;
            this.lblHour.Text = "Hora";
            // 
            // txtDaysOfMonth
            // 
            this.txtDaysOfMonth.Location = new System.Drawing.Point(455, 25);
            this.txtDaysOfMonth.Name = "txtDaysOfMonth";
            this.txtDaysOfMonth.Size = new System.Drawing.Size(207, 20);
            this.txtDaysOfMonth.TabIndex = 2;
            // 
            // lblDaysOfMonth
            // 
            this.lblDaysOfMonth.AutoSize = true;
            this.lblDaysOfMonth.Location = new System.Drawing.Point(452, 9);
            this.lblDaysOfMonth.Name = "lblDaysOfMonth";
            this.lblDaysOfMonth.Size = new System.Drawing.Size(160, 13);
            this.lblDaysOfMonth.TabIndex = 8;
            this.lblDaysOfMonth.Text = "Dias do Mês (Separados por \",\")";
            // 
            // gpbDaysOfWeek
            // 
            this.gpbDaysOfWeek.Controls.Add(this.chkDaysOfWeek);
            this.gpbDaysOfWeek.Controls.Add(this.chkSunday);
            this.gpbDaysOfWeek.Controls.Add(this.chkFriday);
            this.gpbDaysOfWeek.Controls.Add(this.chkThursday);
            this.gpbDaysOfWeek.Controls.Add(this.chkWednesday);
            this.gpbDaysOfWeek.Controls.Add(this.chkTuesday);
            this.gpbDaysOfWeek.Controls.Add(this.chkMonday);
            this.gpbDaysOfWeek.Controls.Add(this.chkSaturday);
            this.gpbDaysOfWeek.Location = new System.Drawing.Point(486, 82);
            this.gpbDaysOfWeek.Margin = new System.Windows.Forms.Padding(2);
            this.gpbDaysOfWeek.Name = "gpbDaysOfWeek";
            this.gpbDaysOfWeek.Padding = new System.Windows.Forms.Padding(2);
            this.gpbDaysOfWeek.Size = new System.Drawing.Size(148, 163);
            this.gpbDaysOfWeek.TabIndex = 4;
            this.gpbDaysOfWeek.TabStop = false;
            this.gpbDaysOfWeek.Text = "Dias da Semana";
            // 
            // chkDaysOfWeek
            // 
            this.chkDaysOfWeek.AutoSize = true;
            this.chkDaysOfWeek.Location = new System.Drawing.Point(118, 0);
            this.chkDaysOfWeek.Margin = new System.Windows.Forms.Padding(2);
            this.chkDaysOfWeek.Name = "chkDaysOfWeek";
            this.chkDaysOfWeek.Size = new System.Drawing.Size(15, 14);
            this.chkDaysOfWeek.TabIndex = 0;
            this.chkDaysOfWeek.UseVisualStyleBackColor = true;
            // 
            // chkSunday
            // 
            this.chkSunday.AutoSize = true;
            this.chkSunday.Location = new System.Drawing.Point(4, 144);
            this.chkSunday.Margin = new System.Windows.Forms.Padding(2);
            this.chkSunday.Name = "chkSunday";
            this.chkSunday.Size = new System.Drawing.Size(68, 17);
            this.chkSunday.TabIndex = 7;
            this.chkSunday.Text = "Domingo";
            this.chkSunday.UseVisualStyleBackColor = true;
            // 
            // chkFriday
            // 
            this.chkFriday.AutoSize = true;
            this.chkFriday.Location = new System.Drawing.Point(4, 123);
            this.chkFriday.Margin = new System.Windows.Forms.Padding(2);
            this.chkFriday.Name = "chkFriday";
            this.chkFriday.Size = new System.Drawing.Size(79, 17);
            this.chkFriday.TabIndex = 6;
            this.chkFriday.Text = "Sexta-Feira";
            this.chkFriday.UseVisualStyleBackColor = true;
            // 
            // chkThursday
            // 
            this.chkThursday.AutoSize = true;
            this.chkThursday.Location = new System.Drawing.Point(4, 101);
            this.chkThursday.Margin = new System.Windows.Forms.Padding(2);
            this.chkThursday.Name = "chkThursday";
            this.chkThursday.Size = new System.Drawing.Size(83, 17);
            this.chkThursday.TabIndex = 5;
            this.chkThursday.Text = "Quinta-Feira";
            this.chkThursday.UseVisualStyleBackColor = true;
            // 
            // chkWednesday
            // 
            this.chkWednesday.AutoSize = true;
            this.chkWednesday.Location = new System.Drawing.Point(4, 80);
            this.chkWednesday.Margin = new System.Windows.Forms.Padding(2);
            this.chkWednesday.Name = "chkWednesday";
            this.chkWednesday.Size = new System.Drawing.Size(84, 17);
            this.chkWednesday.TabIndex = 4;
            this.chkWednesday.Text = "Quarta-Feira";
            this.chkWednesday.UseVisualStyleBackColor = true;
            // 
            // chkTuesday
            // 
            this.chkTuesday.AutoSize = true;
            this.chkTuesday.Location = new System.Drawing.Point(4, 59);
            this.chkTuesday.Margin = new System.Windows.Forms.Padding(2);
            this.chkTuesday.Name = "chkTuesday";
            this.chkTuesday.Size = new System.Drawing.Size(80, 17);
            this.chkTuesday.TabIndex = 3;
            this.chkTuesday.Text = "Terça-Feira";
            this.chkTuesday.UseVisualStyleBackColor = true;
            // 
            // chkMonday
            // 
            this.chkMonday.AutoSize = true;
            this.chkMonday.Location = new System.Drawing.Point(4, 38);
            this.chkMonday.Margin = new System.Windows.Forms.Padding(2);
            this.chkMonday.Name = "chkMonday";
            this.chkMonday.Size = new System.Drawing.Size(95, 17);
            this.chkMonday.TabIndex = 2;
            this.chkMonday.Text = "Segunda-Feira";
            this.chkMonday.UseVisualStyleBackColor = true;
            // 
            // chkSaturday
            // 
            this.chkSaturday.AutoSize = true;
            this.chkSaturday.Location = new System.Drawing.Point(4, 17);
            this.chkSaturday.Margin = new System.Windows.Forms.Padding(2);
            this.chkSaturday.Name = "chkSaturday";
            this.chkSaturday.Size = new System.Drawing.Size(63, 17);
            this.chkSaturday.TabIndex = 1;
            this.chkSaturday.Text = "Sábado";
            this.chkSaturday.UseVisualStyleBackColor = true;
            // 
            // lblReports
            // 
            this.lblReports.AutoSize = true;
            this.lblReports.Location = new System.Drawing.Point(12, 275);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new System.Drawing.Size(58, 13);
            this.lblReports.TabIndex = 10;
            this.lblReports.Text = "Relatórios*";
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 290);
            this.dgvData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(650, 186);
            this.dgvData.TabIndex = 11;
            // 
            // lsbEmails
            // 
            this.lsbEmails.FormattingEnabled = true;
            this.lsbEmails.Location = new System.Drawing.Point(15, 168);
            this.lsbEmails.Margin = new System.Windows.Forms.Padding(2);
            this.lsbEmails.Name = "lsbEmails";
            this.lsbEmails.Size = new System.Drawing.Size(434, 95);
            this.lsbEmails.TabIndex = 25;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(13, 152);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 13);
            this.lblEmail.TabIndex = 24;
            this.lblEmail.Text = "E-mail*";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(506, 481);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(587, 481);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnRemoveReport
            // 
            this.btnRemoveReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveReport.Location = new System.Drawing.Point(80, 480);
            this.btnRemoveReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveReport.Name = "btnRemoveReport";
            this.btnRemoveReport.Size = new System.Drawing.Size(66, 19);
            this.btnRemoveReport.TabIndex = 8;
            this.btnRemoveReport.Text = "Remover";
            this.btnRemoveReport.UseVisualStyleBackColor = true;
            // 
            // btnAddReport
            // 
            this.btnAddReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddReport.Location = new System.Drawing.Point(12, 480);
            this.btnAddReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddReport.Name = "btnAddReport";
            this.btnAddReport.Size = new System.Drawing.Size(66, 19);
            this.btnAddReport.TabIndex = 7;
            this.btnAddReport.Text = "Adicionar";
            this.btnAddReport.UseVisualStyleBackColor = true;
            // 
            // btnRemoverEmail
            // 
            this.btnRemoverEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoverEmail.Location = new System.Drawing.Point(383, 267);
            this.btnRemoverEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverEmail.Name = "btnRemoverEmail";
            this.btnRemoverEmail.Size = new System.Drawing.Size(66, 19);
            this.btnRemoverEmail.TabIndex = 6;
            this.btnRemoverEmail.Text = "Remover";
            this.btnRemoverEmail.UseVisualStyleBackColor = true;
            // 
            // btnAddEmail
            // 
            this.btnAddEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddEmail.Location = new System.Drawing.Point(315, 267);
            this.btnAddEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddEmail.Name = "btnAddEmail";
            this.btnAddEmail.Size = new System.Drawing.Size(66, 19);
            this.btnAddEmail.TabIndex = 5;
            this.btnAddEmail.Text = "Adicionar";
            this.btnAddEmail.UseVisualStyleBackColor = true;
            // 
            // lblValidationResult
            // 
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Location = new System.Drawing.Point(461, 48);
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(0, 13);
            this.lblValidationResult.TabIndex = 32;
            // 
            // frmSchedule_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 512);
            this.Controls.Add(this.lblValidationResult);
            this.Controls.Add(this.btnRemoverEmail);
            this.Controls.Add(this.btnAddEmail);
            this.Controls.Add(this.btnRemoveReport);
            this.Controls.Add(this.btnAddReport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lsbEmails);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.lblReports);
            this.Controls.Add(this.gpbDaysOfWeek);
            this.Controls.Add(this.lblDaysOfMonth);
            this.Controls.Add(this.txtDaysOfMonth);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.mtxHour);
            this.Controls.Add(this.lblObservation);
            this.Controls.Add(this.txtObservation);
            this.Controls.Add(this.txtScheduleDescription);
            this.Controls.Add(this.txtScheduleId);
            this.Controls.Add(this.lblSchedule);
            this.Name = "frmSchedule_Information";
            this.Text = "frmSchedule_Add";
            this.gpbDaysOfWeek.ResumeLayout(false);
            this.gpbDaysOfWeek.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.TextBox txtScheduleId;
        private System.Windows.Forms.TextBox txtScheduleDescription;
        private System.Windows.Forms.TextBox txtObservation;
        private System.Windows.Forms.Label lblObservation;
        private System.Windows.Forms.MaskedTextBox mtxHour;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.TextBox txtDaysOfMonth;
        private System.Windows.Forms.Label lblDaysOfMonth;
        private System.Windows.Forms.GroupBox gpbDaysOfWeek;
        private System.Windows.Forms.CheckBox chkDaysOfWeek;
        private System.Windows.Forms.CheckBox chkSunday;
        private System.Windows.Forms.CheckBox chkFriday;
        private System.Windows.Forms.CheckBox chkThursday;
        private System.Windows.Forms.CheckBox chkWednesday;
        private System.Windows.Forms.CheckBox chkTuesday;
        private System.Windows.Forms.CheckBox chkMonday;
        private System.Windows.Forms.CheckBox chkSaturday;
        private System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ListBox lsbEmails;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRemoveReport;
        private System.Windows.Forms.Button btnAddReport;
        private System.Windows.Forms.Button btnRemoverEmail;
        private System.Windows.Forms.Button btnAddEmail;
        private System.Windows.Forms.Label lblValidationResult;
    }
}