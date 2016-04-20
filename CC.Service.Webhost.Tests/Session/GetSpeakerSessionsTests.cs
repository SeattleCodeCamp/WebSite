using System.Linq;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CC.Data;
using CC.Service.Webhost.Tests.Helpers;

namespace CC.Service.Webhost.Tests.Session
{
    [TestClass]
    public class GetSpeakerSessionsTests
    {
        [TestMethod]
        public void WhenGettingSpeakerSessionsOnlySessionsForThatSpeakerAreReturned()
        {
            var speaker1FirstName = "Test";
            var speaker1LastName = "Speaker";
            var speaker1SessionNames = "Sessions by Speaker 1";

            var speaker2FirstName = "Other";
            var speaker2LastName = "Presenter";
            var speaker2SessionNames = "Sessions by Speaker 2";

            // Assemble
            var dbContext = new InMemoryOCCDB()
                .WithEvent("Test Code Camp")
                .WithPerson(speaker1FirstName, speaker1LastName)
                .WithPerson(speaker2FirstName, speaker2LastName);

            var @event = dbContext.Events.First();
            var speaker1 = dbContext.People.First(p => p.FirstName.Equals(speaker1FirstName) && p.LastName.Equals(speaker1LastName));
            var speaker2 = dbContext.People.First(p => p.FirstName.Equals(speaker2FirstName) && p.LastName.Equals(speaker2LastName));

            var sessions = new[]
            {
                new Data.Session
                {
                    Event_ID = @event.ID,
                    Name = speaker1SessionNames,
                    Speaker_ID = speaker1.ID,
                },
                new Data.Session
                {
                    Event_ID = @event.ID,
                    Name = speaker1SessionNames,
                    Speaker_ID = speaker1.ID,
                },
                new Data.Session
                {
                    Event_ID = @event.ID,
                    Name = speaker1SessionNames,
                    Speaker_ID = speaker1.ID,
                },
                new Data.Session
                {
                    Event_ID = @event.ID,
                    Name = speaker2SessionNames,
                    Speaker_ID = speaker2.ID,
                },
                new Data.Session
                {
                    Event_ID = @event.ID,
                    Name = speaker2SessionNames,
                    Speaker_ID = speaker2.ID,
                }
            };

            sessions.ForEach(s=>dbContext.Sessions.Add(s));
            dbContext.SaveChanges();

            var service = TestHelper.GetTestService(dbContext);

            // Act
            var speaker2Sessions = service.GetSpeakerSessions(@event.ID,speaker2.ID);

            // Assert
            Assert.AreEqual(speaker2Sessions.Count(), 2);

            speaker2Sessions.ForEach(s =>
            {
                Assert.AreEqual(s.Name,speaker2SessionNames);
            });
        }
    }
}