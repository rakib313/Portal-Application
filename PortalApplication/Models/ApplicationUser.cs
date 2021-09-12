
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.Models
{
    public class ApplicationUser : IdentityUser
    {

        //public ApplicationUser()
        //{
        //    this.Notifications = new List<Notification>();
        //}
        //public async Task GenerateUserIdentityAsync(UserManager manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        public string Address { get; set; }

        //[Required]
        //[StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        //[DataType(DataType.PhoneNumber)]
        public int Contact { get; set; }

        //public virtual ICollection<Notification> Notifications { get; set; }

    }
}
