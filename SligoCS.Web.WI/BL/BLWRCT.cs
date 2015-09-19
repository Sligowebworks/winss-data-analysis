using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;

namespace SligoCS.BL.WI
{
    public class BLWRCT : BLWIBase
    {
        public v_WRCT GetWRCT(string fullKey)
        {
            DALWRCT wrct = new DALWRCT();
            v_WRCT ds = wrct.GetWRCT(fullKey);
            return ds;
        }
    }
}
