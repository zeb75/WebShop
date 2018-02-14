namespace WebShop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebShop.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebShop.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            ApplicationUser myAdmin;
            ApplicationUser myUser;

            if (context.Users.SingleOrDefault(u => u.Email == "admin@admin.se") == null)
            {
                myAdmin = new ApplicationUser() { Email = "admin@admin.se", UserName = "admin@admin.se" };
                userManager.Create(myAdmin, "Password1!");
            }

            if (context.Users.SingleOrDefault(u => u.Email == "user@user.se") == null)
            {
                myUser = new ApplicationUser() { Email = "user@user.se", UserName = "user@user.se" };
                userManager.Create(myUser, "Password1!");
            }

            if (roleManager.FindByName("Admin") == null)
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (roleManager.FindByName("Customer") == null)
            {
                roleManager.Create(new IdentityRole("Customer"));
            }

            context.SaveChanges(); // Save changes to Db(context) 

            myAdmin = userManager.FindByEmail("admin@admin.se");
            myUser = userManager.FindByEmail("user@user.se");

            userManager.AddToRole(myAdmin.Id, "Admin");
            userManager.AddToRole(myUser.Id, "Customer");

        }
    }
}
