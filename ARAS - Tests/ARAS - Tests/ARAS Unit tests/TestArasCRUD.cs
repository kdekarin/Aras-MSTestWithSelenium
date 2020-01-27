using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ARAS.Tests.ARAS_Unit_tests
{
    [TestClass]
    public class TestArasCRUD : TestBase
    {
        [TestMethod]
        public void TestJetzt()
        {
            Login();
            Navigate();
            CreateRecord();
            DeleteRecord();

            driver.Close();
        }

        public void Login()
        {
            //Navigate to ARAS start page
            driver.Navigate().GoToUrl(config["ARAS-Url"]);

            //Set username
            IWebElement userNameElement = waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("Username")));
            userNameElement.Clear();
            userNameElement.SendKeys("admin");

            //Set password
            IWebElement passwordElement = driver.FindElementByXPath("//input[@id='Password']");
            passwordElement.Clear();
            passwordElement.SendKeys("inn0vator0015#.A");

            //Select Database
            IWebElement databaseElement = driver.FindElement(By.Name("Database"));
            var selectElement = new SelectElement(databaseElement);
            selectElement.SelectByText("InnovatorSolutions");

            //Login
            driver.FindElement(By.XPath("//button[@id='Login']")).Click();

            //Check if user is logged in successfully:
            IWebElement navigationPannelButton = waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@title='Open Navigation Panel']")));
            Assert.IsTrue(navigationPannelButton.Displayed);
        }

        public void Navigate()
        {
            //prove if navigation panel is allready opened if not open it
            bool isNavaigationPanelOpened = Convert.ToBoolean(driver.FindElement(By.XPath("//button[@title='Open Navigation Panel']")).GetAttribute("aria-pressed"));
            if (!isNavaigationPanelOpened)
            {
                IWebElement NavigationPannelButton = driver.FindElement(By.XPath("//button[@title='Open Navigation Panel']"));
                NavigationPannelButton.Click();
            }

            //Choose administration in navigation menu
            IWebElement navAdministration = waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[text() = 'Administration']")));
            navAdministration.Click();

            //Choose Users in navigation menu
            IWebElement navUsers = waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[text() = 'Users']")));
            navUsers.Click();

            //Choose create new in navigation menu
            IWebElement navCreateNewUser = waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='aras-button aras-secondary-menu__create-button']")));
            navCreateNewUser.Click();
        }

        public void CreateRecord()
        {
            //Switch to correct IFRAME element
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//iframe[@class='tabs_content-iframe']")));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@class='tabs_content-iframe']")));
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//iframe[@id='instance']")));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id='instance']")));

            //Enter login name
            driver.FindElement(By.XPath("//input[@name='login_name']")).Click();
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='login_name']")));

            //Enter password
            driver.FindElement(By.XPath("//input[@name='password']")).Click();
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='password']")));

            //Enter other users data
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("myPassword");
            driver.FindElement(By.XPath("//input[@name='confirm_password']")).SendKeys("myPassword");
            driver.FindElement(By.XPath("//input[@name='login_name']")).SendKeys("mylogin name");
            driver.FindElement(By.XPath("//input[@name='starting_page']")).SendKeys("Search");
            //driver.FindElement(By.XPath("//input[@name='default_vault']")).SendKeys("Default");
            driver.FindElement(By.XPath("//input[@name='first_name']")).SendKeys("my name");
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("someemail@domain.com");
            driver.FindElement(By.XPath("//input[@name='fax']")).SendKeys("132456");
            driver.FindElement(By.XPath("//input[@name='cell']")).SendKeys("123456789");
            driver.FindElement(By.XPath("//input[@name='user_no']")).SendKeys("123");
            driver.FindElement(By.XPath("//input[@name='working_directory']")).SendKeys("some working directory");
            driver.FindElement(By.XPath("//input[@name='last_name']")).SendKeys("some last name");
            driver.FindElement(By.XPath("//input[@name='telephone']")).SendKeys("987654321");
            driver.FindElement(By.XPath("//input[@name='home_phone']")).SendKeys("852");
            driver.FindElement(By.XPath("//input[@name='pager']")).SendKeys("456");
            driver.FindElement(By.XPath("//input[@name='company_name']")).SendKeys("T-Systems on site");
            driver.FindElement(By.XPath("//input[@name='mail_stop']")).SendKeys("some mail stop");

            //Switch back from the IFRAME
            driver.SwitchTo().DefaultContent();
            //Switch to IFRAME
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@class='tabs_content-iframe']")));

            //Click on button done
            driver.FindElement(By.XPath("//button[@title='Done Editing']")).Click();
        }

        public void DeleteRecord()
        {
            //Click on button more
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@title='More']"))).Click();

            //Click on buttone delete menu
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//li[@data-index='itemview.itemcommandbar.default.more.deletemenu']"))).Click();

            //Click on the button delete
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//li[@data-index='itemview.itemcommandbar.default.deletemenu.delete']"))).Click();

            //Switch to the default IFRAME
            driver.SwitchTo().DefaultContent();
            //Switch to contents IFRAME
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@class='tabs_content-iframe']")));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@class='aras-dialog__iframe']")));
            
            //Click on button yes - Apply changes
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='btnYes']"))).Click();
            
            //Wait for delete action to execute
            System.Threading.Thread.Sleep(5000);

            //Switch back from the IFRAME
            driver.SwitchTo().DefaultContent();
        }

        //[TestMethod]
        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    Console.WriteLine("Test from initialisor");
        //}

        //[TestMethod]
        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    Console.WriteLine("Test from clean up");
        //}
    }
}