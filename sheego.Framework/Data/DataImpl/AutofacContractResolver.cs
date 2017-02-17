using System;
using Newtonsoft.Json.Serialization;
using sheego.Framework.Data.Shared.Locator;

namespace sheego.Framework.Data.Impl
{
    internal class AutofacContractResolver : DefaultContractResolver
    {
        public AutofacContractResolver()
        {
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract contract = base.CreateObjectContract(objectType);

            // use Autofac to create types that have been registered with it
            contract.DefaultCreator = () => DataLocator.GetObject(objectType);
            return contract;
        }
    }
}