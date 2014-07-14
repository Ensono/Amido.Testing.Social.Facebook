using System;

using Amido.Testing.Social.Facebook.PageObjects;

using OpenQA.Selenium;

namespace Amido.Testing.Social.Facebook
{
    public static class Bootstrap
    {
        public static void Init(Func<IWebDriver> webDriver)
        {
            FacebookSignInPageObject.Init(webDriver);
            FacebookAllowAppPageObject.Init(webDriver);
        }
    }
}