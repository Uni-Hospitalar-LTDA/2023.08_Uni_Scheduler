using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmConnections : CustomDefaultForm
    {
        public frmConnections()
        {
            InitializeComponent();

            ConfigureFormAttributes();
            ConfigureFormProperties();
            ConfigureFormEvents();
        }
        
        List<SchedulerApp_Connection> Connections = new List<SchedulerApp_Connection>();

        /** Async Tasks **/
        private async Task<List<SchedulerApp_Connection>> getConnectionsAsync()
        {
            try
            {
                return await SchedulerApp_Connection.getAllToListAsync();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Não foi possível carregar as conexões: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar as conexões: "+ex.Message);
                return null;
            }
        }

        /** Sync Methods **/
        
         
        /** Configure Form **/
        private void ConfigureFormAttributes()
        {
            this.Text = "Conexões com o Banco de Dados";
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
            Connections = await getConnectionsAsync();

            
            ConfigureDataGridViewAttributes();
            ConfigureDataGridViewProperties();
            ConfigureButtonProperties();


            ConfigureButtonEvents();
            ConfigureDataGridViewEvents();
            ConfigureTextBoxEvent();
        }

        /** Configure DataGridView **/
        private void  ConfigureDataGridViewAttributes()
        {
            addConnectionsToDataGridView(txtSearch.Text,dgvData);
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
            var select = from conn in Connections.ToList()
                         where conn.id == description || conn.description.ToUpper().Contains(description) || conn.observation.ToUpper().Contains(description)
                         select conn;
            _dgv.Invoke((Action)delegate 
            { 
                if (_dgv.Columns.Count == 0)
                {
                    _dgv.Columns.Add("connectionId", "Id");
                    _dgv.Columns.Add("connectionDescription", "Connection");
                    _dgv.Columns.Add("server", "Server");
                }
                _dgv.Rows.Clear();
                foreach (var conn in select)
                {
                    _dgv.Rows.Add(conn.id, conn.description, conn.server);
                }
                _dgv.toDefault();
                _dgv.Refresh();
            });


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
            this.Cursor = Cursors .WaitCursor;
            addConnectionsToDataGridView(txtSearch.Text,dgvData);
            this.Cursor = Cursors.Default;
        }
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            frmConnection_Information frmConnection_Information = new frmConnection_Information();
            frmConnection_Information.insert = false;
            frmConnection_Information.connection = Connections.Where(conn=> conn.id == dgvData.CurrentRow.Cells[0].Value.ToString()).FirstOrDefault();
            frmConnection_Information.ShowDialog();
            Connections = await getConnectionsAsync();
            txtSearch.Text = string.Empty;
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmConnection_Information frmConnection_Information = new frmConnection_Information();
            frmConnection_Information.insert = true;
            frmConnection_Information.ShowDialog();
            txtSearch.Text = string.Empty;
            Connections = await getConnectionsAsync();            
            addConnectionsToDataGridView(txtSearch.Text,dgvData);
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
