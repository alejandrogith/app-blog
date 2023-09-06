using BlogCoreAccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contTrabajo)
        {
            _contenedorTrabajo = contTrabajo;
        }


        public IActionResult Index() { 

            var claimsIdentity =(ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier  );

            return View(_contenedorTrabajo.Usuario.GetAll(u=>u.Id!=usuarioActual.Value));
        }

        public IActionResult Bloquear(string id)
        {
            if (id==null) {

                return NotFound();
            }

            _contenedorTrabajo.Usuario.BloquearUsuario(id);

            return RedirectToAction(nameof(Index));

        }


        public IActionResult DesBloquear(string id)
        {
            if (id == null)
            {

                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloquearUsuario(id);

            return RedirectToAction(nameof(Index));

        }

    }
}
