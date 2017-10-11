using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sheego.Framework.Presentation.Web.Models
{
    public class Deployment
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "Release")]
        public string ReleaseVersion { set; get; }

        public SelectList ReleaseVersions { set; get; }

        public string Description { set; get; }

        [Display(Name = "Deployment on")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { set; get; }

        public DeploymentStatus Status { set; get; }

        public string Environment { set; get; }
        
        public SelectList Environments { set; get; }

        public List<VerificationMessage> VerificationMessages { set; get; }

        [Display(Name = "Participants")]
        public List<Stakeholder> StakeholderList { set; get; }

        public Deployment()
        {
            VerificationMessages = new List<VerificationMessage>();
            StakeholderList = new List<Stakeholder>();
        }
    }
}