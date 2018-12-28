using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;
using PagedList;

namespace Ttcn_web.Services.Implementations
{
    public class FurnitureTypeService : IFurnitureTypeService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public void Create(FormCollection formCollection)
        {
            var furnitureType = new ARFurnitureType();
            var lastFurnitureType = db.ARFurnitureTypes.ToList().LastOrDefault();
            
            furnitureType.ARFurnitureTypeID = lastFurnitureType.ARFurnitureTypeID + 1;
            furnitureType.AAStatus = "Alive";
            furnitureType.AACreatedDate = DateTime.Now;
            furnitureType.ARFurnitureTypeName = formCollection["name"];
            furnitureType.ARFurnitureTypeNo = "LNT" + "-" + furnitureType.ARFurnitureTypeID;
            furnitureType.ARFurnitureTypeDesc = formCollection["desc"];
            furnitureType.FK_ARFurnitureTypeGroupID = Int32.Parse(formCollection["group"]);
            furnitureType.ARFurnitureTypeImageUrl = formCollection["url"];

            if (furnitureType == null)
            {
                return;
            }

            db.ARFurnitureTypes.Add(furnitureType);
            db.SaveChanges();
        }

        public void Edit(FormCollection formCollection, int furnitureTypeId)
        {
            var furnitureType = db.ARFurnitureTypes.Find(furnitureTypeId);

            if (furnitureType == null)
            {
                return;
            }

            furnitureType.AAUpdatedDate = DateTime.Now;
            furnitureType.ARFurnitureTypeName = formCollection["name"] != string.Empty ? formCollection["name"] : furnitureType.ARFurnitureTypeName;
            furnitureType.ARFurnitureTypeDesc = formCollection["desc"] != string.Empty ? formCollection["desc"] : furnitureType.ARFurnitureTypeDesc;
            furnitureType.FK_ARFurnitureTypeGroupID = formCollection["group"] != string.Empty ? Int32.Parse(formCollection["group"]) : furnitureType.FK_ARFurnitureTypeGroupID;
            furnitureType.ARFurnitureTypeImageUrl = formCollection["url"] != string.Empty ? formCollection["url"] : furnitureType.ARFurnitureTypeImageUrl;

            db.SaveChanges();
        }

        public IEnumerable<ARFurnitureType> GetAll()
        {
            var a = db.ARFurnitureTypes.Where(x => x.AAStatus == "Alive").ToList();

            return a;
        }

        public ARFurnitureType Get(int furnitureTypeId)
        {
            return db.ARFurnitureTypes.Find(furnitureTypeId);
        }

        public void Delete(int furnitureTypeId)
        {
            var furnitureType = db.ARFurnitureTypes.Find(furnitureTypeId);

            if (furnitureType == null)
            {
                return;
            }

            furnitureType.AAStatus = "Delete";

            db.ARFurnitureTypes.AddOrUpdate();
            db.SaveChanges();
        }

        public IEnumerable<ARFurnitureType> Filter(int furnitureTypeGroupId)
        {
            return db.ARFurnitureTypes.Where(x => x.FK_ARFurnitureTypeGroupID == furnitureTypeGroupId && x.AAStatus == "Alive").ToList();
        }

        public IEnumerable<ARFurnitureType> GetAllObjectOfCurrentPage(int page, int pageSize)
        {
            var list = db.ARFurnitureTypes.Where(x => x.AAStatus == "Alive").OrderBy(p => p.ARFurnitureTypeID).ToPagedList(page, pageSize);
            return list;
        }
    }
}