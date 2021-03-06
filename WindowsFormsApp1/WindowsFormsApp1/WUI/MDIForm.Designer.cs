
namespace WindowsFormsApp1.WUI {
    partial class MDIForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.courseSchedulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCourseToStudentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.courseSchedulerToolStripMenuItem,
            this.addCourseToStudentToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // courseSchedulerToolStripMenuItem
            // 
            this.courseSchedulerToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.courseSchedulerToolStripMenuItem.Name = "courseSchedulerToolStripMenuItem";
            this.courseSchedulerToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
            this.courseSchedulerToolStripMenuItem.Text = "Course Scheduler";
            this.courseSchedulerToolStripMenuItem.Click += new System.EventHandler(this.courseSchedulerToolStripMenuItem_Click);
            // 
            // addCourseToStudentToolStripMenuItem
            // 
            this.addCourseToStudentToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.addCourseToStudentToolStripMenuItem.Name = "addCourseToStudentToolStripMenuItem";
            this.addCourseToStudentToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.addCourseToStudentToolStripMenuItem.Text = "Add Course to Student";
            this.addCourseToStudentToolStripMenuItem.Click += new System.EventHandler(this.addCourseToStudentToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // MDIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDIForm";
            this.Text = " University Course manager";
            this.Load += new System.EventHandler(this.MDIForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem courseSchedulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCourseToStudentToolStripMenuItem;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}