using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// These constants are used with the SligoDataGrid to change its behavior.
    /// </summary>
    public class Constants
    {
        [Obsolete("No longer needed.  Use SligoCS.Web.WI.WebServerControls.WI.SligoDataGrid instead.")]
        public const string MERGECOLUMN = "MergeColumn";  //MERGECOLUMN defaults to FALSE

        [Obsolete("No longer needed.  Use SligoCS.Web.WI.WebServerControls.WI.SligoDataGrid instead.")]
        public const string VISIBLECOLUMN = "VisibleColumn"; //VISIBLECOLUMN defaults to TRUE

        [Obsolete("No longer needed.  Use SligoCS.Web.WI.WebServerControls.WI.SligoDataGrid instead.")]
        public const string DISPLAYNAME = "DisplayName"; //Defaults to column name from database.

        [Obsolete("No longer needed.  Use SligoCS.Web.WI.WebServerControls.WI.SligoDataGrid instead.")]
        public const string FORMAT = "Format"; //allows caller to set display format.

        [Obsolete("No longer needed.  Use SligoCS.Web.WI.WebServerControls.WI.SligoDataGrid instead.")]
        public const string FORMAT_RATE = "System.Decimal:#,##0.###";


        /// According to Client email 10-10-08

        ///Rounding.  We would prefer
        ///more consistency in rounding across topics/questions on WINSS.   
        ///For most topics, we'd prefer that rounding be to the nearest 
        ///tenth.  Two exceptions:  (1) round to the nearest whole percent 
        ///if table space is a problem and that needs to happen to get 
        ///the % signs in the cells and (2) round to nearest hundredth 
        ///might be good in some cases if the %s are typically very small 
        ///(e.g. less than 2 or 3%).    
        /// If we need to be consistent across all 
        ///topics for your purposes then we'd pick rounding to the 
        ///nearest tenth.  I don't think we should ever be rounding 
        ///to the nearest thousandth in WINSS tables although I know 
        ///we've been doing that for years for some topics.   Is this 
        ///a good time to make this change?  See Figure 7 for why I 
        ///think Retention might meet the 2nd criterion for an exception - 
        ///prefer rounding to nearest hundredth for this topic.

        ///Rounding for retention in the figures above is
        ///to the nearest thousandth and in this case I think rounding for 
        ///this topic should be to the nearest hundredth.   

        //Round the following rates:
        //    retention, dropouts, and suspension/expulsion rates 
        //to the nearest hundredth of a percent. 
        public const string FORMAT_RATE_02_PERC = "#,##0.##%"; // e.x. 23.12%
        public const string FORMAT_RATE_02 = "#,##0.##"; // e.x. 23.12

        public const string FORMAT_RATE_01_PERC = "#,##0.0%"; // e.x. 23.0%, or 23.1%
        public const string FORMAT_RATE_01 = "#,##0.0"; // e.x. 23.0  or 23.4
        //public const string FORMAT_RATE_DOLLAR_0_NO_DOT = "$#,##0.###";
        // changed back and forth, initial req said $1234.56. #1015 said $1234, to be the same as old site
        public const string FORMAT_RATE_DOLLAR_0_NO_DOT = "$#,##0";  // e.x. 23.123%
        public const string FORMAT_RATE_0_NO_DOT = "#,##0"; // e.x. 23.123%

        //public const string FORMAT_RATE_03_PERC = "#,##0.###%"; // e.x. 23.123%
        public const string FORMAT_RATE_03 = "#,##0.###"; // e.x.  123, or 23.123

        // RedirectUrlMapping = new NameValueCollection();
        public const string BLANK_REDIRECT_PAGE = "BlankPageUrl";
        public const string GRAPH_FILE_RETENTION = "RETENTION";
        public const string GRAPH_FILE_HIGHSCHOOLCOMPLETION = "HIGHSCHOOLCOMPLETION";
        public const string GRAPH_FILE_ATTENDANCE = "ATTENDANCE";
        public const string GRAPH_FILE_TRUANCY = "TRUANCY";
        public const string GRAPH_FILE_STAFF = "STAFF";
        public const string GRAPH_FILE_MONEY = "MONEY";
        public const string GRAPH_FILE_AP = "AP";
        public const string GRAPH_FILE_ACT = "ACT";
        public const string GRAPH_FILE_GGRADREQS = "GGRADREQS";
        public const string GRAPH_FILE_TEACHERQUALIFICATIONS = "TEACHERQUALIFICATIONS";
        public const string GRAPH_FILE_DROPOUTS = "DROPOUTS";
        public const string GRAPH_FILE_ETHNICENROLL = "ETHNICENROLL";
        public const string GRAPH_FILE_SUSPEXPINCIDENTS = "SUSPEXPINCIDENTS";
        public const string GRAPH_FILE_GEXPSERVICES = "GEXPSERVICES";
        public const string GRAPH_FILE_GROUPS = "GROUPS";
        public const string GRAPH_FILE_DISABILITIES = "DISABILITIES";
        public const string GRAPH_FILE_GWRCT = "GWRCT";
        public const string GRAPH_FILE_GGRADPLAN = "GGRADPLAN";

    }
}
