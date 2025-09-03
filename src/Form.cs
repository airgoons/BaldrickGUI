namespace BaldrickGUI
{
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();
            updateBaldrick_button_Click(this, new EventArgs());
        }

        private void local_dataSource_radioButton_CheckedChanged(object sender, EventArgs e) {
            if (local_dataSource_radioButton.Checked) {
                dataSource_info.Text = "";
                select_data_source_info.Text = "";
                bool result = Actions.LocalDataSelected(dataSource_info, select_data_source_info);

                if (result) {
                    runBaldrick_button.Enabled = true;
                }
                else {
                    local_dataSource_radioButton.Checked = false;
                    runBaldrick_button.Enabled = false;
                }

            }
        }

        private async void googleSheets_dataSource_radioButton_CheckedChanged(object sender, EventArgs e) {
            if (googleSheets_dataSource_radioButton.Checked) {
                // open dialog
                dataSource_info.Text = "";
                select_data_source_info.Text = "";

                bool result = await Actions.GoogleSheetsDataSelected(dataSource_info, select_data_source_info);
                
                if (result) {
                    runBaldrick_button.Enabled = true;
                }
                else {
                    googleSheets_dataSource_radioButton.Checked = false;
                    runBaldrick_button.Enabled = false;
                }
            }
        }

        private async void updateBaldrick_button_Click (object sender, EventArgs e) {
            updateBaldrick_button.Enabled = false;
            await Actions.UpdateBaldrick(update_baldrick_info);
            updateBaldrick_button.Enabled = true;
        }

        private async void runBaldrick_button_Click(object sender, EventArgs e) {
            List<ButtonBase> buttons = new List<ButtonBase> {
                runBaldrick_button, updateBaldrick_button, local_dataSource_radioButton, googleSheets_dataSource_radioButton
            };

            // prevent UI and file access issues by disabling the various UI controls
            foreach (var button in buttons) {
                button.Enabled = false;
            }

            await Actions.RunBaldrick(run_baldrick_info, local_dataSource_radioButton, googleSheets_dataSource_radioButton);

            // re-enable when task is complete
            foreach (var button in buttons) {
                button.Enabled = true;
            }
        }
    }
}

