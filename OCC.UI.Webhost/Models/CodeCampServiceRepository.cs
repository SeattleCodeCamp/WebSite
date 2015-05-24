using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using OCC.UI.Webhost.CodeCampService;

namespace OCC.UI.Webhost.Models
{
    public class CodeCampServiceRepository : ICodeCampServiceRepository
    {
        private ICodeCampService service;

        private IEnumerable<SelectListItem> DefaultTShirtSize
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "Select a t-shirt size",
                }, count: 1);
            }
        }

        public CodeCampServiceRepository(ICodeCampService service)
        {
            this.service = service;
        }

        public IEnumerable<SelectListItem> GetTShirtSizes()
        {
            string tshirtSizesJson = service.GetValueForKey("tshirtSizes");
            List<Tuple<int, string>> tshirtSizes = JsonConvert.DeserializeObject<List<Tuple<int, string>>>(tshirtSizesJson);

            var allTShirtSizes = tshirtSizes.Select(f => new SelectListItem
            {
                Value = f.Item1.ToString(),
                Text = f.Item2
            });

            return DefaultTShirtSize.Concat(allTShirtSizes);
        }
    }
}