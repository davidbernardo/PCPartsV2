namespace PCPartsV2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PCPartsV2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PCPartsV2.Models.ApplicationDbContext>
    {


        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override async void Seed(PCPartsV2.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //USERS
            ApplicationUser normal = new ApplicationUser
            {
                UserName= "user@a.a",
                Email="user@a.a"
                
            };
            ApplicationUser admin = new ApplicationUser
            {
                UserName = "admin@a.a",
                Email = "admin@a.a"
            };

            roleManager.Create(new IdentityRole("user"));
            roleManager.Create(new IdentityRole("admin"));

            var createdUser = userManager.Create(normal, "123Qwe!");
            var createdAdmin = userManager.Create(admin, "123Qwe!");

            if (createdUser.Succeeded)
            {
                userManager.AddToRole(normal.Id, "user");

            }
            else
            {
                throw new Exception("nao criou************");
            }

            if (createdAdmin.Succeeded)
            {
                userManager.AddToRole(admin.Id, "admin");
            }

            //var users = new List<User_Details>
            //{
            //    new User_Details { UserID=normal.Id,DateOfBirth=new DateTime(1996,8,7),Address="Teste",PostalCode="123" }
            //};
            //users.ForEach(uu => context.User_Details.Add(uu));
            //context.SaveChanges();

            //ORDERS
            var orders = new List<Orders>
            {
                new Orders {Details="Coiso",OrderDate= new DateTime(2017,5,7),OrderID=1,Status="Processing",Address="teste", PaymentMethod ="teste",PostalCode="teste",Price=12,UserId=normal.Id }
            };
            orders.ForEach(oo => context.Orders.Add(oo));
            context.SaveChanges();

            var suppliers = new List<Suppliers>
            {
                new Suppliers {SupplierID=1,Phone="912345678",Name="Parts",Address="rua n2",Email="teste@teste.pt",PostalCode="123" }
            };
            suppliers.ForEach(ss => context.Suppliers.Add(ss));
            context.SaveChanges();

            var producttype = new List<ProductType>
            {
                new ProductType {ProductTypeID=1,Type="Processador"  }
            };
            producttype.ForEach(pt => context.ProductType.Add(pt));
            context.SaveChanges();

            var products = new List<Products>
            {
                new Products {Description="Processador tops",Details="5*",Name="Coiso",Price=5.99,ProductID=1,SupplierFK=1,Image="image1.png",Stock=1,ProductTypeFK=1  }
            };
            products.ForEach(pr => context.Products.Add(pr));
            context.SaveChanges();

            var product_order = new List<Product_Order>
            {
                new Product_Order {OrderFK=1,ProductFK=1,Quantity=10 }
            };
            product_order.ForEach(po => context.Product_Order.Add(po));
            context.SaveChanges();
        }
    }
}
