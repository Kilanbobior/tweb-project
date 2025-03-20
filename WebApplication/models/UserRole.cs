using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}