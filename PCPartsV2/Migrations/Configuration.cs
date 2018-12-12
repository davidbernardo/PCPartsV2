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
                throw new Exception("nao criou");
            }

            if (createdAdmin.Succeeded)
            {
                userManager.AddToRole(admin.Id, "admin");
            }

            //ORDERS
            var orders = new List<Orders>
            {
                new Orders {Details="Unused",OrderDate= new DateTime(2017,5,7),OrderID=1,Status="Processing",Address="Rua da Esquina nº20", PaymentMethod ="VISA",PostalCode="2300-433",Price=180.99,UserId=normal.Id },
                new Orders {Details="Unused",OrderDate= new DateTime(2017,5,7),OrderID=2,Status="Delivered",Address="Quinta das Flores nº9", PaymentMethod ="Multibanco",PostalCode="2300-789",Price=130,UserId=normal.Id },
                new Orders {Details="Unused",OrderDate= new DateTime(2017,5,7),OrderID=3,Status="Processing",Address="Casa do Admin nº42", PaymentMethod ="VISA",PostalCode="2300-123",Price=99.99,UserId=admin.Id }
            };
            orders.ForEach(oo => context.Orders.Add(oo));
            context.SaveChanges();

            var suppliers = new List<Suppliers>
            {
                new Suppliers {SupplierID=1,Phone="912345678",Name="PartsProcessador",Address="Rua dos Processadores nº1",Email="supplier1@mail.pt",PostalCode="2300-639" },
                new Suppliers {SupplierID=2,Phone="912345678",Name="PartsRAM",Address="Rua das Memórias RAM nº2",Email="supplier2@mail.pt",PostalCode="2300-392" },
                new Suppliers {SupplierID=3,Phone="912345678",Name="PartsMotherboard",Address="Rua das Motherboards nº3",Email="supplier3@mail.pt",PostalCode="2300-947" }
            };
            suppliers.ForEach(ss => context.Suppliers.Add(ss));
            context.SaveChanges();

            var producttype = new List<ProductType>
            {
                new ProductType {ProductTypeID=1,Type="Processor"  },
                new ProductType {ProductTypeID=2,Type="RAM Memory"  },
                new ProductType {ProductTypeID=3,Type="Motherboard"  }
            };
            producttype.ForEach(pt => context.ProductType.Add(pt));
            context.SaveChanges();

            var products = new List<Products>
            {
                new Products {Description="Processador1 topo de game com muitas funcionalidades.",Details="Frequência Clock: 3.2 GHz",Name="Processador Intel Celeron G4920 Dual-Core 3.2GHz 2MB Skt1151",Price=59.90,ProductID=1,SupplierFK=1,Image="image1.jpg",Stock=1,ProductTypeFK=1, Discount=25  },
                new Products {Description="Processador2 topo de game com muitas funcionalidades.",Details="Nº Núcleos: 4",Name="Processador Intel Xeon E3-1220 v5 3.0GHz 8MB Sk1151",Price=229.00,ProductID=2,SupplierFK=1,Image="image2.jpg",Stock=1,ProductTypeFK=1  },
                new Products {Description="Processador3 topo de game com muitas funcionalidades.",Details="Cache: 9 MB",Name="Processador Intel Core i5-8400 Hexa-Core 2.8GHz c/ Turbo 4.0GHz 9MB Skt1151",Price=259.90,ProductID=3,SupplierFK=1,Image="image3.jpg",Stock=1,ProductTypeFK=1  },
                new Products {Description="Processador4 topo de game com muitas funcionalidades.",Details="Número Threads: 12",Name="Processador Intel Core i7-8086K Hexa-Core 4.0GHz c/ Turbo 5.0GHz 12MB Skt1151",Price=469.90,ProductID=4,SupplierFK=1,Image="image4.jpg",Stock=1,ProductTypeFK=1, Discount= 70  },
                new Products {Description="Processador5 topo de game com muitas funcionalidades.",Details="Frequência Turbo: Até 4.30 GHz",Name="Processador Intel Core i9-7940X Fourteen-Core 3.1GHz c/ Turbo 4.3GHz 19.25MB Skt2066",Price=1189.90,ProductID=5,SupplierFK=1,Image="image5.jpg",Stock=1,ProductTypeFK=1 },
                new Products {Description="RAM1 topo de game com muitas funcionalidades.",Details="Velocidade de Frequência: 2666MHz",Name="Memória RAM HyperX Fury 8GB (1x8GB) DDR4-2666MHz CL16 ",Price=79.70,ProductID=6,SupplierFK=2,Image="image6.jpg",Stock=1,ProductTypeFK=2, Discount=30  },
                new Products {Description="RAM2 topo de game com muitas funcionalidades.",Details="Voltagem: 1.20v",Name="Memória RAM Kingston 16GB (1x16GB) DDR4-2400MHz CL17",Price=142.50,ProductID=7,SupplierFK=2,Image="image7.jpg",Stock=1,ProductTypeFK=2  },
                new Products {Description="Motherboard1 topo de game com muitas funcionalidades.",Details="4 x Portas SATA 6Gb/s",Name="Motherboard Micro-ATX MSI H110M PRO-VH PLUS",Price=55.90,ProductID=8,SupplierFK=3,Image="image8.jpg",Stock=1,ProductTypeFK=3,Discount=50  },
                new Products {Description="Motherboard2 topo de game com muitas funcionalidades.",Details="7.1 Canais de Alta Definição Áudio",Name="Motherboard Micro-ATX MSI B250M PRO-VD",Price=62.90,ProductID=9,SupplierFK=3,Image="image9.jpg",Stock=1,ProductTypeFK=3  }
            };
            products.ForEach(pr => context.Products.Add(pr));
            context.SaveChanges();

            var product_order = new List<Product_Order>
            {
                new Product_Order {OrderFK=1,ProductFK=1,Quantity=1 },
                new Product_Order {OrderFK=1,ProductFK=7,Quantity=2 },
                new Product_Order {OrderFK=2,ProductFK=9,Quantity=1 },
                new Product_Order {OrderFK=3,ProductFK=8,Quantity=1 }
            };
            product_order.ForEach(po => context.Product_Order.Add(po));
            context.SaveChanges();
        }
    }
}
