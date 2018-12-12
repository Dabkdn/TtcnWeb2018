using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface ISaleOrderService
    {
        int CreateObject(int userID,int productID);

        void Edit(FormCollection formCollection, int productId);

        void Delete(int productId);

        IEnumerable<ICProduct> Filter(int furnitureTypeId);

        int GetMaxSaleOrderID();

        void UpdateTotalAmount(ARSaleOrder objSaleOrder);

        void UpdateObject(ARSaleOrder objSaleOrder);

        ARSaleOrder CheckSaleOrderCurrentInUser(int userID);

        void Checkout(int userID);

        ARSaleOrder GetObjectByItemID(int saleOrderItemID);

        int GetItemQuantityInSaleOrderByUserID(int userID);
    }
}
