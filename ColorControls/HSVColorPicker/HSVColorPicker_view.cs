using System;
using System.Drawing;
using System.Runtime.InteropServices;

// Copyright (c) T.Yoshimura 2019-2024
// https://github.com/tk-yoshimura

namespace ColorControls {
    public partial class HSVColorPicker {
        static readonly Bitmap pointer = Properties.Resources.ImagePointer;
        Bitmap circle, triangle;

        protected void DrawCircle() {
            if (circle is not null) {
                circle.Dispose();
            }

            if (IsValidSize()) {
                circle = new Bitmap(circle_size.Width, circle_size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else {
                circle = new Bitmap(1, 1);
                return;
            }

            int width = circle.Width, height = circle.Height;
            double outer_thr_sq = pic_center * pic_center * 0.8464, inner_thr_sq = pic_center * pic_center * 0.5776, norm_rate = 0.25 / Math.Sqrt(inner_thr_sq);
            double dx, dy, norm_sq, alpha, hue, r, g, b;

            byte[] buf = new byte[width * height * 4];

            unsafe {
                fixed (byte* c = buf) {
                    for (int x, y = 0, i = 0; y < height; y++) {
                        dy = y - pic_center;
                        for (x = 0; x < width; x++, i += 4) {
                            dx = x - pic_center;
                            norm_sq = dx * dx + dy * dy;

                            if (norm_sq > outer_thr_sq || inner_thr_sq > norm_sq) {
                                continue;
                            }

                            alpha = Math.Min(1, norm_rate * Math.Min(outer_thr_sq - norm_sq, norm_sq - inner_thr_sq));
                            hue = (Math.Atan2(-dx, dy) / Math.PI + 1.0) * 3.0;

                            if (hue < 1) {
                                r = 1;
                                g = hue - 0;
                                b = 0;
                            }
                            else if (hue < 2) {
                                r = 2 - hue;
                                g = 1;
                                b = 0;
                            }
                            else if (hue < 3) {
                                r = 0;
                                g = 1;
                                b = hue - 2;
                            }
                            else if (hue < 4) {
                                r = 0;
                                g = 4 - hue;
                                b = 1;
                            }
                            else if (hue < 5) {
                                r = hue - 4;
                                g = 0;
                                b = 1;
                            }
                            else {
                                r = 1;
                                g = 0;
                                b = 6 - hue;
                            }

                            c[i] = (byte)(b * 255);
                            c[i + 1] = (byte)(g * 255);
                            c[i + 2] = (byte)(r * 255);
                            c[i + 3] = (byte)(alpha * 255);
                        }
                    }
                }
            }

            var bmpdata = circle.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, circle.PixelFormat);
            Marshal.Copy(buf, 0, bmpdata.Scan0, buf.Length);
            circle.UnlockBits(bmpdata);
        }

        protected void DrawTriangle() {
            if (triangle is not null)
                triangle.Dispose();

            if (IsValidSize()) {
                triangle = new Bitmap(tri_size.Width, tri_size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else {
                triangle = new Bitmap(1, 1);
                return;
            }

            int width = triangle.Width, height = triangle.Height;
            double sqrt3 = Math.Sqrt(3.0), sqrt3_div2 = sqrt3 * 0.5, smooth_rate = 0.70, inv_w = 1.0 / (width - 7), inv_h = 1.0 / (sqrt3_div2 * width - 7);
            double alpha, dx, dy, sat, val, r, g, b, ds, d = Math.Floor(hsv.H), f = hsv.H - d;
            int cr_i = (int)d;

            byte[] buf = new byte[width * height * 4];
            tri_area = new bool[width, height];

            unsafe {
                double* cv, cs, cf;

                switch (cr_i) {
                    case 0:
                        cv = &r;
                        cf = &g;
                        cs = &b;
                        f = (1 - f);
                        break;
                    case 1:
                        cf = &r;
                        cv = &g;
                        cs = &b;
                        break;
                    case 2:
                        cs = &r;
                        cv = &g;
                        cf = &b;
                        f = (1 - f);
                        break;
                    case 3:
                        cs = &r;
                        cf = &g;
                        cv = &b;
                        break;
                    case 4:
                        cf = &r;
                        cs = &g;
                        cv = &b;
                        f = (1 - f);
                        break;
                    default:
                        cv = &r;
                        cs = &g;
                        cf = &b;
                        break;
                }

                fixed (byte* c = buf) {
                    for (int x, y = 0, i = 0; y < height; y++) {
                        dy = (height - y - 2) * inv_h;
                        dy = (dy < 0) ? 0 : (dy > 1) ? 1 : dy;

                        for (x = 0; x < width; x++) {
                            alpha = Math.Min(1, smooth_rate * Math.Min((sqrt3 * x) - (height - y), (sqrt3 * (width - x - 1)) - (height - y)));
                            if (alpha < 0) {
                                i += 4;
                                continue;
                            }

                            dx = (x - 3) * inv_w;
                            dx = (dx < 0) ? 0 : (dx > 1) ? 1 : dx;
                            ds = 2 * dx + dy;
                            val = ds * 0.5;
                            sat = (ds > 0) ? (2 * dy / ds) : 0;
                            sat = (sat > 1) ? 1 : sat;

                            r = g = b = val;
                            *cs *= 1 - sat;
                            *cf *= 1 - sat * f;

                            c[i++] = (byte)Math.Min((b * 255 + 0.5), 255);
                            c[i++] = (byte)Math.Min((g * 255 + 0.5), 255);
                            c[i++] = (byte)Math.Min((r * 255 + 0.5), 255);
                            c[i++] = (byte)(alpha * 255);

                            tri_area[x, y] = (alpha >= 1);
                        }
                    }
                }
            }

            var bmpdata = triangle.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, triangle.PixelFormat);
            Marshal.Copy(buf, 0, bmpdata.Scan0, buf.Length);
            triangle.UnlockBits(bmpdata);
        }

        protected void DrawImage() {
            pic_size = Math.Max(Math.Min(this.Width, this.Height) / 2 * 2 - 1, 49);
            pic_center = pic_size / 2;

            circle_size = new Size(pic_size, pic_size);
            tri_size = new Size((pic_size * 63 / 100) / 2 * 2 - 1, pic_size * 273 / 500 + 1);

            circle_pos = new Point((this.Width - circle_size.Width) / 2, (this.Height - circle_size.Height) / 2);
            tri_pos = new Point(circle_pos.X + (circle_size.Width - tri_size.Width) / 2, circle_pos.Y + pic_size * 27 / 200);

            DrawCircle();
            DrawTriangle();
        }

        private void DrawPointer(Graphics g) {
            if (pointer is null || g is null || !IsValidSize()) {
                return;
            }

            g.DrawImageUnscaled(pointer, (int)(pic_center + circle_pos.X + pic_size * Math.Sin(hsv.H * Math.PI / 3.0) * 0.42 - pointer.Width * 0.5 + 1),
                                         (int)(pic_center + circle_pos.Y - pic_size * Math.Cos(hsv.H * Math.PI / 3.0) * 0.42 - pointer.Height * 0.5 + 1));

            int x, y;
            double dx, dy;
            dx = hsv.V - (hsv.V * hsv.S) / 2;
            dy = hsv.S * hsv.V;

            x = (int)(dx * (tri_size.Width - 7));
            y = tri_size.Height - (int)(dy * (tri_size.Width * 0.5 * Math.Sqrt(3.0) - 7) + 5);

            g.DrawImageUnscaled(pointer, x + tri_pos.X, y + tri_pos.Y);
        }
    }
}
