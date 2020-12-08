using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PISLabs.Models;
using Serilog;

namespace PISLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            Log.Information("Get app information");
            var versionInfo = new Models.Version
            {
                Company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company,
                Product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
                ProductVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
            };

            Log.Information($"Acquired version is {versionInfo.ProductVersion}");
            Log.Debug($"Full version info: {@versionInfo}");
            return Ok(versionInfo);
        }
    }
}
