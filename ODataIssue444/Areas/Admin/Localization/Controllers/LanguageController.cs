using Microsoft.AspNetCore.Mvc;

namespace ODataIssue444.Areas.Admin.Localization.Controllers
{
    [Area("Admin/Localization")]
    [Route("admin/localization/languages")]
    public class LanguageController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}