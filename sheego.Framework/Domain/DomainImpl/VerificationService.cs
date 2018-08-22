using System.Collections.Generic;
using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class VerificationService : IVerificationService
    {
        public IList<IVerificationMessage> Verify(IDeployment deployment)
        {
            var testList = new List<IVerificationMessage>
            {
                new VerificationMessage() {MessageContent = "Testing Message", Status = VerificationStatus.Ok}
            };
            return testList;
        }
        
        public IList<IVerificationMessage> Verify(IDeployment deployment, string option)
        {//ToDo: Method for testing -> to be deleted later
            var testList = new List<IVerificationMessage>();
            switch (option)
            {
                case "allok":
                    testList.AddRange(new List<VerificationMessage>
                    {
                        new VerificationMessage() { MessageContent = "Step list validation", Status = VerificationStatus.Ok },
                        new VerificationMessage() { MessageContent = "MasterBuild validation", Status = VerificationStatus.Ok },
                        new VerificationMessage() { MessageContent = "WorkItem validation", Status = VerificationStatus.Ok }
                    });
                    break;

                case "okwarn":
                    testList.AddRange(new List<VerificationMessage>
                    {
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Ok },
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Warning }
                    });
                    break;

                case "okfail":
                    testList.AddRange(new List<VerificationMessage>
                    {
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Ok },
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Failed }
                    });
                    break;

                case "warnfail":
                    testList.AddRange(new List<VerificationMessage>
                    {
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Failed },
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Warning }
                    });
                    break;

                default:
                    testList.Add(new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Ok });
                    break;
            }
            return testList;
        }
    }
}
