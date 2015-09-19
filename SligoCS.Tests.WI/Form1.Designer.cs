namespace SligoCS.Tests.WI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRetentionPage = new System.Windows.Forms.TabPage();
            this.btnGetGraphTitle = new System.Windows.Forms.Button();
            this.btnGetVisibleColumns = new System.Windows.Forms.Button();
            this.tabDropoutsPage = new System.Windows.Forms.TabPage();
            this.tabAttendance = new System.Windows.Forms.TabPage();
            this.btnCheckAttendanceSQL = new System.Windows.Forms.Button();
            this.tabHSCompletion = new System.Windows.Forms.TabPage();
            this.btnHSCompletionGetVisibleColumns = new System.Windows.Forms.Button();
            this.tabTeacherQual = new System.Windows.Forms.TabPage();
            this.btnTeacherQualColumns = new System.Windows.Forms.Button();
            this.btnTQ841_OrgLevel = new System.Windows.Forms.Button();
            this.tabAPExams = new System.Windows.Forms.TabPage();
            this.btnAPExamsCols = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabRetentionPage.SuspendLayout();
            this.tabAttendance.SuspendLayout();
            this.tabHSCompletion.SuspendLayout();
            this.tabTeacherQual.SuspendLayout();
            this.tabAPExams.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabRetentionPage);
            this.tabControl1.Controls.Add(this.tabDropoutsPage);
            this.tabControl1.Controls.Add(this.tabAttendance);
            this.tabControl1.Controls.Add(this.tabHSCompletion);
            this.tabControl1.Controls.Add(this.tabTeacherQual);
            this.tabControl1.Controls.Add(this.tabAPExams);
            this.tabControl1.Location = new System.Drawing.Point(53, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(549, 340);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRetentionPage
            // 
            this.tabRetentionPage.Controls.Add(this.btnGetGraphTitle);
            this.tabRetentionPage.Controls.Add(this.btnGetVisibleColumns);
            this.tabRetentionPage.Location = new System.Drawing.Point(4, 22);
            this.tabRetentionPage.Name = "tabRetentionPage";
            this.tabRetentionPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabRetentionPage.Size = new System.Drawing.Size(541, 314);
            this.tabRetentionPage.TabIndex = 0;
            this.tabRetentionPage.Text = "Retention Page";
            this.tabRetentionPage.UseVisualStyleBackColor = true;
            // 
            // btnGetGraphTitle
            // 
            this.btnGetGraphTitle.Location = new System.Drawing.Point(97, 67);
            this.btnGetGraphTitle.Name = "btnGetGraphTitle";
            this.btnGetGraphTitle.Size = new System.Drawing.Size(157, 23);
            this.btnGetGraphTitle.TabIndex = 1;
            this.btnGetGraphTitle.Text = "Get Graph Title";
            this.btnGetGraphTitle.UseVisualStyleBackColor = true;
            this.btnGetGraphTitle.Click += new System.EventHandler(this.btnGetGraphTitle_Click);
            // 
            // btnGetVisibleColumns
            // 
            this.btnGetVisibleColumns.Location = new System.Drawing.Point(97, 24);
            this.btnGetVisibleColumns.Name = "btnGetVisibleColumns";
            this.btnGetVisibleColumns.Size = new System.Drawing.Size(157, 23);
            this.btnGetVisibleColumns.TabIndex = 0;
            this.btnGetVisibleColumns.Text = "Get Visible Columns";
            this.btnGetVisibleColumns.UseVisualStyleBackColor = true;
            this.btnGetVisibleColumns.Click += new System.EventHandler(this.btnGetVisibleColumns_Click);
            // 
            // tabDropoutsPage
            // 
            this.tabDropoutsPage.Location = new System.Drawing.Point(4, 22);
            this.tabDropoutsPage.Name = "tabDropoutsPage";
            this.tabDropoutsPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabDropoutsPage.Size = new System.Drawing.Size(541, 314);
            this.tabDropoutsPage.TabIndex = 1;
            this.tabDropoutsPage.Text = "Dropouts Page";
            this.tabDropoutsPage.UseVisualStyleBackColor = true;
            // 
            // tabAttendance
            // 
            this.tabAttendance.Controls.Add(this.btnCheckAttendanceSQL);
            this.tabAttendance.Location = new System.Drawing.Point(4, 22);
            this.tabAttendance.Name = "tabAttendance";
            this.tabAttendance.Size = new System.Drawing.Size(541, 314);
            this.tabAttendance.TabIndex = 2;
            this.tabAttendance.Text = "Attendance Page";
            this.tabAttendance.UseVisualStyleBackColor = true;
            // 
            // btnCheckAttendanceSQL
            // 
            this.btnCheckAttendanceSQL.Location = new System.Drawing.Point(164, 28);
            this.btnCheckAttendanceSQL.Name = "btnCheckAttendanceSQL";
            this.btnCheckAttendanceSQL.Size = new System.Drawing.Size(103, 23);
            this.btnCheckAttendanceSQL.TabIndex = 0;
            this.btnCheckAttendanceSQL.Text = "Check SQL";
            this.btnCheckAttendanceSQL.UseVisualStyleBackColor = true;
            this.btnCheckAttendanceSQL.Click += new System.EventHandler(this.btnCheckAttendanceSQL_Click);
            // 
            // tabHSCompletion
            // 
            this.tabHSCompletion.Controls.Add(this.btnHSCompletionGetVisibleColumns);
            this.tabHSCompletion.Location = new System.Drawing.Point(4, 22);
            this.tabHSCompletion.Name = "tabHSCompletion";
            this.tabHSCompletion.Size = new System.Drawing.Size(541, 314);
            this.tabHSCompletion.TabIndex = 3;
            this.tabHSCompletion.Text = "HS Completion";
            this.tabHSCompletion.UseVisualStyleBackColor = true;
            // 
            // btnHSCompletionGetVisibleColumns
            // 
            this.btnHSCompletionGetVisibleColumns.Location = new System.Drawing.Point(187, 83);
            this.btnHSCompletionGetVisibleColumns.Name = "btnHSCompletionGetVisibleColumns";
            this.btnHSCompletionGetVisibleColumns.Size = new System.Drawing.Size(181, 23);
            this.btnHSCompletionGetVisibleColumns.TabIndex = 0;
            this.btnHSCompletionGetVisibleColumns.Text = "Get Visible Columns";
            this.btnHSCompletionGetVisibleColumns.UseVisualStyleBackColor = true;
            this.btnHSCompletionGetVisibleColumns.Click += new System.EventHandler(this.btnHSCompletionGetVisibleColumns_Click);
            // 
            // tabTeacherQual
            // 
            this.tabTeacherQual.Controls.Add(this.button1);
            this.tabTeacherQual.Controls.Add(this.btnTeacherQualColumns);
            this.tabTeacherQual.Controls.Add(this.btnTQ841_OrgLevel);
            this.tabTeacherQual.Location = new System.Drawing.Point(4, 22);
            this.tabTeacherQual.Name = "tabTeacherQual";
            this.tabTeacherQual.Padding = new System.Windows.Forms.Padding(3);
            this.tabTeacherQual.Size = new System.Drawing.Size(541, 314);
            this.tabTeacherQual.TabIndex = 4;
            this.tabTeacherQual.Text = "TeacherQual Page";
            this.tabTeacherQual.UseVisualStyleBackColor = true;
            // 
            // btnTeacherQualColumns
            // 
            this.btnTeacherQualColumns.Location = new System.Drawing.Point(43, 59);
            this.btnTeacherQualColumns.Name = "btnTeacherQualColumns";
            this.btnTeacherQualColumns.Size = new System.Drawing.Size(156, 23);
            this.btnTeacherQualColumns.TabIndex = 1;
            this.btnTeacherQualColumns.Text = "TeacherQual : colums";
            this.btnTeacherQualColumns.UseVisualStyleBackColor = true;
            this.btnTeacherQualColumns.Click += new System.EventHandler(this.btnTeacherQualColumns_Click);
            // 
            // btnTQ841_OrgLevel
            // 
            this.btnTQ841_OrgLevel.Location = new System.Drawing.Point(43, 30);
            this.btnTQ841_OrgLevel.Name = "btnTQ841_OrgLevel";
            this.btnTQ841_OrgLevel.Size = new System.Drawing.Size(156, 23);
            this.btnTQ841_OrgLevel.TabIndex = 0;
            this.btnTQ841_OrgLevel.Text = "TeacherQual : org level";
            this.btnTQ841_OrgLevel.UseVisualStyleBackColor = true;
            this.btnTQ841_OrgLevel.Click += new System.EventHandler(this.btnTQ841_OrgLevel_Click);
            // 
            // tabAPExams
            // 
            this.tabAPExams.Controls.Add(this.btnAPExamsCols);
            this.tabAPExams.Location = new System.Drawing.Point(4, 22);
            this.tabAPExams.Name = "tabAPExams";
            this.tabAPExams.Padding = new System.Windows.Forms.Padding(3);
            this.tabAPExams.Size = new System.Drawing.Size(541, 314);
            this.tabAPExams.TabIndex = 5;
            this.tabAPExams.Text = "AP Exams";
            this.tabAPExams.UseVisualStyleBackColor = true;
            // 
            // btnAPExamsCols
            // 
            this.btnAPExamsCols.Location = new System.Drawing.Point(167, 63);
            this.btnAPExamsCols.Name = "btnAPExamsCols";
            this.btnAPExamsCols.Size = new System.Drawing.Size(144, 23);
            this.btnAPExamsCols.TabIndex = 0;
            this.btnAPExamsCols.Text = "AP Exams Cols";
            this.btnAPExamsCols.UseVisualStyleBackColor = true;
            this.btnAPExamsCols.Click += new System.EventHandler(this.btnAPExamsCols_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "TQ: bug 895";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 458);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabRetentionPage.ResumeLayout(false);
            this.tabAttendance.ResumeLayout(false);
            this.tabHSCompletion.ResumeLayout(false);
            this.tabTeacherQual.ResumeLayout(false);
            this.tabAPExams.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRetentionPage;
        private System.Windows.Forms.Button btnGetVisibleColumns;
        private System.Windows.Forms.TabPage tabDropoutsPage;
        private System.Windows.Forms.Button btnGetGraphTitle;
        private System.Windows.Forms.TabPage tabAttendance;
        private System.Windows.Forms.Button btnCheckAttendanceSQL;
        private System.Windows.Forms.TabPage tabHSCompletion;
        private System.Windows.Forms.Button btnHSCompletionGetVisibleColumns;
        private System.Windows.Forms.TabPage tabTeacherQual;
        private System.Windows.Forms.Button btnTQ841_OrgLevel;
        private System.Windows.Forms.Button btnTeacherQualColumns;
        private System.Windows.Forms.TabPage tabAPExams;
        private System.Windows.Forms.Button btnAPExamsCols;
        private System.Windows.Forms.Button button1;
    }
}

