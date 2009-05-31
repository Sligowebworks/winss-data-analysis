using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class WRCTPerformance : PageBaseWI
    {
        protected v_WSAS _ds = new v_WSAS();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

        }
    }
}
