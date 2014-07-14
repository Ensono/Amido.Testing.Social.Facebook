using System;

using Amido.WebDriver.Utilities.Extensions;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Amido.Testing.Social.Facebook.PageObjects
{
    public class FacebookSignInPageObject
    {
        private static Func<IWebDriver> webDriver;

        public static void Init(Func<IWebDriver> getWebDriver)
        {
            if (webDriver == null)
            {
                throw new Exception("You must call FacebookSignInPageObject.Init once before using this class");
            }

            webDriver = getWebDriver;
        }

        public FacebookSignInPageObject()
        {
            PageFactory.InitElements(webDriver(), this);
        }

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.Id, Using = "pass")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Name, Using = "login")]
        public IWebElement LoginButton { get; set; }

        public bool IsLoaded()
        {
            return this.LoginButton.WaitUntil(e => e.Displayed).Displayed;
        }
    }
}