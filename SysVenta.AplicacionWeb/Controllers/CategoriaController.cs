﻿using Microsoft.AspNetCore.Mvc;

namespace SysVenta.AplicacionWeb.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}