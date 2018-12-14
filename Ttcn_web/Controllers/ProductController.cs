using System;
using System.Net;
using System.Web.Mvc;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class ProductController : Controller
    {

        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Product
        public ActionResult Index()
        {
            var result = _productService.GetAll();

            return View(result);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productService.Get(id.GetValueOrDefault());

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            _productService.Create(formCollection);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productService.Get(id.GetValueOrDefault());

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection, int id)
        {
            _productService.Edit(formCollection, id);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productService.Get(id.GetValueOrDefault());

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult AddNewProduct(FormCollection form)
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Account");
            int furnitureTypeID = Convert.ToInt32(Request.Form["selectedItemFurnitureType"].ToString());
            int userID = Convert.ToInt32(Session["userID"]);
            int productID = _productService.CreateObject(form, userID, furnitureTypeID);
            return RedirectToAction("Details", "Product", new { id = productID });
        }

        public ActionResult ShowAddProduct()
        {
            return View("Create");
        }
    }
}