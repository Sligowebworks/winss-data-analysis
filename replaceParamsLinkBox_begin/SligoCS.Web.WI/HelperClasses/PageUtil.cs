using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI.HelperClasses
{
    public class PageUtil
    {
        public static string GetMappedWebPage(string graphFile)
        {
            string result = string.Empty;
            switch (graphFile)
            {
                case Constants.GRAPH_FILE_RETENTION:
                    result = "GridPage.aspx";
                    break;
                case Constants.GRAPH_FILE_HIGHSCHOOLCOMPLETION:
                    result = "HSCompletionPage.aspx";
                    break;
                case Constants.GRAPH_FILE_ATTENDANCE:
                    result = "AttendancePage.aspx";
                    break;
                case Constants.GRAPH_FILE_TRUANCY:
                    result = "Truancy.aspx";
                    break;
                case Constants.GRAPH_FILE_STAFF:
                    result = "StaffPage.aspx";
                    break;
                case Constants.GRAPH_FILE_MONEY:
                    result = "MoneyPage.aspx";
                    break;
                case Constants.GRAPH_FILE_AP:
                    result = "APTestsPage.aspx";
                    break;
                case Constants.GRAPH_FILE_ACT:
                    result = "ACTPage.aspx";
                    break;
                case Constants.GRAPH_FILE_GGRADREQS:
                    result = "GradReqsPage.aspx";
                    break;
                case Constants.GRAPH_FILE_TEACHERQUALIFICATIONS:
                    result = "TeacherQualifications.aspx";
                    break;

                //case  Constants.GRAPH_FILE_DROPOUTS:
                //    result = "";
                //    break;                    
                //case  Constants.GRAPH_FILE_ETHNICENROLL:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_SUSPEXPINCIDENTS:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_GEXPSERVICES:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_GROUPS:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_SUSPEXP:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_SSACTIVITIES:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_GGRADPLAN:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_GCOURSEOFFER:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_GCOURSETAKE:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_TEACHERQUALIFICATIONSSCATTER:
                //    result = "";
                //    break;
                //case  Constants.GRAPH_FILE_DISABILITIES:
                //    break;
                //case  Constants.GRAPH_FILE_GWRCT:
                //    break;
                //case  Constants.GRAPH_FILE_GGRADRATE:
                //    result = "";
                //    break;
                default:
                    result = "GridPage.aspx";
                    break;

            }
            return result;
        }
    }
}
