using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019-2021
// https://github.com/tk-yoshimura

namespace ColorControls {
    public class HSVColorChangedEventArgs : EventArgs {
        public HSV HSV { private set; get; }

        public HSVColorChangedEventArgs(HSV hsv) {
            this.HSV = hsv;
        }

        public override string ToString() {
            return "h=" + HSV.H.ToString("0.000") + " s=" + HSV.S.ToString("0.000") + " v=" + HSV.V.ToString("0.000");
        }
    }

    public delegate void HSVColorChangedHandler(object sender, HSVColorChangedEventArgs cce);

    public partial class HSVColorPicker : UserControl {

        static readonly Bitmap pointer = Properties.Resources.ImagePointer;

        int pic_size, pic_center;
        Point circle_pos, tri_pos, prev_pointer_pos;
        Size circle_size, tri_size;

        HSV hsv;

        Bitmap circle, triangle;
        bool[,] tri_area;

        enum ManipulatePlace { None, Circle, Triangle };
        ManipulatePlace manipulate_place = ManipulatePlace.None;

        public HSVColorPicker() {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            DrawImage();
        }

        public event HSVColorChangedHandler ValueChanged;

        public HSV HSV {
            get {
                return hsv;
            }
            set {
                if (hsv.H != value.H) {
                    hsv = value;
                    DrawTriangle();
                }
                hsv = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            if (IsValidSize()) {
                Graphics g = pe.Graphics;

                if (circle is not null) {
                    g.DrawImageUnscaled(circle, circle_pos);
                }

                if (triangle is not null) {
                    g.DrawImageUnscaled(triangle, tri_pos);
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

                if (IsCircleArea(e.X, e.Y)) {
                    manipulate_place = ManipulatePlace.Circle;
                    AcceptManipulateCircle(e);
                }
                else if (IsTriangleArea(e.X, e.Y)) {
                    manipulate_place = ManipulatePlace.Triangle;
                    AcceptManipulateTriangle(e);
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
                if (manipulate_place == ManipulatePlace.Circle) {
                    AcceptManipulateCircle(e);
                }
                else if (manipulate_place == ManipulatePlace.Triangle) {
                    AcceptManipulateTriangle(e);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && manipulate_place == ManipulatePlace.None && IsValidSize()) {
                if (IsCircleArea(e.X, e.Y)) {
                    AcceptManipulateCircle(e);
                }
                else if (IsTriangleArea(e.X, e.Y)) {
                    AcceptManipulateTriangle(e);
                }
            }
            base.OnMouseClick(e);
        }

        protected override void OnHandleDestroyed(EventArgs e) {
            if (circle is not null) {
                circle.Dispose();
                circle = null;
            }
            if (triangle is not null) {
                triangle.Dispose();
                triangle = null;
            }
            base.OnHandleDestroyed(e);
        }

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

        private void AcceptManipulateCircle(MouseEventArgs e) {
            if (!IsValidSize()) {
                return;
            }

            double dx = e.X - circle_pos.X - pic_center, dy = e.Y - circle_pos.Y - pic_center;

            if (Math.Sqrt(dx * dx + dy * dy) < (pic_center * 0.3)) {
                return;
            }

            hsv.H = (Math.Atan2(-dx, dy) / Math.PI + 1.0) * 3.0;
            DrawTriangle();
            Invalidate();

            ValueChanged?.Invoke(this, new HSVColorChangedEventArgs(hsv));
        }

        private void AcceptManipulateTriangle(MouseEventArgs e) {
            if (!IsValidSize()) {
                return;
            }

            int x = e.X, y = e.Y;
            bool is_accept = false;

            if (IsTriangleArea(x, y)) {
                is_accept = true;
            }
            else {
                int buf_x = prev_pointer_pos.X, buf_y = prev_pointer_pos.Y;
                int[] dx = { +1, -1, +1, -1, +1, -1, 0, 0 };
                int[] dy = { +1, +1, -1, -1, 0, 0, +1, -1 };

                bool is_move(int new_x, int new_y) {
                    if (IsTriangleArea(new_x, new_y) == false) {
                        return false;
                    }

                    int odx = x - buf_x, ody = y - buf_y;
                    int ndx = x - new_x, ndy = y - new_y;

                    if ((odx * odx + ody * ody) < (ndx * ndx + ndy * ndy))
                        return false;

                    return true;
                }

                for (int i = 0, j; i < pic_center; i++) {
                    for (j = 0; j < 8; j++) {
                        if (is_move(buf_x + dx[j], buf_y + dy[j])) {
                            buf_x += dx[j];
                            buf_y += dy[j];
                            is_accept = true;
                            break;
                        }
                    }
                    if (j == 8)
                        break;
                }

                x = buf_x; y = buf_y;
            }

            if (is_accept) {
                int width = tri_size.Width, height = tri_size.Height;
                double inv_w = 1.0 / (width - 7), inv_h = 1.0 / (width * 0.5 * Math.Sqrt(3.0) - 7);
                double dx, dy, sat, val, ds;

                prev_pointer_pos.X = x; prev_pointer_pos.Y = y;

                x -= tri_pos.X; y -= tri_pos.Y;

                dx = (x - 3) * inv_w;
                dx = (dx < 0) ? 0 : (dx > 1) ? 1 : dx;
                dy = (height - y - 2) * inv_h;
                dy = (dy < 0) ? 0 : (dy > 1) ? 1 : dy;
                ds = 2 * dx + dy;
                val = ds * 0.5;
                sat = (ds > 0) ? (2 * dy / ds) : 0;

                hsv.V = val > 0 ? (val > 1 ? 1 : val) : 0;
                hsv.S = sat > 0 ? (sat > 1 ? 1 : sat) : 0;
                Invalidate();

                ValueChanged?.Invoke(this, new HSVColorChangedEventArgs(hsv));
            }
        }

        private bool IsCircleArea(int x, int y) {
            if (!IsValidSize()) {
                return false;
            }

            double dx = x - circle_pos.X - pic_center, dy = y - circle_pos.Y - pic_center, norm = Math.Sqrt(dx * dx + dy * dy);
            return (norm > (0.76 * pic_center) && norm < (0.92 * pic_center));
        }

        private bool IsTriangleArea(int x, int y) {
            if (tri_area is null || !IsValidSize()) {
                return false;
            }

            int w = tri_area.GetLength(0), h = tri_area.GetLength(1);
            x -= tri_pos.X; y -= tri_pos.Y;

            if (x < 0 || x >= w || y < 0 || y >= h) {
                return false;
            }

            return tri_area[x, y];
        }

        private bool IsValidSize() {
            return pic_size > 49;
        }
    }
}
