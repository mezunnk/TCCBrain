using System.ComponentModel.DataAnnotations;

namespace BrainFlow.UI.Web.ViewModels
{
    public class CadastroUsuarioViewMOD
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmacaoSenha { get; set; }
    }
}
