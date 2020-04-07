namespace Vectty
{
    partial class ExportTypeSelector
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbSinclair = new System.Windows.Forms.RadioButton();
            this.rbBoriel = new System.Windows.Forms.RadioButton();
            this.rbAssembler = new System.Windows.Forms.RadioButton();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txtIdent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckInclude = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(124, 185);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbSinclair
            // 
            this.rbSinclair.AutoSize = true;
            this.rbSinclair.Checked = true;
            this.rbSinclair.Location = new System.Drawing.Point(12, 12);
            this.rbSinclair.Name = "rbSinclair";
            this.rbSinclair.Size = new System.Drawing.Size(134, 17);
            this.rbSinclair.TabIndex = 2;
            this.rbSinclair.TabStop = true;
            this.rbSinclair.Text = "Export as Sinclair basic";
            this.rbSinclair.UseVisualStyleBackColor = true;
            this.rbSinclair.CheckedChanged += new System.EventHandler(this.rbSinclair_CheckedChanged);
            // 
            // rbBoriel
            // 
            this.rbBoriel.AutoSize = true;
            this.rbBoriel.Location = new System.Drawing.Point(12, 35);
            this.rbBoriel.Name = "rbBoriel";
            this.rbBoriel.Size = new System.Drawing.Size(127, 17);
            this.rbBoriel.TabIndex = 3;
            this.rbBoriel.Text = "Export as Boriel Basic";
            this.rbBoriel.UseVisualStyleBackColor = true;
            this.rbBoriel.CheckedChanged += new System.EventHandler(this.rbBoriel_CheckedChanged);
            // 
            // rbAssembler
            // 
            this.rbAssembler.AutoSize = true;
            this.rbAssembler.Location = new System.Drawing.Point(12, 58);
            this.rbAssembler.Name = "rbAssembler";
            this.rbAssembler.Size = new System.Drawing.Size(119, 17);
            this.rbAssembler.TabIndex = 4;
            this.rbAssembler.Text = "Export as assembler";
            this.rbAssembler.UseVisualStyleBackColor = true;
            this.rbAssembler.CheckedChanged += new System.EventHandler(this.rbAssembler_CheckedChanged);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 158);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(134, 20);
            this.txtPath.TabIndex = 5;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(153, 156);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(46, 23);
            this.btnSelectPath.TabIndex = 6;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // txtIdent
            // 
            this.txtIdent.Enabled = false;
            this.txtIdent.Location = new System.Drawing.Point(65, 104);
            this.txtIdent.Name = "txtIdent";
            this.txtIdent.Size = new System.Drawing.Size(134, 20);
            this.txtIdent.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Identifier";
            // 
            // ckInclude
            // 
            this.ckInclude.AutoSize = true;
            this.ckInclude.Location = new System.Drawing.Point(12, 81);
            this.ckInclude.Name = "ckInclude";
            this.ckInclude.Size = new System.Drawing.Size(133, 17);
            this.ckInclude.TabIndex = 9;
            this.ckInclude.Text = "Include draw functions";
            this.ckInclude.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Address";
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(65, 130);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(134, 20);
            this.txtAddr.TabIndex = 10;
            // 
            // ExportTypeSelector
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(211, 219);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.ckInclude);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdent);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.rbAssembler);
            this.Controls.Add(this.rbBoriel);
            this.Controls.Add(this.rbSinclair);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportTypeSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbSinclair;
        private System.Windows.Forms.RadioButton rbBoriel;
        private System.Windows.Forms.RadioButton rbAssembler;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txtIdent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckInclude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAddr;
    }
}