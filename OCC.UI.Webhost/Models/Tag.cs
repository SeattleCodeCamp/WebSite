using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OCC.UI.Webhost.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string TagName { get; set; }

        public int SessionsCount { get; set; }

    }
}