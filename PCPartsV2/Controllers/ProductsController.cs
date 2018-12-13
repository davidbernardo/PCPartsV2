using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCPartsV2.Models;
using System.IO;

namespace PCPartsV2.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        /// <summary>
        /// Returns all products with their related objects included 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType).Include(p => p.Suppliers);
            return View(products.ToList());
        }

        // GET: Products/Type/5
        /// <summary>
        /// Returns all products of a certain type with their related objects included 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Type(int id)
        {
            var products = db.Products.Include(t => t.ProductType).Where(x => x.ProductTypeFK== id).ToList();
            return View(products);
        }


        // GET: Products/Details/5
        /// <summary>
        /// Returns a certain product by Id with supplier details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Include(p => p.Suppliers).Where(p => p.ProductID == id).FirstOrDefault();
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        /// <summary>
        /// Returns the information of the related entities to feed the dropdown inputs
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type");
            ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name");
            return View();
        }

        // POST: Products/Create
        /// <summary>
        /// Creates a new product and also uploads its image
        /// </summary>
        /// <param name="products"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Price,Description,Details,Stock,Image,SupplierFK,ProductTypeFK,Discount")] Products products, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0 && file.ContentType.Contains("image"))
                {
                    var folderImage = Server.MapPath("~/ProductsPhotos");
                    var fileName = Path.GetFileName(Guid.NewGuid() + file.FileName);
                    products.Image = fileName;
                    var path = Path.Combine(folderImage, fileName);
                    file.SaveAs(path);
                }
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(products);
            }
        }

        // GET: Products/Edit/5
        /// <summary>
        /// Returns the information of the related entities to feed the dropdown inputs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
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
            ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type", products.ProductTypeFK);
            ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name", products.SupplierFK);
            return View(products);
        }

        // POST: Products/Edit/5
        /// <summary>
        /// Edits a product with the exception of the image upload
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns the information about the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
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
        /// <summary>
        /// Removes the selected product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        }
    }
}
