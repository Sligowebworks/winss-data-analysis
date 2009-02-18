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



        /// <summary>
        /// Overload.  Creates a CSV string such as " AND (myFieldname in ('a','b','c'))"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string WhereClauseCSV(WhereClauseJoiner joiner, string fieldName, List<int> data)
        {
            List<string> stringData = data.ConvertAll<string>(ConvertIntToString);
            string retval = WhereClauseCSV(joiner, fieldName, stringData);
            return retval;
        }


        /// <summary>
        /// Implementor.  Creates a CSV string such as " AND (myFieldname in ('a','b','c'))"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string WhereClauseCSV(WhereClauseJoiner joiner, string fieldName, List<string> data)
        {
            string retval = string.Empty;
            if (data.Count > 0)
            {
                retval = string.Format(WHERECLAUSECSV_FORMAT, GetJoinerString(joiner), fieldName, ConvertToCSV(data, true));
            }

            return retval;
        }

        /// <summary>
        /// Converts the given list into a comma separated values (CSV) list.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ConvertToCSV(List<string> data, bool surroundWithSingleQuote)
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
        /// Overload.  Created this function for convienience.
        /// </summary>
        /// <param name="joiner"></param>
        /// <param name="fieldName"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public string WhereClauseMinAndMaxInclusive(WhereClauseJoiner joiner, string fieldName, List<int> vals)
        {
            string retval = string.Empty;
            if (vals.Count > 2)
                throw new Exception("The list can only contain at most 2 values.");

            if (vals.Count == 1)
                retval = WhereClauseMinAndMaxInclusive(joiner, fieldName, vals[0], vals[0]);
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
        public string WhereClauseMinAndMaxInclusive(WhereClauseJoiner joiner, string fieldName, int min, int max)
        {
            string retval = string.Format(" {0} (({1} >= {2}) AND ({1} <= {3}))", GetJoinerString(joiner), fieldName, min, max);
            return retval;
        }

        private string GetJoinerString(WhereClauseJoiner joiner)
        {
            string retval;
            if (joiner == WhereClauseJoiner.NONE)
                retval = " ";
            else
                retval = joiner.ToString();

            return retval;
        }

    }
}
