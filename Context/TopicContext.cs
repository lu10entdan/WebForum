using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebForum.Models;

namespace WebForum.Context
{
    public class TopicContext : DbContext
    {
        public TopicContext() : base("TopicContext")
        {
            Database.SetInitializer<TopicContext>(new CreateDatabaseIfNotExists<TopicContext>());

            //Database.SetInitializer<TopicContext>(new DropCreateDatabaseIfModelChanges<TopicContext>());
            //Database.SetInitializer<TopicContext>(new DropCreateDatabaseAlways<TopicContext>());
            //Database.SetInitializer<TopicContext>(new TopicInitializer());
        }

        public DbSet<Topic> Topics { get; set; }
    }
}