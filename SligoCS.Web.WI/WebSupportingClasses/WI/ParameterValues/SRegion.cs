using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SRegionKeys
	{
		public const string County = "Country";
		public const string AthleticConf = "Athlectic Conference";
		public const string CESA = "CESA";
	}

	public class SRegion : ParameterValues
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

			range.Add(SRegionKeys.County, "1");
			range.Add(SRegionKeys.AthleticConf, "2");
			range.Add(SRegionKeys.CESA, "3");

			return range;
		}
	}
}
