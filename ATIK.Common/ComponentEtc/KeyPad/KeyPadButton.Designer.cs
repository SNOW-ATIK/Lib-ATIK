
namespace ATIK
{
    partial class KeyPadButton
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
            this.KeyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KeyButton
            // 
            this.KeyButton.BackColor = System.Drawing.Color.White;
            this.KeyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KeyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KeyButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyButton.Location = new System.Drawing.Point(0, 0);
            this.KeyButton.Name = "KeyButton";
            this.KeyButton.Size = new System.Drawing.Size(150, 150);
            this.KeyButton.TabIndex = 0;
            this.KeyButton.UseVisualStyleBackColor = false;
            // 
            // KeyPadButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.KeyButton);
            this.Name = "KeyPadButton";
            this.Load += new System.EventHandler(this.KeyPadButton_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button KeyButton;
    }
}
