using Microsoft.AspNetCore.Mvc;

namespace ODataIssue444.Areas.Admin.Localization.Controllers
{
    [Area("Admin/Localization")]
    [Route("admin/localization/localizable-strings")]
    public class LocalizableStringController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("")]
        public Microsoft.AspNetCore.Mvc.ActionResult Index()
        {
            return View();
        }
    }
}