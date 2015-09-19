using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct LevelKeys
	{
		public const string All = "All Levels";
		public const string Advanced = "Advanced";
		public const string AdvancedProficient = "Advanced + Proficient";
		public const string AdvProfBas = "Advanced + Proficient + Basic";
		public const string BasicMinSkillEng = "Basic + Min Perf + WAA SwD/ELL + No WSAS";
		public const string WAA_SwD = "WAA SwD";
		public const string WAA_ELL = "WAA ELL";
		public const string NoWSAS = "No WSAS";
		public const string MinPerfNotTested = "Min Perf + Not Tested on WRCT";
		public const string MinimumPerf = "Min Perf";
		public const string NotTested = "Not Tested on WRCT";
	}

	public class Level : ParameterValues
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

			range.Add(LevelKeys.All, "ALL");
			range.Add(LevelKeys.Advanced, "ADV");
			range.Add(LevelKeys.AdvancedProficient, "A-P");
			range.Add(LevelKeys.AdvProfBas, "A-P-B");
			range.Add(LevelKeys.BasicMinSkillEng, "B-M-NT");
			range.Add(LevelKeys.WAA_SwD, "SWD");
			range.Add(LevelKeys.WAA_ELL, "ELL");
			range.Add(LevelKeys.NoWSAS, "No-W");
			range.Add(LevelKeys.MinPerfNotTested, "M-NT-wrct");
			range.Add(LevelKeys.MinimumPerf, "MIN");
			range.Add(LevelKeys.NotTested, "NT-wrct");

			return range;
		}
	}
}
