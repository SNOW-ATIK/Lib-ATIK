
namespace ATIK
{
    partial class UsrCtrl_TimeNow
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_TimeNow = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.tmr_TimeNow = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_TimeNow, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Title, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(118, 62);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_TimeNow
            // 
            this.lbl_TimeNow.BackColor = System.Drawing.Color.White;
            this.lbl_TimeNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TimeNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TimeNow.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_TimeNow.Location = new System.Drawing.Point(1, 21);
            this.lbl_TimeNow.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_TimeNow.Name = "lbl_TimeNow";
            this.lbl_TimeNow.Size = new System.Drawing.Size(116, 40);
            this.lbl_TimeNow.TabIndex = 2;
            this.lbl_TimeNow.Text = "2022-01-01\r\n00:00:00";
            this.lbl_TimeNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.LemonChiffon;
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Title.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(1, 1);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(116, 18);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Time Now";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmr_TimeNow
            // 
            this.tmr_TimeNow.Enabled = true;
            this.tmr_TimeNow.Interval = 1000;
            this.tmr_TimeNow.Tick += new System.EventHandler(this.tmr_TimeNow_Tick);
            // 
            // UsrCtrl_TimeNow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 9F);
            this.Name = "UsrCtrl_TimeNow";
            this.Size = new System.Drawing.Size(118, 62);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_TimeNow;
        private System.Windows.Forms.Timer tmr_TimeNow;
    }
}
