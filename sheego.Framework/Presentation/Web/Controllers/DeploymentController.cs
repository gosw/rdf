﻿using sheego.Framework.Data.Shared.Locator;
using sheego.Framework.Domain.Shared.Locator;
using sheego.Framework.Presentation.Web.Models;
using sheego.Framework.Presentation.Web.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Controllers
{
    public class DeploymentController : Controller
    {
        // GET: Deployment
        //[FrameworkAuthorization]
        public ActionResult Index()
        {
            List<Deployment> indexDeploymentList = new List<Deployment>();
            using (var service = DomainLocator.GetRepositoryService())
            {
                var deployments = service.Object.ReadDeployments();
                foreach (var deploymentBO in deployments)
                {
                    var converter = new Converter();
                    indexDeploymentList.Add(converter.Convert(deploymentBO));
                }
            }
            return View(indexDeploymentList);
        }

        // GET: Deployments/Create
        //[FrameworkAuthorization]
        public ActionResult Create(string name)
        {
            var deployment = new Deployment();
            deployment.Name = name;

            //ToDo: Liste der environments von config holen
            using (var service = DomainLocator.GetRepositoryService())
            {
                deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                deployment.Environments = new SelectList(new[] { "Test", "Preprod" });
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

                case "confirmdeployment":
                    //ToDo: Add field Status in Deployment and set Status here
                    break;
            }
            using (var service = DomainLocator.GetRepositoryService())
            {
                deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                deployment.Environments = new SelectList(new[] { "Test", "Preprod" });
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
                    if (deploymentBO.Name == name)
                    {
                        var converter = new Converter();
                        deployment = converter.Convert(deploymentBO);
                        deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                        deployment.Environments = new SelectList(new[] { "Test", "Preprod" });
                        break;
                    }
                }
            }

            if (deployment == null)
            {
                return HttpNotFound();
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
                            return RedirectToAction("Index");
                        }
                    }
                    break;

                case "confirmrelease":
                    // Verify if Deployment correct
                    /*
                    using (var service = DomainLocator.GetVerificationService())
                    {
                        var converter = new Converter();
                        IList<VerificationMessage> verificationMessageList = service.Object.Verify(converter.Convert(deployment));
                    }
                    */
                    //ToDo: Add field Status in Deployment and set Status here
                    break;
            }
            using (var service = DomainLocator.GetRepositoryService())
            {
                deployment.ReleaseVersions = new SelectList(service.Object.ReadReleaseVersions());
                deployment.Environments = new SelectList(new[] { "Test", "Preprod" });
                return View(deployment);
            }
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
                    if (deploymentBO.Name == name)
                    {
                        var converter = new Converter();
                        deployment = new Deployment();
                        deployment = converter.Convert(deploymentBO);
                    }
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
            if (action == "delete")
            {
                using (var service = DomainLocator.GetRepositoryService())
                {
                    var converter = new Converter();
                    service.Object.DeleteDeployment(converter.Convert(deployment));
                    return RedirectToAction("Index");
                }
            }
            return View();
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
                //runDeployment.DeploymentSteps = converter.Convert(deploymentBO.Steps);

                //Mock
                runDeployment.DeploymentSteps = new List<DeploymentStep>()
                {
                    new DeploymentStep() {
                        Id = "1",
                        Description = "Passivate the environment",
                        StepState = DeploymentStepState.Successful
                    },
                    new DeploymentStep() {
                        Id = "2",
                        Description = "Install system A",
                        StepState = DeploymentStepState.Active
                    },
                    new DeploymentStep() {
                        Id = "3",
                        Description = "Install system B",
                        StepState = DeploymentStepState.Active
                    },
                    new DeploymentStep() {
                        Id = "4",
                        Description = "Passivate the environment",
                        StepState = DeploymentStepState.Init
                    }
                };
            }

            if (runDeployment == null)
            {
                return HttpNotFound();
            }
            return View(runDeployment);
        }

        // GET: Deployments/Execute
        //[FrameworkAuthorization]
        public ActionResult RunExecute(string name, string stepAction, string stepId)
        {
            if (name == null)
            {
                ViewBag.Message = "Invalid name. Nothing to display.";
                return View();
            }

            //For Deployment Name change the status of Step with stepId and matching stepAction
            using (var service = DomainLocator.GetRepositoryService())
            {
                var deploymentBO = service.Object.ReadDeployment(name);


                switch (stepAction)
                {
                    case "complete":
                        //deploymentBO.Steps.Where(s => s.id == stepId).Single().State = Successfull;
                        break;

                    case "skip":

                        break;

                    case "failed":

                        break;
                }
                service.Object.CreateDeployment(deploymentBO);
            }
                return RedirectToAction("Execute", new { name = name });
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}