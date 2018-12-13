using PCPartsV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCPartsV2.Controllers
{
    //https://www.c-sharpcorner.com/article/creating-shopping-cart-application-from-scratch-in-mvc-part2/
    [Authorize]
    public class CartController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Add product to Cart
        /// <summary>
        /// Gets a product by its Id and adds the product to the cart using the session variable "cart"
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="RedirectTo"></param>
        /// <returns></returns>
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
                // Checks if the added product already exists in the cart, if it does exist the quantity atribute is incremented, else it creates a new object with quantity = 1
                var item = li.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();
                if(item == null)
                {
                    li.Add(new ProductsQuantity { Quantity = 1, Product = product });
                }
                else
                {
                    item.Quantity = item.Quantity + 1;
                }
                // Saves 
                Session["cart"] = li;
                Session["cartCount"] = Convert.ToInt32(Session["cartCount"]) + 1;
            }
            return Redirect("~/"+RedirectTo);
        }

        // Class that is used to save the data of the cart products
        public class ProductsQuantity
        {
            public int Quantity { get; set; }
            public Products Product { get; set; }
        }

        // GET: Returns the list of cart products
        public ActionResult Products()
        {
            var cartProducts = (List<ProductsQuantity>)Session["cart"];
            cartProducts = cartProducts ?? new List<ProductsQuantity>();

            return View(cartProducts);
        }

    }
}