using Moq;
using RP.AccountModule.Services;
using RP.AdminModule.Services;
using RP.Data.Context;
using RP.Entities.Account;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace RP.AdminModule.Tests
{
    public class XmlServiceTest
    {
        private static Mock<DbSet<User>> CreateDbSetMockForUser()
        {
            var elementsAsQueryable = new List<User>()
            {
                new User { Id = 1, Email = "Test1", Password = "Test1"},

                new User{ Id = 2, Email = "Test2", Password = "Test2"},

                new User { Id = 3, Email = "Test2", Password = "Test2"}
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<User>>();


            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        private static Mock<DbSet<SurveyAnswer>> CreateDbSetMockForSurveyAnswer()
        {
            var elementsAsQueryable = new List<SurveyAnswer>()
            {
                new SurveyAnswer { Id = 1, SurveyQuestionId = 1, UserId = 1, Answer = "Test1" },

                new SurveyAnswer{ Id = 2, SurveyQuestionId = 2, UserId = 2, Answer = "Test2" },

                new SurveyAnswer { Id = 3, SurveyQuestionId = 3, UserId = 3, Answer = "Test3" },
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<SurveyAnswer>>();


            dbSetMock.As<IQueryable<SurveyAnswer>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<SurveyAnswer>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<SurveyAnswer>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<SurveyAnswer>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => elementsAsQueryable.FirstOrDefault(d => d.Id == (int)ids[0]));

            return dbSetMock;
        }

        private static Mock<DbSet<SurveyQuestion>> CreateDbSetMockForSurveyQuestion()
        {
            var elementsAsQueryable = new List<SurveyQuestion>()
            {
                new SurveyQuestion { Id = 1, RecruitmentProcessId = 1, Question = "Test1"},

                new SurveyQuestion { Id = 2, RecruitmentProcessId = 2, Question = "Test2"},

                new SurveyQuestion { Id = 3, RecruitmentProcessId = 3, Question = "Test3"},
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<SurveyQuestion>>();


            dbSetMock.As<IQueryable<SurveyQuestion>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<SurveyQuestion>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<SurveyQuestion>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<SurveyQuestion>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
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

        //public void ImportRecruitmentProcesses()
        //{
        //    string path = ConfigurationManager.AppSettings["xmlImport"];
        //    XDocument doc;
        //    try
        //    {
        //        doc = XDocument.Load(path);
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        doc = new XDocument(new XElement("Processes"));
        //        doc.Save(path);
        //    }
        //    foreach (var process in doc.Descendants("Process"))
        //    {
        //        CreateRecruitmentProcessFromXml(process);
        //    }
        //}

        //public void ExportApplicantsData()
        //{
        //    foreach (RecruitmentProcess recruitmentProcess in _recruitmentProcessService.GetAllRecruitmentProcesses())
        //    {
        //        foreach (User user in recruitmentProcess.Users)
        //        {
        //            CreateApplicantsData(recruitmentProcess, user);
        //        }
        //    }
        //}

        [Fact]
        public void ExportApplicantsData_Export()
        {
            var mockSet = CreateDbSetMockForRP();
            var mockSet2 = CreateDbSetMockForUser();
            var mockSet3 = CreateDbSetMockForSurveyAnswer();

            var mockContext = new Mock<RPDbContext>();
            mockContext.Setup(m => m.RecruitmentProcesses).Returns(mockSet.Object);

            var rpService = new RecruitmentProcessService(mockContext.Object);
            var userService = new UserService(mockContext.Object);
            var saService = new SurveyAnswerService(mockContext.Object);
            var xmlService = new XmlService(mockContext.Object, rpService, userService, saService);

            var ex = Record.Exception(() => xmlService.ExportApplicantsData());

            Assert.Null(ex);
        }
    }
}
