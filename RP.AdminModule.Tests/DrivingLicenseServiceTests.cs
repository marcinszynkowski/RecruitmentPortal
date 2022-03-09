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
    public class DrivingLicenseServiceTests
    {
        private static Mock<DbSet<DrivingLicense>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<DrivingLicense>()
            {
                new DrivingLicense { Id = 1, Category = "Test1" },

                new DrivingLicense{ Id = 2, Category = "Test2" },

                new DrivingLicense { Id = 3, Category = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<DrivingLicense>>();

            dbSetMock.As<IQueryable<DrivingLicense>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<DrivingLicense>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<DrivingLicense>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<DrivingLicense>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        [Fact]
        public void GetAllDrivingLicenses_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);

            var service = new DrivingLicenseService(mockContext.Object);
            var drivingLicenses = service.GetAllDrivingLicenses();

            Assert.Equal(3, drivingLicenses.Count);
            Assert.Equal("Test1", drivingLicenses[0].Category);
            Assert.Equal(1, drivingLicenses[0].Id);
            Assert.Equal("Test2", drivingLicenses[1].Category);
            Assert.Equal(2, drivingLicenses[1].Id);
            Assert.Equal("Test3", drivingLicenses[2].Category);
            Assert.Equal(3, drivingLicenses[2].Id);
        }

        [Fact]
        public void GetDrivingLicenseByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);

            var service = new DrivingLicenseService(mockContext.Object);
            var drivingLicense = service.GetDrivingLicenseByCategory("Test2");

            Assert.Equal(2, drivingLicense.Id);
            Assert.Equal("Test2", drivingLicense.Category);
        }

        [Fact]
        public void GetDrivingLicenseByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);

            var service = new DrivingLicenseService(mockContext.Object);
            var drivingLicense = service.GetDrivingLicenseByCategory("Test4");

            Assert.Equal(null, drivingLicense);
        }

        [Fact]
        public void GetDrivingLicenseById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            var drivingLicense = service.GetDrivingLicenseById(3);

            Assert.Equal(3, drivingLicense.Id);
            Assert.Equal("Test3", drivingLicense.Category);
        }

        [Fact]
        public void GetDrivingLicenseById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            var drivingLicense = service.GetDrivingLicenseById(5);

            Assert.Equal(null, drivingLicense);
        }

        [Fact]
        public void GetDrivingLicenseName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            var drivingLicense = service.GetDrivingLicenseCategory(1);

            Assert.Equal("Test1", drivingLicense);
        }

        [Fact]
        public void GetDrivingLicenseName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);

            var service = new DrivingLicenseService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetDrivingLicenseCategory(5));
        }

        [Fact]
        public void SetDrivingLicenseName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            service.SetDrivingLicenseCategory(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetDrivingLicenseName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            service.SetDrivingLicenseCategory(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateDrivingLicense_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            service.CreateDrivingLicense("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<DrivingLicense>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateDrivingLicense_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            service.CreateDrivingLicense("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<DrivingLicense>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteDrivingLicense_Deleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            service.DeleteDrivingLicense("Test2");

            mockSet.Verify(m => m.Remove(It.IsAny<DrivingLicense>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteDrivingLicense_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.DrivingLicenses).Returns(mockSet.Object);


            var service = new DrivingLicenseService(mockContext.Object);
            service.DeleteDrivingLicense("Test4");

            mockSet.Verify(m => m.Remove(It.IsAny<DrivingLicense>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}