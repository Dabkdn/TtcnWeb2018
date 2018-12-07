using System.Net;
using System.Web.Mvc;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class FurnitureTypeGroupController : Controller
    {
        private IFurnitureTypeGroupService _furnitureTypeGroupService;

        private IFurnitureTypeService _furnitureTypeService;

        public FurnitureTypeGroupController(IFurnitureTypeGroupService furnitureTypeGroupService, IFurnitureTypeService furnitureTypeService)
        {
            _furnitureTypeGroupService = furnitureTypeGroupService;
            _furnitureTypeService = furnitureTypeService;
        }

        // GET: FurnitureTypeGroup
        public ActionResult Index()
        {
            var result = _furnitureTypeGroupService.GetAll();

            return View(result);
        }

        // GET: FurnitureTypeGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureTypeGroup = _furnitureTypeGroupService.Get(id.GetValueOrDefault());

            if (furnitureTypeGroup == null)
            {
                return HttpNotFound();
            }

            return View(furnitureTypeGroup);
        }

        /// <summary>
        /// get all furniture type of group id
        /// </summary>
        public ActionResult Filter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureTypes = _furnitureTypeService.Filter(id.GetValueOrDefault());

            if (furnitureTypes == null)
            {
                return HttpNotFound();
            }

            return View(furnitureTypes);
        }

        // GET: FurnitureTypeGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FurnitureTypeGroup/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            _furnitureTypeGroupService.Create(formCollection);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: FurnitureTypeGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureTypeGroup = _furnitureTypeGroupService.Get(id.GetValueOrDefault());

            if (furnitureTypeGroup == null)
            {
                return HttpNotFound();
            }

            return View(furnitureTypeGroup);
        }

        // POST: FurnitureTypeGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection, int id)
        {
            _furnitureTypeGroupService.Edit(formCollection, id);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: FurnitureTypeGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var furnitureTypeGroup = _furnitureTypeGroupService.Get(id.GetValueOrDefault());

            if (furnitureTypeGroup == null)
            {
                return HttpNotFound();
            }

            return View(furnitureTypeGroup);
        }

        // POST: FurnitureTypeGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _furnitureTypeGroupService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
