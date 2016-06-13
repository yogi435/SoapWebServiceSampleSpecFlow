using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using OpenQA.Selenium;
namespace SoapWebService.SampleTest
{
    [Binding]
    public class WeatherSampleSteps
    {
        // IWebDriver driver = BaseDriver.getDriver();
        String serviceEndpoint =Properties.Settings.Default.ServiceEndpoint;
        String path = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory().ToString()).ToString();
       
       // String sampleXMLRequestPath = "r:\\My Documents\\Visual Studio 2013\\Projects\\SpecFlowSample\\SpecFlowSample\\SoapRequest.xml";
        String sampleXMLRequestPath ="";

        String soapResult;
        SoapUtil soapUtil = new SoapUtil();
        XmlDocument soapRequestXML;

        [Given(@"I have valid weather request")]
        public void IHaveValidWeatherRequest()
        {
            sampleXMLRequestPath = path + "\\SpecFlowSample\\SoapRequest.xml";
            soapRequestXML = new XmlDocument();
            soapRequestXML.Load(sampleXMLRequestPath);
        }

        [Given(@"I have ""(.*)"" as ""(.*)""")]
        public void GivenIHaveAs(string p0, string p1)
        {
            soapRequestXML = soapUtil.updateXMLSoapRequest(soapRequestXML, p0, p1);

        }


        [When(@"I request for weather report")]
        public void WhenIRequestForWeatherReport()
        {
            soapResult = soapUtil.sendAndGetSoapResponse(serviceEndpoint, soapRequestXML);
        }
        [Then(@"I verify the Status as ""(.*)""")]
        public void ThenIVerifyTheStatusAs(string status)
        {
            soapUtil.verifyStatus(status);

        }

        [Then(@"I Login to ""(.*)""")]
        public void ThenINavigateTo(string p0)
        {

       
            BaseDriver.getDriver().Navigate().GoToUrl(p0);
            LoginPage loginPage = new LoginPage(BaseDriver.getDriver());
            loginPage.signIn("superman", "enterprise1");
        }

        [AfterScenario]
        public void closeBrowser()
        {
            if (BaseDriver.driver != null)
            {

                ITakesScreenshot screenShotDriver = (ITakesScreenshot)BaseDriver.driver;

                Screenshot sc = screenShotDriver.GetScreenshot();
             sc.SaveAsFile("abc.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
             Console.WriteLine(System.IO.Directory.GetCurrentDirectory() + "\\abc.jpg");
                BaseDriver.driver.Close();
                BaseDriver.setDriver(null);
            }



        }
        [Then(@"I get the values from xls")]

        public void IGetTheValuesFromXls()
        {
            WorkbookUtil util = new WorkbookUtil();
            DataTable str = util.GetDataTableFromExcelFile("R:\\My Documents\\Visual Studio 2013\\Projects\\SpecFlowSample\\SpecFlowSample\\Sample.xlsx", "Sheet1");
            foreach (DataRow dataRow in str.Rows)
            {

                foreach (var i in dataRow.ItemArray)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }



}