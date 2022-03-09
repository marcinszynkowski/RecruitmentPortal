namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateRolesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Roles(Name, Description) values ('Administrator', 'Administrator Portalu, mo¿e wszystko')");
            Sql("Insert into Roles(Name, Description) values ('U¿ytkownik', 'U¿ytkownik Portalu, mo¿e niewiele')");
            Sql("Insert into Roles(Name, Description) values ('Nieaktywowany', 'U¿ytkownik Portalu przed aktywacj¹ konta, mo¿e aktywowaæ konto')");
        }

        public override void Down()
        {
            Sql("Delete from Roles where Name='Administrator'");
            Sql("Delete from Roles where Name='U¿ytkownik'");
            Sql("Delete from Roles where Name='Nieaktywowany'");
        }
    }
}
