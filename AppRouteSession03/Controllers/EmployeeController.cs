﻿using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL.Helpers;
using AppRouteSession03.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace AppRouteSession03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            //IEmployeeRepository employeeRepository,
            IWebHostEnvironment env 
            /*IDepartmentRepository departmentRepository*/)
        {
            _mapper = mapper;     
            _unitOfWork = unitOfWork;
            _env = env;
            //_departmentRepository = departmentRepository;
        }




        [HttpGet]
        public async Task <IActionResult> Index(string searchInp)
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
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;


            if (string.IsNullOrEmpty(searchInp))
                employees = await employeeRepo.GetAllAsync();
               
         
            else
                employees = employeeRepo.SearchEmployeesByname(searchInp.ToLower());
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
        public async Task< IActionResult>Create(EmployeeViewModel employeeVm)
        {

            if (ModelState.IsValid)
            {
               employeeVm.ImageName = await DocumentSetting.UploadFile(employeeVm.Image, "images");

                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.Repository<Employee>().Add(MappedEmp);

                // Update Depatmrnt 
                // _unitofwork.DepartmentRepository.Update(departmrnt)

                // 3. TempData is Dictinory Type Property (Introduced in ASP.NET Framework 3.5)
                //           => is used to pass data between two consecutive Requestes
                var Count = await _unitOfWork.Complete();
                if (Count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfuly";
                }

                else
                    TempData["Message"] = "An Error !! Has Occured, Employee Not Craeted ";

                return RedirectToAction(nameof(Index));


            }
            return View(employeeVm);

        }

        [HttpGet]
        public async Task< IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //400       

            var employee = await _unitOfWork.Repository<Employee>().GetAsync(id.Value);
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee is null)
                return NotFound(); //404

            return View(viewName, MappedEmp);
        }

        public async Task< IActionResult> Edit(int? id)
        {
        
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVm)
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
                _unitOfWork.Repository<Employee>().Update(MappedEmp);
               await _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVm)
        {
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;
            try
            {
                employeeRepo.Delete(_mapper.Map<EmployeeViewModel, Employee>(employeeVm));
                var Count = await _unitOfWork.Complete();
                if( Count > 0)
                {
                    DocumentSetting.DeleteFile(employeeVm.ImageName, "images");
                    return RedirectToAction(nameof(Index));

                }
                return View(employeeVm);


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
