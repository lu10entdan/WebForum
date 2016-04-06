using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.ModelBinding;

namespace WebForum.Models
{
    public class Post
    {
        
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Title")]
        public string Name { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public int Version { get; set; }

        [BindNever]                           
        [ScaffoldColumn(false)]
        public DateTime LastUpdated { get; set; }
                                               
        [BindNever]
        [ScaffoldColumn(false)]              
        public bool Deleted { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        
        public string Body { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public Guid TopicId { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public Guid? UserId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        /* 
        Properties to properly navigate through the models. 
        Will be used to display a topic name from a post-TopicId
        http://blog.staticvoid.co.nz/2012/7/17/entity_framework-navigation_property_basics_with_code_first
        //public virtual ICollection<Topic> TopicListCollection { get; set; }
        */
        public Post()
        {
            Id = Guid.NewGuid();

            Version += 1;

            LastUpdated = DateTime.UtcNow;

            Date = DateTime.Now;

            Deleted = false;

            TopicId = Guid.Empty;
        }

    }
}