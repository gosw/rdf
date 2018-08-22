using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class Stakeholder : IStakeholder
    {
        public string Name { get; set; }

        public bool IsParticipating { get; set; }
    }
}
