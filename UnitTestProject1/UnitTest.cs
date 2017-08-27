using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcmeCorporationWebApp.Controllers;
using Models;
using System.Collections.Generic;
using System;
using ClassLibrary;

namespace WebAppUnitTest
{
    [TestClass]
    public class UnitTest
    {
        List<Serial> testPSNList = new List<Serial>();
        [TestInitialize]
        public void Setup()
        {
            
            for (int i = 0; i < 10; i++)
            {
                Guid serial = Guid.NewGuid();
                Serial PSN = new Serial() { ProductSerialNumber = serial.ToString(), Uses = 0, Valid = true };
                testPSNList.Add(PSN);
            }
            
        }

        [TestMethod]
        public void TestFormView()
        {
            var controller = new TheDrawController();
            var result = controller.Form() as ViewResult;
            Assert.AreEqual("Form", result.ViewName);
        }
        [TestMethod]
        public void TestSubmissionsView()
        {
            var controller = new TheDrawController();
            var result = controller.Submissions() as ViewResult;
            Assert.AreEqual("Submissions", result.ViewName);
        }
        [TestMethod]
        public void TestPSNValidation1()
        {
            ProductSerialNumberHandler PSNHandler = new ProductSerialNumberHandler();
            string validation;
            validation = PSNHandler.ValidatePsnTEST(testPSNList[2].ProductSerialNumber, testPSNList);
            Assert.AreEqual("valid", validation);
            Assert.AreEqual(1, testPSNList[testPSNList.Count - 1].Uses);
        }
        [TestMethod]
        public void TestPSNValidation2()
        {
            ProductSerialNumberHandler PSNHandler = new ProductSerialNumberHandler();
            string validation;
            PSNHandler.ValidatePsnTEST(testPSNList[2].ProductSerialNumber, testPSNList);
            validation = PSNHandler.ValidatePsnTEST(testPSNList[testPSNList.Count - 1].ProductSerialNumber, testPSNList);
            Assert.AreEqual("valid", validation);
            Assert.AreEqual(2, testPSNList[testPSNList.Count - 1].Uses);
        }

        [TestMethod]
        public void TestPSNValidation3()
        {
            ProductSerialNumberHandler PSNHandler = new ProductSerialNumberHandler();
            string validation;
            PSNHandler.ValidatePsnTEST(testPSNList[2].ProductSerialNumber, testPSNList);
            PSNHandler.ValidatePsnTEST(testPSNList[testPSNList.Count - 1].ProductSerialNumber, testPSNList);
            validation = PSNHandler.ValidatePsnTEST(testPSNList[testPSNList.Count - 1].ProductSerialNumber, testPSNList);
            Assert.AreEqual("invalid", validation);
            Assert.AreEqual(false, testPSNList[testPSNList.Count - 1].Valid);
        }

    }
}
