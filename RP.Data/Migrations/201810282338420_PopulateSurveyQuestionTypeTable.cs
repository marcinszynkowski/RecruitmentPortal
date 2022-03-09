namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateSurveyQuestionTypeTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into SurveyQuestionTypes(Name) values ('Otwarte')");
            Sql("Insert into SurveyQuestionTypes(Name) values ('Zamkniête jednokrotnego wyboru')");
            Sql("Insert into SurveyQuestionTypes(Name) values ('Zamkniête wielokrotnego wyboru')");
        }
        
        public override void Down()
        {
            Sql("Delete from SurveyQuestionTypes where Name='Otwarte'");
            Sql("Delete from SurveyQuestionTypes where Name='Zamkniête jednokrotnego wyboru'");
            Sql("Delete from SurveyQuestionTypes where Name='Zamkniête wielokrotnego wyboru'");
        }
    }
}