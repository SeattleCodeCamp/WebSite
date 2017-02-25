using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CC.UI.Webhost.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "tag name")]
        public string TagName { get; set; }

        public int SessionsCount { get; set; }

    }
}