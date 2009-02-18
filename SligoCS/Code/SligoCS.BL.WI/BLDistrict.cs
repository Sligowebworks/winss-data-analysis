using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{
    public class BLDistrict : EntityWIBase
    {

        public v_Districts GetDistric(string fullKey, int year)
        {
            DALDistrict district = new DALDistrict();
            string masked = GetMaskedFullkey(fullKey, OrgLevel.District);
            v_Districts ds = district.GetDistrict(masked, year);
            return ds;
        }

        public string GetDistrictName(string fullKey, int year)
        {
            string retval = string.Empty;
            v_Districts ds = GetDistric(fullKey, year);
            if (ds._v_Districts.Rows.Count == 1)
            {
                retval = ds._v_Districts[0].Name.Trim();
            }
            return retval;
        }
    }
}
