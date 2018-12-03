using Eshop.ClassLibrary.DAL;
using Eshop.ClassLibrary.Models.Products;
using Eshop.ClassLibrary.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Web.Security;
using System.IO;

namespace Eshop.Administration.Controllers
{
    public class ProductsController : Controller
    {
        private IRepository<Product> _repo = null;
        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="p"></param>
        public ProductsController(IRepository<Product> p)
        {
           _repo = p;
        }

        // GET: Products
        public ActionResult Index(string sortDirection = "", string sortExpression = "")
        {
           return View( _repo.GetAll().ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            using (var db = new MyContext())
            {
                ProductViewModel productModel = new ProductViewModel
                {
                    ProductCategories = db.ProductCategories.ToList()
                };
                return View(productModel);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel, HttpPostedFileBase file)
        {
            var product = productViewModel.Product;
            try
            {
                if (product != null) 
                {
                    product.DateCreated = DateTime.UtcNow;
                    product.CreatedBy = User.Identity.Name; 
                    if (file!=null)
                    {
                        string filename = product.Name;
                        string extension = Path.GetExtension(file.FileName);
                        filename += extension;
                        
                        
                        string path = Path.Combine(Server.MapPath("~/Eshop/Content/Images/"), filename);
                        file.SaveAs(path);

                        product.ImageUrl = "~/Eshop/Content/Images/"+ filename;   
                    }
                    _repo.Insert(product);
                    _repo.Save();

                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //εδω θα βαλουμε κωδιξ ο οποιος θα γραφει το σφαλμα σε ενα αρχειο. Το γνωστό και πολυ χρήσιμο logging
                return Content(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            Product p = _repo.GetById(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product, HttpPostedFileBase file)
        {
            try
            {
                if (product != null)
                {
                    var p = _repo.GetById(id);
                    if (p != null)
                    {
                        p.Name = product.Name;
                        p.SKU = product.SKU;
                        p.Price = product.Price;
                        p.StockQuantity = product.StockQuantity;
                        p.Description = product.Description;

                        p.DateUpdated = DateTime.UtcNow;
                        p.UpdatedBy = User.Identity.Name;
                        if (file != null)
                        {
                            string filename = product.Name;
                            string extension = Path.GetExtension(file.FileName);
                            filename += extension;

                            string path = Path.Combine(Server.MapPath("~/Content/Images/"), filename);
                            file.SaveAs(path);

                            p.ImageUrl = "~/Content/Images/" + filename;
                        }
                        _repo.Update(p);
                        _repo.Save();
                    }
                }
                return RedirectToAction("Index");

                
            }
            catch (Exception ex)
            {
                //εδω θα βαλουμε κωδιξ ο οποιος θα γραφει το σφαλμα σε ενα αρχειο. Το γνωστό και πολυ χρήσιμο logging
                //return View();
                return Content(ex.ToString());
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _repo.GetById(id);
            if (product ==null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }

        // POST: Products/Delete/5
        [HttpDelete] // Εδω ηταν HttpPost. Το εκανα εγω HttpDelete γιατι απλα ειναι πιο σωστο
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyContext())
                {

                    var p = db.Products.FirstOrDefault(x => x.Id == id);

                    if (p != null)
                        db.Products.Remove(p);
                    else
                        return Json(new
                        {
                            error = "Δεν βρεθηκε το προϊόν"
                        });
                }

                return Json(new
                {

                    error = string.Empty
                });
            }
            catch (Exception ex)
            {

                // εσκασε θα γυρισω πισω το σφαλμα. 
                return Json(new
                {
                    error = ex.ToString()
                });
            }
        }
    }
}
/* 
 Ο τρόπος που χειρίζομαι τα σφάλματα δεν αποτελει κανόνα και ο συγκεκριμενος σε αυτο το αρχειο ειναι ετσι 
 για λογους που θα σας εξηγησω στο αμεσο μελλον.  Το σημαντικο ειναι να μπορουμε να ξερουμε τι εσκασε 
 και να το χειρζόμαστε με τετοιο τρόπο που και ο χρηστης να βλεπει ενα κατανοητο μηνυμα 
 αλλα και εμεις να γνωριζουμε τι εγινε.
*/