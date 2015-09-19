using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.Base.WebServerControls.WI;

namespace SligoCS.Web.WI
{
    public partial class StateTestsScatterplot : PageBaseWI
    {
        protected override void OnInit(EventArgs e)
        {
            OnCheckPrerequisites += new CheckPrerequisitesHandler(StateTestsScatterplot_OnCheckPrerequisites);
            base.OnInit(e);
        }
        void StateTestsScatterplot_OnCheckPrerequisites(PageBaseWI page, EventArgs args)
        {
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) return;

            if (FullKeyUtils.GetOrgLevelFromFullKeyX(GlobalValues.FULLKEY).Key == OrgLevelKeys.State)
                OnRedirectUser += InitialAgencyRedirect;
        }
        protected override string SetPageHeading()
        {
            string heading = "How did the performance of my district compare to other districts statewide?";
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                heading = heading.Replace("district", "school");
            return heading;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALWSASDemographics();
        }
        protected override GridView InitDataGrid()
        {
            return StateTestsScatterDataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return scatterplot;
        }
        public static List<String> getGradeCodeRange(PageBaseWI Page)
        {
            List<String> codes = new List<String>();
            QueryMarshaller qm = new QueryMarshaller(Page.GlobalValues);

            Page.GlobalValues.SQL = DALWSAS.WSASGrades(qm);
            qm.AssignQuery(new DALWSAS(), Page.GlobalValues.SQL);
            DataSet result = qm.Database.DataSet.Copy();
            if (result == null || result.Tables[0].Rows.Count == 0) return codes;
            //throw new Exception(tbl.GetLength(0).ToString() + " || " + tbl[0][0].ToString());
            foreach (DataRow row in result.Tables[0].Rows)
            {
                codes.Add(row[0].ToString());
            }
            //disable all tested:
            String disag = Page.GlobalValues.Grade.Range[GradeKeys.AllDisAgg];
            if (codes.Contains(disag)) codes.Remove(disag);

            //unsupported grade by Subject:
            string[] unsupported = 
            {
                Page.GlobalValues.Grade.Range[GradeKeys.Grade_3], 
                Page.GlobalValues.Grade.Range[GradeKeys.Grade_5], 
                Page.GlobalValues.Grade.Range[GradeKeys.Grade_6],
                Page.GlobalValues.Grade.Range[GradeKeys.Grade_7]
            };
            if (Page.GlobalValues.SubjectID.Key == SubjectIDKeys.Language
                || Page.GlobalValues.SubjectID.Key == SubjectIDKeys.Science
                || Page.GlobalValues.SubjectID.Key == SubjectIDKeys.SocialStudies)
            {
                int i =0;
                for(i = 0 ; i < unsupported.Length; i++)
                {
                        codes.Remove(unsupported[i]);
                }
            }
            return codes;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.CurrentYear = 2011;

            //Disable "All Tested Subjects"
            if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested) GlobalValues.SubjectID.Key = SubjectIDKeys.Reading;
            nlrSubject.LinkControlAdded += new LinkControlAddedHandler(disableAllSubjects_LinkControlAdded);

            //View By Group Unsupported.
            GlobalValues.Group.Key = GroupKeys.All;
            //SchoolType Unsupported.
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
            //Compare To Unsupported.
            GlobalValues.CompareTo.Key = CompareToKeys.Current;
            //StateLevel Unsupported.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) GlobalValues.OrgLevel.Key = OrgLevelKeys.District;

            GlobalValues.FAYCode.Key = FAYCodeKeys.FAY;

            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.SubjectID, nlrSubject, SubjectIDKeys.Reading);
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Grade, nlrGrade, GradeKeys.Combined_PreK_12);

            GlobalValues.GradeCodesActive = getGradeCodeRange(this);
            if (GlobalValues.GradeCodesActive.Count > 0 &&
                !GlobalValues.GradeCodesActive.Contains(GlobalValues.Grade.Value)) GlobalValues.Grade.Value = GlobalValues.GradeCodesActive[0];  //default to minimum grade
            nlrGrade.LinkControlAdded += new LinkControlAddedHandler(disableGradeLinks_LinkControlAdded);

            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Level, nlrLevel, LevelKeys.AdvancedProficient);
            if (GlobalValues.Level.Key == LevelKeys.All) GlobalValues.Level.Key = LevelKeys.AdvancedProficient;
            nlrLevel.LinkControlAdded += new LinkControlAddedHandler(disableLevels_LinkControlAdded);

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && (GlobalValues.Level.Key == LevelKeys.WAA_ELL || GlobalValues.Level.Key == LevelKeys.WAA_SwD))
                GlobalValues.Level.Key = LevelKeys.NoWSAS;

            base.OnInitComplete(e);

            if (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12)
                DataGrid.Columns.FieldsChanged += new EventHandler(renameEnrolledColumn_FieldsChanged);

            nlrLevel.LinkControlAdded += new LinkControlAddedHandler(renameLevelLabels_LinkControlAdded);
        }

        public static void disableAllSubjects_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID == "linkSubjectIDAllTested") link.Enabled = false;
        }

        void renameLevelLabels_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID != "linkLevelBasicMinSkillEng") return;

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                link.Text = "Basic + Min Perf +No WSAS";
        }
        public static void disableSubjects_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID == "linkSubjectIDAllTested") link.Enabled = false;
        }

        void disableLevels_LinkControlAdded(HyperLinkPlus link)
        {
            if ( link.ID == "linkLevelAll")
                link.Enabled = false;

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && (link.ID == "linkLevelWAA_SwD" || link.ID == "linkLevelWAA_ELL")
                ) link.Enabled = false;

        }
        public static void disableGradeLinks_LinkControlAdded(HyperLinkPlus link)
        {
            if (!((PageBaseWI)link.Page).GlobalValues.GradeCodesActive.Contains(link.ParamValue)) link.Enabled = false;
        }
        public static void renameEnrolledColumn_FieldsChanged(object sender, EventArgs e)
        {
            DataControlFieldCollection columns = (DataControlFieldCollection)sender;
            
            foreach (DataControlField field in columns)
            {
                if (field.HeaderText == "Enrolled at Test Time")
                    field.HeaderText = "Enrolled in Tested Grade(s) at Test Time";
            }
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //show message if the graph is not visible
            pnlMessage.Visible = (Graph.Visible == false);

            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(
                    ((GlobalValues.WOW.Key == WOWKeys.WSASCombined) ?
                        WOWKeys.WSASCombined : WOWKeys.WKCE)
                        + " - " + (
                        (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12 
                        || GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
                        ? String.Empty
                        : "Grade "
                        ) + GlobalValues.Grade.Key
                        + " - " + GlobalValues.SubjectID.Key + WebSupportingClasses.TitleBuilder.newline
                        +  GlobalValues.Level.Key
                    ) 
                ;

            DataSetTitle = DataSetTitle.Replace(
                    GlobalValues.GetOrgName(),
                    GlobalValues.GetOrgName() + "Compared to Other "
                    + ((GlobalValues.OrgLevel.Key == OrgLevelKeys.School)? "Schools" : "Districts") + " in "
                    + ((GlobalValues.LF.Key == LFKeys.State)? "Entire State" :
                    (GlobalValues.LF.Key == LFKeys.CESA)? "CESA " + GlobalValues.Agency.CESA.Trim() :
                    GlobalValues.Agency.CountyName.Trim()
                    )
                    
                    );

            if (GlobalValues.FAYCode.Key == FAYCodeKeys.FAY)
                DataSetTitle = DataSetTitle.Replace(GlobalValues.GetOrgName(), GlobalValues.GetOrgName().Trim() + "  FAY ");

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
                DataSetTitle = DataSetTitle.Replace("+ WAA SwD/ELL ", String.Empty);

            DataSetTitle = 
                DataSetTitle.Replace(TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years),
                     "November " + (GlobalValues.Year-1).ToString() + " Data");

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                DataSetTitle = DataSetTitle.Replace(TitleBuilder.GetSchoolTypeInTitle(GlobalValues.STYP), String.Empty);
            }

            StateTestsScatterDataGrid.AddSuperHeader(DataSetTitle);
            StateTestsScatterDataGrid.AddSuperHeader(GetSecondSuperHeader());

            List<String> order = QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns);
            order.Insert(0, v_WSAS.School_Name);
            order.Insert(0, v_WSAS.District_Name); //same index is intentional... district, then school
            StateTestsScatterDataGrid.OrderBy = String.Join(", ", order.ToArray());

            StateTestsScatterDataGrid.ForceCurrentFloatToTopOrdering = true;

            SetUpChart();

            set_state();
            setBottomLink();
        }

        private void SetUpChart()
        {
            //try
            {
                scatterplot.Title = DataSetTitle;
                //use a higher than normal height since page's title is long, Y-Axis Labels are long, and scatterplot benefits in readbility
                scatterplot.Height = ((int)scatterplot.Height.Value) + 100;

                #region X-Axis settings

                Rel  relatedTo = GlobalValues.Rel;
                string xValueColumnName = string.Empty;
                int xMin = 0;
                int xMax = 100;
                int xStep = 10;
                string axisXDescription = string.Empty;
                int maxValueInResult = 0;

                xValueColumnName = GetGraphXAxisColumn();

                if (relatedTo.CompareToKey(RelKeys.DistrictSize) ||
                    relatedTo.CompareToKey(RelKeys.DistrictSpending))
                {
                    foreach (DataRow row in DataSet.Tables[0].Rows)
                    {
                        try
                        {
                            if (maxValueInResult < (int)Convert.ToDouble(row[xValueColumnName].ToString().Trim()))
                            {
                                maxValueInResult = (int)Convert.ToDouble(row[xValueColumnName].ToString().Trim());
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }


                if (relatedTo.CompareToKey(RelKeys.DistrictSpending))
                {
                    axisXDescription = "District Current Education Cost Per Member";
                    xMax = maxValueInResult + 1;
                    xStep = 2000;
                    scatterplot.AxisX.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Currency;
                }
                else if (relatedTo.CompareToKey(RelKeys.DistrictSize))
                {
                    axisXDescription = "District Size -- Number of Students Enrolled";
                    xMax = maxValueInResult + 1;
                    xStep = 10000;
                    scatterplot.AxisX.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Number;
                }
                else if (relatedTo.CompareToKey(RelKeys.EconDisadvantaged))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.LEP))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.Disabilities))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.Native))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.Asian))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.Black))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.Hispanic))
                {
                    axisXDescription = relatedTo.Key + " FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                else if (relatedTo.CompareToKey(RelKeys.White))
                {
                    axisXDescription = relatedTo.Key + "(Not of Hispanic Origin) FAY";
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                scatterplot.XValueColumnName = xValueColumnName;
                scatterplot.AxisXMin = xMin;
                scatterplot.AxisXMax = xMax;
                scatterplot.AxisXStep = xStep;
                scatterplot.AxisXDescription = axisXDescription;
                #endregion

                #region Y-Axis settings

                string yValueColumnName = string.Empty;
                yValueColumnName = GetGraphYAxisColumn();

                scatterplot.YValueColumnName = yValueColumnName;
                scatterplot.AxisYMin = 0;
                scatterplot.AxisYMax = 100;
                scatterplot.AxisYStep = 10;
                scatterplot.AxisYDescription = GetYAxisDescription();
                scatterplot.AxisY.LabelsFormat.CustomFormat = "0" + "\\%";

                #endregion

                ArrayList friendlySeriesName = new ArrayList();
                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                {
                    friendlySeriesName.Add("Current School");
                    friendlySeriesName.Add("Other Schools");
                }
                else 
                {
                    friendlySeriesName.Add("Current District");

                    friendlySeriesName.Add("Other Districts");
                }

                if (GlobalValues.Group2.Key == Group2Keys.None)
                {
                    scatterplot.FriendlySeriesName = friendlySeriesName;
                }
                else
                {
                    scatterplot.FriendlySeriesName = GetGraphSeriesLabels();
                }
                
                //only stratify series when:
                if (GlobalValues.Group2.Key != Group2Keys.None)
                    scatterplot.SeriesByStrata = GetSeriesStrata();

                scatterplot.SeriesColumnName = GetSeriesColumnName();
            }
            /*catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }*/
            
            ChartFX.WebForms.TitleDockable title = new ChartFX.WebForms.TitleDockable(GetLegendTitleText());
            scatterplot.LegendBox.Titles.Add(title);
        }

        private String GetLegendTitleText()
        {
            if (GlobalValues.Group2.Key == Group2Keys.None) return String.Empty;
            
            String title = String.Empty;

            if (GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
                title = "District Current Education Cost per Member";
            else if (GlobalValues.Group2.Key == Group2Keys.DistrictSize)
                title = "District Size -- Number of Students Enrolled";
            else
                title = GlobalValues.Group2.Key + " FAY";

            DataRow[] data = DataSet.Tables[0].Select("fullkey = '" + FullKeyUtils.GetMaskedFullkey(GlobalValues.FULLKEY, GlobalValues.OrgLevel) + "'");
            if (data.Length != 1) return title;

            int col = data[0].Table.Columns.IndexOf(GetSeriesColumnName());
            String val = String.Empty;
            if (col > -1) val = data[0].ItemArray[col].ToString();

            if (GlobalValues.Group2.Key == Group2Keys.DistrictSpending
                || GlobalValues.Group2.Key == Group2Keys.DistrictSize)
            {
                String frmt;
                if (GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
                {
                    frmt = "{0:$#,###}";
                }
                else //if (GlobalValues.Group2.Key == Group2Keys.DistrictSize)
                {
                    frmt = "{0:#,###}";
                }

                try
                {
                    val = String.Format(frmt, Convert.ToDecimal(val)); //format doesn't work properly on a string
                }
                catch { }
            }
            else
            {
                val = val + "%";
            }

            title = title + WebSupportingClasses.TitleBuilder.newline
            + "Current Value = " + val;

            return title;
                
        }
        private String GetYAxisDescription()
        {
            String desc = "% " + GlobalValues.Level.Key + " FAY";
            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined) desc = desc.Replace("+ WAA SwD/ELL ", String.Empty);
            return desc;
        }

        private double[] GetSeriesStrata()
        {
            double[] strata;
            if (GlobalValues.Group2.Key == Group2Keys.DistrictSize)
                strata = new double[] { 0, 500, 1000, 2000, 10000 };
            else if (GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
                strata = new double[] { 0, 9500, 10500, 11500 };
            else if (GlobalValues.Group2.Key == Group2Keys.EconDisadvantaged)
                strata = new double[] { 0, 10, 25, 50, 75, 100 };
            else
                strata = new double[] { 0, 10, 25, 50, 100 };

            return strata;
        }

        private ArrayList GetGraphSeriesLabels()
        {
            ArrayList labels = new ArrayList();
            double[] strata = GetSeriesStrata();

            labels.Add(
                        "Current " +
                        ((GlobalValues.OrgLevel.Key == OrgLevelKeys.School) ? "School FAY" : "District")
                                                );

            if (GlobalValues.Group2.Key == Group2Keys.DistrictSize
                || GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
            {

                String frmt;
                if (GlobalValues.Group2.Key == Group2Keys.DistrictSize)
                    frmt = "#,###,##0";
                else //if (GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
                    frmt = "$#,##0";

                for (int n = 0; n < strata.Length; n++)
                {
                    if (n == 0)
                    {
                        labels.Add(strata[n].ToString(frmt) + "-" + strata[n + 1].ToString(frmt));
                    }
                    else if (n != strata.Length - 1)
                    {
                        labels.Add(strata[n].ToString(frmt) 
                            + ( (GlobalValues.Group2.Key != Group2Keys.DistrictSize)? ".01-" : "-" )
                            + strata[n + 1].ToString(frmt));
                    }
                    else if (n == strata.Length - 1)
                    {
                        labels.Add("Greater Than " + strata[n].ToString(frmt));
                    }
                }
            }
            else //percents
            {
                for (int n = 0; n < strata.Length; n++)
                {
                    if (n == 0)
                    {
                        labels.Add(strata[n] + "%-" + strata[n + 1] + "%");
                    }
                    else if (n != strata.Length - 1)
                    {
                        labels.Add(strata[n] + ".01%-" + strata[n + 1] + "%");
                    }
                    else if (n == strata.Length - 1)
                    {
                        //don't add open-ended strata for percents
                    }
                }
            }

            return labels;
        }

        private String GetGraphYAxisColumn()
        {
            if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    return v_WSASDemographics.AdvancedWSAS;
                else
                    return v_WSASDemographics.Percent_Advanced;
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    return v_WSASDemographics.AdvancedPlusProficientTotalWSAS;
                else
                    return v_WSASDemographics.PCTAdvPlusPCTPrf;
            }
            else if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    return v_WSASDemographics.BasicPlusMinPerfPlusNoWSASTotalWSAS;
                }
                else
                    return v_WSASDemographics.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal;
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_SwD && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                return v_WSASDemographics.PctTotalWAADisabil;
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_ELL && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                return v_WSASDemographics.PctTotalWAALep;
            }
            else if (GlobalValues.Level.Key == LevelKeys.NoWSAS)
            {
                return v_WSASDemographics.No_WSAS_Total; //and for graph
            }
            //fail
            return String.Empty;
        }
        private String GetGraphXAxisColumn()
        {
            if (GlobalValues.Rel.Key == RelKeys.DistrictSize)
                return v_WSASDemographics.District_Size;

            else if (GlobalValues.Rel.Key == RelKeys.DistrictSpending)
                return v_WSASDemographics.Cost_Per_Member;

            else if (GlobalValues.Rel.Key == RelKeys.EconDisadvantaged)
                return v_WSASDemographics.PctEcon;

            else if (GlobalValues.Rel.Key == RelKeys.LEP)
                return v_WSASDemographics.PctLEP;

            else if (GlobalValues.Rel.Key == RelKeys.Disabilities)
                return v_WSASDemographics.PctDisabled;

            else if (GlobalValues.Rel.Key == RelKeys.Native)
                return v_WSASDemographics.PctAmInd;

            else if (GlobalValues.Rel.Key == RelKeys.Asian)
                return v_WSASDemographics.PctAsian;

            else if (GlobalValues.Rel.Key == RelKeys.Black)
                return v_WSASDemographics.PctBlack;

            else if (GlobalValues.Rel.Key == RelKeys.Hispanic)
                return v_WSASDemographics.PctHisp;

            else if (GlobalValues.Rel.Key == RelKeys.White)
                return v_WSASDemographics.PctWhite;

            //fail
            return String.Empty;

        }
        private String GetSeriesColumnName()
        {
            if (GlobalValues.Group2.Key == RelKeys.DistrictSize)
                return v_WSASDemographics.District_Size;

            else if (GlobalValues.Group2.Key == RelKeys.DistrictSpending)
                return v_WSASDemographics.Cost_Per_Member;

            else if (GlobalValues.Group2.Key == Group2Keys.EconDisadvantaged)
                return v_WSASDemographics.PctEcon;

            else if (GlobalValues.Group2.Key == Group2Keys.LEP)
                return v_WSASDemographics.PctLEP;

            else if (GlobalValues.Group2.Key == Group2Keys.Disabilities)
                return v_WSASDemographics.PctDisabled;

            else if (GlobalValues.Group2.Key == Group2Keys.Native)
                return v_WSASDemographics.PctAmInd;

            else if (GlobalValues.Group2.Key == Group2Keys.Asian)
                return v_WSASDemographics.PctAsian;

            else if (GlobalValues.Group2.Key == Group2Keys.Black)
                return v_WSASDemographics.PctBlack;

            else if (GlobalValues.Group2.Key == Group2Keys.Hispanic)
                return v_WSASDemographics.PctHisp;

            else if (GlobalValues.Group2.Key == Group2Keys.White)
                return v_WSASDemographics.PctWhite;

            //fail
            return String.Empty;

        }
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<String> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add(v_WSASDemographics.Enrolled);

            if (GlobalValues.Rel.Key == RelKeys.DistrictSize
                || GlobalValues.Group2.Key == Group2Keys.DistrictSize)
                cols.Add(v_WSASDemographics.District_Size);

            if (GlobalValues.Rel.Key == RelKeys.DistrictSpending
                || GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
                cols.Add(v_WSASDemographics.Cost_Per_Member);

            if (GlobalValues.Rel.Key == RelKeys.EconDisadvantaged
                || GlobalValues.Group2.Key == Group2Keys.EconDisadvantaged)
                cols.Add(v_WSASDemographics.PctEcon);

            if (GlobalValues.Rel.Key == RelKeys.LEP
               || GlobalValues.Group2.Key == Group2Keys.LEP)
                cols.Add(v_WSASDemographics.PctLEP);

            if (GlobalValues.Rel.Key == RelKeys.Disabilities
               || GlobalValues.Group2.Key == Group2Keys.Disabilities)
                cols.Add(v_WSASDemographics.PctDisabled);

            if (GlobalValues.Rel.Key == RelKeys.Native
               || GlobalValues.Group2.Key == Group2Keys.Native)
                cols.Add(v_WSASDemographics.PctAmInd);

            if (GlobalValues.Rel.Key == RelKeys.Asian
               || GlobalValues.Group2.Key == Group2Keys.Asian)
                cols.Add(v_WSASDemographics.PctAsian);

            if (GlobalValues.Rel.Key == RelKeys.Black
               || GlobalValues.Group2.Key == Group2Keys.Black)
                cols.Add(v_WSASDemographics.PctBlack);

            if (GlobalValues.Rel.Key == RelKeys.Hispanic
               || GlobalValues.Group2.Key == Group2Keys.Hispanic)
                cols.Add(v_WSASDemographics.PctHisp);

            if (GlobalValues.Rel.Key == RelKeys.White
               || GlobalValues.Group2.Key == Group2Keys.White)
                cols.Add(v_WSASDemographics.PctWhite);

           if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    cols.Add(v_WSASDemographics.AdvancedWSAS);
                else
                    cols.Add(v_WSASDemographics.Percent_Advanced);
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    cols.Add(v_WSASDemographics.AdvancedPlusProficientTotalWSAS);
                else
                    cols.Add(v_WSASDemographics.PCTAdvPlusPCTPrf);
            }
            else if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    cols.Add(v_WSASDemographics.BasicPlusMinPerfPlusNoWSASTotalWSAS);
                }
                else
                    cols.Add(v_WSASDemographics.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal);
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_SwD && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                cols.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_1);
                cols.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_2);
                cols.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_3);
                cols.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_4);
                cols.Add(v_WSASDemographics.PctTotalWAADisabil);
                //FOR GRAPH:
                // v_WSAS.PctTotalWAADisabil
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_ELL && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                cols.Add(v_WSASDemographics.Percent_PreReq_Eng_Minimal);
                cols.Add(v_WSASDemographics.Percent_PreReq_Eng_Basic);
                cols.Add(v_WSASDemographics.Percent_PreReq_Eng_Proficient);
                cols.Add(v_WSASDemographics.Percent_PreReq_Eng_Advanced);
                cols.Add(v_WSASDemographics.PctTotalWAALep);
                //for graph:
                //v_WSAS.PctTotalWAALep
            }
            else if (GlobalValues.Level.Key == LevelKeys.NoWSAS)
            {
                cols.Add(v_WSASDemographics.ExcusedByParent);
                cols.Add(v_WSASDemographics.EligibleButNotTested);
                cols.Add(v_WSASDemographics.No_WSAS_Total); //and for graph
            }

            return cols;
        }

        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            TQRelateTo relateTo = GlobalValues.TQRelateTo;

            WinssDataGrid.AddTableCell(tr, "&nbsp;", 1); //first label column is always blank

            string wkcewsas = GlobalValues.WOW.Key;
            string grade = ((GlobalValues.Grade.Key != GradeKeys.Combined_PreK_12  && GlobalValues.Grade.Key != GradeKeys.AllDisAgg) 
                ? "Grade " : String.Empty)
            + GlobalValues.Grade.Key;
            
            int fayColSpan = 
                (GlobalValues.Level.Key == LevelKeys.WAA_ELL || GlobalValues.Level.Key == LevelKeys.WAA_SwD)
                ? 6 : 2;

            if (GlobalValues.Level.Key == LevelKeys.NoWSAS) fayColSpan = fayColSpan + 2;

            if ((GlobalValues.Rel.Key != RelKeys.DistrictSize &&
                GlobalValues.Rel.Key != RelKeys.DistrictSpending)
                || (GlobalValues.Group2.Key != Group2Keys.DistrictSize &&
                GlobalValues.Group2.Key != Group2Keys.DistrictSpending)
                )
            {
                fayColSpan++;
            }

            if (GlobalValues.Rel.Key == RelKeys.DistrictSize
                || GlobalValues.Group2.Key == Group2Keys.DistrictSize)
            {
                WinssDataGrid.AddTableCell(tr,RelKeys.DistrictSize, 1);
            }
            
            if (GlobalValues.Rel.Key == RelKeys.DistrictSpending
                || GlobalValues.Group2.Key == Group2Keys.DistrictSpending)
            {
                WinssDataGrid.AddTableCell(tr, RelKeys.DistrictSpending, 1);
            }

            WinssDataGrid.AddTableCell(tr, wkcewsas + " -- All Students Enrolled FAY -- " + grade, fayColSpan);

            foreach (TableCell cell in tr.Cells)
            {
                cell.BorderColor = System.Drawing.Color.Gray;
                cell.BorderWidth = 4;
            }

            return tr;
        }
        
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile.DistrictCd = GlobalValues.DistrictCode;
        }

  
        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            int index;

            index = cols.IndexOf(v_WSASDemographics.Enrolled);
            cols.Insert(index, v_WSASDemographics.SubjectLabel);

            if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSASDemographics.AdvancedWSAS);
                    cols.Insert(index, v_WSASDemographics.Number_AdvancedWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSASDemographics.Percent_Advanced);
                    cols.Insert(index, v_WSASDemographics.Number_Advanced); 
                }
                    
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSASDemographics.AdvancedPlusProficientTotalWSAS);
                    cols.Insert(index, v_WSASDemographics.Number_AdvancedPlusProficientTotalWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSASDemographics.PCTAdvPlusPCTPrf);
                    cols.Insert(index, v_WSASDemographics.Number_AdvPlusPCTPrf);
                }
            }
            else if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSASDemographics.BasicPlusMinPerfPlusNoWSASTotalWSAS);
                    cols.Insert(index, v_WSASDemographics.Number_BasicPlusMinPerfPlusNoWSASTotalWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSASDemographics.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal);
                    cols.Insert(index, v_WSASDemographics.Number_BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal);
                }
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_SwD && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Skill_Level_1);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Skill_Level_1);
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Skill_Level_2);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Skill_Level_2);
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Skill_Level_3);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Skill_Level_3);
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Skill_Level_4);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Skill_Level_4);
                index = cols.IndexOf(v_WSASDemographics.PctTotalWAADisabil);
                cols.Insert(index, v_WSASDemographics.Number_TotalWAADisabil);
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_ELL && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Eng_Minimal);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Eng_Minimal);
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Eng_Basic);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Eng_Basic);
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Eng_Proficient);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Eng_Proficient);
                index = cols.IndexOf(v_WSASDemographics.Percent_PreReq_Eng_Advanced);
                cols.Insert(index, v_WSASDemographics.Number_PreReq_Eng_Advanced);
                index = cols.IndexOf(v_WSASDemographics.PctTotalWAALep);
                cols.Insert(index, v_WSASDemographics.Number_TotalWAALep);
            }
            else if (GlobalValues.Level.Key == LevelKeys.NoWSAS)
            {
                index = cols.IndexOf(v_WSASDemographics.ExcusedByParent);
                cols.Insert(index, v_WSASDemographics.Number_ExcusedByParent);
                index = cols.IndexOf(v_WSASDemographics.EligibleButNotTested);
                cols.Insert(index, v_WSASDemographics.Number_EligibleButNotTested);
                index = cols.IndexOf(v_WSASDemographics.No_WSAS_Total);
                cols.Insert(index, v_WSASDemographics.Number_No_WSAS_Total);
            }

            ///Relate TO Column:
            if (GlobalValues.Rel.Key == RelKeys.EconDisadvantaged
                || GlobalValues.Group2.Key == Group2Keys.EconDisadvantaged)
            {
                index = cols.IndexOf(v_WSASDemographics.PctEcon);
                cols.Insert(index, v_WSASDemographics.NumEcon);
            }
            if (GlobalValues.Rel.Key == RelKeys.LEP
                || GlobalValues.Group2.Key == Group2Keys.LEP)
            {
                index = cols.IndexOf(v_WSASDemographics.PctLEP);
                cols.Insert(index, v_WSASDemographics.NumLEP);
            }
            if (GlobalValues.Rel.Key == RelKeys.Disabilities
               || GlobalValues.Group2.Key == Group2Keys.Disabilities)
            {
                index = cols.IndexOf(v_WSASDemographics.PctDisabled);
                cols.Insert(index, v_WSASDemographics.NumDisabled);
            }
            if (GlobalValues.Rel.Key == RelKeys.Native
               || GlobalValues.Group2.Key == Group2Keys.Native)
            {
                index = cols.IndexOf(v_WSASDemographics.PctAmInd);
                cols.Insert(index, v_WSASDemographics.NumAmInd);
            }
            if (GlobalValues.Rel.Key == RelKeys.Asian
               || GlobalValues.Group2.Key == Group2Keys.Asian)
            {   
                index = cols.IndexOf(v_WSASDemographics.PctAsian);
                cols.Insert(index, v_WSASDemographics.NumAsian);
            }
            if (GlobalValues.Rel.Key == RelKeys.Black
               || GlobalValues.Group2.Key == Group2Keys.Black)
            {   
                index = cols.IndexOf(v_WSASDemographics.PctBlack);
                cols.Insert(index, v_WSASDemographics.NumBlack);
            }
            if (GlobalValues.Rel.Key == RelKeys.Hispanic
               || GlobalValues.Group2.Key == Group2Keys.Hispanic)
            {   
                index = cols.IndexOf(v_WSASDemographics.PctHisp);
                cols.Insert(index, v_WSASDemographics.NumHisp);
            }
            else if (GlobalValues.Rel.Key == RelKeys.White
               || GlobalValues.Group2.Key == Group2Keys.White)
            {
                index = cols.IndexOf(v_WSASDemographics.PctWhite);
                cols.Insert(index, v_WSASDemographics.NumWhite);
            }
            cols.Remove(v_WSASDemographics.RaceLabel);
            cols.Remove(v_WSASDemographics.SexLabel);
            cols.Remove(v_WSASDemographics.DisabilityLabel);
            cols.Remove(v_WSASDemographics.EconDisadvLabel);
            cols.Remove(v_WSASDemographics.ELPLabel);
            cols.Remove(v_WSASDemographics.MigrantLabel);
            return cols; 
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            newLabels.Remove(v_WSASDemographics.GradeLabel);
            newLabels.Add(v_WSASDemographics.GradeLabel, "tested_grades_combined");
            newLabels.Add(v_WSASDemographics.District_Size, "district_size_total_fall_enrollment_prek-12");
            newLabels.Add(v_WSASDemographics.Cost_Per_Member, "district_ current_education_cost_per_member");
            newLabels.Add(v_WSASDemographics.AdvancedWSAS, "percent_advanced_wsas");
            newLabels.Add(v_WSASDemographics.Number_AdvancedWSAS, "number_advanced_wsas");
            newLabels.Add(v_WSASDemographics.Percent_Advanced, "percent_advanced_wkce");
            newLabels.Add(v_WSASDemographics.Number_Advanced, "number_advanced_wkce");
            newLabels.Add(v_WSASDemographics.AdvancedPlusProficientTotalWSAS, "percent_advanced_plus_proficient_wsas");
            newLabels.Add(v_WSASDemographics.Number_AdvancedPlusProficientTotalWSAS, "number_advanced_plus_proficient_wsas");
            newLabels.Add(v_WSASDemographics.PCTAdvPlusPCTPrf, "percent_advanced_plus_proficient_wkce");
            newLabels.Add(v_WSASDemographics.Number_AdvPlusPCTPrf, "number_advanced_plus_proficient_wkce");
            newLabels.Add(v_WSASDemographics.BasicPlusMinPerfPlusNoWSASTotalWSAS, "percent_basic_plus_minperf_plus_nowsas_wsas");
            newLabels.Add(v_WSASDemographics.Number_BasicPlusMinPerfPlusNoWSASTotalWSAS, "number_basic_plus_minperf_plus_nowsas_wsas");
            newLabels.Add(v_WSASDemographics.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal, "percent_basic_plus_minperf_plus_waa_swd_ell_plus_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.Number_BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal, "number_basic_plus_minperf_plus_waa_swd_ell_plus_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.PctTotalWAALep, "percent_waa_ell_total_wkce");
            newLabels.Add(v_WSASDemographics.Number_TotalWAALep, "number_waa_ell_total_wkce");
            newLabels.Add(v_WSASDemographics.ExcusedByParent, "percent_excused_by_parent");
            newLabels.Add(v_WSASDemographics.Number_ExcusedByParent, "number_excused_by_parent");
            newLabels.Add(v_WSASDemographics.EligibleButNotTested, "percent_eligible_not_tested");
            newLabels.Add(v_WSASDemographics.Number_EligibleButNotTested, "number_eligible_not_tested");
            newLabels.Add(v_WSASDemographics.No_WSAS_Total, "percent_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.Number_No_WSAS_Total, "number_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.PctEcon, "percent_economically_disadvantaged");
            newLabels.Add(v_WSASDemographics.NumEcon, "number_economically_disadvantaged");
            newLabels.Add(v_WSASDemographics.PctLEP, "percent_students_with_disabilities");
            newLabels.Add(v_WSASDemographics.NumLEP, "number_limited_english_proficient");
            newLabels.Add(v_WSASDemographics.PctDisabled, "percent_students_with_disabilities");
            newLabels.Add(v_WSASDemographics.NumDisabled, "number_students_with_disabilities");
            newLabels.Add(v_WSASDemographics.PctAmInd, "percent_amerind");
            newLabels.Add(v_WSASDemographics.NumAmInd, "number_amerind");
            newLabels.Add(v_WSASDemographics.PctAsian, "percent_asian");
            newLabels.Add(v_WSASDemographics.NumAsian, "number_asian");
            newLabels.Add(v_WSASDemographics.PctBlack, "percent_black");
            newLabels.Add(v_WSASDemographics.NumBlack, "number_black");
            newLabels.Add(v_WSASDemographics.PctHisp, "percent_hispanic");
            newLabels.Add(v_WSASDemographics.NumHisp, "number_hispanic");
            newLabels.Add(v_WSASDemographics.PctWhite, "percent_white");
            newLabels.Add(v_WSASDemographics.NumWhite, "number_white");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_1, "percent_waa_swd_minimal");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_1, "number_waa_swd_minimal");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_2, "percent_waa_swd_basic");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_2, "number_waa_swd_basic");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_3, "percent_waa_swd_proficient");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_3, "number_waa_swd_proficient");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_4, "percent_waa_swd_advanced");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_4, "number_waa_swd_advanced");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Basic, "percent_waa_ell_basic");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Minimal, "percent_waa_ell_minimal");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Proficient, "percent_waa_ell_proficient");
            newLabels.Add(v_WSASDemographics.PctTotalWAADisabil, "percent_waa_swd_total_wkce");
            newLabels.Add(v_WSASDemographics.Number_TotalWAADisabil, "number_waa_swd_total_wkce");
            return newLabels;
        }
    }
}
