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
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType).Include(p => p.Suppliers);
            return View(products.ToList());
        }

        // GET: Products/Type/5
        public ActionResult Type(int id)
        {
            var products = db.Products.Include(t => t.ProductType).Where(x => x.ProductTypeFK== id).ToList();
            return View(products);
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create([Bind(Include = "ProductID,Name,Price,Description,Details,Stock,Image,SupplierFK,ProductTypeFK,Discount")] Products products, HttpPostedFileBase file)
        {
            /*if (file != null && file.ContentLength > 0 && file.ContentType.Contains("image"))
            {
                var folderImage = Server.MapPath("~/ProductsPhotos");
                var fileName = Path.GetFileName(Guid.NewGuid() + file.FileName);
                products.Image = fileName;
                var path = Path.Combine(folderImage, fileName);
                file.SaveAs(path);
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeFK = new SelectList(db.ProductType, "ProductTypeID", "Type", products.ProductTypeFK);
            ViewBag.SupplierFK = new SelectList(db.Suppliers, "SupplierID", "Name", products.SupplierFK);
            return View(products);*/
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

        /*//POST: /Manage/Perfil
        [HttpPost]
        public ActionResult Perfil(HttpPostedFileBase file)
        {
            var userId = User.Identity.GetUserId();
            var userNick = db.Users.Select(x => x).Where(x => x.Id == userId).FirstOrDefault().Nickname;
            if (file != null && file.ContentLength > 0 && file.ContentType.Contains("image"))
            {
                var pastaImagem = Server.MapPath("~/Avatars");
                var nomeFicheiro = Path.GetFileName("Avatar" + file.FileName + userId);
                var caminho = Path.Combine(pastaImagem, nomeFicheiro);
                file.SaveAs(caminho);
                var user = db.Users.Select(x => x).Where(x => x.Id == userId).FirstOrDefault();
                user.Avatar = nomeFicheiro;
                db.SaveChanges();
                return RedirectToAction("Perfil", new { nick = userNick });
            }
            else
            {
                TempData["erro"] = "Não foi possivel carregar a imagem.";
                return RedirectToAction("Perfil", new { nick = userNick });
            }
        }*/

        // GET: Products/Edit/5
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
        }
    }
}
