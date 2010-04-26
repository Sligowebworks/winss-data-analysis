using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SligoCS.Tests.WI.NUnitASPTesters;

namespace SligoCS.Tests.WI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetVisibleColumns_Click(object sender, EventArgs e)
        {
            RetentionPageTest rpt = new RetentionPageTest();
            rpt.Bug617_GetVisibleColumns();
        }

        private void btnGetGraphTitle_Click(object sender, EventArgs e)
        {
            RetentionPageTest rpt = new RetentionPageTest();
            rpt.Bug808_GraphTitles();
        }

        private void btnCheckAttendanceSQL_Click(object sender, EventArgs e)
        {
            AttendancePageTester apt = new AttendancePageTester();
            apt.Bug807_SQL();
        }

        private void btnHSCompletionGetVisibleColumns_Click(object sender, EventArgs e)
        {
            HSCompletionPageTester hsc = new HSCompletionPageTester();
            hsc.MasterSetUp();
            hsc.Bug617_GetVisibleColumns();
        }

        private void btnTQ841_OrgLevel_Click(object sender, EventArgs e)
        {

            TeacherQualificationsPageTester tq = new TeacherQualificationsPageTester();
            tq.MasterSetUp();
            tq.Bug841_TeacherQual_OrgLevel();

        }

        private void btnTeacherQualColumns_Click(object sender, EventArgs e)
        {
            TeacherQualificationsPageTester tq = new TeacherQualificationsPageTester();
            tq.MasterSetUp();
            tq.Bug841_TeacherQual_CheckColumns();
        }

        private void btnAPExamsCols_Click(object sender, EventArgs e)
        {
            APTestsPageTester tester = new APTestsPageTester();
            tester.MasterSetUp();
            tester.Bug842_APTests_Columns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TeacherQualificationsPageTester tq = new TeacherQualificationsPageTester();
            tq.MasterSetUp();
            tq.Bug895_TeacherQual_CheckColumns();
        }
    }
}