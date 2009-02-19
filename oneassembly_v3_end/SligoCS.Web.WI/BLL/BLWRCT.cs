using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BLL.WI
{
    public class BLWRCT : BLWIBase
    {
        public v_WRCT GetWRCT(string fullKey)
        {
            DALWRCT wrct = new DALWRCT();
            v_WRCT ds = wrct.GetWRCT(FullKeyEncode(fullKey));
            return ds;
        }
    }
}
