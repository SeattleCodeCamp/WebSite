using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCC.Data;
using OCC.Service.Webhost.Tests.Helpers;

namespace OCC.Service.Webhost.Tests.Task
{
    [TestClass]
    public class UpdateTaskTests
    {
        private Data.Task _actualTask;
        private Services.Task _expectedTask;

        [TestInitialize]
        public void WhenUpdatingTaskPropertiesAreSavedCorrectly()
        {
            // Assemble
            var dbContext = new InMemoryOCCDB()
                .WithEvent("Test Event")
                .WithEvent("Second Event")
                .WithTask("Test Task");

            _expectedTask = new Services.Task
            {
                Description = "This is the Modified Task",
                Capacity = 5,
                StartTime = DateTime.Today,
                EndTime = DateTime.Now,
                EventID = 2
            };

            var service = TestHelper.GetTestService(dbContext);

            var originalTask =service.GetTaskById(1);
            originalTask.Description = _expectedTask.Description;
            originalTask.Capacity = _expectedTask.Capacity;
            originalTask.StartTime = _expectedTask.StartTime;
            originalTask.EndTime = _expectedTask.EndTime;
            originalTask.EventID = _expectedTask.EventID;

            // Act
            service.UpdateTask(originalTask);

            // Assert
            _actualTask = dbContext.Tasks.First();
        }

        [TestMethod]
        public void Description_ProertyIsModified()
        {
            Assert.AreEqual(_actualTask.Description, _expectedTask.Description);
        }

        [TestMethod]
        public void Capacity_ProertyIsModified()
        {
            Assert.AreEqual(_actualTask.Capacity, _expectedTask.Capacity);
        }

        [TestMethod]
        public void StartTime_ProertyIsModified()
        {
            Assert.AreEqual(_actualTask.StartTime, _expectedTask.StartTime);
        }

        [TestMethod]
        public void EndTime_ProertyIsModified()
        {
            Assert.AreEqual(_actualTask.EndTime, _expectedTask.EndTime);
        }

        [TestMethod]
        public void EventID_ProertyIsModified()
        {
            Assert.AreEqual(_actualTask.Event_ID, _expectedTask.EventID);
        }
    }
}