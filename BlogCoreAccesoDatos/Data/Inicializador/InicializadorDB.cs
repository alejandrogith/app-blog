using BlogCore.AccesoDatos.Data;
using BlogCoreModels;
using BlogCoreUtilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializadorDB(ApplicationDbContext db,
         UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager)
        {
            _db= db;
            _userManager= userManager;
            _roleManager = roleManager;
        }

        public void Inicializar()
        {

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }   
                    }
            catch (Exception){}

            if (_db.Roles.Any(ro => ro.Name == CNT.Admin)) return;


            _roleManager.CreateAsync(new IdentityRole(CNT.Admin)).GetAwaiter().GetResult() ;
            _roleManager.CreateAsync(new IdentityRole(CNT.Usuario)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser()
            { 
            UserName="Admin123@",
            Email = "Admin123@",
            EmailConfirmed=true,
            Nombre="AMD"
            },"Admin123@").GetAwaiter().GetResult();

            ApplicationUser usuario=_db.ApplicationUser
                .Where(x => x.Email == "Admin123@")
                .FirstOrDefault();

            _userManager.AddToRoleAsync(usuario,CNT.Admin).GetAwaiter().GetResult();


        }
    }
}
