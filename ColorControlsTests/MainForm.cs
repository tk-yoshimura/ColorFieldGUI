using ColorControls;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ColorControlsTests {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void TrackBarHSV_Scroll(object sender, EventArgs e) {
            HSV hsv = new(trackBarH.Value / 100f, trackBarS.Value / 500f, trackBarV.Value / 500f);

            hsvColorPicker.Value = hsv;

            labelHSV.Text = $"{hsvColorPicker.Value}";

            Trace.WriteLine("TrackBarHSV_Scroll");
        }

        private void HSVColorPicker_ValueChanged(object sender, HSVColorChangedEventArgs cce) {
            HSV hsv = hsvColorPicker.Value;

            trackBarH.Value = (int)(hsv.H * 100);
            trackBarS.Value = (int)(hsv.S * 500);
            trackBarV.Value = (int)(hsv.V * 500);

            labelHSV.Text = $"{hsvColorPicker.Value}";

            Trace.WriteLine("HSVColorPicker_ValueChanged");
        }

        private void TrackBarYCbCr_Scroll(object sender, EventArgs e) {
            YCbCr ycbcr = new(trackBarY.Value / 500f, trackBarCb.Value / 200f, trackBarCr.Value / 200f);

            ycbcrColorPicker.Value = ycbcr;

            labelYCbCr.Text = $"{ycbcrColorPicker.Value}";

            Trace.WriteLine("TrackBarYCbCr_Scroll");
        }

        private void YCbCrColorPicker_ValueChanged(object sender, YCbCrColorChangedEventArgs cce) {
            YCbCr ycbcr = ycbcrColorPicker.Value;

            trackBarY.Value = (int)(ycbcr.Y * 500);
            trackBarCb.Value = (int)(ycbcr.Cb * 200);
            trackBarCr.Value = (int)(ycbcr.Cr * 200);

            labelYCbCr.Text = $"{ycbcrColorPicker.Value}";

            Trace.WriteLine("YCbCrColorPicker_ValueChanged");
        }

        private void TrackBarPTrack_Scroll(object sender, EventArgs e) {
            pictureTrack.Value = trackBarPTrack.Value;

            labelPTrack.Text = $"{pictureTrack.Value}/{pictureTrack.Range}";

            Trace.WriteLine("TrackBarPTrack_Scroll");
        }

        private void PictureTrack_ValueChanged(object sender, SliderMoveEventArgs e) {
            trackBarPTrack.Value = pictureTrack.Value;

            labelPTrack.Text = $"{pictureTrack.Value}/{pictureTrack.Range}";

            Trace.WriteLine("TrackBarPTrack_ValueChanged");
        }
    }
}
