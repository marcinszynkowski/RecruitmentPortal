namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateProcessStatusTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into ProcessStatus(Name) values ('w trakcie')");
            Sql("Insert into ProcessStatus(Name) values ('zakoñczony')");
        }
        
        public override void Down()
        {
            Sql("Delete from ProcessStatus where Name='w trakcie'");
            Sql("Delete from ProcessStatus where Name='zakoñczony'");
        }
    }
}