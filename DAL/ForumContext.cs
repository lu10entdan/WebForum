using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForum.Models;

namespace WebForum.DAL
{
    public class ForumContext : DbContext
    {

        public ForumContext() : base("ForumContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ForumContext>());
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Match> Matches { get; set; }

        // This would make the tables singlular instead of pluralized
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}