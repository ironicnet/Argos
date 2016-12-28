namespace ArgosWeb.Controllers
{
    using System.Web.Http;

    public class ResourcesController : ApiController
    {
        protected readonly ArgosCore.IGuardian Guardian;
        public ResourcesController(ArgosCore.IGuardian guardian)
        {
            Guardian = guardian;
        }
        public IHttpActionResult Get()
        {
            return Json(Guardian.Resources);
        }
    }
}
