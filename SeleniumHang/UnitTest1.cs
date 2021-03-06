﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace SampleWebApplication.FunctionalTests
{
    [TestClass]
    public class SampleFunctionalTests
    {
        private static TestContext testContext;
        private RemoteWebDriver driver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SampleFunctionalTests.testContext = testContext;
        }

        [TestMethod]
        public void SampleFunctionalTest1()
        {
            driver = GetChromeDriver();
            var webAppUrl = "www.google.com";
            driver.Navigate().GoToUrl(webAppUrl);
            Assert.AreEqual("Home Page - My ASP.NET Application", driver.Title, "Expected title to be 'Home Page - My ASP.NET Application'");
            var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()) + ".png";
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(filePath);
            testContext.AddResultFile(filePath);
        }

        private RemoteWebDriver GetChromeDriver()
        {
            var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }
    }
}