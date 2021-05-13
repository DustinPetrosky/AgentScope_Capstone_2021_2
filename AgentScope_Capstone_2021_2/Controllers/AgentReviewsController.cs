using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgentScope_Capstone_2021_2.Models;
using Microsoft.AspNet.Identity;

namespace AgentScope_Capstone_2021_2.Controllers
{
    public class AgentReviewsController : Controller
    {
        private CapstoneProjectEntities db = new CapstoneProjectEntities();

        public ActionResult ReviewCount() {

            int reviews = db.AgentReviews.Count();
            return PartialView(reviews);
        }
        // GET: AgentReviews
        public ActionResult Index(int? rev_page, int? rev_pageSize, string id)
        {
            //var agentReviews = db.AgentReviews.Where(r => r.AgentId == id);
            var loggedUserId = User.Identity.GetUserId();
            string profileId;
            if (!string.IsNullOrWhiteSpace(id)) {
                profileId = id;
            } else if (User.IsInRole("Agent")) {
                profileId = loggedUserId;
            } else {
                return new HttpNotFoundResult();
            }
            var realPage = rev_page ?? 1;
            var realPageSize = rev_pageSize ?? 1000;
            var agentAcc = db.AgentAccounts.Find(profileId);
            var numOfPages = (int)Math.Ceiling(agentAcc.AgentReviews.Count() / (double)realPageSize);

            if (realPage > numOfPages) {
                realPage = numOfPages;
            }

            var agentReviews = agentAcc.AgentReviews
                .OrderByDescending(h => h.DateOfReview)
                .Skip(realPageSize * (realPage - 1))
                .Take(realPageSize);
            ViewBag.numOfPages = numOfPages;
            ViewBag.pageNumber = realPage;
            ViewBag.agentId = profileId;
            return PartialView(agentReviews.ToList());
        }

        // GET: AgentReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentReview agentReview = db.AgentReviews.Find(id);
            if (agentReview == null)
            {
                return HttpNotFound();
            }
            return View(agentReview);
        }

        [Authorize(Roles = "Reviewer")]
        // GET: AgentReviews/Create
        public ActionResult Create(string id)
        {
            var userId = User.Identity.GetUserId();
            var reviewModel = new AgentReview() {
                AgentId = id,
                ReviewerId = userId,
                DateOfReview = DateTime.Now,                
            };
            return View(reviewModel);
        }

        // POST: AgentReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgentId,ReviewerId,Rating,BodyText,DateOfReview")] AgentReview agentReview)
        {
            if (ModelState.IsValid)
            {
                db.AgentReviews.Add(agentReview);
                db.SaveChanges();
                return RedirectToAction("Profile", "Agent", new { id = agentReview.AgentId});
            }

            return View(agentReview);
        }

        // GET: AgentReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentReview agentReview = db.AgentReviews.Find(id);
            if (agentReview == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReviewerId = new SelectList(db.ReviewerAccounts, "Id", "FirstName", agentReview.ReviewerId);
            ViewBag.AgentId = new SelectList(db.AgentAccounts, "Id", "FirstName", agentReview.AgentId);
            return View(agentReview);
        }

        // POST: AgentReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgentId,ReviewerId,Rating,BodyText,DateOfReview")] AgentReview agentReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReviewerId = new SelectList(db.ReviewerAccounts, "Id", "FirstName", agentReview.ReviewerId);
            ViewBag.AgentId = new SelectList(db.AgentAccounts, "Id", "FirstName", agentReview.AgentId);
            return View(agentReview);
        }

        // GET: AgentReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentReview agentReview = db.AgentReviews.Find(id);
            if (agentReview == null)
            {
                return HttpNotFound();
            }
            return View(agentReview);
        }

        // POST: AgentReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgentReview agentReview = db.AgentReviews.Find(id);
            db.AgentReviews.Remove(agentReview);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
