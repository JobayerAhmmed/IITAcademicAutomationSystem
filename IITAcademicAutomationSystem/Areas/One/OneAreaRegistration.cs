﻿using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One
{
    public class OneAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "One";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "One_default",
                "One/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "IITAcademicAutomationSystem.Areas.One.Controllers" }
            );
        }
    }
}