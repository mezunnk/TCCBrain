using BrainFlow.Data.Models;
using BrainFlow.Repository.Interfaces;
using BrainFlow.UI.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrainFlow.UI.Web.Areas.Afiliado.Controllers
{
    [Area("Afiliado")]
    [Authorize(Roles = "2")]
    public class CursosController : Controller
    {
        #region Repositories
        private readonly ICursoREP _cursoREP;
        private readonly IAfiliadoREP _afiliadoREP;
        private readonly IModuloREP _moduloREP;
        #endregion

        #region Constructor
        public CursosController(ICursoREP cursoREP, 
            IAfiliadoREP afiliadoREP,
            IModuloREP moduloREP)
        {
            _cursoREP = cursoREP;
            _afiliadoREP = afiliadoREP;
            _moduloREP = moduloREP;
        }
        #endregion

        #region Methods

        #region Index (Listagem)
        /// <summary>
        /// Action que exibe a lista de cursos do afiliado logado, com filtros.
        /// </summary>
        public async Task<IActionResult> Index(string pesquisaNomeCurso)
         {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);

            var cursos = await _cursoREP.GetByAfiliadoIdFiltered(afiliado.CdAfiliado, pesquisaNomeCurso);

            var viewModel = new CursoIndexViewMOD
            {
                Cursos = cursos,
                PesquisaNomeCurso = pesquisaNomeCurso
            };

            return View(viewModel);
        }
        #endregion

        #region Criar 
        /// <summary>
        /// Exibe o formulário para criar um novo curso.
        /// </summary>
        [HttpGet]
        public IActionResult Criar()
        {
            var viewModel = new CursoDetalhesViewMOD
            {
                Curso = new CursoMOD
                {
                    SnAtivo = true, 
                    DcValor = 0.00M 
                },
                Modulos = new List<ModuloMOD>() 
            };
            return View(viewModel);
        }

        /// <summary>
        /// Processa o formulário de criação de novo curso, incluindo o upload de imagem.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CursoDetalhesViewMOD viewModel)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);

            if (afiliado == null)
            {
                TempData["MensagemErro"] = "Afiliado não encontrado. Verifique seu login.";
                return RedirectToAction("Index");
            }

            viewModel.Curso.CdAfiliado = afiliado.CdAfiliado; 
            viewModel.Curso.SnAtivo = true;
            viewModel.Curso.DtCadastro = DateTime.Now;

            ModelState.Remove("Modulos");
            ModelState.Remove("Curso.CdAfiliadoNavigation");

            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Por favor, corrija os erros no formulário.";
                return View(viewModel);
            }
            await _cursoREP.Add(viewModel.Curso);

            if (viewModel.ImagemUpload != null && viewModel.ImagemUpload.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(viewModel.ImagemUpload.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImagemUpload", "Apenas imagens JPG, JPEG e PNG são permitidas.");
                    TempData["MensagemErro"] = "Formato de imagem inválido.";

                    viewModel.Modulos = new List<ModuloMOD>(); 
                    return View(viewModel);
                }

                if (viewModel.ImagemUpload.Length > 2 * 1024 * 1024) // 2 MB
                {
                    ModelState.AddModelError("ImagemUpload", "O tamanho máximo permitido para a imagem é de 2MB.");
                    TempData["MensagemErro"] = "Imagem muito grande.";
                    viewModel.Modulos = new List<ModuloMOD>();
                    return View(viewModel);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "cursos", viewModel.Curso.CdCurso.ToString());

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.ImagemUpload.CopyToAsync(fileStream);
                }

                viewModel.Curso.TxCaminhoImagem = "/uploads/cursos/" + viewModel.Curso.CdCurso + "/" + uniqueFileName;
                await _cursoREP.Update(viewModel.Curso); 
            }

            TempData["MensagemSucesso"] = "Curso criado com sucesso!";
            return RedirectToAction("Detalhes", new { id = viewModel.Curso.CdCurso });
        }
        #endregion

        #region SalvarDetalhesCurso (Edição do Curso)
        /// <summary>
        /// Salva as alterações nos detalhes do curso, incluindo o upload de imagem.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarDetalhesCurso(CursoDetalhesViewMOD viewModel)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            viewModel.Curso.CdAfiliadoNavigation = await _afiliadoREP.GetByUsuarioId(usuarioId);

            var curso = await _cursoREP.GetById(viewModel.Curso.CdCurso);

            if (curso == null || curso.CdAfiliado != viewModel.Curso.CdAfiliadoNavigation.CdAfiliado)
            {
                TempData["MensagemErro"] = "Acesso negado ou curso não encontrado.";
                return RedirectToAction("Index");
            }

            if (viewModel.ImagemUpload != null && viewModel.ImagemUpload.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(viewModel.ImagemUpload.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImagemUpload", "Apenas imagens JPG, JPEG e PNG são permitidas.");
                    viewModel.Modulos = await _moduloREP.GetByCursoId(viewModel.Curso.CdCurso);
                    TempData["MensagemErro"] = "Formato de imagem inválido.";
                    return View("Detalhes", viewModel);
                }

                if (viewModel.ImagemUpload.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImagemUpload", "O tamanho máximo permitido para a imagem é de 2MB.");
                    viewModel.Modulos = await _moduloREP.GetByCursoId(viewModel.Curso.CdCurso);
                    TempData["MensagemErro"] = "Imagem muito grande.";
                    return View("Detalhes", viewModel);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "cursos", curso.CdCurso.ToString());

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (!string.IsNullOrEmpty(curso.TxCaminhoImagem))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", curso.TxCaminhoImagem.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.ImagemUpload.CopyToAsync(fileStream);
                }

                curso.TxCaminhoImagem = "/uploads/cursos/" + curso.CdCurso + "/" + uniqueFileName;
            }
            else if (viewModel.Curso.TxCaminhoImagem == null && !string.IsNullOrEmpty(curso.TxCaminhoImagem))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", curso.TxCaminhoImagem.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                curso.TxCaminhoImagem = null;
            }

            curso.NoCurso = viewModel.Curso.NoCurso;
            curso.TxDescricao = viewModel.Curso.TxDescricao;
            curso.DcValor = viewModel.Curso.DcValor;

            await _cursoREP.Update(curso);

            TempData["MensagemSucesso"] = "Detalhes do curso salvos com sucesso!";
            return RedirectToAction("Detalhes", new { id = curso.CdCurso });
        }
        #endregion

        #region Detalhes
        /// <summary>
        /// Action para exibir a página de detalhes/gerenciamento de um curso.
        /// </summary>
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _cursoREP.GetById(id.Value);
            if (curso == null) return NotFound();

            // Checagem de segurança (essencial)
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);
            if (curso.CdAfiliado != afiliado.CdAfiliado)
            {
                return Forbid();
            }

            var modulos = await _moduloREP.GetByCursoId(id.Value);

            var viewModel = new CursoDetalhesViewMOD
            {
                Curso = curso,
                Modulos = modulos
            };

            return View(viewModel);
        }
        #endregion

        #region Partials

        #region RemoverImagemCurso (AJAX)
        /// <summary>
        /// Remove a imagem de capa de um curso via AJAX.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverImagemCurso(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);

            var curso = await _cursoREP.GetById(id);

            if (curso == null || curso.CdAfiliado != afiliado.CdAfiliado)
            {
                return Json(new { success = false, message = "Acesso negado ou curso não encontrado." });
            }

            if (!string.IsNullOrEmpty(curso.TxCaminhoImagem))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", curso.TxCaminhoImagem.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                curso.TxCaminhoImagem = null; 
                await _cursoREP.Update(curso); 
            }

            return Json(new { success = true });
        }
        #endregion

        #region _ListaModulos
        /// <summary>
        /// Action que retorna apenas a Partial View da lista de módulos.
        /// Usada para atualização via AJAX.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> _ListaModulos(int cursoId)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);
            var curso = await _cursoREP.GetById(cursoId);

            if (curso == null || curso.CdAfiliado != afiliado.CdAfiliado)
            {
                return Forbid();
            }

            var modulos = await _moduloREP.GetByCursoId(cursoId);
            return PartialView("_PartialListaModulos", modulos);
        }
        #endregion

        #endregion

        #endregion
    }
}