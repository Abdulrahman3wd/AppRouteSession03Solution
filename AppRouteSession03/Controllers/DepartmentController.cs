using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace AppRouteSession03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inheritance : DepartmentController is a Controller 
        // Composition : DepartmentController has a Department Repository  

        private readonly IDepartmentRepository _departmentRepo; // Null
        public DepartmentController(IDepartmentRepository departmentRepository) // Ask CLR for Creating an object from Class Implementing IDepartmentRepository
        {
            _departmentRepo = departmentRepository;
        }

        // / Department/Index
        public IActionResult Index()
        {
            //  var departments = _departmentRepo.GetAll();
            return View();
        }
    }
}
