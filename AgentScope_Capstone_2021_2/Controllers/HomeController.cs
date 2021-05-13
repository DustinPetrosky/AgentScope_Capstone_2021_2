using AgentScope_Capstone_2021_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgentScope_Capstone_2021_2.Controllers {
    public class HomeController : Controller {

        private CapstoneProjectEntities db = new CapstoneProjectEntities();
        public ActionResult Index() {
            var agentAcc = db.AgentAccounts.OrderByDescending(a => a.DateCreated).Take(7);
            return View(agentAcc.ToList());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }

}