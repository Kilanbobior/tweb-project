using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string PasswordHash { get; set; }

        public Guid? RoleIds { get; set; }

        [ForeignKey("RoleIds")]
        public virtual Role Role { get; set; } // Добавьте это свойство

        

        [ForeignKey("RoleIds")]
        

        // Обратная связь с заказами
        public virtual ICollection<Order> Orders { get; set; }
    }
}
}  