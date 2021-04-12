using System;
using System.Drawing;
using System.Runtime.InteropServices;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {
    public partial class YCbCrColorPicker {
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
    }
}
