using Microsoft.AspNetCore.Identity;
using System;

namespace Toyota.Data
{
    public class User : IdentityUser
    {
        public String Tag { get; set; }
    }
}
