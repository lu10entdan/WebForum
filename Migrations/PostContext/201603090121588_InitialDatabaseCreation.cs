namespace WebForum.Migrations.PostContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
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
                "dbo.TopicPosts",
                c => new
                    {
                        Topic_Id = c.Guid(nullable: false),
                        Post_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Topic_Id, t.Post_Id })
                .ForeignKey("dbo.Topics", t => t.Topic_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .Index(t => t.Topic_Id)
                .Index(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.TopicPosts", "Topic_Id", "dbo.Topics");
            DropIndex("dbo.TopicPosts", new[] { "Post_Id" });
            DropIndex("dbo.TopicPosts", new[] { "Topic_Id" });
            DropTable("dbo.TopicPosts");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
        }
    }
}
