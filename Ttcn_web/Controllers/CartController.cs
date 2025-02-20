using System;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Models.DTO;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        private readonly ISaleOrderService _saleOrderService;

        private readonly ISaleOrderItemService _saleOrderItemService;

        public CartController(ICartService cartService, ISaleOrderService saleOrderService, ISaleOrderItemService saleOrderItemService)
        {
            _cartService = cartService;
            _saleOrderService = saleOrderService;
            _saleOrderItemService = saleOrderItemService;
        }

        public ActionResult ShowCartsOfUser()
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Account");
            int userID = Convert.ToInt32(Session["userID"]);
            Carts objCart = _cartService.GetCartsOfUser(userID);
            return View("Cart", objCart);
        }

        public ActionResult Checkout()
        {
            int userID = Convert.ToInt32(Session["userID"]);
            _saleOrderService.Checkout(userID);
            Carts objCart = new Carts();
            Session["CartItemQty"] = 0;
            return View("Cart", objCart);
        }

        public ActionResult DeleteItemInCart(FormCollection form)
        {
            int userID = Convert.ToInt32(Session["userID"]);
            int saleOrderItemID = Convert.ToInt32(form["buttonDelete"].ToString());
            _saleOrderItemService.UpdateStatusObject(saleOrderItemID);
            ARSaleOrder objsaleOrder = _saleOrderService.GetObjectByItemID(saleOrderItemID);
            if(objsaleOrder != null)
                _saleOrderService.UpdateTotalAmount(objsaleOrder);
            Session["CartItemQty"] = Convert.ToInt32(Session["CartItemQty"]) - _saleOrderItemService.GetSaleOrderItemQuantity(saleOrderItemID);
            return RedirectToAction("ShowCartsOfUser", "Cart");
        }

        [HttpPost]
        public JsonResult IncreaseItemInCart(int id)
        {
            var quantity = _saleOrderItemService.IncreaseItemQuantity(id);
            ARSaleOrder objsaleOrder = _saleOrderService.GetObjectByItemID(id);
            if (objsaleOrder != null)
                _saleOrderService.UpdateTotalAmount(objsaleOrder);
            Session["CartItemQty"] = Convert.ToInt32(Session["CartItemQty"]) + 1;
            return Json(new
            {
                status = quantity
            });
        }

        [HttpPost]
        public JsonResult DecreaseItemInCart(int id)
        {
            var quantity = _saleOrderItemService.DecreaseItemQuantity(id);
            ARSaleOrder objsaleOrder = _saleOrderService.GetObjectByItemID(id);
            if (objsaleOrder != null)
                _saleOrderService.UpdateTotalAmount(objsaleOrder);
            Session["CartItemQty"] = Convert.ToInt32(Session["CartItemQty"]) - 1;
            return Json(new
            {
                status = quantity
            });
        }
    }
}