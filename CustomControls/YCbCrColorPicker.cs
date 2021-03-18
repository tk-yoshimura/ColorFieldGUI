using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace CustomControls {
    class YCbCrColorPicker : UserControl {
        static Bitmap pointer = Properties.Resources.ImagePointer;
        
        int pic_size;
        double cy = 0.50, cb = 0, cr = 0;
        Point bar_pos, panel_pos;
        Size bar_size, panel_size;

        Bitmap bar, panel;

        enum ManipulatePlace { None, Bar, Panel };
        ManipulatePlace manipulate_place = ManipulatePlace.None;

        public YCbCrColorPicker() {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetImage();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            if(IsValidSize()) {
                Graphics g = pe.Graphics;

                if(bar != null) {
                    g.DrawImageUnscaled(bar, bar_pos);
                }

                if(panel != null) {
                    g.DrawImageUnscaled(panel, panel_pos);
                }

                DrawPointer(g);
            }
            
            base.OnPaint(pe);
        }

        protected override void OnResize(EventArgs e) {
            manipulate_place = ManipulatePlace.None;
            SetImage();
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMove(EventArgs e) {
            manipulate_place = ManipulatePlace.None;
            base.OnMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if(e.Button == MouseButtons.Left && IsValidSize()) {
                manipulate_place = ManipulatePlace.None;

                if(IsBarArea(e.X, e.Y)) {
                    manipulate_place = ManipulatePlace.Bar;
                    AcceptManipulateBar(e);
                }
                else if(IsPanelArea(e.X, e.Y)) {
                    manipulate_place = ManipulatePlace.Panel;
                    AcceptManipulatePanel(e);
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                manipulate_place = ManipulatePlace.None;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            manipulate_place = ManipulatePlace.None;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if(e.Button == MouseButtons.Left && IsValidSize()) {
                if(manipulate_place == ManipulatePlace.Bar) {
                    AcceptManipulateBar(e);
                }
                else if(manipulate_place == ManipulatePlace.Panel) {
                    AcceptManipulatePanel(e);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            if(e.Button == MouseButtons.Left && manipulate_place == ManipulatePlace.None && IsValidSize()) {
                if(IsBarArea(e.X, e.Y)) {
                    AcceptManipulateBar(e);
                }
                else if(IsPanelArea(e.X, e.Y)) {
                    AcceptManipulatePanel(e);
                }
            }
            base.OnMouseClick(e);
        }

        protected override void OnHandleDestroyed(EventArgs e) {
            if(bar != null) {
                bar.Dispose();
                bar = null;
            }
            if(bar != null) {
                bar.Dispose();
                bar = null;
            }
            base.OnHandleDestroyed(e);
        }

        protected void SetBar() {
            if(bar != null)
                bar.Dispose();

            if(IsValidSize()) {
                bar = new Bitmap(bar_size.Width, bar_size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else {
                bar = new Bitmap(1, 1);
                return;
            }

            int width = bar.Width, height = bar.Height;
            byte gray;

            byte[] buf = new byte[width * height * 4];

            unsafe{
                fixed (byte* c = buf){
                    for(int x, y = 0, i = 0; y < height; y++) {

                        gray = (byte)(255 - 256 * y / height);

                        for(x = 0; x < width; x++, i += 4) {
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

        protected void SetPanel() {
            if(panel != null)
                panel.Dispose();

            if(IsValidSize()) {
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

            Func<double, byte> clip = c => (byte)Math.Max(Math.Min(c, 255), 0);

            unsafe{
                fixed (byte* c = buf){
                    for(int x, y = 0, i = 0; y < height; y++) {

                        cr = 0.5 - y * inv_pic_size;

                        for(x = 0; x < width; x++) {

                            cb = x * inv_pic_size - 0.5;

                            b = cy + 1.7364369465163 * cr - 0.13272312247338 * cb;
                            g = cy - 0.4182635918629 * cr - 0.71007339166301 * cb;
                            r = cy + 0.1590866773267 * cr + 1.44462714671624 * cb;

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

        protected void SetImage() {
            int mx, my;
            pic_size = Math.Min(this.Width * 10 / 13, this.Height) - 4;
            mx = (this.Width - pic_size * 13 / 10) / 2;
            my = (this.Height - pic_size) / 2;

            bar_size = new Size(pic_size / 5, pic_size);
            panel_size = new Size(pic_size, pic_size);

            bar_pos = new Point(mx, my);
            panel_pos = new Point(mx + pic_size * 3 / 10, my);

            SetBar();
            SetPanel();
        }

        private void DrawPointer(Graphics g) {
            if(pointer == null || g == null || !IsValidSize()) {
                return;
            }

            g.DrawImageUnscaled(pointer, bar_pos.X + bar_size.Width / 2 - pointer.Width / 2, (int)(bar_pos.Y + (1 - cy) * (bar_size.Height - 1)) - pointer.Height / 2);
            g.DrawImageUnscaled(pointer, (int)(panel_pos.X + (cb + 0.5) * (panel_size.Width - 1)) - pointer.Width / 2, (int)(panel_pos.Y + (cr + 0.5) * (panel_size.Height - 1)) - pointer.Width / 2);
        }

        private void AcceptManipulateBar(MouseEventArgs e) {
            if(!IsValidSize()) {
                return;
            }

            cy = 1 - (double)(e.Y - bar_pos.Y) / (double)(bar_size.Height - 1);
            cy = cy > 0 ? (cy > 1 ? 1 : cy) : 0;

            SetPanel();
            Invalidate();
        }

        private void AcceptManipulatePanel(MouseEventArgs e) {
            if(!IsValidSize()) {
                return;
            }

            double inv_pic_size = 1.0 / (pic_size - 1);

            cb = (e.X - panel_pos.X) * inv_pic_size - 0.5;
            cr = (e.Y - panel_pos.Y) * inv_pic_size - 0.5;

            cb = cb > -0.5 ? (cb > +0.5 ? +0.5 : cb) : -0.5;
            cr = cr > -0.5 ? (cr > +0.5 ? +0.5 : cr) : -0.5;

            Invalidate();
        }

        private bool IsBarArea(int x, int y) {
            if(!IsValidSize()) {
                return false;
            }

            return (bar_pos.X <= x && (bar_pos.X + bar_size.Width) > x && bar_pos.Y <= y && (bar_pos.Y + bar_size.Height) > y);
        }

        private bool IsPanelArea(int x, int y) {
            if(!IsValidSize()) {
                return false;
            }

            return (panel_pos.X <= x && (panel_pos.X + panel_size.Width) > x && panel_pos.Y <= y && (panel_pos.Y + panel_size.Height) > y);
        }

        private bool IsValidSize() {
            return pic_size > 49;
        }
    }
}
