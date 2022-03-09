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
    public class CourseKindServiceTests
    {
        private static Mock<DbSet<CourseKind>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<CourseKind>()
            {
                new CourseKind { Id = 1, Name = "Test1" },

                new CourseKind{ Id = 2, Name = "Test2" },

                new CourseKind { Id = 3, Name = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<CourseKind>>();

            dbSetMock.As<IQueryable<CourseKind>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<CourseKind>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<CourseKind>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<CourseKind>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        [Fact]
        public void GetAllCourseKinds_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);

            var service = new CourseKindService(mockContext.Object);
            var courseKinds = service.GetAllCourseKinds();

            Assert.Equal(3, courseKinds.Count);
            Assert.Equal("Test1", courseKinds[0].Name);
            Assert.Equal(1, courseKinds[0].Id);
            Assert.Equal("Test2", courseKinds[1].Name);
            Assert.Equal(2, courseKinds[1].Id);
            Assert.Equal("Test3", courseKinds[2].Name);
            Assert.Equal(3, courseKinds[2].Id);
        }

        [Fact]
        public void GetCourseKindByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);

            var service = new CourseKindService(mockContext.Object);
            var courseKind = service.GetCourseKindByName("Test2");

            Assert.Equal(2, courseKind.Id);
            Assert.Equal("Test2", courseKind.Name);
        }

        [Fact]
        public void GetCourseKindByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);

            var service = new CourseKindService(mockContext.Object);
            var courseKind = service.GetCourseKindByName("Test4");

            Assert.Equal(null, courseKind);
        }

        [Fact]
        public void GetCourseKindById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            var courseKind = service.GetCourseKindById(3);

            Assert.Equal(3, courseKind.Id);
            Assert.Equal("Test3", courseKind.Name);
        }

        [Fact]
        public void GetCourseKindById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            var courseKind = service.GetCourseKindById(5);

            Assert.Equal(null, courseKind);
        }

        [Fact]
        public void GetCourseKindName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            var courseKind = service.GetCourseKindName(1);

            Assert.Equal("Test1", courseKind);
        }

        [Fact]
        public void GetCourseKindName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);

            var service = new CourseKindService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetCourseKindName(5));
        }

        [Fact]
        public void SetCourseKindName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            service.SetCourseKindName(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetCourseKindName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            service.SetCourseKindName(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateCourseKind_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            service.CreateCourseKind("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<CourseKind>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateCourseKind_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            service.CreateCourseKind("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<CourseKind>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteCourseKind_Deleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            service.DeleteCourseKind("Test2");

            mockSet.Verify(m => m.Remove(It.IsAny<CourseKind>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteCourseKind_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.CourseKinds).Returns(mockSet.Object);


            var service = new CourseKindService(mockContext.Object);
            service.DeleteCourseKind("Test4");

            mockSet.Verify(m => m.Remove(It.IsAny<CourseKind>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}