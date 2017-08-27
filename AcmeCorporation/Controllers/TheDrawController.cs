using ClassLibrary;
using Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AcmeCorporationWebApp.Controllers
{
    public class TheDrawController : Controller
    {
        ProductSerialNumberHandler PSNHandler;
        DataAccess dataAccess;
       
        //
        // GET: /TheDraw/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TheDraw/Form/
        public ActionResult Form()
        {
            return View("Form");
        }

        //
        //GET: /TheDraw/Submit
        [HttpPost]
        public ActionResult Submit(Submission submission)
        {
            if (ModelState.IsValid)
            {
                if (Validate(submission.ProductSerialNumber) == "valid")
                {
                    ViewBag.Message = "true";
                    dataAccess = new DataAccess();
                    dataAccess.SaveSubmission(submission);
                    //Submission newSub = submission;
                }
                else
                {
                    ViewBag.Message = "false";
                }
                ModelState.Clear();
            }

            return View("Form");
        }

        //
        //GET: /TheDraw/Validate
        private string Validate(string PSN)
        {
            PSNHandler = new ProductSerialNumberHandler();

            string validation;

            validation = PSNHandler.ValidatePSN(PSN);
            return validation;
        }

        //
        //GET: /TheDraw/Submissions
        public ActionResult Submissions()
        {
            dataAccess = new DataAccess();
            List<Submission> subList = new List<Submission>();
            subList.AddRange(dataAccess.GetSubmissions());

            return View("Submissions", subList);
        }
    }
}