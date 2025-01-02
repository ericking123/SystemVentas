using Microsoft.AspNetCore.Mvc;

namespace SysVenta.AplicacionWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
