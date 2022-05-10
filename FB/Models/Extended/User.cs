using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FB.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string confirmPassword { get; set; }
    }
    public class UserMetadata
    {
        [Required]
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Password length mush be at least 8")]
        public string password { get; set; }


        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage = "Not matching")]
        public string confirmPassword { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string phone_number { get; set; }

        [Display(Name = "Profile picture")]
        [DataType(DataType.ImageUrl)]
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public string profile_picture { get; set; }

    }
}