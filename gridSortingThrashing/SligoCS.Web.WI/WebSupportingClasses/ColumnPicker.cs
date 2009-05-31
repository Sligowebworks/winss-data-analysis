using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.WI.WebSupportingClasses.WI;
//for enums:
using SligoCS.DAL.WI;
using SligoCS.BL.WI;

namespace SligoCS.Web.WI.WebSupportingClasses
{
    public class ColumnPicker
    {
        private GlobalValues globals;

        public ColumnPicker()
        {
            globals = new GlobalValues();
        }

        public ColumnPicker(GlobalValues initGlobals)
        {
            globals = initGlobals;
        }

        //Notes for graph
        public virtual string GetViewByColumnName()
        {
            string retval = string.Empty;
            if ((globals.OrgLevel.Key != OrgLevelKeys.School) && (globals.STYP.Key == STYPKeys.AllTypes))
            {
                retval = CommonColumnNamesForGraph.SchooltypeLabel.ToString();
            }
            else
            {
                Dictionary<string, string> map = new Dictionary<string, string>();

                map.Add(GroupKeys.Race, CommonColumnNamesForGraph.RaceShortLabel.ToString());
                map.Add(GroupKeys.Gender, CommonColumnNamesForGraph.SexLabel.ToString());
                map.Add(GroupKeys.Grade, CommonColumnNamesForGraph.GradeShortLabel.ToString());
                map.Add(GroupKeys.Disability, CommonColumnNamesForGraph.DisabilityLabel.ToString());
                map.Add(GroupKeys.EconDisadv, CommonColumnNamesForGraph.ShortEconDisadvLabel.ToString());
                map.Add(GroupKeys.EngLangProf, CommonColumnNamesForGraph.ShortELPLabel.ToString());
                //Only need a column which has the same value in all rows and is not null
                //This column has no meaning in the grid(table) display, just used for graph
                map.Add(GroupKeys.All, retval = CommonColumnNamesForGraph.SexLabel.ToString());
                if (map.ContainsKey(globals.Group.Key))
                    retval = map[globals.Group.Key];
                else 
                    retval = String.Empty;
            }
            return retval;
        }

        //Notes For Graph
        public virtual string GetCompareToColumnName()
        {
            string retval = string.Empty;
            
            if (globals.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                retval = CommonColumnNamesForGraph.OrgLevelLabel.ToString();
            }

            if (globals.CompareTo.Key == CompareToKeys.SelSchools || globals.CompareTo.Key == CompareToKeys.SelDistricts
                || globals.CompareTo.Key ==  CompareToKeys.Current)
            {
                if (globals.OrgLevel.Key == OrgLevelKeys.District)
                    {
                        retval = CommonColumnNamesForGraph.District_Name.ToString().Replace("_", " ");
                    }
                    else if (globals.OrgLevel.Key == OrgLevelKeys.School)
                    {
                        retval = CommonColumnNamesForGraph.School_Name.ToString().Replace("_", " ");
                    }
            }
            
            if (globals.CompareTo.Key == CompareToKeys.Years)
            {
                retval = CommonColumnNamesForGraph.YearFormatted.ToString();
            }

            return retval;
        }
    }

    //Notes for graph
    public enum CommonColumnNamesForGraph
    {
        YearFormatted,
        SchooltypeLabel,
        RaceShortLabel,
        SexLabel,
        GradeShortLabel,
        DisabilityLabel,
        ShortEconDisadvLabel,
        ShortELPLabel,
        OrgLevelLabel,
        District_Name,
        School_Name,
        AllStudents
    }
}
