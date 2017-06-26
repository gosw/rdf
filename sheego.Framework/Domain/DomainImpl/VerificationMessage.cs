using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class VerificationMessage : IVerificationMessage
    {
        public string MessageContent { get; set; }

        public VerificationStatus Status { get; set; }
    }
}
