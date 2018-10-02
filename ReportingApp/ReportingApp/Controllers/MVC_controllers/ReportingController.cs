using System;
using System.Web.Mvc;
using ReportingApp.Models;
using Rotativa;

namespace ReportingApp.Controllers.MVC_controllers
{
    public class ReportingController : Controller
    {
        readonly string _url = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        public FileDownloadViewModel GetReportByte(int id = 1)
        {
            string header = _url + "/reporting/RHeader";
           
            ViewBag.HeaderText = "This report built with Rotativa";

            string customSwitches = string.Format("--header-html  \"{0}\" " +
                                                  "--header-spacing \"0\" " +
                                                 "--header-font-size \"10\" ", header);
           
            var pdfResult = new ViewAsPdf("SampleReport")
            {
                FileName = "SimpleReport.PDF",
                PageSize = Rotativa.Options.Size.A4,
                CustomSwitches = customSwitches
            };
            byte[] applicationPDFData = pdfResult.BuildPdf(ControllerContext);

            return new FileDownloadViewModel
            {
                FileName = pdfResult.FileName,
                MainData = applicationPDFData
            };
        }

      
        [AllowAnonymous]
        public ActionResult RHeader()
        {
            return View("~/views/reporting/RHeader.cshtml");
        }
    }
}