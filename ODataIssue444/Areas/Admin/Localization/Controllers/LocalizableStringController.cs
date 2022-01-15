using Microsoft.AspNetCore.Mvc;

namespace ODataIssue444.Areas.Admin.Localization.Controllers
{
    [Area("Admin/Localization")]
    [Route("admin/localization/localizable-strings")]
    public class LocalizableStringController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}