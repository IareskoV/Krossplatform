using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Db
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
