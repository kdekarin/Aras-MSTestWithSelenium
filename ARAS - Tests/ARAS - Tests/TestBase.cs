using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARAS.Tests
{
    [TestClass]
    public class TestBase
    {
        public IConfiguration config;
        public ChromeDriver driver;
        public WebDriverWait waitForElement;

        public TestBase()
        {
            config = InitConfiguration();
            string chromiumDirectory = config["ChromiumDirectory"];

            
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = AppDomain.CurrentDomain.BaseDirectory + chromiumDirectory;
            chromeOptions.AddArguments("no-sandbox");
            string chromeDriverPath = AppDomain.CurrentDomain.BaseDirectory;
            driver = new ChromeDriver(chromeDriverPath, chromeOptions, TimeSpan.FromSeconds(600));
            waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        [AssemblyInitialize]
        public static void BeforeClass(TestContext tc)
        {
            Console.WriteLine("Before all tests");
        }

        [TestInitialize]
        public void BeforeTest()
        {
            Console.WriteLine("Before each test");
        }

        public IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
    }
}