namespace OCC.UI.Webhost.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Principal;
    using System.Web;

    public class Person : IPrincipal
    {
        public Person()
        {
            // Plan for three roles: admin, speaker, volunteer
            this.Roles = new List<string>(3);
        }
        public int ID { get; set; }

        [Required]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "first name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "last name")]
        public string LastName { get; set; }

        [Display(Name = "title")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "bio")]
        public string Bio { get; set; }

        [Display(Name = "website")]
        public string Website { get; set; }

        [Display(Name = "blog")]
        public string Blog { get; set; }

        [Display(Name = "twitter")]
        public string Twitter { get; set; }

        [Required]
        [Display(Name = "city, state")]
        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public bool IsAdmin { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public HttpPostedFileBase Avatar { get; set; }

        public IList<string> Roles { get; private set; }

        // REF: http://mikehadlow.blogspot.com/2008/03/forms-authentication-with-mvc-framework.html

        public IIdentity Identity
        {
            get { return new Identity(true, Email); }
        }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return false;
            }           
            return this.Roles.Contains(role);
        }
    }

    public class Identity : IIdentity
    {
        bool isAuthenticated;
        string name;

        public Identity(bool isAuthenticated, string name)
        {
            this.isAuthenticated = isAuthenticated;
            this.name = name;
        }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion
    }
}