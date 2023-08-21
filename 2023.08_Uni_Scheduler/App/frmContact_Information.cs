using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmContact_Information : CustomDefaultForm
    {

        internal bool insert { get; set; } = true;
        internal SchedulerApp_Contact contact { get; set; } = new SchedulerApp_Contact();
        public frmContact_Information()
        {
            InitializeComponent();
            ConfigureFormAttributes();
            ConfigureFormProperties();


            ConfigureCheckBoxProperties();
            ConfigureTextBoxProperties();
            ConfigureButtonProperties();



            ConfigureFormEvents();
        }

        /** async tasks **/        
        private async Task saveAsync()
        {
            try
            {
                
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()) || string.IsNullOrEmpty(txtContactDescription.Text.Trim()))
                {
                    MessageBox.Show("Preencha as informações necessárias.");
                    return;
                }
                
                var contact = new SchedulerApp_Contact();                

                contact.id = txtContactId.Text;
                contact.description = txtContactDescription.Text;
                contact.observation = txtObservation.Text;
                contact.canrequest = Convert.ToInt16(chkCanRequest.Checked).ToString();
                contact.mail = txtEmail.Text;                


                var toInsert = new List<SchedulerApp_Contact>();
                toInsert.Add(contact);
                await SchedulerApp_Contact.insertAsync(toInsert);

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Processo cocluído com Sucesso!");
                this.Close();
            }

        }
        private async Task updateAsync()
        {
            try
            {
                var contact = new SchedulerApp_Contact();
                contact.id = txtContactId.Text;
                contact.description = txtContactDescription.Text;
                contact.observation = txtObservation.Text;
                contact.canrequest = Convert.ToInt16(chkCanRequest.Checked).ToString();
                contact.mail = txtEmail.Text;
                
                await SchedulerApp_Contact.updateAsync(contact);                

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Processo cocluído com Sucesso!");
                this.Close();
            }
        }

        /** Configure Form **/
        private void ConfigureFormAttributes()
        {
            this.Text = "Contatos";
        }
        private void ConfigureFormProperties()
        {
            this.ConfigureDefault();
            this.KeyPreview = true;
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmQuery_Information_Load;
        }
        private async void frmQuery_Information_Load(object sender, EventArgs e)
        {
            await ConfigureTextBoxAttributes();
            
            ConfigureButtonEvents();
        }

        /** Button Configuration **/
        private void ConfigureButtonProperties()
        {
            btnClose.toDefaultCloseButton();
        }
        private void ConfigureButtonEvents()
        {
            btnSave.Click += btnSave_Click;            
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (insert)
                await saveAsync();
            else
                await updateAsync();
        }

        /** TextBox Configuration **/
        private async Task ConfigureTextBoxAttributes()
        {
            if (insert)
            {
                txtContactId.Text = (await SchedulerApp_Contact.getNextCodeAsync()).ToString();
            }
            else
            {
                txtContactId.Text = contact.id;
                txtContactDescription.Text = contact.description;
                txtObservation.Text = contact.observation;
                chkCanRequest.Checked = Convert.ToBoolean(Convert.ToInt16(contact.canrequest));
                txtEmail.Text = contact.mail;                
            }

        }
        private void ConfigureTextBoxProperties()
        {
            txtContactId.ReadOnly = true;
            txtContactId.TabStop = false;

            txtContactDescription.TabIndex = 0;
            txtObservation.TabIndex = 1;
            txtEmail.TabIndex = 2;
        }


        /** CheckBox configuration **/
        private void ConfigureCheckBoxProperties()
        {
            chkCanRequest.TabIndex = 3;
        }
    }
}
