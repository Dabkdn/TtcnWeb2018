﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TtcnWebEntities : DbContext
    {
        public TtcnWebEntities()
            : base("name=TtcnWebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADUserGroup> ADUserGroups { get; set; }
        public virtual DbSet<ADUser> ADUsers { get; set; }
        public virtual DbSet<APSupplier> APSuppliers { get; set; }
        public virtual DbSet<ARCommonImage> ARCommonImages { get; set; }
        public virtual DbSet<ARCustomerDeliveryAddresss> ARCustomerDeliveryAddressses { get; set; }
        public virtual DbSet<ARCustomer> ARCustomers { get; set; }
        public virtual DbSet<ARFurnitureTypeGroup> ARFurnitureTypeGroups { get; set; }
        public virtual DbSet<ARFurnitureType> ARFurnitureTypes { get; set; }
        public virtual DbSet<ARSaleOrderItem> ARSaleOrderItems { get; set; }
        public virtual DbSet<ARSaleOrder> ARSaleOrders { get; set; }
        public virtual DbSet<GEDistrict> GEDistricts { get; set; }
        public virtual DbSet<GEStateProvince> GEStateProvinces { get; set; }
        public virtual DbSet<GEStreet> GEStreets { get; set; }
        public virtual DbSet<GEWard> GEWards { get; set; }
        public virtual DbSet<ICProduct> ICProducts { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
