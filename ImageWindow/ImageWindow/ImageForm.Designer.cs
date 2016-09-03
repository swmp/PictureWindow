namespace ImageWindow
{
    partial class ImageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageForm));
            this.picTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // picTimer
            // 
            this.picTimer.Enabled = true;
            this.picTimer.Interval = 5000;
            this.picTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 613);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageForm";
            this.Text = "ImageForm";
            this.Load += new System.EventHandler(this.ImageForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer picTimer;

    }
}