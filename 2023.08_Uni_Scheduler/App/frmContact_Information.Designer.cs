namespace _2023._08_Uni_Scheduler.App
{
    partial class frmContact_Information
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
            this.chkCanRequest = new System.Windows.Forms.CheckBox();
            this.lblObservation = new System.Windows.Forms.Label();
            this.txtObservation = new System.Windows.Forms.TextBox();
            this.txtContactDescription = new System.Windows.Forms.TextBox();
            this.txtContactId = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkCanRequest
            // 
            this.chkCanRequest.AutoSize = true;
            this.chkCanRequest.Location = new System.Drawing.Point(306, 4);
            this.chkCanRequest.Margin = new System.Windows.Forms.Padding(2);
            this.chkCanRequest.Name = "chkCanRequest";
            this.chkCanRequest.Size = new System.Drawing.Size(143, 17);
            this.chkCanRequest.TabIndex = 16;
            this.chkCanRequest.Text = "Pode realizar requisições";
            this.chkCanRequest.UseVisualStyleBackColor = true;
            // 
            // lblObservation
            // 
            this.lblObservation.AutoSize = true;
            this.lblObservation.Location = new System.Drawing.Point(12, 48);
            this.lblObservation.Name = "lblObservation";
            this.lblObservation.Size = new System.Drawing.Size(65, 13);
            this.lblObservation.TabIndex = 15;
            this.lblObservation.Text = "Observação";
            // 
            // txtObservation
            // 
            this.txtObservation.Location = new System.Drawing.Point(12, 64);
            this.txtObservation.Multiline = true;
            this.txtObservation.Name = "txtObservation";
            this.txtObservation.Size = new System.Drawing.Size(437, 76);
            this.txtObservation.TabIndex = 14;
            // 
            // txtContactDescription
            // 
            this.txtContactDescription.Location = new System.Drawing.Point(62, 25);
            this.txtContactDescription.Name = "txtContactDescription";
            this.txtContactDescription.Size = new System.Drawing.Size(387, 20);
            this.txtContactDescription.TabIndex = 13;
            // 
            // txtContactId
            // 
            this.txtContactId.Location = new System.Drawing.Point(11, 25);
            this.txtContactId.Name = "txtContactId";
            this.txtContactId.Size = new System.Drawing.Size(45, 20);
            this.txtContactId.TabIndex = 12;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(12, 9);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(53, 13);
            this.lblContact.TabIndex = 11;
            this.lblContact.Text = "Relatório*";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(15, 159);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(434, 20);
            this.txtEmail.TabIndex = 18;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 143);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 13);
            this.lblEmail.TabIndex = 17;
            this.lblEmail.Text = "E-mail*";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(374, 185);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(293, 185);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmContact_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 216);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.chkCanRequest);
            this.Controls.Add(this.lblObservation);
            this.Controls.Add(this.txtObservation);
            this.Controls.Add(this.txtContactDescription);
            this.Controls.Add(this.txtContactId);
            this.Controls.Add(this.lblContact);
            this.Name = "frmContact_Information";
            this.Text = "frmContact_Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCanRequest;
        private System.Windows.Forms.Label lblObservation;
        private System.Windows.Forms.TextBox txtObservation;
        private System.Windows.Forms.TextBox txtContactDescription;
        private System.Windows.Forms.TextBox txtContactId;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}