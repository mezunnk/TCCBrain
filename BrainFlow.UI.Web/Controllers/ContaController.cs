using BrainFlow.Data.Models;
using BrainFlow.Repository.Interfaces;
using BrainFlow.UI.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrainFlow.UI.Web.Controllers
{
    public class ContaController : Controller
    {
        #region Context
        private readonly IUsuarioREP _usuarioREP;
        private readonly IEmailService _emailService;
        private readonly ICursoREP _cursoREP;
        #endregion

        #region Constructor
        public ContaController(IUsuarioREP usuarioREP,
            IEmailService emailService,
            ICursoREP cursoREP)
        {
            _usuarioREP = usuarioREP;
            _emailService = emailService;
            _cursoREP = cursoREP;
        }
        #endregion

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
                var usuarioExistente = await _usuarioREP.GetByEmail(viewModel.Email);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(viewModel);
                }

                var novoUsuario = new UsuarioMOD
                {
                    NoUsuario = viewModel.Nome,
                    TxEmail = viewModel.Email,
                    CdTipoUsuario = 3, // 3 = ID do tipo "Usuário Comum" (ajuste se for diferente)
                    SnAtivo = true,
                    DtCadastro = DateTime.Now
                };

                novoUsuario.UsuarioLogins.Add(new UsuarioLoginMOD
                {
                    TxSenhaHash = BCrypt.Net.BCrypt.HashPassword(viewModel.Senha),
                    DtCadastro = DateTime.Now
                });

                await _usuarioREP.Add(novoUsuario);

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
            // Temporariamente comentado para teste do design
            // Busca cursos para exibir na página de login
            // var cursos = await _cursoREP.GetAll();
            // ViewBag.Cursos = cursos.Take(6).ToList(); // Limita a 6 cursos
            ViewBag.Cursos = new List<BrainFlow.Data.Models.CursoMOD>(); // Lista vazia temporária
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
                var usuario = await _usuarioREP.GetByEmailWithLogin(viewModel.Email);

                // Verifica se o usuário existe e se a senha está correta
                if (usuario != null && BCrypt.Net.BCrypt.Verify(viewModel.Senha, usuario.UsuarioLogins.First().TxSenhaHash))
                {
                    // --- CRIA A AUTENTICAÇÃO (COOKIE) ---
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.CdUsuario.ToString()),
                        new Claim(ClaimTypes.Name, usuario.NoUsuario),
                        new Claim(ClaimTypes.Email, usuario.TxEmail),
                        new Claim(ClaimTypes.Role, usuario.CdTipoUsuario.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Identity.Login");
                    var authProperties = new AuthenticationProperties
                    {
                        // Define se o cookie de login persiste após o navegador ser fechado
                        IsPersistent = viewModel.LembrarMe
                    };

                    await HttpContext.SignInAsync("Identity.Login", new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home"); // Redireciona para a página principal após o login
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
            var cursos = await _cursoREP.GetAllFiltered(termo);
            return PartialView("_CursosPartial", cursos.Take(6).ToList());
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
                var usuario = await _usuarioREP.GetByEmailWithLogin(viewModel.Email);
                if (usuario != null)
                {
                    var loginInfo = usuario.UsuarioLogins.First();
                    loginInfo.TxTokenRecuperacao = Guid.NewGuid().ToString();
                    loginInfo.DtValidadeToken = DateTime.Now.AddHours(1);

                    await _usuarioREP.Update(usuario);

                    var link = Url.Action("RedefinirSenha", "Conta", new { token = loginInfo.TxTokenRecuperacao }, Request.Scheme);

                    var emailContent = new Dictionary<string, string>
                    {
                       { "{LINK_BOTAO}", link }

                    };

                    await _emailService.SendEmailAsync(
                        email: usuario.TxEmail,
                        name: usuario.NoUsuario,
                        subject: "BrainFlow | Redefinição de Senha",
                        title: "Redefinição de Senha",
                        message: "", 
                        addData: emailContent
                    );
                }
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

            var usuario = await _usuarioREP.GetByToken(token);
            if (usuario == null)
            {
                TempData["Modal-Erro"] = "Token inválido ou expirado.";
                return RedirectToAction("Login");
            }

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
                var usuario = await _usuarioREP.GetByToken(viewModel.Token);
                if (usuario == null)
                {
                    TempData["Modal-Erro"] = "Token inválido ou expirado. Tente o processo novamente.";
                    return RedirectToAction("Login");
                }

                var loginInfo = usuario.UsuarioLogins.First();
                loginInfo.TxSenhaHash = BCrypt.Net.BCrypt.HashPassword(viewModel.NovaSenha);
                loginInfo.TxTokenRecuperacao = null; // Invalida o token após o uso
                loginInfo.DtValidadeToken = null;

                await _usuarioREP.Update(usuario);

                TempData["Modal-Sucesso"] = "Sua senha foi redefinida com sucesso!";
                return RedirectToAction("Login");
            }

            return View(viewModel);
        }

        #endregion

        #endregion
    }
}