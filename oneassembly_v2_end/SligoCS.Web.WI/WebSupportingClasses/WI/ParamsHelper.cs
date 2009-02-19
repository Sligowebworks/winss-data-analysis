using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlTypes;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

using SligoCS.BL.WI;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    /// <summary>
    /// This class will deal with the ~50 or so QueryString variables.
    /// It will populate its properties by looking at the QueryString first, 
    /// then default value in 'StickyParameterDefaults.xml'.
    /// </summary>
    [Serializable]
    public class ParamsHelper
    {
        private const string ITEM = "Item";

        private static Dictionary<string, string> defaultValues = new Dictionary<string, string>();
         
        #region public functions

        public static OrgLevel GetOrgLevel(StickyParameter stickyParameter)
        {
            OrgLevel orglevel = BLWIBase.GetOrgLevel(stickyParameter.ORGLEVEL);
            return orglevel;
        }

        /// <summary>
        /// overrides the standard ToString() method. 
        /// </summary>
        /// <returns></returns>
        public static string GetTrace(StickyParameter stickyParameter)   
        {
            XmlSerializer ser = new XmlSerializer(stickyParameter.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            //display current values
            if ((stickyParameter.TraceLevels & StickyParameter.TraceLevel.CurrentNameValPairs) == StickyParameter.TraceLevel.CurrentNameValPairs)
                ser.Serialize(writer, stickyParameter);

            //display default values
            if ((stickyParameter.TraceLevels & StickyParameter.TraceLevel.DefaultNameValuePairs) == StickyParameter.TraceLevel.DefaultNameValuePairs)
            {
                //ser.Serialize() does not support Dictionary<string, string>, since it implemented IDictionary
                //XmlSerializer seri = new XmlSerializer(typeof(Dictionary<string, string>));
                //seri.Serialize(writer, DefaultValues);

                // So have to used alternative approach:
                XmlSerializer seri = new XmlSerializer(typeof(StickyParameter));
                StickyParameter defaultStickyParameter = new StickyParameter();
                LoadFromContext(defaultStickyParameter, true);
                seri.Serialize(writer, defaultStickyParameter);
            }
            writer.Close();

            //display just the SQL statement.
            if ((stickyParameter.TraceLevels & StickyParameter.TraceLevel.SQLStatement) == StickyParameter.TraceLevel.SQLStatement)
                sb.Append(stickyParameter.SQL);

            return sb.ToString();
        }

        /// <summary>
        /// Implementor.  Returns a string in the format of a standard querystring, containing the name/value pairs of all properties.
        /// Allows caller to override one param name & its value.
        /// </summary>
        /// <param name="overrideParamName">Caller can override one param name.  Use String.Empty to ignore.</param>
        /// <param name="overrideParamVal">Caller can override one param's value.  Use String.Empty to ignore.</param>
        /// <returns></returns>
        public static string GetQueryString(StickyParameter stickyParameter, string overrideParamName, string overrideParamVal)
        {
            string result = String.Empty;
            string cachedQueryString = String.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.CACHED_QUERY_STRING] != null &&
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.CACHED_QUERY_STRING].ToString() != String.Empty)
            {
                cachedQueryString = HttpContext.Current.Session[SligoCS.BL.WI.Constants.CACHED_QUERY_STRING].ToString();
            }

            if (cachedQueryString == String.Empty)
            {
                StringBuilder qstring = new StringBuilder();
                PropertyInfo[] properties = stickyParameter.GetType().GetProperties();

                //Loop through each of the properties in stickyParameter object, loop only once.
                foreach (PropertyInfo property in properties)
                {
                    string name = property.Name;
                    if ((name != ITEM) && (name.ToLower() != "sql"))
                    {
                        //"Item" is a reserved keyword, part of the .NET framework
                        //SQL is used only in tracelevels for debugging, not in the querystring!
                        object val = property.GetValue(stickyParameter, null);
                        if (val != null)
                        {
                            string valString = val.ToString();
                            if ((valString != string.Empty) && (valString != "0"))
                            {
                                AddToQstring(qstring, name, HttpUtility.UrlEncode(valString));
                            }
                            //if ((valString != string.Empty))
                            //{
                            //    AddToQstring(qstring, name, HttpUtility.UrlEncode(valString));
                            //}
                            //if (valString == "0")
                            //{
                            //    string var = name;
                            //    //throw new Exception("Another para other than S4All that has zero as value.");
                            //}

                        }
                    }
                }
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.CACHED_QUERY_STRING] =
                    "?" + qstring.ToString();
                cachedQueryString = "?" + qstring.ToString();
            }

            if (overrideParamName != String.Empty)
            {
                result = ReplaceQueryString(cachedQueryString, overrideParamName, overrideParamVal);
            }
            else
            {
                result = cachedQueryString;
            }

            return result;
        }
        
        // the queryString should start with ?, it could also be a full URL 
        public static string ReplaceQueryString(string queryString, string param, string value)
        {
            
            if (queryString.IndexOf("?") >= 0)
            {
                bool foundParam = false;
                string paramPart = "";
                if (queryString.ToLower().IndexOf("?" + param.ToLower() + "=") >= 0)
                {
                    foundParam = true;
                    paramPart = "?" + param + "=";
                }
                else if (queryString.ToLower().IndexOf("&" + param.ToLower() + "=") >= 0)
                {
                    foundParam = true;
                    paramPart = "&" + param + "=";
                }

                if ( foundParam )
                {
                    int valueStartIndex = queryString.ToLower().IndexOf(paramPart.ToLower()) + paramPart.Length;
                    int valueEndIndex = queryString.IndexOf('&', valueStartIndex);
                    string oldValue;
                    if ( valueEndIndex == -1 )
                    {
                        oldValue = queryString.Substring(valueStartIndex);
                    }
                    else
                    {
                        oldValue = queryString.Substring(valueStartIndex, valueEndIndex - valueStartIndex);
                    }
                    queryString = queryString.Replace(paramPart + oldValue, paramPart + value);
                }
                else
                {
                    queryString += "&" + param + "=" + value;
                }
            }
            else
            {
                queryString += "?" + param + "=" + value;
            }
            return queryString;
        }

        public static NameValueCollection ParseQueryString(string queryString)
        {
            string regexPattern = @"\?(?<nv>(?<n>[^=]*)=(?<v>[^&]*)[&]?)*";
            Regex regex = new Regex(regexPattern, RegexOptions.ExplicitCapture);
            Match match = regex.Match(queryString);
            NameValueCollection nameValueCollection = new NameValueCollection();

            for (int currentCapture = 0; currentCapture < match.Groups["nv"].Captures.Count; currentCapture++)
            {
                nameValueCollection.Add(match.Groups["n"].Captures[currentCapture].Value,
                match.Groups["v"].Captures[currentCapture].Value);
            }

            return nameValueCollection;
        }

        /// <summary>
        /// Overload.  Creates a new URL, based on the current page + current querystring, 
        /// but overwrites a single Querystring param.
        /// </summary>
        /// <param name="overrideQStringParamName"></param>
        /// <param name="overrideQStringParamVal"></param>
        /// <returns></returns>
        public static string GetURL(StickyParameter stickyParameter, string overrideQStringParamName, string overrideQStringParamVal)
        {
            string retval = string.Empty;

            if ((HttpContext.Current != null) && (HttpContext.Current.Request != null))
            {
                retval = GetURL(stickyParameter, HttpContext.Current.Request.Url.LocalPath, overrideQStringParamName, overrideQStringParamVal);
            }
            return retval;                    
        }

        /// <summary>
        /// Implementor.  Creates a new URL, based of given page name + current querystring,
        /// but allows caller to override one querystring stickyParameter.
        /// </summary>
        /// <param name="basePage"></param>
        /// <param name="overrideQStringParamName"></param>
        /// <param name="overrideQStringParamVal"></param>
        /// <returns></returns>
        public static string GetURL(StickyParameter stickyParameter, string basePageURL, string overrideQStringParamName, string overrideQStringParamVal)
        {
            string qString = GetQueryString(stickyParameter, overrideQStringParamName, overrideQStringParamVal);
            string retval = string.Format("{0}{1}", basePageURL, qString);
            return retval;   
        }

        public static void AddToQstring(StringBuilder qstring, string name, string value)
        {
            if (qstring.Length > 0)
                qstring.Append("&");

            qstring.AppendFormat("{0}={1}", name, value);
        }

        /// <summary>
        /// Loads the properties for StickyParameter object.  
        /// Searches Querystring,  then 'StickyParameterDefaults.xml' for default values.
        /// </summary>
        /// <param name="nvc"></param>
        public static void LoadFromContext(StickyParameter stickyParameter)
        {
            // load in sequenc
            LoadFromContext(stickyParameter,false);
        }

        /// <summary>
        /// Loads the properties for StickyParameter object.  Searches Querystring, 
        /// then web.StickyParameterDefaults.xml for default values.
        /// If loadOnlyFromDefault = true, then load only from default dictionary
        /// </summary>
        /// <param name="nvc"></param>
        public static void LoadFromContext(StickyParameter stickyParameter, bool loadOnlyFromDefault)
        {
            NameValueCollection queryString = null;
            HttpSessionState session = null;
            if (HttpContext.Current != null)
            {
                queryString = HttpContext.Current.Request.QueryString;
                session = HttpContext.Current.Session;
            }
            ////BR: notes, this is temporary code only for debugging, will be removed before release
            //if ( queryString.Keys.Count < 27  && queryString.Keys.Count > 1 )
            //{
            //    //throw new Exception ("The query string was too short, or it was somehow cut short.");
            //}
            PropertyInfo[] properties = stickyParameter.GetType().GetProperties();

            //Loop through each of the properties in the stickyParameter object.
            foreach (PropertyInfo property in properties)
            {
                ///BR: There is no heavy use of System.Reflection here, 29 times every round trip.

                string name = property.Name;
                if (name != ITEM)
                {
                    object ovalue = GetParamFromContext(name, loadOnlyFromDefault);

                    //attempt to cast the object as its expected type.
                    try
                    {
                        object typedValue = null;
                        if (property.PropertyType.BaseType.ToString() == "System.Enum")
                        {
                            //this property is an enummeration (e.g. TraceLevels) 
                            //so we need to handle the type casting differently.
                            typedValue = Enum.Parse(property.PropertyType, ovalue.ToString());
                        }
                        else
                        {
                            //this line is necessary to convert the value from string to any other data type (such as int).                    
                            typedValue = Convert.ChangeType(ovalue, property.PropertyType);
                        }
                        property.SetValue(stickyParameter, typedValue, null);

                    }
                    catch
                    {
                        string msg = string.Format("Could not determine a value for {0}", name);
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a single Sticky Parameter from the context:  Querystring first,
        /// then Default value from xml file 'StickyParameterDefaults.xml'.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns>class, object</returns>
        public static object GetParamFromContext(string paramName, bool loadOnlyFromDefault)
        {
            NameValueCollection queryString = null;
            HttpSessionState session = null;
            if (HttpContext.Current != null)
            {
                queryString = HttpContext.Current.Request.QueryString;
                session = HttpContext.Current.Session;
            }
            object ovalue = null;

            if (paramName != ITEM)
            {
                //use the naming convention used in SetSessionValues() to retrieve values from Session.
                //string sessionVarName = string.Format("{0}.{1}", SESSIONKEY_StickyParameter, paramName);
                if (loadOnlyFromDefault == false)
                {
                    if ((queryString != null) && (queryString[paramName] != null))
                    {
                        ovalue = queryString[paramName];
                    }
                    else if (DefaultValues.ContainsKey(paramName))
                    {
                        //ovalue = ConfigurationManager.AppSettings[name];
                        ovalue = DefaultValues[paramName];
                    }
                }
                else
                {
                    if (DefaultValues.ContainsKey(paramName))
                    {
                        //ovalue = ConfigurationManager.AppSettings[name];
                        ovalue = DefaultValues[paramName];
                    }
                }
            }

            return ovalue;
        }

        /// <summary>
        /// Reads the system default values from XML file 'StickyParameterDefaults.xml'
        /// </summary>
        private static Dictionary<string, string> DefaultValues
        {
            get
            {
                if (defaultValues.Count == 0)
                {

                    string path = "..\\..\\..\\SligoCS.Web.WI\\StickyParameterDefaults.xml";
                    //If the default values haven't already been loaded, read them from XML.
                    if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
                    {
                        path = HttpContext.Current.Server.MapPath("/SligoWI/ParameterDefaults.xml");
                    }

                    if (File.Exists(path))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(path);
                        if (doc.LastChild.Name == "StickyParameter")
                        {
                            XmlNode stickyParameterNode = doc.LastChild;
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
        #endregion

    }

}
