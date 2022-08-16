namespace QuandaryHint
{
    partial class gameSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gameSelect));
            this.lockedInDead = new System.Windows.Forms.Button();
            this.runawayTrain = new System.Windows.Forms.Button();
            this.psychWard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lockedInDead
            // 
            this.lockedInDead.Location = new System.Drawing.Point(28, 62);
            this.lockedInDead.Margin = new System.Windows.Forms.Padding(4);
            this.lockedInDead.Name = "lockedInDead";
            this.lockedInDead.Size = new System.Drawing.Size(250, 100);
            this.lockedInDead.TabIndex = 0;
            this.lockedInDead.Text = "The Locked in Dead";
            this.lockedInDead.UseVisualStyleBackColor = true;
            this.lockedInDead.Click += new System.EventHandler(this.lockedInDead_Click);
            // 
            // runawayTrain
            // 
            this.runawayTrain.Location = new System.Drawing.Point(296, 62);
            this.runawayTrain.Margin = new System.Windows.Forms.Padding(4);
            this.runawayTrain.Name = "runawayTrain";
            this.runawayTrain.Size = new System.Drawing.Size(250, 100);
            this.runawayTrain.TabIndex = 1;
            this.runawayTrain.Text = "The Candy Shoppe";
            this.runawayTrain.UseVisualStyleBackColor = true;
            this.runawayTrain.Click += new System.EventHandler(this.candyShoppe_Click);
            // 
            // psychWard
            // 
            this.psychWard.Location = new System.Drawing.Point(564, 62);
            this.psychWard.Margin = new System.Windows.Forms.Padding(4);
            this.psychWard.Name = "psychWard";
            this.psychWard.Size = new System.Drawing.Size(250, 100);
            this.psychWard.TabIndex = 2;
            this.psychWard.Text = "The Dynaline Incident";
            this.psychWard.UseVisualStyleBackColor = true;
            this.psychWard.Click += new System.EventHandler(this.psychWard_Click);
            // 
            // gameSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 229);
            this.Controls.Add(this.psychWard);
            this.Controls.Add(this.runawayTrain);
            this.Controls.Add(this.lockedInDead);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "gameSelect";
            this.Text = "gameSelect";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lockedInDead;
        private System.Windows.Forms.Button runawayTrain;
        private System.Windows.Forms.Button psychWard;
    }
}