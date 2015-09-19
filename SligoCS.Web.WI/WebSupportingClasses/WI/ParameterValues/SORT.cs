using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SORTKeys
	{
		public const string Advanced = "%  Advanced";
		public const string AdvPlusProf = "% Advanced + Proficient";
	}

	public class SORT : ParameterValues
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

			range.Add(SORTKeys.Advanced, "A");
			range.Add(SORTKeys.AdvPlusProf, "AP");

			return range;
		}
	}
}
