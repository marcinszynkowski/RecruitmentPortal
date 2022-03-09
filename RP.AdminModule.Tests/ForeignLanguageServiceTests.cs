using Moq;
using RP.AdminModule.Services;
using RP.Data.Context;
using RP.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace RP.AdminModule.Tests
{
    public class ForeignLanguageServiceTests
    {
        private static Mock<DbSet<ForeignLanguage>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<ForeignLanguage>()
            {
                new ForeignLanguage { Id = 1, Name = "Test1" },

                new ForeignLanguage{ Id = 2, Name = "Test2" },

                new ForeignLanguage { Id = 3, Name = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<ForeignLanguage>>();

            dbSetMock.As<IQueryable<ForeignLanguage>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<ForeignLanguage>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<ForeignLanguage>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<ForeignLanguage>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        [Fact]
        public void GetAllForeignLanguages_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);

            var service = new ForeignLanguageService(mockContext.Object);
            var foreignLanguages = service.GetAllForeignLanguages();

            Assert.Equal(3, foreignLanguages.Count);
            Assert.Equal("Test1", foreignLanguages[0].Name);
            Assert.Equal(1, foreignLanguages[0].Id);
            Assert.Equal("Test2", foreignLanguages[1].Name);
            Assert.Equal(2, foreignLanguages[1].Id);
            Assert.Equal("Test3", foreignLanguages[2].Name);
            Assert.Equal(3, foreignLanguages[2].Id);
        }

        [Fact]
        public void GetForeignLanguageByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);

            var service = new ForeignLanguageService(mockContext.Object);
            var foreignLanguage = service.GetForeignLanguageByName("Test2");

            Assert.Equal(2, foreignLanguage.Id);
            Assert.Equal("Test2", foreignLanguage.Name);
        }

        [Fact]
        public void GetForeignLanguageByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);

            var service = new ForeignLanguageService(mockContext.Object);
            var foreignLanguage = service.GetForeignLanguageByName("Test4");

            Assert.Equal(null, foreignLanguage);
        }

        [Fact]
        public void GetForeignLanguageById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            var foreignLanguage = service.GetForeignLanguageById(3);

            Assert.Equal(3, foreignLanguage.Id);
            Assert.Equal("Test3", foreignLanguage.Name);
        }

        [Fact]
        public void GetForeignLanguageById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            var foreignLanguage = service.GetForeignLanguageById(5);

            Assert.Equal(null, foreignLanguage);
        }

        [Fact]
        public void GetForeignLanguageName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            var foreignLanguage = service.GetForeignLanguageName(1);

            Assert.Equal("Test1", foreignLanguage);
        }

        [Fact]
        public void GetForeignLanguageName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);

            var service = new ForeignLanguageService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetForeignLanguageName(5));
        }

        [Fact]
        public void SetForeignLanguageName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            service.SetForeignLanguageName(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetForeignLanguageName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            service.SetForeignLanguageName(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateForeignLanguage_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            service.CreateForeignLanguage("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<ForeignLanguage>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateForeignLanguage_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            service.CreateForeignLanguage("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<ForeignLanguage>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteForeignLanguage_Deleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            service.DeleteForeignLanguage("Test2");

            mockSet.Verify(m => m.Remove(It.IsAny<ForeignLanguage>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteForeignLanguage_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguages).Returns(mockSet.Object);


            var service = new ForeignLanguageService(mockContext.Object);
            service.DeleteForeignLanguage("Test4");

            mockSet.Verify(m => m.Remove(It.IsAny<ForeignLanguage>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}