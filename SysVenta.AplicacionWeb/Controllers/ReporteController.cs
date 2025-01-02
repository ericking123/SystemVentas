using Microsoft.AspNetCore.Mvc;

namespace SysVenta.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
