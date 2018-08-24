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
using System.Linq;

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
                    releaseUnit.Name = releaseCombined.NewReleaseUnit;
                    for (var i = 0; i < (releaseCombined.StakeholdersHeadline != null ? releaseCombined.StakeholdersHeadline.Count : 0); i++)
                    {
                        releaseUnit.StakeholderList.Add(new Stakeholder()
                        {
                            Name = releaseCombined.StakeholdersHeadline[i].Name,
                            IsParticipating = false
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
                        foreach (var releaseUnit in releaseCombined.Release.UnitList)
                        {
                            foreach (var headline in releaseCombined.StakeholdersHeadline)
                            {
                                if (releaseUnit.StakeholderList.Count(x => x.Name == headline.Name) > 0)
                                    continue;
                                releaseUnit.StakeholderList.Add(headline); //ToDo: delete stakeholder in a release unit if not present in config
                            }
                        }
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
                    releaseUnit.Name = releaseCombined.NewReleaseUnit;
                    using (var service = DomainLocator.GetRepositoryService())
                    {
                        var configData = service.Object.ReadConfiguration("MainConfiguration"); //Needed only to display all available stakeholders
                        var converter = new Converter();
                        releaseCombined.StakeholdersHeadline = converter.Convert(configData).Stakeholders;

                        for (var i = 0; i < releaseCombined.StakeholdersHeadline.Count; i++)
                        {
                            releaseUnit.StakeholderList.Add(new Stakeholder()
                            {
                                Name = releaseCombined.StakeholdersHeadline[i].Name,
                                IsParticipating = false
                            });
                        }
                        releaseCombined.Release.UnitList.Add(releaseUnit);
                    }
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
                        foreach (var releaseUnit in releaseCombined.Release.UnitList)
                        {
                            foreach (var headline in releaseCombined.StakeholdersHeadline)
                            {
                                if (releaseUnit.StakeholderList.Count(x => x.Name == headline.Name) > 0)
                                    continue;
                                releaseUnit.StakeholderList.Add(headline);
                            }
                        }
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
                releasePrepared = new ReleasePrepared();
                var configData = service.Object.ReadConfiguration("MainConfiguration"); //Needed only to display all available stakeholders
                var converter = new Converter();
                releasePrepared.StakeholdersHeadline = converter.Convert(configData).Stakeholders;

                //ToDo: Change to ReadReleaseVersions() and then Read() one Release
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version == version)
                    {
                        releasePrepared.Release = converter.Convert(releaseBO);
                        foreach (var releaseUnit in releasePrepared.Release.UnitList)
                        {
                            foreach (var headline in releasePrepared.StakeholdersHeadline)
                            {
                                if (releaseUnit.StakeholderList.Count(x => x.Name == headline.Name) > 0)
                                    continue;
                                releaseUnit.StakeholderList.Add(headline);
                            }
                        }
                    }
                }
            }

            List<string> participatingStakeholdersUnits = new List<string>();
            foreach (var releaseUnit in releasePrepared.Release.UnitList)
            {
                foreach (var stakeholder in releaseUnit.StakeholderList)
                {
                    if (stakeholder.IsParticipating == false)
                        continue;
                    participatingStakeholdersUnits.Add(string.Format("{0}~{1}~", releaseUnit.Name, stakeholder.Name));
                }
            }
            releasePrepared.ReleaseElementOptions = new SelectList(participatingStakeholdersUnits, "Select release unit and team");

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
        public ActionResult Prepare(ReleasePrepared releasePrepared, string action)
        {
            switch (action)
            {
                case "addreleaseelement":
                    //ToDo: Add list of Release Elements in Domain.Release and add Release Elements here
                    var releaseElement = new ReleaseElement();
                    releaseElement.SelectListPrefix = releasePrepared.ReleaseElementOption;
                    releaseElement.Content = releasePrepared.NewReleaseElement;
                    //ToDo: catch NurrReferenceExeption
                    releasePrepared.Release.UnitList.FirstOrDefault(
                        x => x.Name == releasePrepared.ReleaseElementOption.Split('~')[0]).ReleaseElementsList.Add(releaseElement);

                    //Create SelectList anew
                    List<string> participatingStakeholdersUnits = new List<string>();
                    foreach (var releaseUnit in releasePrepared.Release.UnitList)
                    {
                        foreach (var stakeholder in releaseUnit.StakeholderList)
                        {
                            if (stakeholder.IsParticipating == false)
                                continue;
                            participatingStakeholdersUnits.Add(string.Format("{0}~{1}~", releaseUnit.Name, stakeholder.Name));
                        }
                    }
                    releasePrepared.ReleaseElementOptions = new SelectList(participatingStakeholdersUnits, "Select release unit and team");
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