namespace Vectty
{
    partial class ActionList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionList));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lstActions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlActionTypes = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDegRad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblEY = new System.Windows.Forms.Label();
            this.lblEX = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSY = new System.Windows.Forms.Label();
            this.lblSX = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btCopy = new System.Windows.Forms.ToolStripButton();
            this.btPaste = new System.Windows.Forms.ToolStripButton();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.btUp = new System.Windows.Forms.ToolStripButton();
            this.btDown = new System.Windows.Forms.ToolStripButton();
            this.btHMirror = new System.Windows.Forms.ToolStripButton();
            this.btVMirror = new System.Windows.Forms.ToolStripButton();
            this.btAHMirror = new System.Windows.Forms.ToolStripButton();
            this.btAVMirror = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lstActions);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(120, 437);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(120, 512);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btCopy,
            this.btPaste,
            this.btDelete,
            this.btUp,
            this.btDown});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(116, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // lstActions
            // 
            this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstActions.FullRowSelect = true;
            this.lstActions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstActions.HideSelection = false;
            this.lstActions.LargeImageList = this.imlActionTypes;
            this.lstActions.Location = new System.Drawing.Point(0, 0);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(120, 280);
            this.lstActions.SmallImageList = this.imlActionTypes;
            this.lstActions.TabIndex = 0;
            this.lstActions.UseCompatibleStateImageBehavior = false;
            this.lstActions.View = System.Windows.Forms.View.Details;
            this.lstActions.SelectedIndexChanged += new System.EventHandler(this.lstActions_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "df";
            this.columnHeader1.Width = 97;
            // 
            // imlActionTypes
            // 
            this.imlActionTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlActionTypes.ImageStream")));
            this.imlActionTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.imlActionTypes.Images.SetKeyName(0, "Line.png");
            this.imlActionTypes.Images.SetKeyName(1, "Rectangle.png");
            this.imlActionTypes.Images.SetKeyName(2, "Circle.png");
            this.imlActionTypes.Images.SetKeyName(3, "Arc.png");
            this.imlActionTypes.Images.SetKeyName(4, "Bucket.png");
            this.imlActionTypes.Images.SetKeyName(5, "Eraser.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDegRad);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblAction);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 157);
            this.panel1.TabIndex = 1;
            // 
            // lblDegRad
            // 
            this.lblDegRad.AutoSize = true;
            this.lblDegRad.Location = new System.Drawing.Point(64, 136);
            this.lblDegRad.Name = "lblDegRad";
            this.lblDegRad.Size = new System.Drawing.Size(19, 13);
            this.lblDegRad.TabIndex = 6;
            this.lblDegRad.Text = "----";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Deg/Rad:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblEY);
            this.groupBox2.Controls.Add(this.lblEX);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(5, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(116, 54);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "End coords.";
            // 
            // lblEY
            // 
            this.lblEY.AutoSize = true;
            this.lblEY.Location = new System.Drawing.Point(29, 34);
            this.lblEY.Name = "lblEY";
            this.lblEY.Size = new System.Drawing.Size(16, 13);
            this.lblEY.TabIndex = 3;
            this.lblEY.Text = "---";
            // 
            // lblEX
            // 
            this.lblEX.AutoSize = true;
            this.lblEX.Location = new System.Drawing.Point(29, 18);
            this.lblEX.Name = "lblEX";
            this.lblEX.Size = new System.Drawing.Size(16, 13);
            this.lblEX.TabIndex = 2;
            this.lblEX.Text = "---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Y:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "X:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSY);
            this.groupBox1.Controls.Add(this.lblSX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 54);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Start coords.";
            // 
            // lblSY
            // 
            this.lblSY.AutoSize = true;
            this.lblSY.Location = new System.Drawing.Point(29, 34);
            this.lblSY.Name = "lblSY";
            this.lblSY.Size = new System.Drawing.Size(16, 13);
            this.lblSY.TabIndex = 3;
            this.lblSY.Text = "---";
            // 
            // lblSX
            // 
            this.lblSX.AutoSize = true;
            this.lblSX.Location = new System.Drawing.Point(29, 18);
            this.lblSX.Name = "lblSX";
            this.lblSX.Size = new System.Drawing.Size(16, 13);
            this.lblSX.TabIndex = 2;
            this.lblSX.Text = "---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "X:";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(49, 3);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(19, 13);
            this.lblAction.TabIndex = 1;
            this.lblAction.Text = "----";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Action:";
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btHMirror,
            this.btVMirror,
            this.btAHMirror,
            this.btAVMirror});
            this.toolStrip2.Location = new System.Drawing.Point(3, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(116, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btCopy
            // 
            this.btCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCopy.Image = global::Vectty.Properties.Resources.Copy;
            this.btCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(23, 22);
            this.btCopy.Text = "Copy";
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btPaste
            // 
            this.btPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btPaste.Image = global::Vectty.Properties.Resources.Paste;
            this.btPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btPaste.Name = "btPaste";
            this.btPaste.Size = new System.Drawing.Size(23, 22);
            this.btPaste.Text = "Paste";
            this.btPaste.Click += new System.EventHandler(this.btPaste_Click);
            // 
            // btDelete
            // 
            this.btDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDelete.Image = global::Vectty.Properties.Resources.Delete;
            this.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(23, 22);
            this.btDelete.Text = "Delete";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btUp
            // 
            this.btUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btUp.Image = global::Vectty.Properties.Resources.Up;
            this.btUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(23, 22);
            this.btUp.Text = "Up";
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDown.Image = global::Vectty.Properties.Resources.Down;
            this.btDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(23, 22);
            this.btDown.Text = "Down";
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btHMirror
            // 
            this.btHMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btHMirror.Image = global::Vectty.Properties.Resources.HMirror;
            this.btHMirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btHMirror.Name = "btHMirror";
            this.btHMirror.Size = new System.Drawing.Size(23, 22);
            this.btHMirror.Text = "Horizontal mirror";
            this.btHMirror.Click += new System.EventHandler(this.btHMirror_Click);
            // 
            // btVMirror
            // 
            this.btVMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btVMirror.Image = global::Vectty.Properties.Resources.VMirror;
            this.btVMirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btVMirror.Name = "btVMirror";
            this.btVMirror.Size = new System.Drawing.Size(23, 22);
            this.btVMirror.Text = "Vertical mirror";
            this.btVMirror.Click += new System.EventHandler(this.btVMirror_Click);
            // 
            // btAHMirror
            // 
            this.btAHMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAHMirror.Image = global::Vectty.Properties.Resources.AbsHMirror;
            this.btAHMirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAHMirror.Name = "btAHMirror";
            this.btAHMirror.Size = new System.Drawing.Size(23, 22);
            this.btAHMirror.Text = "Absolute horzontal mirror";
            this.btAHMirror.Click += new System.EventHandler(this.btAHMirror_Click);
            // 
            // btAVMirror
            // 
            this.btAVMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAVMirror.Image = global::Vectty.Properties.Resources.AbsVMirror;
            this.btAVMirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAVMirror.Name = "btAVMirror";
            this.btAVMirror.Size = new System.Drawing.Size(23, 22);
            this.btAVMirror.Text = "Absolute vertical mirror";
            this.btAVMirror.Click += new System.EventHandler(this.btAVMirror_Click);
            // 
            // ActionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 512);
            this.Controls.Add(this.toolStripContainer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(135, 4096);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(135, 551);
            this.Name = "ActionList";
            this.Text = "Action List";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView lstActions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imlActionTypes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblEY;
        private System.Windows.Forms.Label lblEX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSY;
        private System.Windows.Forms.Label lblSX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDegRad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton btCopy;
        private System.Windows.Forms.ToolStripButton btPaste;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripButton btUp;
        private System.Windows.Forms.ToolStripButton btDown;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btHMirror;
        private System.Windows.Forms.ToolStripButton btVMirror;
        private System.Windows.Forms.ToolStripButton btAHMirror;
        private System.Windows.Forms.ToolStripButton btAVMirror;
    }
}