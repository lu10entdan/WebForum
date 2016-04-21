using Microsoft.AspNet.Identity;
using WebForum.Models;

namespace WebForum.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        // Fill the database with test users
        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password1");
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "test@user.com",
                    PasswordHash=password,
                    PhoneNumber = "12345678"

                });
        }
    }
}
