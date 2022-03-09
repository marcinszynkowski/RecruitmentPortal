namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RecruitmentProcessUsers", newName: "UserRecruitmentProcesses");
            DropPrimaryKey("dbo.UserRecruitmentProcesses");
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyQuestionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RecruitmentProcesses", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.RecruitmentProcesses", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.RecruitmentProcesses", "PositionId", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyQuestions", "SurveyQuestionTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyQuestions", "ParentQuestionId", c => c.Int());
            AddPrimaryKey("dbo.UserRecruitmentProcesses", new[] { "User_Id", "RecruitmentProcess_Id" });
            CreateIndex("dbo.RecruitmentProcesses", "CompanyId");
            CreateIndex("dbo.RecruitmentProcesses", "CityId");
            CreateIndex("dbo.RecruitmentProcesses", "PositionId");
            CreateIndex("dbo.SurveyQuestions", "SurveyQuestionTypeId");
            AddForeignKey("dbo.RecruitmentProcesses", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecruitmentProcesses", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecruitmentProcesses", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SurveyQuestions", "SurveyQuestionTypeId", "dbo.SurveyQuestionTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.RecruitmentProcesses", "Company");
            DropColumn("dbo.RecruitmentProcesses", "City");
            DropColumn("dbo.RecruitmentProcesses", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecruitmentProcesses", "Position", c => c.String(nullable: false));
            AddColumn("dbo.RecruitmentProcesses", "City", c => c.String(nullable: false));
            AddColumn("dbo.RecruitmentProcesses", "Company", c => c.String(nullable: false));
            DropForeignKey("dbo.SurveyQuestions", "SurveyQuestionTypeId", "dbo.SurveyQuestionTypes");
            DropForeignKey("dbo.RecruitmentProcesses", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.RecruitmentProcesses", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.RecruitmentProcesses", "CityId", "dbo.Cities");
            DropIndex("dbo.SurveyQuestions", new[] { "SurveyQuestionTypeId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "PositionId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "CityId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "CompanyId" });
            DropPrimaryKey("dbo.UserRecruitmentProcesses");
            DropColumn("dbo.SurveyQuestions", "ParentQuestionId");
            DropColumn("dbo.SurveyQuestions", "SurveyQuestionTypeId");
            DropColumn("dbo.RecruitmentProcesses", "PositionId");
            DropColumn("dbo.RecruitmentProcesses", "CityId");
            DropColumn("dbo.RecruitmentProcesses", "CompanyId");
            DropTable("dbo.SurveyQuestionTypes");
            DropTable("dbo.Positions");
            DropTable("dbo.Companies");
            DropTable("dbo.Cities");
            AddPrimaryKey("dbo.UserRecruitmentProcesses", new[] { "RecruitmentProcess_Id", "User_Id" });
            RenameTable(name: "dbo.UserRecruitmentProcesses", newName: "RecruitmentProcessUsers");
        }
    }
}
