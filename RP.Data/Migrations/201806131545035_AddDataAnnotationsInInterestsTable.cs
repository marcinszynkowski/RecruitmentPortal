namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInInterestsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interests", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interests", "Name", c => c.String());
        }
    }
}
