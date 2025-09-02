using System.Diagnostics;
using System.Threading.Tasks;

namespace BaldrickGUI
{
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            if (radioButton1.Checked) {
                dataSource_info.Text = "";
                select_data_source_info.Text = "";
                bool result = Actions.LocalDataSelected(dataSource_info, select_data_source_info);

                if (result) {
                    runBaldrick_button.Enabled = true;
                }
                else {
                    radioButton1.Checked = false;
                    runBaldrick_button.Enabled = false;
                }

            }
        }

        private async void radioButton2_CheckedChanged(object sender, EventArgs e) {
            if (radioButton2.Checked) {
                // open dialog
                dataSource_info.Text = "";
                select_data_source_info.Text = "";

                bool result = await Actions.GoogleSheetsDataSelected(dataSource_info, select_data_source_info);
                
                if (result) {
                    runBaldrick_button.Enabled = true;
                }
                else {
                    radioButton2.Checked = false;
                    runBaldrick_button.Enabled = false;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e) {
            updateBaldrick_button.Enabled = false;
            await Actions.UpdateBaldrick(update_baldrick_info);
            updateBaldrick_button.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e) {
            Actions.RunBaldrick();
        }
    }
}

