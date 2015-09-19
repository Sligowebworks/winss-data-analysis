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
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class BottomLinkDownload : System.Web.UI.UserControl
    {
        private string url;
        private string statewideDownloadUrl;
        public Panel pnlStatewideDownload = new Panel();

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        public string StatewideDownloadUrl
        {
            get
            {
                return statewideDownloadUrl;
            }
            set
            {
                statewideDownloadUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["GenericCsvName"]))
            {
                Url = ResolveUrl("~/serveRawDataCsv.aspx");
            }
            else
            {
                Url = ResolveUrl("~/" + Session["RawCsvName"]);
            }

            PageBaseWI page = ((PageBaseWI)Page);
            SupDwnld supDwnld = page.GlobalValues.SuperDownload;

            StatewideDownloadUrl = Request.Url.LocalPath + page.UserValues.GetQueryString(supDwnld.Name, supDwnld.Range[SupDwnldKeys.True]);

            pnlStatewideDownload.Visible = showStatewideDownloadPanel();
        }

        private Boolean showStatewideDownloadPanel()
        {
            GlobalValues globals = ((PageBaseWI)Page).GlobalValues;
            System.Collections.Generic.List<String> pages = new System.Collections.Generic.List<String>
            (
                new String[] { 
                    GraphFileKeys.ACT,
                    GraphFileKeys.ActivitiesPartic,
                    GraphFileKeys.ActivityOffer,
                    GraphFileKeys.AP,
                    GraphFileKeys.ATTENDANCE,
                    GraphFileKeys.CompareContinuing,
                    GraphFileKeys.DISABILITIES,
                    GraphFileKeys.DROPOUTS,
                    GraphFileKeys.EXPULSIONS,
                    GraphFileKeys.EXPULSIONSDAYSLOST,
                    GraphFileKeys.GEXPRETURNS,
                    GraphFileKeys.GEXPSERVICES,
                    GraphFileKeys.GGRADREQS,
                    GraphFileKeys.GROUPS,
                    GraphFileKeys.HIGHSCHOOLCOMPLETION,
                    GraphFileKeys.MONEY,
                    GraphFileKeys.RETENTION,
                    GraphFileKeys.STAFF,
                    GraphFileKeys.SUSPENSIONS,
                    GraphFileKeys.SUSPENSIONSDAYSLOST,
                    GraphFileKeys.SUSPEXPINCIDENTS,
                    GraphFileKeys.TEACHERQUALIFICATIONS,
                    GraphFileKeys.TRUANCY,
                    GraphFileKeys.GWRCT,
                    GraphFileKeys.HIGHSCHOOLCOMPLETION,
                    GraphFileKeys.RETENTION,
                    GraphFileKeys.POSTGRADPLAN,
                }
            );

            if (globals.SubjectID.Key != SubjectIDKeys.AllTested || globals.Group.Key == GroupKeys.All) pages.Add(GraphFileKeys.StateTests);

            if (globals.TraceLevels == 0)
                return pages.Contains(globals.GraphFile.Key);
            else
                return true;
        }
    }
}