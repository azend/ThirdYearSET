using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SoapConsumer
{
    public struct varData
    {
        public readonly String varName;
        public readonly String varType;
        public readonly String varPName;


        public varData(String varName, String varType, String varPName)
        {
            this.varName = varName;
            this.varType = varType;
            this.varPName = varPName;

        }
    }
    public static class ConfigReader
    {

        const String configFile = @"ConfigurationFile.xml";

        public static List<String> getServiceNames()
        {
            List<String> serviceNames = new List<String>();

            using (XmlReader reader = XmlReader.Create(configFile))
            {
                reader.ReadToFollowing("WebService");
                do
                {
                    serviceNames.Add(reader.GetAttribute("pName"));
                } while (reader.ReadToNextSibling("WebService"));
            }

            return serviceNames;
        }

        public static List<String> getMethodNames(String serviceName)
        {
            List<String> methodNames = new List<String>();

            using (XmlReader reader = XmlReader.Create(configFile))
            {
                reader.ReadToFollowing("WebService");
                Boolean exitLoop = false;
                String pName;
                do
                {
                    pName = reader.GetAttribute("pName");
                    if (pName == serviceName)
                    {
                        reader.ReadToFollowing("WebMethod");
                        do
                        {
                            methodNames.Add(reader.GetAttribute("pName"));
                        }
                        while (reader.ReadToNextSibling("WebMethod"));
                        exitLoop = true;
                    }
                } while (exitLoop == false && reader.ReadToNextSibling("WebService"));

            }
            return methodNames;
        }


        public static List<varData> getVariables(String serviceName, String methodName)
        {
            List<varData> variableData = new List<varData>();

            using (XmlReader reader = XmlReader.Create(configFile))
            {
                reader.ReadToFollowing("WebService");
                Boolean exitLoop = false;
                String pName;
                String mName;
                do
                {
                    pName = reader.GetAttribute("pName");
                    if (pName == serviceName)
                    {
                        reader.ReadToFollowing("WebMethod");
                        do
                        {
                            mName = reader.GetAttribute("pName");

                            if (mName == methodName)
                            {
                                reader.ReadToFollowing("Input");
                                using (XmlReader inner = reader.ReadSubtree())
                                {
                                    Boolean nameFlag = false;
                                    Boolean typeFlag = false;
                                    if (inner.ReadToDescendant("Value"))
                                    {
                                        do
                                        {
                                            String tempPName = inner.GetAttribute("pName");
                                            String tempName = String.Empty;
                                            String tempType = String.Empty;


                                            //Dirty code starts here
                                            inner.ReadToDescendant("Name");
                                            tempName = inner.ReadElementContentAsString();


                                            inner.ReadToFollowing("Type");
                                            tempType = inner.ReadElementContentAsString();

                                            //TODO Fix the dirty code above
                                            /*Where it should be fixed
                                             do
                                             {
                                         
                                                inner.ReadStartElement();
                                                switch (inner.Name)
                                                {
                                                    case "Name":
                                                        tempName = inner.ReadElementContentAsString();
                                                        nameFlag = true;
                                                        break;
                                                    case "Type":
                                                        tempType = inner.ReadElementContentAsString();
                                                        typeFlag = true;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }while(nameFlag == false && typeFlag == false);

                                            nameFlag = false;
                                            typeFlag = false;
                                            */
                                            variableData.Add(new varData(tempName, tempType, tempPName));

                                        } while (inner.ReadToFollowing("Value"));
                                    }
                                    
                                    exitLoop = true;

                                }

                            }
                        }
                        while (exitLoop == false && reader.ReadToNextSibling("WebMethod"));
                    }
                } while (exitLoop == false && reader.ReadToNextSibling("WebService"));

            }
            return variableData;
        }



    }
}
