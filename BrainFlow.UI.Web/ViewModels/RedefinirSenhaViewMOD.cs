using System.ComponentModel.DataAnnotations;

namespace BrainFlow.UI.Web.ViewModels
{
    public class RedefinirSenhaViewMOD
    {
        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmacaoNovaSenha { get; set; }
    }
}