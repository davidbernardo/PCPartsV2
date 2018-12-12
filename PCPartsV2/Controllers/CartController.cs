using PCPartsV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCPartsV2.Controllers
{
    //https://www.c-sharpcorner.com/article/creating-shopping-cart-application-from-scratch-in-mvc-part2/

    public class CartController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        
//            foreach(var p in cartProducts)
//            {
//                var item = quantityList.Where(x => x.Product.ProductID == p.Product.ProductID).FirstOrDefault(); //verifica se lá esta o produto
//                if(item != null)
//                {
//                    item.Quantity = item.Quantity + 1;
//                }
//                else
//                {
//                    quantityList.Add(new ProductsQuantity { Quantity = 1, Product = p
//});
//                }


        // GET: Add product to Cart
        public ActionResult AddToCart(int ProductID, string RedirectTo)
        {
            var product = db.Products.Where(p => p.ProductID == ProductID).FirstOrDefault();
            if (Session["cart"] == null)
            {
                List<ProductsQuantity> li = new List<ProductsQuantity>();
                li.Add(new ProductsQuantity { Quantity = 1, Product = product });
                Session["cart"] = li;
                Session["cartCount"] = 1;
            }
            else
            {
                List<ProductsQuantity> li = (List<ProductsQuantity>)Session["cart"];
                var item = li.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault(); //verifica se já existe o item no carrigo
                if(item == null)
                {
                    li.Add(new ProductsQuantity { Quantity = 1, Product = product });
                }
                else
                {
                    item.Quantity = item.Quantity + 1;
                }
                
                Session["cart"] = li;
                Session["cartCount"] = Convert.ToInt32(Session["cartCount"]) + 1;
            }
            return Redirect("~/"+RedirectTo);
        }

        public class ProductsQuantity
        {
            public int Quantity { get; set; }
            public Products Product { get; set; }
        }

        // GET: List of cart products
        public ActionResult Products()
        {
            var cartProducts = (List<ProductsQuantity>)Session["cart"];

            return View(cartProducts);
        }

    }
}