using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainFlow.UI.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}