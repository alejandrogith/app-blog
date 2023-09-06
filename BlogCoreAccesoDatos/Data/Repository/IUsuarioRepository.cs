using BlogCoreModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public interface IUsuarioRepository:IRepository<ApplicationUser>
    {
        void BloquearUsuario(string IdUsuario);
        void DesbloquearUsuario(string IdUsuario);
    }
}
