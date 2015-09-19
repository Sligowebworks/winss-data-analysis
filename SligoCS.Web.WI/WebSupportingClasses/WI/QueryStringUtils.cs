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
        public QueryStringUtils(): base()
        {
            //Params to always include in the QueryString
            inQS = new List<String>(HttpContext.Current.Request.QueryString.AllKeys);

            if (!inQS.Contains(GraphFile.Name)) inQS.Add(GraphFile.Name);
            if (!inQS.Contains("SSchoolFullKeys")) inQS.Add("SSchoolFullKeys");
            if (!inQS.Contains("SDistrictFullKeys")) inQS.Add("SDistrictFullKeys");
            if (!inQS.Contains(S4orALL.Name)) inQS.Add(S4orALL.Name);
            if (!inQS.Contains(SRegion.Name)) inQS.Add(SRegion.Name);
            if (!inQS.Contains("SCounty")) inQS.Add("SCounty");
            if (!inQS.Contains("SAthleticConf")) inQS.Add("SAthleticConf");
            if (!inQS.Contains("SCESA")) inQS.Add("SCESA");

        }

        #region protected functions

        /// <summary>
        ///   Returns a string in the format of a standard querystring, containing the name/value pairs of all properties.
        /// Allows caller to override one param name & its value.  Reconstructs the RequestQueryString using any overrides in the StickyParameter class provided and the new name value pair provided.
        /// </summary>
        /// <param name="overrideParamName">Caller can override one param name.  Use String.Empty to ignore.</param>
        /// <param name="overrideParamVal">Caller can override one param's value.  Use String.Empty to ignore.</param>
        /// <returns></returns>
        protected static string GetQueryString(StickyParameter stickyParameter, string overrideParamName, string overrideParamVal)
        {
            //The heart of QueryString generation in conjunction with ReplaceQueryString.

            StringBuilder qstring = new StringBuilder("?");
            NameValueCollection qsVars = HttpContext.Current.Request.QueryString;
            String val = null;
            Object paramval = null;
            PropertyInfo prop;

            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString.Count > 0)
            {
                foreach (String key in StickyParameter.inQS)
                {
                    if (key == null) continue;
                    
                    prop = typeof(StickyParameter).GetProperty(key);
                    paramval = null;
                    val = null;

                    if (prop != null) paramval = prop.GetValue(stickyParameter, null);
                    if (paramval != null)
                    {
                        if (paramval is ParameterValues)
                            val = ((ParameterValues)paramval).Value;
                        else if (prop.PropertyType == typeof(int))
                            val = ((int)paramval).ToString();
                        else if (prop.PropertyType == typeof(string))
                            val = paramval.ToString();
                        else
                        {
                            try //if not a supported type, then get value from Current Request QueryString
                            {
                                val = HttpContext.Current.Request.QueryString[prop.Name];
                            }
                            catch
                            {
                                val = null;
                            }
                        }
                    }
                    
                    if (!String.IsNullOrEmpty(val)) AddToQstring(qstring, key, HttpUtility.UrlEncode(val));
                }
            }

            return ReplaceQueryString(qstring.ToString(), overrideParamName, overrideParamVal);
        }
#endregion //protected functions

        public String GetBaseQueryString()
        {
            return GetQueryString(this, String.Empty, String.Empty);
        }
        public String GetQueryString(String[] NameValuePairs)
        {
            String qs = GetBaseQueryString();
            String[] pair;
            foreach (String param in NameValuePairs)
            {
                pair = param.Split(new char[] {'='});
                qs = ReplaceQueryString(qs, pair[0], pair[1]);
            }
            return qs;
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
            queryString = ContentFilterEncode(queryString); // incase next line returns, and called again at end
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
                    //This line causes QS replacements to be case sensitive. This is good because ParameterValues values are case-sensitive.
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

            queryString =ContentFilterEncode( queryString) ;
            System.Diagnostics.Debug.WriteLine("[querystring:]" + queryString);
            return queryString;
        }
        /// <summary>
        /// guard against content filters blocking XXX
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static String ContentFilterEncode(String qs)
        {
            return (String.IsNullOrEmpty(qs))?
                    qs
                  : qs.Replace('X', Convert.ToChar(96));
        }
        /// <summary>
        /// Reverse Content Filter Encoding
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static String ContentFilterDecode(String qs)
        {
            return (String.IsNullOrEmpty(qs))?
                  qs
                : qs.Replace(Convert.ToChar(96), 'X');
        }


        private static void AddToQstring( StringBuilder qstring, string name, string value)
        {
            if (qstring.Length > 1) // 1 because might contain just the "?"
                qstring.Append("&");

            qstring.AppendFormat("{0}={1}", name, value);
        }
    }
}
