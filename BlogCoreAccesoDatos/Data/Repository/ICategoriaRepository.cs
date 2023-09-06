﻿using BlogCoreModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public interface ICategoriaRepository:IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategorias();

        void Update(Categoria categoria);
    }
}
