namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInPersonalDataTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonalDatas", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.PersonalDatas");
            AlterColumn("dbo.PersonalDatas", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.PersonalDatas", "LastName", c => c.String(nullable: false));
            AddPrimaryKey("dbo.PersonalDatas", "UserId");
            AddForeignKey("dbo.PersonalDatas", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.PersonalDatas", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalDatas", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PersonalDatas", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.PersonalDatas");
            AlterColumn("dbo.PersonalDatas", "LastName", c => c.String());
            AlterColumn("dbo.PersonalDatas", "FirstName", c => c.String());
            AddPrimaryKey("dbo.PersonalDatas", "Id");
            AddForeignKey("dbo.PersonalDatas", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
