using FB.Models;
using System;
using System.Collections.Generic;
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

        [NonAction]
        public bool isEmailExist(string email)
        {
            return db.Users.Where(u => u.email == email).ToList().Count() != 0;
        }

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            bool Status = false;
            String Message = "";

            //if validation is ok
            if(ModelState.IsValid)
            {
                //if email already exists in our database
                if(isEmailExist(user.email))
                {
                    ModelState.AddModelError("EmailExist", "Email already taken!");
                    return View(user);
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
                    // if remeber me checked make the session last one year otherwise last only 2 hours
                    int timeoutMins = login.rememberMe ? 525600 : 120;
                    var ticket = new FormsAuthenticationTicket(login.email,login.rememberMe,timeoutMins);
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