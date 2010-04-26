using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.Base.WebServerControls.WI;
using SligoCS.Web.WI.WebUserControls;

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
        //protected global::System.Web.UI.WebControls.HyperLink ChangeSelectedSchoolOrDistrict;
        
        #region private members
        private GlobalValues userValues;
        private GlobalValues globalValues;
        private QueryMarshaller queryMarshaller;
        private TitleBuilder titleBuilder;
        private String dataSetTitle;
        private String rawCsvName;  //Download Raw Data FileName

//        private BLWIBase entity;
        private DALWIBase database;
        private DataSet dataset;
        private GridView datagrid;
        private ChartFX.WebForms.Chart graph;
        private Panel graphPanel;
        private NavViewByGroup navRowGroups;
        #endregion //  private members

        #region abstract initializers
        //commented out until ready to implement in each child page....
        //protected abstract BLWIBase InitEntity();// { BLWIBase e = new BLACT(); PrepBLEntity(e); return e;}
        //protected abstract void InitGraphPanel(Panel p);
        protected virtual DALWIBase InitDatabase() { return null;}
        protected virtual DataSet InitDataSet() { return new DataSet(); }
        protected virtual GridView InitDataGrid() { return new GridView(); }
        protected virtual ChartFX.WebForms.Chart InitGraph() { return null; }
        protected virtual NavViewByGroup InitNavRowGroups() { return null; }
        
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
            System.Diagnostics.Debug.WriteLine("Entered PageBaseWI.OnInit");
            
            OnCheckPrerequisites += CheckSelectedSchoolOrDistrict;
            
            userValues = new GlobalValues();
            globalValues = new GlobalValues();
            globalValues.Page = this;

            //Warning: dependencies between rules dictate order in which they are called
            InitComplete += GlobalValues.OverrideCompareToWhenOrgLevelIsState;
            InitComplete += GlobalValues.OverrideSchoolTypeWhenGroupIsGrade;
            InitComplete += GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool;
            InitComplete += GlobalValues.OverrideGroupWhenSchoolTypeIsAll;
            InitComplete += GlobalValues.OverrideLowGradeHighGradeForPriorYears;
            
            queryMarshaller = new QueryMarshaller(globalValues);
            titleBuilder = new TitleBuilder(globalValues);

            base.OnInit(e);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("PageBaseWI.OnInitComplete() Entered;");
            
            String myPageName = Page.Request.ServerVariables["SCRIPT_NAME"];
            String[] nameParts = myPageName.Split('/');
            int last = nameParts.GetLength(0) - 1;
            myPageName = nameParts[last];

            if (UserValues.GraphFile.Range.ContainsKey(myPageName))
            {
                GlobalValues.GraphFile.Value = UserValues.GraphFile.Value = UserValues.GraphFile.Range[myPageName];
            }

            base.OnInitComplete(e);

            Database = InitDatabase();
            DataSet = InitDataSet();
            DataGrid = InitDataGrid();
            Graph = InitGraph();
            NavRowGroups = InitNavRowGroups();

            if (Database != null) Database.DataSet = DataSet;
            if (QueryMarshaller != null) QueryMarshaller.Database = Database;
            
            OnAssociateCompareSelectedToOrgLevel(UserValues, GlobalValues);

            if (NavRowGroups != null)
                GlobalValues.OverrideGroupByLinksShown(NavRowGroups.LinksEnabled);
            else //disable
                GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];
            
            // run prerequisite checks
            if (OnCheckPrerequisites != null) OnCheckPrerequisites(this, EventArgs.Empty);

            // if prerequisite checks registered any redirects...
            if (OnRedirectUser != null && !GlobalValues.ShortCircuitRedirectTests ) OnRedirectUser();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            Page.Master.EnableViewState = false;
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading( SetPageHeading());
            Page.Title = Page.Title + " - " + SetPageHeading();

            //SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);

            if (Database != null)
            {
                QueryMarshaller.AutoQuery(Database);
                DataSet = Database.DataSet.Copy();
            }

            OnSetGridColumnVisibility();

            if (Graph != null)
            {
                //Don't display the Graph if Compare To Selected is All
                Graph.Visible = !(
                    (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                    && GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn
                    );

                //if not already hidden, Check whether there is any data to graph.
                if (Graph.Visible) Graph.Visible = (DataSet.Tables[0].Rows.Count != 0);
                //Always show when debug = graph
                if ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevel.Graph) == TraceStateUtils.TraceLevel.Graph) Graph.Visible = true;
            }

            //hide DataGrid Table
             if ((Request.QueryString["DETAIL"] != null) && (Request.QueryString["DETAIL"].ToString() != string.Empty))
            {
                string detailVal = Request.QueryString["DETAIL"];
                if (detailVal == "NO") DataGrid.Visible = false;
             }

            //actually raises the Load Event, so child Pages' handler is not executed until this is called.
            base.OnLoad(e);

            if (!String.IsNullOrEmpty(TitleBuilder.Prefix))//throw new Exception();
            {
                rawCsvName = TitleBuilder.DownloadRawDataFileName(TitleBuilder.Prefix);
                Session.Add("RawCsvName", rawCsvName);
                Session.Add("RawCsvData", GenerateRawCsvData(DataSet));
                //throw new Exception(Session[rawCsvName].ToString());
            }

            OnDataBindTable(); //must be called after Page Load has been
            if (Graph != null && Graph.Visible)OnDataBindGraph();
        }
        protected virtual String GenerateRawCsvData(DataSet ds)
        {
            StringBuilder data = new StringBuilder();
            List<String> labels = new List<string>();
            DataColumnCollection dsCols = ds.Tables[0].Columns;
            DataTable dt = ds.Tables[0];

            //replicated from WinssDataGrid onDataBind
            // Sort the Data:
            String orderBy = String.Empty;

            Boolean secondarySort = ((WinssDataGrid)datagrid).SelectedSortBySecondarySort;
            orderBy = ((WinssDataGrid)datagrid).OrderBy;

//            throw new Exception(orderBy);

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy =
                            SligoCS.DAL.WI.SQLHelper.ConvertToCSV(
                                queryMarshaller.BuildOrderByList(dsCols), false
                                );
            }

                orderBy = orderBy.Replace("year ASC", "year DESC");

                dt =
                    (secondarySort)
                    ? SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SecondarySortCompareSelectedFloatToTop(
                        dt, queryMarshaller, orderBy) 
                    :SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SortAndCompareSelectedFloatToTop(
                        dt, queryMarshaller, orderBy)
                    ;
            // end Sort

                String name;
            foreach (String col in GetDownloadRawVisibleColumns()) 
            { 
                if (dt.Columns.Contains(col))
                {

                    if (GetDownloadRawColumnLabelMapping().ContainsKey(col))
                        name = GetDownloadRawColumnLabelMapping()[col];
                    else
                        name = col;

                    labels.Add("\"" + DownloadRawDataColumnHelper.NormalizeColumnName(name.Trim()) + "\"");
                }
            }
            data.Append(String.Join(",", labels.ToArray()) + "\r\n");

            foreach (DataRow row in dt.Rows)
            {
                Array arrRow = row.ItemArray;
                List<String> strRow = new List<String>();
                //quote text:
                foreach (String col in GetDownloadRawVisibleColumns()) 
                { 
                    if (dt.Columns.Contains(col))
                    strRow.Add("\"" + row[col].ToString().Trim() + "\""); 
                }
                //delimit fields
                data.Append(String.Join(",", strRow.ToArray()) + "\r\n");
            }
            //throw new Exception(data.ToString());
            return data.ToString();
        }
        protected virtual List<String> GetDownloadRawVisibleColumns()
        {
            List<String> list = DownloadRawDataColumnHelper.GetStandardColumns();
            List<String> tableCols = GetVisibleColumns(GlobalValues.Group, GlobalValues.OrgLevel, GlobalValues.CompareTo, GlobalValues.STYP);
            foreach (String col in tableCols)
            {
                if (!list.Contains(col)) list.Add(col);
            }
            //remove standard columns used only in table and graph:
            List<String> rem = new List<String>();
            rem.Add(ColumnPicker.CommonNames.YearFormatted.ToString());
            rem.Add(ColumnPicker.CommonNames.SchooltypeLabel.ToString());
            rem.Add(ColumnPicker.CommonNames.OrgLevelLabel.ToString());
            rem.Add(ColumnPicker.CommonNames.LinkedName.ToString());
            rem.Add(ColumnPicker.CommonNames.OrgSchoolTypeLabel.ToString());
            //remove all columns contained in rem List:
            list.RemoveAll(rem.Contains);

            return list;
        }
        protected virtual SortedList<string,string> GetDownloadRawColumnLabelMapping()
        {
            return DownloadRawDataColumnHelper.MapStandardColumnsToDownloadLabels();
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            
            VisitControls(Page.Controls,
                delegate(Control ctrl) { return (ctrl is HyperLinkPlus); },
                    setNavigationlinkURL
             );

            base.OnPreRenderComplete(e);
        
        }
        protected virtual void OnDataBindTable()
        {
            DataBindTable(DataGrid, DataSet);
        }
        protected virtual void OnDataBindGraph()
        {
            if (Graph != null)
                DataBindGraph(Graph, DataSet);
        }
        protected virtual void OnSetGridColumnVisibility()
        {
            SetGridColumnVisibility(DataGrid);
        }
        protected virtual void OnAssociateCompareSelectedToOrgLevel(GlobalValues user, GlobalValues app)
        {
            GlobalValues.associateCompareSelectedToOrgLevel(user, app);
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
        public ChartFX.WebForms.Chart Graph
        {
            get { return graph; }
            set { graph = value; }
        }
        public virtual NavViewByGroup NavRowGroups
        {
            get {return navRowGroups; }
            set {navRowGroups = value; }
        }
        public virtual String DataSetTitle
        {
            get { return dataSetTitle; }
            set { dataSetTitle = value; }
        }

        #endregion //public properties

        #region legacy dungeon
       
        private void SetSligoDataGridColumnVisibility(SligoDataGrid grid, List<String> cols)
        {
            grid.SetAllBoundColumnsVisible(false);

            foreach (string name in cols)
            {
                grid.SetBoundColumnVisible(name, true);
            }
        }
       
        #endregion //legacy dungeon

        #region Routines
        public virtual void DataBindTable(GridView grid, DataSet ds)
        {
            if (DataSet.Tables.Count > 0)
            {
                grid.DataSource = ds.Tables[0].DefaultView;
                grid.DataBind();
            }
        }
        /// <summary>
        /// Assigns graph.DataSource and calls graph.DataBind()
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="ds"></param>
        public virtual void DataBindGraph(ChartFX.WebForms.Chart graph, DataSet ds)
        {
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //throw new Exception(GlobalValues.TraceSql);
                /*//trace To observe the raw Data Source
                if ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevel.Graph) == TraceStateUtils.TraceLevel.Graph)
                {
                    try
                    {
                        dt = ((DataView)ds.Tables[0].DefaultView).Table;
                    }
                    catch
                    {
                        dt = (DataTable)ds.Tables[0];
                    }
                    int i;
                    int n;
                    for (n = 0; n < dt.Columns.Count; n++)
                    {
                        Response.Write("N:" + dt.Columns[n].ColumnName + "|");
                        Response.Write("T:" + dt.Rows[0].ItemArray[n].GetType().Name + "<br/>");
                    }
                    Response.Write("<br />");
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        for (n = 0; n < dt.Columns.Count; n++)
                        {
                            Response.Write(
                                "V:" + dt.Rows[i].ItemArray[n] + "|"
                                );
                        }
                        Response.Write("<br />");
                    }
                }
                // end Trace*/

                graph.DataSource = ds.Tables[0].DefaultView;
                if ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevel.Graph) == TraceStateUtils.TraceLevel.Graph && graph.DataSource != null)
                                {
                                    Response.Write("<br />DataBindGraph(): graph.DataSource Assigned");
                                    Response.Write("<br /> ds.Tables.[0].DefaultView.Count:" + ds.Tables[0].DefaultView.Count);
                                }
                graph.DataBind();
                                if ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevel.Graph) == TraceStateUtils.TraceLevel.Graph && graph.DataSource != null)
                                {
                                    Response.Write("<br />DataBindGraph(): graph.DataBind() called;");
                                    Response.Write("<br />graph.DataSource.GetType().Name:" + graph.DataSource.GetType().Name);
                                    Response.Write("<br />");

                                    try
                                    {
                                        dt = ((DataView)graph.DataSource).Table;
                                    }
                                    catch
                                    {
                                        dt = (DataTable)graph.DataSource;
                                    }
                                    int i;
                                    int n;
                                    for (n = 0; n < dt.Columns.Count; n++)
                                    {
                                        Response.Write("N:" + dt.Columns[n].ColumnName + "|");
                                        if (dt.Rows.Count > 0)
                                            Response.Write("T:" + dt.Rows[0].ItemArray[n].GetType().Name + "<br/>");
                                    }
                                    Response.Write("<br />");
                                    if (dt.Rows.Count > 0)
                                    {
                                        for (i = 0; i < dt.Rows.Count; i++)
                                        {
                                            for (n = 0; n < dt.Columns.Count; n++)
                                            {
                                                Response.Write(
                                                    "V:" + dt.Rows[i].ItemArray[n] + "|"
                                                    );
                                            }
                                            Response.Write("<br />");
                                        }
                                    }
                                }
            }
            else
            {
                if ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevel.Graph) == TraceStateUtils.TraceLevel.Graph)
                        Response.Write("<br />DataBindGraph: ds.Tables.Count NOT > 0");
            }
        }
        public virtual void  SetGridColumnVisibility(GridView table)
        {
            if (table == null)  return;

            List<String> cols = GetVisibleColumns(
                        GlobalValues.Group,
                        GlobalValues.OrgLevel,
                        GlobalValues.CompareTo,
                        GlobalValues.STYP
                    );

            if (typeof(WinssDataGrid).IsInstanceOfType(table))
            {
                ((WinssDataGrid)table).SetVisibleColumns(cols);
            }

            if (typeof(SligoDataGrid).IsInstanceOfType(table))
            {
                SetSligoDataGridColumnVisibility(((SligoDataGrid)table), cols);
            }
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
        protected virtual String GetTitle(String beginTitle)
        {
            return TitleBuilder.GetTitle(beginTitle, GlobalValues, QueryMarshaller);
        }
        protected virtual String GetTitleForSchoolTypeUnsupported(String beginTitle)
        {
            return TitleBuilder.GetTitleForSchoolTypeUnsupported(beginTitle, GlobalValues, QueryMarshaller);
        }
        protected virtual String GetTitleWithoutGroup(String beginTitle)
        {
            return TitleBuilder.GetTitleWithoutGroup(beginTitle, GlobalValues, QueryMarshaller);
        }
        protected virtual String GetTitleWithoutGroupForSchoolTypeUnsupported(String beginTitle)
        {
            return TitleBuilder.GetTitleWithoutGroupForSchoolTypeUnsupported(beginTitle, GlobalValues, QueryMarshaller);
        }

         public virtual List<string> GetVisibleColumns(Group viewBy,
            OrgLevel orgLevel,
            CompareTo compareTo,
            STYP schoolType)
        {
            return ColumnPicker.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
        }

        /// <summary>
        /// DEPRECATED
        /// Force GraphPanel Visibility = true, except in case of S4orALL.AllSchoolsOrDistrictsIn.
        /// MZD: Appears to have been created to DISAble the graph for Compare to Selected case ALL.
        /// Name is not self-documenting. Needs to be incorporated into a more global business logic and architecture.
        /// Idea to search for the Graph Panel in the Controls Collection or incorporate graph Panel into the Graph UserControl...
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

        /// <summary>
        /// Redirects the user if conditions are not met for Selected School (/District) Page View.
        /// </summary>
        /// <param name="entity"></param>
        protected void CheckSelectedSchoolOrDistrict(Object sender, EventArgs args)
        {
            if (
                (GlobalValues.CompareTo.Key != CompareToKeys.SelSchools && GlobalValues.CompareTo.Key != CompareToKeys.SelDistricts) 
                || GlobalValues.S4orALL.Key != S4orALLKeys.FourSchoolsOrDistrictsIn 
                //|| Request.QueryString["B2G"] == "1" 
                || GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
            {
                return;
            }

            if (
                String.IsNullOrEmpty
                (GlobalValues.SFullKeys(GlobalValues.OrgLevel) )
                )
                OnRedirectUser += CompareToSelectedRedirect;
            else return;
        }

        private void CompareToSelectedRedirect()
        {
            System.Diagnostics.Debug.WriteLine("CompareToSelectedRedirect()");
            string qs = string.Empty;
            string filedest = string.Empty;
            OrgLevel ol = GlobalValues.OrgLevel;

            //hackish escape from redirect::
            String magicPageNameString = "ChooseSelected";

            /*if (ol.Key == OrgLevelKeys.District) filedest = magicPageNameString + "Districts.aspx"; 
            else if (ol.Key == OrgLevelKeys.School) filedest = magicPageNameString + "Schools.aspx"; 
            else  throw new Exception("Invalid OrgLevel for Selected Fullkeys Redirect");*/
            filedest = magicPageNameString + ".aspx";

            // so that the redirect does not get into an endless loop.  
            // and so we can select a different question
            if (Request.FilePath.Contains(magicPageNameString)
                || Request.FilePath.Contains("questions.aspx")
                || Request.FilePath.Contains("performance.aspx")
                || Request.FilePath.Contains("offerings.aspx")
                || Request.FilePath.Contains("demographics.aspx")
                || Request.FilePath.Contains("attendance.aspx")
                ) return;
            
            
            qs = UserValues.GetBaseQueryString();
            string NavigateUrl = GlobalValues.CreateURL("~/" +  filedest, qs);
            Response.Redirect(NavigateUrl, true);
        }
        protected void InitialAgencyRedirect()
        {
            System.Diagnostics.Debug.WriteLine("InitialAgencyRedirect()");

            string qs = string.Empty;
            string filedest = string.Empty;

            filedest = "SelSchool.aspx";
            qs = UserValues.GetBaseQueryString();
            string NavigateUrl = GlobalValues.CreateURL("~/" + filedest, qs);

            Response.Redirect(NavigateUrl, true);
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
            System.Reflection.PropertyInfo propertyInfo = GlobalValues.GetType().
                GetProperty(theLink.ParamName,
                            System.Reflection.BindingFlags.GetProperty
                            | System.Reflection.BindingFlags.Public
                            | System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.IgnoreCase);

             SetNavigationLinkSelectedStatus(propertyInfo, theLink);
            
        }
        public void SetNavigationLinkSelectedStatus(System.Reflection.PropertyInfo propertyInfo, HyperLinkPlus theLink)
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
        protected static void DisableSchoolType(Object sender, EventArgs e)
        {
            if (!(sender is GlobalValues)) return;

            GlobalValues globals = (GlobalValues)sender;

            if (globals.OrgLevel.Key != OrgLevelKeys.School)
            globals.STYP.Value = globals.STYP.Range[STYPKeys.StateSummary];
        }
        #endregion //Routines
    }
}
