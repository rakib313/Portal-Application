using Microsoft.AspNetCore.Http;
using PortalApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        public string Name { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Required(ErrorMessage = "Please provide Valid for Email field")]
        public string Email { get; set; }
        public Dept? Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
