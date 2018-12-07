﻿using System.Net;
using System.Web.Mvc;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class FurnitureTypeController : Controller
    {
        private readonly IFurnitureTypeService _furnitureTypeService;

        private readonly IProductService _productService;

        public FurnitureTypeController(IFurnitureTypeService furnitureTypeService, IProductService productService)
        {
            _furnitureTypeService = furnitureTypeService;
            _productService = productService;
        }

        // GET: FurnitureType
        public ActionResult Index()
        {
            var result = _furnitureTypeService.GetAll();

            return View(result);
        }

        // GET: FurnitureType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureType = _furnitureTypeService.Get(id.GetValueOrDefault());

            if (furnitureType == null)
            {
                return HttpNotFound();
            }
            return View(furnitureType);
        }

        /// <summary>
        /// get all product of type id
        /// </summary>
        public ActionResult Filter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var products = _productService.Filter(id.GetValueOrDefault());

            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: FurnitureType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FurnitureType/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            _furnitureTypeService.Create(formCollection);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: FurnitureType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureType = _furnitureTypeService.Get(id.GetValueOrDefault());

            if (furnitureType == null)
            {
                return HttpNotFound();
            }

            return View(furnitureType);
        }

        // POST: FurnitureType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection, int id)
        {
            _furnitureTypeService.Edit(formCollection, id);
            
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: FurnitureType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureType = _furnitureTypeService.Get(id.GetValueOrDefault());

            if (furnitureType == null)
            {
                return HttpNotFound();
            }

            return View(furnitureType);
        }

        // POST: FurnitureType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _furnitureTypeService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
