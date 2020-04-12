namespace Vectty
{
    partial class PatternEditor
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.ilPatterns = new System.Windows.Forms.ImageList(this.components);
            this.drawArea = new Vectty.PixelPerfectPicturebox();
            this.fpPatterns = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawArea)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(14, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(129, 23);
            this.panel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnUp,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(129, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::Vectty.Properties.Resources.Plus;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::Vectty.Properties.Resources.Less;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Remove";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Image = global::Vectty.Properties.Resources.Up;
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 22);
            this.btnUp.Text = "Move up";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Vectty.Properties.Resources.Down;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // ilPatterns
            // 
            this.ilPatterns.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilPatterns.ImageSize = new System.Drawing.Size(32, 32);
            this.ilPatterns.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // drawArea
            // 
            this.drawArea.BackColor = System.Drawing.Color.Transparent;
            this.drawArea.Location = new System.Drawing.Point(0, 32);
            this.drawArea.Margin = new System.Windows.Forms.Padding(0);
            this.drawArea.MaximumSize = new System.Drawing.Size(128, 128);
            this.drawArea.MinimumSize = new System.Drawing.Size(128, 128);
            this.drawArea.Name = "drawArea";
            this.drawArea.PixelPerfect = true;
            this.drawArea.Size = new System.Drawing.Size(128, 128);
            this.drawArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.drawArea.TabIndex = 0;
            this.drawArea.TabStop = false;
            this.drawArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseUp);
            // 
            // fpPatterns
            // 
            this.fpPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fpPatterns.AutoScroll = true;
            this.fpPatterns.Location = new System.Drawing.Point(0, 163);
            this.fpPatterns.MaximumSize = new System.Drawing.Size(128, 4096);
            this.fpPatterns.MinimumSize = new System.Drawing.Size(128, 133);
            this.fpPatterns.Name = "fpPatterns";
            this.fpPatterns.Size = new System.Drawing.Size(128, 133);
            this.fpPatterns.TabIndex = 4;
            this.fpPatterns.Click += new System.EventHandler(this.fpPatterns_Click);
            // 
            // PatternEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(130, 296);
            this.ControlBox = false;
            this.Controls.Add(this.fpPatterns);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.drawArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(146, 4096);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(146, 39);
            this.Name = "PatternEditor";
            this.Text = "Patterns";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PixelPerfectPicturebox drawArea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ImageList ilPatterns;
        private System.Windows.Forms.FlowLayoutPanel fpPatterns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}