﻿using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppRouteSession03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository, IWebHostEnvironment env  /*IDepartmentRepository departmentRepository*/)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _env = env;
            //_departmentRepository = departmentRepository;
        }




        [HttpGet]
        public IActionResult Index(string searchInp)
        {
            /// Binding Through View`s Dictionary :  Transfer Data from Action to view
            ///
            /// 1. View Data is a Dictionary Type Property (introduced in ASP.NET Framework 3.5)
            ///     => It Helps Us To Trasnsfer The Data Feom Controller[Action] to view  
            ///
            ///ViewData["Message"] = "Hello View Data";
            TempData.Keep();
            /// 2. View Bag is a dynamic Type Property (introduced in ASP.NET Framework 4.0) based on dynamic Keyword
            ///     => It Helps Us To Trasnsfer The Data Feom Controller[Action] to view  
            ///ViewBag.Message = "Hello View Bag";
            var employees =Enumerable.Empty<Employee>();


            if (string.IsNullOrEmpty(searchInp))
                employees = _employeeRepository.GetAll();
               
         
            else
                employees = _employeeRepository.SearchEmployeesByname(searchInp.ToLower());
            var MappedEpm = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(MappedEpm);

        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                var Count = _employeeRepository.Add(MappedEmp);

                // 3. TempData is Dictinory Type Property (Introduced in ASP.NET Framework 3.5)
                //           => is used to pass data between two consecutive Requestes

                if (Count > 0)
                    TempData["Message"] = "Employee is Created Successfuly";

                else
                    TempData["Message"] = "An Error !! Has Occured, Employee Not Craeted ";

                return RedirectToAction(nameof(Index));


            }
            return View(employeeVm);

        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //400       

            var employee = _employeeRepository.Get(id.Value);
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee is null)
                return NotFound(); //404

            return View(viewName, MappedEmp);
        }

        public IActionResult Edit(int? id)
        {
        
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest("An Error :(");

            }
            if (!ModelState.IsValid)
                return View(employeeVm);
            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _employeeRepository.Update(MappedEmp);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Updating The Employee");

                return View(employeeVm);
            }



        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVm)
        {
            try
            {
                var MappedDept = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _employeeRepository.Delete(MappedDept);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Deleting The Employee");

                return View(employeeVm);
            }
        }

    }
}
