using _2023._08_Uni_Scheduler.App.Generic_Screen;
using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmQuery_Information : CustomDefaultForm
    {
        /** Instance **/

        internal bool insert { get; set; } = true;
        internal SchedulerApp_Query query { get; set; } = new SchedulerApp_Query();
        internal List<SchedulerApp_Query_Connection> query_connections {get;set;} = new List<SchedulerApp_Query_Connection>();
        internal List<SchedulerApp_Connection> connections { get; set; } = new List<SchedulerApp_Connection>();

        private int currentPage = 0;


        public frmQuery_Information()
        {
            InitializeComponent();

            ConfigureFormAttributes();
            ConfigureFormProperties();
            
            ConfigureTextBoxProperties();            
            ConfigureButtonProperties();
            ConfigureRichTextBoxProperties();
            ConfigureListBoxProperties();
            ConfigureDataGridViewProperties();
            ConfigureProgressBarProperties();
            ConfigureLabelProperties();
            ConfigureFormEvents();
        }

        /** Async Methods **/
        /** getConnections **/
        private async Task getConnectiopns()
        {
            connections = await SchedulerApp_Connection.getAllToListAsync();
        }
        private async Task saveAsync()
        {
            try
            {
                var query = new SchedulerApp_Query();
                var query_connection = new List<SchedulerApp_Query_Connection>();

                query.id = txtQueryId.Text;
                query.description = txtQueryDescription.Text;
                query.observation = txtObservation.Text;
                query.SQLcode = rtxtQuery.Text;

                var toInsert = new List<SchedulerApp_Query>();
                toInsert.Add(query);
                await SchedulerApp_Query.insertAsync(toInsert);

                int lastCode = (await SchedulerApp_Query.getLastCodeAsync());
                foreach (var item in lsbConnections.Items)
                {
                    string[] t = item.ToString().Split('-');                    
                    query_connection.Add(new SchedulerApp_Query_Connection { idConnection = t[0].Trim() ,idQuery = lastCode.ToString()});
                }                
                await SchedulerApp_Query_Connection.insertAsync(query_connection);

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
                var query = new SchedulerApp_Query();
                var query_connection = new List<SchedulerApp_Query_Connection>();

                query.id = txtQueryId.Text;
                query.description = txtQueryDescription.Text;
                query.observation = txtObservation.Text;
                query.SQLcode = rtxtQuery.Text;

                var toInsert = new List<SchedulerApp_Query>();
                
                await SchedulerApp_Query.updateAsync(query);
                
                foreach (var item in lsbConnections.Items)
                {
                    string[] t = item.ToString().Split('-');
                    query_connection.Add(new SchedulerApp_Query_Connection { idConnection = t[0].Trim(), idQuery = query.id });
                }

                await SchedulerApp_Query_Connection.deleteAsync(query.id);
                await SchedulerApp_Query_Connection.insertAsync(query_connection);

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

        /**Sync Methods **/
        private HashSet<string> sqlKeywords = new HashSet<string>
        {
            "select", "from", "where",  "like",  "group",
            "by", "having", "order", "distinct", "insert", "into", "values", "update", "set",
            "delete", "create", "table", "database", "index", "view", "procedure", "primary", "key", "foreign", "constraint",
            "default", "check", "unique", "alter", "drop", "commit", "rollback", "trigger", "cursor", "declare"
        };
        private HashSet<string> sqlJoins = new HashSet<string>
        {
            "inner join", "left join", "right join", "outer join", "join",
            "and", "or", "not", "null", "in", "between", "exists", "on",
            "is", "asc", "desc", "union", "all", "top", "*"
        };
        private bool IsLineComment(string line)
        {
            return line.TrimStart().StartsWith("//");
        }

        private bool IsBlockCommentStart(string line)
        {
            return line.TrimStart().StartsWith("/**");
        }

        private bool IsBlockCommentEnd(string line)
        {
            return line.Trim().EndsWith("*/");
        }

        private string GetWordAt(string text, int position)
        {
            if (position >= text.Length)
                return string.Empty;

            int start = position;
            while (position < text.Length && !char.IsWhiteSpace(text[position]))
            {
                position++;
            }
            return text.Substring(start, position - start);
        }
        private int SkipWhitespace(string text, int position)
        {
            while (position < text.Length && char.IsWhiteSpace(text[position]))
            {
                position++;
            }
            return position;
        }

        /** Configure Form **/
        private void ConfigureFormAttributes()
        {
            this.Text = "Consulta do Relatório";
        }
        private void ConfigureFormProperties()
        {
            this.ConfigureDefault();
            this.KeyPreview = true;
            this.MaximizeBox = true;
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmQuery_Information_Load;
            this.KeyDown += frmQuery_Information_KeyDown;    
        }
        private void frmQuery_Information_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                btnExecute_Click(sender, e);
            }
        }        
        private async void frmQuery_Information_Load(object sender, EventArgs e)
        {


            await getConnectiopns();
            await ConfigureTextBoxAttributes();

            
            ConfigureButtonEvents();
            ConfigureRichTextBoxEvents(); 
            ConfigureListBoxEvents();
        }

        /** Label Configuration **/
        private void ConfigureLabelProperties()
        {
            lblObservation.Anchor = AnchorStyles.Top | AnchorStyles.Right; 
        }

        /** TextBox Configuration **/
        private async Task ConfigureTextBoxAttributes()
        {
            if (insert)
            {
                txtQueryId.Text = (await SchedulerApp_Query.getNextCodeAsync()).ToString();
            }
            else
            {
                txtQueryId.Text = query.id;
                txtQueryDescription.Text = query.description;   
                txtObservation.Text = query.observation;
                rtxtQuery.Text = query.SQLcode.Replace("`","'");

                query_connections = await SchedulerApp_Query_Connection.getAllToListByIdAsync(query.id);

                foreach (var conn in query_connections)
                {

                    var connection = connections.Where(c => c.id == conn.idConnection).FirstOrDefault();
                    lsbConnections.Items.Add($"{connection.id} - {connection.description} - {connection.server}");
                }



            }

        }
        private void ConfigureTextBoxProperties()
        {
            txtQueryId.ReadOnly = true;
            txtQueryId.TabStop = false;
            txtOutput.ReadOnly = true;
            txtOutput.TabStop = false;
            txtActive.ReadOnly = true;
            txtActive.TabStop = false;

            txtQueryDescription.MaxLength = 25;
            txtObservation.MaxLength = 255;
            txtOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                                               
        }
       


        /** Button Configuration **/
        private void ConfigureButtonProperties()
        {
            btnPdfFormat.Enabled = false;
            btnClose.toDefaultCloseButton();


            btnPreviousPage.TabStop = false;
            btnNextPage.TabStop = false;


            btnAddConnection.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnRemoveConnection.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnNextPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPreviousPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        }
        private void ConfigureButtonEvents()
        {
            btnAddConnection.Click += btnAddConnection_Click;
            btnRemoveConnection.Click += btnRemoveConnection_Click;
            btnExecute.Click += btnExecute_Click;
            btnSave.Click += btnSave_Click;
            btnJsonFormat.Click += btnJsonFormat_Click;
            btnXmlFormat.Click += btnXmlFormat_Click;
            btnXlsxFormat.Click += btnXlsxFormat_Click;
            btnPreviousPage.Click += btnPreviousPage_Click;
            btnNextPage.Click += btnNextPage_Click;
            

        }

        
        private async void btnAddConnection_Click(object sender, EventArgs e)
        {
            frmGenericChoice choice = new frmGenericChoice();
            var list = await SchedulerApp_Connection.getAllToListAsync();
            if (list != null)
            {
                foreach (var conn in list)
                {
                    if (lsbConnections.Items.Contains($"{conn.id} - {conn.description} - {conn.server}"))
                        choice.clbGenericItems.Items.Add($"{conn.id} - {conn.description} - {conn.server}", true);
                    else
                        choice.clbGenericItems.Items.Add($"{conn.id} - {conn.description} - {conn.server}", false);

                }
            }
            choice.ShowDialog();

            lsbConnections.Items.Clear();
            foreach (var conn in choice.clbGenericItems.CheckedItems)
            {
                string[] str = conn.ToString().Split('-');

                var connection = list?.Where(c => Convert.ToInt32(c.id) == Convert.ToInt32(str[0])).FirstOrDefault();
                if (connection != null)
                {
                    lsbConnections.Items.Add($"{connection.id} - {connection.description} - {connection.server}");                    
                }                
            }
        }
        private void btnRemoveConnection_Click(object sender, EventArgs e)
        {
            
            lsbConnections.Items.Remove(lsbConnections.SelectedItem);
        }
        private async void btnExecute_Click(object sender, EventArgs e)
        {         
            try
            {                
                pcbCharge.Visible = true;
                pcbCharge.Value = 0;
                if (!string.IsNullOrEmpty(txtActive.Text))
                {
                    if (!string.IsNullOrEmpty(rtxtQuery.Text))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        var conn = (await SchedulerApp_Connection.getAllToListAsync())
                            .Where(c => Convert.ToInt32(c.id) == Convert.ToInt32(txtActive.Text.Split('-')[0]))
                            .FirstOrDefault();

                        // Obtenha a contagem de linhas antes de executar a consulta
                        int rowCount = await SchedulerApp_Query.GetRowCountAsync(rtxtQuery.Text, conn);
                        pcbCharge.Value = 20;
                        if (rowCount > 30000)
                        {
                            MessageBox.Show("Sua consulta retornará mais de 30.000 linhas. Considere otimizar ou limitar sua consulta.");
                            return;
                        }

                        Tuple<string, DataTable> execution;
                        pcbCharge.Value = 40;
                        if (!chkFree.Checked) 
                        {
                            execution = await SchedulerApp_Query.ExecuteAsync(rtxtQuery.Text, conn, currentPage);
                        }
                        else
                        {
                            execution = await SchedulerApp_Query.ExecuteAsync(rtxtQuery.Text, conn);
                        }
                        txtOutput.Text = execution.Item1;
                        dgvData.DataSource = execution.Item2;
                        pcbCharge.Value = 80;
                    }
                    else
                    {
                        MessageBox.Show("Escreva a consulta");
                        pcbCharge.Value = 100;
                        pcbCharge.Visible = false;
                        return;

                    }
                }
                else
                {
                    MessageBox.Show("Selecione uma conexão!!");
                    pcbCharge.Value = 100;
                    pcbCharge.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            pcbCharge.Value = 100;
            MessageBox.Show("Carregamento concluído!");
            pcbCharge.Visible = false;
            this.Cursor = Cursors.Default;
        }
        private void btnJsonFormat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                pcbCharge.Visible = true;
                pcbCharge.Value = 0;

                if (dgvData.DataSource != null)
                {
                    string name = Exportation.toJsonWithPath(dgvData, Path.GetRandomFileName());

                    // Verifique se o Notepad++ está instalado
                    string notepadPlusPlusPath32Bit = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
                    string notepadPlusPlusPath64Bit = @"C:\Program Files\Notepad++\notepad++.exe";
                    pcbCharge.Value = 20;
                    if (File.Exists(notepadPlusPlusPath32Bit))
                    {
                        Process.Start(notepadPlusPlusPath32Bit, name);
                    }
                    else if (File.Exists(notepadPlusPlusPath64Bit))
                    {
                        Process.Start(notepadPlusPlusPath64Bit, name);
                    }
                    else
                    {
                        MessageBox.Show("Notepad++ não está instalado em seu sistema. Recomendamos que você o instale a partir do site oficial: https://notepad-plus-plus.org/");
                    }
                    pcbCharge.Value = 80;
                }
                else
                {
                    MessageBox.Show("Efetue uma consulta para visualizar o protótipo.");
                    pcbCharge.Value = 100;
                    pcbCharge.Visible = false;
                    this.Cursor = Cursors.Default;
                    return;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
            pcbCharge.Value = 100;            
            MessageBox.Show("Carregamento concluído!");
            pcbCharge.Visible = false; 
        }     
        private void btnXmlFormat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                pcbCharge.Value = 0;
                pcbCharge.Visible = true;
                if (dgvData.DataSource != null)
                {
                    string name = Exportation.toXmlWithPath(dgvData, Path.GetRandomFileName());

                    // Verifique se o Notepad++ está instalado
                    string notepadPlusPlusPath32Bit = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
                    string notepadPlusPlusPath64Bit = @"C:\Program Files\Notepad++\notepad++.exe";
                    pcbCharge.Value = 20;
                    if (File.Exists(notepadPlusPlusPath32Bit))
                    {
                        Process.Start(notepadPlusPlusPath32Bit, name);
                    }
                    else if (File.Exists(notepadPlusPlusPath64Bit))
                    {
                        Process.Start(notepadPlusPlusPath64Bit, name);
                    }
                    else
                    {
                        MessageBox.Show("Notepad++ não está instalado em seu sistema. Recomendamos que você o instale a partir do site oficial: https://notepad-plus-plus.org/");
                        pcbCharge.Value = 100;
                        pcbCharge.Visible = false;
                        return;

                    }
                }
                else
                {
                    MessageBox.Show("Efetue uma consulta para visualizar o protótipo.");
                    pcbCharge.Value = 100;
                    pcbCharge.Visible = false;
                    this.Cursor = Cursors.Default;
                    return;
                }
                pcbCharge.Value = 40;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
            pcbCharge.Value = 100;
            this.Cursor = Cursors.Default;
            MessageBox.Show("Carregamento concluído!");
            pcbCharge.Visible = false;

        }       
        private void btnXlsxFormat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                pcbCharge.Value = 0;
                pcbCharge.Visible = true;
                if (dgvData.DataSource != null)
                {
                    pcbCharge.Value = 20;
                    string name = Exportation.toExcelWithPath(dgvData, Path.GetRandomFileName());
                    
                    // Check if Microsoft Excel is installed
                    string excelPath32Bit = @"C:\Program Files (x86)\Microsoft Office\root\Office16\EXCEL.EXE";
                    string excelPath64Bit = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
                    pcbCharge.Value = 40;
                    if (File.Exists(excelPath32Bit))
                    {
                        Process.Start(excelPath32Bit, name);
                    }
                    else if (File.Exists(excelPath64Bit))
                    {
                        Process.Start(excelPath64Bit, name);
                    }
                    else
                    {
                        MessageBox.Show("Microsoft Excel não está instalado em seu sistema. Recomendamos que você o instale a partir do site oficial da Microsoft.");
                    }
                    pcbCharge.Value = 80;
                }
                else
                {
                    MessageBox.Show("Efetue uma consulta para visualizar o protótipo.");
                    pcbCharge.Value = 100;
                    pcbCharge.Visible = false;
                    this.Cursor = Cursors.Default;
                    return;
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }

            pcbCharge.Value = 100;
            this.Cursor = Cursors.Default;
            MessageBox.Show("Carregamento conclído!");
            pcbCharge.Visible = false;
        }
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            currentPage++;
            btnExecute_Click(sender, e);
        }
        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                btnExecute_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Você já está na primeira página.");
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (insert) 
                await saveAsync();
            else 
                await updateAsync();
        }


        /**RichTextBox Configuration**/
        private void ConfigureRichTextBoxEvents()
        {
            rtxtQuery.TextChanged += rtxtQuery_TextChanged;
        }
        private void ConfigureRichTextBoxProperties()
        {
            

            rtxtQuery.ScrollBars = RichTextBoxScrollBars.Both;
            rtxtQuery.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
        }
        private void rtxtQuery_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rtxtQuery.TextChanged -= rtxtQuery_TextChanged;

                int currentSelectionStart = rtxtQuery.SelectionStart;

                rtxtQuery.SelectAll();
                rtxtQuery.SelectionColor = Color.Black;
                rtxtQuery.SelectionFont = new Font(rtxtQuery.Font, FontStyle.Regular);

                int position = 0;
                bool inBlockComment = false;

                while (position < rtxtQuery.Text.Length)
                {
                    string word = GetWordAt(rtxtQuery.Text, position);
                    string line = rtxtQuery.Lines[rtxtQuery.GetLineFromCharIndex(position)];

                    if (IsLineComment(line))
                    {
                        rtxtQuery.Select(position, line.Length);
                        rtxtQuery.SelectionColor = Color.Green;
                        position += line.Length + 1; // Jump to next line
                        continue;
                    }
                    else if (IsBlockCommentStart(line))
                    {
                        int blockCommentStart = position;
                        int blockCommentLength = 0;
                        inBlockComment = true;

                        while (inBlockComment && position < rtxtQuery.Text.Length)
                        {
                            blockCommentLength += rtxtQuery.Lines[rtxtQuery.GetLineFromCharIndex(position)].Length + 1;
                            if (IsBlockCommentEnd(rtxtQuery.Lines[rtxtQuery.GetLineFromCharIndex(position)]))
                            {
                                inBlockComment = false;
                            }
                            position += rtxtQuery.Lines[rtxtQuery.GetLineFromCharIndex(position)].Length + 1; // Jump to next line
                        }
                        rtxtQuery.Select(blockCommentStart, blockCommentLength);
                        rtxtQuery.SelectionColor = Color.Green;
                        continue;
                    }
                    else if (sqlKeywords.Contains(word.ToLower()))
                    {
                        rtxtQuery.Select(position, word.Length);
                        rtxtQuery.SelectionColor = Color.Blue;
                        rtxtQuery.SelectionFont = new Font(rtxtQuery.Font, FontStyle.Bold);
                    }
                    else
                    {
                        int nextWordPosition = position + word.Length;
                        nextWordPosition = SkipWhitespace(rtxtQuery.Text, nextWordPosition);

                        string nextWord = GetWordAt(rtxtQuery.Text, nextWordPosition);
                        string twoWords = word + " " + nextWord;

                        if (sqlJoins.Contains(twoWords.ToLower()))
                        {
                            rtxtQuery.Select(position, twoWords.Length);
                            rtxtQuery.SelectionColor = Color.Gray;
                            rtxtQuery.SelectionFont = new Font(rtxtQuery.Font, FontStyle.Bold);
                            position = nextWordPosition + nextWord.Length;
                        }
                        else if (sqlJoins.Contains(word.ToLower()))
                        {
                            rtxtQuery.Select(position, word.Length);
                            rtxtQuery.SelectionColor = Color.Gray;
                            rtxtQuery.SelectionFont = new Font(rtxtQuery.Font, FontStyle.Bold);
                        }
                    }

                    position += word.Length;
                    position = SkipWhitespace(rtxtQuery.Text, position);
                }

                rtxtQuery.SelectionStart = currentSelectionStart;
                rtxtQuery.SelectionLength = 0;

                rtxtQuery.TextChanged += rtxtQuery_TextChanged;
            }
            catch (Exception)
            {

            }
        }


        /** ListBox configuration **/
        private void ConfigureListBoxProperties()
        {
            lsbConnections.TabStop = false;
            lsbConnections.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }
        private void ConfigureListBoxEvents()
        {
            lsbConnections.DoubleClick += lsbConnections_DoubleClick;
        }

        private void lsbConnections_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lsbConnections.SelectedItem != null) 
                    txtActive.Text = lsbConnections.SelectedItem.ToString();
            }
            catch (Exception)
            {

            }
        }

        /** DataGridView Configuration **/
        private void ConfigureDataGridViewProperties()
        {
            dgvData.TabStop = false;
            dgvData.toDefault();
            dgvData.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        /** ProgressBar Configuration **/
        private void ConfigureProgressBarProperties()
        {
            pcbCharge.Maximum = 100;  
            pcbCharge.Visible= false;
        }


        
    }
}
