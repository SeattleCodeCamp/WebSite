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
        [TestMethod]
        public void WhenGettingSessionPropertiesAreMappedCorrectly()
        {
            // Assemble
            var dbContext = new InMemoryOCCDB()
                .WithEvent("Test Code Camp")
                .WithPerson("Test", "Speaker");

            var session = new Data.Session
            {
                Description = "This is the event",
                Event_ID = dbContext.Events.First().ID,
                Level = 300,
                Location = "The really far building",
                Name = "Best .NET Session",
                Speaker_ID = dbContext.People.First().ID,
                Status = "Still Happening"
            };

            dbContext.Sessions.Add(session);
            dbContext.SaveChanges();

            var service = TestHelper.GetTestService(dbContext);

            // Act
            var retrievedSession = service.GetSession(session.ID);

            // Assert
            Assert.AreEqual(retrievedSession.Description,session.Description);
            Assert.AreEqual(retrievedSession.EventID, session.Event_ID);
            Assert.AreEqual(retrievedSession.ID, session.ID);
            Assert.AreEqual(retrievedSession.Level, session.Level);
            Assert.AreEqual(retrievedSession.Location, session.Location);
            Assert.AreEqual(retrievedSession.Name, session.Name);
            Assert.AreEqual(retrievedSession.SpeakerID, session.Speaker_ID);
            Assert.AreEqual(retrievedSession.Status, session.Status);
        }
    }
}
