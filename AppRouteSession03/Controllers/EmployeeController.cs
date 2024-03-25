using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace AppRouteSession03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepository , IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _env = env;
        }




        [HttpGet]
        public IActionResult Index()
        {
            // Binding Through View`s Dictionary :  Transfer Data from Action to view

            // 1. View Data is a Dictionary Type Property (introduced in ASP.NET Framework 3.5)
            //     => It Helps Us To Trasnsfer The Data Feom Controller[Action] to view  
            //
            ViewData["Message"] = "Hello View Data";

            // 2. View Bag is a dynamic Type Property (introduced in ASP.NET Framework 4.0) based on dynamic Keyword
            //     => It Helps Us To Trasnsfer The Data Feom Controller[Action] to view  
            ViewBag.Message = "Hello View Bag";


            var employee = _employeeRepository.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee) 
        {
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Add(employee);
                    if (Count > 0)
                        return RedirectToAction(nameof(Index));
                
            }
            return View(employee);

        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //400       

            var employee = _employeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound(); //404

            return View(viewName, employee);
        }

        public IActionResult Edit(int? id)
        {

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id , Employee employee )
        {
            if (id != employee.Id)
            {
                return BadRequest("An Error :(");

            }
            if (!ModelState.IsValid)
                return View(employee);
            try
            {
                _employeeRepository.Update(employee);
                return RedirectToAction(nameof(Index)); 


            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Updating The Employee");

                return View(employee);
            }



        }

        [HttpPost]
        public IActionResult Delete(Employee employee) 
        {
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Deleting The Employee");

                return View(employee);
            }
        }

    }
}
