using System.Collections.Generic;
using System.Web.Mvc;

namespace OCC.UI.Webhost.Models
{
    public interface ICodeCampServiceRepository
    {
        IEnumerable<SelectListItem> GetTShirtSizes();
    }
}