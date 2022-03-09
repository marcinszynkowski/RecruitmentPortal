namespace RP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVirtualCVAdressToUser : DbMigration
    {
        public override void Up()
        {
            Sql("Update Users Set VirtualCVAdress = '" + Guid.NewGuid() + "' Where Email = 'grygowiczdawid@gmail.com'");
            Sql("Update Users Set VirtualCVAdress = '" + Guid.NewGuid() + "' Where Email = 'loginandrole@gmail.com'");
            Sql("Update Users Set VirtualCVAdress = '" + Guid.NewGuid() + "' Where Email = 'none@gmail.com'");
        }
        
        public override void Down()
        {
            Sql("Update Users Set VirtualCVAdress = NULL Where Email = 'grygowiczdawid@gmail.com'");
            Sql("Update Users Set VirtualCVAdress = NULL Where Email = 'loginandrole@gmail.com'");
            Sql("Update Users Set VirtualCVAdress = NULL Where Email = 'none@gmail.com'");
        }
    }
}