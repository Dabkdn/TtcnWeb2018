using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

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
            product.ICProductType = "hỏi thằng Bình vì sao có field này";

            if (product == null)
            {
                return;
            }

            db.ICProducts.Add(product);
            db.SaveChanges();
        }

        public void Edit(FormCollection formCollection, int productId)
        {
            var product = db.ICProducts.Find(productId);

            if (product == null)
            {
                return;
            }

            product.AAUpdatedDate = DateTime.Now;
            product.ICProductName = formCollection["ICProductName"];
            product.ICProductNo = formCollection["ICProductNo"];
            product.ICProductDesc = formCollection["ICProductDesc"];

            db.ICProducts.AddOrUpdate();
            db.SaveChanges();
        }

        public IEnumerable<ICProduct> GetAll()
        {
            var a = db.ICProducts.Where(x => x.AAStatus == "Alive").ToList();

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

        public IEnumerable<ICProduct> Filter(int furnitureTypeId)
        {
            return db.ICProducts.Where(x => x.FK_ARFurnitureTypeID == furnitureTypeId && x.AAStatus == "Alive").ToList();
        }
    }
}