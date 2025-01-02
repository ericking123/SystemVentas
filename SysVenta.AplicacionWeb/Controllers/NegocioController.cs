using Microsoft.AspNetCore.Mvc;

namespace SysVenta.AplicacionWeb.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
