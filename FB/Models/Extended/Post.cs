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

        [Display(Name = "Likes")]
        public Nullable<bool> likes_count { get; set; }

        [Display(Name = "DisLikes")]
        public Nullable<bool> dislikes_count { get; set; }

        [Display(Name = "Comments")]
        public Nullable<bool> comments_count { get; set; }

        [Display(Name = "Time")]
        public Nullable<bool> time { get; set; }
    }
}