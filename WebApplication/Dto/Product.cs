using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int RoomCount { get; set; }
        public int GuestCount { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public decimal? HolidayPrice { get; set; }
        public bool IsActive { get; set; }

        public string MainImageUrl { get; set; }
        public List<string> AdditionalImageUrls { get; set; } = new List<string>();
        public List<string> AdditionalDescriptions { get; set; } = new List<string>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }


        public int StockQuantity { get; set; }

        // Обратная связь с заказами
        public virtual ICollection<Order> Orders { get; set; }

    }
}