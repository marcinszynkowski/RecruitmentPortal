namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeleteAdditionalInformationTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRecruitmentProcesses", newName: "RecruitmentProcessUsers");
            DropForeignKey("dbo.AdditionalInformations", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.AdditionalInformations", "RecruitmentProcessId", "dbo.RecruitmentProcesses");
            DropForeignKey("dbo.AdditionalInformations", "UserId", "dbo.Users");
            DropIndex("dbo.AdditionalInformations", new[] { "UserId" });
            DropIndex("dbo.AdditionalInformations", new[] { "RecruitmentProcessId" });
            DropIndex("dbo.AdditionalInformations", new[] { "PaymentTypeId" });
            DropPrimaryKey("dbo.RecruitmentProcessUsers");
            AddPrimaryKey("dbo.RecruitmentProcessUsers", new[] { "RecruitmentProcess_Id", "User_Id" });
            DropTable("dbo.AdditionalInformations");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropPrimaryKey("dbo.RecruitmentProcessUsers");
            AddPrimaryKey("dbo.RecruitmentProcessUsers", new[] { "User_Id", "RecruitmentProcess_Id" });
            CreateIndex("dbo.AdditionalInformations", "PaymentTypeId");
            CreateIndex("dbo.AdditionalInformations", "RecruitmentProcessId");
            CreateIndex("dbo.AdditionalInformations", "UserId");
            AddForeignKey("dbo.AdditionalInformations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdditionalInformations", "RecruitmentProcessId", "dbo.RecruitmentProcesses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdditionalInformations", "PaymentTypeId", "dbo.PaymentTypes", "Id");
            RenameTable(name: "dbo.RecruitmentProcessUsers", newName: "UserRecruitmentProcesses");
        }
    }
}
