using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct ShowKeys
	{
		public const string Extracurricular = "Extra/Co-curricular Activites";
		public const string Community = "Community Activities";
	}

	public class Show : ParameterValues
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

			range.Add(ShowKeys.Extracurricular, "EXTRA");
			range.Add(ShowKeys.Community, "COMM");

			return range;
		}
	}
}
