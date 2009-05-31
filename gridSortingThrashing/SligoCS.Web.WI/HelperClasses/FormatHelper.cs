using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.WI.WebSupportingClasses;

namespace SligoCS.Web.WI.HelperClasses
{
    /// <summary>
    /// This class contains general purpose formatting functions, which 
    /// can be reused against multiple pages.
    /// </summary>
    public class FormatHelper
    {

        public void SetRaceAbbr(SligoDataGrid grid, GridViewRow row, string dataField)
        {
            int raceColIndex = grid.FindBoundColumnIndex(dataField);
            if ((raceColIndex >= 0) && (grid.Columns[raceColIndex].Visible))
            {
                string raceVal = GetRaceAbbr(row.Cells[raceColIndex].Text);
                row.Cells[raceColIndex].Text = raceVal;
            }
        }

        public void SetRaceAbbr(WinssDataGrid grid, GridViewRow row, String fieldName)
        {
            int raceColIndex = grid.FindBoundColumnIndex(fieldName);
            if ((raceColIndex >= 0) && (grid.Columns[raceColIndex].Visible))
            {
                string raceVal = GetRaceAbbr(row.Cells[raceColIndex].Text);
                row.Cells[raceColIndex].Text = raceVal;
            }
        }

        public string GetRaceAbbr(string longRaceName)
        {
            //Bug 671:  abbreviate Race/Ethnicity label
            string raceVal = longRaceName.Replace("American Indian/Alaskan Native", "Amer Indian");
            raceVal = raceVal.Replace("Asian/Pacific Islander", "Asian");
            raceVal = raceVal.Replace("Black Not Hispanic", "Black Not Hisp");
            raceVal = raceVal.Replace("Hispanic", "Hispanic");
            raceVal = raceVal.Replace("White Not Hispanic", "White Not Hisp");
            return raceVal;
        }
    }
}
