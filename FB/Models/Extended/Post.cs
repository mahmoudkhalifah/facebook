using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FB.Models
{
    [MetadataType(typeof(PostMetadata))]
    public partial class Post
    {
        
    }

    public class PostMetadata
    {
        [Required]
        [Display(Name = "Post")]
        public string content { get; set; }

        [Display(Name = "only me")]
        public Nullable<bool> is_private { get; set; }
    }
}