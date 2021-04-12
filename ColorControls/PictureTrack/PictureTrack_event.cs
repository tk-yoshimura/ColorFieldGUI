using System;
using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {
    public partial class PictureTrack {
        protected override void OnPaint(PaintEventArgs pe) {
            if (track is not null && slider is not null) {
                Size s = this.DefaultSize;
                Point p = new((this.Width - s.Width) / 2, (this.Height - s.Height) / 2);

                Graphics g = pe.Graphics;

                g.DrawImageUnscaled(track, p.X + slider.Width / 2, p.Y);
                g.DrawImageUnscaled(slider, p.X + (track.Width * slider_position - (slider.Width & 1)) / slider_range, p.Y + slider_top_position);
            }

            base.OnPaint(pe);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (track is not null && slider is not null) {
                if (e.Button == MouseButtons.Left) {
                    is_accept_mouse = true;

                    int x = e.X - (this.Width - (track.Width + slider.Width)) / 2 + track.Width / slider_range / 2;

                    Value = slider_range * (x - slider.Width / 2) / track.Width;
                    Invalidate();

                    ValueChanged?.Invoke(this, new SliderMoveEventArgs(slider_range, slider_position));
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                is_accept_mouse = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            is_accept_mouse = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if (track is not null && slider is not null) {
                if (is_accept_mouse && e.Button == MouseButtons.Left) {
                    int x = e.X - (this.Width - (track.Width + slider.Width)) / 2 + track.Width / slider_range / 2;

                    Value = slider_range * (x - slider.Width / 2) / track.Width;
                    Invalidate();

                    ValueChanged?.Invoke(this, new SliderMoveEventArgs(slider_range, slider_position));
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            if (track is not null && slider is not null) {
                if (e.Button == MouseButtons.Left) {
                    int x = e.X - (this.Width - (track.Width + slider.Width)) / 2 + track.Width / slider_range / 2;

                    Value = slider_range * (x - slider.Width / 2) / track.Width;
                    Invalidate();

                    ValueChanged?.Invoke(this, new SliderMoveEventArgs(slider_range, slider_position));
                }
            }
            base.OnMouseClick(e);
        }


        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.KeyData != Keys.Left && e.KeyData != Keys.Right) {
                return;
            }

            key_press_count++;
            int change = Shifts;
            for (int i = 5; i <= 50; i += 5) {
                if (key_press_count > i) {
                    change *= 2;
                }
                else {
                    break;
                }
            }

            if (e.KeyData == Keys.Left) {
                Value -= change;
            }
            if (e.KeyData == Keys.Right) {
                Value += change;
            }

            Invalidate();

            ValueChanged?.Invoke(this, new SliderMoveEventArgs(slider_range, slider_position));
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            if (e.KeyData == Keys.Left || e.KeyData == Keys.Right) {
                key_press_count = 0;
            }
        }
    }
}
