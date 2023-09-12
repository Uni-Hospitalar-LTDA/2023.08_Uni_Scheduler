using _2023._08_Uni_Scheduler.App.Generic_Screen;
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
    public partial class frmSchedule_Information : CustomDefaultForm
    {

        /** Instance **/
        internal bool insert { get; set; } = true;
        internal SchedulerApp_Schedule Schedule { get; set; } = new SchedulerApp_Schedule();
        internal List<SchedulerApp_Contact> Contacts { get; set; } = new List<SchedulerApp_Contact>();
        internal List<SchedulerApp_Report> Reports { get; set; } = new List<SchedulerApp_Report> ();
        public frmSchedule_Information()
        {
            InitializeComponent();
            ConfigureFormAttributes();
            ConfigureFormProperties();


            ConfigureTextBoxProperties();
            ConfigureButtonProperties();
            ConfigureCheckBoxProperties();
            ConfigureDataGridViewProperties();
            ConfigureFormEvents();
        }


        /** Async Methods **/
        private async Task saveAsync()
        {
            try
            {               
                var schedule = new SchedulerApp_Schedule();
                var schedule_Report = new List<SchedulerApp_Schedule_Report>();
                var schedule_Contact = new List<SchedulerApp_Schedule_Contact>();

                schedule.id = txtScheduleId.Text;
                schedule.description = txtScheduleDescription.Text;
                schedule.observation = txtObservation.Text;
                schedule.hour = mtxHour.Text;                                
                schedule.daysofweek += (chkSunday.Checked ? "1" : "");
                schedule.daysofweek += (chkMonday.Checked ? "2" : "");
                schedule.daysofweek += (chkTuesday.Checked ? "3" : "");
                schedule.daysofweek += (chkWednesday.Checked ? "4" : "");
                schedule.daysofweek += (chkThursday.Checked ? "5" : "");
                schedule.daysofweek += (chkFriday.Checked ? "6" : "");
                schedule.daysofweek += (chkSunday.Checked ? "7" : "");
                schedule.daysofmonth = txtDaysOfMonth.Text;

                var toInsert = new List<SchedulerApp_Schedule>();
                toInsert.Add(schedule);
                await SchedulerApp_Schedule.insertAsync(toInsert);

                int lastCode = (await SchedulerApp_Schedule.getLastCodeAsync());

                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    schedule_Report.Add(new SchedulerApp_Schedule_Report { idSchedule = lastCode.ToString(), idReport = row.Cells[0].Value.ToString() });
                }
                await SchedulerApp_Schedule_Report.insertAsync(schedule_Report);

                foreach (var item in lsbEmails.Items)
                {
                    string[] t = item.ToString().Split('-');
                    schedule_Contact.Add(new SchedulerApp_Schedule_Contact { idContact = t[0].Trim(), idSchedule = lastCode.ToString() });
                }
                await SchedulerApp_Schedule_Contact.insertAsync(schedule_Contact);

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        private async Task updateAsync()
        {

            try
            {             
                var schedule = new SchedulerApp_Schedule();
                var schedule_Report = new List<SchedulerApp_Schedule_Report>();
                var schedule_Contact = new List<SchedulerApp_Schedule_Contact>();

                schedule.id = txtScheduleId.Text;
                schedule.description = txtScheduleDescription.Text;
                schedule.observation = txtObservation.Text;
                schedule.hour = mtxHour.Text;
                schedule.daysofweek += (chkSunday.Checked ? "1" : "");
                schedule.daysofweek += (chkMonday.Checked ? "2" : "");
                schedule.daysofweek += (chkTuesday.Checked ? "3" : "");
                schedule.daysofweek += (chkWednesday.Checked ? "4" : "");
                schedule.daysofweek += (chkThursday.Checked ? "5" : "");
                schedule.daysofweek += (chkFriday.Checked ? "6" : "");
                schedule.daysofweek += (chkSunday.Checked ? "7" : "");
                schedule.daysofmonth = txtDaysOfMonth.Text;

                
                await SchedulerApp_Schedule.updateAsync(schedule);
                
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    schedule_Report.Add(new SchedulerApp_Schedule_Report { idSchedule = schedule.id, idReport = row.Cells[0].Value.ToString() });
                }
                await SchedulerApp_Schedule_Report.deleteAsync(schedule.id);
                await SchedulerApp_Schedule_Report.insertAsync(schedule_Report);
                
                foreach (var item in lsbEmails.Items)
                {
                    string[] t = item.ToString().Split('-');
                    schedule_Contact.Add(new SchedulerApp_Schedule_Contact { idContact = t[0].Trim(), idSchedule = schedule.id });
                }
                await SchedulerApp_Schedule_Contact.deleteAsync(schedule.id);
                await SchedulerApp_Schedule_Contact.insertAsync(schedule_Contact);

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        



        /** Configure Form **/
        private void ConfigureFormAttributes()
        {
            this.Text = "Montagem da Agenda";
        }
        private void ConfigureFormProperties()
        {
            this.ConfigureDefault();
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmQuery_Information_Load;
        }
        private async void frmQuery_Information_Load(object sender, EventArgs e)
        {
            await ConfigureTextBoxAttributes();
            await ConfigureDataGridViewAttributes();
            await ConfigureListBoxAttributes();
            ConfigureCheckBoxEvents();
            ConfigureButtonEvents();
            ConfigureTextBoxEvents();
        }


        /** TextBox Configuration **/
        private async Task ConfigureTextBoxAttributes()
        {
            if (insert)
            {
                txtScheduleId.Text = (await SchedulerApp_Schedule.getNextCodeAsync()).ToString();
                mtxHour.Text = "08:00";
            }
            else if (!insert)
            {
                txtScheduleId.Text = Schedule.id;
                txtScheduleDescription.Text = Schedule.description;
                txtObservation.Text = Schedule.observation;
                txtDaysOfMonth.Text = Schedule.daysofmonth;
                mtxHour.Text = Schedule.hour;
                chkDaysOfWeek.Checked = (string.IsNullOrEmpty(Schedule.daysofweek) == false);
                chkSunday.Checked = (Schedule.daysofweek.Contains("1"));
                chkMonday.Checked = (Schedule.daysofweek.Contains("2"));
                chkTuesday.Checked = (Schedule.daysofweek.Contains("3"));
                chkWednesday.Checked = (Schedule.daysofweek.Contains("4"));
                chkThursday.Checked = (Schedule.daysofweek.Contains("5"));
                chkFriday.Checked = (Schedule.daysofweek.Contains("6"));
                chkSaturday.Checked = (Schedule.daysofweek.Contains("7"));                
            }

        }
        private void ConfigureTextBoxProperties()
        {
            txtScheduleId.ReadOnly = true;
            txtScheduleId.TabStop = false;

            
        }
        private void ConfigureTextBoxEvents()
        {
            txtDaysOfMonth.TextChanged += txtDaysOfMonth_TextChanged;
        }
        private void txtDaysOfMonth_TextChanged(object sender, EventArgs e)
        {
            string input = txtDaysOfMonth.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                lblValidationResult.Text = "Preencha para ativar.";
                return;
            }

            string[] parts = input.Split(',');

            foreach (string part in parts)
            {
                if (part.Trim().ToLower() != "last")
                {
                    if (!int.TryParse(part.Trim(), out int num))
                    {
                        lblValidationResult.Text = "[Invalid] Use números ou 'last'.";
                        return;
                    }

                    if (num <= 0 || num > 31)
                    {
                        lblValidationResult.Text = "[Invalid] Números <= 31 ou 'last'.";
                        return;
                    }
                }
            }

            lblValidationResult.Text = "[Valid]";
        }

        /** Button Configuration **/
        private void ConfigureButtonProperties()
        {
            btnClose.toDefaultCloseButton();
        }
        private void ConfigureButtonEvents()
        {
            btnSave.Click += btnSave_Click;
            btnAddReport.Click += btnAddReport_Click;
            btnRemoveReport.Click += btnRemoveReport_Click;
            btnAddEmail.Click += btnAddEmail_Click;
            btnRemoverEmail.Click += btnRemoverEmail_Click;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!chkDaysOfWeek.Checked && string.IsNullOrEmpty(txtDaysOfMonth.Text))
            {
                MessageBox.Show("Preencha a periodicidade");
                return;
            }
            if (!mtxHour.MaskFull)
            {
                MessageBox.Show("Preencha o horário de envio");
                return;
            }
            if (string.IsNullOrEmpty(txtScheduleDescription.Text) || lsbEmails.Items.Count == 0 || dgvData.Rows.Count == 0)
            {
                MessageBox.Show("Preencha as informações necessárias");
                return;
            }
            if (lblValidationResult.Text.Contains("Invalid"))
            {
                MessageBox.Show("Parâmetros inválidos.");
                return;
            }

            try
            {
                if (insert)
                    await saveAsync();
                else
                    await updateAsync();
            }
            catch (Exception)
            {

            }
            finally
            {
                MessageBox.Show("Processo cocluído com Sucesso!");
                this.Close();
            }
        }

        private bool _Exists(DataGridView dataGridView, string colunaNome, string dadoProcurado)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {                
                if (row.Cells[colunaNome].Value != null && Convert.ToInt32(row.Cells[colunaNome].Value.ToString()) == Convert.ToInt32(dadoProcurado))
                {
                    return true; // O dado foi encontrado na coluna
                }
            }

            return false; // O dado não foi encontrado na coluna
        }
        private async void btnAddReport_Click(object sender, EventArgs e)
        {
            frmGenericChoice choice = new frmGenericChoice();
            var list = await SchedulerApp_Report.getAllToListAsync();
            if (list != null)
            {
                foreach (var report in list)
                {                    
                    if (_Exists(dgvData, "id", report.id))
                        choice.clbGenericItems.Items.Add($"{report.id} - {report.description} - {report.observation} - {report.format}", true);
                    else
                        choice.clbGenericItems.Items.Add($"{report.id} - {report.description} - {report.observation} - {report.format}", false);

                }
            }
            choice.ShowDialog();

            dgvData.Rows.Clear();
            foreach (var conn in choice.clbGenericItems.CheckedItems)
            {
                string[] str = conn.ToString().Split('-');

                var queries = list?.Where(c => Convert.ToInt32(c.id) == Convert.ToInt32(str[0])).FirstOrDefault();
                if (queries != null)
                {
                    dgvData.Rows.Add(str[0], str[1], str[2], str[3]);
                }
            }
        }
        private void btnRemoveReport_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                dgvData.Rows.Remove(dgvData.CurrentRow);
            }
        }
        private async void btnAddEmail_Click(object sender, EventArgs e)
        {
            frmGenericChoice choice = new frmGenericChoice();
            var list = await SchedulerApp_Contact.getAllToListAsync();
            if (list != null)
            {
                foreach (var conn in list)
                {
                    if (lsbEmails.Items.Contains($"{conn.id} - {conn.description} - {conn.mail}"))
                        choice.clbGenericItems.Items.Add($"{conn.id} - {conn.description} - {conn.mail}", true);
                    else
                        choice.clbGenericItems.Items.Add($"{conn.id} - {conn.description} - {conn.mail}", false);

                }
            }
            choice.ShowDialog();

            lsbEmails.Items.Clear();
            foreach (var conn in choice.clbGenericItems.CheckedItems)
            {
                string[] str = conn.ToString().Split('-');

                var connection = list?.Where(c => Convert.ToInt32(c.id) == Convert.ToInt32(str[0])).FirstOrDefault();
                if (connection != null)
                {
                    lsbEmails.Items.Add($"{connection.id} - {connection.description} - {connection.mail}");
                }
            }
        }
        private void btnRemoverEmail_Click(object sender, EventArgs e)
        {

            lsbEmails.Items.Remove(lsbEmails.SelectedItem);
        }

        /** CheckBox configuration **/
        private void ConfigureCheckBoxProperties()
        {
            chkDaysOfWeek.Checked = true;
            chkMonday.Checked = true;
        }
        private void ConfigureCheckBoxEvents()
        {
            chkDaysOfWeek.CheckedChanged += chkDaysOfWeek_CheckedChanged;
        }
        private void chkDaysOfWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDaysOfWeek.Checked)
            {
                chkSunday.Enabled = true;
                chkMonday.Enabled = true;
                chkTuesday.Enabled = true;
                chkWednesday.Enabled = true;
                chkThursday.Enabled = true;
                chkFriday.Enabled = true;
                chkSaturday.Enabled = true;
                
                
            }
            else
            {
                chkSunday.Enabled = false;
                chkMonday.Enabled = false;
                chkTuesday.Enabled = false;
                chkWednesday.Enabled = false;
                chkThursday.Enabled = false;
                chkFriday.Enabled = false;
                chkSaturday.Enabled = false;

            }
        }

        /** DataGridView Configuration **/
        private void ConfigureDataGridViewProperties()
        {
            dgvData.Columns.Add("id", "Id");
            dgvData.Columns.Add("description", "Report");
            dgvData.Columns.Add("observation", "Observation");
            dgvData.Columns.Add("format", "Format");
            dgvData.toDefault();
        }
        private async Task ConfigureDataGridViewAttributes()
        {
            dgvData.TabStop = false;
            //Get Reports 
            if (!insert) 
            {
                var Reports = await SchedulerApp_Report.getAllToListByCodeAsync(Schedule.id);
                foreach (var Report in Reports)
                {
                    dgvData.Rows.Add(Report.id, Report.description, Report.observation, Report.format);
                }
            }
        }

        /** ListBox Attributes **/
        //Get contacts
        private async Task ConfigureListBoxAttributes()
        {
            if (!insert)
            {
                var Contacts = await SchedulerApp_Contact.getAllToListByCodeAsync(Schedule.id);
                foreach (var Contact in Contacts)
                {
                    lsbEmails.Items.Add($"{Contact.id} - {Contact.description} - {Contact.mail}");
                }
            }
        }






    }
}
