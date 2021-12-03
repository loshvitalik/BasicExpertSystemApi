namespace BasicExpertSystemApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Position = c.Int(nullable: false),
                        Condition = c.String(),
                        Result = c.String(),
                        System_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Systems", t => t.System_Id)
                .Index(t => t.System_Id);
            
            CreateTable(
                "dbo.Systems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rules", "System_Id", "dbo.Systems");
            DropIndex("dbo.Rules", new[] { "System_Id" });
            DropTable("dbo.Systems");
            DropTable("dbo.Rules");
        }
    }
}
