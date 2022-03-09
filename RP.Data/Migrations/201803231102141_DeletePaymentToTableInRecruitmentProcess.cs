namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeletePaymentToTableInRecruitmentProcess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentProcesses", "Payment", c => c.Int());
            DropColumn("dbo.RecruitmentProcesses", "PaymentFrom");
            DropColumn("dbo.RecruitmentProcesses", "PaymentTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecruitmentProcesses", "PaymentTo", c => c.Int());
            AddColumn("dbo.RecruitmentProcesses", "PaymentFrom", c => c.Int());
            DropColumn("dbo.RecruitmentProcesses", "Payment");
        }
    }
}
