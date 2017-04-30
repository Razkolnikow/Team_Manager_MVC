using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Team_Manager.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string CurrentUserId => User.Identity.GetUserId();
    }
}