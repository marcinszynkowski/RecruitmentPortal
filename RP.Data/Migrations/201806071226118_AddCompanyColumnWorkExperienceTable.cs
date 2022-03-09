namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCompanyColumnWorkExperienceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkExperiences", "Company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkExperiences", "Company");
        }
    }
}
