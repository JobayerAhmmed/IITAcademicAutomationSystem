using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.Two
{
    public class TwoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Two";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Two_default",
                "Two/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "IITAcademicAutomationSystem.Areas.Two.Controllers" }
            );
        }
    }
}