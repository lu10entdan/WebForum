using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebForum.Context;
using WebForum.Models;

namespace WebForum.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        readonly TopicContext tdb = new TopicContext();
        readonly PostContext db = new PostContext();

        // GET: Post
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(Guid? TopicId)
        {

            try
            {
                if (Request.IsAuthenticated)
                {
                    // Kept losing the TopicId when browsing through Post Pages
                    if (Session["TopicId"] != null)
                        TopicId = (Guid)Session["TopicId"];

                    if (TopicId != null)
                    {
                        Session["TopicId"] = TopicId;
                        ViewBag.Message = "Here are all the posts for topic: "
                            + tdb.Topics.Find(TopicId).Name;
                        var post = db.Posts.Where(p => p.TopicId == TopicId);
                        TempData["Topics"] = tdb.Topics;
                        return View(post);
                    }
                }

            }
            catch (Exception)
            {
                ViewBag.Message = "Error collecting the topic's posts";
                return View(db.Posts.ToList());
            }
            ViewBag.Message = "Here are all the posts";
            return View(db.Posts.ToList());
        }

        // GET: Post/Details/5
        [Authorize]
        public ActionResult Display(Guid id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }

        // GET: Post/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create(Guid? TopicId)
        {
            ViewBag.HasTopicId = false;
            ViewBag.TopicList = tdb.Topics.AsEnumerable();
            if (Session["TopicId"] != null)
                TopicId = (Guid)Session["TopicId"];
            if (TopicId != null)
            {
                Session["TopicId"] = TopicId;
                ViewBag.HasTopicId = true;
            }
            return View();

        }

        // POST: Post/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Post post, Guid? TopicId, string selectedTopicId)
        {
            ViewBag.HasTopicId = false;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["TopicId"] != null)
                        TopicId = (Guid)Session["TopicId"];

                    if (Request.IsAuthenticated)
                    {
                        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                            .GetUserManager<ApplicationUserManager>()
                            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                        post.UserId = Guid.Parse(user.Id);
                    }
                    if (TopicId != null)
                    {
                        Session["TopicId"] = TopicId;
                        post.TopicId = TopicId.Value;
                    }

                    else
                        post.TopicId = Guid.Parse(selectedTopicId);

                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index", post.Id);
                }
                return View(post);
            }
            catch
            {
                return View();
            }
        }
        // GET
        [HttpGet]
        [Authorize]
        public ActionResult UserPost(Guid? TopicId)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    // Kept losing the TopicId when browsing through Post Pages
                    if (TopicId != null)
                    {
                        Session["TopicId"] = TopicId;
                    }

                    // Get current user Id through Owin
                    ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    Guid userId = Guid.Parse(user.Id);

                    ViewBag.Message = "Here are all the posts for user: "
                        + user.Email;
                    var post = db.Posts.Where(p => p.UserId == userId);

                    return View(post);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error collecting your posts because of Exception: \n " + ex;
                return RedirectToAction("Index");
            }

            ViewBag.Message = "You are not logged in.";
            return RedirectToAction("Index");
        }
        // GET: Post/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();
            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(post);
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Post post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                Post post = new Post();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    post = db.Posts.Find(id);
                    if (post == null)
                        return HttpNotFound();
                    db.Posts.Remove(post);
                    db.SaveChanges();
                    return RedirectToAction("UserPost");
                }
                return View(post);
            }
            catch
            {
                return View();
            }
        }

        // Displays info without reloading page
        public ActionResult DisplayBody(string postId)
        {
            var post = db.Posts.Find(Guid.Parse(postId));
            return PartialView("_DisplayPost", post);
        }
    }
}