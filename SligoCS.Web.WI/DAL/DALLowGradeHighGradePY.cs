using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALLowGradeHighGradePY : DALWIBase
    {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            Marshaller.InitFullkeyList();

            sql.Append("select 'LowGradePY' = isnull(min(LowGrade),'12'), 'HighGradePY' = isnull(max(HighGrade),'64') FROM [tblAgencyFull] where ");

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.NONE, "FullKey"));

            return sql.ToString();
        }
        public System.Collections.Hashtable GetLowGradeHiGradePY(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            System.Collections.Hashtable LowGrHiGr = new System.Collections.Hashtable();
            
            Marshaller.GlobalValues.SQL = this.BuildSQL(Marshaller);
            Marshaller.AssignQuery(this, Marshaller.GlobalValues.SQL);
            
            if (Table.Rows.Count < 1)
            {
                if (Marshaller.GlobalValues.TraceLevels > 0) throw new Exception("query returned empty;");

                return null;
            }
            LowGrHiGr["low"] = GetStringColumn("LowGradePY");
            LowGrHiGr["hi"] = GetStringColumn("HighGradePY");

            return LowGrHiGr;
        }
    }
}
