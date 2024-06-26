﻿using App.DAL.Models;
using AppRouteSession03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        public IQueryable<Department> SearchDepartmentByname(string name);


    }
}
