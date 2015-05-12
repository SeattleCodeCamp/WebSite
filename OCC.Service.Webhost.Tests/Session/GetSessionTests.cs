using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCC.Data;
using OCC.Service.Webhost.Tests.Helpers;

namespace OCC.Service.Webhost.Tests.Session
{
    [TestClass]
    public class GetSessionTests
    {
        private Data.Session _expectedSession;
        private Event _expectedEvent;
        private Person _expectedSpeaker;

        private Services.Session _actualSession;

        [TestInitialize]
        public void WhenGettingSessionPropertiesAreRetrievedCorrectly()
        {
            // Assemble
            var dbContext = new InMemoryOCCDB()
                .WithEvent("Test Code Camp")
                .WithPerson("Test", "Speaker");

            _expectedEvent = dbContext.Events.First();
            _expectedSpeaker = dbContext.People.First();

            _expectedSession = new Data.Session
            {
                Description = "This is the event",
                Event_ID = _expectedEvent.ID,
                Level = 300,
                Location = "The really far building",
                Name = "Best .NET Session",
                Speaker_ID = _expectedSpeaker.ID,
                Status = "Still Happening",
            };

            dbContext.Sessions.Add(_expectedSession);
            dbContext.SaveChanges();

            var service = TestHelper.GetTestService(dbContext);

            // Act
            _actualSession = service.GetSession(_expectedSession.ID);

            // Assert
        }

        [TestMethod]
        public void Description_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.Description, _expectedSession.Description);
        }

        [TestMethod]
        public void EventId_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.EventID, _expectedSession.Event_ID);
        }

        [TestMethod]
        public void Event_Id_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.EventID, _expectedEvent.ID);
        }

        [TestMethod]
        public void Id_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.ID, _expectedSession.ID);
        }

        [TestMethod]
        public void Level_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.Level, _expectedSession.Level);
        }

        [TestMethod]
        public void Location_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.Location, _expectedSession.Location);
        }

        [TestMethod]
        public void Name_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.Name, _expectedSession.Name);
        }

        [TestMethod]
        public void Speaker_ID_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.SpeakerID, _expectedSession.Speaker_ID);
        }

        [TestMethod]
        public void Status_ProertyIsRetreived()
        {
            Assert.AreEqual(_actualSession.Status, _expectedSession.Status);
        }

        [TestMethod]
        public void Speaker_ProertyIsRetreived()
        {
            Assert.IsNotNull(_actualSession.Speaker);
        }

        [TestMethod]
        public void Speaker_Id_ProertyIsRetreived()
        {
            Assert.AreEqual(_expectedSpeaker.ID, _expectedSession.Speaker.ID);
        }

        [TestMethod]
        public void Speaker_FirstName_ProertyIsRetreived()
        {
            Assert.AreEqual(_expectedSpeaker.FirstName, _expectedSession.Speaker.FirstName);
        }

        [TestMethod]
        public void Speaker_LastName_ProertyIsRetreived()
        {
            Assert.AreEqual(_expectedSpeaker.LastName, _expectedSession.Speaker.LastName);
        }
    }
}
