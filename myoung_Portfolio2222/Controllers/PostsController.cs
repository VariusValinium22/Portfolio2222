using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using myoung_Portfolio2222.Helpers;
using myoung_Portfolio2222.Models;
using PagedList;
using PagedList.Mvc;

namespace myoung_Portfolio2222.Controllers
{
	[RequireHttps]

	public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(int? page)
        {
			int pageSize = 3;
			int pageNumber = (page ?? 1);
			if (Request.IsAuthenticated && User.IsInRole("Admin"))
			{
				return View(db.Posts.OrderByDescending(p => p.CreationDate).ToPagedList(pageNumber, pageSize));
			}
			return View(db.Posts.OrderByDescending(p => p.CreationDate).ToPagedList(pageNumber, pageSize));
		}

		[HttpPost]
		public ActionResult Index(string searchStr, int? page)
		{
			int pageSize = 3;
			int pageNumber = (page ?? 1);
			ViewBag.Search = searchStr;
			SearchHelper search = new SearchHelper();
			var postList = search.IndexSearch(searchStr);
			if (Request.IsAuthenticated && User.IsInRole("Admin"))
			{
				return View(postList.OrderByDescending(p => p.CreationDate).ToPagedList(pageNumber, pageSize));
			}
			return View(postList.OrderByDescending(p => p.CreationDate).ToPagedList(pageNumber, pageSize));
		}

		// GET: Posts/Details/5
		public ActionResult Details(string Slug)
		{
			if (String.IsNullOrWhiteSpace(Slug))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Post post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
			if (post == null)
			{
				return HttpNotFound();
			}
			return View(post);
		}

		// GET: Posts/Create
		[Authorize(Roles = "Admin")]
		public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
		[Authorize (Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Title,Body,MediaURL")] Post post, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				var Slug = StringUtilities.URLFriendly(post.Title);
				if (String.IsNullOrWhiteSpace(Slug))
				{
					ModelState.AddModelError("Title", "Invalid title");
					return View(post);
				}
				if (db.Posts.Any(p => p.Slug == Slug))
				{
					ModelState.AddModelError("Title", "The title must be unique");
					return View(post);
				}		
			
			if (image != null && image.ContentLength > 0)
			{
				var ext = Path.GetExtension(image.FileName).ToLower();//this validates the image
				if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
					ModelState.AddModelError("image", "Invalid");
				
					var filePath = "/one-page/assets/images/";  //adding the file to the database
					var absPath = Server.MapPath("~" + filePath);
					post.MediaURL = filePath + image.FileName;   //specifies the path of the file
					image.SaveAs(Path.Combine(absPath, image.FileName)); //saves the file. necessary for the specified path to have something to point at
				}

				post.Slug = Slug;

				post.CreationDate = System.DateTime.Now;
				db.Posts.Add(post);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(post);
		}

		// GET: Posts/Edit/5
		[Authorize(Roles = "Admin, Moderator")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }



		[HttpPost]
		[Authorize(Roles = "Admin")]
		[RequireHttps]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Title,Body,UpdatedDate,MediaURL")] Post post, string MediaURL, HttpPostedFileBase image)
		{
			if (image != null && image.ContentLength > 0)
			{
				var ext = Path.GetExtension(image.FileName).ToLower();
				if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp") // curly braces not needed for 1 line if statements
				{
					ModelState.AddModelError("image", "Invalid Format.");
				}
			}
			if (ModelState.IsValid)
			{
				db.Posts.Attach(post);
				db.Entry(post).Property("Id").IsModified = true;
				db.Entry(post).Property("Title").IsModified = true;
				db.Entry(post).Property("Body").IsModified = true;
				db.Entry(post).Property("UpdatedDate").IsModified = true;			
				db.Entry(post).Property("MediaURL").IsModified = true;

				if (image != null)
				{
					var filePath = "/one-page/assets/images/";
					var absPath = Server.MapPath("~" + filePath);
					post.MediaURL = filePath + image.FileName;
					image.SaveAs(Path.Combine(absPath, image.FileName));
				}

				else
				{
					post.MediaURL = MediaURL;
				}
				post.UpdatedDate = DateTime.Now;
				db.SaveChanges();
				return RedirectToAction("Index");

			}
			return View(post);
		}
		// GET: Posts/Delete/5		
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

		// POST: Posts/Delete/5

		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		//public ActionResult CreateComment()
		//{
		//	ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName");
		//	ViewBag.PostId = new SelectList(db.Posts, "Id", "Title");
		//	return View();
		//}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateComment([Bind(Include = "Id,PostId,AuthorId,Body,CreationDate,UpdatedDate,UpdatedReason,IsDeleted")] Comment comment)
		{
			if (ModelState.IsValid)
			{
				var user = db.Users.Find(User.Identity.GetUserId());

				comment.CreationDate = DateTime.Now;
				comment.AuthorId = user.Id;
				db.Comments.Add(comment);
				db.SaveChanges();

				Post post = db.Posts.Find(comment.PostId);
				return RedirectToAction("Details", "Posts", new { Slug = post.Slug });
			}
			return View(comment);
		}
			//ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
			//         ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
