namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCompanyColumnAndRemoveDateColumnsRPTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentProcesses", "Company", c => c.String());
            DropColumn("dbo.RecruitmentProcesses", "DateFrom");
            DropColumn("dbo.RecruitmentProcesses", "DateTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecruitmentProcesses", "DateTo", c => c.DateTime());
            AddColumn("dbo.RecruitmentProcesses", "DateFrom", c => c.DateTime());
            DropColumn("dbo.RecruitmentProcesses", "Company");
        }
    }
}
