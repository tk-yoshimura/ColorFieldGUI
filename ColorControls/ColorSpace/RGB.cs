// Copyright (c) T.Yoshimura 2019-2024
// https://github.com/tk-yoshimura

namespace ColorControls {
    public struct RGB {
        public RGB(double r, double g, double b) {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public double R { set; get; }

        public double G { set; get; }

        public double B { set; get; }

        public static implicit operator HSV(RGB rgb) {
            HSV hsv = new() {
                RGB = rgb
            };

            return hsv;
        }

        public static implicit operator YCbCr(RGB rgb) {
            YCbCr ycbcr = new() {
                RGB = rgb
            };

            return ycbcr;
        }

        public readonly RGB Normalize =>
            new(
                R > 0 ? (R > 1 ? 1 : R) : 0,
                G > 0 ? (G > 1 ? 1 : G) : 0,
                B > 0 ? (B > 1 ? 1 : B) : 0
            );

        public override readonly string ToString() {
            return $"r={R:0.000} g={G:0.000} b={B:0.000}";
        }
    }
}
