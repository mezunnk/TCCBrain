using BrainFlow.Data.Models;
using BrainFlow.Repository.Interfaces;
using BrainFlow.UI.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BCrypt.Net;

namespace BrainFlow.UI.Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUsuarioREP _usuarioRepository;
        
        public ContaController(IUsuarioREP usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET: /Conta/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Conta/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewMOD model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Buscar usuário por email
                var usuario = await _usuarioRepository.GetByEmailAsync(model.Email);
                
                if (usuario == null)
                {
                    ModelState.AddModelError("", "E-mail ou senha inválidos.");
                    return View(model);
                }

                // Verificar senha
                var usuarioLogin = usuario.UsuarioLogins.FirstOrDefault();
                if (usuarioLogin == null)
                {
                    ModelState.AddModelError("", "E-mail ou senha inválidos.");
                    return View(model);
                }

                var senhaHash = GerarHashSenha(model.Senha);
                if (usuarioLogin.TxSenhaHash != senhaHash)
                {
                    ModelState.AddModelError("", "E-mail ou senha inválidos.");
                    return View(model);
                }

                // Criar claims do usuário
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.CdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NoUsuario),
                    new Claim(ClaimTypes.Email, usuario.TxEmail),
                    new Claim("TipoUsuario", usuario.CdTipoUsuario.ToString()),
                    new Claim("NomeCompleto", usuario.NoUsuario)
                };

                // Adicionar claims específicos por tipo
                switch (usuario.CdTipoUsuario)
                {
                    case 1:
                        claims.Add(new Claim(ClaimTypes.Role, "Usuario"));
                        break;
                    case 2:
                        claims.Add(new Claim(ClaimTypes.Role, "Afiliado"));
                        break;
                    case 3:
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        break;
                }

                var identity = new ClaimsIdentity(claims, "BrainFlow.Cookie");
                var principal = new ClaimsPrincipal(identity);

                // Fazer login
                await HttpContext.SignInAsync("BrainFlow.Cookie", principal, new AuthenticationProperties
                {
                    IsPersistent = model.LembrarMe,
                    ExpiresUtc = model.LembrarMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(8)
                });

                // Registrar login
                await _usuarioRepository.RegistrarLoginAsync(usuario.CdUsuario);

                // Redirecionar baseado no tipo de usuário
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return usuario.CdTipoUsuario switch
                {
                    2 => RedirectToAction("Index", "Dashboard", new { area = "Afiliado" }),
                    3 => RedirectToAction("Index", "Admin"),
                    _ => RedirectToAction("Index", "Dashboard")
                };
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro interno. Tente novamente.");
                return View(model);
            }
        }

        // GET: /Conta/Registro
        [HttpGet]
        public IActionResult Registro()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        // POST: /Conta/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegisterViewMOD model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Verificar se o email já existe
                var usuarioExistente = await _usuarioRepository.GetByEmailAsync(model.Email);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(model);
                }

                // Criar novo usuário
                var novoUsuario = new UsuarioMOD
                {
                    NoUsuario = model.Nome,
                    TxEmail = model.Email,
                    CdTipoUsuario = model.TipoUsuario,
                    DtCadastro = DateTime.Now,
                    SnAtivo = true
                };

                await _usuarioRepository.CreateAsync(novoUsuario, model.Senha);

                TempData["SuccessMessage"] = "Conta criada com sucesso! Faça login para continuar.";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro ao criar conta. Tente novamente.");
                return View(model);
            }
        }

        // POST: /Conta/Logout
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("BrainFlow.Cookie");
            TempData["InfoMessage"] = "Você foi desconectado com sucesso.";
            return RedirectToAction("Login");
        }

        // GET: /Conta/AcessoNegado
        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        // API: /Conta/GetUserInfo
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Json(new UserInfoViewMOD { IsAuthenticated = false });
            }

            var tipoUsuario = int.Parse(User.FindFirst("TipoUsuario")?.Value ?? "1");
            
            return Json(new UserInfoViewMOD
            {
                IsAuthenticated = true,
                CdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"),
                Nome = User.FindFirst("NomeCompleto")?.Value ?? "",
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

        // Método auxiliar para gerar hash da senha
        private string GerarHashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha + "BrainFlow_Salt_2024"));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}