using System.Collections.Generic;
using System.Web.Mvc;

namespace CC.UI.Webhost.Models
{
    public interface ICodeCampServiceRepository
    {
        IEnumerable<SelectListItem> GetTShirtSizes();
    }
}