using PCPartsV2.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using static PCPartsV2.Controllers.CartController;
using System;
using System.Linq;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;

namespace PCPartsV2.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders/Create
        /// <summary>
        /// View for the create post
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        /// <summary>
        /// Creates an order using three input parameters, the current user Id and the session variable "cart"
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="PostalCode"></param>
        /// <param name="PaymentMethod"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Address, string PostalCode, string PaymentMethod)
        {
            try
            {
                var cartProducts = (List<ProductsQuantity>)Session["cart"];

                //Calculates the total price of the cart
                double Price = 0;
                foreach (var po in cartProducts)
                {
                    Price = Price + po.Product.Price * po.Quantity;
                }

                //Creates the object order
                Orders order = new Orders
                {
                    Address = Address,
                    OrderDate = DateTime.Now,
                    PaymentMethod = PaymentMethod,
                    PostalCode = PostalCode,
                    Status = "Processing",
                    Details = "Unused",
                    Price = Price,
                    UserId = User.Identity.GetUserId()
                };

                //Adds the created order to the Orders table
                var addedOrder= db.Orders.Add(order);

                //Creates the data in the table Product_Order, result of the N-M relation between products and order
                foreach (var po in cartProducts)
                {
                    Product_Order prd_ord = new Product_Order { Orders = addedOrder, ProductFK = po.Product.ProductID, Quantity = po.Quantity};
                    db.Product_Order.Add(prd_ord);
                }
                db.SaveChanges();

                //Resets the values of the cart so that the cart is empty again
                Session["cart"] = null;
                Session["cartCount"] = 0;

                return Redirect("~/Home/Index");
            }
            catch (Exception ex)
            {
                return View();
            }   
        }

        // GET: Orders
        /// <summary>
        /// Returns all the orders if the user is an admin or returns the clients orders in case its a user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                var orders = db.Orders.Include(u => u.User);
                return View(orders.ToList());
            }
            else
            {
                var userID = User.Identity.GetUserId();
                var orders = db.Orders.Include(u => u.User).Where(o => o.UserId == userID);
                return View(orders.ToList());
            }
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ProductsQuantity> ProductDetails = new List<ProductsQuantity>();
            var order = db.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            var user = db.Users.Where(o => o.Id == order.UserId).Select(o => o.UserName).FirstOrDefault();
            var product_order = db.Product_Order.Where(po => po.OrderFK == order.OrderID).ToList();
            
            foreach (var prd in product_order)
            {
                var product = db.Products.Where(p => p.ProductID == prd.ProductFK).FirstOrDefault();
                ProductDetails.Add(new ProductsQuantity { Quantity = prd.Quantity, Product = product });
            }

            if (order == null || user == null || product_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderDetails = order;
            ViewBag.UserName = user;
            ViewBag.ProductDetails= ProductDetails;

            return View();
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Users, "Id", "Email", order.UserId);
            //ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name", products.SupplierFK);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID, Status, Address, Details, PaymentMethod, PostalCode, UserId")] Orders order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Users, "Id", "Email", order.UserId);
            //ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type", products.ProductTypeFK);
            //ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name", products.SupplierFK);
            return View(order);
        }
    }
}
