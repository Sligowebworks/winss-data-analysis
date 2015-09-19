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
        //Notes for graph
        public static string GetViewByColumnName(GlobalValues globals)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            map.Add(GroupKeys.Race, CommonGraphNames.RaceShortLabel.ToString());
            map.Add(GroupKeys.RaceGender, CommonGraphNames.RaceShortLabel.ToString());
            map.Add(GroupKeys.Gender, CommonGraphNames.SexLabel.ToString());
            map.Add(GroupKeys.Grade, CommonGraphNames.GradeShortLabel.ToString());
            map.Add(GroupKeys.Disability, CommonGraphNames.DisabilityLabel.ToString());
            map.Add(GroupKeys.EconDisadv, CommonGraphNames.ShortEconDisadvLabel.ToString());
            map.Add(GroupKeys.EngLangProf, CommonGraphNames.ShortELPLabel.ToString());
            map.Add(GroupKeys.Migrant, CommonGraphNames.MigrantLabel.ToString());
            //Only need a column which has the same value in all rows and is not null
            //This column has no meaning in the grid(table) display, just used for graph
            map.Add(GroupKeys.All, CommonGraphNames.SexLabel.ToString());
        
            return 
              (map.ContainsKey(globals.Group.Key))?
               map[globals.Group.Key]
               : String.Empty;
        }

        //Notes For graph
        public static string GetCompareToColumnName(GlobalValues globals)
        {
            string retval = string.Empty;
            
            if (globals.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                retval = 
                (globals.STYP.Key == STYPKeys.AllTypes)?
                    retval = CommonGraphNames.OrgLevelLabel.ToString()
                    :retval = CommonGraphNames.OrgSchoolTypeLabel.ToString()
                ;
            }

            if (globals.CompareTo.Key == CompareToKeys.SelSchools || globals.CompareTo.Key == CompareToKeys.SelDistricts)
            {
                if (globals.OrgLevel.Key == OrgLevelKeys.District)
                    {
                        retval = CommonGraphNames.District_Name.ToString().Replace("_", " ");
                    }
                    else if (globals.OrgLevel.Key == OrgLevelKeys.School)
                    {
                        //retval = CommonGraphNames.School_Name.ToString().Replace("_", " ");
                        retval = "Name";
                    }
                // no Org Level State for compare selected
            }

            if (globals.CompareTo.Key == CompareToKeys.Current)
            {
                if (globals.Group.Key != GroupKeys.RaceGender)
                {
                    retval = CommonGraphNames.SchooltypeLabel.ToString();
                }
                else
                {
                    retval = CommonGraphNames.SexLabel.ToString();
                }
            }
            
            if (globals.CompareTo.Key == CompareToKeys.Years)
            {
                if (globals.Group.Key != GroupKeys.RaceGender)
                {
                    retval = CommonGraphNames.YearFormatted.ToString();
                }
                else
                {
                    retval = CommonGraphNames.SexLabel.ToString();
                }
            }

            return retval;
        }
        public static List<string> GetVisibleColumns(Group viewBy,
            OrgLevel orgLevel,
            CompareTo compareTo,
            STYP schoolType)
        {
            ////Notes for graph : Can't reuse this function in graph, because the visible column is used in the Grade Display(Grade Column paramName)
            //the grap need the Data Set Column paramName, there is a little difference betwen theem. So can't reuse it in graph
            List<string> retval = new List<string>();

            Dictionary<String, String> map = new Dictionary<string, string>();
            map.Add(GroupKeys.Race, CommonNames.RaceLabel.ToString());
            map.Add(GroupKeys.Gender, CommonNames.SexLabel.ToString());
            map.Add(GroupKeys.Grade, CommonNames.GradeLabel.ToString());
            map.Add(GroupKeys.Disability, CommonNames.DisabilityLabel.ToString());
            map.Add(GroupKeys.EconDisadv, CommonNames.EconDisadvLabel.ToString());
            map.Add(GroupKeys.EngLangProf, CommonNames.ELPLabel.ToString());

            //not all values result in adding a column
            if (map.ContainsKey(viewBy.Key)) retval.Add(map[viewBy.Key]);

            if (viewBy.Key == GroupKeys.RaceGender)
            {
                retval.Add(CommonNames.RaceLabel.ToString());
                retval.Add(CommonNames.SexLabel.ToString());
            }

            //Note that when "OrgLevel <> SC" and "Schooltype = 1 (all types)", we add
            //another column, SchooltypeLabel, to label the schooltype itemization...
            if ((orgLevel.Key != OrgLevelKeys.School) && (schoolType.Key == STYPKeys.AllTypes))
            {
                retval.Add(CommonNames.SchooltypeLabel.ToString());
            }

            if (compareTo.Key == CompareToKeys.OrgLevel)
            {
                if(orgLevel.Key != OrgLevelKeys.School && schoolType.Key == STYPKeys.AllTypes)
                {
                    retval.Add(CommonNames.OrgLevelLabel.ToString());
                }
                else{
                    retval.Add(CommonNames.OrgSchoolTypeLabel.ToString());
                }
            }

            if ((compareTo.Key == CompareToKeys.SelSchools || compareTo.Key == CompareToKeys.SelDistricts) || (compareTo.Key == CompareToKeys.Current))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                retval.Add(CommonNames.LinkedName.ToString());
            }

            //only show Year column when CompareToEnum == Prior Years
            if (compareTo.Key == CompareToKeys.Years)
                retval.Add(CommonNames.YearFormatted.ToString());

            return retval;
        }

        public enum CommonNames
        {
            YearFormatted,
            SchooltypeLabel,
            RaceLabel,
            SexLabel,
            GradeLabel,
            DisabilityLabel,
            EconDisadvLabel,
            ELPLabel,
            OrgLevelLabel,
            OrgSchoolTypeLabel,
            District_Name,
            LinkedName

        }
        //Notes for graph
        public enum CommonGraphNames
        {
            YearFormatted,
            SchooltypeLabel,
            RaceShortLabel,
            SexLabel,
            GradeShortLabel,
            DisabilityLabel,
            ShortEconDisadvLabel,
            ShortELPLabel,
            MigrantLabel,
            OrgLevelLabel,
            OrgSchoolTypeLabel,
            OrgSchoolTypeLabelAbbr,
            District_Name,
            School_Name
        }
}
}
