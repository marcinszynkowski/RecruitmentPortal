namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInForeignLanguageLevelsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForeignLanguageLevels", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ForeignLanguageLevels", "Name", c => c.String());
        }
    }
}
