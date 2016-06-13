using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapWebService.SampleTest
{
    using Microsoft.Web.Services3;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Xml;
    using System.Xml.Linq;
    
    class SoapUtil
    {
        String soapResult;


        ///<summary>
        ///updating the sample soap xml request with the appropriate tag values
        ///</summary>

        internal XmlDocument updateXMLSoapRequest(XmlDocument soapRequestXML, String nodeName, String nodeInnerXml)
        {
                        
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapRequestXML.NameTable);
           nsmgr.AddNamespace("web", "http://www.webserviceX.NET");
            XmlNode node = soapRequestXML.SelectSingleNode("//web:" + nodeName, nsmgr);
           Console.WriteLine(node.InnerText);
            node.InnerText = nodeInnerXml;

            Console.WriteLine(node.InnerText);
               return soapRequestXML;

        }

        ///<summary>
        ///Send soap request and get response from the webservice endpoint
        ///</summary>

        internal String sendAndGetSoapResponse(String endPoint, XmlDocument xmlSoapRequest)
        {
                       
             HttpWebRequest request = CreateWebRequest(endPoint);
            NetworkCredential credentials = new NetworkCredential();
            credentials.UserName = "";
            credentials.Password = "";
            request.Credentials=credentials;
            
            using (Stream stream = request.GetRequestStream())
            {
                xmlSoapRequest.Save(stream);
            }
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                }
            }
            getTemperatureOfWeatherReport(soapResult);
            return soapResult;
        }


           public static HttpWebRequest CreateWebRequest(String endPoint)
        {
           
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "application/soap+xml;charset=\"utf-8\"";
            webRequest.Accept =  "application/soap+xml;charset=\"utf-8\"";
            webRequest.Method = "POST";
            return webRequest;
        }

        public String getTemperatureOfWeatherReport(String soapResult)
        {

            XmlDocument soapResponseXML = new XmlDocument();

            soapResponseXML.LoadXml(soapResult);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapResponseXML.NameTable);
            nsmgr.AddNamespace("abc", "http://www.webserviceX.NET");
            XmlNode node = soapResponseXML.SelectSingleNode("//abc:" + "GetWeatherResult", nsmgr);
            XmlDocument cDATAXML = new XmlDocument();
            cDATAXML.LoadXml(node.InnerText);
           return cDATAXML.SelectSingleNode("//Temperature").InnerText;

        }



        internal void verifyStatus(string expectedStatus)
        {
            XmlDocument soapResponseXML = new XmlDocument();

            soapResponseXML.LoadXml(soapResult);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapResponseXML.NameTable);
            nsmgr.AddNamespace("abc", "http://www.webserviceX.NET");
            XmlNode node = soapResponseXML.SelectSingleNode("//abc:" + "GetWeatherResult", nsmgr);
            XmlDocument cDATAXML = new XmlDocument();
            Console.Write(node.InnerText);
            cDATAXML.LoadXml(node.InnerText);
            String actualStatus = cDATAXML.SelectSingleNode("//Status").InnerText;
            Console.Write("" + actualStatus);
            Assert.IsTrue(expectedStatus.Equals(actualStatus,StringComparison.Ordinal));
            Console.Write("************************" + cDATAXML.SelectSingleNode("//Temperature").InnerText);


        }
    }

}
