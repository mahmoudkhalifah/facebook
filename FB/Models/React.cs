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
    
    public partial class React
    {
        public int reacter_id { get; set; }
        public int post_id { get; set; }
        public ReactType is_like { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
