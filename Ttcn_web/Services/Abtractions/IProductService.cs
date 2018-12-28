using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IProductService
    {
        IEnumerable<ICProduct> GetAllObjectOfCurrentPage(int page, int pageSize);

        ICProduct Get(int productId);

        void Create(FormCollection formCollection);

        void Edit(FormCollection formCollection, int productId);

        void Delete(int productId);

        IEnumerable<ICProduct> Filter(int furnitureTypeId, int page, int pageSize);

        int CreateObject(FormCollection formCollection, int userID, int furnitureTypeID);

        IEnumerable<ICProduct> GetAll();
    }
}
