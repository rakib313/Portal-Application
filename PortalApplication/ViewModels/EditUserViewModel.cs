using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            //Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Contact { get; set; }
        
        //public List<string> Claims { get; set; }
        
        public IList<string> Roles { get; set; }
    }
}
