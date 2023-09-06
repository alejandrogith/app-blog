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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {

        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }



        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;



            _db.SaveChanges(); ;
        }


    }
}
