using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.BL.WI
{
    /// <summary>
    /// This class contains functions that help when using Generics Lists.
    /// </summary>
    public class GenericsListHelper
    {

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="val1"></param>
        /// <returns></returns>
        public static List<int> GetPopulatedList(int val1)
        {
            return GetPopulatedList(val1, int.MinValue, int.MinValue, int.MinValue, int.MinValue);
        }

        /// <summary>
        /// Overload. Create a new Generics List, populated with the given values.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static List<int> GetPopulatedList(int val1, int val2)
        {
            return GetPopulatedList(val1, val2, int.MinValue, int.MinValue, int.MinValue);
        }

        /// <summary>
        /// Implementor.  Create a new Generics List, populated with the given values.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <param name="val3"></param>
        /// <param name="val4"></param>
        /// <param name="val5"></param>
        /// <returns></returns>
        public static List<int> GetPopulatedList(int val1, int val2, int val3, int val4, int val5)
        {
            List<int> retval = new List<int>();
            if (val1 != int.MinValue) retval.Add(val1);
            if (val2 != int.MinValue) retval.Add(val2);
            if (val3 != int.MinValue) retval.Add(val3);
            if (val4 != int.MinValue) retval.Add(val4);
            if (val5 != int.MinValue) retval.Add(val5);
            return retval;
        }

        public static List<int> GetPopulatedList(int val1, 
            int val2, int val3, int val4, int val5,int val6)
        {
            List<int> retval = new List<int>();
            if (val1 != int.MinValue) retval.Add(val1);
            if (val2 != int.MinValue) retval.Add(val2);
            if (val3 != int.MinValue) retval.Add(val3);
            if (val4 != int.MinValue) retval.Add(val4);
            if (val5 != int.MinValue) retval.Add(val5);
            if (val6 != int.MinValue) retval.Add(val6);
            return retval;
        }
        public static List<int> GetPopulatedList(int val1,
            int val2, int val3, int val4, int val5, int val6, int val7)
        {
            List<int> retval = new List<int>();
            if (val1 != int.MinValue) retval.Add(val1);
            if (val2 != int.MinValue) retval.Add(val2);
            if (val3 != int.MinValue) retval.Add(val3);
            if (val4 != int.MinValue) retval.Add(val4);
            if (val5 != int.MinValue) retval.Add(val5);
            if (val6 != int.MinValue) retval.Add(val6);
            if (val7 != int.MinValue) retval.Add(val7);
            return retval;
        }

    }
}
