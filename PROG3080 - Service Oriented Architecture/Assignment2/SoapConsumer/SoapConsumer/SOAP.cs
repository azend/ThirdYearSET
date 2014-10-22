using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SoapConsumer
{
    public class SOAP
    {
        const String configFile = @"ConfigurationFile.xml";



        public String webServicePName;
        public String webServiceTarget;
        public String webServiceAddress;
        public String webMethodElement;

        public String webMethodPName;

        public SOAP()
        { }

        public void populateServiceData(String pName)
        {
            webServicePName = pName;


            using (XmlReader reader = XmlReader.Create(configFile))
            {
                reader.ReadToFollowing("WebService");
                Boolean exitLoop = false;
                String tempName;
                do
                {
                    tempName = reader.GetAttribute("pName");
                    if (tempName == webServicePName)
                    {
                        using (XmlReader inner = reader.ReadSubtree())
                        {
                            inner.Read();
                            while (inner.Read())
                            {
                                if (inner.NodeType == XmlNodeType.Element)
                                {
                                    switch (inner.Name)
                                    {
                                        case "Target":
                                            webServiceTarget = inner.ReadElementContentAsString();
                                            break;
                                        case "Address":
                                            webServiceAddress = inner.ReadElementContentAsString();
                                            break;
                                        default:
                                            inner.Skip();
                                            break;
                                    }

                                }

                            }
                        }



                        exitLoop = true;
                    }
                } while (exitLoop == false && reader.ReadToNextSibling("WebService"));

            }















        }


        public void populateMethodData(String methodName)
        {

            using (XmlReader reader = XmlReader.Create(configFile))
            {
                reader.ReadToFollowing("WebService");
                Boolean exitLoop = false;
                String pName;
                do
                {
                    pName = reader.GetAttribute("pName");
                    if (pName == webServicePName)
                    {
                        reader.ReadToFollowing("WebMethod");
                        do
                        {

                            if (reader.GetAttribute("pName") == methodName)
                            {

                                using (XmlReader inner = reader.ReadSubtree())
                                {
                                    inner.Read();
                                    while (inner.Read())
                                    {
                                        if (inner.NodeType == XmlNodeType.Element)
                                        {
                                            switch (inner.Name)
                                            {
                                                case "element":
                                                    webMethodElement = inner.ReadElementContentAsString();
                                                    break;
                                                default:
                                                    inner.Skip();
                                                    break;
                                            }

                                        }

                                    }
                                }
                                exitLoop = true;
                            }
                        }
                        while (exitLoop == false && reader.ReadToNextSibling("WebMethod"));

                    }
                } while (exitLoop == false && reader.ReadToNextSibling("WebService"));

            }

        }






        public String generateRequest(SortedList<String, String> variables)
        {
            StringBuilder request = new StringBuilder();
            request.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            request.AppendFormat("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tns=\"{0}\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">",webServiceTarget);
            request.Append("	<soap:Body>");
            request.AppendFormat("		<{0}>",webMethodElement);
            foreach(KeyValuePair<String,String> var in variables)
            {
                request.AppendFormat("			<tns:{0}>{1}</tns:{0}>", var.Key, var.Value);
            }
            
            request.AppendFormat("		</{0}>",webMethodElement);
            request.Append("	</soap:Body>");
            request.Append("</soap:Envelope>");


            return request.ToString();


        }

        public String sendRequest(SortedList<String, String> variables)
        {
            String request = generateRequest(variables);
            HttpWebRequest req = WebRequest.CreateHttp(webServiceAddress);

            //req.Headers.Add("SOAPAction: \"\"");
            req.ContentType = @"text/xml; charset=UTF-8";
            req.UserAgent = @"MySoapApp";
            req.Method = "POST";


            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(request);
                }
            }

            string s = String.Empty;

            try
            {
                //Gets the response
                WebResponse response = req.GetResponse();
                //Writes the Response
                Stream responseStream = response.GetResponseStream();


                StreamReader sr = new StreamReader(responseStream);
                s = sr.ReadToEnd();


            }
            catch (Exception e)
            {
                MessageBox.Show("Error receiving response from server.");
            }

            return s;
        }






        }
    }

