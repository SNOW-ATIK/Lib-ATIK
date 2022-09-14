namespace ATIK
{
    partial class MsgFrm_NotifyOnly
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
            this.btn_Msg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Msg
            // 
            this.btn_Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Msg.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Msg.Location = new System.Drawing.Point(29, 29);
            this.btn_Msg.Margin = new System.Windows.Forms.Padding(20);
            this.btn_Msg.Name = "btn_Msg";
            this.btn_Msg.Size = new System.Drawing.Size(742, 92);
            this.btn_Msg.TabIndex = 5;
            this.btn_Msg.UseVisualStyleBackColor = true;
            this.btn_Msg.Click += new System.EventHandler(this.btn_Msg_Click);
            // 
            // MsgFrm_NotifyOnly
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 150);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MsgFrm_NotifyOnly";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRM_Notify";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FRM_Notify_MsgOnly_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Msg;





    }
}