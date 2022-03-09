namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TableAdnotationToNotPluralWorkExperenceTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WorkExperiences", newName: "WorkExperience");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.WorkExperience", newName: "WorkExperiences");
        }
    }
}
