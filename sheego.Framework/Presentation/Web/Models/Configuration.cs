﻿using System.Collections.Generic;

namespace sheego.Framework.Presentation.Web.Models
{
    public class Configuration
    {
        public List<Stakeholder> Stakeholders { set; get; }

        public List<string> DeployEnvironments { set; get; }

        public List<string> InitFolders { get; set; }

        public Configuration()
        {
            Stakeholders = new List<Stakeholder>();
            DeployEnvironments = new List<string>(); //ToDo: Convert string to Object DeployEnvironment in Web
            InitFolders = new List<string>();
        }
    }
}