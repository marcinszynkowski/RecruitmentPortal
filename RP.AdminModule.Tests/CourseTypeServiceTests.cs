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
    public class CourseTypeServiceTests
    {
        private static Mock<DbSet<CourseType>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<CourseType>()
            {
                new CourseType { Id = 1, Name = "Test1" },

                new CourseType{ Id = 2, Name = "Test2" },

                new CourseType { Id = 3, Name = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<CourseType>>();

            dbSetMock.As<IQueryable<CourseType>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<CourseType>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<CourseType>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<CourseType>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        [Fact]
        public void GetAllCourseTypes_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);

            var service = new CourseTypeService(mockContext.Object);
            var courseTypes = service.GetAllCourseTypes();

            Assert.Equal(3, courseTypes.Count);
            Assert.Equal("Test1", courseTypes[0].Name);
            Assert.Equal(1, courseTypes[0].Id);
            Assert.Equal("Test2", courseTypes[1].Name);
            Assert.Equal(2, courseTypes[1].Id);
            Assert.Equal("Test3", courseTypes[2].Name);
            Assert.Equal(3, courseTypes[2].Id);
        }

        [Fact]
        public void GetCourseTypeByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);

            var service = new CourseTypeService(mockContext.Object);
            var courseType = service.GetCourseTypeByName("Test2");

            Assert.Equal(2, courseType.Id);
            Assert.Equal("Test2", courseType.Name);
        }

        [Fact]
        public void GetCourseTypeByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);

            var service = new CourseTypeService(mockContext.Object);
            var courseType = service.GetCourseTypeByName("Test4");

            Assert.Equal(null, courseType);
        }

        [Fact]
        public void GetCourseTypeById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            var courseType = service.GetCourseTypeById(3);

            Assert.Equal(3, courseType.Id);
            Assert.Equal("Test3", courseType.Name);
        }

        [Fact]
        public void GetCourseTypeById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            var courseType = service.GetCourseTypeById(5);

            Assert.Equal(null, courseType);
        }

        [Fact]
        public void GetCourseTypeName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            var courseType = service.GetCourseTypeName(1);

            Assert.Equal("Test1", courseType);
        }

        [Fact]
        public void GetCourseTypeName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);

            var service = new CourseTypeService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetCourseTypeName(5));
        }

        [Fact]
        public void SetCourseTypeName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            service.SetCourseTypeName(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetCourseTypeName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            service.SetCourseTypeName(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateCourseType_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            service.CreateCourseType("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<CourseType>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateCourseType_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            service.CreateCourseType("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<CourseType>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteCourseType_Deleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            service.DeleteCourseType("Test2");

            mockSet.Verify(m => m.Remove(It.IsAny<CourseType>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteCourseType_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseTypes).Returns(mockSet.Object);


            var service = new CourseTypeService(mockContext.Object);
            service.DeleteCourseType("Test4");

            mockSet.Verify(m => m.Remove(It.IsAny<CourseType>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}