using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgentScope_Capstone_2021_2.Models;
using Microsoft.AspNet.Identity;

namespace AgentScope_Capstone_2021_2.Controllers
{
    public class AgentHomeSoldsController : Controller
    {
        private CapstoneProjectEntities db = new CapstoneProjectEntities();

        // GET: AgentHomeSolds
        public ActionResult Index(int? home_page, int? home_pageSize, string id)
        {

            var loggedUserId = User.Identity.GetUserId();
            string profileId;
            if (!string.IsNullOrWhiteSpace(id)) {
                profileId = id;
            } else if (User.IsInRole("Agent")) {
                profileId = loggedUserId;
            } else {
                return new HttpNotFoundResult();
            }
            var agentAcc = db.AgentAccounts.Find(profileId);
            var realPage = home_page ?? 1;
            var realPageSize = home_pageSize ?? 1000;
            var numOfPages = (int)Math.Ceiling(agentAcc.AgentHomeSolds.Count() / (double)realPageSize);

            if (realPage > numOfPages) {
                realPage = numOfPages;
            }

            var agentHomeSolds = agentAcc.AgentHomeSolds
                .OrderByDescending(h => h.DateSold)
                .Skip(realPageSize * (realPage - 1))
                .Take(realPageSize);
            ViewBag.numOfPages = numOfPages;
            ViewBag.pageNumber = realPage;
            ViewBag.agentId = profileId;
            return PartialView(agentHomeSolds.ToList());
        }

        // GET: AgentHomeSolds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentHomeSold agentHomeSold = db.AgentHomeSolds.Find(id);
            if (agentHomeSold == null)
            {
                return HttpNotFound();
            }
            return View(agentHomeSold);
        }

        // GET: AgentHomeSolds/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var homeSoldModel = new AgentHomeSold() {
                AgentId = userId               
            };
            return View(homeSoldModel);
        }

        // POST: AgentHomeSolds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AgentId,StreetNumber,StreetName,City,StateProv,ZipCode,Price,DateSold,ImageFile")] AgentHomeSold agentHomeSold, HttpPostedFileBase uploadImage)
        {
            var isValid = ModelState.IsValid;
            var hasFile = uploadImage != null && uploadImage.ContentLength > 0;
            if (ModelState.IsValid && hasFile) {
                if (!uploadImage.ContentType.StartsWith("image/")) {
                    isValid = false;
                    ModelState.AddModelError("ProfileImage", "Invalid file");
                }
            }
            if (isValid)
            {
                string imageFileName = null;
                if (hasFile) {
                    var folderPath = Server.MapPath("~/UserContent");
                    var extentionIndex = uploadImage.FileName.LastIndexOf(".");
                    var imageExtention = uploadImage.FileName.Substring(extentionIndex);
                    imageFileName = agentHomeSold.AgentId + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + imageExtention;
                    var imageFilePath = Path.Combine(folderPath, imageFileName);
                    uploadImage.SaveAs(imageFilePath);
                }

                agentHomeSold.ImageFile = imageFileName;
                
                db.AgentHomeSolds.Add(agentHomeSold);
                db.SaveChanges();
                return RedirectToAction("Profile", "Agent", new { id = agentHomeSold.AgentId });
            }

            ViewBag.AgentId = new SelectList(db.AgentAccounts, "Id", "FirstName", agentHomeSold.AgentId);
            return View(agentHomeSold);
        }

        // GET: AgentHomeSolds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentHomeSold agentHomeSold = db.AgentHomeSolds.Find(id);
            if (agentHomeSold == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.AgentAccounts, "Id", "FirstName", agentHomeSold.AgentId);
            return View(agentHomeSold);
        }

        // POST: AgentHomeSolds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgentId,StreetNumber,StreetName,City,StateProv,ZipCode,Price,DateSold,ImageFile")] AgentHomeSold agentHomeSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentHomeSold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.AgentAccounts, "Id", "FirstName", agentHomeSold.AgentId);
            return View(agentHomeSold);
        }

        // GET: AgentHomeSolds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentHomeSold agentHomeSold = db.AgentHomeSolds.Find(id);
            if (agentHomeSold == null)
            {
                return HttpNotFound();
            }
            return View(agentHomeSold);
        }

        // POST: AgentHomeSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgentHomeSold agentHomeSold = db.AgentHomeSolds.Find(id);
            db.AgentHomeSolds.Remove(agentHomeSold);
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
