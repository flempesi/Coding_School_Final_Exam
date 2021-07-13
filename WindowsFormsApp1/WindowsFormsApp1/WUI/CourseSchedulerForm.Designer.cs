
namespace WindowsFormsApp1.WUI {
    partial class CourseSchedulerForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseSchedulerForm));
            this.dGVCourses = new System.Windows.Forms.DataGridView();
            this.ctrlDate = new System.Windows.Forms.DateTimePicker();
            this.ctrlTime = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dGVProfessors = new System.Windows.Forms.DataGridView();
            this.dGVSchedule = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGVCourses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProfessors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVCourses
            // 
            this.dGVCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVCourses.Location = new System.Drawing.Point(54, 63);
            this.dGVCourses.Name = "dGVCourses";
            this.dGVCourses.RowHeadersWidth = 51;
            this.dGVCourses.RowTemplate.Height = 24;
            this.dGVCourses.Size = new System.Drawing.Size(612, 150);
            this.dGVCourses.TabIndex = 21;
            // 
            // ctrlDate
            // 
            this.ctrlDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ctrlDate.Location = new System.Drawing.Point(314, 246);
            this.ctrlDate.Name = "ctrlDate";
            this.ctrlDate.Size = new System.Drawing.Size(337, 26);
            this.ctrlDate.TabIndex = 22;
            this.ctrlDate.Value = new System.DateTime(2021, 7, 13, 15, 42, 4, 0);
            // 
            // ctrlTime
            // 
            this.ctrlTime.CustomFormat = "HH:00";
            this.ctrlTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ctrlTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ctrlTime.Location = new System.Drawing.Point(685, 246);
            this.ctrlTime.Name = "ctrlTime";
            this.ctrlTime.Size = new System.Drawing.Size(200, 26);
            this.ctrlTime.TabIndex = 23;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(314, 298);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(337, 47);
            this.btnAdd.TabIndex = 24;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(51, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "Select a course :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(710, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "And select a professor :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(50, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "And select a date and time :";
            // 
            // dGVProfessors
            // 
            this.dGVProfessors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVProfessors.Location = new System.Drawing.Point(713, 63);
            this.dGVProfessors.Name = "dGVProfessors";
            this.dGVProfessors.RowHeadersWidth = 51;
            this.dGVProfessors.RowTemplate.Height = 24;
            this.dGVProfessors.Size = new System.Drawing.Size(312, 150);
            this.dGVProfessors.TabIndex = 28;
            // 
            // dGVSchedule
            // 
            this.dGVSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVSchedule.Location = new System.Drawing.Point(54, 426);
            this.dGVSchedule.Name = "dGVSchedule";
            this.dGVSchedule.RowHeadersWidth = 51;
            this.dGVSchedule.RowTemplate.Height = 24;
            this.dGVSchedule.Size = new System.Drawing.Size(971, 150);
            this.dGVSchedule.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(413, 379);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 20);
            this.label4.TabIndex = 30;
            this.label4.Text = "Scheduled Courses :";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(776, 611);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 49);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(906, 611);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 49);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(535, 21);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(131, 36);
            this.btnRefresh.TabIndex = 39;
            this.btnRefresh.Text = "Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // CourseSchedulerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 672);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dGVSchedule);
            this.Controls.Add(this.dGVProfessors);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.ctrlTime);
            this.Controls.Add(this.ctrlDate);
            this.Controls.Add(this.dGVCourses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CourseSchedulerForm";
            this.Text = "Course Scheduler ";
            this.Load += new System.EventHandler(this.CourseSchedulerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVCourses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProfessors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVSchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVCourses;
        private System.Windows.Forms.DateTimePicker ctrlDate;
        private System.Windows.Forms.DateTimePicker ctrlTime;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dGVProfessors;
        private System.Windows.Forms.DataGridView dGVSchedule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRefresh;
    }
}