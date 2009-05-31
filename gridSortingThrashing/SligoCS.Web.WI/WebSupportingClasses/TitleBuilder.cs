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
            BLAgencyFull agencyBL = new BLAgencyFull();
            BLAthleticConf acBL = new BLAthleticConf();

            if (GlobalValues.SRegion.Key == SRegionKeys.County
                 && !string.IsNullOrEmpty(GlobalValues.SCounty))
            {
                result = agencyBL.GetCountyNameByID
                    (GlobalValues.SCounty).Trim();
            }
            else if (GlobalValues.SRegion.Key == SRegionKeys.AthleticConf
                 && !string.IsNullOrEmpty(GlobalValues.SAthleticConf))
            {
                result = acBL.GetAthleticConfNameByID
                    (int.Parse(GlobalValues.SAthleticConf)).Trim()
                    + " Athletic Conference";
            }
            else if (GlobalValues.SRegion.Key == SRegionKeys.CESA
                    && !string.IsNullOrEmpty(GlobalValues.SCESA))
            {
                result = agencyBL.GetCESANameByID
                    (GlobalValues.SCESA).Trim();
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
            return GetTitleWithoutSchoolType(prefix, globals, qm) + GetSchoolTypeInTitle();
        }
        /// <summary>
        /// Get Title for graph and table, 
        /// Determines a graph's title based off current context
        /// </summary>
        /// <param name="prefix">Begining of the Title String, usually the major identifier of the data.</param>
        /// <param name="entity"></param>
        /// <param name="regionString"></param>
        /// <returns></returns>
        public String GetTitleWithoutSchoolType(
            string prefix,
            GlobalValues globals, QueryMarshaller qm)
        {
            String regionString = GetRegionString(globals);
            qm.InitLists();

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

            string orgName = GlobalValues.GetOrgName(globals.OrgLevel, globals.FULLKEY);

            return prefix + 
                    GetViewByInTitle(globals.Group) +
                    GetOrgNameInTitle(orgName) +
                    GetYearRangeInTitle(qm.years) +
                    GetCompareToInTitle(globals.OrgLevel,
                                            globals.CompareTo,
                                            globals.STYP,
                                            globals.S4orALL,
                                            regionString);
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
                sb.AppendFormat(" by {0}<br/>", viewBy.Key);
            }
            else
            {
                sb.AppendFormat(" - {0}<br/>", viewBy.Key);
            }

            return sb.ToString();
        }

        public static String GetOrgNameInTitle(string orgName)
        {
            return orgName + "<br/>";
        }

        /// <summary>
        /// Get Year Range In Title
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public String GetYearRangeInTitle( List<int> years)
        {
            years.Sort();
            int max = years.Count -1;
            return  String.Format("{0}-{1}", (years[max] - 1), years[max].ToString().Substring(2));
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
                        sb.AppendFormat(" Compared to Selected Districts");
                    }
                }
            }
            else
            {
                if (compareTo.Key != CompareToKeys.Current)
                {
                    sb.AppendFormat(" Compared to {0}", compareTo.Key);
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
            string retval = string.Empty;
            //if (schoolType == STYP.AllTypes)
            //    retval = "All";
            //else if (schoolType == STYP.Elem)
            //    retval = "Elementary";
            //else if (schoolType == STYP.ElSec)
            //    retval = "Elementary / Secondary";
            //else if (schoolType == STYP.Hi)
            //    retval = "High";
            //else if (schoolType == STYP.Mid)
            //    retval = "Mid/Jr Hi";
            //else if (schoolType == STYP.StateSummary)
            //    retval = "State Summary";

            if (schoolType.Key == STYPKeys.AllTypes)
                retval = "(All School Types)";
            else if (schoolType.Key == STYPKeys.Elem)
                retval = "Elem. Schools";
            else if (schoolType.Key == STYPKeys.ElSec)
                retval = "El/Sec Combined Schls";
            else if (schoolType.Key == STYPKeys.Hi)
                retval = "High Schools";
            else if (schoolType.Key == STYPKeys.Mid)
                retval = "Mid/Jr Hi Schools";
            else if (schoolType.Key == STYPKeys.StateSummary)
                retval = "Summary";
            return retval;
        }
        /// <summary>
        /// Get STYP In Title
        /// </summary>
        /// <param name="orgLevelKey"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <param name="s4orALL"></param>
        /// <returns></returns>
        public String GetSchoolTypeInTitle()
        {
            String schoolTypeInTitle = GetSchoolType(globals.STYP);
            if (!String.IsNullOrEmpty(schoolTypeInTitle))
            {
                schoolTypeInTitle = " <br/> " + schoolTypeInTitle + " ";
            }

            return schoolTypeInTitle;
        }

        //Notes for graph
        public string GetSchoolType(STYP schoolType)
        {
            StringBuilder sb = new StringBuilder();

            if (globals.CompareTo.Key == CompareToKeys.SelSchools ||
                globals.CompareTo.Key == CompareToKeys.SelDistricts)
            {
                if (globals.S4orALL.Key != S4orALLKeys.AllSchoolsOrDistrictsIn
                    && globals.OrgLevel.Key == OrgLevelKeys.District)
                {
                    sb.AppendFormat(SchoolTypeLabel(schoolType));
                }
            }
            else
            {
                if (globals.CompareTo.Key != CompareToKeys.Current)
                {
                    sb.AppendFormat(SchoolTypeLabel(schoolType));
                }
            }

            return sb.ToString();
        }

       
    }
}
