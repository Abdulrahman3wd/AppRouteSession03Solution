using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        int Complete();
    }
}
