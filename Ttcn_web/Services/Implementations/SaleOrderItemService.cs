using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class SaleOrderItemService : ISaleOrderItemService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public ARSaleOrderItem CheckExistingProductInListCart(int userID, int productID)
        {
            ARSaleOrderItem saleOrderItem = db.ARSaleOrders.FirstOrDefault(p => p.FK_ADUserID == userID && p.ARSaleOrderStatus == "New")
                        .ARSaleOrderItems.FirstOrDefault(p => p.FK_ICProductID == productID && p.AAStatus == "Alive");
            return saleOrderItem;
        }

        public void UpdateObject(ARSaleOrderItem objSaleOrderItem)
        {
            var saleOrderItem = db.ARSaleOrderItems.Find(objSaleOrderItem.ARSaleOrderItemID);
            if (saleOrderItem == null)
                return;
            saleOrderItem.AAUpdatedDate = DateTime.Now;
            objSaleOrderItem.AAStatus = objSaleOrderItem.AAStatus;
            objSaleOrderItem.FK_ARSaleOrderID = objSaleOrderItem.FK_ARSaleOrderID;
            objSaleOrderItem.FK_ARFurnitureTypeGroupID = objSaleOrderItem.FK_ARFurnitureTypeGroupID;
            objSaleOrderItem.FK_ARFurnitureTypeID = objSaleOrderItem.FK_ARFurnitureTypeID;
            objSaleOrderItem.FK_ICProductID = objSaleOrderItem.FK_ICProductID;
            objSaleOrderItem.FK_APSupplierID = objSaleOrderItem.FK_APSupplierID;
            objSaleOrderItem.ARSaleOrderItemName = objSaleOrderItem.ARSaleOrderItemName;
            objSaleOrderItem.ARSaleOrderItemDesc = objSaleOrderItem.ARSaleOrderItemDesc;
            objSaleOrderItem.ARSaleOrderItemDiscountPercent = objSaleOrderItem.ARSaleOrderItemDiscountPercent;
            objSaleOrderItem.ARSaleOrderItemQty = objSaleOrderItem.ARSaleOrderItemQty;
            objSaleOrderItem.ARSaleOrderItemPrice = objSaleOrderItem.ARSaleOrderItemPrice;
            db.SaveChanges();
        }

        public void CreateObject(int saleOrderID, int productID)
        {
            var objSaleOderItem = new ARSaleOrderItem();
            objSaleOderItem.ARSaleOrderItemID = GetMaxSaleOrderItemID() + 1;
            objSaleOderItem.AACreatedDate = DateTime.Now;
            objSaleOderItem.AAStatus = "Alive";
            objSaleOderItem.FK_ARSaleOrderID = saleOrderID;
            objSaleOderItem.ARSaleOrderItemQty = 1;
            var objProduct = db.ICProducts.Find(productID);
            if(objProduct != null)
            {
                objSaleOderItem.FK_APSupplierID = objProduct.FK_APSupplierID;
                objSaleOderItem.FK_ARFurnitureTypeID = objProduct.FK_ARFurnitureTypeID;
                objSaleOderItem.FK_ICProductID = objProduct.ICProductID;
                objSaleOderItem.ARSaleOrderItemName = objProduct.ICProductName;
                objSaleOderItem.ARSaleOrderItemDesc = objProduct.ICProductDesc;
                objSaleOderItem.ARSaleOrderItemPrice = objProduct.ICProductPrice.GetValueOrDefault();
                objSaleOderItem.ARSaleOrderItemSubTotalAmount = objSaleOderItem.ARSaleOrderItemPrice * objSaleOderItem.ARSaleOrderItemQty;
                objSaleOderItem.ARSaleOrderItemTotalAmount = objSaleOderItem.ARSaleOrderItemSubTotalAmount - objSaleOderItem.ARSaleOrderItemDiscountAmount.GetValueOrDefault();
            }
            db.ARSaleOrderItems.Add(objSaleOderItem);
            db.SaveChanges();
        }

        public int GetMaxSaleOrderItemID()
        {
            return db.ARSaleOrderItems.ToList().LastOrDefault().ARSaleOrderItemID;
        }

        public void UpdateTotalAmount(ARSaleOrderItem saleOrderItem)
        {
            saleOrderItem.ARSaleOrderItemSubTotalAmount = saleOrderItem.ARSaleOrderItemPrice.GetValueOrDefault() * saleOrderItem.ARSaleOrderItemQty.GetValueOrDefault();
            saleOrderItem.ARSaleOrderItemTotalAmount = saleOrderItem.ARSaleOrderItemSubTotalAmount.GetValueOrDefault() - saleOrderItem.ARSaleOrderItemDiscountAmount.GetValueOrDefault();
        }

        public void UpdateStatusObject(int saleOrderItemID)
        {
            var objSaleOrderItem = db.ARSaleOrderItems.Find(saleOrderItemID);
            if (objSaleOrderItem == null)
                return;
            objSaleOrderItem.AAStatus = "Delete";
            db.SaveChanges();
        }

        public int GetSaleOrderItemQuantity(int saleOrderItemID)
        {
            decimal? itemQuantity = db.ARSaleOrderItems.Find(saleOrderItemID).ARSaleOrderItemQty;
            if (itemQuantity == null)
                return 0;
            return Convert.ToInt32(itemQuantity);
        }

        public int IncreaseItemQuantity(int saleOrderItemID)
        {
            var objSaleOrderItem = db.ARSaleOrderItems.FirstOrDefault(p => p.ARSaleOrderItemID == saleOrderItemID);
            if (objSaleOrderItem == null)
                return Decimal.ToInt32(objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault());
            objSaleOrderItem.ARSaleOrderItemQty += 1;
            objSaleOrderItem.ARSaleOrderItemSubTotalAmount = objSaleOrderItem.ARSaleOrderItemPrice.GetValueOrDefault() * objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault();
            objSaleOrderItem.ARSaleOrderItemTotalAmount = objSaleOrderItem.ARSaleOrderItemSubTotalAmount.GetValueOrDefault() - objSaleOrderItem.ARSaleOrderItemDiscountAmount.GetValueOrDefault();
            db.SaveChanges();
            return objSaleOrderItem != null ? (Decimal.ToInt32(objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault())) : Decimal.ToInt32(objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault());
        }

        public int DecreaseItemQuantity(int saleOrderItemID)
        {
            var objSaleOrderItem = db.ARSaleOrderItems.FirstOrDefault(p => p.ARSaleOrderItemID == saleOrderItemID);
            if(objSaleOrderItem != null)
            { 
                if (objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault() == 1)
                    return 1;
                objSaleOrderItem.ARSaleOrderItemQty -= 1;
                objSaleOrderItem.ARSaleOrderItemSubTotalAmount = objSaleOrderItem.ARSaleOrderItemPrice.GetValueOrDefault() * objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault();
                objSaleOrderItem.ARSaleOrderItemTotalAmount = objSaleOrderItem.ARSaleOrderItemSubTotalAmount.GetValueOrDefault() - objSaleOrderItem.ARSaleOrderItemDiscountAmount.GetValueOrDefault();
                db.SaveChanges();
            }
            return objSaleOrderItem != null ? (Decimal.ToInt32(objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault())) : Decimal.ToInt32(objSaleOrderItem.ARSaleOrderItemQty.GetValueOrDefault());
        }
    }
}