namespace _2023._08_Uni_Scheduler
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lsbLogs = new System.Windows.Forms.ListBox();
            this.btnSchedules = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnConnections = new System.Windows.Forms.Button();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.btnEmailAnalysis_Play = new System.Windows.Forms.Button();
            this.btnContacts = new System.Windows.Forms.Button();
            this.btnScheduledMail_Play = new System.Windows.Forms.Button();
            this.pcbEmailAnalysis = new System.Windows.Forms.ProgressBar();
            this.pcbScheduledMail = new System.Windows.Forms.ProgressBar();
            this.btnEmailAnalysis_Stop = new System.Windows.Forms.Button();
            this.btnScheduledMail_Stop = new System.Windows.Forms.Button();
            this.lblEmailAnalysis = new System.Windows.Forms.Label();
            this.lblScheduledMail = new System.Windows.Forms.Label();
            this.btnQuerys = new System.Windows.Forms.Button();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.pcbOperation = new System.Windows.Forms.ProgressBar();
            this.btnLogs = new System.Windows.Forms.Button();
            this.lsbSchedule = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold);
            this.lblApplicationName.Location = new System.Drawing.Point(209, 9);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(356, 42);
            this.lblApplicationName.TabIndex = 0;
            this.lblApplicationName.Text = "Atenas Data Bot v2.0";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1075, 473);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Sair";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // lsbLogs
            // 
            this.lsbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbLogs.FormattingEnabled = true;
            this.lsbLogs.Location = new System.Drawing.Point(12, 216);
            this.lsbLogs.Name = "lsbLogs";
            this.lsbLogs.Size = new System.Drawing.Size(1138, 251);
            this.lsbLogs.TabIndex = 2;
            // 
            // btnSchedules
            // 
            this.btnSchedules.Location = new System.Drawing.Point(13, 139);
            this.btnSchedules.Name = "btnSchedules";
            this.btnSchedules.Size = new System.Drawing.Size(119, 40);
            this.btnSchedules.TabIndex = 3;
            this.btnSchedules.Text = "Agendas";
            this.btnSchedules.UseVisualStyleBackColor = true;
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(263, 139);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(119, 40);
            this.btnReports.TabIndex = 4;
            this.btnReports.Text = "Relatórios";
            this.btnReports.UseVisualStyleBackColor = true;
            // 
            // btnConnections
            // 
            this.btnConnections.Location = new System.Drawing.Point(388, 139);
            this.btnConnections.Name = "btnConnections";
            this.btnConnections.Size = new System.Drawing.Size(119, 40);
            this.btnConnections.TabIndex = 6;
            this.btnConnections.Text = "Conexões";
            this.btnConnections.UseVisualStyleBackColor = true;
            // 
            // txtHour
            // 
            this.txtHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHour.Location = new System.Drawing.Point(295, 56);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(180, 49);
            this.txtHour.TabIndex = 7;
            // 
            // btnEmailAnalysis_Play
            // 
            this.btnEmailAnalysis_Play.Location = new System.Drawing.Point(590, 77);
            this.btnEmailAnalysis_Play.Name = "btnEmailAnalysis_Play";
            this.btnEmailAnalysis_Play.Size = new System.Drawing.Size(42, 25);
            this.btnEmailAnalysis_Play.TabIndex = 8;
            this.btnEmailAnalysis_Play.Text = "Play";
            this.btnEmailAnalysis_Play.UseVisualStyleBackColor = true;
            // 
            // btnContacts
            // 
            this.btnContacts.Location = new System.Drawing.Point(138, 139);
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(119, 40);
            this.btnContacts.TabIndex = 9;
            this.btnContacts.Text = "Contatos";
            this.btnContacts.UseVisualStyleBackColor = true;
            // 
            // btnScheduledMail_Play
            // 
            this.btnScheduledMail_Play.Location = new System.Drawing.Point(90, 77);
            this.btnScheduledMail_Play.Name = "btnScheduledMail_Play";
            this.btnScheduledMail_Play.Size = new System.Drawing.Size(42, 25);
            this.btnScheduledMail_Play.TabIndex = 10;
            this.btnScheduledMail_Play.Text = "Play";
            this.btnScheduledMail_Play.UseVisualStyleBackColor = true;
            // 
            // pcbEmailAnalysis
            // 
            this.pcbEmailAnalysis.Location = new System.Drawing.Point(513, 108);
            this.pcbEmailAnalysis.Name = "pcbEmailAnalysis";
            this.pcbEmailAnalysis.Size = new System.Drawing.Size(244, 25);
            this.pcbEmailAnalysis.TabIndex = 11;
            // 
            // pcbScheduledMail
            // 
            this.pcbScheduledMail.Location = new System.Drawing.Point(13, 108);
            this.pcbScheduledMail.Name = "pcbScheduledMail";
            this.pcbScheduledMail.Size = new System.Drawing.Size(244, 25);
            this.pcbScheduledMail.TabIndex = 12;
            // 
            // btnEmailAnalysis_Stop
            // 
            this.btnEmailAnalysis_Stop.Location = new System.Drawing.Point(638, 77);
            this.btnEmailAnalysis_Stop.Name = "btnEmailAnalysis_Stop";
            this.btnEmailAnalysis_Stop.Size = new System.Drawing.Size(42, 25);
            this.btnEmailAnalysis_Stop.TabIndex = 13;
            this.btnEmailAnalysis_Stop.Text = "Stop";
            this.btnEmailAnalysis_Stop.UseVisualStyleBackColor = true;
            // 
            // btnScheduledMail_Stop
            // 
            this.btnScheduledMail_Stop.Location = new System.Drawing.Point(138, 77);
            this.btnScheduledMail_Stop.Name = "btnScheduledMail_Stop";
            this.btnScheduledMail_Stop.Size = new System.Drawing.Size(42, 25);
            this.btnScheduledMail_Stop.TabIndex = 14;
            this.btnScheduledMail_Stop.Text = "Stop";
            this.btnScheduledMail_Stop.UseVisualStyleBackColor = true;
            // 
            // lblEmailAnalysis
            // 
            this.lblEmailAnalysis.AutoSize = true;
            this.lblEmailAnalysis.Location = new System.Drawing.Point(524, 61);
            this.lblEmailAnalysis.Name = "lblEmailAnalysis";
            this.lblEmailAnalysis.Size = new System.Drawing.Size(221, 13);
            this.lblEmailAnalysis.TabIndex = 15;
            this.lblEmailAnalysis.Text = "Análise Automática de Solicitações por E-mail";
            // 
            // lblScheduledMail
            // 
            this.lblScheduledMail.AutoSize = true;
            this.lblScheduledMail.Location = new System.Drawing.Point(36, 61);
            this.lblScheduledMail.Name = "lblScheduledMail";
            this.lblScheduledMail.Size = new System.Drawing.Size(200, 13);
            this.lblScheduledMail.TabIndex = 16;
            this.lblScheduledMail.Text = "Envio Agendado de Relatórios por E-mail";
            // 
            // btnQuerys
            // 
            this.btnQuerys.Location = new System.Drawing.Point(513, 139);
            this.btnQuerys.Name = "btnQuerys";
            this.btnQuerys.Size = new System.Drawing.Size(119, 40);
            this.btnQuerys.TabIndex = 19;
            this.btnQuerys.Text = "Querys";
            this.btnQuerys.UseVisualStyleBackColor = true;
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(638, 139);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(119, 40);
            this.btnGenerator.TabIndex = 20;
            this.btnGenerator.Text = "Gerador";
            this.btnGenerator.UseVisualStyleBackColor = true;
            // 
            // pcbOperation
            // 
            this.pcbOperation.Location = new System.Drawing.Point(13, 185);
            this.pcbOperation.Name = "pcbOperation";
            this.pcbOperation.Size = new System.Drawing.Size(744, 25);
            this.pcbOperation.TabIndex = 21;
            // 
            // btnLogs
            // 
            this.btnLogs.Location = new System.Drawing.Point(13, 473);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(97, 23);
            this.btnLogs.TabIndex = 22;
            this.btnLogs.Text = "Exportar Logs";
            this.btnLogs.UseVisualStyleBackColor = true;
            // 
            // lsbSchedule
            // 
            this.lsbSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbSchedule.FormattingEnabled = true;
            this.lsbSchedule.Location = new System.Drawing.Point(763, 12);
            this.lsbSchedule.Name = "lsbSchedule";
            this.lsbSchedule.Size = new System.Drawing.Size(387, 199);
            this.lsbSchedule.TabIndex = 23;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 504);
            this.Controls.Add(this.lsbSchedule);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.pcbOperation);
            this.Controls.Add(this.btnGenerator);
            this.Controls.Add(this.btnQuerys);
            this.Controls.Add(this.lblScheduledMail);
            this.Controls.Add(this.lblEmailAnalysis);
            this.Controls.Add(this.btnScheduledMail_Stop);
            this.Controls.Add(this.btnEmailAnalysis_Stop);
            this.Controls.Add(this.pcbScheduledMail);
            this.Controls.Add(this.pcbEmailAnalysis);
            this.Controls.Add(this.btnScheduledMail_Play);
            this.Controls.Add(this.btnContacts);
            this.Controls.Add(this.btnEmailAnalysis_Play);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.btnConnections);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnSchedules);
            this.Controls.Add(this.lsbLogs);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblApplicationName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Atenas Data Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox lsbLogs;
        private System.Windows.Forms.Button btnSchedules;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnConnections;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Button btnEmailAnalysis_Play;
        private System.Windows.Forms.Button btnContacts;
        private System.Windows.Forms.Button btnScheduledMail_Play;
        private System.Windows.Forms.ProgressBar pcbEmailAnalysis;
        private System.Windows.Forms.ProgressBar pcbScheduledMail;
        private System.Windows.Forms.Button btnEmailAnalysis_Stop;
        private System.Windows.Forms.Button btnScheduledMail_Stop;
        private System.Windows.Forms.Label lblEmailAnalysis;
        private System.Windows.Forms.Label lblScheduledMail;
        private System.Windows.Forms.Button btnQuerys;
        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.ProgressBar pcbOperation;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.ListBox lsbSchedule;
    }
}

