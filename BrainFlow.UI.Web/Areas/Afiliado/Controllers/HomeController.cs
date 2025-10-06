using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainFlow.UI.Web.Areas.Afiliado.Controllers
{
    [Area("Afiliado")]
    [Authorize(Roles = "2")]
    public class HomeController : Controller
    {
        #region Methods

        #region Index
        /// <summary>
        /// Action para exibir o painel principal do Afiliado.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #endregion
    }
}
