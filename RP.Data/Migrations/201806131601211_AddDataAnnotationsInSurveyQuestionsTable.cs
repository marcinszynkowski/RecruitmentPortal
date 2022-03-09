namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInSurveyQuestionsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SurveyQuestions", "Question", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SurveyQuestions", "Question", c => c.String());
        }
    }
}
