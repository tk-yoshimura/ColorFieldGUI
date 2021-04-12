using System;
using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {

    public partial class PictureTrack : UserControl {
        static readonly Bitmap default_track, default_slider;

        int slider_position = 0, slider_range = 100, slider_shifts = 1, slider_top_position = 18, key_press_count = 0;
        bool is_accept_mouse = false;
        Bitmap track = new(default_track), slider = new(default_slider);

        public event SliderMoveHandler ValueChanged;

        static PictureTrack() {
            default_track = new Bitmap(143, 22);
            default_slider = new Bitmap(7, 7);

            using (Graphics g_track = Graphics.FromImage(default_track)) {
                g_track.Clear(Color.DarkGray);
            }

            using Graphics g_slider = Graphics.FromImage(default_slider);

            Pen p = new(Color.DarkGray, (float)0.5);
            Brush b = new SolidBrush(Color.Black);
            Point[] points = new Point[3] { new Point(0, 6), new Point(6, 6), new Point(3, 0) };

            g_slider.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g_slider.DrawPolygon(p, points);
            g_slider.FillPolygon(b, points);
        }

        public PictureTrack() {
            Size = DefaultSize;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        public Bitmap TrackImage {
            set {
                if (value is null) {
                    return;
                }

                track = value;
                Invalidate();
            }
            get {
                return track;
            }
        }

        public Bitmap SliderImage {
            set {
                if (value is null) {
                    return;
                }

                slider = value;
                Invalidate();
            }
            get {
                return slider;
            }
        }

        public int Value {
            get {
                return slider_position;
            }
            set {
                slider_position = value > 0 ? (value > slider_range ? slider_range : value) : 0;
                Invalidate();
            }
        }

        public int Range {
            get {
                return slider_range;
            }
            set {
                slider_range = (value > 0) ? value : 1;
                Invalidate();
            }
        }

        public int Shifts {
            get {
                return slider_shifts;
            }
            set {
                slider_shifts = (value > 0) ? value : 1;
            }
        }

        public int SliderTopPosition {
            get {
                return slider_top_position;
            }
            set {
                slider_top_position = (value >= 0) ? value : 0;
                Invalidate();
            }
        }

        protected override Size DefaultSize {
            get {
                if (track is not null && slider is not null) {
                    return new Size(track.Width + slider.Width, Math.Max(track.Height, slider.Height + slider_top_position));
                }

                return new Size(0, 0);
            }
        }

        public override Size MinimumSize {
            get {
                return DefaultSize;
            }
        }

        protected override bool IsInputKey(Keys keyData) {
            if (keyData == Keys.Left || keyData == Keys.Right) {
                return true;
            }

            return base.IsInputKey(keyData);
        }
    }
}
