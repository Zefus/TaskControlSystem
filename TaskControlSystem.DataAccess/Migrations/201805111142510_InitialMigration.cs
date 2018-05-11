namespace TaskControlSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Executors = c.String(),
                        Status = c.Byte(nullable: false),
                        RegisterDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CompletionDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        SystemTask_Id = c.Int(),
                        ParentSystemTask_Id = c.Int(),
                        SystemTask_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SystemTask", t => t.SystemTask_Id)
                .ForeignKey("dbo.SystemTask", t => t.ParentSystemTask_Id)
                .ForeignKey("dbo.SystemTask", t => t.SystemTask_Id1)
                .Index(t => t.SystemTask_Id)
                .Index(t => t.ParentSystemTask_Id)
                .Index(t => t.SystemTask_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemTask", "SystemTask_Id1", "dbo.SystemTask");
            DropForeignKey("dbo.SystemTask", "ParentSystemTask_Id", "dbo.SystemTask");
            DropForeignKey("dbo.SystemTask", "SystemTask_Id", "dbo.SystemTask");
            DropIndex("dbo.SystemTask", new[] { "SystemTask_Id1" });
            DropIndex("dbo.SystemTask", new[] { "ParentSystemTask_Id" });
            DropIndex("dbo.SystemTask", new[] { "SystemTask_Id" });
            DropTable("dbo.SystemTask");
        }
    }
}
