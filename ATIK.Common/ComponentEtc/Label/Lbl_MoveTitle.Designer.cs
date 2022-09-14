
namespace ATIK
{
    partial class Lbl_MoveTitle
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Title.Font = new System.Drawing.Font("Ink Free", 30F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(300, 60);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Title";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Title.DoubleClick += new System.EventHandler(this.lbl_Title_DoubleClick);
            this.lbl_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_Title_MouseDown);
            this.lbl_Title.MouseLeave += new System.EventHandler(this.lbl_Title_MouseLeave);
            this.lbl_Title.MouseHover += new System.EventHandler(this.lbl_Title_MouseHover);
            this.lbl_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Title_MouseMove);
            // 
            // Lbl_MoveTitle
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lbl_Title);
            this.Name = "Lbl_MoveTitle";
            this.Size = new System.Drawing.Size(300, 60);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
    }
}
