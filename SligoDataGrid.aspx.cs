using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    //private const string YEAR = "Year";
    //private const string SCHOOLTYPE = "SchoolType";
    //private const string PERCENT = "Percent";
    //private const string LMB = "LMB";

    protected void Page_Load(object sender, EventArgs e)
    {
        //DataSet ds = CreateEmptyDataset();
        //PopulateDataset(ds);

        BusinessLayer bl = new BusinessLayer();
        DataSet ds = bl.GetSampleData();
        this.SligoDataGrid1.DataTable = ds.Tables[0];
        this.SligoDataGrid1.DataGridTitle = "My Test Data";

    }



    /// <summary>
    /// Given the dataset loaded with data, prepare it for presentation by 
    /// setting the ExtendedProperties for each column in the dataset.
    /// </summary>
    /// <param name="ds"></param>
    //private void SetColumnExtendedProperties(DataSet ds)
    //{
    //    if ((ds != null) && (ds.Tables.Count > 0))
    //    {
    //        DataTable table = ds.Tables[0];

    //        if (table.Columns.Contains(YEAR))
    //        {
    //            table.Columns[YEAR].ExtendedProperties[Constants.MERGECOLUMN] = true;                
    //        }

    //        if (table.Columns.Contains(SCHOOLTYPE))
    //        {
    //            table.Columns[SCHOOLTYPE].ExtendedProperties[Constants.MERGECOLUMN] = true;
    //            table.Columns[SCHOOLTYPE].ExtendedProperties[Constants.DISPLAYNAME] = "School Type";
    //        }

    //        if (table.Columns.Contains(PERCENT))
    //        {
    //            table.Columns[PERCENT].ExtendedProperties[Constants.MERGECOLUMN] = false;
    //        }

    //        if (table.Columns.Contains(LMB))
    //        {
    //            table.Columns[LMB].ExtendedProperties[Constants.VISIBLECOLUMN] = false;
    //        }
    //    }
    //}



}
