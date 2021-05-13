using AgentScope_Capstone_2021_2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AgentScope_Capstone_2021_2.Controllers
{
    public class AgentController : Controller {
        CapstoneProjectEntities db = new CapstoneProjectEntities();
        public ActionResult Search(string id) {
            //TODO: Add more or's for different results
            var result = db.AgentAccounts.Where(a => a.FirstName.Contains(id) || a.LastName.Contains(id));
            ViewBag.keyword = id;
            return View(result);
        }

        public ActionResult AccountCount() {

            int accounts = db.AgentAccounts.Count();
            return PartialView(accounts);
        }

        //[Authorize(Roles = "Agent")]
        // GET: Agent
        public ActionResult Profile(string id, int? page)
        {
            var loggedUserId = User.Identity.GetUserId();
            string userId;
            if (!string.IsNullOrWhiteSpace(id)) {
                userId = id;
            } else if (User.IsInRole("Agent")) {
                userId = loggedUserId;
            } else {
                return new HttpNotFoundResult();                
            }
            var agentAcc = db.AgentAccounts.Find(userId);
            ViewBag.pageNumber = page ?? 1;
            return View(agentAcc);
        }

        [Authorize(Roles = "Agent")]        
        // GET: AgentAccounts/Edit
        public ActionResult Edit() {
            string id = User.Identity.GetUserId();

            AgentAccount agent = db.AgentAccounts.Find(id);
            if (agent == null) {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: AgentAccounts/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneCell,PhoneOffice,Company,StreetNumber,StreetName,SuiteNumber,City,StateProv,ZipCode,AboutMeText,YearsOfExp,WebsiteLink,FacebookLink,TwitterLink,InstagramLink,LinkedInLink,RealEstateLicense,LicensedState,ProfileImage")] AgentAccount agent, HttpPostedFileBase uploadImage) {

            var isValid = ModelState.IsValid;
            var hasFile = uploadImage != null && uploadImage.ContentLength > 0;
            if (ModelState.IsValid && hasFile) {
                if (!uploadImage.ContentType.StartsWith("image/")) {
                    isValid = false;
                    ModelState.AddModelError("ProfileImage", "Invalid file");
                }
            }
            if (isValid) {

                if(hasFile) {
                    string imageFileName = null;
                    var folderPath = Server.MapPath("~/UserContent");
                    var extentionIndex = uploadImage.FileName.LastIndexOf(".");
                    var imageExtention = uploadImage.FileName.Substring(extentionIndex);
                    imageFileName = agent.Id + imageExtention;
                    var imageFilePath = Path.Combine(folderPath, imageFileName);
                    uploadImage.SaveAs(imageFilePath);

                    agent.ProfileImage = imageFileName;
                } else {                
                    var agentDb = db.AgentAccounts.Find(agent.Id);
                    agent.ProfileImage = agentDb.ProfileImage;
                }

                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(agent);
        }

        protected override void Dispose(bool disposing) {

            if (disposing) {
                if (db != null) {
                    db.Dispose();
                    db = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}