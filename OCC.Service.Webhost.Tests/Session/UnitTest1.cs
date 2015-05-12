using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var dbContext = new InMemoryOCCDB();
            var session = new Data.Session
            {
                
            };

            dbContext.Sessions.Add(session);
            var service = TestHelper.GetTestService(dbContext);

            // Act
            var actualSession = service.GetSession(1);

            // Assert
        }
    }
}
