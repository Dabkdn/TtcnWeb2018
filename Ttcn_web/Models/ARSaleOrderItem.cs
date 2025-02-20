//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ttcn_web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ARSaleOrderItem
    {
        public int ARSaleOrderItemID { get; set; }
        public Nullable<System.DateTime> AACreatedDate { get; set; }
        public string AACreatedUser { get; set; }
        public Nullable<System.DateTime> AAUpdatedDate { get; set; }
        public string AAUpdatedUser { get; set; }
        public string AAStatus { get; set; }
        public Nullable<int> FK_ARSaleOrderID { get; set; }
        public Nullable<int> FK_ARFurnitureTypeGroupID { get; set; }
        public Nullable<int> FK_ARFurnitureTypeID { get; set; }
        public Nullable<int> FK_ICProductID { get; set; }
        public Nullable<int> FK_APSupplierID { get; set; }
        public string ARSaleOrderItemNo { get; set; }
        public string ARSaleOrderItemName { get; set; }
        public string ARSaleOrderItemDesc { get; set; }
        public Nullable<decimal> ARSaleOrderItemDiscountPercent { get; set; }
        public Nullable<decimal> ARSaleOrderItemDiscountAmount { get; set; }
        public Nullable<decimal> ARSaleOrderItemQty { get; set; }
        public Nullable<decimal> ARSaleOrderItemPrice { get; set; }
        public Nullable<decimal> ARSaleOrderItemSubTotalAmount { get; set; }
        public Nullable<decimal> ARSaleOrderItemTotalAmount { get; set; }
    
        public virtual APSupplier APSupplier { get; set; }
        public virtual ARFurnitureTypeGroup ARFurnitureTypeGroup { get; set; }
        public virtual ARFurnitureType ARFurnitureType { get; set; }
        public virtual ARSaleOrder ARSaleOrder { get; set; }
        public virtual ICProduct ICProduct { get; set; }
    }
}
