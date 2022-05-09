using FB.Models;
using System;
using System.Collections.Generic;
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
    }
}