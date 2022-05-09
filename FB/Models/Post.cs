//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.comments = new HashSet<comment>();
            this.Reacts = new HashSet<React>();
        }
    
        public int id { get; set; }
        public int poster_id { get; set; }
        public string content { get; set; }
        public System.DateTime time { get; set; }
        public int comments_count { get; set; }
        public int likes_count { get; set; }
        public int dislikes_count { get; set; }
        public Nullable<bool> is_private { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<React> Reacts { get; set; }
    }
}
