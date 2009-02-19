using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebServerControls.WI;

namespace SligoCS.Web.WI.PageBase.WI
{
    /// <summary>
    /// All web pages (.aspx.cs files) from the Wisconsin web site should 
    ///     be derived from this class.
    /// </summary>
    public abstract class PageBaseWI : System.Web.UI.Page
    {
        #region class level variables
        /// <summary>
        /// For every page in the Wisconsin web site, maintain a StickyParameter object.
        /// </summary>
        private StickyParameter stickyParameter = new StickyParameter();
        #endregion

        #region public properties
        /// <summary>
        /// The public version of the StickyParameter is read-only, 
        /// so it cannot be set by derived classes,
        ///  but it's properties can be accessed.
        /// </summary>
        public StickyParameter StickyParameter 
        { 
            get 
            { 
                return stickyParameter; 
            } 
            set 
            { 
                stickyParameter = value; 
            }
        }

        public abstract DataSet GetDataSet();
        #endregion

        /// <summary>
        /// This function prepares a Business Layer Entity class prior to loading a dataset.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PrepBLEntity(BLWIBase entity)
        {
            //TODO:  move these Enum statements into the StickyParameter object.
        #region Enum Conversions
            SchoolType schoolType = GetSchoolType(StickyParameter.FULLKEY, entity);
            //convert the VIEW BY string into an enumerated type
            ViewByGroup viewBy = (ViewByGroup)Enum.Parse(typeof(ViewByGroup), StickyParameter.Group);
            //convert the COMPARE TO string into an enumerated type.
            //convert FullKey to OrgLevel
            OrgLevel orgLevel = ParamsHelper.GetOrgLevel(StickyParameter);
            //end TODO block --djw 11/21/07

            CompareTo compareTo = (CompareTo)Enum.Parse(typeof(CompareTo), StickyParameter.CompareTo);
            S4orALL s4orALL = (S4orALL)Enum.Parse(typeof(S4orALL), StickyParameter.S4orALL.ToString());
            SRegion sRegion = (SRegion)Enum.Parse(typeof(SRegion), StickyParameter.SRegion.ToString());
        #endregion

            //get the list of selected schools when user clicks Compare To: Selected Schools
            //List<string> compareToSchoolsOrDistrict = GetCompareToSchools();

            //set the entity's properties
            entity.SchoolType = schoolType;
            entity.ViewBy = viewBy;            
            entity.OrigFullKey = StickyParameter.FULLKEY;
            entity.CompareTo = compareTo;
            entity.S4orALL = s4orALL;
            entity.OrgLevel = orgLevel;
            entity.SRegion = sRegion;

            entity.SCounty = StickyParameter.SCounty;
            entity.SAthleticConf = StickyParameter.SAthleticConf;
            entity.SCESA = StickyParameter.SCESA;

            if (orgLevel == OrgLevel.School)
            {
                entity.CompareToSchoolsOrDistrict = SligoCS.BL.WI.Utilities.BLUtil.GetFullKeyList
                    (StickyParameter.SSchoolFullKeys);
        }
            else if (orgLevel == OrgLevel.District)
            {
                entity.CompareToSchoolsOrDistrict = SligoCS.BL.WI.Utilities.BLUtil.GetFullKeyList
                    (StickyParameter.SDistrictFullKeys);
            }
            else
            {
                entity.CompareToSchoolsOrDistrict = new List<string>();
            }

        }

        #region protected functions

        /// <summary>
        /// If STYP is set in the querystring, use that. 
        /// Otherwise, if the Fullkey is provided and matches a single school, do a
        ///     db lookup to determine the STYP,
        /// Otherwise, use the StickyParameter default STYP (3).
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        private SchoolType GetSchoolType(string fullKey, BLWIBase entity)
        {
            bool found = false;            
            string tmp = string.Empty;
            if ((HttpContext.Current != null) && (HttpContext.Current.Request != null))
            {
                tmp = Request.QueryString[StickyParameter.QStringVar.STYP.ToString()];
            }
            if ((tmp != null) && (tmp != string.Empty) && (tmp.Trim().Length != 0))
            {
                found = true;
            }
            else
            {
                BLSchool school = new BLSchool();                 
                v_Schools ds = school.GetSchool(fullKey, entity.CurrentYear);
                if (ds._v_Schools.Count == 1)
                {
                    tmp = ds._v_Schools[0].schooltype;
                    found = true;
                }
            }

            if (!found)
            {
                tmp = StickyParameter.STYP.ToString();
            }

            SchoolType retval = (SchoolType)Enum.Parse(typeof(SchoolType), tmp);

            return retval;
        }

        /// <summary>
        /// overload, Get Org Name by fullkey and org level
        /// </summary>
        /// <param name="orgLevel"></param>
        /// <returns></returns>
        public string GetOrgName(OrgLevel orgLevel)
        {
            string retval = string.Empty;
            if (stickyParameter != null)
                retval = GetOrgName(orgLevel, stickyParameter.FULLKEY);
            return retval;
        }        

        /// <summary>
        /// Get Org Name by fullkey and org level
        /// </summary>
        /// <param name="orgLevel"></param>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        public string GetOrgName(OrgLevel orgLevel, string fullKey)
        {
            //TODO:  implement a db call here based off FullKey.
            string retval = string.Empty;
            if (orgLevel == OrgLevel.School)
            {
                BLSchool school = new BLSchool();
                retval = school.GetSchoolName(fullKey, school.CurrentYear);
            }
            else if (orgLevel == OrgLevel.District)
            {
                BLDistrict dist = new BLDistrict();
                retval = dist.GetDistrictName(fullKey, dist.CurrentYear);
            }
            else
            {
                //(OrgLevel == OrgLevel.State)
                retval = "Entire State";
            }
            return retval;

        }

        /// <summary>
        /// Overload.  Determines a graph's title based off current context.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTitle(
            string typeOfData,
            BLWIBase entity)
        {
            return GetTitle ( typeOfData, entity, "");
        }

        /// <summary>
        /// Get Title for graph and table, 
        /// Determines a graph's title based off current context
        /// </summary>
        /// <param name="typeOfData"></param>
        /// <param name="entity"></param>
        /// <param name="regionString"></param>
        /// <returns></returns>
        protected virtual string GetTitle(
            string typeOfData,
            BLWIBase entity,
            string regionString)
        {
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
            //    //District Name  - Elem Schools                                                   
            //    //2005-06 Compared to Prior Years                                                 

            //    //Bottom line is that graph and table titles should completely describe the
            //    //options selected.  sometimes I think we have left off the school type and the
            //    //compare to options when this information is important for understanding the
            //    //data in the graph or table.    
            
            string orgName = GetOrgName(entity.OrgLevel);

            return typeOfData +
                    GetViewByInTitle(entity.ViewBy) +
                    GetOrgNameInTitle(orgName) +
                    GetYearRangeInTitle(entity.Years) +
                    GetCompareToInTitle(entity.OrgLevel,
                                            entity.CompareTo,
                                            entity.SchoolType,
                                            entity.S4orALL,
                                            regionString) +
                    GetSchoolTypeInTitle((BLWIBase)entity);

        }

        /// <summary>
        /// Get ViewBy In Title
        /// </summary>
        /// <param name="viewBy"></param>
        /// <returns></returns>
        public string GetViewByInTitle( ViewByGroup viewBy)
        {
            StringBuilder sb = new StringBuilder();

            //Bug 513 Comment #5
            if (viewBy != ViewByGroup.AllStudentsFAY)
            {
                sb.AppendFormat(" by {0}<br/>", BLWIBase.Convert(viewBy));
            }
            else 
            {
                sb.AppendFormat(" - {0}<br/>", BLWIBase.Convert(viewBy));
            }

            return sb.ToString();
        }

        public string GetOrgNameInTitle( string orgName )
        {
            return orgName + "<br/>";
        }

        /// <summary>
        /// Get Year Range In Title
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public string GetYearRangeInTitle( List<int> years)
        {
            StringBuilder sb = new StringBuilder();

            if (years.Count == 1)
            {
                //if we ran the query on 2007, then display 2005-06
                //See bug 513 Comment #3
                int priorYear = years[0] - 1;
                years.Insert(0, priorYear);
            }

            if (years.Count == 2)
            {
                if (years[0] == years[1])
                {
                    sb.Append(years[0]);
                }
                else
                {
                    //Rewrite_QA_101308_Part2.doc  4. ACT d
                    //sb.AppendFormat("{0}-{1}", years[0], years[1].ToString().Substring(2));
                    sb.AppendFormat("{0}-{1}", (years[1]-1), years[1].ToString().Substring(2));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get CompareTo In Title
        /// </summary>
        /// <param name="orgLevel"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <param name="s4orALL"></param>
        /// <param name="regionString"></param>
        /// <returns></returns>
        public string GetCompareToInTitle(
            OrgLevel orgLevel,
            CompareTo compareTo,
            SchoolType schoolType,
            S4orALL s4orALL,
            string regionString)
        {
            StringBuilder sb = new StringBuilder();

            if (compareTo == CompareTo.SELSCHOOLS)
            {
                if (s4orALL == S4orALL.AllSchoolsOrDistrictsIn)
                {
                    if (orgLevel == OrgLevel.School)
                    {
                        sb.AppendFormat(" Compared to All {0} in {1} ",
                        BLWIBase.Convert(schoolType), regionString);
                    }
                    else if (orgLevel == OrgLevel.District)
                    {
                        sb.AppendFormat(" Compared to All Districts in {0} ", regionString);
                    }
                }
                else
                {
                    if (orgLevel == OrgLevel.School)
                    {
                        sb.AppendFormat(" Compared to Selected {0} ", BLWIBase.Convert(schoolType));
                    }
                    else if (orgLevel == OrgLevel.District)
                    {
                        sb.AppendFormat(" Compared to Selected Districts");
                    }
                }
            }
            else
            {
                if (compareTo != CompareTo.CURRENTONLY)
                {
                    sb.AppendFormat(" Compared to {0}", BLWIBase.Convert(compareTo));
                }
            }

            return sb.ToString();
        }

        //Notes for graph

        /// <summary>
        /// Get SchoolType In Title
        /// </summary>
        /// <param name="orgLevel"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <param name="s4orALL"></param>
        /// <returns></returns>
        public string GetSchoolTypeInTitle(BLWIBase blBase)
        {
            String schoolTypeInTitle = blBase.GetSchoolType();
            if (!String.IsNullOrEmpty(schoolTypeInTitle))
            {
                schoolTypeInTitle = " <br/> " + schoolTypeInTitle + " ";
            }

            return schoolTypeInTitle;
        }

          protected enum CommonColumnNames
        {
            YearFormatted,
            SchooltypeLabel,
            RaceLabel,
            SexLabel,
            GradeLabel,
            DisabilityLabel,
            EconDisadvLabel,
            ELPLabel,
            //OrgLevelLabel,
            District_Name,
            LinkedName

        }

        /// <summary>
        /// Since the rules for showing/hiding columns are consistent across pages for most common columns, 
        /// just override / extend this function for your special columns.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="ds"></param>
        /// <param name="viewBy"></param>
        /// <param name="orgLevel"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        protected void SetVisibleColumns2(SligoDataGrid grid, DataSet ds,
            ViewByGroup viewBy,
            OrgLevel orgLevel,
            CompareTo compareTo,
            SchoolType schoolType)
        {
            grid.SetAllBoundColumnsVisible(false);

            List<string> visibleCols = GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            foreach (string colName in visibleCols)
            {
                grid.SetBoundColumnVisible(colName, true);
            }
        }
       
        public virtual List<string> GetVisibleColumns(ViewByGroup viewBy,
            OrgLevel orgLevel,
            CompareTo compareTo,
            SchoolType schoolType)
        {

            List<string> retval = new List<string>();

            ////Notes for graph : Can't reuse this function in graph, because the visible column is used in the Grade Display(Grade Column Name)
            //the grap need the Data Set Column Name, there is a little difference betwen theem. So can't reuse it in graph
            //if (String.Empty != BLWIBase.GetColumnName(viewBy))
            //    retval.Add(BLWIBase.GetColumnName(viewBy));
            //if (String.Empty != BLWIBase.GetColumnName(compareTo))
            //    retval.Add(BLWIBase.GetColumnName(compareTo));
            //if (String.Empty != BLWIBase.GetColumnName(orgLevel, schoolType))
            //    retval.Add(BLWIBase.GetColumnName(orgLevel, schoolType));
            if (viewBy == ViewByGroup.RaceEthnicity)
                retval.Add(CommonColumnNames.RaceLabel.ToString());

            if (viewBy == ViewByGroup.Gender)
                retval.Add(CommonColumnNames.SexLabel.ToString());

            if (viewBy == ViewByGroup.Grade)
                retval.Add(CommonColumnNames.GradeLabel.ToString());

            if (viewBy == ViewByGroup.Disability)
                retval.Add(CommonColumnNames.DisabilityLabel.ToString());

            if (viewBy == ViewByGroup.EconDisadv)
                retval.Add(CommonColumnNames.EconDisadvLabel.ToString());

            if (viewBy == ViewByGroup.ELP)
                retval.Add(CommonColumnNames.ELPLabel.ToString());



            //Note that when "OrgLevel <> SC" and "Schooltype = 1 (all types)", we add
            //another column, SchooltypeLabel, to label the schooltype itemization...
            if ((orgLevel != OrgLevel.School) && (schoolType == SchoolType.AllTypes))
            {
                retval.Add(CommonColumnNames.SchooltypeLabel.ToString());
            }

            if (compareTo == CompareTo.DISTSTATE)
                retval.Add(CommonColumnNames.District_Name.ToString().Replace("_", " "));

            if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                retval.Add(CommonColumnNames.LinkedName.ToString());
            }


            //only show Year column when CompareTo == Prior Years
            if (compareTo == CompareTo.PRIORYEARS)
                retval.Add(CommonColumnNames.YearFormatted.ToString());

            return retval;
        }

        /// <summary>
        /// Checks OrgLevel and sets HyperLink Text and URL accordingly. Sets Visibility too.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ChangeSelectedSchoolOrDistrict"></param>
        protected virtual void SetLinkChangeSelectedSchoolOrDistrict(
            BLWIBase entity,
            HyperLink ChangeSelectedSchoolOrDistrict)
        {
            ChangeSelectedSchoolOrDistrict.Visible = false;

            if ((entity != null) && (entity.CompareTo == CompareTo.SELSCHOOLS))
            {
                string qs = ParamsHelper.GetQueryString(StickyParameter, string.Empty, string.Empty);

                if (entity.OrgLevel == OrgLevel.School)
                {
                    ChangeSelectedSchoolOrDistrict.Text = "Change selected schools";
                    ChangeSelectedSchoolOrDistrict.NavigateUrl = "~/selMultiSchools.aspx" + qs;
                    ChangeSelectedSchoolOrDistrict.Visible = true;
                }

                if (entity.OrgLevel == OrgLevel.District)
                {
                    ChangeSelectedSchoolOrDistrict.Text = "Change selected districts";
                    ChangeSelectedSchoolOrDistrict.NavigateUrl = "~/selMultiDistricts.aspx" + qs;
                    ChangeSelectedSchoolOrDistrict.Visible = true;
                }
            }
        }

        /// <summary>
        /// Force GraphPanel Visibility = true, except in case of S4orALL.AllSchoolsOrDistrictsIn.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="GraphPanel"></param>
        /// <returns></returns>
        protected virtual bool CheckIfGraphPanelVisible(
                BLWIBase entity,
                Panel GraphPanel)
        {
            bool result = true;
            GraphPanel.Visible = true;

            if ((entity != null) && (entity.CompareTo == CompareTo.SELSCHOOLS))
            {

                if (entity.S4orALL == S4orALL.AllSchoolsOrDistrictsIn)
                {
                    GraphPanel.Visible = false;
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Redirects the user if conditions are not met for Selected School (/District) Page View.
        /// </summary>
        /// <param name="entity"></param>
        protected void CheckSelectedSchoolOrDistrict(BLWIBase entity)
        {
            if (entity == null ||
                entity.CompareTo != CompareTo.SELSCHOOLS ||
                entity.S4orALL != S4orALL.FourSchoolsOrDistrictsIn ||
                IsSchoolsDistrictsSelected() == true ||
                Request.QueryString["B2G"] == "1" ||
                entity.OrgLevel == OrgLevel.State)
            {
                return;
            }

            string selectedFullkeys;
            selectedFullkeys = StickyParameter.SelectedFullkeys(entity.OrgLevel);
            RedirectIfFullkeysEmpty(selectedFullkeys, entity);
        }

        private void RedirectIfFullkeysEmpty(string Fullkeys, BLWIBase entity)
        {
            if (Fullkeys != string.Empty) return;
            //else, we have work to do.

            string qs = string.Empty;
            string filedest = string.Empty;

            switch (entity.OrgLevel)
            {
                case OrgLevel.District: filedest = "~/selMultiDistricts.aspx"; break;
                case OrgLevel.School: filedest = "~/selMultiSchools.aspx"; break;
                default: throw new Exception("Invalid OrgLevel for Selected Fullkeys Redirect");
            }
            qs = ParamsHelper.GetQueryString(StickyParameter, string.Empty, string.Empty);
            string NavigateUrl = filedest + qs;
            Response.Redirect(NavigateUrl, true);
        }

        /// <summary>
        /// Tests for readyness of user state for Compare To Selected.... Uses StickyParameter and tests for Fullkeys when "Four In" and tests for appropriate Athletic Conference, CESA, or Count, ortherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsSchoolsDistrictsSelected()
        {
            bool result = false;

            S4orALL s4orAll = (S4orALL)Enum.Parse(typeof(S4orALL),
                StickyParameter.S4orALL.ToString() );

            if (s4orAll == S4orALL.FourSchoolsOrDistrictsIn)
            {
                if (StickyParameter.ORGLEVEL == OrgLevel.School.ToString() &&
                    String.IsNullOrEmpty(StickyParameter.SSchoolFullKeys) == false)
                {
                    result = true;
                }
                if (StickyParameter.ORGLEVEL == OrgLevel.District.ToString() &&
                    String.IsNullOrEmpty(StickyParameter.SDistrictFullKeys) == false)
                {
                    result = true;
                }
            }

            if (s4orAll == S4orALL.AllSchoolsOrDistrictsIn)
            {
                SRegion sRegion = (SRegion)Enum.Parse(typeof(SRegion),
                    StickyParameter.SRegion.ToString());

                if (sRegion == SRegion.AthleticConf &&
                    String.IsNullOrEmpty(StickyParameter.SAthleticConf) == false)
                {
                    result = true;
                }
                if (sRegion == SRegion.CESA &&
                    String.IsNullOrEmpty(StickyParameter.SCESA) == false)
                {
                    result = true;
                }
                if (sRegion == SRegion.County &&
                    String.IsNullOrEmpty(StickyParameter.SCounty) == false)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Overrides DataGrid Row Labels when
        /// ViewBy == All Students  AND CompareTo == District/State, then change row labels.
        /// See bug #513 comment #11.
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SetOrgLevelRowLabels(BLWIBase entity, SligoDataGrid grid, GridViewRow row)
        {
            //if ((entity != null) && (entity.ViewBy == ViewByGroup.AllStudentsFAY) && (entity.CompareTo == CompareTo.DISTSTATE))
            if ((entity != null) && (entity.CompareTo == CompareTo.DISTSTATE))
            {
                //set the disply name:  see 
                int colIndex = grid.FindBoundColumnIndex(CommonColumnNames.District_Name.ToString().Replace("_", " "));
                if ((colIndex >= 0) && (row.DataItem != null))
                {
                    DataRowView dataRow = (DataRowView)row.DataItem;
                    const string ORGLEVELLABEL = "OrgLevelLabel";

                    //look at the row's OrgLevelLabel to determine how to display the Row Label.
                    if ((dataRow.Row.Table.Columns.Contains(ORGLEVELLABEL)) && (dataRow[ORGLEVELLABEL] != DBNull.Value))
                    {
                        string orgLevelLabelValue = dataRow[ORGLEVELLABEL].ToString().ToLower();
                        if (orgLevelLabelValue.Contains("school"))
                        {
                            //Bug 513 Comment #11: school row label should appear like:
                            //    Current School
                            row.Cells[colIndex].Text = "Current School";
                        }
                        else if (orgLevelLabelValue.Contains("district"))
                        {
                            //Bug 513 Comment #11: district row label should appear like:
                            //    Milwaukee: Hi Schools

                            // remove School type per DPI fix request 'rewrite_QA_091408-OnlyNew.doc' 
                            //row.Cells[colIndex].Text += ": " + BLWIBase.Convert( entity.SchoolType) + " Schools";
                            // use 'District' instead of specific district name per second round DPI fix request 'r
                            row.Cells[colIndex].Text = "District: " + entity.GetSchoolType();
                        }
                        else if (orgLevelLabelValue.Contains("state"))
                        {
                            //Bug 513 Comment #11: state row label should appear like:
                            //      State: Hi Schools
                            
                            // remove School type per DPI fix request 'rewrite_QA_091408-OnlyNew.doc' 
                            //row.Cells[colIndex].Text = "State: " + BLWIBase.Convert(entity.SchoolType) + " Schools";
                            row.Cells[colIndex].Text = "State: " + entity.GetSchoolType();                            
                        }
                    }

                }


            }


        }

        /// <summary>
        /// Performs Is Nul Check on Session var and returns an empty string when null.
        /// </summary>
        /// <param name="SessionKey"></param>
        /// <returns></returns>
        protected string GetStringFromSession(string SessionKey)
        {
            string result = string.Empty;
            if (HttpContext.Current.Session[SessionKey] != null)
            {
                result = HttpContext.Current.Session[SessionKey].ToString();
            }
            return result;
        }

        /// <summary>
        /// Abstracts retrieving Name Strings for County, Athletic Conf and CESA.
        /// </summary>
        /// <returns></returns>
        protected string GetRegionString()
        {
            string result = string.Empty;
            BLAgencyFull agencyBL = new BLAgencyFull();
            BLAthleticConf acBL = new BLAthleticConf();

            if (StickyParameter.SRegion == (int) SRegion.County
                 && !string.IsNullOrEmpty (StickyParameter.SCounty) )
            {
                result = agencyBL.GetCountyNameByID
                    (StickyParameter.SCounty).Trim();
            } 
            else if (StickyParameter.SRegion == (int) SRegion.AthleticConf
                 && !string.IsNullOrEmpty(StickyParameter.SAthleticConf))
            {
                result = acBL.GetAthleticConfNameByID
                    (int.Parse(StickyParameter.SAthleticConf)).Trim() 
                    + " Athletic Conference";
            }
            else if (StickyParameter.SRegion == (int) SRegion.CESA
                    && !string.IsNullOrEmpty(StickyParameter.SCESA))
            {
                result = agencyBL.GetCESANameByID
                    (StickyParameter.SCESA).Trim();
            }

            return result;
        }

        /// <summary>
        /// Returns a District fullkey (District "Cd" - Code) from the District or School Fullkey in the StickyParameter object.
        /// </summary>
        /// <returns></returns>
        public string GetDistrictCdByFullKey()
        {
            string result = string.Empty;
            if (StickyParameter.ORGLEVEL == OrgLevel.District.ToString()
                || StickyParameter.ORGLEVEL == OrgLevel.School.ToString())
            {
                result = StickyParameter.FULLKEY.Substring(2, 4);
            }
            return result;
        }

        /// <summary>
        /// Returns a School fullkey ("Cd" - Code) from the Fullkey in the StickyParameter object.
        /// </summary>
        /// <returns></returns>
        public string GetSchoolCdByFullKey()
        {
            string result = string.Empty;
            if ( StickyParameter.ORGLEVEL == OrgLevel.School.ToString())
            {
                result = StickyParameter.FULLKEY.Substring(8, 4);
            }
            return result;
        }

        /// <summary>
        /// Formats schoolType based on compareTo. Returns an empty string if conditions not met.
        /// </summary>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        public string GetSchoolTypeLabel(CompareTo compareTo, SchoolType schoolType)
        {
            string retval = string.Empty;
            if ((compareTo == CompareTo.DISTSTATE)
                || (compareTo == CompareTo.SELDISTRICTS)
                || (compareTo == CompareTo.CURRENTONLY))
            {
                retval = string.Format(" - {0} Schools", BLWIBase.Convert(schoolType));
            }
            return retval;
        }
        #endregion
    }
}
