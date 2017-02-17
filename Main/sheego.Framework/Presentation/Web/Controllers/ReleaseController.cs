using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Util;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Controllers
{
    public class ReleaseController : Controller
    {
        // GET: Releases
        //[FrameworkAuthorization]
        public ActionResult Index()
        {
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

        // GET: Releases/Create
        //[FrameworkAuthorization]
        public ActionResult Create(string version)
        {
            var releaseCombined = new ReleaseCombined();
            releaseCombined.Release = new Release();
            releaseCombined.Release.Version = version;
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
                    releaseCombined.Release.UnitList.Add(new ReleaseUnit() { Name = releaseCombined.newReleaseUnit });
                    //Clear 
                    releaseCombined.newReleaseUnit = ""; //ToDo: is not empty
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

                case "confirmrelease":
                    //ToDo: Add field Status in Deployment and set Status here
                    break;
            }
            //ToDo: is not empty here too releaseCombined.newReleaseUnit = ""; 
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
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version == version)
                    {
                        var converter = new Converter();
                        releaseCombined = new ReleaseCombined();
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
                    releaseCombined.Release.UnitList.Add(new ReleaseUnit() { Name = releaseCombined.newReleaseUnit });
                    //Clear 
                    releaseCombined.newReleaseUnit = ""; //ToDo: is not empty
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

                case "confirmrelease":
                    //ToDo: Add field Status in Deployment and set Status here
                    break;
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
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version == version)
                    {
                        var converter = new Converter();
                        releaseCombined = new ReleaseCombined();
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

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}