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
    public class PaymentTypeServiceTests
    {
        private static Mock<DbSet<PaymentType>> CreateDbSetMock()
        {
            var elementsAsQueryable = new List<PaymentType>()
            {
                new PaymentType { Id = 1, Name = "Test1" },

                new PaymentType{ Id = 2, Name = "Test2" },

                new PaymentType { Id = 3, Name = "Test3" }
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<PaymentType>>();


            dbSetMock.As<IQueryable<PaymentType>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<PaymentType>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<PaymentType>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<PaymentType>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
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
        public void GetAllPaymentTypes_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);

            var service = new PaymentTypeService(mockContext.Object);
            var paymentTypes = service.GetAllPaymentTypes();

            Assert.Equal(3, paymentTypes.Count);
            Assert.Equal("Test1", paymentTypes[0].Name);
            Assert.Equal(1, paymentTypes[0].Id);
            Assert.Equal("Test2", paymentTypes[1].Name);
            Assert.Equal(2, paymentTypes[1].Id);
            Assert.Equal("Test3", paymentTypes[2].Name);
            Assert.Equal(3, paymentTypes[2].Id);
        }

        [Fact]
        public void GetPaymentTypeByName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);

            var service = new PaymentTypeService(mockContext.Object);
            var paymentType = service.GetPaymentTypeByName("Test2");

            Assert.Equal(2, paymentType.Id);
            Assert.Equal("Test2", paymentType.Name);
        }

        [Fact]
        public void GetPaymentTypeByName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);

            var service = new PaymentTypeService(mockContext.Object);
            var paymentType = service.GetPaymentTypeByName("Test4");

            Assert.Equal(null, paymentType);
        }

        [Fact]
        public void GetPaymentTypeById_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            var paymentType = service.GetPaymentTypeById(3);

            Assert.Equal(3, paymentType.Id);
            Assert.Equal("Test3", paymentType.Name);
        }

        [Fact]
        public void GetPaymentTypeById_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            var paymentType = service.GetPaymentTypeById(5);

            Assert.Equal(null, paymentType);
        }

        [Fact]
        public void GetPaymentTypeName_Get()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            var paymentType = service.GetPaymentTypeName(1);

            Assert.Equal("Test1", paymentType);
        }

        [Fact]
        public void GetPaymentTypeName_NotGet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);

            var service = new PaymentTypeService(mockContext.Object);

            Assert.Throws<NullReferenceException>(() => service.GetPaymentTypeName(5));
        }

        [Fact]
        public void SetPaymentTypeName_Set()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            service.SetPaymentTypeName(1, "Test4");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SetPaymentTypeName_NotSet()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            service.SetPaymentTypeName(1, "Test3");

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreatePaymentType_Created()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            service.CreatePaymentType("Test4");

            mockSet.Verify(m => m.Add(It.IsAny<PaymentType>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreatePaymentType_NotCreated()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            service.CreatePaymentType("Test2");

            mockSet.Verify(m => m.Add(It.IsAny<PaymentType>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeletePaymentType_Deleted()
        {
            var mockSet = CreateDbSetMock();
            var mockSet2 = CreateDbSetMockForRP();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);
            mockContext.Setup(m => m.RecruitmentProcesses).Returns(mockSet2.Object);

            var rpService = new RecruitmentProcessService(mockContext.Object);
            var service = new PaymentTypeService(mockContext.Object, rpService);

            service.DeletePaymentType(1);

            mockSet.Verify(m => m.Remove(It.IsAny<PaymentType>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeletePaymentType_NotDeleted()
        {
            var mockSet = CreateDbSetMock();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.PaymentTypes).Returns(mockSet.Object);


            var service = new PaymentTypeService(mockContext.Object);
            service.DeletePaymentType(4);

            mockSet.Verify(m => m.Remove(It.IsAny<PaymentType>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}
