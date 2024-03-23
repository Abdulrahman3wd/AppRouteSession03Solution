using App.DAL.Models;
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
            var departments = _departmentRepo.GetAll();
            return View(departments);
        }

        // Get
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
              var Count =  _departmentRepo.Add(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();  //400       
            
            var department = _departmentRepo.Get(id.Value);
            if ( department is null)
                return NotFound(); //404
            
            return View(department);
        }
    }
}
