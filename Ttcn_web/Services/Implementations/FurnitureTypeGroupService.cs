using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class FurnitureTypeGroupService : IFurnitureTypeGroupService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public void Create(FormCollection formCollection)
        {
            var furnitureTypeGroup = new ARFurnitureTypeGroup();
            var lastFurnitureTypeGroup = db.ARFurnitureTypeGroups.ToList().LastOrDefault();
            
            furnitureTypeGroup.ARFurnitureTypeGroupID = lastFurnitureTypeGroup.ARFurnitureTypeGroupID + 1;
            furnitureTypeGroup.AAStatus = "Alive";
            furnitureTypeGroup.AACreatedDate = DateTime.Now;
            furnitureTypeGroup.ARFurnitureTypeGroupName = formCollection["ARFurnitureTypeGroupName"];
            furnitureTypeGroup.ARFurnitureTypeGroupNo = formCollection["ARFurnitureTypeGroupNo"];
            furnitureTypeGroup.ARFurnitureTypeGroupDesc = formCollection["ARFurnitureTypeGroupDesc"];

            if (furnitureTypeGroup == null)
            {
                return;
            }

            db.ARFurnitureTypeGroups.Add(furnitureTypeGroup);
            db.SaveChanges();
        }

        public void Edit(FormCollection formCollection, int furnitureTypeGroupId)
        {
            var furnitureTypeGroup = db.ARFurnitureTypeGroups.Find(furnitureTypeGroupId);

            if (furnitureTypeGroup == null)
            {
                return;
            }

            furnitureTypeGroup.AAUpdatedDate = DateTime.Now;
            furnitureTypeGroup.ARFurnitureTypeGroupName = formCollection["ARFurnitureTypeGroupName"];
            furnitureTypeGroup.ARFurnitureTypeGroupNo = formCollection["ARFurnitureTypeGroupNo"];
            furnitureTypeGroup.ARFurnitureTypeGroupDesc = formCollection["ARFurnitureTypeGroupDesc"];

            db.ARFurnitureTypeGroups.AddOrUpdate();
            db.SaveChanges();
        }

        public IEnumerable<ARFurnitureTypeGroup> GetAll()
        {
            var a = db.ARFurnitureTypeGroups.Where(x => x.AAStatus == "Alive").ToList();

            return a;
        }

        public ARFurnitureTypeGroup Get(int furnitureTypeGroupId)
        {
            return db.ARFurnitureTypeGroups.Find(furnitureTypeGroupId);
        }

        public void Delete(int furnitureTypeGroupId)
        {
            var furnitureTypeGroup = db.ARFurnitureTypeGroups.Find(furnitureTypeGroupId);

            if (furnitureTypeGroup == null)
            {
                return;
            }

            furnitureTypeGroup.AAStatus = "Delete";

            db.ARFurnitureTypeGroups.AddOrUpdate();
            db.SaveChanges();
        }
    }
}