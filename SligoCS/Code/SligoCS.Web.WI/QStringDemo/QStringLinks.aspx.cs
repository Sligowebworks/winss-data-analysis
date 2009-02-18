using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.Base.PageBase.WI;

namespace SligoCS.Web.WI.QStringDemo
{
    public partial class QStingLinks : PageBaseWI
    {

        protected string DESTURL = "/SligoWI/QstringDemo/QStringDemo.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkSession_Click(object sender, EventArgs e)
        {            
            ParamsHelper.FULLKEY = "012619040022";
            ParamsHelper.GraphFile = "HIGHSCHOOLCOMPLETION";
            ParamsHelper.CompareTo = "PRIORYEARS";
            ParamsHelper.ORGLEVEL = "SC";
            ParamsHelper.Group = "AllStudentsFAY";
            ParamsHelper.DN = "Milwaukee";
            ParamsHelper.SN = "Madison Hi";
            ParamsHelper.DETAIL = "YES";
            ParamsHelper.NumSchools = "4";
            ParamsHelper.ZBackTo = "performance.aspx";
            ParamsHelper.FileQuery = "select count(*) from sysdatabases";
            ParamsHelper.FileName = "c:\\sample.txt";
            ParamsHelper.SchoolWebaddress = "http://mpsportal.milwaukee.k12.wi.us";
            ParamsHelper.DistrictWebaddress = "http://www.milwaukee.k12.wi.us";
            ParamsHelper.STYP = 9;
            ParamsHelper.COUNTY = 40;
            ParamsHelper.YearLocal = 2006;
            ParamsHelper.TrendStartYearLocal = 1997;
            ParamsHelper.WhichSchool = 4;
            ParamsHelper.ConferenceKey = 27;
            ParamsHelper.DistrictID = 3619;
            ParamsHelper.GradeBreakout = 999;
            ParamsHelper.GradeBreakoutLAG = 555;
            ParamsHelper.GradeBreakoutEDISA = 212;
            ParamsHelper.SetSessionValues(Session, true);

            Response.Redirect (DESTURL, false);
        }

        protected void linkConfig_Click(object sender, EventArgs e)
        {
            Response.Redirect(DESTURL, false);
        }

        protected void linkMixed_Click(object sender, EventArgs e)
        {
            //<a href="/QstringDemo/QStringDemo.aspx?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=SC&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileQuery=select%20count(*)%20from%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20sys.databases&FileName=c:\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=9&COUNTY=40&YearLocal=2006&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212">By QString</a>
            //DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=9&COUNTY=40&YearLocal=2006&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212">By QString</a>

            //The URL will set the first 4 parameters
            string url = string.Format("{0}?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=SC", DESTURL);

            //The session will set the next 8 parameters.
            ParamsHelper.Group = "AllStudentsFAY";
            ParamsHelper.DN = "Milwaukee";
            ParamsHelper.SN = "Madison Hi";
            ParamsHelper.DETAIL = "YES";
            ParamsHelper.NumSchools = "4";
            ParamsHelper.ZBackTo = "performance.aspx";
            ParamsHelper.FileQuery = "select count(*) from sys.databases";
            ParamsHelper.FileName="c:\\sample.txt";
            ParamsHelper.SetSessionValues(Session, true);

            //The last 11 will use the default values found in web.config.
            //No coded needed for those...


            Response.Redirect(url, false);

        }
    }
}
