// Exemplo de como implementar headers dinâmicos no ASP.NET Core

// 1. ViewModel para o Header
public class HeaderViewModel
{
    public bool IsAuthenticated { get; set; }
    public string UserType { get; set; } // "User", "Affiliate", "Admin"
    public string UserName { get; set; }
}

// 2. Partial View para o Header (_Header.cshtml)
@model HeaderViewModel

<header class="nav">
  <div class="wrap">
    <div class="brand">
      <img src="~/img/logo_branco.png" width="128" height="128" alt="Brain Flow">
    </div>
    <nav class="menu">
      <a href="@Url.Action("Index", "Home")">Home</a>
      
      @if (!Model.IsAuthenticated)
      {
        <!-- Header para visitantes -->
        <a href="@Url.Action("Index", "Cursos")">Cursos</a>
        <a href="@Url.Action("Login", "Conta")">Login</a>
        <a class="signup" href="@Url.Action("Cadastro", "Conta")">Cadastro</a>
      }
      else if (Model.UserType == "Admin")
      {
        <!-- Header para Admin -->
        <a href="@Url.Action("Index", "Cursos")">Cursos</a>
        <a href="@Url.Action("Dashboard", "Admin")">Admin</a>
        <a class="signup" href="@Url.Action("Logout", "Conta")">Sair</a>
      }
      else if (Model.UserType == "Affiliate")
      {
        <!-- Header para Afiliado -->
        <a href="@Url.Action("Dashboard", "Afiliado")">Dashboard</a>
        <a href="@Url.Action("MeusLinks", "Afiliado")">Meus Links</a>
        <a class="signup" href="@Url.Action("Logout", "Conta")">Sair</a>
      }
      else
      {
        <!-- Header para Usuário comum -->
        <a href="@Url.Action("Index", "Cursos")">Cursos</a>
        <a href="@Url.Action("Perfil", "Usuario")">Perfil</a>
        <a class="signup" href="@Url.Action("Logout", "Conta")">Sair</a>
      }
    </nav>
  </div>
</header>

// 3. Base Controller para popular o Header
public class BaseController : Controller
{
    protected HeaderViewModel GetHeaderViewModel()
    {
        return new HeaderViewModel
        {
            IsAuthenticated = User.Identity.IsAuthenticated,
            UserType = User.IsInRole("Admin") ? "Admin" : 
                      User.IsInRole("Affiliate") ? "Affiliate" : "User",
            UserName = User.Identity.Name
        };
    }
}

// 4. Usar em qualquer Controller
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Header = GetHeaderViewModel();
        return View();
    }
}

// 5. No Layout principal (_Layout.cshtml)
@{
    var headerModel = ViewBag.Header as HeaderViewModel ?? new HeaderViewModel();
}

<partial name="_Header" model="headerModel" />