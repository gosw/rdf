using sheego.Framework.Domain.Shared.Locator;
using BO = sheego.Framework.Domain.Shared;
using WEB = sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Util;
using System.Collections.Generic;
using System.Web.Mvc;
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
            var indexReleaseList = new List<Release>();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var releases = service.Object.ReadReleases();
                indexReleaseList.AddRange(from releaseBO in releases let converter = new Converter()
                    select converter.Convert(releaseBO));
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
            var releaseCombined = new ReleaseCombined { Release = new Release { Version = version },
                StakeholdersHeadline = GetStakeholdersHeadline()
            };
            return View(releaseCombined);
        }

        // POST: Releases/Create
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ReleaseCombined releaseCombined, string action)
        {
            releaseCombined.StakeholdersHeadline = GetStakeholdersHeadline();
            switch (action)
            {
                case "addreleaseunit":
                    var releaseUnit = new ReleaseUnit { Name = releaseCombined.NewReleaseUnit };
                    foreach (var stakeholderHead in releaseCombined.StakeholdersHeadline)
                    {
                        releaseUnit.StakeholderList.Add(new Stakeholder()
                        {
                            Name = stakeholderHead.Name, IsParticipating = false
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

            ReleaseCombined releaseCombined = new ReleaseCombined { StakeholdersHeadline = GetStakeholdersHeadline() };
            using (var service = DomainLocator.GetRepositoryService())
            {
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version != version) continue;
                    var converter = new Converter();
                    releaseCombined.Release = converter.Convert(releaseBO);
                    foreach (var releaseUnit in releaseCombined.Release.UnitList)
                    {
                        foreach (var headline in releaseCombined.StakeholdersHeadline)
                        {
                            if (releaseUnit.StakeholderList.Count(x => x.Name == headline.Name) > 0) continue;
                            releaseUnit.StakeholderList.Add(headline); //ToDo: delete stakeholder in a release unit if not present in config
                        }
                    }
                }
            }
            return View(releaseCombined);
        }

        // POST: Releases/Edit
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ReleaseCombined releaseCombined, string action)
        {
            releaseCombined.StakeholdersHeadline = GetStakeholdersHeadline();
            switch (action)
            {
                case "addreleaseunit":
                    var releaseUnit = new ReleaseUnit { Name = releaseCombined.NewReleaseUnit };
                    foreach (var stakeholderHead in releaseCombined.StakeholdersHeadline)
                    {
                        releaseUnit.StakeholderList.Add(new Stakeholder()
                        {
                            Name = stakeholderHead.Name, IsParticipating = false
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
            }
            return View(releaseCombined);
        }

        // GET: Releases/Prepare
        //[FrameworkAuthorization]
        public ActionResult Prepare(string version)
        {
            if (version == null)
            {
                ViewBag.Message = "Invalid version number. Nothing to display.";
                return View();
            }

            ReleasePrepared releasePrepared = new ReleasePrepared { StakeholdersHeadline = GetStakeholdersHeadline() };
            using (var service = DomainLocator.GetRepositoryService())
            {
                //ToDo: Change to ReadReleaseVersions() and then Read() one Release
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version != version) continue;
                    var converter = new Converter();
                    releasePrepared.Release = converter.Convert(releaseBO);
                    foreach (var releaseUnit in releasePrepared.Release.UnitList)
                    {
                        foreach (var headline in releasePrepared.StakeholdersHeadline)
                        {
                            if (releaseUnit.StakeholderList.Count(x => x.Name == headline.Name) > 0) continue;
                            releaseUnit.StakeholderList.Add(headline);
                        }
                    }
                }
            }

            //Create SelectList
            var participatingStakeholdersUnits = (from releaseUnit in releasePrepared.Release.UnitList
                                                  from stakeholder in releaseUnit.StakeholderList
                                                  where stakeholder.IsParticipating
                                                  select string.Format("{0}~{1}~", releaseUnit.Name, stakeholder.Name)).ToList();

            releasePrepared.ReleaseElementOptions = new SelectList(participatingStakeholdersUnits, "Select release unit and team");

            return View(releasePrepared);
        }

        // POST: Releases/Prepare
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Prepare(ReleasePrepared releasePrepared, string action)
        {
            releasePrepared.StakeholdersHeadline = GetStakeholdersHeadline();
            switch (action)
            {
                case "addreleaseelement":
                    //ToDo: Add list of Release Elements in Domain.Release and add Release Elements here
                    var releaseElement = new ReleaseElement {
                        SelectListPrefix = releasePrepared.ReleaseElementOption, Content = releasePrepared.NewReleaseElement };
                    //ToDo: catch NullReferenceExeption
                    releasePrepared.Release.UnitList.FirstOrDefault(
                        x => x.Name == releasePrepared.ReleaseElementOption.Split('~')[0]).
                        ReleaseElementsList.Add(releaseElement);

                    //Create SelectList anew
                    var participatingStakeholdersUnits = (from releaseUnit in releasePrepared.Release.UnitList
                                                          from stakeholder in releaseUnit.StakeholderList
                                                          where stakeholder.IsParticipating
                                                          select string.Format("{0}~{1}~", releaseUnit.Name, stakeholder.Name)).ToList();

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

            ReleaseCombined releaseCombined = new ReleaseCombined { StakeholdersHeadline = GetStakeholdersHeadline() };
            using (var service = DomainLocator.GetRepositoryService())
            {
                var releases = service.Object.ReadReleases();
                foreach (var releaseBO in releases)
                {
                    if (releaseBO.Version != version) continue;
                    var converter = new Converter();
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

        private List<Stakeholder> GetStakeholdersHeadline()
        {
            using (var service = DomainLocator.GetRepositoryService())
            {
                var stakeholderList = new List<Stakeholder>();
                var configData = service.Object.ReadConfiguration("MainConfiguration"); //Needed only to display all available stakeholders
                var converter = new Converter();
                return stakeholderList = converter.Convert(configData).Stakeholders;
            }
        }
    }
}