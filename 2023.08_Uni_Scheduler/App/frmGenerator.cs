using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using _2023._08_Uni_Scheduler.Domain.Entities.Email;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmGenerator : CustomDefaultForm
    {
        public frmGenerator()
        {
            InitializeComponent();
            ConfigureFormAttributes();
            ConfigureFormProperties();

            ConfigureRichTextBoxProperties();
            ConfigureTextBoxProperties();
            ConfigureDataGridViewProperties();
            ConfigureProgressBarProperties();
            ConfigureCheckBoxProperties();
            ConfigureFormEvents();
        }


        private List<SchedulerApp_Connection> connections = new List<SchedulerApp_Connection>();
        private List<Archive> archives = new List<Archive>();
        /** Async Tasks **/
        private async void executeAsync()
        {

            if (string.IsNullOrEmpty(txtActive.Text))
            {
                MessageBox.Show("Selecione uma conexão.");
                return;
            }
            var conn = connections.Where(c => Convert.ToInt32(c.id) == Convert.ToInt32(txtActive.Text.Split('-')[0])).FirstOrDefault();
            var result = await SchedulerApp_Query.ExecuteAsync(rtxtQuery.Text, conn);
            if (result != null)
            {
                txtOutput.Text = result.Item1;
                dgvData.DataSource = result.Item2;
            }
        }

        private async Task getConnections()
        {

            connections = await SchedulerApp_Connection.getAllToListAsync();
            
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
            this.Text = "Gerador de Relatórios";
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
            this.Load += frmGenerator_Load; ;
            this.KeyDown += frmGenerator_KeyDown;
        }
        private async void frmGenerator_Load(object sender, EventArgs e)
        {
            if (false)
            {
                rtxtQuery.Text = "SELECT TOP 100 * FROM DMD.dbo.CLIEN";
                txtEmailTitle.Text = "Teste de Envio novo Atenas";                
                txtTo.Text = "rafael.silva@unihospitalar.com.br,ricardo.santos@unihospitalar.com.br";
            }

            
            await getConnections();


            ConfigureListBoxAttributes();


            ConfigureButtonEvents();
            ConfigureRichTextBoxEvents();
            ConfigureListBoxEvents();
        }
        private void frmGenerator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                executeAsync();
            }
        }

        /** ListBox configuration **/
        private void ConfigureListBoxAttributes()
        {
            if (connections != null)
            {
                foreach (var conn in connections)
                {
                    lsbConnections.Items.Add($"{conn.id} - {conn.description} - {conn.server}");
                }
            }
        }
        private void ConfigureListBoxEvents()
        {
            lsbConnections.DoubleClick += lsbConnections_DoubleClick;
            lsbReportList.DoubleClick += lsbReportList_DoubleClick;
            lsbReportList.KeyDown += lsbReportList_KeyDown;
        }

        private void lsbReportList_DoubleClick(object sender, EventArgs e)
        {
            rtxtQuery.Text =  archives.Where(a => a.description.Equals(lsbReportList.SelectedItem.ToString())).FirstOrDefault().query;
        }
        private void lsbReportList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                archives.Remove(archives.Where(a => a.description.Equals(lsbReportList.SelectedItem.ToString())).FirstOrDefault());
                lsbReportList.Items.Remove(lsbReportList.SelectedItem);
                
            }
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
        
        /** Button Configuration**/
        private void ConfigureButtonEvents()
        {
            btnAdd.Click += btnAdd_Click;
            btnExecute.Click += btnExecute_Click;
            btnSendEmail.Click += btnSendEmail_Click;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            executeAsync();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSheetName.Text.replaceSpecialCharacters()) && !string.IsNullOrEmpty(rtxtQuery.Text) && dgvData.DataSource != null)
            {
                lsbReportList.Items.Add(txtSheetName.Text.replaceSpecialCharacters());

                string formats = string.Empty;
                formats += (chkExcel.Checked ? "E":"");
                formats += (chkXml.Checked? "X" : "");
                formats += (chkJson.Checked ? "J" : "");
                
                archives.Add(new Archive
                {
                    description = txtSheetName.Text,
                    query = rtxtQuery.Text,
                    data = (DataTable)dgvData.DataSource,
                    format = formats
                });
                txtSheetName.Text = string.Empty;
                dgvData.DataSource = null;
                MessageBox.Show("Adicionado com sucesso!");                
            }
            else
            {
                MessageBox.Show("Preencha as informações necessárias");
            }
        }

        private async void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (lsbReportList.Items.Count == 0 )
            {
                MessageBox.Show("Não é possível enviar o e-mail sem relatórios.");
                return;

            }
            
            var conn = connections.Where(c => Convert.ToInt32(c.id) == Convert.ToInt32(txtActive.Text.Split('-')[0])).FirstOrDefault();
            List<string> emails = new List<string>();
            foreach (var email in txtTo.Text.Split(','))
            {
                emails.Add(email);
            }
            var dt = new List<string>();            

            foreach (var archive in archives)
            {
                dt.Add(archive.description);
            }

            string description =

                $@"Olá,

Este robô envia relatórios e alertas de forma autônoma. Por favor, não nos responda. Para quaisquer dúvidas, entre em contato pelo e-mail pelo telefone (81) 3472-7201.";

            string bottom_message = $@"
                                            Atenciosamente,                                            
                                            Intelligence Bot";

            try
            {
                this.Cursor = Cursors.WaitCursor;
                Email.getMailCredentials();
                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;
                string str = await Email.SendEmailWithExcelAttachment(emails, "ti@unihospitalar.com.br", txtEmailTitle.Text, description, dt, bottom_message, conn.logo, archives, chkSeparateDashboards.Checked);
                progressBar1.Visible = false;                
                MessageBox.Show("Enviado");
            }
            catch (Exception)
            {

            }
            finally
            {

                this.Cursor = Cursors.Default;
            }
        }


        /** TextBox Configuration **/
        private void ConfigureTextBoxProperties()
        {            
            txtOutput.ReadOnly = true;
            txtOutput.TabStop = false;
            txtActive.ReadOnly = true;
            txtActive.TabStop = false;


            txtSheetName.MaxLength = 31;
            

        }

        /**RichTextBox Configuration**/
        private void ConfigureRichTextBoxEvents()
        {
            rtxtQuery.TextChanged += rtxtQuery_TextChanged;
        }
        private void ConfigureRichTextBoxProperties()
        {
            rtxtQuery.TabIndex = 13;
            rtxtQuery.ScrollBars = RichTextBoxScrollBars.Both;
            rtxtQuery.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
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

        /** Checked Box Configuration **/
        private void ConfigureCheckBoxProperties()
        {
            chkExcel.Checked = true;
        }

        /** Progress Bar configuration **/
        private void ConfigureProgressBarProperties()
        {
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
        }
        /** DataGridView Configuration **/
        private void ConfigureDataGridViewProperties()
        {
            dgvData.TabStop = false;
            dgvData.toDefault();
            dgvData.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
        }
    }
}
