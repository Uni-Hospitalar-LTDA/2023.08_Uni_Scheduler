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
    public partial class frmSchedule : CustomDefaultForm
    {

        /** Instance **/
        List<SchedulerApp_Schedule> Schedules = new List<SchedulerApp_Schedule>();

        public frmSchedule()
        {
            InitializeComponent();
            ConfigureFormAttributes();
            ConfigureFormProperties();
            ConfigureFormEvents();
        }
        /** Async Tasks **/
        private async Task<List<SchedulerApp_Schedule>> getSchedulesAsync()
        {
            try
            {
                return await SchedulerApp_Schedule.getAllToListAsync();
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
            this.Text = "Agendas";
        }
        private void ConfigureFormProperties()
        {
            this.ConfigureDefault();
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmSchedule_Load;
        }

        private async void frmSchedule_Load(object sender, EventArgs e)
        {
            Schedules = await getSchedulesAsync();


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
            var select = from sched in Schedules.ToList()
                         where sched.id == description || sched.description.ToUpper().Contains(description) || sched.observation.ToUpper().Contains(description)
                         select sched;
            _dgv.Invoke((Action)delegate
            {
                if (_dgv.Columns.Count == 0)
                {
                    _dgv.Columns.Add("id", "Id");
                    _dgv.Columns.Add("description", "Report");
                    _dgv.Columns.Add("observation", "Observation");
                    _dgv.Columns.Add("DaysOfWeek", "Days Of Week");
                    _dgv.Columns.Add("DaysOFMonth", "Days Of Month");
                }
                _dgv.Rows.Clear();
                foreach (var sched in select)
                {
                    _dgv.Rows.Add(sched.id, sched.description, sched.observation,sched.daysofweek,sched.daysofmonth);
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
            frmSchedule_Information frmSchedule_Information = new frmSchedule_Information();
            frmSchedule_Information.insert = false;
            frmSchedule_Information.Schedule = Schedules.Where(quer => quer.id == dgvData.CurrentRow.Cells[0].Value.ToString()).FirstOrDefault();
            frmSchedule_Information.ShowDialog();
            Schedules = await getSchedulesAsync();
            txtSearch.Text = string.Empty;
            addConnectionsToDataGridView(txtSearch.Text, dgvData);
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmSchedule_Information frmSchedule_Information = new frmSchedule_Information();
            frmSchedule_Information.insert = true;
            frmSchedule_Information.ShowDialog();
            txtSearch.Text = string.Empty;
            Schedules = await getSchedulesAsync();
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