using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ReportingApp.Controllers.MVC_controllers;

namespace ReportingApp.Controllers.WebApi_controllers
{
    //[Authorize]  //Your Authorizations
    [RoutePrefix("api/ReportingApi")]
    public class ReportingApiController : ApiController
    {

        [HttpGet]
        [Route("AuthorizedReport")]
        public HttpResponseMessage AuthorizedReport(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "AuthorizedReport");

            ReportingController controller = new ReportingController();

            RouteData route = new RouteData();
            route.Values.Add("action", "GetReportByte");
            route.Values.Add("controller", "Reporting");

            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext
                (new HttpContextWrapper(System.Web.HttpContext.Current), route, controller);
            controller.ControllerContext = newContext;

            var fileDownload = controller.GetReportByte(id);
            response.Content = new ByteArrayContent(fileDownload.MainData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.ReasonPhrase = fileDownload.FileName;
            return response;
        }
    }
}
