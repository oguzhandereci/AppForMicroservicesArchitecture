using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Utilities.AddedClassesForStatusCodeResult
{
    [DefaultStatusCode(424)]
    public class FailResult : StatusCodeResult
    {
        public FailResult() : base(424) { }
    }
}
