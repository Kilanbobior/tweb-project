namespace BusinessLogic.Migrations
{
    using Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BusinessLogic.TWebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BusinessLogic.TWebDbContext context)
        {
            var user = context.Users.FirstOrDefault(r => r.Name == "admin");

            if (user == null)
            {
                user = new User
                {
                    Name = "admin",
                    PasswordHash = "A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=",
                    Role = "Admin",
                    Email = "admin@resort.com",
                    PhoneNumber = "11111111111",
                    Id = Guid.NewGuid()
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
