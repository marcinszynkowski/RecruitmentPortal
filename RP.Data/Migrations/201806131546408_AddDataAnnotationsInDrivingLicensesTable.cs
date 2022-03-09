namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInDrivingLicensesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DrivingLicenses", "Category", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DrivingLicenses", "Category", c => c.String());
        }
    }
}
