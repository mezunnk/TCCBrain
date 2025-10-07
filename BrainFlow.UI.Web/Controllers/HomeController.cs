using BrainFlow.UI.Web.ViewModels;
using BrainFlow.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrainFlow.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICursoREP _cursoRepository;

        public HomeController(ILogger<HomeController> logger, ICursoREP cursoRepository)
        {
            _logger = logger;
            _cursoRepository = cursoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cursos()
        {
            var cursos = await _cursoRepository.GetAll();
            var viewModel = new CursoIndexViewMOD
            {
                Cursos = cursos,
                PesquisaNomeCurso = ""
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
