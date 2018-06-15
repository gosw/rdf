using sheego.Framework.Domain.Shared.Locator;
using BO = sheego.Framework.Domain.Shared;
using WEB = sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Util;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System;

namespace sheego.Framework.Presentation.Web.Controllers
{
    public class ReleaseController : Controller
    {
        // GET: Releases
        //[FrameworkAuthorization]
        public ActionResult Index()
        {
            RunInitializeCheck();
            List<Release> indexReleaseList = new List<Release>();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    var converter = new Converter();
                    indexReleaseList.Add(converter.Convert(releaseBO));
                }
            }
            return View(indexReleaseList);
        }

        private void RunInitializeCheck()
        {
            using (var service = DomainLocator.GetRepositoryService())
            {
                var configuration = new WEB.Configuration();

                /**
                configuration.Stakeholders.AddRange(new List<Stakeholder>
                {
                    new Stakeholder() {Name ="ERP", isParticipating = false},
                    new Stakeholder() {Name ="ESB", isParticipating = false},
                    new Stakeholder() {Name ="DWH", isParticipating = false},
                    new Stakeholder() {Name ="Webshop", isParticipating = false}
                });
                configuration.DeployEnvironments.AddRange(new List<string>
                {
                    "PRODUCTION",
                    "PRE-PROD",
                    "PROD-PERFORMANCE",
                    "TEST",
                    "DEVELOPMENT"
                });
                configuration.InitFolders.AddRange(new List<string>
                {
                    "IRelease",
                    "IDeployment"
                });
                */

                var converter = new Converter();
                service.Object.CreateConfiguration("MainConfiguration", converter.Convert(configuration));
            }
        }

        // GET: Releases/Create
        //[FrameworkAuthorization]
        public ActionResult Create(string version)
        {
            var releaseCombined = new ReleaseCombined();
            releaseCombined.Release = new Release();
            releaseCombined.Release.Version = version;

            using (var service = DomainLocator.GetRepositoryService())
            {
                var configData = service.Object.ReadConfiguration("MainConfiguration"); //Needed only to display all available stakeholders
                var converter = new Converter();
                releaseCombined.StakeholdersHeadline = converter.Convert(configData).Stakeholders;
            }
            return View(releaseCombined);
        }

        // POST: Releases/Create
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ReleaseCombined releaseCombined, string action)
        {
            switch (action)
            {
                case "addreleaseunit":
                    var releaseUnit = new ReleaseUnit();
                    releaseUnit.Name = releaseCombined.newReleaseUnit;
                    for (var i = 0; i < (releaseCombined.StakeholdersHeadline != null ? releaseCombined.StakeholdersHeadline.Count : 0); i++)
                    {
                        releaseUnit.StakeholderList.Add(new Stakeholder()
                        {
                            Name = releaseCombined.StakeholdersHeadline[i].Name,
                            isParticipating = false
                        });
                    }
                    releaseCombined.Release.UnitList.Add(releaseUnit);
                    break;

                case "save":
                    if (ModelState.IsValid)
                    {
                        using (var service = DomainLocator.GetRepositoryService())
                        {
                            var converter = new Converter();
                            service.Object.CreateRelease(converter.Convert(releaseCombined.Release));
                        }
                        return RedirectToAction("Index");
                    }
                    break;

                case "deletereleaseunit":
                    //ToDo: Complete view and logic
                    break;
                
                //case "confirmrelease":
                //    //ToDo: Add field Status in Deployment and set Status here
                //    break;
            }
            return View(releaseCombined);
        }

        // GET: Releases/Edit
        //[FrameworkAuthorization]
        public ActionResult Edit(string version)
        {
            if (version == null)
            {
                ViewBag.Message = "Invalid version number. Nothing to display.";
                return View();
            }

            ReleaseCombined releaseCombined = null;
            using (var service = DomainLocator.GetRepositoryService())
            {
                releaseCombined = new ReleaseCombined();
                var configData = service.Object.ReadConfiguration("MainConfiguration"); //Needed only to display all available stakeholders
                var converter = new Converter();
                releaseCombined.StakeholdersHeadline = converter.Convert(configData).Stakeholders;

                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version == version)
                    {
                        releaseCombined.Release = converter.Convert(releaseBO);
                    }
                }
            }

            if (releaseCombined == null)
            {
                return HttpNotFound();
            }
            return View(releaseCombined);
        }

        // POST: Releases/Edit
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ReleaseCombined releaseCombined, string action)
        {
            switch (action)
            {
                case "addreleaseunit":
                    var releaseUnit = new ReleaseUnit();
                    releaseUnit.Name = releaseCombined.newReleaseUnit;
                    for (var i = 0; i < releaseCombined.StakeholdersHeadline.Count; i++)
                    {
                        releaseUnit.StakeholderList.Add(new Stakeholder()
                        {
                            Name = releaseCombined.StakeholdersHeadline[i].Name,
                            isParticipating = false
                        });
                    }
                    releaseCombined.Release.UnitList.Add(releaseUnit);
                    break;

                case "save":
                    if (ModelState.IsValid)
                    {
                        using (var service = DomainLocator.GetRepositoryService())
                        {
                            var converter = new Converter();
                            service.Object.CreateRelease(converter.Convert(releaseCombined.Release));
                            return RedirectToAction("Index");
                        }
                    }
                    break;

                case "deletereleaseunit":
                    //ToDo: Complete view and logic
                    break;

                //case "confirmrelease":
                //    //ToDo: Add field Status in Deployment and set Status here
                //    break;
            }
            return View(releaseCombined);
        }

        // GET: Releases/Delete
        //[FrameworkAuthorization]
        public ActionResult Delete(string version)
        {
            ViewBag.Message = "You are about to delete:";
            if (version == null)
            {
                ViewBag.Message = "Invalid version number. Nothing to display.";
                return View();
            }

            ReleaseCombined releaseCombined = null;
            using (var service = DomainLocator.GetRepositoryService())
            {
                releaseCombined = new ReleaseCombined();
                var configData = service.Object.ReadConfiguration("MainConfiguration"); //Needed only to display all available stakeholders
                var converter = new Converter();
                releaseCombined.StakeholdersHeadline = converter.Convert(configData).Stakeholders;

                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version == version)
                    {
                        releaseCombined.Release = converter.Convert(releaseBO);
                    }
                }
            }

            if (releaseCombined == null)
            {
                return HttpNotFound();
            }
            return View(releaseCombined);
        }

        // POST: Releases/Delete
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(ReleaseCombined releaseCombined, string action)
        {
            if (action == "delete")
            {
                using (var service = DomainLocator.GetRepositoryService())
                {
                    var converter = new Converter();
                    service.Object.DeleteRelease(converter.Convert(releaseCombined.Release));
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Releases/Prepare
        //[FrameworkAuthorization]
        public ActionResult Prepare(string version)
        {
            //Get Release by version and display release units
            if (version == null)
            {
                ViewBag.Message = "Invalid version number. Nothing to display.";
                return View();
            }

            ReleasePrepared releasePrepared = null;
            using (var service = DomainLocator.GetRepositoryService())
            {
                //ToDo: Change to ReadReleaseVersions() and then Read() one Release
                var releases = service.Object.ReadReleases(); 
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version == version)
                    {
                        var converter = new Converter();
                        releasePrepared = new ReleasePrepared();
                        releasePrepared.Release = converter.Convert(releaseBO);
                    }
                }
            }

            if (releasePrepared == null)
            {
                return HttpNotFound();
            }
            return View(releasePrepared);
        }

        // POST: Releases/Prepare
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Prepare(ReleasePrepared releasePrepared, string action, HttpPostedFileBase file)
        {
            switch (action)
            {
                case "addreleaseelement":
                    //ToDo: Add list of Release Elements in Domain.Release and add Release Elements here
                    //releasePrepared.Release.ElementList.Add(new ReleaseElement() { Element = releasePrepared.newReleaseElement });

                    //Temp: Add path of the uploaded file to display it here
                    string fileName = Path.GetFileName(releasePrepared.newReleaseElement.FileName);
                    string filePath = Path.Combine(ConfigurationManager.AppSettings["RootPath"], "Attachment", fileName);
                    releasePrepared.newReleaseElement.SaveAs(filePath); //Overwrite file if already exists

                    //Clear form
                    //releasePrepared.newReleaseElement = ""; //ToDo: is not empty
                    break;

                case "save":
                    if (ModelState.IsValid)
                    {
                        using (var service = DomainLocator.GetRepositoryService())
                        {
                            var converter = new Converter();
                            service.Object.CreateRelease(converter.Convert(releasePrepared.Release));
                            return RedirectToAction("Index");
                        }
                    }
                    break;

                case "confirmprepare":
                    //ToDo: Add field Status in ??? and set Status here
                    break;
            }
            return View(releasePrepared);
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}