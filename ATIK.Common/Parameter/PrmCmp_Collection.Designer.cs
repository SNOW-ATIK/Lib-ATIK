
namespace ATIK
{
    partial class PrmCmp_Collection
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbl_PrmName = new System.Windows.Forms.Label();
            this.cmb_Values = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbl_PrmName);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmb_Values);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(252, 25);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbl_PrmName
            // 
            this.lbl_PrmName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_PrmName.BackColor = System.Drawing.Color.DarkGray;
            this.lbl_PrmName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PrmName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PrmName.Location = new System.Drawing.Point(-1, -1);
            this.lbl_PrmName.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_PrmName.Name = "lbl_PrmName";
            this.lbl_PrmName.Size = new System.Drawing.Size(101, 26);
            this.lbl_PrmName.TabIndex = 0;
            this.lbl_PrmName.Text = "Prm Name";
            this.lbl_PrmName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmb_Values
            // 
            this.cmb_Values.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_Values.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_Values.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmb_Values.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Values.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Values.FormattingEnabled = true;
            this.cmb_Values.Location = new System.Drawing.Point(-1, 0);
            this.cmb_Values.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmb_Values.Name = "cmb_Values";
            this.cmb_Values.Size = new System.Drawing.Size(158, 27);
            this.cmb_Values.TabIndex = 0;
            this.cmb_Values.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Cmb_Values_DrawItem);
            // 
            // PrmCmp_Collection
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.MaximumSize = new System.Drawing.Size(0, 27);
            this.MinimumSize = new System.Drawing.Size(252, 27);
            this.Name = "PrmCmp_Collection";
            this.Size = new System.Drawing.Size(252, 25);
            this.SizeChanged += new System.EventHandler(this.PrmCmp_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbl_PrmName;
        private System.Windows.Forms.ComboBox cmb_Values;
    }
}
