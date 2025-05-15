using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class AdminDashboardViewModel
    {
        public int UsersCount { get; set; }
        public int ProductsCount { get; set; }
        public int OrdersCount { get; set; }
        public List<Order> RecentOrders { get; set; }
    }

    // Models/ViewModels/UserEditViewModel.cs
    public class UserEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public Guid? RoleIds { get; set; }
    }

    // Models/ViewModels/ProductFilter.cs
    public class ProductFilter
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}