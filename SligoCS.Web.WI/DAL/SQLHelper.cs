using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    /// <summary>
    /// This class helps the DAL classes to build a SQL WHERE clause.  It may eventually be promoted to 
    /// the base DAL (as opposed to the Wisconsin DAL), depending on whether other applications can re-use
    /// the functions contained in this class.
    /// </summary>
    public class SQLHelper
    {

        public enum WhereClauseJoiner
        {
            NONE,
            AND,
            OR
        }
        
        private const string WHERECLAUSECSV_FORMAT = " {0} ({1} in ({2}))";

        

        /// <summary>
        /// Use this function to converts a Generics list of <int> to a Generics list of <string>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertIntToString(int dataItem)
        {
            return dataItem.ToString();
        }




        public static string GetOrderByClause(List<string> orderBy)
        {
            string retval = string.Empty;
            string orderByCSV = ConvertToCSV(orderBy, false);
            if (orderByCSV.Length > 0)
                retval = string.Format(" ORDER BY {0}", orderByCSV);
            return retval;
        }

        

        /// <summary>
        /// Overload.  Creates a CSV string such as " AND (myFieldname in ('a','b','c'))"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string WhereClauseValuesInList(WhereClauseJoiner joiner, string fieldName, List<int> data)
        {
            List<string> stringData = data.ConvertAll<string>(ConvertIntToString);
            string retval = WhereClauseValuesInList(joiner, fieldName, stringData);
            return retval;
        }




        /// <summary>
        /// Implementor.  Creates a CSV string such as " AND (myFieldname in ('a','b','c'))"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string WhereClauseValuesInList(WhereClauseJoiner joiner, string fieldName, List<string> data)
        {
            string retval = string.Empty;
            if (data.Count > 0)
            {
                retval = string.Format(WHERECLAUSECSV_FORMAT, GetJoinerString(joiner), fieldName, ConvertToCSV(data, true));
            }

            return retval;
        }

        public static string WhereClauseEquals(WhereClauseJoiner joiner, string fieldname, string value)
        {
            string retval = string.Empty;
            if ((fieldname != string.Empty) && (value != string.Empty))
            {
                retval = string.Format(" {0} ({1} = '{2}')", GetJoinerString(joiner), fieldname, value);
            }
            return retval;
        }

        public static string WhereClauseNotEquals(WhereClauseJoiner joiner, string fieldname, string value)
        {
            string retval = string.Empty;
            if ((fieldname != string.Empty) && (value != string.Empty))
            {
                retval = string.Format(" {0} ({1} <> '{2}')", GetJoinerString(joiner), fieldname, value);
            }
            return retval;
        }


        /// <summary>
        /// Converts the given list into a comma separated values (CSV) list.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertToCSV(List<string> data, bool surroundWithSingleQuote)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (string s in data)
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                if (surroundWithSingleQuote)
                    sb.AppendFormat("'{0}'", s.ToString());
                else
                    sb.AppendFormat("{0}", s.ToString());
            }
            return sb.ToString();
        }




        /// <summary>
        /// Returns an integer range for a WHERE clause. If vals.Count is one, uses same value on both ends of range. 
        /// </summary>
        /// <param name="joiner"></param>
        /// <param name="fieldName"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static string WhereClauseSingleValueOrInclusiveRange(WhereClauseJoiner joiner, string fieldName, List<String> vals)
        {
            string retval = string.Empty;
            if (vals.Count > 2)
                throw new Exception("The list can only contain at most 2 values.");

            if (vals.Count == 1)
                retval = WhereClauseValuesInList(joiner, fieldName, vals);
            else if (vals.Count == 2)
                retval = WhereClauseMinAndMaxInclusive(joiner, fieldName, vals[0], vals[1]);

            return retval;
        }


        /// <summary>
        /// Implementor.  Creates a string such as " AND ((gradeCode >= 16) AND (gradeCode <= 64))"
        /// </summary>
        /// <param name="joiner"></param>
        /// <param name="fieldName"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static string WhereClauseMinAndMaxInclusive(WhereClauseJoiner joiner, string fieldName, string min, string max)
        {
            string retval = string.Format(" {0} (({1} >= {2}) AND ({1} <= {3}))", GetJoinerString(joiner), fieldName, min, max);
            return retval;
        }

        public static string GetJoinerString(WhereClauseJoiner joiner)
        {
            string retval;
            if (joiner == WhereClauseJoiner.NONE)
                retval = " ";
            else
                retval = joiner.ToString();

            return retval;
        }
        public static string SelectStarFromWhereFormat(String dbObject)
        {
            return String.Format("SELECT * FROM {0} WHERE ", dbObject);
        }
    }
}
