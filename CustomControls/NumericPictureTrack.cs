using System;
using System.Windows.Forms;
using System.Drawing;

// Copyright (c) T.Yoshimura 2019
// https://github.com/tk-yoshimura

namespace CustomControls {

    public class ValueChangedEventArgs : EventArgs {
        public int Minimum, Maximum, Value;

        public ValueChangedEventArgs(int minimum, int maximum, int value) {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Value = value;
        }

        public override string ToString() {
            return Value.ToString() + " [ " + Minimum.ToString() + " , " + Maximum.ToString() + " ] ";
        }
    }

    public delegate void ValueChangedHandler(object sender, ValueChangedEventArgs me);

    public class NumericPictureTrack : UserControl {
        PictureTrack picture_track = new PictureTrack();
        NumericUpDown numeric_spin = new NumericUpDown();
        int minimum = 0, maximum = 100, decimal_places = 0, increment = 1, value = 0;
        bool event_lock = false;

        public event ValueChangedHandler ValueChanged;

        public NumericPictureTrack() {
            picture_track.Location = new Point(0, 0);
            numeric_spin.Location = new Point(picture_track.Width, 0);
            numeric_spin.Size = new Size(60, 25);

            picture_track.TabIndex = 0;
            numeric_spin.TabIndex = 1;

            this.Controls.Add(picture_track);
            this.Controls.Add(numeric_spin);
            this.FontSize = 12;
            this.Size = DefaultSize;

            this.SetRange();

            picture_track.SliderMove += picture_track_SliderMove;
            numeric_spin.ValueChanged += numeric_spin_ValueChanged;
        }

        private void numeric_spin_ValueChanged(object sender, EventArgs e) {
            if(event_lock)
                return;

            event_lock = true;
            decimal value_dec = numeric_spin.Value;
            for(int i = 0; i < decimal_places; i++) {
                value_dec *= 10;
            }
            value = (int)value_dec;

            picture_track.SliderPosition = value - minimum;

            OnValueChanged(new ValueChangedEventArgs(minimum, maximum, value));
            event_lock = false;
        }

        private void picture_track_SliderMove(object sender, SliderMoveEventArgs se) {
            if(event_lock)
                return;
            
            event_lock = true;
            value = se.Position + minimum;
            decimal value_dec = value;
            for(int i = 0; i < decimal_places; i++) {
                value_dec /= 10;
            }
            numeric_spin.Value = value_dec;

            OnValueChanged(new ValueChangedEventArgs(minimum, maximum, value));
            event_lock = false;
        }

        protected override Size DefaultSize {
            get {
                return new Size(picture_track.Width + numeric_spin.Width, Math.Max(picture_track.Height, numeric_spin.Height));
            }
        }

        public override Size MinimumSize {
            get {
                return DefaultSize;
            }
        }

        public int Minimum {
            set {
                if(value < maximum) {
                    minimum = value;
                }
                else{
                    minimum = value;
                    maximum = value + 1;
                }
                SetRange();
            }
            get {
                return minimum;
            }
        }

        public int Maximum {
            set {
                if(value > minimum) {
                    maximum = value;
                }
                else{
                    minimum = value - 1;
                    maximum = value;
                }
                SetRange();
            }
            get {
                return maximum;
            }
        }

        public int Value {
            set {
                value = (minimum > value) ? minimum : (maximum < value ? maximum : value);

                Decimal value_dec = value;
                for(int i = 0; i < decimal_places; i++) {
                    value_dec /= 10;
                }
                numeric_spin.Value = value_dec;
                picture_track.SliderPosition = value - minimum;
            }
            get {
                return value;
            }
        }

        public int DecimalPlaces {
            set {
                decimal_places = (value > 0) ? value : 0;
                SetRange();
            }
            get {
                return decimal_places;
            }
        }

        public int Increment {
            set {
                increment = value;
                SetRange();
            }
            get {
                return increment;
            }
        }

        public float FontSize {
            set {
                numeric_spin.Font = new Font(numeric_spin.Font.FontFamily, value);
            }
            get {
                return numeric_spin.Font.Size;
            }
        }

        public string FontFamily {
            set {
                numeric_spin.Font = new Font(value, numeric_spin.Font.Size);
            }
            get {
                return numeric_spin.Font.FontFamily.Name;
            }
        }

        public bool Hexadecimal {
            set {
                numeric_spin.Hexadecimal = value; 
            }
            get {
                return numeric_spin.Hexadecimal;
            }
        }

        public override Color ForeColor {
            set {
                numeric_spin.ForeColor = value;
            }
            get {
                return numeric_spin.ForeColor;
            }
        }

        public Bitmap SliderImage {
            set {
                picture_track.SliderImage = value;
            }
            get {
                return picture_track.SliderImage;
            }
        }

        public Bitmap TrackImage {
            set {
                picture_track.TrackImage = value;
            }
            get {
                return picture_track.TrackImage;
            }
        }

        public int SliderTopPosition {
            get {
                return picture_track.SliderTopPosition;
            }
            set {
                picture_track.SliderTopPosition = value;
            }
        }

        private void SetRange() {
            if(minimum > maximum || decimal_places < 0) {
                throw new ArgumentException();
            }

            value = (minimum > value) ? minimum : (maximum < value ? maximum : value);

            Decimal minimum_dec = minimum, maximum_dec = maximum, increment_dec = increment, value_dec = value;
            for(int i = 0; i < decimal_places; i++) {
                minimum_dec /= 10;
                maximum_dec /= 10;
                increment_dec /= 10;
                value_dec /= 10;
            }

            numeric_spin.DecimalPlaces = decimal_places;
            numeric_spin.Minimum = minimum_dec;
            numeric_spin.Maximum = maximum_dec;
            numeric_spin.Value = value_dec;
            numeric_spin.Increment = increment_dec;

            picture_track.SliderRange = maximum - minimum;
        }

        protected virtual void OnValueChanged(ValueChangedEventArgs me) {
            if(ValueChanged != null) {
                ValueChanged(this, me);
            }
        }
    }
}
