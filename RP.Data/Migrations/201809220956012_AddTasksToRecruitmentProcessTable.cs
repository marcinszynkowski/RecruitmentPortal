namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTasksToRecruitmentProcessTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecruitmentProcesses", "FormOfEmploymentId", "dbo.FormOfEmployments");
            DropForeignKey("dbo.RecruitmentProcesses", "PaymentTypeId", "dbo.PaymentTypes");
            DropIndex("dbo.RecruitmentProcesses", new[] { "FormOfEmploymentId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "PaymentTypeId" });
            AddColumn("dbo.RecruitmentProcesses", "Tasks", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "FormOfEmploymentId", c => c.Int());
            AlterColumn("dbo.RecruitmentProcesses", "PaymentTypeId", c => c.Int());
            CreateIndex("dbo.RecruitmentProcesses", "FormOfEmploymentId");
            CreateIndex("dbo.RecruitmentProcesses", "PaymentTypeId");
            AddForeignKey("dbo.RecruitmentProcesses", "FormOfEmploymentId", "dbo.FormOfEmployments", "Id");
            AddForeignKey("dbo.RecruitmentProcesses", "PaymentTypeId", "dbo.PaymentTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecruitmentProcesses", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.RecruitmentProcesses", "FormOfEmploymentId", "dbo.FormOfEmployments");
            DropIndex("dbo.RecruitmentProcesses", new[] { "PaymentTypeId" });
            DropIndex("dbo.RecruitmentProcesses", new[] { "FormOfEmploymentId" });
            AlterColumn("dbo.RecruitmentProcesses", "PaymentTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "FormOfEmploymentId", c => c.Int(nullable: false));
            DropColumn("dbo.RecruitmentProcesses", "Tasks");
            CreateIndex("dbo.RecruitmentProcesses", "PaymentTypeId");
            CreateIndex("dbo.RecruitmentProcesses", "FormOfEmploymentId");
            AddForeignKey("dbo.RecruitmentProcesses", "PaymentTypeId", "dbo.PaymentTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecruitmentProcesses", "FormOfEmploymentId", "dbo.FormOfEmployments", "Id", cascadeDelete: true);
        }
    }
}
