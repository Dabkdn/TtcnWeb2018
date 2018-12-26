using System;
using System.Net;
using System.Web.Mvc;
using Ttcn_web.Services.Abtractions;
using PagedList;

namespace Ttcn_web.Controllers
{
    public class FurnitureTypeController : Controller
    {
        private readonly IFurnitureTypeService _furnitureTypeService;

        private readonly IProductService _productService;

        private readonly ISaleOrderService _saleOrderService;

        private readonly ISaleOrderItemService _saleOrderServiceItem;

        private readonly ICartService _cartService;

        public FurnitureTypeController(IFurnitureTypeService furnitureTypeService, IProductService productService, ISaleOrderService saleOrderService, ISaleOrderItemService saleOrderServiceItem, ICartService cartService)
        {
            _furnitureTypeService = furnitureTypeService;
            _productService = productService;
            _saleOrderService = saleOrderService;
            _saleOrderServiceItem = saleOrderServiceItem;
            _cartService = cartService;
        }

        // GET: FurnitureType
        public ActionResult Index()
        {
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = _furnitureTypeService.GetAll();

            return View(result);
        }

        // GET: FurnitureType/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Filter(int? id, int page = 1, int pageSize = 9)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var products = _productService.Filter(id.GetValueOrDefault(), page, pageSize);
            Session["FurnitureTypeGroupID"] = id;

            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: FurnitureType/Create
        public ActionResult Create()
        {
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: FurnitureType/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

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
            var userType = Session["userType"];

            if ("Admin".Equals(userType) == false)
            {
                return RedirectToAction("Login", "Account");
            }

            _furnitureTypeService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult AddProductToCartValidateQty(int productID)
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Account");
            int userID = Convert.ToInt32(Session["userID"]);
            if(_saleOrderService.CheckSaleOrderCurrentInUser(userID) != null)
            {
                var saleOrder = _saleOrderService.CheckSaleOrderCurrentInUser(userID);
                if (_saleOrderServiceItem.CheckExistingProductInListCart(userID, productID) != null)
                {
                    var saleOrderitem = _saleOrderServiceItem.CheckExistingProductInListCart(userID, productID);
                    saleOrderitem.ARSaleOrderItemQty += 1;
                    _saleOrderServiceItem.UpdateTotalAmount(saleOrderitem);
                    _saleOrderServiceItem.UpdateObject(saleOrderitem);
                }
                else
                {
                    _saleOrderServiceItem.CreateObject(saleOrder.ARSaleOrderID, productID);
                }
                _saleOrderService.UpdateTotalAmount(saleOrder);
            }
            else
            {
                int saleOrderID = _saleOrderService.CreateObject(userID, productID);
                _saleOrderServiceItem.CreateObject(saleOrderID, productID);
            }
            int cartItemQty = Convert.ToInt32(Session["CartItemQty"]) + 1;
            Session["CartItemQty"] = cartItemQty;
            int furnitureTypeGroupID = Convert.ToInt32(Session["FurnitureTypeGroupID"]);
            return RedirectToAction("Filter", "FurnitureType", new { id = furnitureTypeGroupID });
        }
    }
}
