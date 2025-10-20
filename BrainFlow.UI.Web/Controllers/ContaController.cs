using BrainFlow.Data.Models;
using BrainFlow.UI.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BCrypt.Net;

namespace BrainFlow.UI.Web.Controllers
{
    public class ContaController : Controller
    {
        #region Methods

        #region Cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(CadastroUsuarioViewMOD viewModel)
        {
            if (ModelState.IsValid)
            {
                // Simulação - sem banco de dados
                TempData["Modal-Sucesso"] = "Conta criada com sucesso! Você pode fazer login agora.";
                return RedirectToAction("Login", "Conta");
            }
            return View(viewModel);
        }
        #endregion

        #region Login
        /// <summary>
        /// Action para exibir a tela de Login.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Cursos = new List<CursoMOD>(); // Lista vazia para demonstração
            return View();
        }

        /// <summary>
        /// Action para processar a tentativa de login.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewMOD viewModel)
        {
            if (ModelState.IsValid)
            {
                // Simulação de login - aceita qualquer email/senha para demo
                if (!string.IsNullOrEmpty(viewModel.Email) && !string.IsNullOrEmpty(viewModel.Senha))
                {
                    // --- CRIA A AUTENTICAÇÃO (COOKIE) ---
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                        new Claim(ClaimTypes.Name, "Usuário Demo"),
                        new Claim(ClaimTypes.Email, viewModel.Email),
                        new Claim(ClaimTypes.Role, "3") // Usuário comum
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Identity.Login");
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = viewModel.LembrarMe
                    };

                    await HttpContext.SignInAsync("Identity.Login", new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "E-mail ou senha inválidos.");
            }

            return View(viewModel);
        }
        #endregion

        #region Logout
        /// <summary>
        /// Action para fazer o logout do usuário.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Login");
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region BuscarCursos
        /// <summary>
        /// Action para buscar cursos via AJAX.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> BuscarCursos(string termo)
        {
            // Simulação de busca - retorna cursos mock filtrados
            var cursos = new List<CursoMOD>(); // Simulação vazia para demo
            return PartialView("_CursosPartial", cursos);
        }
        #endregion

        #region Solicitar Redefinição de Senha
        /// <summary>
        /// Action para exibir a tela de solicitação de redefinição de senha.
        /// </summary>
        [HttpGet]
        public IActionResult SolicitarRedefinicaoSenha()
        {
            return View();
        }

        /// <summary>
        /// Action para processar a solicitação e gerar o token.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SolicitarRedefinicaoSenha(SolicitarRedefinicaoSenhaViewMOD viewModel)
        {
            if (ModelState.IsValid)
            {
                // Simulação - sempre "envia" o email
                TempData["Modal-Sucesso"] = "Se um usuário com este e-mail estiver cadastrado, um link para redefinição de senha foi enviado para a caixa de entrada.";
                return RedirectToAction("Index","Home");
            }
            return View(viewModel);
        }
        #endregion

        #region Redefinir Senha
        /// <summary>
        /// Action para exibir a tela de cadastro de nova senha, validando o token.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> RedefinirSenha(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                TempData["Modal-Erro"] = "Token inválido ou expirado.";
                return RedirectToAction("Login");
            }

            // Simulação - aceita qualquer token
            var viewModel = new RedefinirSenhaViewMOD { Token = token };
            return View(viewModel);
        }

        /// <summary>
        /// Action para processar a nova senha.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(RedefinirSenhaViewMOD viewModel)
        {
            if (ModelState.IsValid)
            {
                // Simulação - sempre aceita
                TempData["Modal-Sucesso"] = "Sua senha foi redefinida com sucesso!";
                return RedirectToAction("Login");
            }

            return View(viewModel);
        }

        #endregion

        #region API para Autenticação Dinâmica
        
        /// <summary>
        /// API para verificar informações do usuário autenticado (usado pelo JavaScript)
        /// </summary>
        /// <returns>JSON com dados do usuário</returns>
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Json(new { IsAuthenticated = false });
            }

            var tipoUsuario = int.Parse(User.FindFirst("TipoUsuario")?.Value ?? "3");
            
            return Json(new
            {
                IsAuthenticated = true,
                CdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1"),
                Nome = User.FindFirst("NomeCompleto")?.Value ?? "Usuário Demo",
                Email = User.FindFirst(ClaimTypes.Email)?.Value ?? "",
                CdTipoUsuario = tipoUsuario,
                TipoUsuario = tipoUsuario switch
                {
                    1 => "Usuário",
                    2 => "Afiliado", 
                    3 => "Admin",
                    _ => "Usuário"
                },
                IsComum = tipoUsuario == 1,
                IsAfiliado = tipoUsuario == 2,
                IsAdmin = tipoUsuario == 3
            });
        }

        #endregion

        #endregion
    }
}