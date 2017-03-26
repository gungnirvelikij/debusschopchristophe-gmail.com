namespace Examenapplicatie
{
    partial class windowMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(windowMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.achtergrondHerstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afsluitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testknop1 = new System.Windows.Forms.Button();
            this.testknop2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(926, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.achtergrondHerstellenToolStripMenuItem,
            this.afsluitenToolStripMenuItem});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // achtergrondHerstellenToolStripMenuItem
            // 
            this.achtergrondHerstellenToolStripMenuItem.Name = "achtergrondHerstellenToolStripMenuItem";
            this.achtergrondHerstellenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.achtergrondHerstellenToolStripMenuItem.Text = "Achtergrond herstellen";
            this.achtergrondHerstellenToolStripMenuItem.Click += new System.EventHandler(this.achtergrondHerstellenToolStripMenuItem_Click);
            // 
            // afsluitenToolStripMenuItem
            // 
            this.afsluitenToolStripMenuItem.Name = "afsluitenToolStripMenuItem";
            this.afsluitenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.afsluitenToolStripMenuItem.Text = "Afsluiten";
            this.afsluitenToolStripMenuItem.Click += new System.EventHandler(this.afsluitenToolStripMenuItem_Click);
            // 
            // testknop1
            // 
            this.testknop1.Location = new System.Drawing.Point(347, 146);
            this.testknop1.Name = "testknop1";
            this.testknop1.Size = new System.Drawing.Size(75, 23);
            this.testknop1.TabIndex = 1;
            this.testknop1.Text = "OK";
            this.testknop1.UseVisualStyleBackColor = true;
            this.testknop1.Click += new System.EventHandler(this.testknop1_Click);
            // 
            // testknop2
            // 
            this.testknop2.Location = new System.Drawing.Point(462, 146);
            this.testknop2.Name = "testknop2";
            this.testknop2.Size = new System.Drawing.Size(75, 23);
            this.testknop2.TabIndex = 2;
            this.testknop2.Text = "NOK";
            this.testknop2.UseVisualStyleBackColor = true;
            this.testknop2.Click += new System.EventHandler(this.testknop2_Click);
            // 
            // windowMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(926, 427);
            this.Controls.Add(this.testknop2);
            this.Controls.Add(this.testknop1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "windowMain";
            this.Text = "Examen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem achtergrondHerstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afsluitenToolStripMenuItem;
        private System.Windows.Forms.Button testknop1;
        private System.Windows.Forms.Button testknop2;
    }
}

