using System;
using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019-2024
// https://github.com/tk-yoshimura

namespace ColorControls {

    public partial class HSVColorPicker : UserControl {
        int pic_size, pic_center;
        Point circle_pos, tri_pos, prev_pointer_pos;
        Size circle_size, tri_size;

        HSV hsv;

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

        public HSV Value {
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
