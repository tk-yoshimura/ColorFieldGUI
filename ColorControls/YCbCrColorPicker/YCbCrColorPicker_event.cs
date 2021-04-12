using System;
using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {
    public partial class YCbCrColorPicker {
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
    }
}
