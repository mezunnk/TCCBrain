using System.ComponentModel.DataAnnotations;

namespace BrainFlow.UI.Web.ViewModels
{
    public class SolicitarRedefinicaoSenhaViewMOD
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }
    }
}