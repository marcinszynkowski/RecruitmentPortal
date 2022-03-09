using Moq;
using RP.AdminModule.Services;
using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace RP.AdminModule.Tests
{
    public class FormOfEmploymentServiceTests
    {
        private static Mock<DbSet<FormOfEmployment>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<FormOfEmployment>()
            {
                new FormOfEmployment { Id = 1, Name = "Test1" },

                new FormOfEmployment{ Id = 2, Name = "Test2" },

                new FormOfEmployment { Id = 3, Name = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<FormOfEmployment>>();


            dbSetMock.As<IQueryable<FormOfEmployment>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<FormOfEmployment>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<FormOfEmployment>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<FormOfEmployment>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        private static Mock<DbSet<RecruitmentProcess>> CreateDbSetMockForRP()
        {
            var elementsAsQueryable = new List<RecruitmentProcess>()
            {
                new RecruitmentProcess { Id = 1, CityId = 1, CompanyId = 1, Email = "Test1@test1.pl", FormOfEmploymentId = 1, Offer = "", Payment = 456, PaymentTypeId = 1, Phone = "657567", PositionId = 1, ProcessCode = 566, ProcessStatusId = 1, Requirements = "Test1", Tasks = "Test1" },

                new RecruitmentProcess{ Id = 2, CityId = 2, CompanyId = 2, Email = "Test2@test2.pl", FormOfEmploymentId = 2, Offer = "", Payment = 456, PaymentTypeId = 1, Phone = "657567", PositionId = 2, ProcessCode = 566, ProcessStatusId = 1, Requirements = "Test2", Tasks = "Test3" },

                new RecruitmentProcess { Id = 3, CityId = 3, CompanyId = 3, Email = "Test2@test2.pl", FormOfEmploymentId = 3, Offer = "", Payment = 456, PaymentTypeId = 1, Phone = "657567", PositionId = 3, ProcessCode = 566, ProcessStatusId = 1, Requirements = "Test2", Tasks = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<RecruitmentProcess>>();

            dbSetMock.As<IQueryable<RecruitmentProcess>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<RecruitmentProcess>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<RecruitmentProcess>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<RecruitmentProcess>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        [Fact]
        public void GetAllFormOfEmployments_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);

            var service = new FormOfEmploymentService(mockContext.Object);
            var formOfEmployments = service.GetAllFormOfEmployment();

            Assert.Equal(3, formOfEmployments.Count);
            Assert.Equal("Test1", formOfEmployments[0].Name);
            Assert.Equal(1, formOfEmployments[0].Id);
            Assert.Equal("Test2", formOfEmployments[1].Name);
            Assert.Equal(2, formOfEmployments[1].Id);
            Assert.Equal("Test3", formOfEmployments[2].Name);
            Assert.Equal(3, formOfEmployments[2].Id);
        }

        [Fact]
        public void GetFormOfEmploymentByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);

            var service = new FormOfEmploymentService(mockContext.Object);
            var formOfEmployment = service.GetFormOfEmploymentByName("Test2");

            Assert.Equal(2, formOfEmployment.Id);
            Assert.Equal("Test2", formOfEmployment.Name);
        }

        [Fact]
        public void GetFormOfEmploymentByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);

            var service = new FormOfEmploymentService(mockContext.Object);
            var formOfEmployment = service.GetFormOfEmploymentByName("Test4");

            Assert.Equal(null, formOfEmployment);
        }

        [Fact]
        public void GetFormOfEmploymentById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            var formOfEmployment = service.GetFormOfEmploymentById(3);

            Assert.Equal(3, formOfEmployment.Id);
            Assert.Equal("Test3", formOfEmployment.Name);
        }

        [Fact]
        public void GetFormOfEmploymentById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            var formOfEmployment = service.GetFormOfEmploymentById(5);

            Assert.Equal(null, formOfEmployment);
        }

        [Fact]
        public void GetFormOfEmploymentName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            var formOfEmployment = service.GetFormOfEmploymentName(1);

            Assert.Equal("Test1", formOfEmployment);
        }

        [Fact]
        public void GetFormOfEmploymentName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);

            var service = new FormOfEmploymentService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetFormOfEmploymentName(5));
        }

        [Fact]
        public void SetFormOfEmploymentName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            service.SetFormOfEmploymentName(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetFormOfEmploymentName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            service.SetFormOfEmploymentName(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateFormOfEmployment_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            service.CreateFormOfEmployment("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<FormOfEmployment>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateFormOfEmployment_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            service.CreateFormOfEmployment("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<FormOfEmployment>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteFormOfEmployment_Deleted()
        {
            var mockSet = CreateDbSetMock();
            var mockSet2 = CreateDbSetMockForRP();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);
            mockContext.Setup(m => m.RecruitmentProcesses).Returns(mockSet2.Object);

            var rpService = new RecruitmentProcessService(mockContext.Object);
            var service = new FormOfEmploymentService(mockContext.Object, rpService);

            service.DeleteFormOfEmployment(1);

            mockSet.Verify(m => m.Remove(It.IsAny<FormOfEmployment>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteFormOfEmployment_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.FormOfEmployment).Returns(mockSet.Object);


            var service = new FormOfEmploymentService(mockContext.Object);
            service.DeleteFormOfEmployment(4);

            mockSet.Verify(m => m.Remove(It.IsAny<FormOfEmployment>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}
