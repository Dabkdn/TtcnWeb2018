using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface ISaleOrderItemService
    {
        void CreateObject(int saleOrderID, int productID);

        int GetMaxSaleOrderItemID();

        ARSaleOrderItem CheckExistingProductInListCart(int userID, int productID);

        void UpdateObject(ARSaleOrderItem saleOrderItem);

        void UpdateTotalAmount(ARSaleOrderItem saleOrderItem);

        void UpdateStatusObject(int saleOrderItemID);

        int GetSaleOrderItemQuantity(int saleOrderItemID);

        int IncreaseItemQuantity(int saleOrderItemID);

        int DecreaseItemQuantity(int saleOrderItemID);
    }
}
