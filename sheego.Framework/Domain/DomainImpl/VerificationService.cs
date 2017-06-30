using System.Collections.Generic;
using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class VerificationService : IVerificationService
    {
        public IList<IVerificationMessage> Verify(IDeployment deployment)
        {
            var testList = new List<IVerificationMessage>();
            testList.Add(new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.OK });
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
                        new VerificationMessage() { MessageContent = "Step list validation", Status = VerificationStatus.OK },
                        new VerificationMessage() { MessageContent = "MasterBuild validation", Status = VerificationStatus.OK },
                        new VerificationMessage() { MessageContent = "WorkItem validation", Status = VerificationStatus.OK }
                    });
                    break;

                case "okwarn":
                    testList.AddRange(new List<VerificationMessage>
                    {
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.OK },
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.Warning }
                    });
                    break;

                case "okfail":
                    testList.AddRange(new List<VerificationMessage>
                    {
                        new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.OK },
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
                    testList.Add(new VerificationMessage() { MessageContent = "Testing Message", Status = VerificationStatus.OK });
                    break;
            }
            return testList;
        }
    }
}
