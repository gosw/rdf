using System;
using sheego.Framework.Domain.Shared;

namespace sheego.Framework.Domain.Impl
{
    class ReleaseElement : IReleaseElement
    {
        public string Content { get; set; }

        public string SelectListPrefix { get; set; }

        public ReleaseElement()
        {
            Content = "";
            SelectListPrefix = "";
        }
    }
}