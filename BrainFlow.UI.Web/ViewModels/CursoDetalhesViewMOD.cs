using BrainFlow.Data.Models;

namespace BrainFlow.UI.Web.ViewModels
{
    public class CursoDetalhesViewMOD
    {
        public CursoMOD Curso { get; set; }
        public List<ModuloMOD> Modulos { get; set; }
        public IFormFile ImagemUpload { get; set; } 

    }
}