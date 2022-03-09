namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInForeignLanguagesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForeignLanguages", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ForeignLanguages", "Name", c => c.String());
        }
    }
}
