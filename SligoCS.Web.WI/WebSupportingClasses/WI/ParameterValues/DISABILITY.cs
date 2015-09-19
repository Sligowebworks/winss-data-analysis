using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct DISABILITYKeys
	{
		public const string On = "%  Students w/ Disabilites";
		public const string Off = "% Students w/ Disabilites";
	}

	public class DISABILITY : ParameterValues
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

			range.Add(DISABILITYKeys.On, "Y");
			range.Add(DISABILITYKeys.Off, "N");

			return range;
		}
	}
}
