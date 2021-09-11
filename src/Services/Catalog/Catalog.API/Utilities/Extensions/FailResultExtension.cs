using Catalog.API.Utilities.AddedClassesForStatusCodeResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Utilities.Extensions
{
    public static class FailResultExtension
    {
        public static FailResult Fail(this ControllerBase result)
        {
            return new FailResult();
        }
    }
}
