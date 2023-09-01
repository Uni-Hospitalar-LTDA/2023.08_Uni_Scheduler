using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using _2023._08_Uni_Scheduler.Domain.Entities.Email;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmTeste : Form
    {
        public frmTeste()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ////Console.WriteLine(DateExtractor.GetSqlCondition(textBox1.Text));
            var teste = await SchedulerApp_Contact.getAllToListAllowedContactsAsync();
            Email.getMailCredentials();
            List<string> allowedContacts = new List<string>();
            allowedContacts = teste.Select(contact => contact.mail).ToList();
            var emails = new List<EmailMessage>();
            emails.Clear();
            //emails = Email./*ListEmailTitlesAndSenders*/(allowedContacts);
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Email.getMailCredentials();
                //pcbOperation.Visible = true;
                //pcbOperation.Style = ProgressBarStyle.Marquee;

                string description = string.Empty;
                string bottom_message = $@"
                            Atenciosamente,                                            
                            Intelligence Bot";                

                /** Get Contacts **/
                foreach (var email in emails)
                {
                    List<string> contacts = new List<string>();
                    contacts.Add(email.from);                    
                    List<Archive> archives = new List<Archive>();
                    List<string> archives_description = new List<string>();
                    string logo = string.Empty;
                    bool withSheets = false;

                    /** Making reports **/
                    int x = 1;
                    var query = await DateExtractor.GetReportTypeCondition(email.body);
                    if (query == null)
                    {

                    }
                    else
                    {
                                              
                        archives.Add(query.Item1);
                        logo = query.Item2.logo;
                        if (query.Item1 == null)
                        {
                            Console.WriteLine("Não foi possível enviar devido a um erro.");
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

                    }
                }                
            }

            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

