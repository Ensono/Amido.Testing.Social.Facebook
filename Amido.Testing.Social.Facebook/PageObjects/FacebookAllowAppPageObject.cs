using System;

using Amido.WebDriver.Utilities.Extensions;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Amido.Testing.Social.Facebook.PageObjects
{
    public class FacebookAllowAppPageObject
    {
        private static Func<IWebDriver> webDriver;

        public static void Init(Func<IWebDriver> getWebDriver)
        {
            webDriver = getWebDriver;
        }

        public FacebookAllowAppPageObject()
        {
            if (webDriver == null)
            {
                throw new Exception("You must call FacebookAllowAppPageObject.Init once before using this class");
            }

            PageFactory.InitElements(webDriver(), this);
        }

        [FindsBy(How = How.Name, Using = "__CONFIRM__")]
        public IWebElement SignInAllow { get; set; }

        public bool IsLoaded()
        {
            return this.SignInAllow.WaitUntil(e => e.Displayed).Displayed;
        }
    }
}