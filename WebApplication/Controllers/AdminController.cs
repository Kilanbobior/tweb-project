using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication;

using WebApplication.Models;


// Controllers/AdminController.cs
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly TWebDbContext _db = new TWebDbContext();

    // Dashboard - главная страница админки
    [AllowAnonymous]
    public ActionResult AdminPanel()
    {
        return View();
    }

    public ActionResult Dashboard()
    {
        var stats = new AdminDashboardViewModel
        {
            UsersCount = _db.Users.Count(),
            ProductsCount = _db.Products.Count(),
            OrdersCount = _db.Orders.Count(),
            RecentOrders = _db.Orders
                .Include(o => o.User)
                .Include(o => o.Product)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToList()
        };

        return View(stats);
    }

    // Управление пользователями
    public ActionResult Users()
    {
        var users = _db.Users
            .Include(u => u.Role) // Просто Include, без ThenInclude для Name
            .ToList();

        return View(users);
    }

    // Управление товарами
    public ActionResult Products(ProductFilter filter)
    {
        var products = _db.Products.AsQueryable();

        if (!string.IsNullOrEmpty(filter?.Name))
            products = products.Where(p => p.Name.Contains(filter.Name));

        if (filter?.MinPrice.HasValue == true)
            products = products.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter?.MaxPrice.HasValue == true)
            products = products.Where(p => p.Price <= filter.MaxPrice.Value);

        return View(products.ToList());
    }

    // Управление заказами
    public ActionResult Orders(OrderStatus? status = null)
    {
        var orders = _db.Orders
            .Include(o => o.User)
            .Include(o => o.Product)
            .AsQueryable();

        if (status.HasValue)
        {
            orders = orders.Where(o => o.Status == status.Value);
        }

        return View(orders.ToList());
    }

    // CRUD для пользователей
    public ActionResult EditUser(Guid id)
    {
        var user = _db.Users.Find(id);
        if (user == null) return HttpNotFound();

        ViewBag.Roles = _db.Roles.ToList();
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditUser(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _db.Users.Find(model.Id);
            if (user != null)
            {
                user.Username = model.Username;
                user.Email = model.Email;
                user.RoleIds = model.RoleIds;
                _db.SaveChanges();
                TempData["Success"] = "User updated successfully";
            }
            return RedirectToAction("Users");
        }

        ViewBag.Roles = _db.Roles.ToList();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteUser(Guid id)
    {
        var user = _db.Users.Find(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
            TempData["Success"] = "User deleted successfully";
        }
        return RedirectToAction("Users");
    }

    // Аналогичные методы для Products и Orders...
}