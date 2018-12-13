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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Address, string PostalCode, string PaymentMethod)
        {
            try
            {
                var cartProducts = (List<ProductsQuantity>)Session["cart"];

                double Price = 0;

                foreach (var po in cartProducts)
                {
                    Price = Price + po.Product.Price * po.Quantity;
                }

                Orders order = new Orders
                {
                    Address = Address,
                    OrderDate = DateTime.Now,
                    PaymentMethod = PaymentMethod,
                    PostalCode = PostalCode,
                    Status = "Processing",
                    Details = "wewewe",
                    Price = Price,
                    UserId = User.Identity.GetUserId()
                };

                var addedOrder= db.Orders.Add(order);

                foreach (var po in cartProducts)
                {
                    Product_Order prd_ord = new Product_Order { Orders = addedOrder, ProductFK = po.Product.ProductID, Quantity = po.Quantity};
                    db.Product_Order.Add(prd_ord);
                }
                db.SaveChanges();

                Session["cart"] = null;
                Session["cartCount"] = 0;

                return Redirect("~/Home/Index"); //redirect para a home
            }
            catch (Exception ex)
            {
                return View();
            }   
        }

        // GET: Orders
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

            var order = db.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            var user = db.Users.Where(o => o.Id == order.UserId).Select(o => o.UserName).FirstOrDefault();
            var product_order = db.Product_Order.Where(po => po.OrderFK == order.OrderID).FirstOrDefault();
            var product = db.Products.Where(p => p.ProductTypeFK == product_order.ProductFK).FirstOrDefault();

            if (order == null || user == null || product_order == null || product == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderDetails = order;
            ViewBag.UserName = user;
            ViewBag.Quantity = product_order;
            ViewBag.ProductDetails = product;

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

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        /*
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type");
            ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Price,Description,Details,Stock,Image,SupplierFK,ProductTypeFK")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type", products.ProductTypeFK);
            ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name", products.SupplierFK);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Price,Description,Details,Stock,Image,SupplierFK,ProductTypeFK")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type", products.ProductTypeFK);
            ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name", products.SupplierFK);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
