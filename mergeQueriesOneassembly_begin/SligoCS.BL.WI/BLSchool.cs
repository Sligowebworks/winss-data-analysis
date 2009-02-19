using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLSchool : BLWIBase
    {

        public v_Schools GetSchool(string fullKey, int year)
        {
            DALSchools school = new DALSchools();
            v_Schools ds = school.GetSchool(fullKey, year);
            return ds;
        }

        public string GetSchoolName(string fullKey, int year)
        {
            string retval = string.Empty;
            v_Schools ds = GetSchool(fullKey, year);
            if (ds._v_Schools.Rows.Count == 1)
            {
                retval = ds._v_Schools[0].Name.Trim();
            }
            return retval;
        }
    }
}
