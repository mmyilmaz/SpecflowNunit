using OpenQA.Selenium;
using SpecflowNunit.Context;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecflowNunit.Pages
{
    [Binding]
    public class LoginPage
    {
        private readonly DriverContext driverProvider;

        public LoginPage(DriverContext driverProvider)
        {
            this.driverProvider = driverProvider;
        }

        public IWebElement userName => driverProvider.MyDriver.FindElement(By.Id("prependedInput"));
        public IWebElement password => driverProvider.MyDriver.FindElement(By.Id("prependedInput2"));
        public IWebElement submit => driverProvider.MyDriver.FindElement(By.Name("_submit"));
        public IWebElement userMenu => driverProvider.MyDriver.FindElement(By.XPath("//*[@id='user-menu']/a/i"));
        public IWebElement logout => driverProvider.MyDriver.FindElement(By.XPath("//*[text()='Logout']"));
        public void login()
        {
            userName.SendKeys("");
            password.SendKeys("");
            submit.Click();
            // verification that we logged
        }


    }
}
