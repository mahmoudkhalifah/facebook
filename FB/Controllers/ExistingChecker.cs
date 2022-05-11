using FB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FB.Controllers
{
    public class ExistingChecker
    {
        
        [NonAction]
        public static bool isEmailExist(string email)
        {
            DataContext db = new DataContext();
            return db.Users.Where(u => u.email == email).ToList().Count() != 0;
        }

        [NonAction]
        public static bool isEmailExistEdit(string email)
        {
            DataContext db = new DataContext();
            int id = int.Parse(HttpContext.Current.User.Identity.Name);
            return db.Users.Where(u => u.email == email && u.id != id).ToList().Count() != 0;
        }

        [NonAction]
        public static bool isPhoneNumberExist(string phoneNumber)
        {
            DataContext db = new DataContext();
            return db.Users.Where(u => u.phone_number == phoneNumber).ToList().Count() != 0;
        }

        [NonAction]
        public static bool isPhoneNumberExistEdit(string phoneNumber)
        {
            DataContext db = new DataContext();
            int id = int.Parse(HttpContext.Current.User.Identity.Name);
            return db.Users.Where(u => u.phone_number == phoneNumber && u.id != id).ToList().Count() != 0;
        }
    }
}