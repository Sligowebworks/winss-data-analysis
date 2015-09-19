using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct IncidentKeys
	{
		public const string Rate = "Incident Rate";
		public const string Consequences = "Disciplinary Consequences";
	}

	public class Incident : ParameterValues
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

			range.Add(IncidentKeys.Rate, "RATE");
			range.Add(IncidentKeys.Consequences, "CONS");

			return range;
		}
	}
}
