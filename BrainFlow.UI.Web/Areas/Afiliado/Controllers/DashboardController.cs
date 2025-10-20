using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BrainFlow.Repository.Interfaces;
using System.Security.Claims;

namespace BrainFlow.UI.Web.Areas.Afiliado.Controllers
{
    [Area("Afiliado")]
    [Authorize(Policy = "AfiliadoOrAdmin")]
    public class DashboardController : Controller
    {
        #region Properties
        private readonly ICursoREP? _cursoRepository;
        private readonly IModuloREP? _moduloRepository;
        #endregion

        #region Constructor
        public DashboardController(ICursoREP? cursoRepository = null, IModuloREP? moduloRepository = null)
        {
            _cursoRepository = cursoRepository;
            _moduloRepository = moduloRepository;
        }
        #endregion

        #region Methods

        #region Index
        /// <summary>
        /// Dashboard principal do Afiliado - estatísticas e visão geral
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                // Pega o ID do usuário logado
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Conta", new { area = "" });
                }

                // Dados de exemplo para o dashboard (sem banco)
                if (_cursoRepository == null)
                {
                    ViewBag.TotalCursos = 5;
                    ViewBag.CursosAtivos = 3;
                    ViewBag.CursosInativos = 2;
                    ViewBag.NomeUsuario = User.FindFirst(ClaimTypes.Name)?.Value ?? "Afiliado";
                    return View();
                }

                // Busca estatísticas do afiliado (com banco)
                var cursos = await _cursoRepository.GetAll();
                var totalCursos = cursos.Count();
                var cursosAtivos = cursos.Count(c => c.SnAtivo);

                // Dados para o dashboard
                ViewBag.TotalCursos = totalCursos;
                ViewBag.CursosAtivos = cursosAtivos;
                ViewBag.CursosInativos = totalCursos - cursosAtivos;
                ViewBag.NomeUsuario = User.FindFirst(ClaimTypes.Name)?.Value ?? "Afiliado";

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar dashboard: " + ex.Message;
                // Dados de fallback
                ViewBag.TotalCursos = 0;
                ViewBag.CursosAtivos = 0;
                ViewBag.CursosInativos = 0;
                ViewBag.NomeUsuario = User.FindFirst(ClaimTypes.Name)?.Value ?? "Afiliado";
                return View();
            }
        }
        #endregion

        #region Estatisticas
        /// <summary>
        /// Página de estatísticas detalhadas
        /// </summary>
        public async Task<IActionResult> Estatisticas()
        {
            try
            {
                // Dados de exemplo para estatísticas (sem banco)
                if (_cursoRepository == null)
                {
                    ViewBag.TotalCursos = 5;
                    ViewBag.CursosComModulos = 4;
                    ViewBag.MediaModulosPorCurso = 3.2;
                    ViewBag.CursosAtivos = 3;
                    ViewBag.CursosInativos = 2;
                    return View();
                }

                // Busca estatísticas reais (com banco)
                var cursos = await _cursoRepository.GetAll();
                
                ViewBag.TotalCursos = cursos.Count();
                ViewBag.CursosComModulos = cursos.Count(c => c.Modulos != null && c.Modulos.Any());
                ViewBag.MediaModulosPorCurso = cursos.Any() ? 
                    cursos.Where(c => c.Modulos != null).Average(c => c.Modulos.Count()) : 0;
                ViewBag.CursosAtivos = cursos.Count(c => c.SnAtivo);
                ViewBag.CursosInativos = cursos.Count() - cursos.Count(c => c.SnAtivo);

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar estatísticas: " + ex.Message;
                // Dados de fallback
                ViewBag.TotalCursos = 0;
                ViewBag.CursosComModulos = 0;
                ViewBag.MediaModulosPorCurso = 0;
                ViewBag.CursosAtivos = 0;
                ViewBag.CursosInativos = 0;
                return View();
            }
        }
        #endregion

        #endregion
    }
}