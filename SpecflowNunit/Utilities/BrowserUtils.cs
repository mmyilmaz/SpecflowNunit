using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SpecflowNunit.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecflowNunit.Utilities
{
    [Binding]
    class BrowserUtils
    {
        private readonly DriverContext driverProvider;

        public BrowserUtils(DriverContext driverProvider)
        {
            this.driverProvider = driverProvider;
        }


        /**
         * Switches to new window by the exact title. Returns to original window if target title not found
         * @param targetTitle
         */
        public void switchToWindow(String targetTitle)
        {
            String origin = driverProvider.MyDriver.CurrentWindowHandle;
            foreach (String handle in driverProvider.MyDriver.WindowHandles)
            {
                driverProvider.MyDriver.SwitchTo().Window(handle);

                if (driverProvider.MyDriver.Title.Equals(targetTitle))
                {
                    return;
                }
            }
            driverProvider.MyDriver.SwitchTo().Window(origin);
        }

        /**
     * Moves the mouse to given element
     *
     * @param element on which to hover
     */
        public void hover(IWebElement element)
        {
            Actions actions = new Actions(driverProvider.MyDriver);
            actions.MoveToElement(element).Perform();
        }


        /**
    * return a list of string from a list of elements
    *
    * @param list of webelements
    * @return list of string
        */
        public static List<String> getElementsText(List<IWebElement> list)
        {
            List<string> elemTexts = new List<string>();
            foreach (IWebElement el in list)
            {
                elemTexts.Add(el.Text);
            }
            return elemTexts;
        }

        /**
     * Extracts text from list of elements matching the provided locator into new List<String>
     *
     * @param locator
     * @return list of strings
     */
        public List<String> getElementsText(By locator)
        {

            var elems = driverProvider.MyDriver.FindElements(locator);
            List<string> elemTexts = new List<string>();

            foreach (IWebElement el in elems)
            {
                elemTexts.Add(el.Text);
            }
            return elemTexts;
        }

        /**
         * Waits for element matching the locator to be visible on the page
         *
         * @param locator
         * @param timeout
         * @return
         */
        public IWebElement waitForVisibility(By locator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driverProvider.MyDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /**
 * Waits for provided element to be clickable
 *
 * @param element
 * @param timeout
 * @return
 */
        public IWebElement waitForClickablility(IWebElement element, int timeout)
        {

            WebDriverWait wait = new WebDriverWait(driverProvider.MyDriver, TimeSpan.FromSeconds(timeout));

            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        /**
 * Waits for element matching the locator to be clickable
 *
 * @param locator
 * @param timeout
 * @return
 */
        public IWebElement waitForClickablility(By locator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driverProvider.MyDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        /**
     * waits for backgrounds processes on the browser to complete
     *
     * @param timeOutInSeconds
     */
        public void waitForPageToLoad(int timeout)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driverProvider.MyDriver;
            int i = 0;
            while (i <= timeout)
            {
                if (js.ExecuteScript("return document.readyState").Equals("complete"))
                {
                    break;
                }
                else
                {
                    i++;
                    Thread.Sleep(500);
                }
            }

        }



        /**
* Clicks on an element using JavaScript
*
* @param element
*/
        public void clickWithJS(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driverProvider.MyDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].click();", element);
        }

        /**
     * Scrolls down to an element using JavaScript
     *
     * @param element
     */
        public void scrollToElement(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driverProvider.MyDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }


        /**
     * Performs double click action on an element
     *
     * @param element
     */
        public void doubleClick(IWebElement element)
        {
            new Actions(driverProvider.MyDriver).DoubleClick(element).Build().Perform();
        }


        /**
     * Changes the HTML attribute of a Web Element to the given value using JavaScript
     *
     * @param element
     * @param attributeName
     * @param attributeValue
     */
        public void setAttribute(IWebElement element, String attributeName, String attributeValue)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driverProvider.MyDriver;
            js.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, attributeName, attributeValue);
        }

        /**
     * Highlighs an element by changing its background and border color
     * @param element
     */
        public void highlight(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driverProvider.MyDriver;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
            Thread.Sleep(1);
            js.ExecuteScript("arguments[0].removeAttribute('style', 'background: yellow; border: 2px solid red;');", element);
        }

        /**
     * attempts to click on provided element until given time runs out
     *
     * @param element
     * @param timeout
     */
        public void clickWithTimeOut(IWebElement element, int timeout)
        {
            for (int i = 0; i < timeout; i++)
            {
                try
                {
                    element.Click();
                    return;
                }
                catch (WebDriverException e)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /**
    * This method will recover in case of exception after unsuccessful the click,
    * and will try to click on element again.
    *
    * @param by
    * @param attempts
    */
        public void clickWithWait(IWebElement element, int attempts)
        {
            int counter = 0;
            //click on element as many as you specified in attempts parameter
            while (counter < attempts)
            {
                try
                {
                    //selenium must look for element again
                    clickWithJS(element);
                    //if click is successful - then break
                    break;
                }
                catch (WebDriverException e)
                {
                    //if click failed
                    //print exception
                    //print attempt
                    e.ToString();
                    ++counter;
                    //wait for 1 second, and try to click again
                    Thread.Sleep(1000);
                }
            }
        }

    }
}