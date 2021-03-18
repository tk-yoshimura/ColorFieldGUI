using System.Windows.Forms;

namespace CustomControls {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void numt_ValueChanged(object sender, ValueChangedEventArgs me) {
            status_text.Text = me.ToString();
        }

        private void cc_HSVColorChanged(object sender, HSVColorChangedEventArgs cce) {
            double r, g, b;

            cc.HSV.GetRGB(out r, out g, out b);

            status_text.Text = cce.ToString() + r.ToString(" R=0.000") + g.ToString(" G=0.000") + b.ToString(" B=0.000");
        }
    }
}
