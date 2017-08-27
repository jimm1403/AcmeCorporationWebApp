using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcmeCorporationWebApp.Controllers;
using Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void FormDataValidation()
        {
            Submission sub = new Submission("Jimmi", "Christensen", "jimmi@hotmail.dk",
                "28734552", "19-10-1992", "d497d881-4ebe-4699-8700-a5f3b77ac4b4");
            var controller = new TheDrawController();
            controller.Submit(sub);
        }
    }
}
