namespace sheego.Framework.Domain.Shared
{
    public interface IVerificationMessage
    {
        string MessageContent { get; set; }

        VerificationStatus Status { get; set; }
    }

    public enum VerificationStatus
    {
        Ok,
        Warning,
        Failed
    }
}