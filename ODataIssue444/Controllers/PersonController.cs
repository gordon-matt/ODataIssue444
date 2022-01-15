using Microsoft.AspNetCore.Mvc;

namespace ODataIssue444.Controllers
{
    [Route("people")]
    public class PersonController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}