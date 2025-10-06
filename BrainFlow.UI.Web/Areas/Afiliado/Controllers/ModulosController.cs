using BrainFlow.Data.Models;
using BrainFlow.Repository.Interfaces;
using BrainFlow.UI.Web.ViewModels; // Se você tiver ViewModels específicas para Módulos, adicione aqui
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Para User.FindFirstValue

namespace BrainFlow.UI.Web.Areas.Afiliado.Controllers
{
    [Area("Afiliado")]
    [Authorize]
    public class ModulosController : Controller
    {
        #region Repositores
        private readonly IModuloREP _moduloREP;
        private readonly ICursoREP _cursoREP; 
        private readonly IAfiliadoREP _afiliadoREP;
        #endregion

        #region Constructor
        public ModulosController(IModuloREP moduloREP, ICursoREP cursoREP, IAfiliadoREP afiliadoREP)
        {
            _moduloREP = moduloREP;
            _cursoREP = cursoREP;
            _afiliadoREP = afiliadoREP;
        }
        #endregion

        #region Methods

        #region SalvarModulo
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> SalvarModulo([FromBody] ModuloViewMOD viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dados inválidos.", errors = ModelState.ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()) });
            }

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);
            if (afiliado == null) return Json(new { success = false, message = "Afiliado não encontrado." });

            var curso = await _cursoREP.GetById(viewModel.CdCurso);
            if (curso == null || curso.CdAfiliado != afiliado.CdAfiliado)
            {
                return Json(new { success = false, message = "Acesso negado ao curso." });
            }

            if (viewModel.CdModulo == 0)
            {
                var novoModulo = new ModuloMOD
                {
                    NoModulo = viewModel.NoModulo,
                    TxDescricao = viewModel.TxDescricao,
                    CdCurso = viewModel.CdCurso,
                    NrOrdem = (await _moduloREP.GetByCursoId(viewModel.CdCurso)).Count + 1
                };
                await _moduloREP.Add(novoModulo);
                TempData["Modal-Sucesso"] = "Módulo adicionado com sucesso!";
            }
            else
            {
                var moduloExistente = await _moduloREP.GetById(viewModel.CdModulo);
                if (moduloExistente == null || moduloExistente.CdCurso != viewModel.CdCurso)
                {
                    return Json(new { success = false, message = "Módulo não encontrado ou acesso negado." });
                }

                moduloExistente.NoModulo = viewModel.NoModulo;
                moduloExistente.TxDescricao = viewModel.TxDescricao;
                await _moduloREP.Update(moduloExistente);
                TempData["Modal-Sucess"] = "Módulo atualizado com sucesso!";
            }

            return Json(new { success = true });
        }
        #endregion

        #region RemoverModulo
        [HttpPost]
        public async Task<IActionResult> RemoverModulo(int id)
        {
            var modulo = await _moduloREP.GetById(id);
            if (modulo == null) return Json(new { success = false, message = "Módulo não encontrado." });

            var curso = await _cursoREP.GetById(modulo.CdCurso);
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);

            if (curso.CdAfiliado != afiliado.CdAfiliado)
            {
                return Json(new { success = false, message = "Acesso Negado." });
            }

            await _moduloREP.Delete(id);

            return Json(new { success = true });
        }
        #endregion

        #region ReordenarModulos
        [HttpPost]
        public async Task<IActionResult> ReordenarModulos(int cursoId, [FromBody] List<int> modulosIds)
        {
            var curso = await _cursoREP.GetById(cursoId);
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var afiliado = await _afiliadoREP.GetByUsuarioId(usuarioId);

            if (curso == null || curso.CdAfiliado != afiliado.CdAfiliado)
            {
                return Json(new { success = false, message = "Acesso Negado." });
            }

            await _moduloREP.Reordenar(cursoId, modulosIds);

            return Json(new { success = true });
        }
        #endregion

        #endregion

    }
}