namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInSkillsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Skills", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Skills", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skills", "Description", c => c.String());
            AlterColumn("dbo.Skills", "Name", c => c.String());
        }
    }
}
