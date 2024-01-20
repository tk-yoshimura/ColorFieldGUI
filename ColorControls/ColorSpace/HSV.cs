using System;

// Copyright (c) T.Yoshimura 2019-2024
// https://github.com/tk-yoshimura

namespace ColorControls {
    public struct HSV {
        private double h, s, v;

        public HSV(double h, double s, double v) {
            h %= 6.0;
            if (h < 0) h += 6;
            this.h = double.IsNaN(h) ? 0 : h;

            this.s = s > 0 ? (s > 1 ? 1 : s) : 0;

            this.v = v > 0 ? (v > 1 ? 1 : v) : 0;
        }

        public double H {
            readonly get {
                return h;
            }
            set {
                value %= 6.0;
                if (value < 0) value += 6;
                h = double.IsNaN(value) ? 0 : value;
            }
        }

        public double S {
            readonly get {
                return s;
            }
            set {
                s = value > 0 ? (value > 1 ? 1 : value) : 0;
            }
        }

        public double V {
            readonly get {
                return v;
            }
            set {
                v = value > 0 ? (value > 1 ? 1 : value) : 0;
            }
        }

        public RGB RGB {
            set {
                double r = value.R, g = value.G, b = value.B;

                double max_c = Math.Max(Math.Max(r, g), b);
                double min_c = Math.Min(Math.Min(r, g), b);

                h = max_c - min_c;
                s = (max_c > 0) ? (h / max_c) : 0;
                v = max_c;

                if (h > 0) {
                    if (max_c == r) {
                        h = (g - b) / h + ((g >= b) ? 0.0 : 6.0);
                    }
                    else if (max_c == g) {
                        h = (b - r) / h + 2.0;
                    }
                    else {
                        h = (r - g) / h + 4.0;
                    }
                }
            }

            readonly get {
                double r = v, g = v, b = v;

                if (s > 0) {
                    double d = Math.Floor(h);
                    double f = h - d;
                    int i = (int)d;

                    switch (i) {
                        case 0:
                            g *= 1 - s * (1 - f);
                            b *= 1 - s;
                            break;
                        case 1:
                            r *= 1 - s * f;
                            b *= 1 - s;
                            break;
                        case 2:
                            r *= 1 - s;
                            b *= 1 - s * (1 - f);
                            break;
                        case 3:
                            r *= 1 - s;
                            g *= 1 - s * f;
                            break;
                        case 4:
                            r *= 1 - s * (1 - f);
                            g *= 1 - s;
                            break;
                        default:
                            g *= 1 - s;
                            b *= 1 - s * f;
                            break;
                    }
                }

                return new RGB(r, g, b);
            }
        }

        public static implicit operator RGB(HSV hsv) {
            return hsv.RGB;
        }

        public override readonly string ToString() {
            return $"h={H:0.000} s={S:0.000} v={V:0.000}";
        }
    }
}
