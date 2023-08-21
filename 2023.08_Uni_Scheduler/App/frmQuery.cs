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
    public partial class frmQuery : CustomDefaultForm
    {
        public frmQuery()
        {
            InitializeComponent();
            ConfigureFormAttributes();
            ConfigureFormProperties();
            ConfigureFormEvents();
        }

        List<SchedulerApp_Query> Queries = new List<SchedulerApp_Query>();

        /** Async Tasks **/
        private async Task<List<SchedulerApp_Query>> getQueriesAsync()
        {
            try
            {
                return await SchedulerApp_Query.getAllToListAsync();
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
            this.Text = "Consultas";
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
            Queries = await getQueriesAsync();


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
            var select = from quer in Queries.ToList()
                         where quer.id == description || quer.description.ToUpper().Contains(description) || quer.observation.ToUpper().Contains(description)
                         select quer;
            _dgv.Invoke((Action)delegate
            {
                if (_dgv.Columns.Count == 0)
                {
                    _dgv.Columns.Add("queryId", "Id");
                    _dgv.Columns.Add("queryDescription", "Query");
                    _dgv.Columns.Add("observation", "Observation");
                }
                _dgv.Rows.Clear();
                foreach (var quer in select)
                {
                    _dgv.Rows.Add(quer.id, quer.description, quer.observation);
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
            this.Cursor = Cursors.WaitCursor;
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
            this.Cursor = Cursors.Default;
        }
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            frmQuery_Information frmQuery_Information = new frmQuery_Information();
            frmQuery_Information.insert = false;
            frmQuery_Information.query = Queries.Where(quer => quer.id == dgvData.CurrentRow.Cells[0].Value.ToString()).FirstOrDefault();
            frmQuery_Information.ShowDialog();
            Queries = await getQueriesAsync();
            txtSearch.Text = string.Empty;
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmQuery_Information frmQuery_Information = new frmQuery_Information();
            frmQuery_Information.insert = true;
            frmQuery_Information.ShowDialog();
            txtSearch.Text = string.Empty;
            Queries = await getQueriesAsync();
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
