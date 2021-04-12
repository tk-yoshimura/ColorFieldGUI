using System;

namespace ColorControls {
    public class SliderMoveEventArgs : EventArgs {
        public int Range { private set; get; }
        public int Position { private set; get; }

        public SliderMoveEventArgs(int range, int position) {
            this.Range = range;
            this.Position = position;
        }

        public override string ToString() {
            return $"{Position} / {Range}";
        }
    }

    public delegate void SliderMoveHandler(object sender, SliderMoveEventArgs se);
}
