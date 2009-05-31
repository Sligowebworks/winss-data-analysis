using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct TQTeacherVariableKeys
	{
		public const string WiscLicense = "&#37; Full Wisconsin License";
		public const string EmergencyLic = "&#37; Emergency Wisconsin License";
		public const string NoLicense = "&#37; No License for Assignment";
		public const string DistrictExp = "&#37; 5 or More Years District Experience";
		public const string TotalExp = "&#37; 5 or MorevYears Total Experience";
		public const string Degree = "&#37; Masters or Higher Degree";
		public const string ESEA = "&#37; ESEA Qualified";
	}

	public class TQTeacherVariable : ParameterValues
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

			range.Add(TQTeacherVariableKeys.WiscLicense, "LICFULL");
			range.Add(TQTeacherVariableKeys.EmergencyLic, "LICEMER");
			range.Add(TQTeacherVariableKeys.NoLicense, "LICNO");
			range.Add(TQTeacherVariableKeys.DistrictExp, "DISTEXP");
			range.Add(TQTeacherVariableKeys.TotalExp, "TOTEXP");
			range.Add(TQTeacherVariableKeys.Degree, "DEGR");
			range.Add(TQTeacherVariableKeys.ESEA, "ESEAHIQ");

			return range;
		}
	}
}
