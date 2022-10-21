
namespace ATIK
{
    partial class Frm_ModifyDate
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CmpVal_Year = new ATIK.PrmCmp_Value();
            this.CmpVal_Month = new ATIK.PrmCmp_Value();
            this.CmpVal_Day = new ATIK.PrmCmp_Value();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.CmpVal_Hour = new ATIK.PrmCmp_Value();
            this.CmpVal_Minute = new ATIK.PrmCmp_Value();
            this.CmpVal_Second = new ATIK.PrmCmp_Value();
            this.btn_Today = new System.Windows.Forms.Button();
            this.btn_AddYear = new System.Windows.Forms.Button();
            this.btn_AddMonth = new System.Windows.Forms.Button();
            this.btn_AddDay = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Today, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(19, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(282, 322);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_Name
            // 
            this.lbl_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Name.BackColor = System.Drawing.Color.Gold;
            this.lbl_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Name.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.lbl_Name.Location = new System.Drawing.Point(1, 1);
            this.lbl_Name.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(280, 30);
            this.lbl_Name.TabIndex = 8;
            this.lbl_Name.Text = "Modify Date";
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btn_AddYear, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.CmpVal_Year, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.CmpVal_Month, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.CmpVal_Day, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_AddMonth, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_AddDay, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 37);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(282, 90);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // CmpVal_Year
            // 
            this.CmpVal_Year.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Year.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Year.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Year.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Year.GenParam = null;
            this.CmpVal_Year.Location = new System.Drawing.Point(1, 31);
            this.CmpVal_Year.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Year.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Year.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Year.Name = "CmpVal_Year";
            this.CmpVal_Year.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Year.Prm_Name = "Year";
            this.CmpVal_Year.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Year.Prm_Value = "";
            this.CmpVal_Year.Size = new System.Drawing.Size(91, 58);
            this.CmpVal_Year.SplitterDistance = 26;
            this.CmpVal_Year.TabIndex = 2;
            this.CmpVal_Year.UseKeyPadUI = true;
            this.CmpVal_Year.UseUserKeyPad = false;
            // 
            // CmpVal_Month
            // 
            this.CmpVal_Month.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Month.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Month.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Month.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Month.GenParam = null;
            this.CmpVal_Month.Location = new System.Drawing.Point(94, 31);
            this.CmpVal_Month.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Month.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Month.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Month.Name = "CmpVal_Month";
            this.CmpVal_Month.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Month.Prm_Name = "Month";
            this.CmpVal_Month.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Month.Prm_Value = "";
            this.CmpVal_Month.Size = new System.Drawing.Size(91, 58);
            this.CmpVal_Month.SplitterDistance = 26;
            this.CmpVal_Month.TabIndex = 2;
            this.CmpVal_Month.UseKeyPadUI = true;
            this.CmpVal_Month.UseUserKeyPad = false;
            // 
            // CmpVal_Day
            // 
            this.CmpVal_Day.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Day.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Day.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Day.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Day.GenParam = null;
            this.CmpVal_Day.Location = new System.Drawing.Point(187, 31);
            this.CmpVal_Day.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Day.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Day.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Day.Name = "CmpVal_Day";
            this.CmpVal_Day.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Day.Prm_Name = "Day";
            this.CmpVal_Day.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Day.Prm_Value = "";
            this.CmpVal_Day.Size = new System.Drawing.Size(94, 58);
            this.CmpVal_Day.SplitterDistance = 26;
            this.CmpVal_Day.TabIndex = 2;
            this.CmpVal_Day.UseKeyPadUI = true;
            this.CmpVal_Day.UseUserKeyPad = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btn_Apply, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Cancel, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 262);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(282, 60);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Apply.BackColor = System.Drawing.Color.White;
            this.btn_Apply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apply.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Apply.Location = new System.Drawing.Point(1, 1);
            this.btn_Apply.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(129, 58);
            this.btn_Apply.TabIndex = 1;
            this.btn_Apply.Text = "APPLY";
            this.btn_Apply.UseVisualStyleBackColor = false;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BackColor = System.Drawing.Color.White;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(152, 1);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(129, 58);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.CmpVal_Hour, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.CmpVal_Minute, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.CmpVal_Second, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 132);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(282, 60);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // CmpVal_Hour
            // 
            this.CmpVal_Hour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Hour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Hour.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Hour.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Hour.GenParam = null;
            this.CmpVal_Hour.Location = new System.Drawing.Point(1, 1);
            this.CmpVal_Hour.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Hour.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Hour.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Hour.Name = "CmpVal_Hour";
            this.CmpVal_Hour.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Hour.Prm_Name = "Hour";
            this.CmpVal_Hour.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Hour.Prm_Value = "";
            this.CmpVal_Hour.Size = new System.Drawing.Size(92, 58);
            this.CmpVal_Hour.SplitterDistance = 26;
            this.CmpVal_Hour.TabIndex = 2;
            this.CmpVal_Hour.UseKeyPadUI = true;
            this.CmpVal_Hour.UseUserKeyPad = false;
            // 
            // CmpVal_Minute
            // 
            this.CmpVal_Minute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Minute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Minute.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Minute.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Minute.GenParam = null;
            this.CmpVal_Minute.Location = new System.Drawing.Point(95, 1);
            this.CmpVal_Minute.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Minute.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Minute.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Minute.Name = "CmpVal_Minute";
            this.CmpVal_Minute.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Minute.Prm_Name = "Minute";
            this.CmpVal_Minute.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Minute.Prm_Value = "";
            this.CmpVal_Minute.Size = new System.Drawing.Size(92, 58);
            this.CmpVal_Minute.SplitterDistance = 26;
            this.CmpVal_Minute.TabIndex = 2;
            this.CmpVal_Minute.UseKeyPadUI = true;
            this.CmpVal_Minute.UseUserKeyPad = false;
            // 
            // CmpVal_Second
            // 
            this.CmpVal_Second.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Second.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Second.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Second.Color_Value = System.Drawing.Color.White;
            this.CmpVal_Second.GenParam = null;
            this.CmpVal_Second.Location = new System.Drawing.Point(189, 1);
            this.CmpVal_Second.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Second.MaximumSize = new System.Drawing.Size(1000, 96);
            this.CmpVal_Second.MinimumSize = new System.Drawing.Size(30, 49);
            this.CmpVal_Second.Name = "CmpVal_Second";
            this.CmpVal_Second.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Second.Prm_Name = "Second";
            this.CmpVal_Second.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Second.Prm_Value = "";
            this.CmpVal_Second.Size = new System.Drawing.Size(92, 58);
            this.CmpVal_Second.SplitterDistance = 26;
            this.CmpVal_Second.TabIndex = 2;
            this.CmpVal_Second.UseKeyPadUI = true;
            this.CmpVal_Second.UseUserKeyPad = false;
            // 
            // btn_Today
            // 
            this.btn_Today.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Today.BackColor = System.Drawing.Color.White;
            this.btn_Today.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Today.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Today.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Today.Location = new System.Drawing.Point(1, 203);
            this.btn_Today.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Today.Name = "btn_Today";
            this.btn_Today.Size = new System.Drawing.Size(280, 38);
            this.btn_Today.TabIndex = 1;
            this.btn_Today.Text = "TODAY";
            this.btn_Today.UseVisualStyleBackColor = false;
            this.btn_Today.Click += new System.EventHandler(this.btn_Today_Click);
            // 
            // btn_AddYear
            // 
            this.btn_AddYear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddYear.BackColor = System.Drawing.Color.White;
            this.btn_AddYear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddYear.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btn_AddYear.Location = new System.Drawing.Point(1, 1);
            this.btn_AddYear.Margin = new System.Windows.Forms.Padding(1);
            this.btn_AddYear.Name = "btn_AddYear";
            this.btn_AddYear.Size = new System.Drawing.Size(91, 28);
            this.btn_AddYear.TabIndex = 1;
            this.btn_AddYear.Text = "+";
            this.btn_AddYear.UseVisualStyleBackColor = false;
            this.btn_AddYear.Click += new System.EventHandler(this.btn_AddYear_Click);
            // 
            // btn_AddMonth
            // 
            this.btn_AddMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddMonth.BackColor = System.Drawing.Color.White;
            this.btn_AddMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddMonth.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btn_AddMonth.Location = new System.Drawing.Point(94, 1);
            this.btn_AddMonth.Margin = new System.Windows.Forms.Padding(1);
            this.btn_AddMonth.Name = "btn_AddMonth";
            this.btn_AddMonth.Size = new System.Drawing.Size(91, 28);
            this.btn_AddMonth.TabIndex = 1;
            this.btn_AddMonth.Text = "+";
            this.btn_AddMonth.UseVisualStyleBackColor = false;
            this.btn_AddMonth.Click += new System.EventHandler(this.btn_AddMonth_Click);
            // 
            // btn_AddDay
            // 
            this.btn_AddDay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddDay.BackColor = System.Drawing.Color.White;
            this.btn_AddDay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddDay.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btn_AddDay.Location = new System.Drawing.Point(187, 1);
            this.btn_AddDay.Margin = new System.Windows.Forms.Padding(1);
            this.btn_AddDay.Name = "btn_AddDay";
            this.btn_AddDay.Size = new System.Drawing.Size(94, 28);
            this.btn_AddDay.TabIndex = 1;
            this.btn_AddDay.Text = "+";
            this.btn_AddDay.UseVisualStyleBackColor = false;
            this.btn_AddDay.Click += new System.EventHandler(this.btn_AddDay_Click);
            // 
            // Frm_ModifyDate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(320, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_ModifyDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_LifeTime_ModifyDate";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private ATIK.PrmCmp_Value CmpVal_Year;
        private ATIK.PrmCmp_Value CmpVal_Month;
        private ATIK.PrmCmp_Value CmpVal_Day;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private ATIK.PrmCmp_Value CmpVal_Hour;
        private ATIK.PrmCmp_Value CmpVal_Minute;
        private ATIK.PrmCmp_Value CmpVal_Second;
        private System.Windows.Forms.Button btn_Today;
        private System.Windows.Forms.Button btn_AddYear;
        private System.Windows.Forms.Button btn_AddMonth;
        private System.Windows.Forms.Button btn_AddDay;
    }
}