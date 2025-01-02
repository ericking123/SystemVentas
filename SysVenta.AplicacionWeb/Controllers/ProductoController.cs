using Microsoft.AspNetCore.Mvc;

namespace SysVenta.AplicacionWeb.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
