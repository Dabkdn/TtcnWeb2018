using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IProductService
    {
        IEnumerable<ICProduct> GetAll();

        ICProduct Get(int productId);

        void Create(FormCollection formCollection);

        void Edit(FormCollection formCollection, int productId);

        void Delete(int productId);

        IEnumerable<ICProduct> Filter(int furnitureTypeId);

        int CreateObject(FormCollection formCollection, int userID, int furnitureTypeID);
    }
}
