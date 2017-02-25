﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CC.Service.Webhost.Repositories;
using CC.Service.Webhost.Services;
using Moq;
using System;
using CC.Data;
using CC.Service.Webhost.Tests.Helpers;
using CC.Service.Webhost.Tests.Repositories;

namespace CC.Service.Webhost.Tests.Service
{
    [TestClass]
    public class CodeCampServiceTests : TestRepositoryBase
    {
        private const string Key = "tshirtSizes";
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
        public void GetValueForKey_returns_string_when_id_null()
        {
            // Arrange
            var mockRepo = new Mock<MetadataRepository>(null);

            var cut = TestHelper.GetTestService(dbContext, kernel =>
            {
                kernel.Rebind<MetadataRepository>().ToConstant(mockRepo.Object);
            });

            // Act
            string actual = cut.GetValueForKey(null);

            // Assert
            actual.ShouldAllBeEquivalentTo(String.Empty);
        }

        [TestMethod]
        public void GetValueForKey_returns_string_when_valid_id()
        {
            // Arrange
            var mockRepo = new Mock<MetadataRepository>(dbContext);

            var cut = TestHelper.GetTestService(dbContext, kernel =>
            {
                kernel.Rebind<MetadataRepository>().ToConstant(mockRepo.Object);
            });

            // Act
            string actual = cut.GetValueForKey(Key);

            // Assert
            actual.ShouldAllBeEquivalentTo(Value);
        }

    }
}
