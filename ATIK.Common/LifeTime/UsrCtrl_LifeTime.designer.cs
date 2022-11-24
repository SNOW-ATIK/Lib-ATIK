
namespace ATIK
{
    partial class UsrCtrl_LifeTime
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_Select = new System.Windows.Forms.CheckBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.btn_Set = new System.Windows.Forms.Button();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.lbl_Remain = new System.Windows.Forms.Label();
            this.CmpCol_Enabled = new ATIK.PrmCmp_Collection();
            this.CmpVal_Current = new ATIK.PrmCmp_Value();
            this.CmpVal_Last = new ATIK.PrmCmp_Value();
            this.CmpVal_Since = new ATIK.PrmCmp_Value();
            this.CmpCol_Type = new ATIK.PrmCmp_Collection();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.Controls.Add(this.chk_Select, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Name, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Set, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Status, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmpCol_Enabled, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmpVal_Current, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmpVal_Last, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmpVal_Since, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmpCol_Type, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Remain, 8, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 54);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chk_Select
            // 
            this.chk_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_Select.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_Select.AutoSize = true;
            this.chk_Select.BackColor = System.Drawing.Color.White;
            this.chk_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chk_Select.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumSeaGreen;
            this.chk_Select.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SeaGreen;
            this.chk_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Select.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.chk_Select.Location = new System.Drawing.Point(1, 1);
            this.chk_Select.Margin = new System.Windows.Forms.Padding(1);
            this.chk_Select.Name = "chk_Select";
            this.chk_Select.Size = new System.Drawing.Size(52, 52);
            this.chk_Select.TabIndex = 9;
            this.chk_Select.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chk_Select.UseVisualStyleBackColor = false;
            this.chk_Select.CheckedChanged += new System.EventHandler(this.chk_Select_CheckedChanged);
            // 
            // lbl_Name
            // 
            this.lbl_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Name.BackColor = System.Drawing.Color.LemonChiffon;
            this.lbl_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Name.Location = new System.Drawing.Point(55, 1);
            this.lbl_Name.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(148, 52);
            this.lbl_Name.TabIndex = 6;
            this.lbl_Name.Text = "Name";
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Name.DoubleClick += new System.EventHandler(this.lbl_Name_DoubleClick);
            // 
            // btn_Set
            // 
            this.btn_Set.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Set.BackColor = System.Drawing.Color.White;
            this.btn_Set.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Set.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Set.Location = new System.Drawing.Point(931, 1);
            this.btn_Set.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(52, 52);
            this.btn_Set.TabIndex = 1;
            this.btn_Set.Text = "SET";
            this.btn_Set.UseVisualStyleBackColor = false;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Status.BackColor = System.Drawing.Color.LemonChiffon;
            this.lbl_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Status.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Status.Location = new System.Drawing.Point(685, 1);
            this.lbl_Status.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(68, 52);
            this.lbl_Status.TabIndex = 6;
            this.lbl_Status.Text = "Status";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Remain
            // 
            this.lbl_Remain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Remain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Remain.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Remain.Location = new System.Drawing.Point(755, 1);
            this.lbl_Remain.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_Remain.Name = "lbl_Remain";
            this.lbl_Remain.Size = new System.Drawing.Size(174, 52);
            this.lbl_Remain.TabIndex = 9;
            this.lbl_Remain.Text = "label1";
            this.lbl_Remain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Remain.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_Remain_Paint);
            // 
            // CmpCol_Enabled
            // 
            this.CmpCol_Enabled.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_Enabled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_Enabled.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_Enabled.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_Enabled.GenParam = null;
            this.CmpCol_Enabled.Location = new System.Drawing.Point(205, 1);
            this.CmpCol_Enabled.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_Enabled.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpCol_Enabled.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpCol_Enabled.Name = "CmpCol_Enabled";
            this.CmpCol_Enabled.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_Enabled.Prm_Name = "Enabled";
            this.CmpCol_Enabled.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_Enabled.Prm_Value = null;
            this.CmpCol_Enabled.Size = new System.Drawing.Size(78, 52);
            this.CmpCol_Enabled.SplitterDistance = 24;
            this.CmpCol_Enabled.TabIndex = 4;
            this.CmpCol_Enabled.Tag = "";
            this.CmpCol_Enabled.SelectedUserItemChangedEvent += new ATIK.PrmCmp_Collection.SelectedUserItemChangedEventHandler(this.CmpCol_Enabled_SelectedUserItemChangedEvent);
            // 
            // CmpVal_Current
            // 
            this.CmpVal_Current.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Current.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Current.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Current.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Current.GenParam = null;
            this.CmpVal_Current.Location = new System.Drawing.Point(475, 1);
            this.CmpVal_Current.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Current.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Current.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Current.Name = "CmpVal_Current";
            this.CmpVal_Current.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Current.Prm_Name = "Current";
            this.CmpVal_Current.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Current.Prm_Value = "";
            this.CmpVal_Current.Size = new System.Drawing.Size(103, 52);
            this.CmpVal_Current.SplitterDistance = 25;
            this.CmpVal_Current.TabIndex = 8;
            this.CmpVal_Current.UseKeyPadUI = true;
            this.CmpVal_Current.UseUserKeyPad = false;
            // 
            // CmpVal_Last
            // 
            this.CmpVal_Last.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Last.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Last.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Last.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Last.GenParam = null;
            this.CmpVal_Last.Location = new System.Drawing.Point(580, 1);
            this.CmpVal_Last.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Last.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Last.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Last.Name = "CmpVal_Last";
            this.CmpVal_Last.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Last.Prm_Name = "Last";
            this.CmpVal_Last.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Last.Prm_Value = "";
            this.CmpVal_Last.Size = new System.Drawing.Size(103, 52);
            this.CmpVal_Last.SplitterDistance = 25;
            this.CmpVal_Last.TabIndex = 8;
            this.CmpVal_Last.UseKeyPadUI = true;
            this.CmpVal_Last.UseUserKeyPad = false;
            // 
            // CmpVal_Since
            // 
            this.CmpVal_Since.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Since.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Since.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Since.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Since.GenParam = null;
            this.CmpVal_Since.Location = new System.Drawing.Point(370, 1);
            this.CmpVal_Since.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Since.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Since.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Since.Name = "CmpVal_Since";
            this.CmpVal_Since.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Since.Prm_Name = "Since";
            this.CmpVal_Since.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Since.Prm_Value = "";
            this.CmpVal_Since.Size = new System.Drawing.Size(103, 52);
            this.CmpVal_Since.SplitterDistance = 25;
            this.CmpVal_Since.TabIndex = 8;
            this.CmpVal_Since.UseKeyPadUI = true;
            this.CmpVal_Since.UseUserKeyPad = false;
            // 
            // CmpCol_Type
            // 
            this.CmpCol_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_Type.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_Type.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_Type.GenParam = null;
            this.CmpCol_Type.Location = new System.Drawing.Point(285, 1);
            this.CmpCol_Type.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_Type.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpCol_Type.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpCol_Type.Name = "CmpCol_Type";
            this.CmpCol_Type.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_Type.Prm_Name = "Type";
            this.CmpCol_Type.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_Type.Prm_Value = null;
            this.CmpCol_Type.Size = new System.Drawing.Size(83, 52);
            this.CmpCol_Type.SplitterDistance = 24;
            this.CmpCol_Type.TabIndex = 4;
            this.CmpCol_Type.Tag = "";
            this.CmpCol_Type.SelectedUserItemChangedEvent += new ATIK.PrmCmp_Collection.SelectedUserItemChangedEventHandler(this.CmpCol_Type_SelectedUserItemChangedEvent);
            // 
            // UsrCtrl_LifeTime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(0, 54);
            this.Name = "UsrCtrl_LifeTime";
            this.Size = new System.Drawing.Size(984, 54);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_Status;
        private ATIK.PrmCmp_Value CmpVal_Since;
        private ATIK.PrmCmp_Value CmpVal_Current;
        private ATIK.PrmCmp_Value CmpVal_Last;
        private System.Windows.Forms.CheckBox chk_Select;
        private ATIK.PrmCmp_Collection CmpCol_Enabled;
        private ATIK.PrmCmp_Collection CmpCol_Type;
        private System.Windows.Forms.Label lbl_Remain;
    }
}
