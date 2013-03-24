using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using System.Xml;

using SligoCS.BL.WI;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    /// <summary>
    /// Basically a Struct, for User Input Parameters.
    /// It will populate its properties by looking at the QueryString first, 
    /// then default value in 'StickyParameterDefaults.xml'.
    /// Note: partial class. see .PublicAccessors.cs and .PrivateMembers.cs for QS variable declarations
    /// </summary>
    [Serializable]
    public partial class StickyParameter
    {
        private static Dictionary<string, string> defaultValues = new Dictionary<string, string>();

        /// <summary>
        /// What parameters to include when creating QueryStrings for Links. Contains all names in Request.QueryString by default;
        /// CAUTION: Check that duplicate keys are not added.
        /// </summary>
        public List<String> inQS;

        private String InitializeProperty(String name)
        {
            return InitializeProperty(name, HttpContext.Current);
        }
        public static String InitializeProperty(String name, HttpContext context)
        {
            object obj = GetParamFromUser(name, context);
            if (obj == null) obj = StickyParameter.GetParamDefault(name);
            if (obj.ToString() == String.Empty) obj = null;

            return (obj == null) ?
                null
                : QueryStringUtils.ContentFilterDecode(obj.ToString())
            ;
        }
        public Object GetParamFromUser(String paramName)
        {
            return GetParamFromUser(paramName, HttpContext.Current);
        }

        public static Object GetParamFromUser(String paramName, HttpContext context)
        {
            NameValueCollection queryString = new NameValueCollection();

            if (paramName.Contains("."))
            {
                paramName = paramName.Substring(paramName.LastIndexOf(".") + 1);
            }
            if (context != null && context.Request.QueryString.Count > 0)
            {
                queryString = context.Request.QueryString;
            }
            return (String.IsNullOrEmpty(queryString[paramName])) ? null : queryString[paramName];
        }
        /// <summary>
        /// Gets a single Sticky Parameter from ParameterDefaults.xm'.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns>class, object</returns>
        public static object GetParamDefault(string paramName)
        {
            object ovalue = null;

            if (paramName.Contains("."))
            {
                paramName = paramName.Substring(paramName.LastIndexOf(".") + 1);
            }

            if (paramName != "Item")
            {
             //if (DefaultValues.ContainsKey(paramName))
                try
                {
                    //ovalue = ConfigurationManager.AppSettings[name];
                    ovalue = DefaultValues[paramName];
                }
                catch (Exception e)
                {
                    throw new Exception("ParamName[" + paramName + "], not found in configuration", e);
                    }
            }

            return ovalue;
        }
        
        /// <summary>
        /// Reads the system default values from XML file
        /// </summary>
        private static Dictionary<string, string> DefaultValues
        {
            get
            {
                if (defaultValues.Count == 0)
                {

                    string path = null;
                    //If the default values haven't already been loaded, read them from XML.
                    if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
                    {
                        path = HttpContext.Current.Server.MapPath("~/ParameterDefaults.xml");
                    }

                    if (File.Exists(path))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(path);
                        if (doc.GetElementsByTagName("StickyParameter").Count > 0)
                        {
                            XmlNode stickyParameterNode = doc.SelectSingleNode("StickyParameter");
                            foreach (XmlNode child in stickyParameterNode.ChildNodes)
                            {
                                if (!defaultValues.ContainsKey(child.Name))
                                {
                                    defaultValues.Add(child.Name, child.InnerText);
                                }
                            }
                        }
                    }
                }
                return defaultValues;
            }
        }
    }
}
