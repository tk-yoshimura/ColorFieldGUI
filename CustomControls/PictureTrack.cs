using System;
using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace CustomControls {
    public class SliderMoveEventArgs : EventArgs {
        public int Range, Position;

        public SliderMoveEventArgs(int range, int position){
            this.Range = range;
            this.Position = position;
        }

        public override string ToString() {
            return Position.ToString() + " / "  + Range.ToString();
        }
    }

    public delegate void SliderMoveHandler(object sender, SliderMoveEventArgs se);

    public class PictureTrack : UserControl {
        static Bitmap default_track, default_slider;
        
        int slider_position = 0, slider_range = 100, slider_change = 1, slider_top_position = 18, key_press_count = 0;
        bool is_accept_mouse = false;
        Bitmap track = new Bitmap(default_track), slider = new Bitmap(default_slider);
        
        public event SliderMoveHandler SliderMove;

        static PictureTrack() { 
            default_track = new Bitmap(143, 22);
            default_slider = new Bitmap(7, 7);

            using(Graphics g_track = Graphics.FromImage(default_track)) {
                g_track.Clear(Color.DarkGray);
            }

            using(Graphics g_slider = Graphics.FromImage(default_slider)) {
                Pen p = new Pen(Color.DarkGray, (float)0.5);
                Brush b = new SolidBrush(Color.Black);
                Point[] points = new Point[3]{new Point(0, 6), new Point(6, 6), new Point(3, 0)};

                g_slider.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g_slider.DrawPolygon(p, points);
                g_slider.FillPolygon(b, points);
            }
        }

        public PictureTrack(){
            Size = DefaultSize;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        
        public Bitmap TrackImage {
            set {
                if(value == null) {
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
                if(value == null) {
                    return;
                }

                slider = value;
                Invalidate();
            }
            get {
                return slider;
            }
        }

        public int SliderPosition {
            get {
                return slider_position;
            }
            set {
                slider_position = value > 0 ? (value > slider_range ? slider_range : value) : 0;
                Invalidate();
            }
        }

        public int SliderRange {
            get {
                return slider_range;
            }
            set {
                slider_range = (value > 0) ? value : 1;
                this.Invalidate();
            }
        }

        public int SliderChange { 
            get {
                return slider_change;
            }
            set {
                slider_change = (value > 0) ? value : 1;
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
                if(track != null && slider != null) {
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

        protected override void OnPaint(PaintEventArgs pe) {
            if(track != null && slider != null) {
                Size s = this.DefaultSize;
                Point p = new Point((this.Width - s.Width) / 2, (this.Height - s.Height) / 2);

                Graphics g = pe.Graphics;

                g.DrawImageUnscaled(track, p.X + slider.Width / 2, p.Y);
                g.DrawImageUnscaled(slider, p.X + (track.Width * slider_position - (slider.Width & 1)) / slider_range, p.Y + slider_top_position);
            }

            base.OnPaint(pe);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if(track != null && slider != null) {
                if(e.Button == MouseButtons.Left) {
                    is_accept_mouse = true;

                    int x = e.X - (this.Width - (track.Width + slider.Width)) / 2 + track.Width / slider_range / 2;

                    SliderPosition = slider_range * (x - slider.Width / 2) / track.Width;
                    this.Invalidate();
                    this.OnSliderMove(new SliderMoveEventArgs(slider_range, slider_position));
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                is_accept_mouse = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            is_accept_mouse = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if(track != null && slider != null) {
                if(is_accept_mouse && e.Button == MouseButtons.Left) {
                    int x = e.X - (this.Width - (track.Width + slider.Width)) / 2 + track.Width / slider_range / 2;

                    SliderPosition = slider_range * (x - slider.Width / 2) / track.Width;
                    this.Invalidate();
                    this.OnSliderMove(new SliderMoveEventArgs(slider_range, slider_position));
                }
            }
            base.OnMouseMove(e);            
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            if(track != null && slider != null) {
                if(e.Button == MouseButtons.Left) {
                    int x = e.X - (this.Width - (track.Width + slider.Width)) / 2 + track.Width / slider_range / 2;

                    SliderPosition = slider_range * (x - slider.Width / 2) / track.Width;
                    this.Invalidate();
                    this.OnSliderMove(new SliderMoveEventArgs(slider_range, slider_position));
                }
            }
            base.OnMouseClick(e);
        }

        protected override bool IsInputKey(Keys keyData) {
            if(keyData == Keys.Left || keyData == Keys.Right) {
                return true;
            }

            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if(e.KeyData != Keys.Left && e.KeyData != Keys.Right) {
                return;
            }

            key_press_count++;
            int change = SliderChange;
            for(int i = 5; i <= 50; i += 5) {
                if(key_press_count > i) {
                    change *= 2;
                }
                else {
                    break;
                }
            }

            if(e.KeyData == Keys.Left){
                SliderPosition -= change;
            }
            if(e.KeyData == Keys.Right){
                SliderPosition += change;
            }

            this.Invalidate();
            this.OnSliderMove(new SliderMoveEventArgs(slider_range, slider_position));
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            if(e.KeyData == Keys.Left || e.KeyData == Keys.Right) {
                key_press_count = 0;
            }
        }

        protected virtual void OnSliderMove(SliderMoveEventArgs se) {
            if(SliderMove != null) {
                SliderMove(this, se);
            }
        }
    }
}
