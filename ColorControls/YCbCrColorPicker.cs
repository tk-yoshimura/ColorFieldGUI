using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {
    public class YCbCrColorChangedEventArgs : EventArgs {
        public YCbCr YCbCr { private set; get; }

        public YCbCrColorChangedEventArgs(YCbCr ycbcr) {
            this.YCbCr = ycbcr;
        }

        public override string ToString() {
            return "y=" + YCbCr.Y.ToString("0.000") + " cb=" + YCbCr.Cb.ToString("0.000") + " cr=" + YCbCr.Cr.ToString("0.000");
        }
    }

    public delegate void YCbCrColorChangedHandler(object sender, YCbCrColorChangedEventArgs cce);

    public class YCbCrColorPicker : UserControl {
        static readonly Bitmap pointer = Properties.Resources.ImagePointer;

        int pic_size;

        Point bar_pos, panel_pos;
        Size bar_size, panel_size;

        YCbCr ycbcr = new(0.5, 0, 0);

        Bitmap bar, panel;

        enum ManipulatePlace { None, Bar, Panel };
        ManipulatePlace manipulate_place = ManipulatePlace.None;

        public YCbCrColorPicker() {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            DrawImage();
        }

        public event YCbCrColorChangedHandler ValueChanged;

        public YCbCr YCbCr {
            get {
                return ycbcr;
            }
            set {
                ycbcr = value;

                DrawPanel();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            if (IsValidSize()) {
                Graphics g = pe.Graphics;

                if (bar is not null) {
                    g.DrawImageUnscaled(bar, bar_pos);
                }

                if (panel is not null) {
                    g.DrawImageUnscaled(panel, panel_pos);
                }

                DrawPointer(g);
            }

            base.OnPaint(pe);
        }

        protected override void OnResize(EventArgs e) {
            manipulate_place = ManipulatePlace.None;
            DrawImage();
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMove(EventArgs e) {
            manipulate_place = ManipulatePlace.None;
            base.OnMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && IsValidSize()) {
                manipulate_place = ManipulatePlace.None;

                if (IsBarArea(e.X, e.Y)) {
                    manipulate_place = ManipulatePlace.Bar;
                    AcceptManipulateBar(e);
                }
                else if (IsPanelArea(e.X, e.Y)) {
                    manipulate_place = ManipulatePlace.Panel;
                    AcceptManipulatePanel(e);
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                manipulate_place = ManipulatePlace.None;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            manipulate_place = ManipulatePlace.None;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && IsValidSize()) {
                if (manipulate_place == ManipulatePlace.Bar) {
                    AcceptManipulateBar(e);
                }
                else if (manipulate_place == ManipulatePlace.Panel) {
                    AcceptManipulatePanel(e);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && manipulate_place == ManipulatePlace.None && IsValidSize()) {
                if (IsBarArea(e.X, e.Y)) {
                    AcceptManipulateBar(e);
                }
                else if (IsPanelArea(e.X, e.Y)) {
                    AcceptManipulatePanel(e);
                }
            }
            base.OnMouseClick(e);
        }

        protected override void OnHandleDestroyed(EventArgs e) {
            if (bar is not null) {
                bar.Dispose();
                bar = null;
            }
            if (bar is not null) {
                bar.Dispose();
                bar = null;
            }
            base.OnHandleDestroyed(e);
        }

        protected void DrawBar() {
            if (bar is not null)
                bar.Dispose();

            if (IsValidSize()) {
                bar = new Bitmap(bar_size.Width, bar_size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else {
                bar = new Bitmap(1, 1);
                return;
            }

            int width = bar.Width, height = bar.Height;
            byte gray;

            byte[] buf = new byte[width * height * 4];

            unsafe {
                fixed (byte* c = buf) {
                    for (int x, y = 0, i = 0; y < height; y++) {

                        gray = (byte)(255 - 256 * y / height);

                        for (x = 0; x < width; x++, i += 4) {
                            c[i] = c[i + 1] = c[i + 2] = gray;
                            c[i + 3] = 255;
                        }
                    }
                }
            }

            var bmpdata = bar.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bar.PixelFormat);
            Marshal.Copy(buf, 0, bmpdata.Scan0, buf.Length);
            bar.UnlockBits(bmpdata);
        }

        protected void DrawPanel() {
            if (panel is not null)
                panel.Dispose();

            if (IsValidSize()) {
                panel = new Bitmap(panel_size.Width, panel_size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else {
                panel = new Bitmap(1, 1);
                return;
            }

            int width = panel.Width, height = panel.Height;
            double inv_pic_size = 1.0 / (pic_size - 1);
            double r, g, b, cb, cr;

            byte[] buf = new byte[width * height * 4];

            static byte clip(double c) => (byte)Math.Max(Math.Min(c, 255), 0);

            unsafe {
                fixed (byte* c = buf) {
                    for (int x, y = 0, i = 0; y < height; y++) {

                        cr = 0.5 - y * inv_pic_size;

                        for (x = 0; x < width; x++) {

                            cb = x * inv_pic_size - 0.5;

                            b = ycbcr.Y + 1.7364369465163 * cr - 0.13272312247338 * cb;
                            g = ycbcr.Y - 0.4182635918629 * cr - 0.71007339166301 * cb;
                            r = ycbcr.Y + 0.1590866773267 * cr + 1.44462714671624 * cb;

                            c[i++] = clip(b * 255 + 0.5);
                            c[i++] = clip(g * 255 + 0.5);
                            c[i++] = clip(r * 255 + 0.5);
                            c[i++] = 255;
                        }
                    }
                }
            }

            var bmpdata = panel.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, panel.PixelFormat);
            Marshal.Copy(buf, 0, bmpdata.Scan0, buf.Length);
            panel.UnlockBits(bmpdata);
        }

        protected void DrawImage() {
            int mx, my;
            pic_size = Math.Min(this.Width * 10 / 13, this.Height) - 4;
            mx = (this.Width - pic_size * 13 / 10) / 2;
            my = (this.Height - pic_size) / 2;

            bar_size = new Size(pic_size / 5, pic_size);
            panel_size = new Size(pic_size, pic_size);

            bar_pos = new Point(mx, my);
            panel_pos = new Point(mx + pic_size * 3 / 10, my);

            DrawBar();
            DrawPanel();
        }

        private void DrawPointer(Graphics g) {
            if (pointer is null || g is null || !IsValidSize()) {
                return;
            }

            g.DrawImageUnscaled(pointer, bar_pos.X + bar_size.Width / 2 - pointer.Width / 2, (int)(bar_pos.Y + (1 - ycbcr.Y) * (bar_size.Height - 1)) - pointer.Height / 2);
            g.DrawImageUnscaled(pointer, (int)(panel_pos.X + (ycbcr.Cb + 0.5) * (panel_size.Width - 1)) - pointer.Width / 2, (int)(panel_pos.Y + (ycbcr.Cr + 0.5) * (panel_size.Height - 1)) - pointer.Width / 2);
        }

        private void AcceptManipulateBar(MouseEventArgs e) {
            if (!IsValidSize()) {
                return;
            }

            ycbcr.Y = 1 - (double)(e.Y - bar_pos.Y) / (double)(bar_size.Height - 1);

            DrawPanel();
            Invalidate();

            ValueChanged?.Invoke(this, new YCbCrColorChangedEventArgs(YCbCr));
        }

        private void AcceptManipulatePanel(MouseEventArgs e) {
            if (!IsValidSize()) {
                return;
            }

            double inv_pic_size = 1.0 / (pic_size - 1);

            ycbcr.Cb = (e.X - panel_pos.X) * inv_pic_size - 0.5;
            ycbcr.Cr = (e.Y - panel_pos.Y) * inv_pic_size - 0.5;

            Invalidate();

            ValueChanged?.Invoke(this, new YCbCrColorChangedEventArgs(YCbCr));
        }

        private bool IsBarArea(int x, int y) {
            if (!IsValidSize()) {
                return false;
            }

            return (bar_pos.X <= x && (bar_pos.X + bar_size.Width) > x && bar_pos.Y <= y && (bar_pos.Y + bar_size.Height) > y);
        }

        private bool IsPanelArea(int x, int y) {
            if (!IsValidSize()) {
                return false;
            }

            return (panel_pos.X <= x && (panel_pos.X + panel_size.Width) > x && panel_pos.Y <= y && (panel_pos.Y + panel_size.Height) > y);
        }

        private bool IsValidSize() {
            return pic_size > 49;
        }
    }
}
