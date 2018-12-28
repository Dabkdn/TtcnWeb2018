using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;
using PagedList;

namespace Ttcn_web.Services.Implementations
{
    public class ProductService : IProductService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public void Create(FormCollection formCollection)
        {
            var product = new ICProduct();
            var lastProduct = db.ICProducts.ToList().LastOrDefault();
            
            product.ICProductID = lastProduct.ICProductID + 1;
            product.AAStatus = "Alive";
            product.AACreatedDate = DateTime.Now;
            product.ICProductName = formCollection["ICProductName"];
            product.ICProductNo = formCollection["ICProductNo"];
            product.ICProductDesc = formCollection["ICProductDesc"];
            product.ICProductActiveCheck = true;
            if (product == null)
            {
                return;
            }

            db.ICProducts.Add(product);
            db.SaveChanges();
        }

        public int CreateObject(FormCollection formCollection, int userID, int furnitureTypeID)
        {
            var product = new ICProduct();
            var lastProduct = db.ICProducts.ToList().LastOrDefault();
            
            product.ICProductID = lastProduct.ICProductID + 1;
            product.AAStatus = "Alive";
            product.AACreatedDate = DateTime.Now;
            product.ICProductNo = "SP" +"-"+ product.ICProductID.ToString();
            product.ICProductName = formCollection["productName"].ToString();
            product.ICProductDesc = formCollection["productDesc"].ToString();
            product.ICProductPrice = Convert.ToDecimal(formCollection["productPrice"].ToString());
            product.ICProductPictureLink = "../.." + formCollection["productPictureLink"].ToString();
            product.FK_ARFurnitureTypeID = furnitureTypeID;
            product.FK_APSupplierID = db.APSuppliers.FirstOrDefault(p => p.FK_ADUserID == userID) != null ? db.APSuppliers.FirstOrDefault(p => p.FK_ADUserID == userID).APSupplierID : 0;
            product.ICProductActiveCheck = true;

            db.ICProducts.Add(product);
            db.SaveChanges();
            return product.ICProductID;
        }

        public void Edit(FormCollection formCollection, int productId)
        {
            var product = db.ICProducts.Find(productId);

            if (product == null)
            {
                return;
            }

            product.AAUpdatedDate = DateTime.Now;
            product.ICProductName = formCollection["productName"] != string.Empty ? formCollection["productName"] : product.ICProductName;
            product.ICProductPrice = formCollection["productPrice"] != string.Empty? Convert.ToDecimal(formCollection["productPrice"]): product.ICProductPrice;
            product.ICProductPictureLink = formCollection["productPictureLink"] != string.Empty ? formCollection["productPictureLink"] : product.ICProductPictureLink;
            product.ICProductDesc = formCollection["productDesc"] != string.Empty ? formCollection["productDesc"] : product.ICProductDesc;
            product.FK_ARFurnitureTypeID = formCollection["selectedItemFurnitureType"] != string.Empty ? Convert.ToInt32(formCollection["selectedItemFurnitureType"]) : product.FK_ARFurnitureTypeID;

            db.SaveChanges();
        }

        public IEnumerable<ICProduct> GetAll(int page, int pageSize)
        {
            var a = db.ICProducts.Where(x => x.AAStatus == "Alive").OrderBy(p => p.ICProductID).ToPagedList(page, pageSize);

            return a;
        }

        public ICProduct Get(int productId)
        {
            return db.ICProducts.Find(productId);
        }

        public void Delete(int productId)
        {
            var product = db.ICProducts.Find(productId);

            if (product == null)
            {
                return;
            }

            product.AAStatus = "Delete";

            db.ICProducts.AddOrUpdate();
            db.SaveChanges();
        }

        public IEnumerable<ICProduct> Filter(int furnitureTypeId, int page, int pageSize)
        {
            return db.ICProducts.Where(x => x.FK_ARFurnitureTypeID == furnitureTypeId && x.AAStatus == "Alive").OrderByDescending(p => p.ICProductID).ToPagedList(page, pageSize);
        }
    }
}