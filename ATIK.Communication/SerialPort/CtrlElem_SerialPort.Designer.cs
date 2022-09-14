
namespace ATIK.Communication.SerialPort
{
    partial class CtrlElem_SerialPort
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
            this.lbl_Name = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CmpVal_Terminator = new ATIK.PrmCmp_Value();
            this.CmpCol_PortName = new ATIK.PrmCmp_Collection();
            this.CmpVal_WriteTimeOut = new ATIK.PrmCmp_Value();
            this.CmpCol_BaudRate = new ATIK.PrmCmp_Collection();
            this.CmpVal_ReadTimeOut = new ATIK.PrmCmp_Value();
            this.CmpCol_DataBits = new ATIK.PrmCmp_Collection();
            this.CmpCol_Parity = new ATIK.PrmCmp_Collection();
            this.CmpCol_StopBits = new ATIK.PrmCmp_Collection();
            this.btn_Open = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 518);
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
            this.lbl_Name.Size = new System.Drawing.Size(233, 28);
            this.lbl_Name.TabIndex = 7;
            this.lbl_Name.Text = "Logical Name";
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 488);
            this.panel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.CmpVal_Terminator, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.CmpCol_PortName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.CmpVal_WriteTimeOut, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.CmpCol_BaudRate, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.CmpVal_ReadTimeOut, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.CmpCol_DataBits, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.CmpCol_Parity, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.CmpCol_StopBits, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_Open, 0, 9);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(235, 488);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // CmpVal_Terminator
            // 
            this.CmpVal_Terminator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_Terminator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_Terminator.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_Terminator.Color_Value = System.Drawing.Color.Gainsboro;
            this.CmpVal_Terminator.GenParam = null;
            this.CmpVal_Terminator.Location = new System.Drawing.Point(1, 379);
            this.CmpVal_Terminator.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_Terminator.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpVal_Terminator.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpVal_Terminator.Name = "CmpVal_Terminator";
            this.CmpVal_Terminator.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_Terminator.Prm_Name = "Terminator";
            this.CmpVal_Terminator.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_Terminator.Prm_Value = "";
            this.CmpVal_Terminator.Size = new System.Drawing.Size(233, 52);
            this.CmpVal_Terminator.SplitterDistance = 25;
            this.CmpVal_Terminator.TabIndex = 8;
            this.CmpVal_Terminator.UseKeyPadUI = false;
            this.CmpVal_Terminator.UseUserKeyPad = false;
            // 
            // CmpCol_PortName
            // 
            this.CmpCol_PortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_PortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_PortName.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_PortName.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_PortName.GenParam = null;
            this.CmpCol_PortName.Location = new System.Drawing.Point(1, 1);
            this.CmpCol_PortName.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_PortName.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpCol_PortName.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpCol_PortName.Name = "CmpCol_PortName";
            this.CmpCol_PortName.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_PortName.Prm_Name = "PortName";
            this.CmpCol_PortName.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_PortName.Prm_Value = null;
            this.CmpCol_PortName.Size = new System.Drawing.Size(233, 52);
            this.CmpCol_PortName.SplitterDistance = 25;
            this.CmpCol_PortName.TabIndex = 7;
            // 
            // CmpVal_WriteTimeOut
            // 
            this.CmpVal_WriteTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_WriteTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_WriteTimeOut.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_WriteTimeOut.Color_Value = System.Drawing.Color.Gainsboro;
            this.CmpVal_WriteTimeOut.GenParam = null;
            this.CmpVal_WriteTimeOut.Location = new System.Drawing.Point(1, 325);
            this.CmpVal_WriteTimeOut.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_WriteTimeOut.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpVal_WriteTimeOut.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpVal_WriteTimeOut.Name = "CmpVal_WriteTimeOut";
            this.CmpVal_WriteTimeOut.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_WriteTimeOut.Prm_Name = "Write TimeOut";
            this.CmpVal_WriteTimeOut.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_WriteTimeOut.Prm_Value = "";
            this.CmpVal_WriteTimeOut.Size = new System.Drawing.Size(233, 52);
            this.CmpVal_WriteTimeOut.SplitterDistance = 25;
            this.CmpVal_WriteTimeOut.TabIndex = 9;
            this.CmpVal_WriteTimeOut.UseKeyPadUI = false;
            this.CmpVal_WriteTimeOut.UseUserKeyPad = false;
            // 
            // CmpCol_BaudRate
            // 
            this.CmpCol_BaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_BaudRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_BaudRate.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_BaudRate.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_BaudRate.GenParam = null;
            this.CmpCol_BaudRate.Location = new System.Drawing.Point(1, 55);
            this.CmpCol_BaudRate.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_BaudRate.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpCol_BaudRate.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpCol_BaudRate.Name = "CmpCol_BaudRate";
            this.CmpCol_BaudRate.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_BaudRate.Prm_Name = "BaudRate";
            this.CmpCol_BaudRate.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_BaudRate.Prm_Value = null;
            this.CmpCol_BaudRate.Size = new System.Drawing.Size(233, 52);
            this.CmpCol_BaudRate.SplitterDistance = 25;
            this.CmpCol_BaudRate.TabIndex = 6;
            // 
            // CmpVal_ReadTimeOut
            // 
            this.CmpVal_ReadTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpVal_ReadTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpVal_ReadTimeOut.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpVal_ReadTimeOut.Color_Value = System.Drawing.Color.Gainsboro;
            this.CmpVal_ReadTimeOut.GenParam = null;
            this.CmpVal_ReadTimeOut.Location = new System.Drawing.Point(1, 271);
            this.CmpVal_ReadTimeOut.Margin = new System.Windows.Forms.Padding(1);
            this.CmpVal_ReadTimeOut.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpVal_ReadTimeOut.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpVal_ReadTimeOut.Name = "CmpVal_ReadTimeOut";
            this.CmpVal_ReadTimeOut.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpVal_ReadTimeOut.Prm_Name = "Read TimeOut";
            this.CmpVal_ReadTimeOut.Prm_Type = ATIK.PrmCmp.PrmType.Integer;
            this.CmpVal_ReadTimeOut.Prm_Value = "";
            this.CmpVal_ReadTimeOut.Size = new System.Drawing.Size(233, 52);
            this.CmpVal_ReadTimeOut.SplitterDistance = 25;
            this.CmpVal_ReadTimeOut.TabIndex = 10;
            this.CmpVal_ReadTimeOut.UseKeyPadUI = false;
            this.CmpVal_ReadTimeOut.UseUserKeyPad = false;
            // 
            // CmpCol_DataBits
            // 
            this.CmpCol_DataBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_DataBits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_DataBits.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_DataBits.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_DataBits.GenParam = null;
            this.CmpCol_DataBits.Location = new System.Drawing.Point(1, 109);
            this.CmpCol_DataBits.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_DataBits.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpCol_DataBits.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpCol_DataBits.Name = "CmpCol_DataBits";
            this.CmpCol_DataBits.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_DataBits.Prm_Name = "DataBits";
            this.CmpCol_DataBits.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_DataBits.Prm_Value = null;
            this.CmpCol_DataBits.Size = new System.Drawing.Size(233, 52);
            this.CmpCol_DataBits.SplitterDistance = 25;
            this.CmpCol_DataBits.TabIndex = 5;
            // 
            // CmpCol_Parity
            // 
            this.CmpCol_Parity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_Parity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_Parity.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_Parity.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_Parity.GenParam = null;
            this.CmpCol_Parity.Location = new System.Drawing.Point(1, 217);
            this.CmpCol_Parity.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_Parity.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpCol_Parity.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpCol_Parity.Name = "CmpCol_Parity";
            this.CmpCol_Parity.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_Parity.Prm_Name = "Parity";
            this.CmpCol_Parity.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_Parity.Prm_Value = null;
            this.CmpCol_Parity.Size = new System.Drawing.Size(233, 52);
            this.CmpCol_Parity.SplitterDistance = 25;
            this.CmpCol_Parity.TabIndex = 3;
            // 
            // CmpCol_StopBits
            // 
            this.CmpCol_StopBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmpCol_StopBits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CmpCol_StopBits.Color_Name = System.Drawing.Color.LemonChiffon;
            this.CmpCol_StopBits.Color_Value = System.Drawing.SystemColors.Window;
            this.CmpCol_StopBits.GenParam = null;
            this.CmpCol_StopBits.Location = new System.Drawing.Point(1, 163);
            this.CmpCol_StopBits.Margin = new System.Windows.Forms.Padding(1);
            this.CmpCol_StopBits.MaximumSize = new System.Drawing.Size(1000, 102);
            this.CmpCol_StopBits.MinimumSize = new System.Drawing.Size(30, 52);
            this.CmpCol_StopBits.Name = "CmpCol_StopBits";
            this.CmpCol_StopBits.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.CmpCol_StopBits.Prm_Name = "StopBits";
            this.CmpCol_StopBits.Prm_Type = ATIK.PrmCmp.PrmType.Boolean;
            this.CmpCol_StopBits.Prm_Value = null;
            this.CmpCol_StopBits.Size = new System.Drawing.Size(233, 52);
            this.CmpCol_StopBits.SplitterDistance = 25;
            this.CmpCol_StopBits.TabIndex = 4;
            // 
            // btn_Open
            // 
            this.btn_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Open.BackColor = System.Drawing.Color.Crimson;
            this.btn_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Open.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Open.Location = new System.Drawing.Point(1, 447);
            this.btn_Open.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(233, 40);
            this.btn_Open.TabIndex = 11;
            this.btn_Open.UseVisualStyleBackColor = false;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // CtrlElem_SerialPort
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtrlElem_SerialPort";
            this.Size = new System.Drawing.Size(235, 518);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private PrmCmp_Value CmpVal_Terminator;
        private PrmCmp_Collection CmpCol_PortName;
        private PrmCmp_Value CmpVal_WriteTimeOut;
        private PrmCmp_Collection CmpCol_BaudRate;
        private PrmCmp_Value CmpVal_ReadTimeOut;
        private PrmCmp_Collection CmpCol_DataBits;
        private PrmCmp_Collection CmpCol_Parity;
        private PrmCmp_Collection CmpCol_StopBits;
        private System.Windows.Forms.Button btn_Open;
    }
}
