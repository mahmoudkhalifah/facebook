using FB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FB.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            List<User> users = db.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(User user,HttpPostedFileBase image)
        {
            bool Status = false;
            String Message = "";

            //if validation is ok
            if(ModelState.IsValid)
            {
                //if email already exists in our database
                if(ExistingChecker.isEmailExist(user.email))
                {
                    ModelState.AddModelError("EmailExist", "Email already taken!");
                    return View(user);
                }
                //if phone number already exists in our database
                if (ExistingChecker.isPhoneNumberExist(user.phone_number))
                {
                    ModelState.AddModelError("PhoneNumberExist", "Phone number already taken!");
                    return View(user);
                }
                if (image!= null && image.ContentLength != 0)
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
                //hash password
                user.password = Crypt.Hash(user.password);
                user.confirmPassword = Crypt.Hash(user.confirmPassword);

                //add user to database
                db.Users.Add(user);
                db.SaveChanges();
                //status true to let the view know that the registration is done
                Status = true;
                //go to login
                return RedirectToAction("Login");
            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Status = Status;
            ViewBag.Message = Message;
            return View(user);
            
        }


        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login,string ReturnUrl)
        {
            string Message = "";
            //if validation is ok
            if (ModelState.IsValid)
            {
                //if our database have entry with the entered email and password then should login
                login.password = Crypt.Hash(login.password);
                if (db.Users.Where(u => u.email == login.email && u.password == login.password).ToList().Count()==1)
                {
                    //get id of the user to keep it in the cookie
                    int id = db.Users.Where(user => user.email == login.email).Select(user => user.id).ToList()[0];
                    // if remeber me checked make the session last one year otherwise last only 2 hours
                    int timeoutMins = login.rememberMe ? 525600 : 525600;
                    var ticket = new FormsAuthenticationTicket(id.ToString(),login.rememberMe,timeoutMins);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeoutMins);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if(Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index","User");
                }
                else
                {
                    Message = "Incorrect email or password";
                }
            }
            ViewBag.Message = Message;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}