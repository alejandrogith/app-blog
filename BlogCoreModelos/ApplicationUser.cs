using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreModels
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage="El nombre es obligatoria")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El pais es obligatoria")]
        public string Pais { get; set; }


    }
}
