using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCC.Data;
using OCC.Service.Webhost.Tests.Helpers;

namespace OCC.Service.Webhost.Tests.Session
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Assemble
            var dbContext = new InMemoryOCCDB()
                .WithEvent("Test Code Camp")
                .WithPerson("Test", "Speaker");

            var expectedDescription = "This is the event";
            var expectedLevel = 300;
            var expectedLocation = "The really far building";
            var expectedName = "Best .NET Session";
            var expectedstatus = "Still Happening";
            var session = new Data.Session
            {
                Description = expectedDescription,
                Event_ID = dbContext.Events.First().ID,
                Level = expectedLevel,
                Location = expectedLocation,
                Name = expectedName,
                Speaker_ID = dbContext.People.First().ID,
                Status = expectedstatus
            };

            dbContext.Sessions.Add(session);
            dbContext.SaveChanges();

            var service = TestHelper.GetTestService(dbContext);

            // Act
            var retrievedSession = service.GetSession(session.ID);

            // Assert
            Assert.AreEqual(retrievedSession.Description,expectedDescription);
        }
    }
}
