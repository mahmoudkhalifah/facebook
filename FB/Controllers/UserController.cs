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
       
        //public static String idcook = System.Web.HttpContext.Current.User.Identity.Name;
        //public int idcurr = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
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
        public ActionResult FriendesList()
        {
            int idcurr = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);

            List<Friend> friendeslist = db.Friends.Where(f => f.user1_id == idcurr || f.user2_id == idcurr).ToList();
            List<User> userslist = db.Users.ToList();
            //List<User> userslist = db.Users.Where(p => friendeslist.All(p2 => p2.user1_id == p.id || p2.user2_id == p.id && p.id != id)).ToList();
            List<User> userslist2 = userslist.Where(u => friendeslist.Any( f => f.user1_id == u.id || f.user2_id == u.id  ) && u.id != idcurr).ToList();
            //&& p.id != id && p.id != id
            return View(userslist2);
        }
        [Authorize]
        [HttpGet]
        public ActionResult MyProfile()
        {
            int idcurr = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
            List<Post> postlist = db.Posts.Where(p => p.poster_id == idcurr).ToList();
            
            return View(postlist);

        }
        [Authorize]
        [HttpGet]
        public ActionResult ShowFriend(int? id)
        {
            int idcurr = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
            if (id != null)
            {
                Friend friend = db.Friends.SingleOrDefault(f => (f.user1_id == id && f.user2_id == idcurr )|| (f.user1_id == idcurr && f.user2_id == id));
                if (friend == null)
                    return HttpNotFound();
                List<Post> postlist = db.Posts.Where(p => p.poster_id == id).ToList();
            
                ViewBag.user = db.Users.Where(u => u.id == id).First();
                return View(postlist);
            }
            else
                return RedirectToAction("FriendesList");

        }
    }
}