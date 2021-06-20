using BoDi;
using SpecflowNunit.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecflowNunit.Hooks
{
    [Binding]
    public sealed class Hooks1
    {
        private static DriverContext driverProvider;
        private readonly IObjectContainer objectContainer;
        public Hooks1(IObjectContainer container)
        {
            this.objectContainer = container;
        }
        [BeforeTestRun]
        public static void RunBeforeAllTests()
        {
            driverProvider = new DriverContext();
        }

        [BeforeScenario]
        public void RunBeforeScenario()
        {
            objectContainer.RegisterInstanceAs<DriverContext>(driverProvider);
        }


        //[BeforeScenario]
        //public void BeforeScenario()
        //{
        //    ChromeOptions options = new ChromeOptions();
        //    options.AddArgument("start-maximized");
        //    driverProvider.MyDriver = new ChromeDriver(options);
        //    driverProvider.MyDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //    //driverProvider.MyDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        //}

        [AfterTestRun]
        public static void AfterScenario()
        {
            driverProvider.MyDriver.Quit();
        }
    }
}