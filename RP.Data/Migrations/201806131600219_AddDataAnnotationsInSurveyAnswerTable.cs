namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInSurveyAnswerTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SurveyAnswers", "Answer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SurveyAnswers", "Answer", c => c.String());
        }
    }
}
