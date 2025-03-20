using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Guid RoleId { get; set; }
    }
}  