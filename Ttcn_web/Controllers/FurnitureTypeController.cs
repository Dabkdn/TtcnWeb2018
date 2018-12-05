using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class FurnitureTypeController : Controller
    {
        private readonly IFurnitureTypeService _furnitureTypeService;

        public FurnitureTypeController(IFurnitureTypeService furnitureTypeService)
        {
            _furnitureTypeService = furnitureTypeService;
        }

        private TtcnWebEntities db = new TtcnWebEntities();

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
            ARFurnitureType furnitureType = db.ARFurnitureTypes.Find(id);
            if (furnitureType == null)
            {
                return HttpNotFound();
            }
            return View(furnitureType);
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
            var furnitureType = _furnitureTypeService.Create(formCollection);

            if (ModelState.IsValid)
            {
                db.ARFurnitureTypes.Add(furnitureType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(furnitureType);
        }

        // GET: FurnitureType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARFurnitureType furnitureType = db.ARFurnitureTypes.Find(id);
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
            var furnitureType = _furnitureTypeService.Edit(formCollection, id);

            if (furnitureType == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                db.Entry(furnitureType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(furnitureType);
        }

        // GET: FurnitureType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARFurnitureType furnitureType = db.ARFurnitureTypes.Find(id);
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
            ARFurnitureType furnitureType = db.ARFurnitureTypes.Find(id);
            furnitureType.AAStatus = "Delete";
            
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
