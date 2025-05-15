using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using TWeb48.Helpers;
using WebApplication.Models;

namespace WebApplication
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
            CheckAndCreateRoles();
            CheckAndCreateDefaultUser();
        }
        private void CheckAndCreateRoles()
        {
            using (var context = new TWebDbContext())
            {
                if (!context.Roles.Any())
                {
                    context.Roles.Add(new Role { Name = "Admin", Id = Guid.NewGuid() });
                    context.Roles.Add(new Role { Name = "User", Id = Guid.NewGuid() });
                    context.SaveChanges();
                }
            }
        }
        private void CheckAndCreateDefaultUser()
        {
            using (var context = new TWebDbContext())
            {
                var user = context.Users.FirstOrDefault(r => r.Username == "DemoUser");

                if (user == null)
                {
                    var role = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                    if (role != null)
                    {
                        var pass = PasswordHelper.HashPassword("141592");
                        user = new User
                        {
                            Username = "DemoUser",
                            PasswordHash = pass,
                            Email = "DemoUser@demo.com",
                            Id = Guid.NewGuid(),
                            RoleIds = role.Id
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
    
    }
    