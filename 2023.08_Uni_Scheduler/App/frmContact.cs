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
    public partial class frmContact : CustomDefaultForm
    {
        public frmContact()
        {
            InitializeComponent();
            ConfigureFormAttributes();
            ConfigureFormProperties();

            ConfigureFormEvents();
        }


        List<SchedulerApp_Contact> Contacts = new List<SchedulerApp_Contact>();

        /** Async Tasks **/
        private async Task<List<SchedulerApp_Contact>> getContactsAsync()
        {
            try
            {
                return await SchedulerApp_Contact.getAllToListAsync();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível carregar as conexões: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar as conexões: " + ex.Message);
                return null;
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
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmConnections_Load;
        }
        private async void frmConnections_Load(object sender, EventArgs e)
        {
            Contacts = await getContactsAsync();


            ConfigureDataGridViewAttributes();
            ConfigureDataGridViewProperties();
            ConfigureButtonProperties();


            ConfigureButtonEvents();
            ConfigureDataGridViewEvents();
            ConfigureTextBoxEvent();
        }

        /** Configure DataGridView **/
        private void ConfigureDataGridViewAttributes()
        {
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
        }
        private void ConfigureDataGridViewProperties()
        {
            dgvData.toDefault();
        }
        private void ConfigureDataGridViewEvents()
        {
            dgvData.DoubleClick += dgvData_DoubleClick;

        }
        private void addConnectionsToDataGridView(string description, DataGridView _dgv)
        {
            description = description.ToUpper();
            if (Contacts != null)
            {
                var select = from contact in Contacts.ToList()
                             where contact.id == description || contact.description.ToUpper().Contains(description) || contact.observation.ToUpper().Contains(description) || contact.mail.ToUpper().Contains(description)
                             select contact;
                _dgv.Invoke((Action)delegate
                {
                    if (_dgv.Columns.Count == 0)
                    {
                        _dgv.Columns.Add("id", "Id");
                        _dgv.Columns.Add("description", "Contact");
                        _dgv.Columns.Add("server", "Server");
                        _dgv.Columns.Add("mail", "E-mail");
                    }
                    _dgv.Rows.Clear();
                    foreach (var contact in select)
                    {
                        _dgv.Rows.Add(contact.id, contact.description, contact.observation, contact.mail);
                    }
                    _dgv.toDefault();
                    _dgv.Refresh();
                });
            }

        }
        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        /** Configure Buttons **/
        private void ConfigureButtonProperties()
        {
            btnClose.toDefaultCloseMenuButton();
        }
        private void ConfigureButtonEvents()
        {
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnSearch.Click += btnSearch_Click;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
            this.Cursor = Cursors.Default;
        }
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            frmContact_Information frmContact_Information = new frmContact_Information();
            frmContact_Information.insert = false;
            frmContact_Information.contact = Contacts.Where(conn => conn.id == dgvData.CurrentRow.Cells[0].Value.ToString()).FirstOrDefault();
            frmContact_Information.ShowDialog();
            Contacts = await getContactsAsync();
            txtSearch.Text = string.Empty;
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmContact_Information frmContact_Information = new frmContact_Information();
            frmContact_Information.insert = true;
            frmContact_Information.ShowDialog();
            txtSearch.Text = string.Empty;
            Contacts = await getContactsAsync();
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
        }

        /** Configure TextBox **/
        private void ConfigureTextBoxEvent()
        {
            txtSearch.KeyDown += txtSearch_KeyDown;
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

    }
}
