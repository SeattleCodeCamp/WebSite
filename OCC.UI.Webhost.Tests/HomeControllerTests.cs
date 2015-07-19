using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OCC.UI.Webhost.CodeCampService;
using OCC.UI.Webhost.Controllers;
using OCC.UI.Webhost.Models;

namespace OCC.UI.Webhost.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _cut;
        private Mock<ICodeCampService> _serviceMock;


        [TestInitialize]
        public void Init()
        {
            _serviceMock = new Mock<ICodeCampService>();
        }



        [TestMethod]
        public void Volunteers_NoTasksReturned_PassEmptyModelToView()
        {
            _serviceMock.Setup(serv => serv.GetAllCurrentEventTasks(It.IsAny<int>())).Returns(() => new Task[] { });
            _cut = new HomeController(_serviceMock.Object, new CodeCampServiceRepository(_serviceMock.Object));

            var view = _cut.Volunteers(1);
            Assert.IsTrue(_cut.ViewData.Model.GetType() == typeof(List<VolunteerTask>));
            Assert.AreEqual(0, ((List<VolunteerTask>)_cut.ViewData.Model).Count);
        }

        [TestMethod]
        public void Volunters_EventVolunterRegistrationisOpen_SingleTask_PassTasksToView()
        {

            _serviceMock.Setup(serv => serv.GetAllCurrentEventTasks(It.IsAny<int>())).Returns(() => new Task[] { GetTestTask(true) });
            _cut = new HomeController(_serviceMock.Object, new CodeCampServiceRepository(_serviceMock.Object));

            var view = _cut.Volunteers(1);
            Assert.IsTrue(_cut.ViewData.Model.GetType() == typeof(List<VolunteerTask>));
            Assert.AreEqual(1, ((List<VolunteerTask>)_cut.ViewData.Model).Count);

        }

        [TestMethod]
        public void Volunters_EventVolunterRegistrationisNotOpen_SingleTask_PassEmptyModelToView()
        {

            _serviceMock.Setup(serv => serv.GetAllCurrentEventTasks(It.IsAny<int>())).Returns(() => new Task[] { GetTestTask(false) });
            _cut = new HomeController(_serviceMock.Object, new CodeCampServiceRepository(_serviceMock.Object));

            var view = _cut.Volunteers(1);
            Assert.IsTrue(_cut.ViewData.Model.GetType() == typeof(List<VolunteerTask>));
            Assert.AreEqual(0, ((List<VolunteerTask>)_cut.ViewData.Model).Count);

        }




        private Task GetTestTask(bool volunteerRegistrationEnabled)
        {
            return new Task
            {
                Id = 1,
                Description = "Testing",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Capacity = 1,
                Assignees = new CodeCampService.Person[] { },
                Event = new CodeCampService.Event
                {
                    ID = 1,
                    IsVolunteerRegistrationOpen = volunteerRegistrationEnabled
                }
            };
        }
    }
}
