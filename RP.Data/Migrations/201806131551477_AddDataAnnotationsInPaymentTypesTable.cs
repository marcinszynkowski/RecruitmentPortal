namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInPaymentTypesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentTypes", "Name", c => c.String());
        }
    }
}
