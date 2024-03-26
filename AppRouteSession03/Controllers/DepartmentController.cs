using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppRouteSession03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;

        // Inheritance : DepartmentController is a Controller 
        // Composition : DepartmentController has a Department Repository  

        private readonly IDepartmentRepository _departmentRepo; // Null
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper, IDepartmentRepository departmentRepository , IWebHostEnvironment env) // Ask CLR for Creating an object from Class Implementing IDepartmentRepository
        {
            _mapper = mapper;
            _departmentRepo = departmentRepository;
            _env = env;
        }

        // / Department/Index
        public IActionResult Index(string searchInp)
        {


            var departments =Enumerable.Empty<Department>();
        

            if (string.IsNullOrEmpty(searchInp))
                departments = _departmentRepo.GetAll();


            else
                departments = _departmentRepo.SearchDepartmentByname(searchInp.ToLower());
            var MappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(MappedDept);


        }

        // Get
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVm)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                //1. Manual mapping
                /// var MappedDept = new Department
                /// {
                ///     Name = departmentVm.Name,
                ///     Code = departmentVm.Code,
                ///     DateOFCreation = departmentVm.DateOFCreation,
                ///  };

                //2. Auto Mapper
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
              var Count =  _departmentRepo.Add(MappedDept);
                if (Count > 0)
                {
                    TempData["Message"] = "Department is Created Successfuly";
                }
                else
                {
                    TempData["Message"] = "An Error !! Has Occured, Department Not Craeted ";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVm);
        }

        public IActionResult Details(int? id, string viewName ="Details")
        {
            if (!id.HasValue)
                return BadRequest();  //400       
            
            var department = _departmentRepo.Get(id.Value);
            var MappedDept = _mapper.Map<Department, DepartmentViewModel>(department);
            if ( department is null)
                return NotFound(); //404
            
            return View(viewName , MappedDept);
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
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id , DepartmentViewModel departmentVm)
        {
            if (id != departmentVm.Id)
            {
                return BadRequest("An Error :(");
                
            }
            if (!ModelState.IsValid)            
                return View(departmentVm);
            
            try
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _departmentRepo.Update(MappedDept);
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

                return View(departmentVm);

            }

        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVm)
        {
            try
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _departmentRepo.Delete(MappedDept);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                // 1. Log Exepction
                // 2. Frirndly Message 

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Deleting The Department");

                return View(departmentVm);
            }


        }

        [HttpGet]

        public IActionResult Delete(int? id) 
        {
            return Details(id);

        }
    }
}
