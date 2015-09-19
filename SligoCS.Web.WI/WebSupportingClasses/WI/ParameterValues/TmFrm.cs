using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct TmFrmKeys
	{
		public const string All = "All Timeframes";
		public const string FourYear = "Four-Year Rate";
		public const string SixYear = "Six-Year Rate";
		public const string Legacy = "Legacy Rate (By Age 21)";
	}

	public class TmFrm : ParameterValues
	{
		private static SerializableDictionary<String, String> range;

		public override SerializableDictionary<string, string> Range
		{
			get
			{
				if (range == null) range = initRange();
				return range;
			}
		}
		protected  SerializableDictionary<String, String> initRange()
		{
			SerializableDictionary<String, String> range = new SerializableDictionary<String, String>();

			range.Add(TmFrmKeys.All, "A");
			range.Add(TmFrmKeys.FourYear, "4");
			range.Add(TmFrmKeys.SixYear, "6");
			range.Add(TmFrmKeys.Legacy, "L");

			return range;
		}
	}
}
