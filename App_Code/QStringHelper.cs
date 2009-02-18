using System;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// This class will deal with the ~25 or so QueryString variables.
/// </summary>
public class QStringHelper
{

    //private StringBuilder qstring = null;


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

        //HasKlag = 8192,//: F
        //HasG1lag = 16384,//: F
        //HasG2lag = 32768,//: F
        //HasG3lag = 65536,//: F
        //HasG4lag = 131072,//: F
        //HasG5lag = 262144,//: F
        //HasG6lag = 524288,//: F
        //HasG7lag = 1048576,//: F
        //HasG8lag = 2097152,//: F
        //HasG9lag = 4194304,//: T
        //HasG10lag = 8388608,//: T
        //HasG11lag = 16777216,//: T
        //HasG12lag = 33554432,//: T


    }


    public enum GradeType
    {
        Normal = 1,
        LAG = 2,
        EDISA = 4
    }


    /// <summary>
    /// Use this enum to describe a school's EDISA grades.
    /// </summary>
    //[Flags()]
    //public enum GradeBreakoutEDISAEnum
    //{
    //    None = 0,
    //    HasKEDISA = 1,//: F
    //    HasG3EDISA = 2,//: F
    //    HasG4EDISA = 4,//: F
    //    HasG5EDISA = 8,//: F
    //    HasG6EDISA = 16,//: F
    //    HasG7EDISA = 32,//: F
    //    HasG8EDISA = 64,//: F
    //    HasG9EDISA = 128,//: T
    //    HasG10EDISA = 256,//: T               
    //}

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





    public string FULLKEY{get{return fULLKEY;} set{fULLKEY = value;}}
    public string GraphFile{get{return graphFile;} set{graphFile = value;}}
    public string CompareTo{get{return compareTo;} set{compareTo = value;}}
    public string ORGLEVEL{get{return oRGLEVEL;} set{oRGLEVEL = value;}}
    public string Group{get{return group;} set{group = value;}}
    public string DN{get{return dN ;} set{dN = value;}}
    public string SN{get{return sN;} set{sN = value;}}
    public string DETAIL{get{return dETAIL;} set{dETAIL = value;}}
    public string NumSchools{get{return numSchools;} set{numSchools = value;}}
    public string ZBackTo{get{return zBackTo;} set{zBackTo = value;}}
    public string FileQuery{get{return fileQuery;} set{fileQuery = value;}}
    public string FileName{get{return fileName;} set{fileName = value;}}
    public string SchoolWebaddress{get{return schoolWebaddress;} set{schoolWebaddress = value;}}
    public string DistrictWebaddress{get{return districtWebaddress;} set{districtWebaddress = value;}}

    public int STYP{get{return sTYP;} set{sTYP = value;}}
    public int COUNTY{get{return cOUNTY;} set{cOUNTY = value;}}
    public int YearLocal{get{return yearLocal;} set{yearLocal = value;}}
    public int TrendStartYearLocal{get{return trendStartYearLocal;} set{trendStartYearLocal = value;}}
    public int WhichSchool{get{return whichSchool;} set{whichSchool = value;}}
    public int ConferenceKey{get{return conferenceKey;} set{conferenceKey = value;}}
    public int DistrictID{get{return districtID;} set{districtID = value;}}

    //The next three properties contain the bit fields for the Grade Breakouts for Normal, LAG, and EDISA.
    public int GradeBreakout{get{return gradeBreakout;} set{gradeBreakout = value;}}
    public int GradeBreakoutLAG{get{return gradeBreakoutLAG;} set{gradeBreakoutLAG = value;}}
    public int GradeBreakoutEDISA{get{return gradeBreakoutEDISA;} set{gradeBreakoutEDISA = value;}}


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


    /// <summary>
    /// Returns a string in the format of a standard querystring, containing the name/value pairs of all properties.
    /// </summary>
    /// <returns></returns>
    public string GetQueryString()
    {
        StringBuilder qstring = new StringBuilder();
        PropertyInfo[] properties =  this.GetType().GetProperties();

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
            object ovalue = property.GetValue(this, null);
            if (ovalue != null)
            {
                AddToQstring(qstring, name, ovalue.ToString());
            }
        }
        
        return qstring.ToString();
    }

    private delegate string StringPropertyDelegate();

    private void AddToQstring(StringBuilder qstring, string name, string value)
    {
        if (qstring.Length > 0)
            qstring.Append("&");

        qstring.AppendFormat("{0}={1}", name, value);
    }



    /// <summary>
    /// Reads the Querystring object and sets the local values in this class
    /// </summary>
    /// <param name="nvc"></param>
    public void Load(NameValueCollection nvc)
    {
        foreach (string key in nvc.AllKeys)
        {
            //Note, we can run a little faster at runtime if we avoid System.Reflection,
            //however, it would be less flexible and more effort to maintain, 
            //  and we know the list of properties will change.            
            PropertyInfo property = this.GetType().GetProperty(key);
            if (property != null)
            {
                string sval = nvc[key];

                //this line will handle conversions from string to int, datetime, etc.
                object oval = Convert.ChangeType(sval, property.PropertyType);
                
                property.SetValue(this, oval, null);
            }
        }
    }
    
}
