using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForum.Models
{
    public class Match
    {
        // Might have to change this to be a ViewModel or make a ViewModel to work with this.
        [Key]
        public Guid Id { get; set; }
        // Set attribute Required? Will also delete this if post/topic is deleted

        [Required]
        public Guid PostId { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [ForeignKey("TopicId")]
        public virtual Topic Topics { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Posts { get; set; }

        [ForeignKey("UserEmail")]
        public virtual ApplicationUser User { get; set; }

    }
}
