namespace sheego.Framework.Domain.Shared
{
    public interface IStakeholder
    {
        string Name { get; set; }
        bool IsParticipating { get; set; }
    }
}
