using BlogCoreAccesoDatos.Migrations;
using BlogCoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public interface ISliderRepository:IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
