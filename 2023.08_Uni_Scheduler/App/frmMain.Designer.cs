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
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.lsbLogs = new System.Windows.Forms.ListBox();
            this.lblEmailAnalysis = new System.Windows.Forms.Label();
            this.lblScheduledMail = new System.Windows.Forms.Label();
            this.pcbOperation = new System.Windows.Forms.ProgressBar();
            this.lsbSchedule = new System.Windows.Forms.ListBox();
            this.pcbEmailAnalysis = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.btnEmailAnalysis_Play = new Guna.UI2.WinForms.Guna2Button();
            this.btnEmailAnalysis_Stop = new Guna.UI2.WinForms.Guna2Button();
            this.btnScheduledMail_Play = new Guna.UI2.WinForms.Guna2Button();
            this.btnScheduledMail_Stop = new Guna.UI2.WinForms.Guna2Button();
            this.pcbScheduledMail = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.pcbRobot = new System.Windows.Forms.PictureBox();
            this.txtHour = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnConnections = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnSchedules = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnContacts = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnReports = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnQuerys = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnGenerator = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnLogs = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnExit = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRobot)).BeginInit();
            this.SuspendLayout();
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold);
            this.lblApplicationName.Location = new System.Drawing.Point(206, 9);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(356, 42);
            this.lblApplicationName.TabIndex = 0;
            this.lblApplicationName.Text = "Atenas Data Bot v2.0";
            // 
            // lsbLogs
            // 
            this.lsbLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsbLogs.FormattingEnabled = true;
            this.lsbLogs.Location = new System.Drawing.Point(12, 216);
            this.lsbLogs.Name = "lsbLogs";
            this.lsbLogs.Size = new System.Drawing.Size(1138, 249);
            this.lsbLogs.TabIndex = 2;
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
            // pcbOperation
            // 
            this.pcbOperation.Location = new System.Drawing.Point(13, 185);
            this.pcbOperation.Name = "pcbOperation";
            this.pcbOperation.Size = new System.Drawing.Size(744, 25);
            this.pcbOperation.TabIndex = 21;
            // 
            // lsbSchedule
            // 
            this.lsbSchedule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsbSchedule.FormattingEnabled = true;
            this.lsbSchedule.Location = new System.Drawing.Point(763, 12);
            this.lsbSchedule.Name = "lsbSchedule";
            this.lsbSchedule.Size = new System.Drawing.Size(387, 197);
            this.lsbSchedule.TabIndex = 23;
            // 
            // pcbEmailAnalysis
            // 
            this.pcbEmailAnalysis.BorderRadius = 3;
            this.pcbEmailAnalysis.Location = new System.Drawing.Point(513, 108);
            this.pcbEmailAnalysis.Name = "pcbEmailAnalysis";
            this.pcbEmailAnalysis.ProgressColor = System.Drawing.Color.Green;
            this.pcbEmailAnalysis.ProgressColor2 = System.Drawing.Color.SpringGreen;
            this.pcbEmailAnalysis.Size = new System.Drawing.Size(239, 25);
            this.pcbEmailAnalysis.TabIndex = 25;
            this.pcbEmailAnalysis.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnEmailAnalysis_Play
            // 
            this.btnEmailAnalysis_Play.BorderRadius = 3;
            this.btnEmailAnalysis_Play.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEmailAnalysis_Play.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEmailAnalysis_Play.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEmailAnalysis_Play.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEmailAnalysis_Play.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(184)))), ((int)(((byte)(92)))));
            this.btnEmailAnalysis_Play.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmailAnalysis_Play.ForeColor = System.Drawing.Color.White;
            this.btnEmailAnalysis_Play.Location = new System.Drawing.Point(580, 77);
            this.btnEmailAnalysis_Play.Name = "btnEmailAnalysis_Play";
            this.btnEmailAnalysis_Play.Size = new System.Drawing.Size(52, 24);
            this.btnEmailAnalysis_Play.TabIndex = 27;
            this.btnEmailAnalysis_Play.Text = "Play";
            // 
            // btnEmailAnalysis_Stop
            // 
            this.btnEmailAnalysis_Stop.BorderRadius = 3;
            this.btnEmailAnalysis_Stop.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEmailAnalysis_Stop.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEmailAnalysis_Stop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEmailAnalysis_Stop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEmailAnalysis_Stop.FillColor = System.Drawing.Color.LightCoral;
            this.btnEmailAnalysis_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmailAnalysis_Stop.ForeColor = System.Drawing.Color.White;
            this.btnEmailAnalysis_Stop.Location = new System.Drawing.Point(638, 77);
            this.btnEmailAnalysis_Stop.Name = "btnEmailAnalysis_Stop";
            this.btnEmailAnalysis_Stop.Size = new System.Drawing.Size(52, 24);
            this.btnEmailAnalysis_Stop.TabIndex = 28;
            this.btnEmailAnalysis_Stop.Text = "Stop";
            // 
            // btnScheduledMail_Play
            // 
            this.btnScheduledMail_Play.BorderRadius = 3;
            this.btnScheduledMail_Play.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnScheduledMail_Play.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnScheduledMail_Play.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnScheduledMail_Play.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnScheduledMail_Play.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(184)))), ((int)(((byte)(92)))));
            this.btnScheduledMail_Play.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnScheduledMail_Play.ForeColor = System.Drawing.Color.White;
            this.btnScheduledMail_Play.Location = new System.Drawing.Point(80, 77);
            this.btnScheduledMail_Play.Name = "btnScheduledMail_Play";
            this.btnScheduledMail_Play.Size = new System.Drawing.Size(52, 24);
            this.btnScheduledMail_Play.TabIndex = 29;
            this.btnScheduledMail_Play.Text = "Play";
            // 
            // btnScheduledMail_Stop
            // 
            this.btnScheduledMail_Stop.BorderRadius = 3;
            this.btnScheduledMail_Stop.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnScheduledMail_Stop.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnScheduledMail_Stop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnScheduledMail_Stop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnScheduledMail_Stop.FillColor = System.Drawing.Color.LightCoral;
            this.btnScheduledMail_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnScheduledMail_Stop.ForeColor = System.Drawing.Color.White;
            this.btnScheduledMail_Stop.Location = new System.Drawing.Point(138, 77);
            this.btnScheduledMail_Stop.Name = "btnScheduledMail_Stop";
            this.btnScheduledMail_Stop.Size = new System.Drawing.Size(52, 24);
            this.btnScheduledMail_Stop.TabIndex = 30;
            this.btnScheduledMail_Stop.Text = "Stop";
            // 
            // pcbScheduledMail
            // 
            this.pcbScheduledMail.BorderRadius = 3;
            this.pcbScheduledMail.Location = new System.Drawing.Point(13, 108);
            this.pcbScheduledMail.Name = "pcbScheduledMail";
            this.pcbScheduledMail.ProgressColor = System.Drawing.Color.Green;
            this.pcbScheduledMail.ProgressColor2 = System.Drawing.Color.SpringGreen;
            this.pcbScheduledMail.Size = new System.Drawing.Size(239, 25);
            this.pcbScheduledMail.TabIndex = 31;
            this.pcbScheduledMail.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // pcbRobot
            // 
            this.pcbRobot.Image = global::_2023._08_Uni_Scheduler.Properties.Resources.giphy;
            this.pcbRobot.Location = new System.Drawing.Point(918, 235);
            this.pcbRobot.Name = "pcbRobot";
            this.pcbRobot.Size = new System.Drawing.Size(221, 204);
            this.pcbRobot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbRobot.TabIndex = 40;
            this.pcbRobot.TabStop = false;
            this.pcbRobot.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtHour
            // 
            this.txtHour.Animated = true;
            this.txtHour.BorderColor = System.Drawing.Color.Black;
            this.txtHour.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHour.DefaultText = "";
            this.txtHour.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtHour.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtHour.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHour.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHour.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHour.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHour.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHour.IconLeft = global::_2023._08_Uni_Scheduler.Properties.Resources.relogio_de_parede;
            this.txtHour.Location = new System.Drawing.Point(263, 61);
            this.txtHour.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.txtHour.Name = "txtHour";
            this.txtHour.PasswordChar = '\0';
            this.txtHour.PlaceholderText = "";
            this.txtHour.SelectedText = "";
            this.txtHour.Size = new System.Drawing.Size(244, 41);
            this.txtHour.TabIndex = 26;
            // 
            // btnConnections
            // 
            this.btnConnections.Animated = true;
            this.btnConnections.BorderRadius = 3;
            this.btnConnections.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConnections.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConnections.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConnections.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConnections.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConnections.FillColor = System.Drawing.Color.DarkGray;
            this.btnConnections.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnConnections.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConnections.ForeColor = System.Drawing.Color.White;
            this.btnConnections.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnConnections.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnConnections.Location = new System.Drawing.Point(263, 139);
            this.btnConnections.Name = "btnConnections";
            this.btnConnections.Size = new System.Drawing.Size(119, 40);
            this.btnConnections.TabIndex = 43;
            this.btnConnections.Text = "Conexões";
            // 
            // btnSchedules
            // 
            this.btnSchedules.Animated = true;
            this.btnSchedules.BorderRadius = 3;
            this.btnSchedules.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSchedules.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSchedules.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSchedules.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSchedules.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSchedules.FillColor = System.Drawing.Color.DarkGray;
            this.btnSchedules.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnSchedules.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSchedules.ForeColor = System.Drawing.Color.White;
            this.btnSchedules.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnSchedules.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnSchedules.Location = new System.Drawing.Point(13, 139);
            this.btnSchedules.Name = "btnSchedules";
            this.btnSchedules.Size = new System.Drawing.Size(119, 40);
            this.btnSchedules.TabIndex = 44;
            this.btnSchedules.Text = "Agendas";
            // 
            // btnContacts
            // 
            this.btnContacts.Animated = true;
            this.btnContacts.BorderRadius = 3;
            this.btnContacts.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnContacts.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnContacts.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnContacts.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnContacts.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnContacts.FillColor = System.Drawing.Color.DarkGray;
            this.btnContacts.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnContacts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnContacts.ForeColor = System.Drawing.Color.White;
            this.btnContacts.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnContacts.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnContacts.Location = new System.Drawing.Point(138, 139);
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(119, 40);
            this.btnContacts.TabIndex = 45;
            this.btnContacts.Text = "Contatos";
            // 
            // btnReports
            // 
            this.btnReports.Animated = true;
            this.btnReports.BorderRadius = 3;
            this.btnReports.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReports.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReports.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReports.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReports.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReports.FillColor = System.Drawing.Color.DarkGray;
            this.btnReports.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnReports.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnReports.Location = new System.Drawing.Point(388, 139);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(119, 40);
            this.btnReports.TabIndex = 46;
            this.btnReports.Text = "Relatórios";
            // 
            // btnQuerys
            // 
            this.btnQuerys.Animated = true;
            this.btnQuerys.BorderRadius = 3;
            this.btnQuerys.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuerys.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuerys.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuerys.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuerys.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuerys.FillColor = System.Drawing.Color.DarkGray;
            this.btnQuerys.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnQuerys.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuerys.ForeColor = System.Drawing.Color.White;
            this.btnQuerys.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnQuerys.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnQuerys.Location = new System.Drawing.Point(513, 139);
            this.btnQuerys.Name = "btnQuerys";
            this.btnQuerys.Size = new System.Drawing.Size(119, 40);
            this.btnQuerys.TabIndex = 47;
            this.btnQuerys.Text = "Querys";
            // 
            // btnGenerator
            // 
            this.btnGenerator.Animated = true;
            this.btnGenerator.BorderRadius = 3;
            this.btnGenerator.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerator.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerator.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGenerator.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGenerator.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGenerator.FillColor = System.Drawing.Color.DarkGray;
            this.btnGenerator.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnGenerator.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGenerator.ForeColor = System.Drawing.Color.White;
            this.btnGenerator.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnGenerator.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.btnGenerator.Location = new System.Drawing.Point(638, 139);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(119, 40);
            this.btnGenerator.TabIndex = 48;
            this.btnGenerator.Text = "Gerador";
            // 
            // btnLogs
            // 
            this.btnLogs.Animated = true;
            this.btnLogs.BorderRadius = 5;
            this.btnLogs.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogs.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogs.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogs.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogs.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogs.FillColor = System.Drawing.Color.DarkGray;
            this.btnLogs.FillColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.btnLogs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogs.ForeColor = System.Drawing.Color.White;
            this.btnLogs.Location = new System.Drawing.Point(13, 474);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(97, 22);
            this.btnLogs.TabIndex = 49;
            this.btnLogs.Text = "Exportar Logs";
            // 
            // btnExit
            // 
            this.btnExit.BorderRadius = 5;
            this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExit.FillColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnExit.FillColor2 = System.Drawing.Color.Firebrick;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(1075, 474);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 22);
            this.btnExit.TabIndex = 50;
            this.btnExit.Text = "Sair";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 504);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.btnGenerator);
            this.Controls.Add(this.btnQuerys);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnContacts);
            this.Controls.Add(this.btnSchedules);
            this.Controls.Add(this.btnConnections);
            this.Controls.Add(this.pcbRobot);
            this.Controls.Add(this.pcbScheduledMail);
            this.Controls.Add(this.btnScheduledMail_Stop);
            this.Controls.Add(this.btnScheduledMail_Play);
            this.Controls.Add(this.btnEmailAnalysis_Stop);
            this.Controls.Add(this.btnEmailAnalysis_Play);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.pcbEmailAnalysis);
            this.Controls.Add(this.lsbSchedule);
            this.Controls.Add(this.pcbOperation);
            this.Controls.Add(this.lblScheduledMail);
            this.Controls.Add(this.lblEmailAnalysis);
            this.Controls.Add(this.lsbLogs);
            this.Controls.Add(this.lblApplicationName);
            this.Name = "frmMain";
            this.Text = "Atenas Data Bot";
            ((System.ComponentModel.ISupportInitialize)(this.pcbRobot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.ListBox lsbLogs;
        private System.Windows.Forms.Label lblEmailAnalysis;
        private System.Windows.Forms.Label lblScheduledMail;
        private System.Windows.Forms.ProgressBar pcbOperation;
        private System.Windows.Forms.ListBox lsbSchedule;
        private Guna.UI2.WinForms.Guna2ProgressBar pcbEmailAnalysis;
        private Guna.UI2.WinForms.Guna2TextBox txtHour;
        private Guna.UI2.WinForms.Guna2Button btnEmailAnalysis_Play;
        private Guna.UI2.WinForms.Guna2Button btnEmailAnalysis_Stop;
        private Guna.UI2.WinForms.Guna2Button btnScheduledMail_Play;
        private Guna.UI2.WinForms.Guna2Button btnScheduledMail_Stop;
        private Guna.UI2.WinForms.Guna2ProgressBar pcbScheduledMail;
        private System.Windows.Forms.PictureBox pcbRobot;
        private Guna.UI2.WinForms.Guna2GradientButton btnConnections;
        private Guna.UI2.WinForms.Guna2GradientButton btnSchedules;
        private Guna.UI2.WinForms.Guna2GradientButton btnContacts;
        private Guna.UI2.WinForms.Guna2GradientButton btnReports;
        private Guna.UI2.WinForms.Guna2GradientButton btnQuerys;
        private Guna.UI2.WinForms.Guna2GradientButton btnGenerator;
        private Guna.UI2.WinForms.Guna2GradientButton btnLogs;
        private Guna.UI2.WinForms.Guna2GradientButton btnExit;
    }
}

