namespace BasicExpertSystemApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpertSystemLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        ElapsedMilliseconds = c.Long(nullable: false),
                        SystemId = c.Guid(nullable: false),
                        SystemName = c.String(),
                        InputText = c.String(),
                        Result = c.String(),
                        LogCsv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExpertSystemLogs");
        }
    }
}
