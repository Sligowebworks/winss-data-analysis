using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.BL.WI
{
    public class BLWRCT : BLWIBase
    {
        public v_WRCT GetWRCT(string fullKey)
        {
            DALWRCT wrct = new DALWRCT();
            v_WRCT ds = wrct.GetWRCT(FullKeyUtils.FullKeyEncode(fullKey));
            return ds;
        }
    }
}
