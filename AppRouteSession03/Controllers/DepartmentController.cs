using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace AppRouteSession03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inheritance : DepartmentController is a Controller 
        // Composition : DepartmentController has a Department Repository  

        private readonly IDepartmentRepository _departmentRepo; // Null
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository , IWebHostEnvironment env) // Ask CLR for Creating an object from Class Implementing IDepartmentRepository
        {
            _departmentRepo = departmentRepository;
            _env = env;
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

        public IActionResult Details(int? id, string viewName ="Details")
        {
            if (!id.HasValue)
                return BadRequest();  //400       
            
            var department = _departmentRepo.Get(id.Value);
            if ( department is null)
                return NotFound(); //404
            
            return View(viewName ,department);
        }

        // /Department/Edit/10
        public IActionResult Edit(int? id) 
        {
            ///if (!id.HasValue)
            ///   return BadRequest(); //400
            ///var department =_departmentRepo.Get(id.Value);
            ///if ( department is null)
            ///    return NotFound();//404
            ///return View(department);
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id , Department department)
        {
            if (id != department.Id)
            {
                return BadRequest("An Error :(");
                
            }
            if (!ModelState.IsValid)            
                return View(department);
            
            try
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                // 1. Log Exeption
                // 2. Friendly Message 

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Updating The Department");

                return View(department);

            }

        }
    }
}
