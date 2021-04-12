using System;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace ColorControls {
    public class YCbCrColorChangedEventArgs : EventArgs {
        public YCbCr YCbCr { private set; get; }

        public YCbCrColorChangedEventArgs(YCbCr ycbcr) {
            this.YCbCr = ycbcr;
        }
    }

    public delegate void YCbCrColorChangedHandler(object sender, YCbCrColorChangedEventArgs cce);
}
