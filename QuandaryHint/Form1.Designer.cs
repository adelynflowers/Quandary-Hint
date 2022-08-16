namespace QuandaryHint
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.HintPusher = new System.Windows.Forms.Button();
            this.hintEntry = new System.Windows.Forms.TextBox();
            this.configButton = new System.Windows.Forms.Button();
            this.audioToggle = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartVideoBtn = new System.Windows.Forms.Button();
            this.AlignHintsBtn = new System.Windows.Forms.Button();
            this.PlayPauseBtn = new System.Windows.Forms.Button();
            this.HintSoundBtn = new System.Windows.Forms.Button();
            this.EscapeBtn = new System.Windows.Forms.Button();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.easyStartBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HintPusher
            // 
            this.HintPusher.Location = new System.Drawing.Point(338, 29);
            this.HintPusher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HintPusher.Name = "HintPusher";
            this.HintPusher.Size = new System.Drawing.Size(120, 65);
            this.HintPusher.TabIndex = 0;
            this.HintPusher.Text = "Punch Hint";
            this.HintPusher.UseVisualStyleBackColor = true;
            this.HintPusher.Click += new System.EventHandler(this.HintPusher_Click);
            // 
            // hintEntry
            // 
            this.hintEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hintEntry.Location = new System.Drawing.Point(202, 131);
            this.hintEntry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hintEntry.Multiline = true;
            this.hintEntry.Name = "hintEntry";
            this.hintEntry.Size = new System.Drawing.Size(672, 452);
            this.hintEntry.TabIndex = 1;
            this.hintEntry.TextChanged += new System.EventHandler(this.hintEntry_TextChanged);
            this.hintEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hintEntry_KeyDown);
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(202, 29);
            this.configButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(128, 65);
            this.configButton.TabIndex = 3;
            this.configButton.Text = "Edit Config";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // audioToggle
            // 
            this.audioToggle.AutoSize = true;
            this.audioToggle.Location = new System.Drawing.Point(12, 331);
            this.audioToggle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.audioToggle.Name = "audioToggle";
            this.audioToggle.Size = new System.Drawing.Size(165, 30);
            this.audioToggle.TabIndex = 4;
            this.audioToggle.Text = "Toggle Audio";
            this.audioToggle.UseVisualStyleBackColor = true;
            this.audioToggle.CheckStateChanged += new System.EventHandler(this.audioToggle_CheckStateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 302);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 26);
            this.label3.TabIndex = 11;
            this.label3.Text = "Escape Time";
            // 
            // StartVideoBtn
            // 
            this.StartVideoBtn.Location = new System.Drawing.Point(882, 323);
            this.StartVideoBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartVideoBtn.Name = "StartVideoBtn";
            this.StartVideoBtn.Size = new System.Drawing.Size(128, 75);
            this.StartVideoBtn.TabIndex = 12;
            this.StartVideoBtn.Text = "Start Video";
            this.StartVideoBtn.UseVisualStyleBackColor = true;
            this.StartVideoBtn.Click += new System.EventHandler(this.StartVideoBtn_Click);
            // 
            // AlignHintsBtn
            // 
            this.AlignHintsBtn.Location = new System.Drawing.Point(12, 29);
            this.AlignHintsBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AlignHintsBtn.Name = "AlignHintsBtn";
            this.AlignHintsBtn.Size = new System.Drawing.Size(156, 65);
            this.AlignHintsBtn.TabIndex = 13;
            this.AlignHintsBtn.Text = "Align hints";
            this.AlignHintsBtn.UseVisualStyleBackColor = true;
            this.AlignHintsBtn.Click += new System.EventHandler(this.AlignHintsBtn_Click);
            // 
            // PlayPauseBtn
            // 
            this.PlayPauseBtn.Location = new System.Drawing.Point(12, 156);
            this.PlayPauseBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlayPauseBtn.Name = "PlayPauseBtn";
            this.PlayPauseBtn.Size = new System.Drawing.Size(156, 52);
            this.PlayPauseBtn.TabIndex = 14;
            this.PlayPauseBtn.Text = "Play/Pause";
            this.PlayPauseBtn.UseVisualStyleBackColor = true;
            this.PlayPauseBtn.Click += new System.EventHandler(this.PlayPauseBtn_Click);
            // 
            // HintSoundBtn
            // 
            this.HintSoundBtn.Location = new System.Drawing.Point(618, 29);
            this.HintSoundBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HintSoundBtn.Name = "HintSoundBtn";
            this.HintSoundBtn.Size = new System.Drawing.Size(132, 65);
            this.HintSoundBtn.TabIndex = 16;
            this.HintSoundBtn.Text = "boop";
            this.HintSoundBtn.UseVisualStyleBackColor = true;
            this.HintSoundBtn.Click += new System.EventHandler(this.HintSoundBtn_Click);
            // 
            // EscapeBtn
            // 
            this.EscapeBtn.Location = new System.Drawing.Point(12, 227);
            this.EscapeBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EscapeBtn.Name = "EscapeBtn";
            this.EscapeBtn.Size = new System.Drawing.Size(156, 71);
            this.EscapeBtn.TabIndex = 17;
            this.EscapeBtn.Text = "Escape!";
            this.EscapeBtn.UseVisualStyleBackColor = true;
            this.EscapeBtn.Click += new System.EventHandler(this.EscapeBtn_Click);
            // 
            // ResetBtn
            // 
            this.ResetBtn.Location = new System.Drawing.Point(468, 29);
            this.ResetBtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(140, 65);
            this.ResetBtn.TabIndex = 19;
            this.ResetBtn.Text = "Reset Game";
            this.ResetBtn.UseVisualStyleBackColor = true;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // easyStartBtn
            // 
            this.easyStartBtn.Location = new System.Drawing.Point(882, 250);
            this.easyStartBtn.Margin = new System.Windows.Forms.Padding(4);
            this.easyStartBtn.Name = "easyStartBtn";
            this.easyStartBtn.Size = new System.Drawing.Size(128, 65);
            this.easyStartBtn.TabIndex = 18;
            this.easyStartBtn.Text = "Initial Setup";
            this.easyStartBtn.UseVisualStyleBackColor = true;
            this.easyStartBtn.Click += new System.EventHandler(this.easyStartBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1032, 596);
            this.Controls.Add(this.ResetBtn);
            this.Controls.Add(this.easyStartBtn);
            this.Controls.Add(this.EscapeBtn);
            this.Controls.Add(this.HintSoundBtn);
            this.Controls.Add(this.PlayPauseBtn);
            this.Controls.Add(this.AlignHintsBtn);
            this.Controls.Add(this.StartVideoBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.audioToggle);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.hintEntry);
            this.Controls.Add(this.HintPusher);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Control Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HintPusher;
        private System.Windows.Forms.TextBox hintEntry;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.CheckBox audioToggle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StartVideoBtn;
        private System.Windows.Forms.Button AlignHintsBtn;
        private System.Windows.Forms.Button PlayPauseBtn;
        private System.Windows.Forms.Button HintSoundBtn;
        private System.Windows.Forms.Button EscapeBtn;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.Button easyStartBtn;
    }
}

