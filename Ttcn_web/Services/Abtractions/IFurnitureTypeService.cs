using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IFurnitureTypeService
    {
        IEnumerable<ARFurnitureType> GetAll();

        ARFurnitureType Create(FormCollection formCollection);

        ARFurnitureType Edit(FormCollection formCollection, int furnitureTypeId);
    }
}
