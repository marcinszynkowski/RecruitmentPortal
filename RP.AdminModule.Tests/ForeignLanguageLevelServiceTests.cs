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
    public class ForeignLanguageLevelServiceTests
    {
        private static Mock<DbSet<ForeignLanguageLevel>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<ForeignLanguageLevel>()
            {
                new ForeignLanguageLevel { Id = 1, Name = "Test1" },

                new ForeignLanguageLevel{ Id = 2, Name = "Test2" },

                new ForeignLanguageLevel { Id = 3, Name = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<ForeignLanguageLevel>>();

            dbSetMock.As<IQueryable<ForeignLanguageLevel>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<ForeignLanguageLevel>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<ForeignLanguageLevel>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<ForeignLanguageLevel>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        [Fact]
        public void GetAllForeignLanguageLevels_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);

            var service = new ForeignLanguageLevelService(mockContext.Object);
            var foreignLanguageLevels = service.GetAllForeignLanguageLevels();

            Assert.Equal(3, foreignLanguageLevels.Count);
            Assert.Equal("Test1", foreignLanguageLevels[0].Name);
            Assert.Equal(1, foreignLanguageLevels[0].Id);
            Assert.Equal("Test2", foreignLanguageLevels[1].Name);
            Assert.Equal(2, foreignLanguageLevels[1].Id);
            Assert.Equal("Test3", foreignLanguageLevels[2].Name);
            Assert.Equal(3, foreignLanguageLevels[2].Id);
        }

        [Fact]
        public void GetForeignLanguageLevelByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);

            var service = new ForeignLanguageLevelService(mockContext.Object);
            var foreignLanguageLevel = service.GetForeignLanguageLevelByName("Test2");

            Assert.Equal(2, foreignLanguageLevel.Id);
            Assert.Equal("Test2", foreignLanguageLevel.Name);
        }

        [Fact]
        public void GetForeignLanguageLevelByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);

            var service = new ForeignLanguageLevelService(mockContext.Object);
            var foreignLanguageLevel = service.GetForeignLanguageLevelByName("Test4");

            Assert.Equal(null, foreignLanguageLevel);
        }

        [Fact]
        public void GetForeignLanguageLevelById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            var foreignLanguageLevel = service.GetForeignLanguageLevelById(3);

            Assert.Equal(3, foreignLanguageLevel.Id);
            Assert.Equal("Test3", foreignLanguageLevel.Name);
        }

        [Fact]
        public void GetForeignLanguageLevelById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            var foreignLanguageLevel = service.GetForeignLanguageLevelById(5);

            Assert.Equal(null, foreignLanguageLevel);
        }

        [Fact]
        public void GetForeignLanguageLevelName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            var foreignLanguageLevel = service.GetForeignLanguageLevelName(1);

            Assert.Equal("Test1", foreignLanguageLevel);
        }

        [Fact]
        public void GetForeignLanguageLevelName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);

            var service = new ForeignLanguageLevelService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetForeignLanguageLevelName(5));
        }

        [Fact]
        public void SetForeignLanguageLevelName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            service.SetForeignLanguageLevelName(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetForeignLanguageLevelName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            service.SetForeignLanguageLevelName(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateForeignLanguageLevel_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            service.CreateForeignLanguageLevel("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<ForeignLanguageLevel>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateForeignLanguageLevel_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            service.CreateForeignLanguageLevel("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<ForeignLanguageLevel>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteForeignLanguageLevel_Deleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            service.DeleteForeignLanguageLevel("Test2");

            mockSet.Verify(m => m.Remove(It.IsAny<ForeignLanguageLevel>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteForeignLanguageLevel_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.ForeignLanguageLevels).Returns(mockSet.Object);


            var service = new ForeignLanguageLevelService(mockContext.Object);
            service.DeleteForeignLanguageLevel("Test4");

            mockSet.Verify(m => m.Remove(It.IsAny<ForeignLanguageLevel>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}