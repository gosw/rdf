using System.ComponentModel.DataAnnotations;

namespace sheego.Framework.Presentation.Web.Models
{
    public class VerificationMessage
    {
        [Display(Name = "Verification message")]
        public string MessageContent { get; set; }
        
        public VerificationStatus Status { get; set; }
    }
}