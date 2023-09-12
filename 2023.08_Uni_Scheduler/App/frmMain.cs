using _2023._08_Uni_Scheduler.App;
using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using _2023._08_Uni_Scheduler.Domain.Entities.Email;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Guna.UI2.WinForms;
using iText.Kernel.Colors;
using Microsoft.Office.Core;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = System.Drawing.Color;

namespace _2023._08_Uni_Scheduler
{
    public partial class frmMain : CustomForm
    {

        public frmMain()
        {
            InitializeComponent();

            //Form Properties e Attributes
            ConfigureFormProperties();
            ConfigureFormAttributes();

            //Control properties
            ConfigureLabelProperties();
            ConfigureProgressbarProperties();
            ConfigureTextBoxProperties();
            ConfigureTimerProperties();
            ConfigureButtonProperties();
            ConfigurePictureBoxProperties();

            //Form Events 
            ConfigureFormEvents();
        }



        /** Instance **/
        private System.Windows.Forms.Timer timerHour = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerVerifySchedule = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerSend = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerReportByMail = new System.Windows.Forms.Timer();
        
        private SemaphoreSlim semaphore = new SemaphoreSlim(4); // Limita a 4 threads simultâneas.
        private object syncLock = new object();
        private List<SchedulerApp_Schedule> lScheds = new List<SchedulerApp_Schedule>();
        private Queue<SchedulerApp_Schedule> qScheds = new Queue<SchedulerApp_Schedule>();

        /** Async Methods **/
        private async Task sendBySolicitation()
        {
            try
            {                

                Log("Iniciando envio de e-mails por solicitação...");
                var teste = await SchedulerApp_Contact.getAllToListAllowedContactsAsync();
                Email.getMailCredentials();
                Log("Obtive as credenciais");
                List<string> allowedContacts = teste.Select(contact => contact.mail).ToList();
                var emails = Email.ListPopEmailTitlesAndSenders(allowedContacts);
                Log("Listei " + emails.Count);
                List<Task> emailTasks = new List<Task>();

                foreach (var email in emails)
                {
                    Log($"Preparando para enviar e-mail para {email.from}...");
                    var emailTask = SendIndividualEmailBySolicitationAsync(email);

                    emailTasks.Add(emailTask);

                    if (emailTasks.Count >= 4)
                    {
                        await Task.WhenAll(emailTasks);
                        emailTasks.Clear();
                    }
                }

                if (emailTasks.Count > 0)
                {
                    await Task.WhenAll(emailTasks);
                }

                Log($"Verificação Concluída. ");                
            }
            catch (Exception ex)
            {
                Log("Erro: " + ex.Message);
            }
            
        }

        private async Task SendIndividualEmailBySolicitationAsync(EmailMessage email)
        {
            try
            {                

                string description = string.Empty;
                string bottom_message = $@"
                    Atenciosamente,                                            
                    Intelligence Bot";

                List<string> contacts = new List<string>();
                contacts.Add(email.from);

                List<Archive> archives = new List<Archive>();
                List<string> archives_description = new List<string>();
                string logo = string.Empty;
                bool withSheets = false;
                
                var query = await DateExtractor.GetReportTypeCondition(email.body);

                if (query != null)
                {
                    archives.Add(query.Item1);
                    logo = query.Item2.logo;

                    if (query.Item1 == null)
                    {
                        Console.WriteLine($"Não foi possível enviar devido a um erro. [{DateTime.Now.ToString("hh:mm")}]");
                        return;
                    }

                    description = $@"Olá,
                            
    Este robô envia relatórios e alertas de forma autônoma. Por favor, não nos responda. Para quaisquer dúvidas, entre em contato pelo e-mail <strong>ti@unihospitalar.com.br</strong> pelo telefone (81) 3472-7201.

    Para a sua mensagem:

    <strong>""{email.body.ToUpper()}""</strong>
    {Email.GenerateHtmlWithDataTableAndList("Eu entendi que você solicitou os seguintes <strong>PARÂMETROS</strong>:", DateExtractor.getInterpretation(email.body))}
    ";

                    archives_description.Add(query.Item1.description);
                    var archiveList = new List<string>();
                    archiveList.Add(archives[0].description);

                    string str = await Email.SendEmailWithExcelAttachment(contacts, "ti@unihospitalar.com.br", archives[0].titleReport, description, archiveList, bottom_message, logo, archives, withSheets);

                    Log($"E-mail enviado com sucesso para {email.from}.");                    
                }
            }
            catch (Exception ex)
            {
                // Seu código de tratamento de erros aqui
                Log($"Erro ao enviar e-mail para {email.from}: {ex.Message}");                
            }
        }
        private async Task verifySchedule()
        {
            var newList = await SchedulerApp_Schedule.getNotifyList();

            // Primeiro, atualizamos/agregamos itens de 'newList' para 'lScheds'
            foreach (var item in newList)
            {
                var existingItem = lScheds.FirstOrDefault(x => x.id == item.id);

                if (existingItem == null)
                {
                    lScheds.Add(item);
                    qScheds.Enqueue(item);
                    Log($"Novo agendamento: {item.description} definido para {item.hour.Substring(0, 5)}. Agendado para envio.");
                    InsertInOrder($"{item.description} [{item.hour.Substring(0, 5)}]");
                }
                else
                {
                    // Se a hora mudou, atualize-a e ajuste 'lsbSchedule'
                    if (item.hour != existingItem.hour)
                    {
                        var oldItemString = $"{existingItem.description} [{existingItem.hour.Substring(0, 5)}]";
                        var idx = lsbSchedule.Items.IndexOf(oldItemString);

                        if (idx != -1) lsbSchedule.Items[idx] = $"{item.description} [{item.hour.Substring(0, 5)}]";
                        existingItem.hour = item.hour;
                    }

                    // Atualizações adicionais, se necessário...
                }
            }

            // Em seguida, removemos itens que não estão mais em 'newList' de 'lScheds' e 'lsbSchedule'
            for (int i = lScheds.Count - 1; i >= 0; i--)
            {
                var existingItem = lScheds[i];
                if (!newList.Any(x => x.id == existingItem.id))
                {
                    var itemString = $"{existingItem.description} [{existingItem.hour.Substring(0, 5)}]";
                    RemoveFromQueue(existingItem.id);
                    lScheds.RemoveAt(i);
                    lsbSchedule.Items.Remove(itemString);

                    Log($"Agendamento {existingItem.description} removido pois não está presente na lista atualizada.");
                }
            }
        }
        private async Task ProcessAndSendReports()
        {
            Log("Início do processo de envio de agendamentos.");

            List<Task> sendTasks = new List<Task>();
            List<SchedulerApp_Schedule> itemsToProcess = new List<SchedulerApp_Schedule>();

            lock (syncLock)
            {
                foreach (var item in qScheds)
                {
                    if (ShouldSendNow(item))
                    {
                        itemsToProcess.Add(item);
                    }
                }
            }

            lsbSchedule.Invoke((Action)delegate
            {
                foreach (var item in itemsToProcess)
                {
                    int idx = lsbSchedule.Items.IndexOf($"{item.description} [{item.hour.Substring(0, 5)}]");
                    if (idx != -1) lsbSchedule.Items[idx] = $"{item.description} sendo enviado agora...";

                    sendTasks.Add(SendAndDequeueItemAsync(item));
                }
            });

            await Task.WhenAll(sendTasks);

            // Verificando se todos os itens foram enviados corretamente
            bool allSentSuccessfully = sendTasks.All(task => task.Status == TaskStatus.RanToCompletion);
            Log(allSentSuccessfully ? "Todos os agendamentos foram enviados." : "Alguns agendamentos não foram enviados com sucesso.");
        }
        private async Task SendAndDequeueItemAsync(SchedulerApp_Schedule item, bool dequeueAfterSending = true)
        {
            await semaphore.WaitAsync();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Email.getMailCredentials();
                pcbOperation.Visible = true;
                pcbOperation.Style = ProgressBarStyle.Marquee;

                lsbLogs.Items.Add($"Enviando {item.description} para {item.hour.Substring(0, 5)}...");

                /** Get Contacts **/
                var contacts = await SchedulerApp_Contact.getAllToListByCodeAsync(item.id);

                List<string> emailList = new List<string>();

                /** Get Contacts **/
                foreach (var to in contacts)
                {
                    emailList.Add(to.mail);
                }
                /** Body e Bottom messages **/
                string description = $@"Olá,

Este robô envia relatórios e alertas de forma autônoma. Por favor, não nos responda. Para quaisquer dúvidas, entre em contato pelo e-mail pelo telefone (81) 3472-7201.";
                string bottom_message = $@"
                            Atenciosamente,                                            
                            Intelligence Bot";

                /** get Reports **/
                var reports = await SchedulerApp_Report.getAllToListByCodeAsync(item.id);

                List<Archive> archives = new List<Archive>();
                List<string> archives_description = new List<string>();
                string logo = string.Empty;
                bool withSheets = false;
                /** Making reports **/
                int x = 1;
                foreach (var report in reports)
                {

                    withSheets = Convert.ToBoolean(Convert.ToInt16(report.withsheets));
                    var queries = await SchedulerApp_Query.getAllToListByIdAsync(report.id);
                    foreach (var query in queries)
                    {

                        var connections = await SchedulerApp_Connection.getAllToListByidAsync(query.id);
                        foreach (var conn in connections)
                        {
                            var t = await SchedulerApp_Query.ExecuteAsync(query.SQLcode.Replace("`", "'"), conn);
                            if (archives.Where(a => a.description == query.description).FirstOrDefault() == null)
                            {

                                archives.Add(new Archive
                                {
                                    Id = x,
                                    description = query.description

                                    ,
                                    titleReport = report.description
                                    ,
                                    data = t.Item2
                                    ,
                                    format = report.format
                                    ,
                                    query = query.SQLcode
                                });
                                logo = conn.logo;
                                if (t.Item2 == null)
                                {
                                    lsbLogs.Items.Add("Não foi possível enviar devido a um erro " + t.Item1);
                                    return;
                                }
                            }
                        }
                        archives_description.Add(query.description);


                    }
                    x++;
                }

                var archiveList = new List<string>();

                foreach (var archive in archives)
                {
                    archiveList.Add(archive.description);
                }

                string str = await Email.SendEmailWithExcelAttachment(emailList, "ti@unihospitalar.com.br", item.description, description, archiveList, bottom_message, logo, archives, withSheets);

                pcbOperation.Visible = false;

                if (dequeueAfterSending)
                {
                    lock (syncLock)
                    {
                        qScheds = new Queue<SchedulerApp_Schedule>(qScheds.Where(q => q.id != item.id));
                    }
                }

                // Atualizando o status do item em 'lsbSchedule' após o envio bem-sucedido
                int idx = lsbSchedule.Items.IndexOf($"{item.description} sendo enviado agora...");
                if (idx != -1) lsbSchedule.Items[idx] = $"{item.description} enviado com sucesso às {item.hour.Substring(0, 5)}";

                lsbLogs.Items.Add($"Item {item.description} enviado com sucesso às {item.hour.Substring(0, 5)}");
            }
            catch (Exception ex)
            {
                pcbOperation.Visible = false;

                // Atualizando o status do item em 'lsbSchedule' após a falha de envio
                int idx = lsbSchedule.Items.IndexOf($"{item.description} sendo enviado agora...");
                if (idx != -1) lsbSchedule.Items[idx] = $"{item.description} falhou ao enviar às {item.hour.Substring(0, 5)}";

                lsbLogs.Items.Add($"Erro ao enviar {item.description}: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
                semaphore.Release();
            }
        }
        /** Sync Methods **/
        private void SaveListBoxItemsToTxt(ListBox listBox)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files|*.txt";
                sfd.Title = "Save ListBox Items to TXT";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        foreach (var item in listBox.Items)
                        {
                            sw.WriteLine(item.ToString());
                        }
                    }
                }
            }
        }
        private void Log(string message)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            lsbLogs.Invoke((Action) delegate
            {
                lsbLogs.Items.Add($"[{currentTime}] {message}");
            } );
        }
        private void InsertInOrder(string item)
        {
            
            lsbSchedule.Invoke((Action)delegate
            
            {
                try { 
                for (int i = 0; i < lsbSchedule.Items.Count; i++)
                {
                    string currentItem = lsbSchedule.Items[i].ToString();
                    string currentHour = currentItem.Substring(currentItem.IndexOf("[") + 1, 5);

                    if (TimeSpan.Parse(item.Substring(item.IndexOf("[") + 1, 5)) < TimeSpan.Parse(currentHour))
                    {
                        lsbSchedule.Items.Insert(i, item);
                        return;
                    }
                }
                // Se não foi inserido anteriormente, adiciona ao final
                lsbSchedule.Items.Add(item);
                }
                catch (Exception)
                {

                }
            });
        }
        private bool ShouldSendNow(SchedulerApp_Schedule item)
        {
            // Verifique se o horário agendado do item é o minuto atual. 
            // Adapte de acordo com sua lógica de verificação.
            return DateTime.Now.ToString("HH:mm") == item.hour.Substring(0, 5);
        }
        private void RemoveFromQueue(string itemId)
        {
            var tempQueue = new Queue<SchedulerApp_Schedule>();
            while (qScheds.Count > 0)
            {
                var currentItem = qScheds.Dequeue();
                if (currentItem.id != itemId)
                    tempQueue.Enqueue(currentItem);
            }
            qScheds = tempQueue;
        }

        /** Timer Configuration **/
        private void ConfigureTimerProperties()
        {
            timerHour.Interval = 1000;
            timerVerifySchedule.Interval = 90000;
            timerReportByMail.Interval = 30000;
            timerSend.Interval = 60000; // 60.000 milissegundos = 1 minuto

        }
        private void ConfigureTimerAttributes()
        {
            timerHour.Start();
        }
        private void ConfigureTimerEvents()
        {
            timerHour.Tick += timerHour_Tick;
            timerVerifySchedule.Tick += timerVerifySchedule_Tick;
            timerSend.Tick += timerSend_Tick;
            timerReportByMail.Tick += timerReportByMail_Tick;

        }
        private void timerHour_Tick(object sender, EventArgs e)
        {
            txtHour.Invoke((Action)delegate
            {
                txtHour.Text = DateTime.Now.ToLongTimeString();
            });
        }
        private async void timerVerifySchedule_Tick(object sender, EventArgs e)
        {
            await Task.Run(() => verifySchedule());
        }
        private async void timerSend_Tick(object sender, EventArgs e)
        {
            await Task.Run(() => ProcessAndSendReports());
        }
     
        private bool isProcessing = false;
        private async void timerReportByMail_Tick(object sender, EventArgs e)
        {
            if (isProcessing)
                return;

            isProcessing = true;
            await Task.Run(() => sendBySolicitation());
            isProcessing = false;
        }
        /** Form Configuration **/
        private void ConfigureFormProperties()
        {
           

            // Set the form of the system in pixels
            this.Size = new System.Drawing.Size(1178, 543);

            // Set the style of the border form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // This disable the maximaze form
            this.MaximizeBox = false;

            // This start the form in center screen
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void ConfigureFormAttributes()
        {
            this.Name = "Atenas Data Bot v2.0.0";
        }   
        private void ConfigureFormEvents()
        {
            this.Load += frmMain_Load;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            ConfigureTextBoxAttributes();
            ConfigureTimerAttributes();

            ConfigureLabelEvents();
            ConfigureProgressbarEvents();
            ConfigureButtonEvents();
            ConfigureTextBoxEvents();
            ConfigureTimerEvents();
            ConfigureListBoxEvents();
        }

        private void ConfigurePictureBoxProperties()
        {
            pcbRobot.Image = Properties.Resources.giphy;
        }
        /** ListBox **/
        private void ConfigureListBoxEvents()
        {
            lsbLogs.MeasureItem += lsbLogs_MeasureItem;
            lsbLogs.DrawItem += lsbLogs_DrawItem;
        }

        private void lsbLogs_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Calcula a altura necessária para cada item (levando em consideração quebras de linha)
            if (e.Index >= 0 && e.Index < lsbLogs.Items.Count)
            {
                string itemText = lsbLogs.Items[e.Index].ToString();
                SizeF textSize = e.Graphics.MeasureString(itemText, lsbLogs.Font, lsbLogs.ClientSize.Width);
                e.ItemHeight = (int)textSize.Height;
            }
        }

        private void lsbLogs_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Desenha o item com quebras de linha
            if (e.Index >= 0 && e.Index < lsbLogs.Items.Count)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();

                string itemText = lsbLogs.Items[e.Index].ToString();
                e.Graphics.DrawString(itemText, lsbLogs.Font, Brushes.Black, e.Bounds);
            }
        }

        /** Label Configuration **/
        private void ConfigureLabelProperties()
        {
            lblApplicationName.Text = CustomApplication.name;
        }
        private void ConfigureLabelEvents()
        {

        }

        /** ProgressBar Configuration **/
        private void ConfigureProgressbarProperties()
        {
            pcbEmailAnalysis.Visible = false;
            pcbEmailAnalysis.Maximum = 100;
            pcbEmailAnalysis.Style = ProgressBarStyle.Marquee;

            pcbScheduledMail.Visible = false;
            pcbScheduledMail.Maximum = 100;
            pcbScheduledMail.Style = ProgressBarStyle.Marquee;

            pcbOperation.Visible = false;

            //Colors from Progress Bar EmailAnalysis
            pcbEmailAnalysis.ProgressColor = Color.Green;
            pcbEmailAnalysis.ProgressColor2 = Color.SpringGreen;
            pcbEmailAnalysis.BorderRadius = 3;

            //Colors from Progress Bar ScheduledMail
            pcbScheduledMail.ProgressColor = Color.Green;
            pcbScheduledMail.ProgressColor2 = Color.SpringGreen;
            pcbScheduledMail.BorderRadius = 3;          
   
        }
        private void ConfigureProgressbarEvents()
        {

        }

        /** Button Configuration **/
        private void ConfigureButtonProperties()
        {
            //Normal Satate ------------------------------------------------------
            
            //Play buttonHoverState
            btnEmailAnalysis_Play.FillColor = SystemColors.ActiveCaption;
            btnEmailAnalysis_Play.Cursor = Cursors.Hand;
            btnEmailAnalysis_Play.Animated = true;
            
            //Stop ButtonHoverState  
            btnEmailAnalysis_Stop.FillColor = SystemColors.ActiveCaption;
            btnEmailAnalysis_Stop.Cursor = Cursors.Hand;
            btnEmailAnalysis_Stop.Animated = true;

            //Hover state -----------------------------------------------------
            
            //Play buttonHoverState
            btnEmailAnalysis_Play.HoverState.FillColor = Color.FromArgb(92, 184, 92);
            btnEmailAnalysis_Play.Cursor = Cursors.Hand;
            btnEmailAnalysis_Play.Animated = true;
           
            
            //Stop ButtonHoverState
            btnEmailAnalysis_Stop.HoverState.FillColor = Color.LightCoral;
            btnEmailAnalysis_Stop.Cursor = Cursors.Hand;
            btnEmailAnalysis_Stop.Animated = true;


            //Hover State ----------------------------------------------------

            // Schedule Play Button
            btnScheduledMail_Play.HoverState. FillColor = Color.FromArgb(92, 184, 92);
            btnScheduledMail_Play.Cursor = Cursors.Hand;
            btnScheduledMail_Play.Animated = true;

            // Schedule Stop Button
            btnScheduledMail_Stop.HoverState.FillColor = Color.LightCoral;
            btnScheduledMail_Stop.Cursor = Cursors.Hand;
            btnScheduledMail_Stop.Animated = true;

            //Normal State --------------------------------------------------
            
            // Schedule Play Button
            btnScheduledMail_Play.FillColor = SystemColors.ActiveCaption;
            btnScheduledMail_Play.Cursor = Cursors.Hand;
            btnScheduledMail_Play.Animated = true;

            // Schedule Stop Button
            btnScheduledMail_Stop.FillColor = SystemColors.ActiveCaption;
            btnScheduledMail_Stop.Cursor = Cursors.Hand;
            btnScheduledMail_Stop.Animated = true;


            //Main Buttons ---------------------------------------------------

            //Normal state BtnSchedules
            btnSchedules.FillColor = Color.DarkGray;
            btnSchedules.FillColor2 = SystemColors.ActiveCaption;
            btnSchedules.Cursor = Cursors.Hand;
            btnSchedules.HoverState.ForeColor = Color.Black;
            //Normal state BtnContacts
            btnContacts.FillColor = Color.DarkGray;
            btnContacts.FillColor2 = SystemColors.ActiveCaption;
            btnContacts.Cursor = Cursors.Hand;
            btnContacts.HoverState.ForeColor = Color.Black;
            //Normal state Connections
            btnConnections.FillColor = Color.DarkGray;
            btnConnections.FillColor2 = SystemColors.ActiveCaption;
            btnConnections.Cursor = Cursors.Hand;
            btnConnections.HoverState.ForeColor = Color.Black;
            //Normal state BtnReports
            btnReports.FillColor = Color.DarkGray;
            btnReports.FillColor2 = SystemColors.ActiveCaption;
            btnReports.Cursor = Cursors.Hand;
            btnReports.HoverState.ForeColor = Color.Black;
            //Normal state BtnQuerys
            btnQuerys.FillColor = Color.DarkGray;
            btnQuerys.FillColor2 = SystemColors.ActiveCaption;
            btnQuerys.Cursor = Cursors.Hand;
            btnQuerys.HoverState.ForeColor = Color.Black;
            //Normal state btnGeneretor
            btnGenerator.FillColor = Color.DarkGray;
            btnGenerator.FillColor2 = SystemColors.ActiveCaption;
            btnGenerator.Cursor = Cursors.Hand;
            btnGenerator.HoverState.ForeColor = Color.Black;


            //Hover State BtnSchedules ---------------------------------------
            btnSchedules.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            btnSchedules.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            //Hover State Contacts
            btnContacts.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            btnContacts.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            //Hover State Connections
            btnConnections.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            btnConnections.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            //Hover State Reports
            btnReports.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            btnReports.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            //Hover State Querys
            btnQuerys.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            btnQuerys.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            //Hover state Generator
            btnGenerator.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            btnGenerator.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);

            //Export logs and Exit ---------------------------------------------
            btnLogs.FillColor = Color.DarkGray;
            btnLogs.FillColor2 = SystemColors.ActiveCaption;
            btnLogs.Cursor = Cursors.Hand;

            btnExit.FillColor = SystemColors.ActiveCaption;
            btnExit.FillColor2 = Color.DarkGray;
            btnExit.Cursor = Cursors.Hand;

            //Hover State exit -------------------------------------------------
            btnExit.FillColor = SystemColors.ActiveCaption;
            btnExit.HoverState.FillColor2 = Color.Firebrick;
            btnExit.Cursor = Cursors.Hand;

            

        }
        private void ConfigureButtonEvents()
        {
            btnEmailAnalysis_Play.Click += btnEmailAnalysis_Play_Click;
            btnScheduledMail_Play.Click += btnScheduledMail_Play_Click;
            btnEmailAnalysis_Stop.Click += btnEmailAnalysis_Stop_Click;
            btnScheduledMail_Stop.Click += btnScheduledMail_Stop_Click;

            btnLogs.Click += btnLogs_Click;
            btnConnections.Click += btnConnections_Click;
            btnQuerys.Click += btnQuerys_Click;
            btnReports.Click += btnReports_Click;
            btnContacts.Click += btnContacts_Click;
            btnSchedules.Click += btnSchedules_Click;
            btnGenerator.Click += btnGenerator_Click;
            btnExit.Click += btnExit_Click;
        }
        private void btnGenerator_Click(object sender, EventArgs e)
        {
            CustomApplication.ShowOrActivateForm<frmGenerator>();
        }
        private void btnQuerys_Click(object sender, EventArgs e)
        {
            CustomApplication.ShowOrActivateForm<frmQuery>();
        }
        private void btnConnections_Click(object sender, EventArgs e)
        {
            CustomApplication.ShowOrActivateForm<frmConnections>();
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            CustomApplication.ShowOrActivateForm<frmReport>();
        }
        private void btnContacts_Click(object sender, EventArgs e)
        {
            CustomApplication.ShowOrActivateForm<frmContact>();
        }
        private void btnSchedules_Click(object sender, EventArgs e)
        {
            CustomApplication.ShowOrActivateForm<frmSchedule>();
        }
        private void btnEmailAnalysis_Play_Click(object sender, EventArgs e)
        {            
                pcbEmailAnalysis.Visible = true;
                timerReportByMail.Start();
                btnEmailAnalysis_Stop.Enabled = true;
                btnEmailAnalysis_Play.Enabled = false;            
        }
        private void btnScheduledMail_Play_Click(object sender, EventArgs e)
        {
            pcbScheduledMail.Visible = true;
            btnScheduledMail_Play.Enabled = false;
            btnScheduledMail_Stop.Enabled = true;
            timerVerifySchedule.Start();
            timerSend.Start();
        }
        private void btnEmailAnalysis_Stop_Click(object sender, EventArgs e)
        {
            pcbEmailAnalysis.Visible = false;
            timerReportByMail.Stop();
            btnEmailAnalysis_Play.Enabled = true;
            btnEmailAnalysis_Stop.Enabled = false;
        }
        private void btnScheduledMail_Stop_Click(object sender, EventArgs e)
        {
            pcbScheduledMail.Visible = false;
            btnScheduledMail_Play.Enabled = true;
            btnScheduledMail_Stop.Enabled = false;
            timerVerifySchedule.Stop();
            timerSend.Stop();

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja encerrar?", "Sair da aplicação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                Application.Exit();            
        }
        private void btnLogs_Click(object sender, EventArgs e)
        {
            SaveListBoxItemsToTxt(lsbLogs);
        }

        /** Text Box Configuration**/
        private void ConfigureTextBoxProperties()
        {
            txtHour.ReadOnly = true;
            txtHour.TabStop = false;
            txtHour.TextAlign = HorizontalAlignment.Center;
        }
        private void ConfigureTextBoxAttributes()
        {
            txtHour.Text = DateTime.Now.ToLongTimeString();
        }
        private void ConfigureTextBoxEvents()
        {

        }
  
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}
