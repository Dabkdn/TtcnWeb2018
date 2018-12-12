using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Models.DTO;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class CartService : ICartService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public Carts GetCartsOfUser(int userID)
        {
            var objcart = new Carts();
            ARCustomerDeliveryAddresss objCustomerDeliveryAddress = null;
            var objsaleOrder = db.ARSaleOrders.Where(p => p.FK_ADUserID == userID && p.ARSaleOrderStatus == "New" && p.AAStatus == "Alive").FirstOrDefault();
            var objCustomer = db.ADUsers.Find(userID).ARCustomers.ToList().FirstOrDefault();
            if(objCustomer != null)
            {
                objcart.CustomerName = objCustomer.ARCustomerName;
                objCustomerDeliveryAddress = db.ARCustomerDeliveryAddressses.Where(p => p.FK_ARCustomerID == objCustomer.ARCustomerID).FirstOrDefault();
            }

            if(objsaleOrder != null)
            {
                objcart.DeliveryAddress = string.IsNullOrEmpty(objsaleOrder.ARSaleOrderHomeNumber) ? string.Empty : (objsaleOrder.ARSaleOrderHomeNumber + " , ");
                objcart.DeliveryDate = objsaleOrder.ARSaleOrderDeliveryDate;
                objcart.SubTotalAmount = objsaleOrder.ARSaleOrderSubTotalAmount.GetValueOrDefault();
                objcart.FeeShipping = 0;
                objcart.TotalAmount = objsaleOrder.ARSaleOrderTotalAmount.GetValueOrDefault();
                objcart.SaleOrderItemList = objsaleOrder.ARSaleOrderItems.Where(p => p.AAStatus == "Alive").ToList();
            }

            if (objCustomerDeliveryAddress != null)
            {
                objcart.DeliveryAddress = objCustomer.ARCustomerAddress + objCustomer.ARCustomerContactPhone;

                //objcart.DeliveryAddress = objCustomerDeliveryAddress.GEStreet.GEStreetName.FirstOrDefault() + " , "
                //                        + objCustomerDeliveryAddress.GEWard.GEWardName.FirstOrDefault() + " , "
                //                        + objCustomerDeliveryAddress.GEDistrict.GEDistrictName.FirstOrDefault() + " , "
                //                        + objCustomerDeliveryAddress.GEStateProvince.GEStateProvinceName.FirstOrDefault() + " , "
                //                        + objCustomerDeliveryAddress.ARCustomerDeliveryAddressContactPhone;
            }
            
            return (Carts)objcart;
        }
    }
}