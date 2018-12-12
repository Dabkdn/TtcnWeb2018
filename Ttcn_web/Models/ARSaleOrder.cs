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
    
    public partial class ARSaleOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ARSaleOrder()
        {
            this.ARSaleOrderItems = new HashSet<ARSaleOrderItem>();
        }
    
        public int ARSaleOrderID { get; set; }
        public Nullable<System.DateTime> AACreatedDate { get; set; }
        public string AACreatedUser { get; set; }
        public Nullable<System.DateTime> AAUpdatedDate { get; set; }
        public string AAUpdatedUser { get; set; }
        public string AAStatus { get; set; }
        public Nullable<int> FK_ARCustomerID { get; set; }
        public Nullable<int> FK_ADUserID { get; set; }
        public string ARSaleOrderNo { get; set; }
        public string ARSaleOrderName { get; set; }
        public string ARSaleOrderDesc { get; set; }
        public string ARSaleOrderComment { get; set; }
        public string ARSaleOrderStatus { get; set; }
        public string ARSaleOrderPaymentMethod { get; set; }
        public Nullable<System.DateTime> ARSaleOrderDate { get; set; }
        public Nullable<System.DateTime> ARSaleOrderDeliveryDate { get; set; }
        public Nullable<decimal> ARSaleOrderDiscountPerCent { get; set; }
        public Nullable<decimal> ARSaleOrderDiscountAmount { get; set; }
        public Nullable<decimal> ARSaleOrderSubTotalAmount { get; set; }
        public Nullable<decimal> ARSaleOrderTotalAmount { get; set; }
        public Nullable<int> FK_GEStateProvinceID { get; set; }
        public Nullable<int> FK_GEDistrictID { get; set; }
        public Nullable<int> FK_GEWardID { get; set; }
        public Nullable<int> FK_GEStreetID { get; set; }
        public string ARSaleOrderHomeNumber { get; set; }
        public Nullable<decimal> ARSaleOrderFeeShipping { get; set; }
    
        public virtual ADUser ADUser { get; set; }
        public virtual ARCustomer ARCustomer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARSaleOrderItem> ARSaleOrderItems { get; set; }
        public virtual GEDistrict GEDistrict { get; set; }
        public virtual GEStateProvince GEStateProvince { get; set; }
        public virtual GEStreet GEStreet { get; set; }
        public virtual GEWard GEWard { get; set; }
    }
}
