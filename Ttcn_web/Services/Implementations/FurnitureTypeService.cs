using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class FurnitureTypeService : IFurnitureTypeService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public ARFurnitureType Create(FormCollection formCollection)
        {
            var furnitureType = new ARFurnitureType();
            var lastFurnitureType = db.ARFurnitureTypes.ToList().LastOrDefault();
            
            furnitureType.ARFurnitureTypeID = lastFurnitureType.ARFurnitureTypeID + 1;
            furnitureType.AAStatus = "Alive";
            furnitureType.AACreatedDate = DateTime.Now;
            furnitureType.ARFurnitureTypeName = formCollection["ARFurnitureTypeName"];
            furnitureType.ARFurnitureTypeNo = formCollection["ARFurnitureTypeNo"];
            furnitureType.ARFurnitureTypeDesc = formCollection["ARFurnitureTypeDesc"];

            return furnitureType;
        }

        public ARFurnitureType Edit(FormCollection formCollection, int furnitureTypeId)
        {
            ARFurnitureType furnitureType = db.ARFurnitureTypes.Find(furnitureTypeId);

            if (furnitureType == null)
            {
                return null;
            }

            furnitureType.AAUpdatedDate = DateTime.Now;
            furnitureType.ARFurnitureTypeName = formCollection["ARFurnitureTypeName"];
            furnitureType.ARFurnitureTypeNo = formCollection["ARFurnitureTypeNo"];
            furnitureType.ARFurnitureTypeDesc = formCollection["ARFurnitureTypeDesc"];

            return furnitureType;
        }

        public IEnumerable<ARFurnitureType> GetAll()
        {
            var a = db.ARFurnitureTypes.Where(x => x.AAStatus == "Alive").ToList();

            return a;
        }
        
    }
}