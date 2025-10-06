using System.ComponentModel.DataAnnotations;

namespace BrainFlow.UI.Web.ViewModels
{
    public class ModuloViewMOD
    {
        public int CdModulo { get; set; }
        public int CdCurso { get; set; }

        [Required(ErrorMessage = "O nome do módulo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NoModulo { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string TxDescricao { get; set; }
    }
}