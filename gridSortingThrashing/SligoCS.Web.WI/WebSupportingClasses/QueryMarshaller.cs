using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.DAL.WI;

namespace SligoCS.BL.WI
{
    public class QueryMarshaller
    {
        #region properties (and constructors)
        private GlobalValues globals;
        private DALWIBase database;

        public List<int> stypList, sexCodes, raceCodes,
            disabilityCodes, gradeCodes, econDisadvCodes, 
            ELPCodes;
        public List<int> years;
        public List<String> fullkeylist;
        public List<String> SchoolFullkey;
        public List<String> DistrictFullkey;
        public List<String> StateFullkey;
        public List<String> orderByList;

        public String clauseForCompareSelected;

        private DataColumnCollection dataColumns;

        public DALWIBase Database
        {
            get 
            { 
                if (database == null) throw new Exception("DALWIBASE obj not set");
                return database;
            }
            set { database = value; }
        }

        public DataColumnCollection DataColumns
        {
            get
            {
                if (Database.DataSet.Tables.Count > 0)
                {
                    dataColumns = Database.DataSet.Tables[0].Columns;
                }
                else
                {
                    dataColumns = new DataTable().Columns;
                }
                return dataColumns;
            }
        }

        public Boolean compareSelectedFullKeys
        {
            get
            {
                bool compare =
                    (globals.CompareTo.Key == CompareToKeys.SelSchools
                    || globals.CompareTo.Key == CompareToKeys.SelDistricts);

                if (globals.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                    compare = false;

                return compare;
            }
        }

        public QueryMarshaller()
        {
            globals = new GlobalValues();
        }

       public  QueryMarshaller(GlobalValues initGlobals)
        {
            globals = initGlobals;
        }
        #endregion //properties
        /// <summary>
        /// Marshalls and Executes the Query, be sure that prerequisites have been assigned before calling.
        /// </summary>
        public DataSet Query(DALWIBase Database)
        {
            InitLists(); // might have already been called, but will catch any changes
            globals.SQL = Database.SQL = Database.BuildSQL(this);
            return Database.Query();
        }
        /// <summary>
        /// analyses Application State and marshalls Lists used in Querying.
        /// </summary>
        public void InitLists()
        {
            stypList = GetSchoolTypesList(globals.STYP);
            sexCodes = ( globals.Group.Key == GroupKeys.Gender)?
                    GenericsListHelper.GetPopulatedList(1, 2)
                    : GenericsListHelper.GetPopulatedList(9);
            raceCodes =  (globals.Group.Key == GroupKeys.Race)?
                    GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5)
                    : GenericsListHelper.GetPopulatedList(9);
            disabilityCodes = (globals.Group.Key == GroupKeys.Disability)?
                    GenericsListHelper.GetPopulatedList(1, 2)
                    : GenericsListHelper.GetPopulatedList(9);
            if (globals.Group.Key == GroupKeys.Grade)
            {
                gradeCodes = (globals.OrgLevel.Key == OrgLevelKeys.School) ?
                        GenericsListHelper.GetPopulatedList(52, 64)
                        : GenericsListHelper.GetPopulatedList(16, 64);
            }
            else
            {
                gradeCodes = GenericsListHelper.GetPopulatedList(globals.Grade);
            }
            econDisadvCodes = (globals.Group.Key == GroupKeys.EconDisadv) ?
                    GenericsListHelper.GetPopulatedList(1, 2)
                    : GenericsListHelper.GetPopulatedList(9);
            ELPCodes = ( globals.Group.Key == GroupKeys.EngLangProf)?
                    GenericsListHelper.GetPopulatedList(1, 2)
                    : GenericsListHelper.GetPopulatedList(9);
            years = (globals.CompareTo.Key != CompareToKeys.Years) ?
                    GenericsListHelper.GetPopulatedList(globals.Year)
                    : GenericsListHelper.GetPopulatedList(globals.TrendStartYear, globals.Year);
            
            BuildFullKeyList(globals.FULLKEY);

            clauseForCompareSelected = BuildClauseForCompareToSelected();

            //orderByList = BuildOrderByList(DataColumns);
        }

        public void BuildFullKeyList(String fullkey)
        {
            List<String> keylist = new List<string>();
            SchoolFullkey = new List<string>();
            DistrictFullkey = new List<string>();
            StateFullkey = new List<string>();

            fullkey = FullKeyUtils.GetMaskedFullkey(fullkey, globals.OrgLevel);
            
            if ((globals.CompareTo.Key == CompareToKeys.Years) || (globals.CompareTo.Key == CompareToKeys.Current))
                //When comparing to Prior Years or Current School Data, 
                //  the WHERE clause should just contain the given (nothing else).
                keylist.Add(fullkey);

            else if (globals.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                //always add the State fullkey to the list.
                keylist.Add(FullKeyUtils.StateFullKey(fullkey));
                DistrictFullkey.Add(FullKeyUtils.DistrictFullKey(fullkey));
                StateFullkey.Add(FullKeyUtils.StateFullKey(fullkey));
                
                if (globals.OrgLevel.Key == OrgLevelKeys.School)
                {
                    //org level is school.  also add school & district full keys.
                    keylist.Add(FullKeyUtils.SchoolFullKey(fullkey));
                    keylist.Add(FullKeyUtils.DistrictFullKey(fullkey));
                    SchoolFullkey.Add(FullKeyUtils.SchoolFullKey(fullkey));
                }
                else if (globals.OrgLevel.Key == OrgLevelKeys.District)
                {
                    //org level is district.  Add District fullkey.
                    keylist.Add(FullKeyUtils.DistrictFullKey(fullkey));
                }
            }
            else if (globals.CompareTo.Key == CompareToKeys.SelSchools || globals.CompareTo.Key == CompareToKeys.SelDistricts)
            {
                //add the original fullkey
                keylist.Add(fullkey);

                // no need for other fullkeys if In all Schools Or Districts
                if (globals.S4orALL.Key== S4orALLKeys.FourSchoolsOrDistrictsIn &&
                    globals.SSchoolFullKeys != null)
                {
                    //also add all of the CompareToSchoolsOrDistrict, but masked appropriately.
                    foreach (String compareToSchool in globals.CompareSelectedFullKeys)
                    {
                        String maskedFullKey = FullKeyUtils.GetMaskedFullkey(compareToSchool, globals.OrgLevel);
                        keylist.Add(maskedFullKey);
                    }
                }
            }

            fullkeylist =  FullKeyUtils.FullKeyDecode(keylist);
        }
        delegate int getIntDelegate(String str);
        public List<int> GetSchoolTypesList(STYP schoolType)
        {
            List<int> schoolTypes;

            if (schoolType.Key != STYPKeys.AllTypes)
            {
                schoolTypes = GenericsListHelper.GetPopulatedList(int.Parse(schoolType.Value));
            }
            else
            {
                getIntDelegate getSchoolTypeInt = delegate (String str){return int.Parse(schoolType.Range[str]); };
                schoolTypes = GenericsListHelper.GetPopulatedList(getSchoolTypeInt(STYPKeys.Elem),
                    getSchoolTypeInt(STYPKeys.Mid),
                    getSchoolTypeInt(STYPKeys.Hi),
                    getSchoolTypeInt(STYPKeys.ElSec),
                    int.MinValue);
            }
            return schoolTypes;
        }
        public String BuildClauseForCompareToSelected()
        {
            String clause = "";
            String comparison =
                (globals.OrgLevel.Key == OrgLevelKeys.School) ?
                "<>" : "=";

            if (globals.SRegion.Key == SRegionKeys.County)
                clause = CompareSelectedClauseTemplate(comparison, "County", globals.SCounty);

            else if (globals.SRegion.Key == SRegionKeys.CESA)
                clause = CompareSelectedClauseTemplate(comparison, "CESA", globals.SCESA);

            else if (globals.SRegion.Key == SRegionKeys.AthleticConf)
                clause = CompareSelectedClauseTemplate(comparison, "ConferenceKey", globals.SAthleticConf);

            return clause;
        }

        private String CompareSelectedClauseTemplate( String Operator, String Name, String Value)
        {
            String template = " ( ( {1} in ('{2}') and right(fullkey,1){0}'X' ) or FullKey in ('{3}')) ";
            
            return String.Format(template,
               Operator, Name, Value, 
                FullKeyUtils.GetMaskedFullkey(globals.FULLKEY, globals.OrgLevel)
                );
        }
        private List<String> BuildOrderByList(DataColumnCollection dataColumns)
        {
            List<String> orderby = new List<String>();

            //PRIMARY SORT: The primary sort key for the Order By is set by Compare To:

            if (globals.CompareTo.Key == CompareToKeys.Years)
            {
                if (dataColumns.Contains(OrderByCols.year.ToString()))
                    orderby.Add(OrderByCols.year.ToString() + " DESC");
            }
            else if (globals.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                if (dataColumns.Contains(OrderByCols.OrgLevelLabel.ToString()))
                    orderby.Add(OrderByCols.OrgLevelLabel.ToString());
            }

            else if (globals.CompareTo.Key == CompareToKeys.SelSchools)
            {
                if ((dataColumns.Contains(OrderByCols.fullkey.ToString()))
                    && (dataColumns.Contains(OrderByCols.OrgLevelLabel.ToString()))
                    && (dataColumns.Contains(OrderByCols.Name.ToString())))
                {
                    string temp = string.Format("(case {0} when '{1}' then {2} else ltrim({3}) end)",
                        OrderByCols.fullkey.ToString(),
                        FullKeyUtils.GetMaskedFullkey(globals.FULLKEY, globals.OrgLevel),
                        OrderByCols.OrgLevelLabel.ToString(),
                        OrderByCols.Name.ToString());
                    orderby.Add(temp);
                }
            }
            else if (globals.CompareTo.Key == CompareToKeys.Current)
            {
                if (dataColumns.Contains(OrderByCols.fullkey.ToString()))
                    orderby.Add(OrderByCols.fullkey.ToString());
            }

            //Comments block from Bug #357:
            //SECONDARY SORT: 

            if (dataColumns.Contains(OrderByCols.SchoolTypeLabel.ToString()))
                orderby.Add(OrderByCols.SchoolTypeLabel.ToString());

            if (dataColumns.Contains(OrderByCols.SexCode.ToString()))
                orderby.Add(OrderByCols.SexCode.ToString());
            if (dataColumns.Contains(OrderByCols.RaceCode.ToString()))
                orderby.Add(OrderByCols.RaceCode.ToString());
            if (dataColumns.Contains(OrderByCols.GradeCode.ToString()))
                orderby.Add(OrderByCols.GradeCode.ToString());
            if (dataColumns.Contains(OrderByCols.DisabilityCode.ToString()))
                orderby.Add(OrderByCols.DisabilityCode.ToString());

            return orderby;
        }
        public GlobalValues GlobalValues
        {
            get { return globals; }
        }
    }
}
