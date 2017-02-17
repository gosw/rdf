using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace sheego.Framework.Presentation.Web.Models
{
    public class Release
    {
        [Required]
        public string Version { get;  set; }

        public string Description { set; get; }

        [Display(Name = "Release due on")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { set; get; }

        public List<ReleaseUnit> UnitList { get; set; }

        //public Stakeholder S { set; get; } //ToDo: to complete later

        public Release()
        {
            UnitList = new List<ReleaseUnit>();
        }
    }
}