using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct GraphFileKeys
	{
		public const string BLANK_REDIRECT_PAGE = "*.aspx";
		public const string RETENTION = "Retention.aspx";
		public const string HIGHSCHOOLCOMPLETION = "HSCompletionPage.aspx";
		public const string ATTENDANCE = "AttendancePage.aspx";
		public const string TRUANCY = "Truancy.aspx";
		public const string STAFF = "StaffPage.aspx";
		public const string MONEY = "MoneyPage.aspx";
		public const string AP = "APTestsPage.aspx";
		public const string ACT = "ACTPage.aspx";
		public const string GGRADREQS = "GradReqsPage.aspx";
		public const string TEACHERQUALIFICATIONS = "TeacherQualifications.aspx";
		public const string TQSCATTERPLOT = "TeacherQualificationsScatterPlot.aspx";
		public const string DROPOUTS = "DropOuts.aspx";
		public const string ETHNICENROLL = "EthnicEnroll.aspx";
		public const string SUSPEXPINCIDENTS = "SuspExpIncidents.aspx";
		public const string SUSPENSIONS = "Suspensions.aspx";
		public const string SUSPENSIONSDAYSLOST = "SuspensionsDaysLost.aspx";
		public const string EXPULSIONS = "Expulsions.aspx";
		public const string EXPULSIONSDAYSLOST = "ExpulsionsDaysLost.aspx";
		public const string GEXPSERVICES = "ExpServices.aspx";
		public const string GEXPLENGTH = "ExpLength.aspx";
		public const string GEXPRETURNS = "ExpReturns.aspx";
		public const string GROUPS = "GroupEnroll.aspx";
		public const string DISABILITIES = "DisabilEnroll.aspx";
		public const string GWRCT = "WRCTPerformance.aspx";
		public const string POSTGRADPLAN = "PostGradIntentPage.aspx";
		public const string StateTests = "StateTestsPerformance.aspx";
		public const string StateTestsScatter = "StateTestsScatterplot.aspx";
		public const string StateTestsSimilar = "StateTestsSimilarSchools.aspx";
		public const string CoursesOffered = "CoursesOffer.aspx";
		public const string CoursesTaken = "CoursesTaking.aspx";
		public const string ActivityOffer = "ActivitiesOffer.aspx";
		public const string ActivitiesPartic = "ActivitiesParticipate.aspx";
		public const string CompareContinuing = "ComparePerformance.aspx";
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
			range.Add(GraphFileKeys.RETENTION, "RETENTION");
			range.Add(GraphFileKeys.HIGHSCHOOLCOMPLETION, "HIGHSCHOOLCOMPLETION");
			range.Add(GraphFileKeys.ATTENDANCE, "ATTENDANCE");
			range.Add(GraphFileKeys.TRUANCY, "TRUANCY");
			range.Add(GraphFileKeys.STAFF, "STAFF");
			range.Add(GraphFileKeys.MONEY, "MONEY");
			range.Add(GraphFileKeys.AP, "AP");
			range.Add(GraphFileKeys.ACT, "ACT");
			range.Add(GraphFileKeys.GGRADREQS, "GGRADREQS");
			range.Add(GraphFileKeys.TEACHERQUALIFICATIONS, "TEACHERQUALIFICATIONS");
			range.Add(GraphFileKeys.TQSCATTERPLOT, "TQSCATTER");
			range.Add(GraphFileKeys.DROPOUTS, "DROPOUTS");
			range.Add(GraphFileKeys.ETHNICENROLL, "ETHNICENROLL");
			range.Add(GraphFileKeys.SUSPEXPINCIDENTS, "SUSPEXPINCIDENTS");
			range.Add(GraphFileKeys.SUSPENSIONS, "SUSPENSIONS");
			range.Add(GraphFileKeys.SUSPENSIONSDAYSLOST, "SUSPENSIONSDAYSLOST");
			range.Add(GraphFileKeys.EXPULSIONS, "EXPULSIONS");
			range.Add(GraphFileKeys.EXPULSIONSDAYSLOST, "EXPULSIONSDAYSLOST");
			range.Add(GraphFileKeys.GEXPSERVICES, "GEXPSERVICES");
			range.Add(GraphFileKeys.GEXPLENGTH, "GEXPLENGTH");
			range.Add(GraphFileKeys.GEXPRETURNS, "GEXPRETURNS");
			range.Add(GraphFileKeys.GROUPS, "GROUPS");
			range.Add(GraphFileKeys.DISABILITIES, "DISABILITIES");
			range.Add(GraphFileKeys.GWRCT, "GWRCT");
			range.Add(GraphFileKeys.POSTGRADPLAN, "GRADPLAN");
			range.Add(GraphFileKeys.StateTests, "GEDISA");
			range.Add(GraphFileKeys.StateTestsScatter, "SCATTERPLOT");
			range.Add(GraphFileKeys.StateTestsSimilar, "GEDISASIMILARSCHOOLS");
			range.Add(GraphFileKeys.CoursesOffered, "GCOURSEOFFER");
			range.Add(GraphFileKeys.CoursesTaken, "GCOURSETAKE");
			range.Add(GraphFileKeys.ActivityOffer, "SSACTIVITIESOFFER");
			range.Add(GraphFileKeys.ActivitiesPartic, "SSACTIVITIESPARTIC");
			range.Add(GraphFileKeys.CompareContinuing, "EDISACONTINUING");

			return range;
		}
	}
}
