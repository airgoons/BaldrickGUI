using System.Diagnostics;

namespace BaldrickGUI
{
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            if (radioButton1.Checked) {
                label3.Text = "Local Selected";
                Actions.LocalDataSelected();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            if (radioButton2.Checked) {
                // open dialog
                label3.Text = "Google Sheets Selected";
                Actions.GoogleSheetsDataSelected();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Actions.UpdateBaldrick();
        }

        private void button2_Click(object sender, EventArgs e) {
            Actions.RunBaldrick();
        }
    }
}
