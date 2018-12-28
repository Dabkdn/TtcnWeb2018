using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IFurnitureTypeService
    {
        IEnumerable<ARFurnitureType> GetAll();

        ARFurnitureType Get(int furnitureTypeId);

        void Create(FormCollection formCollection);

        void Edit(FormCollection formCollection, int furnitureTypeId);

        void Delete(int furnitureTypeId);

        IEnumerable<ARFurnitureType> Filter(int furnitureTypeGroupId);

        IEnumerable<ARFurnitureType> GetAllObjectOfCurrentPage(int page, int pageSize);
    }
}
