using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using OCC.UI.Webhost.CodeCampService;
using OCC.UI.Webhost.Controllers;
using OCC.UI.Webhost.Models;

namespace OCC.UI.Webhost.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        private readonly List<SelectListItem> expected = new List<SelectListItem>
            {
                new SelectListItem{ Value="-1",Text="Select a t-shirt size"},
                new SelectListItem{ Value="1",Text="one"},
                new SelectListItem{ Value="2",Text="two"},
            };

        private AccountController cut;

        [TestInitialize]
        public void Init()
        {
            var input = new List<Tuple<int, string>> { Tuple.Create(1, "one"), Tuple.Create(2, "two") };
            string json = JsonConvert.SerializeObject(input, Formatting.None);
            var serviceMock = Mock.Of<ICodeCampService>(actor =>
                actor.GetValueForKey(It.IsAny<string>()) == json);

            cut = new AccountController(serviceMock, new CodeCampServiceRepository(serviceMock));

        }

        [TestMethod]
        public void Register()
        {
            // Act
            var actual = ((ViewResult)cut.Register());

            // Assert
            (actual.ViewBag.TShirtSizes as IEnumerable<SelectListItem>).ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateProfile()
        {
            // Arrange
            var context = Mock.Of<HttpContextBase>(actor => actor.Session["person"] == new Models.Person { Location = "any" });
            cut.ControllerContext = new ControllerContext(context, new RouteData(), cut);

            // Act
            var actual = ((ViewResult)cut.UpdateProfile());

            // Assert
            (actual.ViewBag.TShirtSizes as IEnumerable<SelectListItem>).ShouldAllBeEquivalentTo(expected);
        }
    }
}
