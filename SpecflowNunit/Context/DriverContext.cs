using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpecflowNunit.Context
{
    public class DriverContext
    {
        public DriverContext()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            MyDriver = new ChromeDriver(options);
        }
        public IWebDriver MyDriver { get; set; }
    }
}
