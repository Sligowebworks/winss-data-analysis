using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct GraphFileKeys
	{
		public const string BLANK_REDIRECT_PAGE = "*.aspx";
		public const string GRAPH_FILE_RETENTION = "GridPage.aspx";
		public const string GRAPH_FILE_HIGHSCHOOLCOMPLETION = "HSCompletionPage.aspx";
		public const string GRAPH_FILE_ATTENDANCE = "AttendancePage.aspx";
		public const string GRAPH_FILE_TRUANCY = "Truancy.aspx";
		public const string GRAPH_FILE_STAFF = "StaffPage.aspx";
		public const string GRAPH_FILE_MONEY = "MoneyPage.aspx";
		public const string GRAPH_FILE_AP = "APTestsPage.aspx";
		public const string GRAPH_FILE_ACT = "ACTPage.aspx";
		public const string GRAPH_FILE_GGRADREQS = "GradReqsPage.aspx";
		public const string GRAPH_FILE_TEACHERQUALIFICATIONS = "TeacherQualifications.aspx";
		public const string GRAPH_FILE_DROPOUTS = "DropOuts.aspx";
		public const string GRAPH_FILE_ETHNICENROLL = "EthnicEnroll.aspx";
		public const string GRAPH_FILE_SUSPEXPINCIDENTS = "SuspExpIncidents.aspx";
		public const string GRAPH_FILE_GEXPSERVICES = "GExpServices.aspx";
		public const string GRAPH_FILE_GROUPS = "Groups.aspx";
		public const string GRAPH_FILE_DISABILITIES = "Disabilities.aspx";
		public const string GRAPH_FILE_GWRCT = "GWRCT.aspx";
		public const string GRAPH_FILE_GGRADPLAN = "GGradPlan.aspx";
		public const string GRAPH_FILE_StateTests = "StateTestsPerformance.aspx";
	}

	public class GraphFile : ParameterValues
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

			range.Add(GraphFileKeys.BLANK_REDIRECT_PAGE, "BlankPageUrl");
			range.Add(GraphFileKeys.GRAPH_FILE_RETENTION, "RETENTION");
			range.Add(GraphFileKeys.GRAPH_FILE_HIGHSCHOOLCOMPLETION, "HIGHSCHOOLCOMPLETION");
			range.Add(GraphFileKeys.GRAPH_FILE_ATTENDANCE, "ATTENDANCE");
			range.Add(GraphFileKeys.GRAPH_FILE_TRUANCY, "TRUANCY");
			range.Add(GraphFileKeys.GRAPH_FILE_STAFF, "STAFF");
			range.Add(GraphFileKeys.GRAPH_FILE_MONEY, "MONEY");
			range.Add(GraphFileKeys.GRAPH_FILE_AP, "AP");
			range.Add(GraphFileKeys.GRAPH_FILE_ACT, "ACT");
			range.Add(GraphFileKeys.GRAPH_FILE_GGRADREQS, "GGRADREQS");
			range.Add(GraphFileKeys.GRAPH_FILE_TEACHERQUALIFICATIONS, "TEACHERQUALIFICATIONS");
			range.Add(GraphFileKeys.GRAPH_FILE_DROPOUTS, "DROPOUTS");
			range.Add(GraphFileKeys.GRAPH_FILE_ETHNICENROLL, "ETHNICENROLL");
			range.Add(GraphFileKeys.GRAPH_FILE_SUSPEXPINCIDENTS, "SUSPEXPINCIDENTS");
			range.Add(GraphFileKeys.GRAPH_FILE_GEXPSERVICES, "GEXPSERVICES");
			range.Add(GraphFileKeys.GRAPH_FILE_GROUPS, "GROUPS");
			range.Add(GraphFileKeys.GRAPH_FILE_DISABILITIES, "DISABILITIES");
			range.Add(GraphFileKeys.GRAPH_FILE_GWRCT, "GWRCT");
			range.Add(GraphFileKeys.GRAPH_FILE_GGRADPLAN, "GGRADPLAN");
			range.Add(GraphFileKeys.GRAPH_FILE_StateTests, "stp");

			return range;
		}
	}
}
