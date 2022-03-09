namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPersonalDataForCreatedUsers : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into PersonalData(UserId, FirstName, LastName) values (1, 'Dawid', 'Grygowicz')");
            Sql("Insert into PersonalData(UserId, FirstName, LastName) values (2, 'Dawid', 'Grygowicz')");
            Sql("Insert into PersonalData(UserId, FirstName, LastName) values (3, 'Dawid', 'Grygowicz')");
        }
        
        public override void Down()
        {
            Sql("Delete from PersonalData where UserId = 1");
            Sql("Delete from PersonalData where UserId = 2");
            Sql("Delete from PersonalData where UserId = 3");
        }
    }
}