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
    /// Extends user input parameter class with utility methods for creating QueryStrings.
    /// Provides an un-altered instance of parent-class, Sticky Parameter, through the GetBaseQueryString() method.
    /// </summary>
    [Serializable]
    public class QueryStringUtils : StickyParameter
    {
        #region protected functions

        /// <summary>
        /// Implementor.  Returns a string in the format of a standard querystring, containing the name/value pairs of all properties.
        /// Allows caller to override one param name & its value.
        /// </summary>
        /// <param name="overrideParamName">Caller can override one param name.  Use String.Empty to ignore.</param>
        /// <param name="overrideParamVal">Caller can override one param's value.  Use String.Empty to ignore.</param>
        /// <returns></returns>
        protected static string GetQueryString(StickyParameter stickyParameter, string overrideParamName, string overrideParamVal)
        {
            StringBuilder qstring = new StringBuilder("?");
            PropertyInfo[] properties = typeof(StickyParameter).GetProperties();

            //Loop through each of the properties in globalValues object, loop only once.
            foreach (PropertyInfo property in properties)
            {
                string name = property.Name;
                if ((name.ToLower() != "item") && (name.ToLower() != "sql"))
                {
                    //"Item" is a reserved keyword, part of the .NET framework
                    //SQL is used only in tracelevels for debugging, not in the querystring!
                    object val = property.GetValue(stickyParameter, null);
                    //for new stickyparameter architecture of ParameterValue classed properties
                    if (typeof(ParameterValues).IsInstanceOfType(val))
                    {
                        val = ((ParameterValues)val).Value;
                        //val = property.
                    }
                    else
                    {
                        val = property.GetValue(stickyParameter, null).ToString();
                    }

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

            return ReplaceQueryString(qstring.ToString(), overrideParamName, overrideParamVal);
        }
#endregion //protected functions

        public String GetBaseQueryString()
        {
            return GetQueryString(this, String.Empty, String.Empty);
        }
        public String GetQueryString(String Name, String Value)
        {
            return GetQueryString(this, Name, Value);
        }

        /// <summary>
        /// creates an href based on un-altered user input with new value supplied. If name already exists, the old value will be replaced.
        /// </summary>
        /// <param name="fileName">the url base</param>
        /// <param name="newQSName">paramName to be added or Overridden</param>
        /// <param name="newQSValue">New Value</param>
        /// <returns></returns>
        public String CreateNewURL(String fileName, String newQSName, String newQSValue)
        {
            String qString = GetQueryString(this, newQSName, newQSValue);
            return CreateURL(fileName, qString);
        }

        /// <summary>
        ///  Creates a URL, based of given page name andquerystring.
        /// </summary>
        /// <param name="basePage"></param>
        /// <param name="overrideQStringParamName"></param>
        /// <param name="overrideQStringParamVal"></param>
        /// <returns></returns>
        public String CreateURL(String basePageURL, String qString)
        {
            string retval = string.Format("{0}{1}", basePageURL, qString);
            return retval;
        }
        /// <summary>
        /// the queryString should start with ?, it could also be a full URL .
        /// If Parameter is not found, it is added.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="param"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceQueryString(string queryString, string param, string value)
        {
            if (param == String.Empty) return queryString;
            if (queryString.Length > 0 && queryString.IndexOf("?") > 0) throw new Exception("invalid QueryString String");

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

                if (foundParam)
                {
                    int valueStartIndex = queryString.ToLower().IndexOf(paramPart.ToLower()) + paramPart.Length;
                    int valueEndIndex = queryString.IndexOf('&', valueStartIndex);
                    string oldValue;
                    if (valueEndIndex == -1)
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

        public static void AddToQstring( StringBuilder qstring, string name, string value)
        {
            if (qstring.Length > 0)
                qstring.Append("&");

            qstring.AppendFormat("{0}={1}", name, value);
        }

        /// <summary>
        /// overrides the standard ToString() method. 
        /// </summary>
        /// <returns></returns>
        public static string GetTrace(StickyParameter stickyParameter)   
        {
            XmlSerializer ser = new XmlSerializer(typeof(StickyParameter));
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
                seri.Serialize(writer, defaultStickyParameter);
            }
            
            //display just the SQL statement.
            if ((stickyParameter.TraceLevels & StickyParameter.TraceLevel.SQLStatement) == StickyParameter.TraceLevel.SQLStatement)
                sb.Append(stickyParameter.SQL);

            writer.Close();

            return sb.ToString();
        }
    }
}
