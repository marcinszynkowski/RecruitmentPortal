namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInFormOfEmploymentTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FormOfEmployments", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FormOfEmployments", "Name", c => c.String());
        }
    }
}
