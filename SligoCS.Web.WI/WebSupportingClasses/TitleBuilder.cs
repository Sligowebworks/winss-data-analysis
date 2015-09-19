using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.Base.PageBase.WI;

namespace SligoCS.Web.WI.WebSupportingClasses
{
    public class TitleBuilder
    {
        private GlobalValues globals;
        public const String newline = "\n";

        public String Prefix = String.Empty;

        public TitleBuilder(GlobalValues GlobalValues)
        {
            globals = GlobalValues;
        }
        /// <summary>
        /// Encapsulates retrieving paramName Strings for County, Athletic Conf and CESA.
        /// </summary>
        /// <returns></returns>
        public static String GetRegionString(GlobalValues GlobalValues)
        {
            string result = string.Empty;
           
            if (GlobalValues.SRegion.Key == SRegionKeys.County
                 && !string.IsNullOrEmpty(GlobalValues.SCounty))
            {
                QueryMarshaller qm = GlobalValues.Page.QueryMarshaller;
                qm.Database = new DALALLAgencies();
                result = ((DALALLAgencies)qm.Database).GetCountyNameByID(GlobalValues.SCounty.Trim());
                
            }
            else if (GlobalValues.SRegion.Key == SRegionKeys.AthleticConf
                 && !string.IsNullOrEmpty(GlobalValues.SAthleticConf))
            {
                QueryMarshaller qm = GlobalValues.Page.QueryMarshaller;
                qm.Database = new DALAthleticConf();
                result = ((DALAthleticConf)qm.Database).GetAthleticConfNameByID(int.Parse(GlobalValues.SAthleticConf)) + " Athletic Conference";
            }
            else if (GlobalValues.SRegion.Key == SRegionKeys.CESA
                    && !string.IsNullOrEmpty(GlobalValues.SCESA))
            {
                result = (new DALALLAgencies()).GetCESANameByID(
                    (GlobalValues.SCESA).Trim());
            }
            else if (GlobalValues.SRegion.Key == SRegionKeys.Statewide)
            {
                result = " the State";
            }

            return result;
        
        }
        /// <summary>
        /// Extends GetTitle with addition of SchoolType
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="globals"></param>
        /// <param name="qm"></param>
        /// <returns></returns>
        public String GetTitle(String prefix, GlobalValues globals, QueryMarshaller qm)
        {
            #region requirements
            //[Cut and paste from Design Guide]
            //GRAPH AND TABLE TITLES                                                          
            //Titles should contain enough information so that if user has only the table or
            //only the graph they will know the options the user selected.  Otherwise users
            //will be confused and make assumptions about the data which are not accurate.    

            //General Idea                                                            
            //Here is how I'd expect most titles to appear.                                   
            //<TYPE OF DATA -- e.g. Dropout Rate> <VIEW BY OPTION-- e.g. by Gender> <OTHER
            //OPTION?>                            
            //<SCHOOL NAME - e.g. Cherokee Elem> or <DISTRICT NAME - e.g. Madison District>
            //or <Entire State>                                                         
            //<YEAR OF DATA> Compared to Prior Years                                          
            //<YEAR OF DATA -- e.g. 2002-03> Compared to Selected  Schools                    
            //<YEAR OF DATA -- e.g. 2002-03> Compared to All <SCHOOL TYPE - e.g. Elem>
            //Schools in <LOCATION -- e.g. CESA 2>>                                           
            //<YEAR OF DATA> Compared to District/State  <SCHOOL TYPE - e.g. Elem Schools>    
            //<YEAR OF DATA> Compared to State  <SCHOOL TYPE - e.g. Elem Schools>             
            //<SCHOOL TYPE> (This line needed for District and State Views if Compare To
            //Prior Years, Selected Districts, or Current Data Only are selected.  Can appear
            //next to the district name above or here below. )                                
            //If Compare to Current Data Only is picked then need the <YEAR OF DATA> on
            //another line.  

            //    //Example 1  -  What are the qualifications of teachers?                          

            //    //Here are the options selected:                                                  
            //    //School Type:  Elem                                                              
            //    //Show: Wisconsin License Status                                                  
            //    //Subject Taught:  All Subjects Combined                                          
            //    //Compare To: Prior Years                                                         

            //    //Here is the resulting title.                                                    
            //    //Teacher Qualifications - Wisconsin License Status                               
            //    //All Subjects Combined                                                           
            //    //District paramName  - Elem Schools                                                   
            //    //2005-06 Compared to Prior Years                                                 

            //    //Bottom line is that graph and table titles should completely describe the
            //    //options selected.  sometimes I think we have left off the school type and the
            //    //compare to options when this information is important for understanding the
            //    //data in the graph or table.    
            #endregion

            //capture prefix in Public Property
            Prefix = prefix;

            return  //fyi: SchoolType Label also appears in GetCompareToInTitle() logic
                GetTitleWithoutGroup(
                 prefix + GetViewByInTitle(globals.Group), globals, qm
                )
                ;
        }
        /// <summary>
        /// Build a standard Data Set Title 
        /// Will always display schooltype as Summary, for use on pages that don't suppot SchoolType
        /// </summary>
        /// <param name="prefix">Begining of the Title String, usually the major identifier of the data.</param>
        /// <param name="entity"></param>
        /// <param name="regionString"></param>
        /// <returns></returns>
        public String GetTitleForSchoolTypeUnsupported(
            string prefix,
            GlobalValues globals, QueryMarshaller qm)
        {
            String styp = String.Empty;

            //capture prefix in Public Property
            Prefix = prefix;

            if (!(globals.OrgLevel.Key == OrgLevelKeys.School
                && globals.CompareTo.Key == CompareToKeys.Years))
            {
                STYP summary = new STYP();
                summary.Value = globals.STYP.Range[STYPKeys.StateSummary];

                styp = newline + GetSchoolTypeInTitle(summary);
            }

            return
                GetTitleBase(
                    prefix + GetViewByInTitle(globals.Group), globals, qm) + styp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="globals"></param>
        /// <param name="qm"></param>
        /// <returns></returns>
        public String GetTitleWithoutGroup(String prefix, GlobalValues globals, QueryMarshaller qm)
        {
            //capture prefix in Public Property
            Prefix = prefix;

            return
               GetTitleBase(prefix, globals, qm) + newline 
               + GetSchoolTypeInTitle(globals.STYP);
        }
        public String GetTitleWithoutGroupForSchoolTypeUnsupported(String prefix, GlobalValues globals, QueryMarshaller qm)
        {
            String styp = String.Empty;
            //capture prefix in Public Property
            Prefix = prefix;

            if ( !(globals.OrgLevel.Key == OrgLevelKeys.School
                && (globals.CompareTo.Key == CompareToKeys.Years
                        || globals.CompareTo.Key == CompareToKeys.SimSchools) )
                )
            {
                STYP summary = new STYP();
                summary.Value = globals.STYP.Range[STYPKeys.StateSummary];

                styp = newline + GetSchoolTypeInTitle(summary);
            }

            return GetTitleBase(prefix, globals, qm) + styp;
        }
        private String GetTitleBase(String prefix, GlobalValues globals, QueryMarshaller qm)
        {
            String regionString = GetRegionString(globals);
            qm.InitLists();

           return  prefix + newline +
                    globals.GetOrgName() + newline +
                    GetYearRangeInTitle(qm.years) + " " + 
                            GetCompareToInTitle(globals.OrgLevel,
                                globals.CompareTo,
                                globals.STYP,
                                globals.S4orALL,
                                regionString)
                     ;
        }
        /// <summary>
        /// Get ViewBy In Title
        /// </summary>
        /// <param name="viewBy"></param>
        /// <returns></returns>
        public String GetViewByInTitle(Group viewBy)
        {
            StringBuilder sb = new StringBuilder();

            //Bug 513 Comment #5
            if (viewBy.Key != GroupKeys.All)
            {
                sb.AppendFormat(" by {0}", viewBy.Key);
            }
            else
            {
                sb.AppendFormat(" - {0}", viewBy.Key);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get Year Range In Title
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public String GetYearRangeInTitle( List<String> years)
        {
            years.Sort();
            int max = years.Count -1;
            return  String.Format("{0}-{1}", (int.Parse(years[max]) - 1), years[max].ToString().Substring(2));
        }

        /// <summary>
        /// Get CompareToEnum In Title
        /// </summary>
        /// <param name="orgLevelKey"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <param name="s4orALL"></param>
        /// <param name="regionString"></param>
        /// <returns></returns>
        public String GetCompareToInTitle(
            OrgLevel orgLevel,
            CompareTo compareTo,
            STYP schoolType,
            S4orALL s4orALL,
            string regionString)
        {
            StringBuilder sb = new StringBuilder();

            if (compareTo.Key == CompareToKeys.SelSchools ||
                compareTo.Key == CompareToKeys.SelDistricts)
            {
                if (s4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                {
                    if (orgLevel.Key == OrgLevelKeys.School)
                    {
                        sb.AppendFormat(" Compared to All {0} in {1} ",
                        SchoolTypeLabel(schoolType), regionString);
                    }
                    else if (orgLevel.Key == OrgLevelKeys.District)
                    {
                        sb.AppendFormat(" Compared to All Districts in {0} ", regionString);
                    }
                }
                else
                {
                    if (orgLevel.Key == OrgLevelKeys.School)
                    {
                        sb.AppendFormat(" Compared to Selected {0} ", SchoolTypeLabel(schoolType));
                    }
                    else if (orgLevel.Key == OrgLevelKeys.District)
                    {
                        sb.AppendFormat(" Compared to Selected Districts ");
                    }
                }
            }
            else
            {
                if (compareTo.Key == CompareToKeys.OrgLevel && orgLevel.Key == OrgLevelKeys.District)
                {
                    sb.AppendFormat(" Compared to {0} ", compareTo.Key.Replace("District/", String.Empty));
                }
                else if (compareTo.Key != CompareToKeys.Current)
                {
                    sb.AppendFormat(" Compared to {0} ", compareTo.Key);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts the enumerated type into a user friendly string.
        /// </summary>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        public  String SchoolTypeLabel(STYP schoolType)
        {
            string label = string.Empty;
            
            if (schoolType.Key == STYPKeys.AllTypes)
                label = "(All School Types)";
            else if (schoolType.Key == STYPKeys.Elem)
                label = "Elem. Schools";
            else if (schoolType.Key == STYPKeys.ElSec)
                label = "El/Sec Combined Schls";
            else if (schoolType.Key == STYPKeys.Hi)
                label = "High Schools";
            else if (schoolType.Key == STYPKeys.Mid)
                label = "Mid/Jr Hi Schools";
            else if (schoolType.Key == STYPKeys.StateSummary)
                label = "Summary - All School Types Combined";
            return label;
        }
        /// <summary>
        /// Get STYP In Title
        /// </summary>
        /// <param name="orgLevelKey"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <param name="s4orALL"></param>
        /// <returns></returns>
        public String GetSchoolTypeInTitle(STYP styp)
        {
            return 
                 (globals.OrgLevel.Key == OrgLevelKeys.School
                && ((globals.CompareTo.Key == CompareToKeys.SelDistricts || globals.CompareTo.Key == CompareToKeys.SelSchools)
                    )
                )?
                 String.Empty
                 : SchoolTypeLabel(styp);
        }

        public String GetGradeTitle()
        {
            int i;
            return
                ((int.TryParse(globals.Grade.Key, out i)) ? "Grade " : String.Empty) + globals.Grade.Key;
        }
        private static String ParseGradeCodesToLabels(int i)
        {
            if (i == 94)
                return "Grades 6-12";

            return "Grade " + (i / 4 - 4);
        }
        /// <summary>
        /// Expects a string title with whitespace, converts to a filename
        /// </summary>
        /// <param name="naturalTitle"></param>
        /// <returns></returns>
        public static String DownloadRawDataFileName(String naturalTitle)
        {
            string sep = "_";
            System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex("[\"?<>/*:.]");
            
            string title = naturalTitle.Replace(newline, sep);
            title = expr.Replace(title, String.Empty);

            title = title.Replace(" - ", sep); // don't kill hyphens in years, so only catch when surrounded in spaces
            title = title.Replace("&reg;", String.Empty);
            title = System.Web.HttpUtility.HtmlDecode(title);
            //title = title.Replace("&#37; ", "Percent_");
            title = title.Replace("%", "Percent" + sep);
            //title = title.Replace("&amp;", "And");
           title = title.Replace("&", "And");
            System.Text.RegularExpressions.Regex regx = new System.Text.RegularExpressions.Regex(@"\s");
            title = regx.Replace(title, sep);
            while (title.IndexOf(sep + sep) > -1)
            {
                title = title.Replace(sep + sep, sep);
            }

            //chop if greater than MS Office maximum filename length (259 chars minus ".csv" = 250)
            if (title.Length > 250)
                title = title.Substring(0, 250);
            
            return title + ".csv";
        }
       
    }
}
