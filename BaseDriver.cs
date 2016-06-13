using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapWebService.SampleTest
{
    class BaseDriver
    {

        public static IWebDriver driver = null;


        public static IWebDriver getDriver()
        {
            String Browser = Properties.Settings.Default.Browser;
            if (driver != null)
            {
                return driver;
            }
            else
            {
                switch (Browser)
                {

                    case "IE": var options = new InternetExplorerOptions();
                        options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        driver = new InternetExplorerDriver(options);
                        return driver;
                    case "FIREFOX":
                        FirefoxProfile profile = new FirefoxProfile();
                        profile.SetPreference("browser.startup.homepage_override.mstone", "ignore");
                        profile.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
                               
                    
                        driver = new FirefoxDriver(profile);
                        return driver;

                    case "CHROME":
                        driver = new ChromeDriver();
                        return driver;
                    default:
                        driver = new FirefoxDriver();
                        return driver;

                }


            }
        }



        public static void setDriver(IWebDriver obj)
        {
            driver = obj;
        }



    }




}

