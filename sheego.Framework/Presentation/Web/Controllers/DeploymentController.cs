using sheego.Framework.Domain.Shared.Locator;
using BO = sheego.Framework.Domain.Shared;
using WEB = sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Controllers
{
    public class DeploymentController : Controller
    {
        // GET: Deployment
        //[FrameworkAuthorization]
        public ActionResult Index()
        {
            RunInitializeCheck();
            var indexDeploymentList = new List<Deployment>();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var deployments = service.Object.ReadDeployments();
                indexDeploymentList.AddRange(from deploymentBO in deployments let converter = new Converter()
                                             select converter.Convert(deploymentBO));
            }
            return View(indexDeploymentList);
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

        // GET: Deployments/Create
        //[FrameworkAuthorization]
        public ActionResult Create(string name)
        {
            var deployment = new Deployment { Name = name };
            using (var service = DomainLocator.GetRepositoryService())
            {
                deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                var configData = service.Object.ReadConfiguration("MainConfiguration");
                var converter = new Converter();
                deployment.Environments = new SelectList(converter.Convert(configData).DeployEnvironments);
            }
            return View(deployment);
        }

        // POST: Deployments/Create
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Deployment deployment, string action)
        {
            switch (action)
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        using (var service = DomainLocator.GetRepositoryService())
                        {
                            var converter = new Converter();
                            service.Object.CreateDeployment(converter.Convert(deployment));
                        }
                        return RedirectToAction("Index");
                    }
                    break;

                //case "verify":
                //    using (var service = DomainLocator.GetVerificationService())
                //    {
                //        var converter = new Converter();
                //        using (var repoService = DomainLocator.GetRepositoryService())
                //        {
                //            deployment.ReleaseVersions = new SelectList(repoService.Object.ReadReleaseVersions());
                //            var configData = repoService.Object.ReadConfiguration("MainConfiguration");
                //            deployment.Environments = new SelectList(converter.Convert(configData).DeployEnvironments);

                //            var verifiedMessages = service.Object.Verify(converter.Convert(deployment), "okwarn");
                //            deployment.Status = DeploymentStatus.Verified;
                //            foreach (var message in verifiedMessages)
                //            {
                //                deployment.VerificationMessages.Add(converter.Convert(message));
                //                if (message.Status != BO.VerificationStatus.OK)
                //                {
                //                    deployment.Status = DeploymentStatus.Init;
                //                }
                //            }
                //        }
                //    }
                //    return View(deployment);
            }

            using (var repoService = DomainLocator.GetRepositoryService())
            {
                deployment.ReleaseVersions = new SelectList(repoService.Object.ReadReleaseVersions());
                var configData = repoService.Object.ReadConfiguration("MainConfiguration");
                var converter = new Converter();
                deployment.Environments = new SelectList(converter.Convert(configData).DeployEnvironments);
                deployment.Status = DeploymentStatus.Init;
                return View(deployment);
            }
        }

        // GET: Deployments/Edit
        //[FrameworkAuthorization]
        public ActionResult Edit(string name)
        {
            if (name == null)
            {
                ViewBag.Message = "Invalid name. Nothing to display.";
                return View();
            }

            var deployment = new Deployment();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var deployments = service.Object.ReadDeployments();
                foreach (var deploymentBO in deployments)
                {
                    if (deploymentBO.Name != name) continue;
                    var converter = new Converter();
                    deployment = converter.Convert(deploymentBO);
                    deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                    var configData = service.Object.ReadConfiguration("MainConfiguration");
                    deployment.Environments = new SelectList(converter.Convert(configData).DeployEnvironments);
                    break;
                }
            }
            return View(deployment);
        }

        // POST: Deployments/Edit
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Deployment deployment, string action)
        {
            switch (action)
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        using (var service = DomainLocator.GetRepositoryService())
                        {
                            var converter = new Converter();
                            service.Object.CreateDeployment(converter.Convert(deployment));
                        }
                        return RedirectToAction("Index");
                    }
                    break;

                case "verify":
                    using (var service = DomainLocator.GetVerificationService())
                    {
                        var converter = new Converter();
                        using (var repoService = DomainLocator.GetRepositoryService())
                        {
                            deployment.ReleaseVersions = new SelectList(repoService.Object.ReadReleaseVersions());
                            var configData = repoService.Object.ReadConfiguration("MainConfiguration");
                            deployment.Environments = new SelectList(converter.Convert(configData).DeployEnvironments);

                            var verifiedMessages = service.Object.Verify(converter.Convert(deployment), "allok");
                            deployment.Status = DeploymentStatus.Verified;
                            foreach (var message in verifiedMessages)
                            {
                                deployment.VerificationMessages.Add(converter.Convert(message));
                                if (message.Status != BO.VerificationStatus.Ok)
                                {
                                    deployment.Status = DeploymentStatus.Init;
                                }
                            }
                        }
                    }
                    return View(deployment);
            }

            using (var service = DomainLocator.GetRepositoryService())
            {
                deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                var configData = service.Object.ReadConfiguration("MainConfiguration");
                var converter = new Converter();
                deployment.Environments = new SelectList(converter.Convert(configData).DeployEnvironments);
                deployment.Status = DeploymentStatus.Init;
                return View(deployment);
            }
        }

        // GET: Deployments/Execute
        //[FrameworkAuthorization]
        public ActionResult Execute(string name)
        {
            if (name == null)
            {
                ViewBag.Message = "Invalid name. Nothing to display.";
                return View();
            }

            var runDeployment = new RunDeployment();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var deploymentBO = service.Object.ReadDeployment(name);
                var converter = new Converter();
                runDeployment.Deployment = converter.Convert(deploymentBO);

                if (runDeployment.Deployment.Status == DeploymentStatus.Active)
                {
                    var deploymentSteps = service.Object.ReadDeploymentSteps(name);
                    var steps = deploymentSteps as BO.IDeploymentStep[] ?? deploymentSteps.ToArray();
                    if (steps.Any())
                    {
                        if (steps.Last().StepState != Domain.Shared.DeploymentStepState.Init
                            && steps.Last().StepState != Domain.Shared.DeploymentStepState.Active)
                        {
                            runDeployment.Deployment.Status = DeploymentStatus.Failed;
                            foreach (var deploymentStep in steps)
                            {
                                if (deploymentStep.StepState != Domain.Shared.DeploymentStepState.Failed)
                                    runDeployment.Deployment.Status = DeploymentStatus.Successful;
                            }
                            service.Object.CreateDeployment(converter.Convert(runDeployment.Deployment));
                            return RedirectToAction("Index");
                        }

                        foreach (var deploymentStep in steps)
                        {
                            runDeployment.DeploymentSteps.Add(converter.Convert(deploymentStep));
                        }
                    }
                    else
                    {
                        runDeployment.DeploymentSteps.Add(
                            new DeploymentStep()
                            {
                                Id = 1,
                                Description = "No steps available",
                                StepState = DeploymentStepState.Active
                            }
                        );
                    }
                }
            }
            return View(runDeployment);
        }

        // GET: Deployments/RunExecute
        //[FrameworkAuthorization]
        public ActionResult RunExecute(string name, string stepAction, int stepId)
        {
            if (name == null)
            {
                ViewBag.Message = "Invalid name. Nothing to display.";
                //return View(); Redirect, because RunExecute View does not exist
                return RedirectToAction("Execute");
            }

            //ToDo: Check if deployment is verified but allow to execute after additional confirmation
            
            using (var service = DomainLocator.GetRepositoryService())
            {
                //Get deployment and its steps.
                var converter = new Converter();
                var deploymentBO = service.Object.ReadDeployment(name);
                var runDeployment = new RunDeployment { Deployment = converter.Convert(deploymentBO) };
                var deploymentSteps = service.Object.ReadDeploymentSteps(name);
                
                switch (stepAction)
                {
                    case "startdeployment":
                        //Set steps' status to init and ...
                        foreach (var deploymentStep in deploymentSteps)
                        {
                            deploymentStep.StepState = BO.DeploymentStepState.Init;
                            service.Object.CreateDeploymentStep(name, deploymentStep);
                            runDeployment.DeploymentSteps.Add(converter.Convert(deploymentStep));

                            //... the first step's to active only if the list is not empty
                            runDeployment.DeploymentSteps[0].StepState = DeploymentStepState.Active;
                        }

                        //Set deployment's status to active when deployment starts...
                        deploymentBO.Status = BO.DeploymentStatus.Active;
                        service.Object.CreateDeployment(deploymentBO);
                        runDeployment.Deployment.Status = DeploymentStatus.Active;
                        break;

                        //... and change the status of a steps with stepId and matching stepAction
                    case "complete":
                        deploymentSteps.Single(s => s.Id == stepId).StepState = BO.DeploymentStepState.Successful;
                        service.Object.CreateDeploymentStep(name, deploymentSteps.Single(s => s.Id == stepId));
                        break;

                    case "skip":
                        deploymentSteps.Single(s => s.Id == stepId).StepState = BO.DeploymentStepState.Skipped;
                        service.Object.CreateDeploymentStep(name, deploymentSteps.Single(s => s.Id == stepId));
                        break;

                    case "failed":
                        deploymentSteps.Single(s => s.Id == stepId).StepState = BO.DeploymentStepState.Failed;
                        service.Object.CreateDeploymentStep(name, deploymentSteps.Single(s => s.Id == stepId));
                        break;
                }
            }
            return RedirectToAction("Execute", new { name });
        }

        // GET: Deployments/Delete
        //[FrameworkAuthorization]
        public ActionResult Delete(string name)
        {
            ViewBag.Message = "You are about to delete:";
            if (name == null)
            {
                ViewBag.Message = "Invalid name. Nothing to display.";
                return View();
            }

            Deployment deployment = null;
            using (var service = DomainLocator.GetRepositoryService())
            {
                var deployments = service.Object.ReadDeployments();
                foreach (var deploymentBO in deployments)
                {
                    if (deploymentBO.Name != name) continue;
                    var converter = new Converter();
                    deployment = converter.Convert(deploymentBO);
                }
            }

            if (deployment == null)
            {
                return HttpNotFound();
            }
            return View(deployment);
        }

        // POST: Deployments/Delete
        [HttpPost]
        //[FrameworkAuthorization]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(Deployment deployment, string action)
        {
            if (action != "delete") return View();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var converter = new Converter();
                service.Object.DeleteDeployment(converter.Convert(deployment));
                return RedirectToAction("Index");
            }
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}