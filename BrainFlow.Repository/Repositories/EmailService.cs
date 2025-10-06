using BrainFlow.Data.Common;
using BrainFlow.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BrainFlow.Repository.Repositories
{
    public class EmailService : IEmailService
    {
        #region Properties
        private readonly EmailSettings _emailSettings;
        private ILogger<EmailService> _logger;
        #endregion

        #region Constructor
        public EmailService(IOptions<EmailSettings> emailSettings,
            ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region SendEmailAsync
        /// <summary>
        /// Enviar um email e preenche os parametros no template padrão
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="addData"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string name, string subject, string title, string message, Dictionary<string, string> addData)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.FromEmail, "BrainFlow | Não Responder")
                };

                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = PopulateBody(name, title, message, addData);
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao enviar email - {ex.Message}");
            }
        }
        #endregion

        #region Helpers

        #region PopulateBody
        private string PopulateBody(string name, string title, string message,  Dictionary<string, string> addData)
        {
            string path = "wwwroot/Template/EmailRedefinirSenha.html";
            string body = File.ReadAllText(path);

            body = body.Replace("{NOME}", name);
            body = body.Replace("{ANO_ATUAL}", DateTime.Now.Year.ToString());
            body = body.Replace("{TITULO_EMAIL}", title);
            body = body.Replace("{TEXTO}", message);

            if (addData.ContainsKey("{LINK_BOTAO}"))
            {
                body = body.Replace("{LINK_BOTAO}", addData["{LINK_BOTAO}"]);
            }
            string infoTableHtml = string.Empty;
            if (addData.Count > 0)
            {
                foreach(var item in addData)
                {
                    infoTableHtml += $"<tr><td><strong>{item.Key}: </strong></td><td>{item.Value}</td></tr>";
                }
            }
            body = body.Replace("{LISTA_INFORMACOES}", infoTableHtml);

            return body;
        }
        #endregion

        #endregion

        #endregion
    }
}