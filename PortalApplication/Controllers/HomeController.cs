using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PortalApplication.Models;
using PortalApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment webHostEnvironment;

        // Inject IEmployeeRepository using Constructor Injection
        public HomeController(IEmployeeRepository employeeRepository,
                                IHostingEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        // Retrieve employee name and return
        public ViewResult Index()
        {
            // Retrive all employees
            var model = _employeeRepository.GetAllEmployees();
            // Pass List of employees to the View
            return View(model);
        }
        [AllowAnonymous]
        // Return .cshtml file
        public ViewResult Details(int? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                // Requested page not found
                Response.StatusCode = 404;
                // Separate view
                return View("EmployeeNotFound", id.Value);
            }
            // Pass data to view using ViewModel
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                PageTitle = "Employee Details",
                Employee = employee
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // // If the Photos property on the incoming model object is not null and if count > 0,
                // // then the user has selected at least one file to upload
                // if (model.Photo != null && model.Photo.Count > 0)
                // {
                //     // Loop thru each selected file
                //     foreach (IFormFile photo in model.Photo)
                //     {
                //         // The file must be uploaded to the images folder in wwwroot
                //         // To get the path of the wwwroot folder we are using the injected
                //         // webHostingEnvironment service provided by ASP.NET Core
                //         string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                //         // To make sure the file name is unique, appending a new
                //         // GUID value and and an underscore to the file name
                //         uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //         string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //         // Use CopyTo() method provided by IFormFile interface to
                //         // copy the file to wwwroot/images folder
                //         photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //     }
                if (model.Photo != null)
                {

                    //path to wwwroot folder
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    employee.PhotoPath = ProcessUploadFile(model);
                }

                // Call update method on the repository service passing it the
                // employee object to update the data in the database table
                Employee updatedEmployee = await _employeeRepository.Update(employee);

                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadFile(EmployeeEditViewModel model)
        {
            string uniqueFileName = null;

            // // If the Photos property on the incoming model object is not null and if count > 0,
            // // then the user has selected at least one file to upload
            // if (model.Photo != null && model.Photo.Count > 0)
            // {
            //     // Loop thru each selected file
            //     foreach (IFormFile photo in model.Photo)
            //     {
            //         // The file must be uploaded to the images folder in wwwroot
            //         // To get the path of the wwwroot folder we are using the injected
            //         // webHostingEnvironment service provided by ASP.NET Core
            //         string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            //         // To make sure the file name is unique, appending a new
            //         // GUID value and and an underscore to the file name
            //         uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            //         string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //         // Use CopyTo() method provided by IFormFile interface to
            //         // copy the file to wwwroot/images folder
            //         photo.CopyTo(new FileStream(filePath, FileMode.Create));
            //     }
            if (model.Photo != null)
            {

                //path to wwwroot folder
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
    }
}
