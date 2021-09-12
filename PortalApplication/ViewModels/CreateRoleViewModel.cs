using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.ViewModels
{
    public class CreateRoleViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
