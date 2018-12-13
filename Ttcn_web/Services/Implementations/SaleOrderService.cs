using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class SaleOrderService : ISaleOrderService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public ARSaleOrder CheckSaleOrderCurrentInUser(int userID)
        {
            return (ARSaleOrder)db.ARSaleOrders.FirstOrDefault(p => p.FK_ADUserID == userID && p.ARSaleOrderStatus == "New");
        }

        public int CreateObject(int userID,int productID)
        {
            var objSaleOrder = new ARSaleOrder();
            objSaleOrder.ARSaleOrderID = GetMaxSaleOrderID() + 1;
            objSaleOrder.AACreatedDate = DateTime.Now;
            objSaleOrder.AACreatedUser = string.Empty;
            objSaleOrder.AAUpdatedDate = DateTime.Now;
            objSaleOrder.AAUpdatedUser = string.Empty;
            objSaleOrder.FK_ADUserID = userID;
            objSaleOrder.AAStatus = "Alive";
            objSaleOrder.ARSaleOrderStatus = "New";
            objSaleOrder.ARSaleOrderDate = DateTime.Now;
            objSaleOrder.ARSaleOrderDeliveryDate = DateTime.Now.AddDays(4);
            var objProduct = db.ICProducts.Find(productID);
            objSaleOrder.ARSaleOrderSubTotalAmount = objProduct.ICProductPrice;
            objSaleOrder.ARSaleOrderTotalAmount = objProduct.ICProductPrice;
            db.ARSaleOrders.Add(objSaleOrder);
            db.SaveChanges();
            return objSaleOrder.ARSaleOrderID;
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public void Edit(FormCollection formCollection, int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICProduct> Filter(int furnitureTypeId)
        {
            throw new NotImplementedException();
        }

        public int GetMaxSaleOrderID()
        {
            return db.ARSaleOrders.ToList().LastOrDefault().ARSaleOrderID;
        }

        public void UpdateObject(ARSaleOrder objSaleOrder)
        {
            var saleOrder = db.ARSaleOrders.FirstOrDefault(p => p.ARSaleOrderID == objSaleOrder.ARSaleOrderID);
            if (saleOrder == null)
                return;
            saleOrder.AAUpdatedDate = DateTime.Now;
            saleOrder.AAStatus = objSaleOrder.AAStatus;
            saleOrder.FK_ARCustomerID = objSaleOrder.FK_ARCustomerID;
            saleOrder.FK_ADUserID = objSaleOrder.FK_ADUserID;
            saleOrder.ARSaleOrderName = objSaleOrder.ARSaleOrderName;
            saleOrder.ARSaleOrderDesc = objSaleOrder.ARSaleOrderDesc;
            saleOrder.ARSaleOrderComment = objSaleOrder.ARSaleOrderComment;
            saleOrder.ARSaleOrderStatus = objSaleOrder.ARSaleOrderStatus;
            saleOrder.ARSaleOrderPaymentMethod = objSaleOrder.ARSaleOrderPaymentMethod;
            saleOrder.ARSaleOrderDate = objSaleOrder.ARSaleOrderDate;
            saleOrder.ARSaleOrderDeliveryDate = objSaleOrder.ARSaleOrderDeliveryDate;
            saleOrder.ARSaleOrderDiscountPerCent = objSaleOrder.ARSaleOrderDiscountPerCent;
            saleOrder.ARSaleOrderDiscountAmount = objSaleOrder.ARSaleOrderDiscountAmount;
            saleOrder.ARSaleOrderSubTotalAmount = objSaleOrder.ARSaleOrderSubTotalAmount;
            saleOrder.ARSaleOrderTotalAmount = objSaleOrder.ARSaleOrderTotalAmount;
            saleOrder.FK_GEStateProvinceID = objSaleOrder.FK_GEStateProvinceID;
            saleOrder.FK_GEDistrictID = objSaleOrder.FK_GEDistrictID;
            saleOrder.FK_GEWardID = objSaleOrder.FK_GEWardID;
            saleOrder.FK_GEStreetID = objSaleOrder.FK_GEStreetID;
            db.SaveChanges();
        }

        public void UpdateTotalAmount(ARSaleOrder objSaleOrder)
        {
            List<ARSaleOrderItem> saleOrderItemList = db.ARSaleOrderItems.Where(p => p.FK_ARSaleOrderID == objSaleOrder.ARSaleOrderID && p.AAStatus == "Alive").ToList();
            var saleOrder = db.ARSaleOrders.FirstOrDefault(p => p.ARSaleOrderID == objSaleOrder.ARSaleOrderID && p.AAStatus == "Alive");
            if(saleOrderItemList != null && saleOrder != null)
            {
                saleOrder.ARSaleOrderSubTotalAmount = new decimal();
                saleOrder.ARSaleOrderTotalAmount = new decimal();
                saleOrderItemList.ForEach(p =>
                {
                    saleOrder.ARSaleOrderSubTotalAmount += p.ARSaleOrderItemSubTotalAmount.GetValueOrDefault();
                    saleOrder.ARSaleOrderTotalAmount += p.ARSaleOrderItemTotalAmount.GetValueOrDefault();
                });
                saleOrder.ARSaleOrderTotalAmount += (saleOrder.ARSaleOrderFeeShipping.GetValueOrDefault() - saleOrder.ARSaleOrderDiscountAmount.GetValueOrDefault());
            }
            db.SaveChanges();
        }

        public void Checkout(int userID)
        {
            var objSaleOrder = db.ARSaleOrders.FirstOrDefault(p => p.FK_ADUserID == userID);
            objSaleOrder.ARSaleOrderStatus = "Confirm";
            db.SaveChanges();
        }

        public ARSaleOrder GetObjectByItemID(int saleOrderItemID)
        {
            ARSaleOrder objSaleOrder = null;
            int? saleOrderID = db.ARSaleOrderItems.Find(saleOrderItemID).FK_ARSaleOrderID;
            if(saleOrderID != 0 && saleOrderID != null)
                objSaleOrder = db.ARSaleOrders.Find(saleOrderID);
            return objSaleOrder;
        }

        public int GetItemQuantityInSaleOrderByUserID(int userID)
        {
            int quantity = 0;
            var objSaleOrder = db.ARSaleOrders.FirstOrDefault(p => p.FK_ADUserID == userID && p.ARSaleOrderStatus == "New" && p.AAStatus == "Alive");
            if (objSaleOrder == null)
                return 0;
            List<ARSaleOrderItem> saleOrderItemList = objSaleOrder.ARSaleOrderItems.Where(p => p.AAStatus == "Alive").ToList();
            if(saleOrderItemList != null)
            {
                saleOrderItemList.ForEach(p =>
                {
                    quantity += Convert.ToInt32(p.ARSaleOrderItemQty);
                });
            }
            return quantity;
        }
    }
}