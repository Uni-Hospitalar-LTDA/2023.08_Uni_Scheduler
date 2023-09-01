using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using _2023._08_Uni_Scheduler.Configuration;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System.Windows.Forms;
using MailKit.Search;
using System.Collections.Concurrent;
using MailKit.Net.Pop3;

namespace _2023._08_Uni_Scheduler.Domain.Entities.Email
{
    public class Email
    {
        //Lista de atributos do e-mail
        public static List<EmailAttributes> _attributes = new List<EmailAttributes>();
        public static void getMailCredentials()
        {
            //Microsoft 1
            _attributes.Add(new EmailAttributes()
            {
                service = "Gmail"
                ,
                popPort = 995
                ,
                popServer = "pop.gmail.com"
                ,
                smtpPort = 587
                ,
                smtpServer = "smtp.gmail.com"
                ,
                imapPort = 993
                ,
                imapServer = "imap.gmail.com"
                ,
                useSsl = true
                ,
                username = "inteligence@unihospitalar.com.br"
                ,
                password = "!@#asd253"
            });
            //_attributes.Add(new EmailAttributes()
            //{
            //    service = "Hotmail"
            //    ,
            //    popPort = 995
            //    ,
            //    popServer = "outlook.office365.com"
            //    ,
            //    smtpPort = 587
            //    ,
            //    smtpServer = "smtp.office365.com"
            //    ,
            //    imapPort = 993
            //    ,
            //    imapServer = "outlook.office365.com"
            //    ,
            //    useSsl = true
            //    ,
            //    username = "inteligence@unihospitalar.com.br"
            //    //,username = "outlook_862B87F9D425CB48@outlook.com"
            //    ,
            //    password = "!@#asd253"
            //});
            //_attributes.Add(new EmailAttributes()
            //{
            //    service = "Hotmail"
            //    ,
            //    popPort = 995
            //    ,
            //    popServer = "outlook.office365.com"
            //    ,
            //    smtpPort = 587
            //    ,
            //    smtpServer = "smtp.office365.com"
            //    ,
            //    imapPort = 993
            //    ,
            //    imapServer = "outlook.office365.com"
            //    ,
            //    useSsl = true
            //    ,
            //    username = "testeunihospitalar2@hotmail.com"
            //    ,
            //    password = "rfds3142365."
            //});
        }
        public static async Task<string> SendEmailWithExcelAttachment(List<string> toAddresses, string ccAddress, string subject, string bodyContent, List<string> itens, string bottom_message, string logo,
        List<Archive> additionalAttachments, bool withSheet = false)
        {
            /** Get Logo link**/
            string logoUrl = logo;

            // Processa o conteúdo do corpo do e-mail
            string processedBodyContent = Regex.Replace(bodyContent, @"(?<word>\b[A-Z0-9]+\b|\b\d{1,2}/\d{1,2}/\d{2,4}\b)", match =>
            {
                // Se a palavra corresponder ao padrão, envolve-a em tags <strong>
                return $"<strong>{match.Groups["word"].Value}</strong>";
            });

            string processedBottomContent = Regex.Replace(bottom_message, @"(?<word>\b[A-Z0-9]+\b|\b\d{1,2}/\d{1,2}/\d{2,4}\b)", match =>
            {
                return $"<strong>{match.Groups["word"].Value}</strong>";
            });

            // Substitui quebras de linha por <br/>
            processedBodyContent = processedBodyContent.Replace("\n", "<br/>");
            processedBottomContent = processedBottomContent.Replace("\n", "<br/>");
            string body = $@"
                            <html>
                            <body style='font-family: Arial, sans-serif; background-color: #f5f5f5;'>
                                <div style='max-width: 80%; margin: auto; padding: 20px; border-top: 15px solid #661922; border-bottom: 15px solid #661922; background: linear-gradient(to bottom, #e5e5e5, #f5f5f5); box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);'>
                                    <img src='{logoUrl}' alt='Logo' style='display: block; margin: 0 auto; max-width: 200px; width: 100%; height: auto; padding: 20px 0;'>
                                    <div style='color: #333333; text-align: justify;'>
                                        <h1 style='color: #bb001d; font-size: 24px; margin-bottom: 20px;'>{subject}</h1>
                                        <p style='font-size: 14px; color: black; margin-bottom: 20px;'>{processedBodyContent}</p>
                                        {GenerateHtmlWithDataTableAndList("Os anexos deste e-mail são: ", itens)}
                                        <p style='font-size: 14px; color: black; margin-bottom: 20px;'>{"A tecnologia é uma ferramenta que pode ser usada para o bem ou para o mal. Cabe a nós usá-la com sabedoria."}</p>
                                        <div style='text-align: center;'>
                                            <p><strong>{processedBottomContent}</strong></p>
                                        </div>
                                    </div>
                                </div>
                            </body>
                            </html>";

            foreach (var credentials in _attributes)
            {
                var fromAddress = new MailAddress(credentials.username, "Intelligence Bot: A machine Learning do Grupo UNI");
                var toAddress = new MailAddress(toAddresses[0], null);

                var smtp = new SmtpClient
                {
                    Host = credentials.smtpServer,
                    Port = credentials.smtpPort,
                    EnableSsl = credentials.useSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, credentials.password)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    // add additional to addresses
                    for (int i = 1; i < toAddresses.Count; i++)
                    {
                        message.To.Add(toAddresses[i]);
                    }

                    // add cc address
                    message.CC.Add(ccAddress);



                    long totalAttachmentSize = 0;

                    if (withSheet)
                    {
                        var groupedAttachments = additionalAttachments
                            .Where(a => a.format.Contains("E"))
                            .GroupBy(a => a.Id);

                        foreach (var group in groupedAttachments)
                        {
                            var memoryStream = new MemoryStream(Exportation.toByteExcelFromArchives(group.ToList()));
                            totalAttachmentSize += memoryStream.Length;

                            // Reseta o cursor do stream antes de anexar
                            memoryStream.Position = 0;

                            string titleReport = group.First().titleReport;  // Aqui pegamos o titleReport do primeiro item do grupo.
                            string fileName = $"{titleReport.replaceSpecialCharacters()}_{group.Key}.xlsx";
                            message.Attachments.Add(new Attachment(memoryStream, fileName));
                        }
                    }

                    //add additional attachments
                    if (additionalAttachments != null)
                    {
                        foreach (var attachment in additionalAttachments)
                        {
                            MemoryStream stream = null;
                            if (attachment.format.Contains("X"))
                            {
                                var xmlFilePath = Exportation.toXmlWithPath(attachment.data, attachment.description);
                                stream = new MemoryStream(File.ReadAllBytes(xmlFilePath));
                                totalAttachmentSize += stream.Length;

                                // Reseta o cursor do stream antes de anexar
                                stream.Position = 0;
                                message.Attachments.Add(new Attachment(stream, $"{attachment.description}.xml"));
                            }
                            if (attachment.format.Contains("J"))
                            {
                                var jsonFilePath = Exportation.toJsonWithPath(attachment.data, attachment.description);
                                stream = new MemoryStream(File.ReadAllBytes(jsonFilePath));
                                totalAttachmentSize += stream.Length;

                                // Reseta o cursor do stream antes de anexar
                                stream.Position = 0;
                                message.Attachments.Add(new Attachment(stream, $"{attachment.description}.json"));
                            }
                            if (!withSheet && attachment.format.Contains("E"))
                            {
                                stream = new MemoryStream(Exportation.toByteExcelFromArchive(attachment));
                                totalAttachmentSize += stream.Length;

                                // Reseta o cursor do stream antes de anexar
                                stream.Position = 0;
                                message.Attachments.Add(new Attachment(stream, $"{attachment.description}.xlsx"));
                            }
                        }
                    }

                    const long fiftyMB = 50L * 1024 * 1024;
                    if (totalAttachmentSize > fiftyMB)
                    {
                        return "Erro: A soma dos tamanhos dos anexos excede 50 MB.";
                    }

                    try
                    {
                        await smtp.SendMailAsync(message);
                        foreach (var i in toAddresses)
                            Console.WriteLine("email enviado para " + i);
                        return "Notificado";
                    }
                    catch (SmtpException ex)
                    {
                        Console.WriteLine($"Falha de SMTP com as credenciais {credentials.service}- {credentials.username}: " + ex.Message);
                    }
                }
            }

            return "Falha de SMTP: Todas as tentativas com diferentes credenciais falharam.";
        }
        public static string GenerateHtmlWithDataTableAndList(string title, List<string> itemList)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div style='margin-top: 20px; margin-bottom: 20px;'>");

            // Título da lista
            htmlBuilder.Append($"<p style='font-size: 14px; color: black; font-weight: bold;'>{title}</p>");

            htmlBuilder.Append("<ul style='list-style-type: disc; margin-left: 20px;'>");

            foreach (string item in itemList)
            {
                htmlBuilder.Append("<li style='font-size: 12px;'><strong>").Append(item).Append("</strong></li>");
            }

            htmlBuilder.Append("</ul>");
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }
        public static string ConvertDataTableToHtml(DataTable dt)
        {
            string html = null;
            if (dt != null)
            {
                html = "<table style='border: solid 1px #DDD; width: 100%;'>";
                //add header row
                html += "<tr style='background-color: #f2f2f2;'>";

                for (int i = 0; i < dt.Columns.Count; i++)
                    html += $"<th style='padding: 10px; border: solid 1px #DDD; color: #800020;'>{dt.Columns[i].ColumnName}</th>";
                html += "</tr>";
                //add rows
                decimal total = 0m;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        object cellData = dt.Rows[i][j];
                        if (cellData is DateTime)
                        {
                            html += $"<td style='padding: 10px; border: solid 1px #DDD;'>{((DateTime)cellData).ToString("dd-MM-yyyy")}</td>";
                        }
                        else if (cellData is decimal)
                        {
                            decimal cellValue = (decimal)cellData;
                            total += cellValue;
                            html += $"<td style='padding: 10px; border: solid 1px #DDD; text-align: right;'>{cellValue.ToString("C")}</td>";
                        }
                        else
                        {
                            html += $"<td style='padding: 10px; border: solid 1px #DDD;'>{cellData.ToString()}</td>";
                        }
                    }
                    html += "</tr>";
                }
                //add total row
                html += "<tr>";
                html += $"<td colspan='{(dt.Columns.Count - 1)}' style='padding: 10px; border: solid 1px #DDD; text-align: right;'><strong>Total:</strong></td>";
                html += $"<td style='padding: 10px; border: solid 1px #DDD; text-align: right;'><strong>{total.ToString("C")}</strong></td>";
                html += "</tr>";
                html += "</table>";
            }
            return html;
        }
        public static List<EmailMessage> ListImapEmailTitlesAndSenders(List<string> mailList)
        {
            ConcurrentBag<EmailMessage> emailMessages = new ConcurrentBag<EmailMessage>();

            foreach (var credentials in _attributes)
            {
                try
                {
                    using (var client = new ImapClient())
                    {
                        client.Connect(credentials.imapServer, credentials.imapPort, credentials.useSsl);
                        client.Authenticate(credentials.username, credentials.password);

                        var inbox = client.Inbox;
                        inbox.Open(MailKit.FolderAccess.ReadWrite);

                        // Busque apenas as mensagens não lidas
                        var unreadMessages = inbox.Search(SearchQuery.NotSeen);

                        Console.WriteLine($"Total unread messages in {credentials.username}: {unreadMessages.Count}");

                        foreach (var uid in unreadMessages)
                        {
                            var message = inbox.GetMessage(uid);
                            var senderEmail = message.From.Mailboxes.FirstOrDefault()?.Address;

                            if (senderEmail != null && mailList.Contains(senderEmail))
                            {
                                EmailMessage emailMessage = new EmailMessage
                                {
                                    id = message.MessageId,
                                    subject = message.Subject,
                                    body = message.TextBody,
                                    from = senderEmail
                                };

                                emailMessages.Add(emailMessage);

                                // Mark the message as read
                                inbox.AddFlags(uid, MessageFlags.Seen, true);
                            }
                        }

                        // Expunge to commit changes
                        inbox.Expunge();

                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch emails from {credentials.username}: " + ex.Message);
                }
            }

            return emailMessages.ToList();
        }
        public static List<EmailMessage> ListPopEmailTitlesAndSenders(List<string> mailList)
        {
            ConcurrentBag<EmailMessage> emailMessages = new ConcurrentBag<EmailMessage>();

            foreach (var credentials in _attributes)
            {
                try
                {
                    using (var client = new Pop3Client())
                    {
                        client.Connect(credentials.popServer, credentials.popPort, credentials.useSsl);
                        client.Authenticate(credentials.username, credentials.password);

                        int messageCount = client.GetMessageCount();
                        for (int i = 0; i < messageCount; i++)
                        {
                            var message = client.GetMessage(i);
                            var senderEmail = message.From.Mailboxes.FirstOrDefault()?.Address;

                            if (senderEmail != null && mailList.Contains(senderEmail))
                            {
                                EmailMessage emailMessage = new EmailMessage
                                {
                                    id = message.MessageId,
                                    subject = message.Subject,
                                    body = message.TextBody,
                                    from = senderEmail
                                };

                                emailMessages.Add(emailMessage);

                                // No need to mark as read, POP3 doesn't have that concept
                            }
                        }

                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch emails from {credentials.username}: " + ex.Message);
                }
            }

            return emailMessages.ToList();
        }
    }

    public class EmailMessage 
    {
        public string id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string from { get; set; }
    }
}
