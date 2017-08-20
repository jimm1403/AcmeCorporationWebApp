using ClassLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcmeCorporationWebApp.Controllers
{
    public class TheDrawController : Controller
    {
        //ProductSerialNumberHandler PSNHandler;
        DataAccess DA;
        

        

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
            return View();
        }
        [HttpPost]
        public ActionResult Validate(FormSub submission)
        {
            FormSub newSub = submission;
            ProductSerialNumberHandler PSNHandler = new ProductSerialNumberHandler();
            //PSNHandler.GenerateSerials();
            PSNHandler.ValidatePSN("c49f6090-a013-4f77-b430-e85580253d98");

            return View("Form");
        }
        public ActionResult GenSerial()
        {
            //ProductSerialNumberHandler PSNHandler = new ProductSerialNumberHandler();
            ////PSNHandler.GenerateSerials();
            //PSNHandler.ValidatePSN("797841dc-32ee-44f0-9fcb-9d251b8f3909");

            return View("Form");
        }

        //
        //GET: /HelloWorld/Welcome/
        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }
    }
}