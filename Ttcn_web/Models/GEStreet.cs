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
    
    public partial class GEStreet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GEStreet()
        {
            this.ARCustomerDeliveryAddressses = new HashSet<ARCustomerDeliveryAddresss>();
            this.ARSaleOrders = new HashSet<ARSaleOrder>();
        }
    
        public int GEStreetID { get; set; }
        public string AAStatus { get; set; }
        public Nullable<int> FK_GEWardID { get; set; }
        public string GEStreetName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARCustomerDeliveryAddresss> ARCustomerDeliveryAddressses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARSaleOrder> ARSaleOrders { get; set; }
        public virtual GEWard GEWard { get; set; }
    }
}
