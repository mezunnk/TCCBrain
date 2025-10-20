using System.ComponentModel.DataAnnotations;

namespace BrainFlow.UI.Web.ViewModels
{
    public class RegisterViewMOD
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de usuário.")]
        [Display(Name = "Tipo de Usuário")]
        public int TipoUsuario { get; set; } = 1; // 1 = Comum, 2 = Afiliado, 3 = Admin

        [Display(Name = "Aceito os termos de uso")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Você deve aceitar os termos de uso.")]
        public bool AceitarTermos { get; set; }
    }
}