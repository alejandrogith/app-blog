using BlogCoreAccesoDatos.Data;
using BlogCoreAccesoDatos.Data.Repository;
using BlogCoreModels;
using BlogCoreModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ArticulosController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        private readonly IWebHostEnvironment _hostingEnviroment;


        public ArticulosController(IContenedorTrabajo contendorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contendorTrabajo;
            _hostingEnviroment = hostingEnviroment; 
        }


        public IActionResult Index()
        {


            return View();
        }


        [HttpGet]
        public IActionResult Create() {

            var artivm = new ArticuloVM()
            {
                Articulo = new Articulo(),

                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()

            };


            return View(artivm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM) {

            if (ModelState.IsValid) {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (artiVM.Articulo.Id==0 && archivos.Count()>0) { 
                
                  //Nuevo articulo
                  string nombreArchivo =Guid.NewGuid().ToString();
                  var subidas = Path.Combine(rutaPrincipal,@"imagenes\articulos");
                  var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas,nombreArchivo+extension),FileMode.Create )) {

                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\"+nombreArchivo+extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof( Index));
                }

            }
        

            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(artiVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            var artivm = new ArticuloVM()
            {
                Articulo = new Articulo(),

                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()

            };

            if (id!= null) {
                artivm.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault() );
            }


            return View(artivm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id);

                if (archivos.Count()>0)
                {
                    //Editamos Imagen

                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension= Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen)) {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Subimos nuevamente el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {

                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aqui es cuando la imagen existe y no se reemplaza
                    //Debe conservar la quie ya estaba
                    artiVM.Articulo.UrlImagen = articuloDesdeDb.UrlImagen;
                }

                _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }

            return View();
        }


        [HttpDelete]
        public IActionResult Delete(int id) {

           



            var articuloDesdeDB = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostingEnviroment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDB.UrlImagen.TrimStart('\\'));
         

            if(System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeDB==null) {
                return Json( new { success =false,message="Error borrando articulo" } );
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeDB);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Articulo borrado con exito" });
        }



            #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
           
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll( includeProperties:"Categoria" )    });
        }


        #endregion




        

    }
}
