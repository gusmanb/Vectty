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
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(124, 113);
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
            this.btnCancel.Location = new System.Drawing.Point(12, 113);
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
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 81);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(134, 20);
            this.txtPath.TabIndex = 5;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(153, 79);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(46, 23);
            this.btnSelectPath.TabIndex = 6;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // ExportTypeSelector
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(211, 148);
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
    }
}