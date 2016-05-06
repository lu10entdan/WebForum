using System.Security.Policy;
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
                    Id = "86a6458d-08d5-41f4-af23-84100b7ef22f",
                    UserName = "User@Test.com",
                    Email = "User@Test.com",
                    PasswordHash=password,
                    PhoneNumber = "12345678",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = "ccc37e0d-d0de-40d8-a9f5-c794d4ed487f"
                });
        }
    }
}
