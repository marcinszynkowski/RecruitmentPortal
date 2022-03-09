namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulatePositionTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Positions(Name) values ('Programista Java')");
            Sql("Insert into Positions(Name) values ('Programsta C#')");
            Sql("Insert into Positions(Name) values ('Seciowiec')");
            Sql("Insert into Positions(Name) values ('M³odszy Programista')");
            Sql("Insert into Positions(Name) values ('Ksiêgowy')");
            Sql("Insert into Positions(Name) values ('Sekretarka')");
        }
        
        public override void Down()
        {
            Sql("Delete from Positions where Name='Programista Java'");
            Sql("Delete from Positions where Name='Programsta C#'");
            Sql("Delete from Positions where Name='Seciowiec'");
            Sql("Delete from Positions where Name='M³odszy Programista'");
            Sql("Delete from Positions where Name='Ksiêgowy'");
            Sql("Delete from Positions where Name='Sekretarka'");
        }
    }
}