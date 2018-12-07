using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IFurnitureTypeGroupService
    {
        IEnumerable<ARFurnitureTypeGroup> GetAll();

        ARFurnitureTypeGroup Get(int furnitureTypeGroupId);

        void Create(FormCollection formCollection);

        void Edit(FormCollection formCollection, int furnitureTypeGroupId);

        void Delete(int furnitureTypeGroupId);
    }
}
