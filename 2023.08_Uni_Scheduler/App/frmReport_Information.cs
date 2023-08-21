using _2023._08_Uni_Scheduler.App.Generic_Screen;
using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.Primes;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmReport_Information : CustomDefaultForm
    {
        /** Instance **/

        internal bool insert { get; set; } = true;
        internal SchedulerApp_Report report { get; set; } = new SchedulerApp_Report();
        
        public frmReport_Information()
        {
            InitializeComponent();

            ConfigureFormAttributes();
            ConfigureFormProperties();





            ConfigureButtonProperties();
            ConfigureCheckBoxProperties();
            ConfigureTextBoxProperties();
            ConfigureDataGridViewProperties();


            ConfigureFormEvents();


        }

        /** Async Methods **/
        private async Task saveAsync()
        {
            try
            {
                if ((!chkJsonFormat.Checked && !chkPdfFormat.Checked && !chkXlsxFormat.Checked && !chkXmlFormat.Checked))
                {
                    MessageBox.Show("É necessário marcar algum formato para envio.");
                    return;
                }
                if (string.IsNullOrEmpty(txtReportDescription.Text.Trim()))
                {
                    MessageBox.Show("Dê um nome ao relatório.");
                    return;
                }
                if (dgvData.Rows.Count == 0)
                {
                    MessageBox.Show("Selecione algum relatório.");
                    return;
                }
                var report = new SchedulerApp_Report();
                var report_Query = new List<SchedulerApp_Report_Query>();

                report.id = txtReportId.Text;
                report.description = txtReportDescription.Text;
                report.observation = txtObservation.Text;
                report.withsheets = Convert.ToInt16(chkWithSheets.Checked).ToString();
                report.format += (chkXlsxFormat.Checked ? "E":"");
                report.format += (chkJsonFormat.Checked ? "J":"");
                report.format += (chkXmlFormat.Checked  ? "X":"");
                report.format += (chkPdfFormat.Checked ? "P" : "");


                var toInsert = new List<SchedulerApp_Report>();
                toInsert.Add(report);
                await SchedulerApp_Report.insertAsync(toInsert);

                int lastCode = (await SchedulerApp_Report.getLastCodeAsync());
                foreach (DataGridViewRow row in dgvData.Rows)
                {                    
                    report_Query.Add(new SchedulerApp_Report_Query { idReport = lastCode.ToString(), idQuery = row.Cells[0].Value.ToString() });
                }
                await SchedulerApp_Report_Query.insertAsync(report_Query);

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
                var report = new SchedulerApp_Report();
                var report_query = new List<SchedulerApp_Report_Query>();

                report.id = txtReportId.Text;
                report.description = txtReportDescription.Text;
                report.observation = txtObservation.Text;
                report.withsheets = Convert.ToInt16(chkWithSheets.Checked).ToString();
                report.format = string.Empty;
                report.format += (chkXlsxFormat.Checked ? "E" : "");
                report.format += (chkJsonFormat.Checked ? "J" : "");
                report.format += (chkXmlFormat.Checked ? "X" : "");
                report.format += (chkPdfFormat.Checked ? "P" : "");
                var toInsert = new List<SchedulerApp_Query>();
                await SchedulerApp_Report.updateAsync(report);
                foreach (DataGridViewRow _row in dgvData.Rows)
                {                    
                    report_query.Add(new SchedulerApp_Report_Query { idReport =report.id, idQuery = _row.Cells[0].Value.ToString() });
                }
                await SchedulerApp_Report_Query.deleteAsync(report.id);
                await SchedulerApp_Report_Query.insertAsync(report_query);

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
            this.Text = "Montagem do Relatório";
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

            ConfigureCheckBoxEvents();
            ConfigureButtonEvents();
        }


        /** TextBox Configuration **/
        private async Task ConfigureTextBoxAttributes()
        {
            if (insert)
            {
                txtReportId.Text = (await SchedulerApp_Report.getNextCodeAsync()).ToString();
            }
            else
            {
                txtReportId.Text = report.id;
                txtReportDescription.Text = report.description;
                txtObservation.Text = report.observation;
                chkWithSheets.Checked = Convert.ToBoolean(Convert.ToInt16(report.withsheets));
                chkJsonFormat.Checked = (report.format.Contains("J"));
                chkXlsxFormat.Checked = (report.format.Contains("E"));
                chkXmlFormat.Checked = (report.format.Contains("X"));
                chkPdfFormat.Checked = (report.format.Contains("P"));

                var report_Queries = await SchedulerApp_Query.getAllToListByIdAsync(report.id);

                if (report_Queries != null)
                {
                    foreach (var conn in report_Queries)
                    {
                        dgvData.Rows.Add(conn.id, conn.description, conn.observation);
                    }
                }
            }

        }
        private void ConfigureTextBoxProperties()
        {
            txtReportId.ReadOnly = true;
            txtReportId.TabStop = false;
            
                                            
        }



        /** Button Configuration **/
        private void ConfigureButtonProperties()
        {            
            btnClose.toDefaultCloseButton();
        }
        private void ConfigureButtonEvents()
        {            
            btnSave.Click += btnSave_Click;
            btnAddQuery.Click += btnAddQuery_Click;
            btnRemoveQuery.Click += btnRemoveQuery_Click;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (insert)
                await saveAsync();
            else
                await updateAsync();
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
        private async void btnAddQuery_Click(object sender, EventArgs e)
        {
            frmGenericChoice choice = new frmGenericChoice();
            var list = await SchedulerApp_Query.getAllToListAsync();
            if (list != null)
            {
                foreach (var quer in list)
                {
                    if (_Exists(dgvData,"id",quer.id))
                        choice.clbGenericItems.Items.Add($"{quer.id} - {quer.description} - {quer.observation}", true);
                    else
                        choice.clbGenericItems.Items.Add($"{quer.id} - {quer.description} - {quer.observation}", false);

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
                    dgvData.Rows.Add(str[0], str[1], str[2]);
                }
            }
        }
        private void btnRemoveQuery_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null) 
            {
                dgvData.Rows.Remove(dgvData.CurrentRow);
            }
        }

        /** CheckBox configuration **/
        private void ConfigureCheckBoxProperties()
        {
            chkPdfFormat.Enabled = false;
        }
        private void ConfigureCheckBoxEvents()
        {
            chkWithSheets.CheckedChanged += chkWithSheets_CheckedChanged;
        }
        private void chkWithSheets_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithSheets.Checked)
            {
                chkXlsxFormat.Checked = true;
                chkXmlFormat.Checked = false;
                chkJsonFormat.Checked = false;
                chkXlsxFormat.Enabled = false;
                chkXmlFormat.Enabled = false;
                chkJsonFormat.Enabled = false;
            }
            else
            {                
                chkXlsxFormat.Enabled = true;
                chkXmlFormat.Enabled = true;
                chkJsonFormat.Enabled = true;
            }
        }

        /** DataGridView Configuration **/
        private void ConfigureDataGridViewProperties()
        {
            dgvData.Columns.Add("id","Id");
            dgvData.Columns.Add("description", "Query");
            dgvData.Columns.Add("observation", "Observation");
            dgvData.toDefault();
            dgvData.TabStop = false;
        }
    }
}
