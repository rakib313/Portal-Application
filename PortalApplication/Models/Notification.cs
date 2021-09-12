using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.Models
{
    public class Notification
    {
        //public Notification()
        //{
        //    this.ApplicationUsers = new List<ApplicationUser>();
        //}

        [Key]
        public int Id { get; set; }

        [Required]
        public string Notifcation { get; set; }


        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
