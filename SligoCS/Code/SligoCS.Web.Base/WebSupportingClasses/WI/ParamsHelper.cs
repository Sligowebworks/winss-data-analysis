using System;
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
using System.Xml.Serialization;

namespace SligoCS.Web.Base.WebSupportingClasses.WI
{
    /// <summary>
    /// This class will deal with the ~25 or so QueryString variables.
    /// It will populate its properties by looking at the QueryString first, then Session, then web.config.
    /// </summary>
    [Serializable]
    public class ParamsHelper
    {

        #region constants and enums
        //private StringBuilder qstring = null;
        private const string ITEM = "Item";
        public const string SESSIONKEY_PARAMSHELPER = "ParamsHelper";

        
        /// <summary>
        /// Querystring can contain max of 255 characters.  We can use the abbreviations below to limit Querystring length.
        /// To use all keys below with the given examples, Querystring length = 222  [Removed duplicates and SURL and DURL].
        /// </summary>
        public enum QStringVar
        {
            FULLKEY,//: 013619040022 (unique key identifying initial agency selected when user entered site) 
            GraphFile,//: HIGHSCHOOLCOMPLETION (all pages use graphshell.asp as a wrapper - this parameter specifies which include file should be used for a given page) 
            CompareTo,//: PRIORYEARS (used to control display text, SQL concatenation, links, and maybe more) 
            ORGLEVEL,//: SC (used to control display text, SQL concatenation, links, and maybe more) 
            Group,//: AllStudentsFAY (used to control display text, SQL concatenation, links, and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality) 
            STYP,//: 9 (URL code for schooltype links and maybe other schooltype functionality) 
            DN,//: Milwaukee (District Name) 
            SN,//: Madison Hi (School Name) 
            DETAIL,//: YES (used to control whether or not the detail table shows on the page - default is YES - controlled by link on every page) 
            COUNTY,//: 40 (County ID - used in URL? used in SQL concatenation, selected schools queries (schools from the same county), and maybe more) 
            YearLocal,//: 2006 (indicates current year of data shown on page - used in query and titles) 
            TrendStartYearLocal,//: 1997 (indicates which year is the first year in Prior Years view - used in query) 
            WhichSchool,//: 4 (used in selected Schools/Districts view) 
            NumSchools,//: (used in selected Schools/Districts view) 
            ConferenceKey,//: 27 (used in SQL concatenation, selected schools queries (schools from the same Athletic Conference), and maybe more)         
            DistrictID,//: 3619 (not sure where this is used, maybe in queries - may be deprecated) 
            zBackTo,//: performance.asp (also navigation: referring URL for getting back to the four-part graphical menu) 
            FileQuery,//: (used in the Download Raw Data feature - holds SQL that is executed and exported to the CSV) 
            FileName,//: (used in the Download Raw Data feature - holds filename that is executed and exported to the CSV) 


            /// <summary>
            /// Removed from Querystring because possibly deprecated and added too much length.
            /// If we need it later, we'll use it in the Session instead.
            /// </summary>
            SchoolWebaddress,//: http://mpsportal.milwaukee.k12.wi.us (used to make hyperlinks in listing of schools - may be deprecated - some pages now pull this data in the SQL query) 

            /// <summary>
            /// Removed from Querystring because possibly deprecated and added too much length.
            /// If we need it later, we'll use it in the Session instead.
            /// </summary>
            DistrictWebaddress,//: http://www.milwaukee.k12.wi.us (used to make hyperlinks in listing of districts - may be deprecated - some pages now pull this data in the SQL query)

        }


        /// <summary>
        /// Use this enum as a BitMap to display a school's grades & lag grades.
        /// </summary>
        [Flags()]
        public enum GradeLevels
        {
            None = 0,

            HasK = 1,//: F
            HasG1 = 2,//: F
            HasG2 = 4,//: F
            HasG3 = 8,//: F
            HasG4 = 16,//: F
            HasG5 = 32,//: F
            HasG6 = 64,//: F
            HasG7 = 128,//: F
            HasG8 = 256,//: F
            HasG9 = 512,//: T
            HasG10 = 1024,//: T
            HasG11 = 2048,//: T
            HasG12 = 4096,//: T
        }

        /// <summary>
        /// If the user adds &Trace=xx, certain values will appear.
        /// </summary>
        [Flags()]
        public enum TraceLevel
        {            
            None = 0,
            CurrentNameValPairs = 1,
            DefaultNameValuePairs = 2,
            SQLStatement = 4            
        }

        public enum GradeType
        {
            Normal = 1,
            LAG = 2,
            EDISA = 4
        }
        #endregion


        #region class level variables
        private string fULLKEY;//: 013619040022 (unique key identifying initial agency selected when user entered site) 
        private string graphFile;//: HIGHSCHOOLCOMPLETION (all pages use graphshell.asp as a wrapper - this parameter specifies which include file should be used for a given page) 
        private string compareTo;//: PRIORYEARS (used to control display text; SQL concatenation; links; and maybe more) 
        private string oRGLEVEL;//: SC (used to control display text; SQL concatenation; links; and maybe more) 
        private string group;//: AllStudentsFAY (used to control display text; SQL concatenation; links; and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality)     
        private string dN;//: Milwaukee (District Name) 
        private string sN;//: Madison Hi (School Name) 
        private string dETAIL;//: YES (used to control whether or not the detail table shows on the page - default is YES - controlled by link on every page) 
        private string numSchools;//: (used in selected Schools/Districts view) 
        private string zBackTo;//: performance.asp (also navigation: referring URL for getting back to the four-part graphical menu) 
        private string fileQuery;//: (used in the Download Raw Data feature - holds SQL that is executed and exported to the CSV) 
        private string fileName;//: (used in the Download Raw Data feature - holds filename that is executed and exported to the CSV) 
        private string schoolWebaddress;
        private string districtWebaddress;
        private string sql;

        private int sTYP;//: 9 (URL code for schooltype links and maybe other schooltype functionality) 
        private int cOUNTY;//: 40 (County ID - used in URL? used in SQL concatenation; selected schools queries (schools from the same county); and maybe more) 
        private int yearLocal;//: 2006 (indicates current year of data shown on page - used in query and titles) 
        private int trendStartYearLocal;//: 1997 (indicates which year is the first year in Prior Years view - used in query) 
        private int whichSchool;//: 4 (used in selected Schools/Districts view) 
        private int conferenceKey;//: 27 (used in SQL concatenation; selected schools queries (schools from the same Athletic Conference); and maybe more)         
        private int districtID;//: 3619 (not sure where this is used; maybe in queries - may be deprecated) 
        private int gradeBreakout;
        private int gradeBreakoutLAG;
        private int gradeBreakoutEDISA;        

        private TraceLevel traceBreakout;

        private static ParamsHelper defaultValues = null;
        #endregion


        #region public properties
        public string FULLKEY { get { return fULLKEY; } set { fULLKEY = value; } }
        public string GraphFile { get { return graphFile; } set { graphFile = value; } }
        public string CompareTo { get { return compareTo; } set { compareTo = value; } }
        public string ORGLEVEL { get { return oRGLEVEL; } set { oRGLEVEL = value; } }
        public string Group { get { return group; } set { group = value; } }
        public string DN { get { return dN; } set { dN = value; } }
        public string SN { get { return sN; } set { sN = value; } }
        public string DETAIL { get { return dETAIL; } set { dETAIL = value; } }
        public string NumSchools { get { return numSchools; } set { numSchools = value; } }
        public string ZBackTo { get { return zBackTo; } set { zBackTo = value; } }
        public string FileQuery { get { return fileQuery; } set { fileQuery = value; } }
        public string FileName { get { return fileName; } set { fileName = value; } }
        public string SchoolWebaddress { get { return schoolWebaddress; } set { schoolWebaddress = value; } }
        public string DistrictWebaddress { get { return districtWebaddress; } set { districtWebaddress = value; } }
        public string SQL { get { return sql; } set { sql = value; } }

        public int STYP { get { return sTYP; } set { sTYP = value; } }
        public int COUNTY { get { return cOUNTY; } set { cOUNTY = value; } }
        public int YearLocal { get { return yearLocal; } set { yearLocal = value; } }
        public int TrendStartYearLocal { get { return trendStartYearLocal; } set { trendStartYearLocal = value; } }
        public int WhichSchool { get { return whichSchool; } set { whichSchool = value; } }
        public int ConferenceKey { get { return conferenceKey; } set { conferenceKey = value; } }
        public int DistrictID { get { return districtID; } set { districtID = value; } }

        //The next three properties contain the bit fields for the Grade Breakouts for Normal, LAG, and EDISA.
        public int GradeBreakout { get { return gradeBreakout; } set { gradeBreakout = value; } }
        public int GradeBreakoutLAG { get { return gradeBreakoutLAG; } set { gradeBreakoutLAG = value; } }
        public int GradeBreakoutEDISA { get { return gradeBreakoutEDISA; } set { gradeBreakoutEDISA = value; } }

        public TraceLevel TraceLevels { get { return traceBreakout; } set { traceBreakout = value; } }
        #endregion


        #region special properties - Grade Breakout
        public GradeLevels GetGradeBreakout(GradeType gradeType)
        {
            int val = 0;
            if (gradeType == GradeType.Normal)
                val = gradeBreakout;
            else if (gradeType == GradeType.LAG)
                val = gradeBreakoutLAG;
            else if (gradeType == GradeType.EDISA)
                val = gradeBreakoutEDISA;
            GradeLevels retval = GetGradeBreakout(val);

            return retval;
        }
        private GradeLevels GetGradeBreakout(int gradeBreakout)
        {
            GradeLevels retval = GradeLevels.None;
            try
            {
                retval = (GradeLevels)Enum.Parse(typeof(GradeLevels), gradeBreakout.ToString());
            }
            catch
            {
                //do nothing
            }
            return retval;
        }
        #endregion


        #region public functions


        /// <summary>
        /// overrides the standard ToString() method. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            XmlSerializer ser = new XmlSerializer(this.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            //display current values
            if ((TraceLevels & TraceLevel.CurrentNameValPairs) == TraceLevel.CurrentNameValPairs)
                ser.Serialize(writer, this);

            //display default values
            if ((TraceLevels & TraceLevel.DefaultNameValuePairs) == TraceLevel.DefaultNameValuePairs)
                ser.Serialize(writer, DefaultValues);

            writer.Close();

            //display just the SQL statement.
            if ((TraceLevels & TraceLevel.SQLStatement) == TraceLevel.SQLStatement)
                sb.Append(this.SQL);

            return sb.ToString();
        }


        /// <summary>
        /// Implementor.  Returns a string in the format of a standard querystring, containing the name/value pairs of all properties.
        /// Allows caller to override one param name & it's value.
        /// </summary>
        /// <param name="overrideParamName">Caller can override one param name.  Use String.Empty to ignore.</param>
        /// <param name="overrideParamVal">Caller can override one param's value.  Use String.Empty to ignore.</param>
        /// <returns></returns>
        public string GetQueryString(string overrideParamName, string overrideParamVal)
        {
            StringBuilder qstring = new StringBuilder();
            PropertyInfo[] properties = this.GetType().GetProperties();

            //NOTE:
            //  It will be slightly faster at runtime to make a series of calls along the lines of
            //      AddToQstring(qstring, "FULLKEY", FULLKEY);
            //      AddToQstring(qstring, "GraphFile", GraphFile);
            //      ...
            //      rather than retrieving a list of properties from System.Reflection.
            //  However, 
            //      1.  it will require repeating the names of the properties as literal strings
            //          (which violates the "write it only once" principle of development
            //      2.  It will require more maintenance, and we expect this list will change over time.


            //Loop through each of the properties in the current object.
            foreach (PropertyInfo property in properties)
            {
                
                string name = property.Name;
                if (name.ToLower() == overrideParamName.ToLower())
                {
                    AddToQstring(qstring, overrideParamName, overrideParamVal);
                }
                else if (name != ITEM)
                {
                    string val = this[name];
                    if ((val != null) && (val != string.Empty) && (val != "0"))
                    {
                        AddToQstring(qstring, name, val);
                    }
                }
            }

            return qstring.ToString();
        }


        /// <summary>
        /// Overload.  Creates a new URL, based on the current page + current querystring, 
        /// but overwrites a single Querystring param.
        /// </summary>
        /// <param name="overrideQStringParamName"></param>
        /// <param name="overrideQStringParamVal"></param>
        /// <returns></returns>
        public string GetURL(string overrideQStringParamName, string overrideQStringParamVal)
        {
            string retval = string.Empty;

            if ((HttpContext.Current != null) && (HttpContext.Current.Request != null))
            {
                retval = GetURL(HttpContext.Current.Request.Url.LocalPath, overrideQStringParamName, overrideQStringParamVal);
            }
            return retval;                    
        }

        /// <summary>
        /// Implementor.  Creates a new URL, based of given page name + current querystring,
        /// but allows caller to override one querystring parameter.
        /// </summary>
        /// <param name="basePage"></param>
        /// <param name="overrideQStringParamName"></param>
        /// <param name="overrideQStringParamVal"></param>
        /// <returns></returns>
        public string GetURL(string basePageURL, string overrideQStringParamName, string overrideQStringParamVal)
        {
            string qString = GetQueryString(overrideQStringParamName, overrideQStringParamVal);
            string retval = string.Format("{0}?{1}", basePageURL, qString);
            return retval;   
        }
        





        /// <summary>
        /// Saves the properties of this class to Session
        /// </summary>
        /// <param name="session">the web site's Session object</param>
        /// <param name="ignoreNullValues">if true, and if the property value is null (or zero), the session value will NOT be set.</param>
        /// 
        public void SetSessionValues(HttpSessionState session, bool ignoreNullValues)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            //Loop through each of the properties in the current object.
            foreach (PropertyInfo property in properties)
            {
                string name = property.Name;


                if (name != ITEM)
                {
                    //apply a naming convention so we can easily identify any Session variables
                    //  set by this class.  e.g.:  Session["ParamsHelper.FULLKEY"]
                    string sessionVarName = string.Format("{0}.{1}", SESSIONKEY_PARAMSHELPER, name);

                    object ovalue = property.GetValue(this, null);

                    //only set the session value if the property value is non-null,
                    //or the caller requested to overwrite ALL values regardless if null.
                    bool setThisValue = false;
                    if (!ignoreNullValues)
                        setThisValue = true;
                    else if (ovalue != null)
                    {
                        if (ovalue is int)
                        {
                            //an integer with non-zero value.
                            if ((int)ovalue != 0)
                                setThisValue = true;
                        }
                        else
                        {
                            //not an integer, and not null
                            setThisValue = true;
                        }
                    }

                    if (setThisValue)
                    {
                        session[sessionVarName] = ovalue;
                    }
                }
            }
        }


        private delegate string StringPropertyDelegate();

        private void AddToQstring(StringBuilder qstring, string name, string value)
        {
            if (qstring.Length > 0)
                qstring.Append("&");

            qstring.AppendFormat("{0}={1}", name, value);
        }



        /// <summary>
        /// Loads the properties for this class.  Searches Querystring, then Session, then web.config for values.
        /// </summary>
        /// <param name="nvc"></param>
        public void LoadFromContext()
        {
            NameValueCollection queryString = null;
            HttpSessionState session = null;
            if (HttpContext.Current != null)
            {
                queryString = HttpContext.Current.Request.QueryString;
                session = HttpContext.Current.Session;                
            }

            PropertyInfo[] properties = this.GetType().GetProperties();

            //NOTE:
            //  It will be slightly faster at runtime to make a series of calls along the lines of
            //      AddToQstring(qstring, "FULLKEY", FULLKEY);
            //      AddToQstring(qstring, "GraphFile", GraphFile);
            //      ...
            //      rather than retrieving a list of properties from System.Reflection.
            //  However, 
            //      1.  it will require repeating the names of the properties as literal strings
            //          (which violates the "write it only once" principle of development
            //      2.  It will require more maintenance, and we expect this list will change over time.


            //Loop through each of the properties in the current object.
            foreach (PropertyInfo property in properties)
            {
                string name = property.Name;
                if (name != ITEM)
                {
                    object ovalue = GetParamFromContext(name);

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
                        property.SetValue(this, typedValue, null);

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
        /// Gets a single parameter from the context:  Querystring first, then Session, then Default.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public object GetParamFromContext(string paramName)
        {
            NameValueCollection queryString = null;
            HttpSessionState session = null;
            if (HttpContext.Current != null)
            {
                queryString = HttpContext.Current.Request.QueryString;
                session = HttpContext.Current.Session;
            }

            PropertyInfo property = this.GetType().GetProperty(paramName);
            object ovalue = null;

            if (paramName != ITEM)
            {
                //use the naming convention used in SetSessionValues() to retrieve values from Session.
                string sessionVarName = string.Format("{0}.{1}", SESSIONKEY_PARAMSHELPER, paramName);
                if ((queryString != null) && (queryString[paramName] != null))
                {
                    ovalue = queryString[paramName];
                }
                else if ((session != null) && (session[sessionVarName] != null))
                {
                    ovalue = session[sessionVarName];
                }
                else
                {
                    //ovalue = ConfigurationManager.AppSettings[name];
                    ovalue = DefaultValues[paramName];
                }
            }

            return ovalue;
        }






        /// <summary>
        /// Reads the system default values from XML file.
        /// </summary>
        private ParamsHelper DefaultValues
        {
            get
            {
                if (defaultValues == null)
                {

                    //If the default values haven't already been loaded, read them from XML.
                    if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
                    {
                        string path = HttpContext.Current.Server.MapPath("/SligoWI/ParameterDefaults.xml");
                        XmlSerializer ser = new XmlSerializer(this.GetType());
                        StreamReader reader = new StreamReader(path);
                        defaultValues = (ParamsHelper)ser.Deserialize(reader);
                        reader.Close();                        
                    }                   
                }
                return defaultValues;
            }                        
        }
        #endregion


        #region indexer
        /// <summary>
        /// Indexer:  another way to get/set property values.
        ///     Note:  using the indexer is a little slower at runtime than
        ///     calling the strongly-typed properties directly.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public string this[string paramName]
        {
            get
            {
                string retval = String.Empty;
                if (paramName != ITEM)  //don't look for a value for this current indexer.
                {
                    //Look for public, non-static properties; ignore case.
                    PropertyInfo property = this.GetType().GetProperty(paramName, 
                        BindingFlags.GetProperty 
                        | BindingFlags.Public 
                        | BindingFlags.Instance 
                        | BindingFlags.IgnoreCase);
                    if (property != null)
                    {
                        object oRetval = property.GetValue(this, null);
                        if (oRetval != null)
                            retval = oRetval.ToString();
                    }
                }
                return retval;
            }
            set
            {
                PropertyInfo property = this.GetType().GetProperty(paramName);

                if (property != null)
                {
                    string name = property.Name;

                    //this line is necessary to convert the value from string to any other data type (such as int).
                    object typedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(this, typedValue, null);                    
                }                                
            }

        }
        #endregion
    }

}
