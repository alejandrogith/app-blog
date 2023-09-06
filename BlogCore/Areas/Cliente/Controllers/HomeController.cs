using BlogCore.Models;
using BlogCoreAccesoDatos.Data.Repository;
using BlogCoreModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _IContenedorTrabajo;

        public HomeController(IContenedorTrabajo IContenedorTrabajo)
        {
            _IContenedorTrabajo = IContenedorTrabajo;
        }

        public IActionResult Index()
        {

            var homeVm = new HomeVM() {
                Sliders = _IContenedorTrabajo.Slider.GetAll(),
                ListaArticulos = _IContenedorTrabajo.Articulo.GetAll(),
            };

            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            var articuloDesdeDb = _IContenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == id);
            return View(articuloDesdeDb);
        }
    }
}
