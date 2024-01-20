using System;
using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019-2024
// https://github.com/tk-yoshimura

namespace ColorControls {
    public partial class HSVColorPicker {
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
    }
}
