using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALAgency : DALWIBase
    {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM v_AgencyFullDistinct");
            sql.Append(" WHERE ");

            //must use the real fullkey, not "org level masked"
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.NONE, "FULLKEY", Marshaller.GlobalValues.FULLKEY));

            //sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "year", new List<String>(new String[] { Marshaller.GlobalValues.Year.ToString() })));
            //sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "year", new List<int>(new int[] {Marshaller.AgencyYear })));

            return sql.ToString();
        }

        public String STYP
        {
            get 
            { 
                string type = GetStringColumn(v_AgencyFullDistinct.SchoolType);
                // some charter schools have Zero SchoolTYpe instead of Null:
                return (type == "0") ? null : type;
            }
        }
        public String Name
        {
            get { return GetStringColumn(v_AgencyFullDistinct.Name); }
        }
        public String CountyName
        {
            get { return GetStringColumn(v_AgencyFullDistinct.CountyName); }
        }
        public String County
        {
            get { return GetStringColumn(v_AgencyFullDistinct.County); }
        }
        public String ConferenceKey
        {
            get { return GetStringColumn(v_AgencyFullDistinct.ConferenceKey); }
        }
        public String CESA
        {
            get { return GetStringColumn(v_AgencyFullDistinct.CESA); }
        }
        public String District
        {
            get { return GetStringColumn(v_AgencyFullDistinct.District); }
        }
        public String DistrictName
        {
            get { return GetStringColumn(v_AgencyFullDistinct.DistrictName); }
        }
        public String Schoolname
        {
            get {
                String name = GetStringColumn(v_AgencyFullDistinct.SchoolName);
                return (name.Trim() != "NONE")? name : String.Empty;
            }
        }
        public String DistrictURL
        {
            get { return GetStringColumn(v_AgencyFullDistinct.DistrictWebAddress); }
        }
        public String SchoolURL
        {
            get { return GetStringColumn(v_AgencyFullDistinct.SchoolWebAddress); }
        }
        public int HighGrade
        {
            get { return int.Parse(GetStringColumn(v_AgencyFullDistinct.HighGrade)); }
        }
        public int LowGrade
        {
            get 
            { 
                return int.Parse(GetStringColumn(v_AgencyFullDistinct.LowGrade)); }
        }
    }
}
