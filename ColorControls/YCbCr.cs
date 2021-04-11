// Copyright (c) T.Yoshimura 2019-2021
// https://github.com/tk-yoshimura

namespace ColorControls {
    public struct YCbCr {
        private double y, cb, cr;

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

        public (double r, double g, double b) RGB {
            set {
                (double r, double g, double b) = value;
                r = r > 0 ? (r > 1 ? 1 : r) : 0;
                g = g > 0 ? (g > 1 ? 1 : g) : 0;
                b = b > 0 ? (b > 1 ? 1 : b) : 0;

                y = 0.299 * r + 0.587 * g + 0.114 * b;
                cb = 0.5 * r - 0.3660254037844414 * g - 0.1339745962155585 * b;
                cr = 0.5 * b - 0.1339745962155657 * r - 0.3660254037844459 * g;
            }

            get {
                double r = y + 0.1590866773267 * cr + 1.44462714671624 * cb;
                double g = y - 0.4182635918629 * cr - 0.71007339166301 * cb;
                double b = y + 1.7364369465163 * cr - 0.13272312247338 * cb;

                return (r, g, b);
            }
        }
    }
}
