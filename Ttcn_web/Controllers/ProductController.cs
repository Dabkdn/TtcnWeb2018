using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class ProductController : Controller
    {

        private IProductService _productService;

        private IFurnitureTypeService _furnitureTypeService;

        public ProductController(IProductService productService, IFurnitureTypeService furnitureTypeService)
        {
            _productService = productService;
            _furnitureTypeService = furnitureTypeService;
        }

        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 20)
        {
            //var userType = Session["userType"];

            //if ("Admin".Equals(userType) == false)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            var result = _productService.GetAll(page, pageSize);

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

            var furnitureTypes = _furnitureTypeService.GetAll();

            return View(furnitureTypes);
        }

        // GET: Product/Search
        public ActionResult Search(FormCollection formCollection)
        {
            var products = _productService.GetAll().Where(x => x.ICProductName.Contains(formCollection["search"]));

            return View(products);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productService.Get(id.GetValueOrDefault());
            var furnitureTypeList = _furnitureTypeService.GetAll();
            ViewBag.FurnitureList = furnitureTypeList;

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            return RedirectToAction("Index", "Product", new { id = productID });
        }
    }
}