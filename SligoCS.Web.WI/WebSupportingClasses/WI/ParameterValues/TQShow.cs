using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct TQShowKeys
	{
		public const string WisconsinLicenseStatus = "Wisconsin License Status";
		public const string DistrictExperience = "District Experience";
		public const string TotalExperience = "Total Experience";
		public const string HighestDegree = "Highest Degree";
		public const string ESEAQualified = "ESEA Qualified";
	}

	public class TQShow : ParameterValues
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

			range.Add(TQShowKeys.WisconsinLicenseStatus, "LICSTAT");
			range.Add(TQShowKeys.DistrictExperience, "DISTEXP");
			range.Add(TQShowKeys.TotalExperience, "TOTEXP");
			range.Add(TQShowKeys.HighestDegree, "DEGR");
			range.Add(TQShowKeys.ESEAQualified, "ESEAHIQ");

			return range;
		}
	}
}
