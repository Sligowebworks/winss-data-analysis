using System;
using System.Collections.Generic;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.DAL.WI;

namespace SligoCS.BL.WI
{
    public class QueryArgumentsWithDisagg
    {
        public delegate List<String> disAggValues();
        public delegate List<String> argSub();

        public Boolean ObeyForceDisAgg = false;

        /// <summary>
        /// List<String> of  Values when Dis-Aggregating</String>
        /// </summary>
        public disAggValues DisAggValues;
        /// <summary>
        /// Subroutine to determine List of Args (when not forcing DisAgg)</String>
        /// </summary>
        public argSub ArgSub;

        public bool ForceDisAgg
        {
            get { return (globals.SuperDownload.Key == SupDwnldKeys.True && ObeyForceDisAgg == true); }
        }

        private GlobalValues globals;

        public List<string> ArgList()
        {
            if (DisAggValues == null) throw new Exception("No DisAggValues delegate assigned;");
            if (ArgSub == null) throw new Exception ("No GetArgs delegate assigned;");

            return (ForceDisAgg)
                ? DisAggValues()
                : ArgSub();
        }

        public static implicit operator List<String>(QueryArgumentsWithDisagg o)
        {
            return o.ArgList();
        }

        public QueryArgumentsWithDisagg(GlobalValues GlobalValues)
        {
            globals = GlobalValues;
        }
    }
}
