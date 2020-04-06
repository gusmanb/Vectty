namespace Vectty
{
    partial class SpeccyDrawControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tempBox = new Vectty.PixelPerfectPicturebox();
            this.drawBox = new Vectty.PixelPerfectPicturebox();
            ((System.ComponentModel.ISupportInitialize)(this.tempBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tempBox
            // 
            this.tempBox.BackColor = System.Drawing.Color.Transparent;
            this.tempBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tempBox.Location = new System.Drawing.Point(0, 0);
            this.tempBox.Name = "tempBox";
            this.tempBox.Size = new System.Drawing.Size(512, 384);
            this.tempBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.tempBox.TabIndex = 1;
            this.tempBox.TabStop = false;
            this.tempBox.Visible = false;
            this.tempBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tempBox_MouseDown);
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.Color.Transparent;
            this.drawBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawBox.Location = new System.Drawing.Point(0, 0);
            this.drawBox.Margin = new System.Windows.Forms.Padding(0);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(512, 384);
            this.drawBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.drawBox.TabIndex = 0;
            this.drawBox.TabStop = false;
            this.drawBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseDown);
            this.drawBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseMove);
            this.drawBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseUp);
            // 
            // SpeccyDrawControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tempBox);
            this.Controls.Add(this.drawBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SpeccyDrawControl";
            this.Size = new System.Drawing.Size(512, 384);
            this.Resize += new System.EventHandler(this.SpeccyDrawControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tempBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PixelPerfectPicturebox drawBox;
        private PixelPerfectPicturebox tempBox;
    }
}
