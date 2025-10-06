namespace BrainFlow.Repository.Interfaces
{
    public interface IEmailService
    {
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
        Task SendEmailAsync(string email, string name, string subject, string title, string message, Dictionary<string, string> addData);
    }
}
