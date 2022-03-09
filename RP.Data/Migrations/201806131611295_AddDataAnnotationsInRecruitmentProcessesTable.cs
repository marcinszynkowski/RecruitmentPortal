namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInRecruitmentProcessesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecruitmentProcesses", "Company", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "City", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "Requirements", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "Offer", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.RecruitmentProcesses", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecruitmentProcesses", "Phone", c => c.String());
            AlterColumn("dbo.RecruitmentProcesses", "Email", c => c.String());
            AlterColumn("dbo.RecruitmentProcesses", "Offer", c => c.String());
            AlterColumn("dbo.RecruitmentProcesses", "Requirements", c => c.String());
            AlterColumn("dbo.RecruitmentProcesses", "Position", c => c.String());
            AlterColumn("dbo.RecruitmentProcesses", "City", c => c.String());
            AlterColumn("dbo.RecruitmentProcesses", "Company", c => c.String());
        }
    }
}
