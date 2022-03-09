namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Web.Helpers;

    public partial class CreateAdminAndUserInUserTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Users(Email, Password, AccessFailedCount, LockoutEnabled, RoleId) values " +
                "('loginandrole@gmail.com', '"+ Crypto.HashPassword("admin") + "', 0, 'false', 1)");
            Sql("Insert into Users(Email, Password, AccessFailedCount, LockoutEnabled, RoleId) values " +
                "('grygowiczdawid@gmail.com', '" + Crypto.HashPassword("user") + "', 0, 'false', 2)");
            Sql("Insert into Users(Email, Password, AccessFailedCount, LockoutEnabled, RoleId) values " +
                "('none@gmail.com', '" + Crypto.HashPassword("none") + "', 0, 'false', 3)");
        }

        public override void Down()
        {
            Sql("Delete from Users where Email='loginandrole@gmail.com'");
            Sql("Delete from Users where Email='grygowiczdawid@gmail.com'");
            Sql("Delete from Users where Email='none@gmail.com'");
        }
    }
}