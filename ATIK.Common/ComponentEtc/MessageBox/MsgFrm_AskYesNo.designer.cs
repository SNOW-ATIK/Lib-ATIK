namespace ATIK
{
    partial class MsgFrm_AskYesNo
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
            this.btn_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.lbl_Msg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Yes
            // 
            this.btn_Yes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Yes.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Yes.Location = new System.Drawing.Point(29, 29);
            this.btn_Yes.Margin = new System.Windows.Forms.Padding(20, 20, 3, 20);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(110, 92);
            this.btn_Yes.TabIndex = 5;
            this.btn_Yes.Text = "YES";
            this.btn_Yes.UseVisualStyleBackColor = true;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_No.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_No.Location = new System.Drawing.Point(661, 29);
            this.btn_No.Margin = new System.Windows.Forms.Padding(3, 20, 20, 20);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(110, 92);
            this.btn_No.TabIndex = 5;
            this.btn_No.Text = "NO";
            this.btn_No.UseVisualStyleBackColor = true;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // lbl_Msg
            // 
            this.lbl_Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_Msg.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Msg.ForeColor = System.Drawing.Color.White;
            this.lbl_Msg.Location = new System.Drawing.Point(145, 29);
            this.lbl_Msg.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.lbl_Msg.Name = "lbl_Msg";
            this.lbl_Msg.Size = new System.Drawing.Size(510, 92);
            this.lbl_Msg.TabIndex = 6;
            this.lbl_Msg.Text = "Message";
            this.lbl_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MsgFrm_AskYesNo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 150);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Msg);
            this.Controls.Add(this.btn_Yes);
            this.Controls.Add(this.btn_No);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MsgFrm_AskYesNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRM_Notify";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRM_Notify_YesNo_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MsgFrm_AskYesNo_FormClosed);
            this.Shown += new System.EventHandler(this.MsgFrm_AskYesNo_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.Label lbl_Msg;





    }
}