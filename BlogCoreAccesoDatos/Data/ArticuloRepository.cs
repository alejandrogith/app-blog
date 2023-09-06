using BlogCore.AccesoDatos.Data;
using BlogCoreAccesoDatos.Data.Repository;
using BlogCoreModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {

        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

   

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;



            _db.SaveChanges(); ;
        }


    }
}
