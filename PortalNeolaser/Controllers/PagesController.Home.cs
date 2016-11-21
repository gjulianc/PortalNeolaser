using System.Web.Mvc;
using System.Web.Security;

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