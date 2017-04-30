using System.Web.Mvc;

namespace Team_Manager.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Administration/Admin
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}