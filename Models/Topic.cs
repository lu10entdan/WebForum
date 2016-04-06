using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace WebForum.Models
{
    public class Topic
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime LastUpdated { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public bool Deleted { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Topic()
        {
            Id = Guid.NewGuid();

            Version += 1;

            LastUpdated = DateTime.UtcNow;

            Deleted = false;
        }
    }
}
