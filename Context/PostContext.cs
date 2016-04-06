using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForum.Models;

namespace WebForum.Context
{
    public class PostContext : DbContext
    {

        public PostContext() : base("PostContext")
        {
            Database.SetInitializer<PostContext>(new CreateDatabaseIfNotExists<PostContext>());

            //Database.SetInitializer<PostContext>(new DropCreateDatabaseIfModelChanges<PostContext>());
            //Database.SetInitializer<PostContext>(new DropCreateDatabaseAlways<PostContext>());
            //Database.SetInitializer<PostContext>(new PostInitializer());
        }

        public DbSet<Post> Posts { get; set; }
    }
}
