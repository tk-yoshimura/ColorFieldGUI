namespace CustomControls {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.status_strip = new System.Windows.Forms.StatusStrip();
            this.status_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.cc = new CustomControls.HSVColorPicker();
            this.numt = new CustomControls.NumericPictureTrack();
            this.status_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // status_strip
            // 
            this.status_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_text});
            this.status_strip.Location = new System.Drawing.Point(0, 526);
            this.status_strip.Name = "status_strip";
            this.status_strip.Size = new System.Drawing.Size(648, 22);
            this.status_strip.TabIndex = 1;
            this.status_strip.Text = "statusStrip1";
            // 
            // status_text
            // 
            this.status_text.Name = "status_text";
            this.status_text.Size = new System.Drawing.Size(67, 17);
            this.status_text.Text = "起動しました";
            // 
            // cc
            // 
            this.cc.Location = new System.Drawing.Point(12, 12);
            this.cc.Name = "cc";
            this.cc.Size = new System.Drawing.Size(614, 480);
            this.cc.TabIndex = 3;
            this.cc.HSVColorChanged += cc_HSVColorChanged;
            this.cc.HSV = new HSVColorPicker.HSVparam(34, 3, 1);
            // 
            // numt
            // 
            this.numt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numt.DecimalPlaces = 0;
            this.numt.FontFamily = "MS UI Gothic";
            this.numt.FontSize = 12F;
            this.numt.Hexadecimal = false;
            this.numt.Increment = 1;
            this.numt.Location = new System.Drawing.Point(12, 498);
            this.numt.Maximum = 100;
            this.numt.Minimum = 0;
            this.numt.MinimumSize = new System.Drawing.Size(210, 25);
            this.numt.Name = "numt";
            this.numt.Size = new System.Drawing.Size(210, 25);
            this.numt.SliderImage = ((System.Drawing.Bitmap)(resources.GetObject("numt.SliderImage")));
            this.numt.SliderTopPosition = 18;
            this.numt.TabIndex = 2;
            this.numt.TrackImage = ((System.Drawing.Bitmap)(resources.GetObject("numt.TrackImage")));
            this.numt.Value = 0;
            this.numt.ValueChanged += new CustomControls.ValueChangedHandler(this.numt_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 548);
            this.Controls.Add(this.cc);
            this.Controls.Add(this.numt);
            this.Controls.Add(this.status_strip);
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.status_strip.ResumeLayout(false);
            this.status_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip status_strip;
        private System.Windows.Forms.ToolStripStatusLabel status_text;
        private NumericPictureTrack numt;
        private HSVColorPicker cc;
    }
}