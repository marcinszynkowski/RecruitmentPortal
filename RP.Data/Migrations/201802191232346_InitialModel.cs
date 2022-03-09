namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RecruitmentProcessId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(),
                        PaymentFrom = c.Int(),
                        PaymentTo = c.Int(),
                        IsUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId)
                .ForeignKey("dbo.RecruitmentProcesses", t => t.RecruitmentProcessId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RecruitmentProcessId)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecruitmentProcesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessCode = c.Int(nullable: false),
                        City = c.String(),
                        Position = c.String(),
                        Requirements = c.String(),
                        Offer = c.String(),
                        DateFrom = c.DateTime(),
                        DateTo = c.DateTime(),
                        FormOfEmploymentId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(nullable: false),
                        PaymentFrom = c.Int(),
                        PaymentTo = c.Int(),
                        Email = c.String(),
                        Phone = c.String(),
                        ProcessStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormOfEmployments", t => t.FormOfEmploymentId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ProcessStatus", t => t.ProcessStatusId, cascadeDelete: true)
                .Index(t => t.FormOfEmploymentId)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.ProcessStatusId);
            
            CreateTable(
                "dbo.FormOfEmployments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecruitmentProcessId = c.Int(nullable: false),
                        Question = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecruitmentProcesses", t => t.RecruitmentProcessId, cascadeDelete: true)
                .Index(t => t.RecruitmentProcessId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        AccessFailedCount = c.Int(nullable: false),
                        LastLogin = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        PasswordLastModified = c.DateTime(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseTypeId = c.Int(nullable: false),
                        CourseKindId = c.Int(nullable: false),
                        DateFrom = c.DateTime(),
                        DateTo = c.DateTime(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseKinds", t => t.CourseKindId, cascadeDelete: true)
                .ForeignKey("dbo.CourseTypes", t => t.CourseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseTypeId)
                .Index(t => t.CourseKindId);
            
            CreateTable(
                "dbo.CourseKinds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrawoJazdy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EducationLevelId = c.Int(nullable: false),
                        NameOfSchool = c.String(),
                        Major = c.String(),
                        Minor = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationLevels", t => t.EducationLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EducationLevelId);
            
            CreateTable(
                "dbo.EducationLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UżytkownicyJęzykiObce",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ForeignLanguageId = c.Int(nullable: false),
                        ForeignLanguageLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForeignLanguageLevels", t => t.ForeignLanguageLevelId, cascadeDelete: true)
                .ForeignKey("dbo.ForeignLanguages", t => t.ForeignLanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ForeignLanguageId)
                .Index(t => t.ForeignLanguageLevelId);
            
            CreateTable(
                "dbo.ForeignLanguageLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForeignLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrzebiegZatrudnienia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Position = c.String(),
                        DateFrom = c.DateTime(),
                        DateTo = c.DateTime(),
                        Responsibilities = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        VirtualCvVisibility = c.Boolean(nullable: false),
                        Cv = c.String(),
                        LetterOfMotivation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PersonalDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(),
                        City = c.String(),
                        Phone = c.String(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SurveyAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SurveyQuestionId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyQuestions", t => t.SurveyQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SurveyQuestionId);
            
            CreateTable(
                "dbo.DrivingLicenseUsers",
                c => new
                    {
                        DrivingLicense_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrivingLicense_Id, t.User_Id })
                .ForeignKey("dbo.PrawoJazdy", t => t.DrivingLicense_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.DrivingLicense_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserRecruitmentProcesses",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        RecruitmentProcess_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.RecruitmentProcess_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.RecruitmentProcesses", t => t.RecruitmentProcess_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.RecruitmentProcess_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyAnswers", "UserId", "dbo.Users");
            DropForeignKey("dbo.SurveyAnswers", "SurveyQuestionId", "dbo.SurveyQuestions");
            DropForeignKey("dbo.PersonalDatas", "UserId", "dbo.Users");
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropForeignKey("dbo.AdditionalInformations", "UserId", "dbo.Users");
            DropForeignKey("dbo.AdditionalInformations", "RecruitmentProcessId", "dbo.RecruitmentProcesses");
            DropForeignKey("dbo.AdditionalInformations", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.PrzebiegZatrudnienia", "UserId", "dbo.Users");
            DropForeignKey("dbo.UżytkownicyJęzykiObce", "UserId", "dbo.Users");
            DropForeignKey("dbo.UżytkownicyJęzykiObce", "ForeignLanguageId", "dbo.ForeignLanguages");
            DropForeignKey("dbo.UżytkownicyJęzykiObce", "ForeignLanguageLevelId", "dbo.ForeignLanguageLevels");
            DropForeignKey("dbo.Skills", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRecruitmentProcesses", "RecruitmentProcess_Id", "dbo.RecruitmentProcesses");
            DropForeignKey("dbo.UserRecruitmentProcesses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Interests", "UserId", "dbo.Users");
            DropForeignKey("dbo.Educations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Educations", "EducationLevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.DrivingLicenseUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.DrivingLicenseUsers", "DrivingLicense_Id", "dbo.PrawoJazdy");
            DropForeignKey("dbo.Courses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Courses", "CourseTypeId", "dbo.CourseTypes");
            DropForeignKey("dbo.Courses", "CourseKindId", "dbo.CourseKinds");
            DropForeignKey("dbo.SurveyQuestions", "RecruitmentProcessId", "dbo.RecruitmentProcesses");
            DropForeignKey("dbo.RecruitmentProcesses", "ProcessStatusId", "dbo.ProcessStatus");
            DropForeignKey("dbo.RecruitmentProcesses", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.RecruitmentProcesses", "FormOfEmploymentId", "dbo.FormOfEmployments");
            DropIndex("dbo.UserRecruitmentProcesses", new[] { "RecruitmentProcess_Id" });
            DropIndex("dbo.UserRecruitmentProcesses", new[] { "User_Id" });
            DropIndex("dbo.DrivingLicenseUsers", new[] { "User_Id" });
            DropIndex("dbo.DrivingLicenseUsers", new[] { "DrivingLicense_Id" });
            DropIndex("dbo.SurveyAnswers", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyAnswers", new[] { "UserId" });
            DropIndex("dbo.PersonalDatas", new[] { "UserId" });
            DropIndex("dbo.Documents", new[] { "UserId" });
            DropIndex("dbo.PrzebiegZatrudnienia", new[] { "UserId" });
            DropIndex("dbo.UżytkownicyJęzykiObce", new[] { "ForeignLanguageLevelId" });
            DropIndex("dbo.UżytkownicyJęzykiObce", new[] { "ForeignLanguageId" });
            DropIndex("dbo.UżytkownicyJęzykiObce", new[] { "UserId" });
            DropIndex("dbo.Skills", new[] { "UserId" });
            DropIndex("dbo.Interests", new[] { "UserId" });
            DropIndex("dbo.Educations", new[] { "EducationLevelId" });
            DropIndex("dbo.Educations", new[] { "UserId" });
            DropIndex("dbo.Courses", new[] { "CourseKindId" });
            DropIndex("dbo.Courses", new[] { "CourseTypeId" });
            DropIndex("dbo.Courses", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.SurveyQuestions", new[] { "RecruitmentProcessId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "ProcessStatusId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "PaymentTypeId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "FormOfEmploymentId" });
            DropIndex("dbo.AdditionalInformations", new[] { "PaymentTypeId" });
            DropIndex("dbo.AdditionalInformations", new[] { "RecruitmentProcessId" });
            DropIndex("dbo.AdditionalInformations", new[] { "UserId" });
            DropTable("dbo.UserRecruitmentProcesses");
            DropTable("dbo.DrivingLicenseUsers");
            DropTable("dbo.SurveyAnswers");
            DropTable("dbo.PersonalDatas");
            DropTable("dbo.Documents");
            DropTable("dbo.PrzebiegZatrudnienia");
            DropTable("dbo.ForeignLanguages");
            DropTable("dbo.ForeignLanguageLevels");
            DropTable("dbo.UżytkownicyJęzykiObce");
            DropTable("dbo.Skills");
            DropTable("dbo.Roles");
            DropTable("dbo.Interests");
            DropTable("dbo.EducationLevels");
            DropTable("dbo.Educations");
            DropTable("dbo.PrawoJazdy");
            DropTable("dbo.CourseTypes");
            DropTable("dbo.CourseKinds");
            DropTable("dbo.Courses");
            DropTable("dbo.Users");
            DropTable("dbo.SurveyQuestions");
            DropTable("dbo.ProcessStatus");
            DropTable("dbo.FormOfEmployments");
            DropTable("dbo.RecruitmentProcesses");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.AdditionalInformations");
        }
    }
}