using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Price { get; set; }

        // Связь с пользователем
        [ForeignKey("User")]
        
       
        public User User { get; set; }
        public Product Product { get; set; }

        // Связь с продуктом
        [ForeignKey("Product")]
        
        

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } 

        // Дополнительные свойства заказа
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}