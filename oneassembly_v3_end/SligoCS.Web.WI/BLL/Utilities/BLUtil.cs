using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.BLL.WI.Utilities
{
    public class BLUtil
    {
        public static string GetCommaDeliFullKeyString(string input)
        {
            string result = string.Empty;
            if (input != null && input.Length > 11)
            {
                result = "'" + input.Substring(0, 12) + "'";
                if (input.Length > 23)
                {
                    result = result + ", '" + input.Substring(12, 12) + "'";
                    if (input.Length > 35)
                    {
                        result = result + ",'" + input.Substring(24, 12) + "'";
                        if (input.Length > 47)
                        {
                            result = result + ",'" + input.Substring(36, 12) + "'";
                        }
                    }

                }
            }
            return result;
        }

        public static List<string> GetFullKeyList(string input)
        {
            List<string> result = new List<string>();

            if (input != null && input.Length > 11)
            {
                result.Add ( input.Substring(0, 12) );
                if (input.Length > 23)
                {
                    result.Add ( input.Substring(12, 12) );
                    if (input.Length > 35)
                    {
                        result.Add ( input.Substring(24, 12));
                        if (input.Length > 47)
                        {
                            result.Add(input.Substring(36, 12));
                        }
                    }

                }
            }
            return result;
        }
    }
}
