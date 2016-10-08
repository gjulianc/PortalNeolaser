using System.Web.Mvc;

namespace PortalNeolaser.Controllers
{
    public partial class PagesController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}