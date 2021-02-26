using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Framework.Selenium
{
// RemoteWebDriver

// DriverFactory.Build() requires a type of browser, 
//      either local to run on my laptop or remote to run on my Grid, 
//      and the browser I want to use, either Chrome or Firefox. 
//  If type equals "remote", the code will reach this else/if block and call BuildRemoteWebdriver().

// This method uses the RemoteWebDriver class 
//  and requires that we pass in the URI to the port to our Grid, 
//  which is http://localhost:4444/wd/hub, and any other options like --start-maximized that we'd like to use as well. 
// Then we return our newly created driver.


    public static class DriverFactory
    {
        public static IWebDriver Build(string type, string browser)
        {
            if (type == "local")
            {
                switch (browser)
                {
                    case "chrome":
                        return BuildChromeDriver();

                    case "firefox":
                        return BuildFirefoxDriver();

                    default:
                        throw new ArgumentException($"{browser} is not supported locally.");
                }
            }

            else if (type == "remote")
            {
                return BuildRemoteDriver(browser);
            }

            else
            {
                throw new ArgumentException($"{type} is invalid. Choose 'local' or 'remote'.");
            }
        }


        private static RemoteWebDriver BuildRemoteDriver(string browser)
        {
            var DOCKER_GRID_HUB_URI = new Uri("http://localhost:4444/wd/hub");

            RemoteWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions
                    {
                        BrowserVersion = "",
                        PlatformName = "LINUX",
                    };

                    chromeOptions.AddArgument("--start-maximized");

                    driver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, chromeOptions.ToCapabilities());
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions
                    {
                        BrowserVersion = "",
                        PlatformName = "LINUX",
                    };

                    driver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, firefoxOptions.ToCapabilities());
                    break;

                default:
                    throw new ArgumentException($"{browser} is not supported remotely.");
            }

            return driver;
        }

        private static ChromeDriver BuildChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            return new ChromeDriver(FW.WORKSPACE_DIRECTORY + PlatformDriver, options);
        }

        private static FirefoxDriver BuildFirefoxDriver()
        {
            return new FirefoxDriver(FW.WORKSPACE_DIRECTORY + PlatformDriver);
        }

        private static string PlatformDriver
        {
            get
            {
                return Environment.OSVersion.Platform.ToString().Contains("Win")
                    ? "_drivers/windows"
                    : "_drivers/mac";
            }
        }
    }
}
