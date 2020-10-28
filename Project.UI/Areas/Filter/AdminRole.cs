using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Filter
{
    public class AdminRole : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filtercontext)
        {
            var SessionControl = filtercontext.HttpContext.Session.GetString("admin");
            if (SessionControl == null)
                filtercontext.Result = new RedirectToActionResult("Index", "Login", new { area = "Admin" });
        }
    }
}
