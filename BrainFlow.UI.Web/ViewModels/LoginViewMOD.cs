using System.ComponentModel.DataAnnotations;

namespace BrainFlow.UI.Web.ViewModels
{
    public class LoginViewMOD
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }
    }
}