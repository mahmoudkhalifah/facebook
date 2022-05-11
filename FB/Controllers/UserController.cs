using FB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FB.Controllers
{
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            if(ModelState.IsValid)
            {
                post.poster_id = int.Parse(User.Identity.Name);
                post.time = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }  

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            int userId = int.Parse(User.Identity.Name);
            User user = db.Users.Where(u => u.id == userId).ToList()[0];
            user.confirmPassword = user.password;
            return View(user);
        }
        
        [HttpPost]
        public ActionResult EditProfile(User user, HttpPostedFileBase image)
        {
            //if validation is ok
            if (ModelState.IsValid)
            {
                //if email already exists in our database
                if (ExistingChecker.isEmailExistEdit(user.email))
                {
                    ModelState.AddModelError("EmailExist", "Email already taken!");
                    return View(user);
                }
                //if phone number already exists in our database
                if (ExistingChecker.isPhoneNumberExistEdit(user.phone_number))
                {
                    ModelState.AddModelError("PhoneNumberExist", "Phone number already taken!");
                    return View(user);
                }
                if (image != null && image.ContentLength != 0)
                {
                    string ext = Path.GetExtension(image.FileName);
                    var exts = new[] { "jpg", "jpeg", "png" };
                    if (exts.Contains(ext.Substring(1)))
                    {
                        string imageGuid = Guid.NewGuid().ToString();
                        user.profile_picture = imageGuid + ext;
                        image.SaveAs(Server.MapPath($"~/uploads/profile_pictures/{user.profile_picture}"));
                    }
                    else
                    {
                        ModelState.AddModelError("NotValidImage", "File extention is not valid! try uplaod png,jpg or jpeg image");
                        return View(user);
                    }

                }
                /*u.id = user.id;
                u.first_name = user.first_name;
                u.last_name = user.last_name;
                u.email = user.email;
                u.phone_number = user.phone_number;
                u.password = user.password;
                u.confirmPassword = user.confirmPassword;*/

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);

        }
    }
}