namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateSurveyQuestionTypeTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into SurveyQuestionTypes(Name) values ('Otwarte')");
            Sql("Insert into SurveyQuestionTypes(Name) values ('Zamkni�te jednokrotnego wyboru')");
            Sql("Insert into SurveyQuestionTypes(Name) values ('Zamkni�te wielokrotnego wyboru')");
        }
        
        public override void Down()
        {
            Sql("Delete from SurveyQuestionTypes where Name='Otwarte'");
            Sql("Delete from SurveyQuestionTypes where Name='Zamkni�te jednokrotnego wyboru'");
            Sql("Delete from SurveyQuestionTypes where Name='Zamkni�te wielokrotnego wyboru'");
        }
    }
}