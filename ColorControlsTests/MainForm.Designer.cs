
using ColorControls;

namespace ColorControlsTests {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.hsvColorPicker = new ColorControls.HSVColorPicker();
            this.pictureTrack = new ColorControls.PictureTrack();
            this.ycbcrColorPicker = new ColorControls.YCbCrColorPicker();
            this.trackBarH = new System.Windows.Forms.TrackBar();
            this.trackBarS = new System.Windows.Forms.TrackBar();
            this.trackBarV = new System.Windows.Forms.TrackBar();
            this.labelH = new System.Windows.Forms.Label();
            this.labelS = new System.Windows.Forms.Label();
            this.labelV = new System.Windows.Forms.Label();
            this.labelHSV = new System.Windows.Forms.Label();
            this.labelCr = new System.Windows.Forms.Label();
            this.labelCb = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.trackBarCr = new System.Windows.Forms.TrackBar();
            this.trackBarCb = new System.Windows.Forms.TrackBar();
            this.trackBarY = new System.Windows.Forms.TrackBar();
            this.labelYCbCr = new System.Windows.Forms.Label();
            this.trackBarPTrack = new System.Windows.Forms.TrackBar();
            this.labelPTrack = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // hsvColorPicker
            // 
            this.hsvColorPicker.Location = new System.Drawing.Point(30, 16);
            this.hsvColorPicker.Name = "hsvColorPicker";
            this.hsvColorPicker.Size = new System.Drawing.Size(150, 150);
            this.hsvColorPicker.TabIndex = 0;
            this.hsvColorPicker.ValueChanged += new ColorControls.HSVColorChangedHandler(this.HSVColorPicker_ValueChanged);
            // 
            // pictureTrack
            // 
            this.pictureTrack.Location = new System.Drawing.Point(30, 212);
            this.pictureTrack.MinimumSize = new System.Drawing.Size(150, 25);
            this.pictureTrack.Name = "pictureTrack";
            this.pictureTrack.Range = 100;
            this.pictureTrack.Shifts = 1;
            this.pictureTrack.Size = new System.Drawing.Size(150, 25);
            this.pictureTrack.SliderImage = ((System.Drawing.Bitmap)(resources.GetObject("pictureTrack.SliderImage")));
            this.pictureTrack.SliderTopPosition = 18;
            this.pictureTrack.TabIndex = 2;
            this.pictureTrack.TrackImage = ((System.Drawing.Bitmap)(resources.GetObject("pictureTrack.TrackImage")));
            this.pictureTrack.Value = 0;
            this.pictureTrack.ValueChanged += new ColorControls.SliderMoveHandler(this.PictureTrack_ValueChanged);
            // 
            // ycbcrColorPicker
            // 
            this.ycbcrColorPicker.Location = new System.Drawing.Point(500, 16);
            this.ycbcrColorPicker.Name = "ycbcrColorPicker";
            this.ycbcrColorPicker.Size = new System.Drawing.Size(150, 150);
            this.ycbcrColorPicker.TabIndex = 3;
            this.ycbcrColorPicker.ValueChanged += new ColorControls.YCbCrColorChangedHandler(this.YCbCrColorPicker_ValueChanged);
            // 
            // trackBarH
            // 
            this.trackBarH.Location = new System.Drawing.Point(225, 34);
            this.trackBarH.Maximum = 600;
            this.trackBarH.Name = "trackBarH";
            this.trackBarH.Size = new System.Drawing.Size(187, 45);
            this.trackBarH.TabIndex = 4;
            this.trackBarH.TickFrequency = 20;
            this.trackBarH.Scroll += new System.EventHandler(this.TrackBarHSV_Scroll);
            // 
            // trackBarS
            // 
            this.trackBarS.Location = new System.Drawing.Point(225, 85);
            this.trackBarS.Maximum = 500;
            this.trackBarS.Name = "trackBarS";
            this.trackBarS.Size = new System.Drawing.Size(187, 45);
            this.trackBarS.TabIndex = 5;
            this.trackBarS.TickFrequency = 20;
            this.trackBarS.Scroll += new System.EventHandler(this.TrackBarHSV_Scroll);
            // 
            // trackBarV
            // 
            this.trackBarV.Location = new System.Drawing.Point(225, 136);
            this.trackBarV.Maximum = 500;
            this.trackBarV.Name = "trackBarV";
            this.trackBarV.Size = new System.Drawing.Size(187, 45);
            this.trackBarV.TabIndex = 6;
            this.trackBarV.TickFrequency = 20;
            this.trackBarV.Scroll += new System.EventHandler(this.TrackBarHSV_Scroll);
            // 
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.Location = new System.Drawing.Point(225, 16);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(16, 15);
            this.labelH.TabIndex = 7;
            this.labelH.Text = "H";
            // 
            // labelS
            // 
            this.labelS.AutoSize = true;
            this.labelS.Location = new System.Drawing.Point(225, 67);
            this.labelS.Name = "labelS";
            this.labelS.Size = new System.Drawing.Size(13, 15);
            this.labelS.TabIndex = 8;
            this.labelS.Text = "S";
            // 
            // labelV
            // 
            this.labelV.AutoSize = true;
            this.labelV.Location = new System.Drawing.Point(224, 118);
            this.labelV.Name = "labelV";
            this.labelV.Size = new System.Drawing.Size(14, 15);
            this.labelV.TabIndex = 9;
            this.labelV.Text = "V";
            // 
            // labelHSV
            // 
            this.labelHSV.AutoSize = true;
            this.labelHSV.Location = new System.Drawing.Point(225, 177);
            this.labelHSV.Name = "labelHSV";
            this.labelHSV.Size = new System.Drawing.Size(0, 15);
            this.labelHSV.TabIndex = 10;
            // 
            // labelCr
            // 
            this.labelCr.AutoSize = true;
            this.labelCr.Location = new System.Drawing.Point(694, 119);
            this.labelCr.Name = "labelCr";
            this.labelCr.Size = new System.Drawing.Size(18, 15);
            this.labelCr.TabIndex = 16;
            this.labelCr.Text = "Cr";
            // 
            // labelCb
            // 
            this.labelCb.AutoSize = true;
            this.labelCb.Location = new System.Drawing.Point(695, 68);
            this.labelCb.Name = "labelCb";
            this.labelCb.Size = new System.Drawing.Size(21, 15);
            this.labelCb.TabIndex = 15;
            this.labelCb.Text = "Cb";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(695, 17);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(14, 15);
            this.labelY.TabIndex = 14;
            this.labelY.Text = "Y";
            // 
            // trackBarCr
            // 
            this.trackBarCr.Location = new System.Drawing.Point(695, 137);
            this.trackBarCr.Maximum = 100;
            this.trackBarCr.Minimum = -100;
            this.trackBarCr.Name = "trackBarCr";
            this.trackBarCr.Size = new System.Drawing.Size(187, 45);
            this.trackBarCr.TabIndex = 13;
            this.trackBarCr.TickFrequency = 10;
            this.trackBarCr.Scroll += new System.EventHandler(this.TrackBarYCbCr_Scroll);
            // 
            // trackBarCb
            // 
            this.trackBarCb.Location = new System.Drawing.Point(695, 86);
            this.trackBarCb.Maximum = 100;
            this.trackBarCb.Minimum = -100;
            this.trackBarCb.Name = "trackBarCb";
            this.trackBarCb.Size = new System.Drawing.Size(187, 45);
            this.trackBarCb.TabIndex = 12;
            this.trackBarCb.TickFrequency = 10;
            this.trackBarCb.Scroll += new System.EventHandler(this.TrackBarYCbCr_Scroll);
            // 
            // trackBarY
            // 
            this.trackBarY.Location = new System.Drawing.Point(695, 35);
            this.trackBarY.Maximum = 500;
            this.trackBarY.Name = "trackBarY";
            this.trackBarY.Size = new System.Drawing.Size(187, 45);
            this.trackBarY.TabIndex = 11;
            this.trackBarY.TickFrequency = 20;
            this.trackBarY.Value = 250;
            this.trackBarY.Scroll += new System.EventHandler(this.TrackBarYCbCr_Scroll);
            // 
            // labelYCbCr
            // 
            this.labelYCbCr.AutoSize = true;
            this.labelYCbCr.Location = new System.Drawing.Point(694, 185);
            this.labelYCbCr.Name = "labelYCbCr";
            this.labelYCbCr.Size = new System.Drawing.Size(0, 15);
            this.labelYCbCr.TabIndex = 17;
            // 
            // trackBarPTrack
            // 
            this.trackBarPTrack.Location = new System.Drawing.Point(224, 212);
            this.trackBarPTrack.Maximum = 100;
            this.trackBarPTrack.Name = "trackBarPTrack";
            this.trackBarPTrack.Size = new System.Drawing.Size(188, 45);
            this.trackBarPTrack.TabIndex = 18;
            this.trackBarPTrack.TickFrequency = 5;
            this.trackBarPTrack.Scroll += new System.EventHandler(this.TrackBarPTrack_Scroll);
            // 
            // labelPTrack
            // 
            this.labelPTrack.AutoSize = true;
            this.labelPTrack.Location = new System.Drawing.Point(30, 254);
            this.labelPTrack.Name = "labelPTrack";
            this.labelPTrack.Size = new System.Drawing.Size(0, 15);
            this.labelPTrack.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 288);
            this.Controls.Add(this.labelPTrack);
            this.Controls.Add(this.trackBarPTrack);
            this.Controls.Add(this.labelYCbCr);
            this.Controls.Add(this.labelCr);
            this.Controls.Add(this.labelCb);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.trackBarCr);
            this.Controls.Add(this.trackBarCb);
            this.Controls.Add(this.trackBarY);
            this.Controls.Add(this.labelHSV);
            this.Controls.Add(this.labelV);
            this.Controls.Add(this.labelS);
            this.Controls.Add(this.labelH);
            this.Controls.Add(this.trackBarV);
            this.Controls.Add(this.trackBarS);
            this.Controls.Add(this.trackBarH);
            this.Controls.Add(this.ycbcrColorPicker);
            this.Controls.Add(this.pictureTrack);
            this.Controls.Add(this.hsvColorPicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HSVColorPicker hsvColorPicker;
        private PictureTrack pictureTrack;
        private YCbCrColorPicker ycbcrColorPicker;
        private System.Windows.Forms.TrackBar trackBarH;
        private System.Windows.Forms.TrackBar trackBarS;
        private System.Windows.Forms.TrackBar trackBarV;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.Label labelS;
        private System.Windows.Forms.Label labelV;
        private System.Windows.Forms.Label labelHSV;
        private System.Windows.Forms.Label labelCr;
        private System.Windows.Forms.Label labelCb;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.TrackBar trackBarCr;
        private System.Windows.Forms.TrackBar trackBarCb;
        private System.Windows.Forms.TrackBar trackBarY;
        private System.Windows.Forms.Label labelYCbCr;
        private System.Windows.Forms.TrackBar trackBarPTrack;
        private System.Windows.Forms.Label labelPTrack;
    }
}

