using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App.Generic_Screen
{
    public partial class frmGenericChoice : CustomDefaultForm
    {
        public frmGenericChoice()
        {
            InitializeComponent();

            ConfigureFormAttributes();
            ConfigureFormProperties();
            
            ConfigureButtonProperties();
            ConfigureCheckedListBoxProperties();


            ConfigureFormEvents();
        }
        /** Configure Form**/
        private void ConfigureFormAttributes()
        {
            this.Text = "Escolha os registros desejados";
        }
        private void ConfigureFormProperties()
        {
            this.ConfigureDefault();
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmGenericChoice_Load;
        }
        private void frmGenericChoice_Load(object sender, EventArgs e)
        {
            int width = 0;
            foreach (var i in clbGenericItems.Items)
            {
                if (i.ToString().Length > 0)
                {
                    width = i.ToString().Length;
                }
            }
            this.MaximumSize = new Size(this.Width,this.Height);
            this.MinimumSize = new Size(width + 300, width + 300);


            this.Width = (width+350);
            this.Height = (width+350);
            


        }

        /** Button Configuration **/
        private void ConfigureButtonProperties()
        {
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.toDefaultCloseMenuButton();
        }

        /** ListBox Configuration **/
        private void ConfigureCheckedListBoxProperties()
        {
            clbGenericItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }
        

        
    }
}
