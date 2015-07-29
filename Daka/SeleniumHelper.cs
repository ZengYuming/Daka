using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Daka
{
    public class SeleniumHelper
    {
        private static ChromeDriver webDriver;
        public static void Execute()
        {
            try
            {
                webDriver = new ChromeDriver();
                //Note: If you get exception of IE security issue then you can look at http://stackoverflow.com/questions/14952348/not-able-to-launch-ie-browser-using-selenium2-webdriver-with-java
                gotoLoginPage();

                Login();

                gotoClockPage();
                swithToTopFrame("mainframe");
                Clock();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool wait(By by)
        {
            // TODO Auto-generated method stub  
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            return wait.Until<bool>((d) =>
               {
                   try
                   {
                       // If the find succeeds, the element exists, and
                       // we want the element to *not* exist, so we want
                       // to return true when the find throws an exception.
                       IWebElement element = d.FindElement(by);
                       return true;
                   }
                   catch (NoSuchElementException)
                   {
                       return false;
                   }
               });
        }


        public static void gotoLoginPage()
        {
            webDriver.Url = "https://info.paxsz.com";
            Console.WriteLine("Go to: " + webDriver.Url);
        }

        public static bool Login()
        {
            try
            {
                if (wait(By.Id("btnLogin")))
                {
                    Console.WriteLine("Login.....");
                    //name
                    string name = ConfigurationManager.AppSettings["name"];
                    IWebElement eUserName = webDriver.FindElement(By.Id("txtUserName"));
                    eUserName.SendKeys(name);
                    Console.WriteLine("Name:" + name);
                    //password
                    string password = ConfigurationManager.AppSettings["password"];
                    IWebElement ePassword = webDriver.FindElement(By.Id("txtPassword"));
                    ePassword.SendKeys(password);
                    Console.WriteLine("Password:" + password);
                    //login
                    IWebElement eLoginButton = webDriver.FindElement(By.Id("btnLogin"));
                    eLoginButton.Click();


                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static void swithToTopFrame(string frameName)
        {
            // all frames
            IList<IWebElement> frames = webDriver.FindElements(By.TagName("frame"));
            IWebElement controlPanelFrame = null;
            foreach (var frame in frames)
            {
                if (frame.GetAttribute("name") == frameName)
                {
                    controlPanelFrame = frame;
                    break;
                }
            }

            if (controlPanelFrame != null)
            {
                webDriver.SwitchTo().Frame(controlPanelFrame);
            }
        }

        public static void gotoClockPage()
        {
            swithToTopFrame("topframe");
            By clockLinkXpath = (By.XPath("//span[@id='ShortCutMenu']/a[contains(@href,'PersonOffice/Clock/Clock.aspx')]"));
            if (wait(clockLinkXpath))
            {
                Console.WriteLine("Goto clock page...");
                //IWebElement eClockLink = webDriver.FindElementById("ShortCutMenu");
                IWebElement eClockLink = webDriver.FindElement(clockLinkXpath);
                //"//a[contains(@href,'/onboarding/Rooms?HotelId=')]"
                eClockLink.Click();
            }
        }

        public static void Clock()
        {

            //IWebElement eClockButton = webDriver.FindElementById("btnClock");
        }
    }
}
