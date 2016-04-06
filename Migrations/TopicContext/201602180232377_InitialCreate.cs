namespace WebForum.Migrations.TopicContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Version = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Version = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Body = c.String(),
                        TopicId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostTopics",
                c => new
                    {
                        Post_Id = c.Guid(nullable: false),
                        Topic_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_Id, t.Topic_Id })
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.Topic_Id, cascadeDelete: true)
                .Index(t => t.Post_Id)
                .Index(t => t.Topic_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTopics", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.PostTopics", "Post_Id", "dbo.Posts");
            DropIndex("dbo.PostTopics", new[] { "Topic_Id" });
            DropIndex("dbo.PostTopics", new[] { "Post_Id" });
            DropTable("dbo.PostTopics");
            DropTable("dbo.Posts");
            DropTable("dbo.Topics");
        }
    }
}
