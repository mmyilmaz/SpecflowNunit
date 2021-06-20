using NUnit.Framework;
using SpecflowNunit.Context;
using SpecflowNunit.Pages;
using SpecflowNunit.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecflowNunit.Steps
{
    [Binding]
    class LoginSteps
    {
        private readonly Context.DriverContext driverProvider;
        private readonly LoginPage loginPage;
        private readonly LoginUserDetails _userLoginDetails;
        private readonly BrowserUtils browserUtils;

        public LoginSteps(Context.DriverContext MyDriver, LoginUserDetails userLogin, LoginPage loginPage, BrowserUtils browserUtils)
        {
            this.driverProvider = MyDriver;
            this._userLoginDetails = userLogin;
            this.loginPage = loginPage;
            this.browserUtils = browserUtils;
        }













        [Given(@"the user is on the login pageee")]
        public void GivenTheUserIsOnTheLoginPageee()
        {
            _userLoginDetails.Url = "https://qa1.vytrack.com";
            driverProvider.MyDriver.Navigate().GoToUrl(_userLoginDetails.Url);
            //Thread.Sleep(2000);
        }

        [When(@"user enters ""(.*)"" and ""(.*)""")]
        public void WhenUserEntersAnd(string username, string password)
        {
            loginPage.userName.SendKeys(username);
            loginPage.password.SendKeys(password);
        }

        [Then(@"user should be able to login")]
        public void ThenUserShouldBeAbleToLogin()
        {
            loginPage.submit.Click();
            Thread.Sleep(3000);
            Assert.IsTrue(driverProvider.MyDriver.Title == "Dashboard");
            browserUtils.clickWithWait(loginPage.userMenu, 15);
            browserUtils.clickWithWait(loginPage.logout, 15);
        }



    }
}


