namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInProcessStatusTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcessStatus", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcessStatus", "Name", c => c.String());
        }
    }
}
