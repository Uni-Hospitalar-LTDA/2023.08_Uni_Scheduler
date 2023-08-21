namespace _2023._08_Uni_Scheduler.App.Generic_Screen
{
    partial class frmGenericChoice
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
            this.clbGenericItems = new System.Windows.Forms.CheckedListBox();
            this.lblGenericItems = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbGenericItems
            // 
            this.clbGenericItems.FormattingEnabled = true;
            this.clbGenericItems.Location = new System.Drawing.Point(15, 25);
            this.clbGenericItems.Name = "clbGenericItems";
            this.clbGenericItems.Size = new System.Drawing.Size(975, 364);
            this.clbGenericItems.TabIndex = 0;
            // 
            // lblGenericItems
            // 
            this.lblGenericItems.AutoSize = true;
            this.lblGenericItems.Location = new System.Drawing.Point(12, 9);
            this.lblGenericItems.Name = "lblGenericItems";
            this.lblGenericItems.Size = new System.Drawing.Size(152, 13);
            this.lblGenericItems.TabIndex = 1;
            this.lblGenericItems.Text = "Escolha os registros desejados";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(915, 395);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmGenericChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 423);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblGenericItems);
            this.Controls.Add(this.clbGenericItems);
            this.Name = "frmGenericChoice";
            this.Text = "frmGenericChoice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblGenericItems;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.CheckedListBox clbGenericItems;
    }
}