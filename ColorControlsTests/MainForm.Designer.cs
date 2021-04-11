
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
            this.hsvColorPicker1 = new ColorControls.HSVColorPicker();
            this.pictureTrack1 = new ColorControls.PictureTrack();
            this.yCbCrColorPicker1 = new ColorControls.YCbCrColorPicker();
            this.SuspendLayout();
            // 
            // hsvColorPicker1
            // 
            this.hsvColorPicker1.Location = new System.Drawing.Point(30, 34);
            this.hsvColorPicker1.Name = "hsvColorPicker1";
            this.hsvColorPicker1.Size = new System.Drawing.Size(150, 150);
            this.hsvColorPicker1.TabIndex = 0;
            // 
            // pictureTrack1
            // 
            this.pictureTrack1.Location = new System.Drawing.Point(224, 52);
            this.pictureTrack1.MinimumSize = new System.Drawing.Size(150, 25);
            this.pictureTrack1.Name = "pictureTrack1";
            this.pictureTrack1.Size = new System.Drawing.Size(150, 25);
            this.pictureTrack1.SliderChange = 1;
            this.pictureTrack1.SliderImage = ((System.Drawing.Bitmap)(resources.GetObject("pictureTrack1.SliderImage")));
            this.pictureTrack1.SliderPosition = 0;
            this.pictureTrack1.SliderRange = 100;
            this.pictureTrack1.SliderTopPosition = 18;
            this.pictureTrack1.TabIndex = 2;
            this.pictureTrack1.TrackImage = ((System.Drawing.Bitmap)(resources.GetObject("pictureTrack1.TrackImage")));
            // 
            // yCbCrColorPicker1
            // 
            this.yCbCrColorPicker1.Location = new System.Drawing.Point(30, 190);
            this.yCbCrColorPicker1.Name = "yCbCrColorPicker1";
            this.yCbCrColorPicker1.Size = new System.Drawing.Size(150, 150);
            this.yCbCrColorPicker1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 371);
            this.Controls.Add(this.yCbCrColorPicker1);
            this.Controls.Add(this.pictureTrack1);
            this.Controls.Add(this.hsvColorPicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private HSVColorPicker hsvColorPicker1;
        private PictureTrack pictureTrack1;
        private YCbCrColorPicker yCbCrColorPicker1;
    }
}

