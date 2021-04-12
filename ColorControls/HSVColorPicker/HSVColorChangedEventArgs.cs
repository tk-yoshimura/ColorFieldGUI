using System;

// Copyright (c) T.Yoshimura 2019-2021
// https://github.com/tk-yoshimura

namespace ColorControls {
    public class HSVColorChangedEventArgs : EventArgs {
        public HSV HSV { private set; get; }

        public HSVColorChangedEventArgs(HSV hsv) {
            this.HSV = hsv;
        }
    }

    public delegate void HSVColorChangedHandler(object sender, HSVColorChangedEventArgs cce);
}
