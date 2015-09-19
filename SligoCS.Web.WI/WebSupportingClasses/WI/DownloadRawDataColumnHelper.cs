using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.DAL.WI;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    public class DownloadRawDataColumnHelper
    {
        public static SortedList<string,string> MapStandardColumnsToDownloadLabels()
        {
            SortedList<string, string> lst = new SortedList<string, string>();

            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.year , "year");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.agencykey, "agency_key");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.AgencyType, "agency_type");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.CESA, "CESA");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.District_Number, "district_number");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.School_Number, "school_number");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.SchoolTypeLabel, "school_type");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.charter, "charter");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.District_Name, "district_name");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.School_Name, "school_name");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.GradeLabel, "grade");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.RaceLabel, "race_ethnicity");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.SexLabel, "gender");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.DisabilityLabel, "disability_status");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.EconDisadvLabel, "economic_status");
            lst.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull.ELPLabel, "english_proficiency_status");
            lst.Add(v_WSAS.MigrantLabel, "migrant_status");

            return lst;
        }

        public static List<String> GetStandardColumns()
        {
            return new List<String> (new String[] {
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.year,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.agencykey,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.AgencyType,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.CESA,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.County,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.ConferenceKey,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.District_Number,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.School_Number,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.SchoolTypeLabel,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.charter,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.District_Name,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.School_Name,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.GradeLabel,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.RaceLabel,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.SexLabel,
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.DisabilityLabel, 
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.EconDisadvLabel, 
                v_Template_Keys_WWoDisEconELP_tblAgencyFull.ELPLabel,
                v_WSAS.MigrantLabel
             });
        }

        public static String NormalizeColumnName(String name)
        {
            name = name.Replace(" ", "_").ToLower();
            name = name.Replace("_pk-12", "_prek-12");
            name = name.Replace("count", "_count");
            name = name.Replace("_county", "county"); //fixes indiscriminate replace on above line
            //name = name.Replace("percent", "_percent");
            name = name.Replace("#", "number");
            name = name.Replace("%", "percent");
            name = name.Replace("(", "");
            name = name.Replace(")", "");
            name = name.Replace("*", "");
            name = name.Replace("__", "_");
            return name;
        }
    }
}
