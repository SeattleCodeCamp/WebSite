using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CC.Data;
using CC.Service.Webhost.Repositories;

namespace CC.Service.Webhost.Tests.Repositories
{
    [TestClass]
    public class MetadataRepositoryTests : TestRepositoryBase
    {
        private const string Key = "tshirtSizes2";
        private const string Value = "[{\"Item1\":1,\"Item2\":\"one\"},{\"Item1\":2,\"Item2\":\"two\"}]";

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            dbContext.KeyValuePairs.Add(new KeyValuePair()
            {
                Id = Key,
                Value = Value
            });

            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetValueForKey_returns_string_when_valid_id()
        {
            // Arrange
            var cut = new MetadataRepository(dbContext);

            // Act
            string actual = cut.GetValueForKey(Key);

            // Assert
            actual.ShouldAllBeEquivalentTo(Value);
        }

        [TestMethod]
        public void GetValueForKey_returns_string_when_id_null()
        {
            // Arrange
            var cut = new MetadataRepository(dbContext);

            // Act
            string actual = cut.GetValueForKey(null);

            // Assert
            actual.ShouldAllBeEquivalentTo(String.Empty);
        }
    }
}
