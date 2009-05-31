using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.Base.WebServerControls.WI;

namespace SligoCS.Web.Base.PageBase.WI
{
    public delegate Boolean DelegateTest(Control ctrl);
    public delegate void ControlDelegate(Control arg);

    /// <summary>
    /// All web pages (.aspx.cs files) from the Wisconsin web site should 
    ///     be derived from this class.
    /// </summary>
    public abstract class PageBaseWI: PageBaseSligo
    {
        protected global::System.Web.UI.WebControls.HyperLink ChangeSelectedSchoolOrDistrict;
        
        #region private members
        private GlobalValues userValues;
        private GlobalValues globalValues;
        private QueryMarshaller queryMarshaller;
        private ColumnPicker columnPicker;
        private TitleBuilder titleBuilder;

//        private BLWIBase entity;
        private DALWIBase database;
        private DataSet dataset;
        private GridView datagrid;
        private Panel graphPanel;
        #endregion //  private members

        #region abstract initializers
        //commented out until ready to implement in each child page....
        //protected abstract BLWIBase InitEntity();// { BLWIBase e = new BLACT(); PrepBLEntity(e); return e;}
        //protected abstract void InitGraphPanel(Panel p);
        protected virtual DALWIBase InitDatabase() { return null;}
        protected virtual DataSet InitDataSet() { return new DataSet(); }
        protected virtual GridView InitDataGrid() { return new GridView(); }
        
        protected virtual String SetPageHeading() { return String.Empty; }
        #endregion //abstract initializers

        #region event types
        public delegate void CheckPrerequisitesHandler( PageBaseWI page, EventArgs args);
        /// <summary>
        /// Before the Load Event, intercept invalid parameter cases. Handlers should use the OnRedirectUser Event
        /// </summary>
        public event CheckPrerequisitesHandler  OnCheckPrerequisites;

        public delegate void RedirectUserHandler();
        public event RedirectUserHandler OnRedirectUser;

        #endregion // events

        #region Execution Control
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            OnCheckPrerequisites += CheckSelectedSchoolOrDistrict;
            InitComplete += OrgLevelCompareToOverride;

            userValues = new GlobalValues();
            //userValues.InitializeUserInput();
            globalValues = new GlobalValues();
            //globalValues.InitializeUserInput();
            queryMarshaller = new QueryMarshaller(globalValues);
            columnPicker = new ColumnPicker(globalValues);
            titleBuilder = new TitleBuilder(globalValues);

            Database = InitDatabase();
            DataSet = InitDataSet();
            DataGrid = InitDataGrid();


            if (Database != null) Database.DataSet = dataset;
            if (QueryMarshaller != null) QueryMarshaller.Database = Database;
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            // run prerequisite checks
            if (OnCheckPrerequisites != null) OnCheckPrerequisites(this, EventArgs.Empty);

            // if prerequisite checks registered any redirects...
            if (OnRedirectUser != null) OnRedirectUser();
        }

        protected override void OnLoad(EventArgs e)
        {
            Page.Master.EnableViewState = false;
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading( SetPageHeading());

            SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);

            if (Database != null)
            {
               QueryMarshaller.Database.DataSet = QueryMarshaller.Query(Database);
            }

            //actually raises the Load Event, so child Pages' handler is not executed until this is called.
            base.OnLoad(e);

            OnDataBindTable(); //must be called after Page Load has been
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
        
            VisitControls(Page.Controls,
                delegate(Control ctrl) { return (ctrl is HyperLinkPlus); },
                    setNavigationlinkURL
             );

            if (ChangeSelectedSchoolOrDistrict != null)
                SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);
        }
        public void ChangeSelectedSchoolOrDistrict_Load(Object link, EventArgs e)
        {
            ChangeSelectedSchoolOrDistrict = (HyperLink)link;
        }
        protected virtual void OnDataBindTable()
        {
            if (typeof(WinssDataGrid).IsInstanceOfType(DataGrid))
                 DataBindTable(DataGrid);
        }
        #endregion //execution control

        #region public properties
        public DataSet DataSet
        {
            get { return dataset; }
            set { dataset = value; }
        }
        public DALWIBase Database
        {
            get { return database; }
            set { database = value; }
        }
        public Panel GraphPanel
        {
            get { return graphPanel; }
            set { graphPanel = value; }
        }
        /// <summary>
        /// Wraps the Querystring and generally represents user-input without derived application state.
        /// Particularly for use in prerequisites check.
        /// </summary>
        public GlobalValues UserValues
        {
            get { return userValues; }
            set { userValues = value; }
        }

        /// <summary>
        /// For every page in the Wisconsin web site, maintain a GlobalValues object.
        /// </summary>
        public GlobalValues GlobalValues 
        { 
            get { return globalValues; } 
            set {  globalValues = value; }
        }

        public ColumnPicker ColumnPicker
        {
            get { return columnPicker; }
            set { columnPicker = value; }
        }

        public QueryMarshaller QueryMarshaller
        {
            get { return queryMarshaller; }
            set { queryMarshaller = value;}
        }

        public TitleBuilder TitleBuilder
        {
            get { return titleBuilder; }
            set { titleBuilder = value; }
        }
        public GridView DataGrid
        {
            get { return datagrid; }
            set { datagrid = value; }
        }

        #endregion //public properties

        #region legacy dungeon
        public virtual DataSet GetDataSet() { return DataSet; }

        /// <summary>
        /// Checks OrgLevel and sets HyperLink Text and URL accordingly. Sets Visibility too.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ChangeSelectedSchoolOrDistrict"></param>
        protected virtual void SetLinkChangeSelectedSchoolOrDistrict(HyperLink ChangeSelectedSchoolOrDistrict)
        {  // implemented in Control, ChangeSelectedSchoolOrDistrictLink
        }

        /// <summary>
        /// This function prepares a Business Layer Entity class prior to loading a dataset.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PrepBLEntity(BLWIBase entity)
        {
            //get the list of selected schools when user clicks Compare To: Selected Schools
            //List<string> compareToSchoolsOrDistrict = GetCompareToSchools();

            //set the entity's properties
            entity.ViewBy = UserValues.Group;
            entity.OrigFullKey = UserValues.FULLKEY;
            entity.S4orALL = UserValues.S4orALL;
            entity.OrgLevel = UserValues.OrgLevel;
            entity.SRegion = UserValues.SRegion;

            entity.SCounty = UserValues.SCounty;
            entity.SAthleticConf = UserValues.SAthleticConf;
            entity.SCESA = UserValues.SCESA;

            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                entity.CompareToSchoolsOrDistrict = FullKeyUtils.ParseFullKeyString
                    (UserValues.SSchoolFullKeys);
            }
            else if (UserValues.OrgLevel.Key == OrgLevelKeys.District)
            {
                entity.CompareToSchoolsOrDistrict = FullKeyUtils.ParseFullKeyString
                    (UserValues.SDistrictFullKeys);
            }
            else
            {
                entity.CompareToSchoolsOrDistrict = new List<string>();
            }
        }

        #endregion //legacy dungeon

        #region Routines
        public virtual void DataBindTable(GridView grid)
        {
            grid.DataSource = DataSet.Tables[0].DefaultView;
            if (grid.DataSource != null) grid.DataBind();
        }
        public void VisitControls(ControlCollection list, DelegateTest isTarget, ControlDelegate visit)
        {  
            foreach (Control member in list)
            {
                if (member.HasControls()) 

                    VisitControls(member.Controls, 
                        isTarget, 
                        visit
                        );
                
                if (isTarget(member))
                    
                    visit(member);
           }
        }

        /// <summary>
        /// Encapsulates and passes the current GlobalValues object to the static method, TitleBuilder.GetTitle().
        /// </summary>
        /// <param name="prefix">Begining of the Title String, usually the major identifier of the data.</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual String GetTitle(
            string beginTitle,
            BLWIBase NOTUSED)
        {
            return GetTitle(beginTitle);
        }
        protected virtual String GetTitle(String beginTitle)
        {
            return TitleBuilder.GetTitle(beginTitle, GlobalValues, QueryMarshaller);
        }

        /// <summary>
        /// Passes state [GlobalValues object] to the static method, TitleBuilder.GetRegionString()
        /// </summary>
        /// <returns></returns>
        protected String GetRegionString()
        {
            return TitleBuilder.GetRegionString(GlobalValues);
        }

          public enum CommonColumnNames
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
            District_Name,
            LinkedName

        }

        /// <summary>
        ///Deprecated for pages that use WinssDataGrid. For legacy support of SligoDataGrid Only.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="ds"></param>
        /// <param name="viewBy"></param>
        /// <param name="orgLevelKey"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        protected void SetVisibleColumns2(SligoDataGrid grid, DataSet ds, Group gr,   OrgLevel ol, CompareTo ct, STYP st)
        {
           grid.SetAllBoundColumnsVisible(false);

            List<string> visibleCols = GetVisibleColumns(GlobalValues.Group, GlobalValues.OrgLevel, GlobalValues.CompareTo, GlobalValues.STYP);

            foreach (string colName in visibleCols)
            {
                grid.SetBoundColumnVisible(colName, true);
            }
        }

        public virtual List<string> GetVisibleColumns(Group viewBy,
            OrgLevel orgLevel,
            CompareTo compareTo,
            STYP schoolType)
        {
            ////Notes for graph : Can't reuse this function in graph, because the visible column is used in the Grade Display(Grade Column paramName)
            //the grap need the Data Set Column paramName, there is a little difference betwen theem. So can't reuse it in graph
            List<string> retval = new List<string>();

            Dictionary<String, String> map = new Dictionary<string, string>();
            map.Add(GroupKeys.Race, CommonColumnNames.RaceLabel.ToString());
            map.Add(GroupKeys.Gender,  CommonColumnNames.SexLabel.ToString());
            map.Add(GroupKeys.Grade, CommonColumnNames.GradeLabel.ToString());
            map.Add(GroupKeys.Disability, CommonColumnNames.DisabilityLabel.ToString());
            map.Add(GroupKeys.EconDisadv, CommonColumnNames.EconDisadvLabel.ToString());
            map.Add(GroupKeys.EngLangProf, CommonColumnNames.ELPLabel.ToString());

            //not all values result in adding a column
            if (map.ContainsKey(viewBy.Key)) retval.Add(map[viewBy.Key]);
            
            //Note that when "OrgLevel <> SC" and "Schooltype = 1 (all types)", we add
            //another column, SchooltypeLabel, to label the schooltype itemization...
            if ((orgLevel.Key != OrgLevelKeys.School) && (schoolType.Key == STYPKeys.AllTypes))
            {
                retval.Add(CommonColumnNames.SchooltypeLabel.ToString());
            }

            if (compareTo.Key == CompareToKeys.OrgLevel)
                retval.Add(CommonColumnNames.OrgLevelLabel.ToString());

            if ((compareTo.Key == CompareToKeys.SelSchools || compareTo.Key == CompareToKeys.SelDistricts) || (compareTo.Key == CompareToKeys.Current))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                retval.Add(CommonColumnNames.LinkedName.ToString());
            }


            //only show Year column when CompareToEnum == Prior Years
            if (compareTo.Key == CompareToKeys.Years)
                retval.Add(CommonColumnNames.YearFormatted.ToString());

            return retval;
        }

        /// <summary>
        /// Force GraphPanel Visibility = true, except in case of S4orALL.AllSchoolsOrDistrictsIn.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="GraphPanel"></param>
        /// <returns></returns>
        protected virtual bool CheckIfGraphPanelVisible(Panel GraphPanel)
        {
            bool result = true;
            GraphPanel.Visible = true;

            if ( GlobalValues.CompareTo.Key == CompareToKeys.SelSchools
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
            {

                if (GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                {
                    GraphPanel.Visible = false;
                    result = false;
                }
            }
            return result;
        }

        protected void CheckSelectedSchoolOrDistrict(Object sender, EventArgs args)
        {
            CheckSelectedSchoolOrDistrict(new BLWIBase());
        }
        /// <summary>
        /// Redirects the user if conditions are not met for Selected School (/District) Page View.
        /// </summary>
        /// <param name="entity"></param>
        protected void CheckSelectedSchoolOrDistrict(BLWIBase Entity)
        {
            if (
                (GlobalValues.CompareTo.Key != CompareToKeys.SelSchools &&
                GlobalValues.CompareTo.Key != CompareToKeys.SelDistricts) ||
                GlobalValues.S4orALL.Key != S4orALLKeys.FourSchoolsOrDistrictsIn ||
                IsSchoolsDistrictsSelected() == true ||
                Request.QueryString["B2G"] == "1" ||
                GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
            {
                return;
            }
            
            if (GlobalValues.SelectedFullkeys(GlobalValues.OrgLevel) == String.Empty)
                OnRedirectUser += EmptyFullkeysRedirect;
        }

        private void EmptyFullkeysRedirect()
        {
            string qs = string.Empty;
            string filedest = string.Empty;
            OrgLevel ol = GlobalValues.OrgLevel;

            if (ol.Key == OrgLevelKeys.District) filedest = "selMultiDistricts.aspx"; 
            else if (ol.Key == OrgLevelKeys.School) filedest = "selMultiSchools.aspx"; 
            else  throw new Exception("Invalid OrgLevel for Selected Fullkeys Redirect");

            // so that the redirect does not get into an endless loop.
            if (Request.FilePath.EndsWith(filedest)) return;
            
            qs = UserValues.GetBaseQueryString();
            string NavigateUrl = GlobalValues.CreateURL("~/" +  filedest, qs);
            Response.Redirect(NavigateUrl, true);
        }


        /// <summary>
        /// Tests for readyness of user state for Compare To Selected.... Uses GlobalValues and tests for Fullkeys when "Four In" and tests for appropriate Athletic Conference, CESA, or Count, ortherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsSchoolsDistrictsSelected()
        {
            bool result = false;

            S4orALL s4orAll = GlobalValues.S4orALL;

            if (s4orAll.Key == S4orALLKeys.FourSchoolsOrDistrictsIn)
            {
                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School &&
                    String.IsNullOrEmpty(GlobalValues.SSchoolFullKeys) == false)
                {
                    result = true;
                }
                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District &&
                    String.IsNullOrEmpty(GlobalValues.SDistrictFullKeys) == false)
                {
                    result = true;
                }
            }

            if (s4orAll.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
            {
                SRegion sRegion = GlobalValues.SRegion;

                if (sRegion.Key == SRegionKeys.AthleticConf &&
                    String.IsNullOrEmpty(GlobalValues.SAthleticConf) == false)
                {
                    result = true;
                }
                if (sRegion.Key == SRegionKeys.CESA &&
                    String.IsNullOrEmpty(GlobalValues.SCESA) == false)
                {
                    result = true;
                }
                if (sRegion.Key == SRegionKeys.County &&
                    String.IsNullOrEmpty(GlobalValues.SCounty) == false)
                {
                    result = true;
                }
            }

            return result;
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
        /// Returns a District fullkey (District "Cd" - Code) from the District or School Fullkey in the GlobalValues object.
        /// </summary>
        /// <returns></returns>
        public string GetDistrictCdByFullKey()
        {
            string result = string.Empty;
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
                || GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                result = GlobalValues.FULLKEY.Substring(2, 4);
            }
            return result;
        }

        /// <summary>
        /// Returns a School fullkey ("Cd" - Code) from the Fullkey in the GlobalValues object.
        /// </summary>
        /// <returns></returns>
        public string GetSchoolCdByFullKey()
        {
            string result = string.Empty;
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                result = GlobalValues.FULLKEY.Substring(8, 4);
            }
            return result;
        }
       
        /// <summary>
        /// callback-like function for setting navigation links. designed for call from loop through control collection.
        /// </summary>
        /// <param name="theLink"></param>
        public void setNavigationlinkURL(Control theCtrl)
        {
            HyperLinkPlus theLink = (HyperLinkPlus)theCtrl;
            string urlFile;

            if (UserValues == null || GlobalValues == null) throw new Exception("SetNavigationLinkURL, prerequisites not met.");
            
            //We want the same URL as the current page, except change the value for my globalValues.
            string qstring = UserValues.GetQueryString(theLink.ParamName, theLink.ParamValue);

            urlFile = 
                (theLink.UrlFile == null) ?  Request.Url.LocalPath 
                :   Request.ApplicationPath +
                     (      ( theLink.UrlFile.StartsWith("/")) ? theLink.UrlFile
                     :      "/" + theLink.UrlFile
                     ); 

            string url = string.Format("{0}{1}", urlFile, qstring);
            theLink.NavigateUrl = url;

            //If the current user's context is already set to use this param/value, 
            //disable this LinkButtonPlus.
            //E.g. if this LinkButtonPlus is set for STYP=1, and the current
            //  user context is set to STYP=1, then disable this. 
            System.Reflection.PropertyInfo propertyInfo = UserValues.GetType().
                GetProperty(theLink.ParamName,
                            System.Reflection.BindingFlags.GetProperty
                            | System.Reflection.BindingFlags.Public
                            | System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.IgnoreCase);

             SetNavigationLinkSelectedStatus(propertyInfo, theLink);
            
        }
        private void SetNavigationLinkSelectedStatus(System.Reflection.PropertyInfo propertyInfo, HyperLinkPlus theLink)
        {
            if (propertyInfo != null && propertyInfo.GetValue(UserValues, null) != null)
            // not sure why we do this test
            {  
                if (!propertyInfo.PropertyType.IsSubclassOf(typeof(ParameterValues)))
                {
                    // the property is not an enumeration
                    theLink.Selected = (theLink.ParamValue == propertyInfo.GetValue(GlobalValues, null).ToString());
                }
                else
                {  // the property is a ParameterValues sub-class
                    try
                    {
                        theLink.Selected =
                            (theLink.ParamValue ==
                            ((ParameterValues)propertyInfo.GetValue(GlobalValues, null)).Value.ToString()
                        );
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
        protected void OrgLevelCompareToOverride(Object Sender, EventArgs e)
        {
            GlobalValues globe = GlobalValues;
            
            
            if (globe.OrgLevel.Key == OrgLevelKeys.State)
            {
                globe.CompareTo.Value = globe.CompareTo.Range[CompareToKeys.Years];
            }
        }
        #endregion //Routines
    }
}
