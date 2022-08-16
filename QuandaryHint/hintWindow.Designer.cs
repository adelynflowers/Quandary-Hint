namespace QuandaryHint
{
    partial class hintWindow
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
            this.hintLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hintLabel
            // 
            this.hintLabel.BackColor = System.Drawing.Color.Transparent;
            this.hintLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hintLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.hintLabel.Location = new System.Drawing.Point(0, 0);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Padding = new System.Windows.Forms.Padding(50, 0, 0, 50);
            this.hintLabel.Size = new System.Drawing.Size(934, 469);
            this.hintLabel.TabIndex = 0;
            this.hintLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // hintWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(934, 469);
            this.Controls.Add(this.hintLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "hintWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "hintWindow";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.WindowText;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label hintLabel;
    }
}