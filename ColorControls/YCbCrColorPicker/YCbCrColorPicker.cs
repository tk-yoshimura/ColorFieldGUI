using System.Drawing;
using System.Windows.Forms;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {
    public partial class YCbCrColorPicker : UserControl {
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

        public YCbCr Value {
            get {
                return ycbcr;
            }
            set {
                ycbcr = value;

                DrawPanel();
                Invalidate();
            }
        }

        private void AcceptManipulateBar(MouseEventArgs e) {
            if (!IsValidSize()) {
                return;
            }

            ycbcr.Y = 1 - (double)(e.Y - bar_pos.Y) / (double)(bar_size.Height - 1);

            DrawPanel();
            Invalidate();

            ValueChanged?.Invoke(this, new YCbCrColorChangedEventArgs(Value));
        }

        private void AcceptManipulatePanel(MouseEventArgs e) {
            if (!IsValidSize()) {
                return;
            }

            double inv_pic_size = 1.0 / (pic_size - 1);

            ycbcr.Cb = (e.X - panel_pos.X) * inv_pic_size - 0.5;
            ycbcr.Cr = (e.Y - panel_pos.Y) * inv_pic_size - 0.5;

            Invalidate();

            ValueChanged?.Invoke(this, new YCbCrColorChangedEventArgs(Value));
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
