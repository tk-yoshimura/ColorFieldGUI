// Copyright (c) T.Yoshimura 2019-2021
// https://github.com/tk-yoshimura

namespace ColorControls {

    /// <summary>YCbCr (ITU-R BT.601)</summary>
    public struct YCbCr {
        private double y, cb, cr;

        public static class Consts {
            public const double yr = 0.299;
            public const double yg = 0.587;
            public const double yb = 0.114;
            public const double cb = 1.772;
            public const double cr = 1.402;

            public const double rgb_to_ycbcr_m11 = +yr;
            public const double rgb_to_ycbcr_m21 = +yg;
            public const double rgb_to_ycbcr_m31 = +yb;
            public const double rgb_to_ycbcr_m12 = -yr / cb;
            public const double rgb_to_ycbcr_m22 = -yg / cb;
            public const double rgb_to_ycbcr_m32 = +0.5;
            public const double rgb_to_ycbcr_m13 = +0.5;
            public const double rgb_to_ycbcr_m23 = -yg / cr;
            public const double rgb_to_ycbcr_m33 = -yb / cr;

            public const double ycbcr_to_rgb_m31 = +cr;
            public const double ycbcr_to_rgb_m22 = -yb * cb / yg;
            public const double ycbcr_to_rgb_m32 = -yr * cr / yg;
            public const double ycbcr_to_rgb_m23 = +cb;
        }

        public YCbCr(double y, double cb, double cr) {
            this.y = y > 0 ? (y > 1 ? 1 : y) : 0;
            this.cb = cb > -0.5 ? (cb > +0.5 ? +0.5 : cb) : -0.5;
            this.cr = cr > -0.5 ? (cr > +0.5 ? +0.5 : cr) : -0.5;
        }

        public double Y {
            get {
                return y;
            }
            set {
                y = value > 0 ? (value > 1 ? 1 : value) : 0;
            }
        }

        public double Cb {
            get {
                return cb;
            }
            set {
                cb = value > -0.5 ? (value > +0.5 ? +0.5 : value) : -0.5;
            }
        }

        public double Cr {
            get {
                return cr;
            }
            set {
                cr = value > -0.5 ? (value > +0.5 ? +0.5 : value) : -0.5;
            }
        }

        public RGB RGB {
            set {
                double r = value.R, g = value.G, b = value.B;

                y = Consts.rgb_to_ycbcr_m11 * r + Consts.rgb_to_ycbcr_m21 * g + Consts.rgb_to_ycbcr_m31 * b;
                cb = Consts.rgb_to_ycbcr_m12 * r + Consts.rgb_to_ycbcr_m22 * g + Consts.rgb_to_ycbcr_m32 * b;
                cr = Consts.rgb_to_ycbcr_m13 * r + Consts.rgb_to_ycbcr_m23 * g + Consts.rgb_to_ycbcr_m33 * b;
            }

            get {
                double r = y + Consts.ycbcr_to_rgb_m31 * cr;
                double g = y + Consts.ycbcr_to_rgb_m22 * cb + Consts.ycbcr_to_rgb_m32 * cr;
                double b = y + Consts.ycbcr_to_rgb_m23 * cb;

                return new RGB(r, g, b);
            }
        }

        public static implicit operator RGB(YCbCr ycbcr) {
            return ycbcr.RGB;
        }

        public override string ToString() {
            return $"y={Y:0.000} cb={Cb:0.000} cr={Cr:0.000}";
        }
    }
}
