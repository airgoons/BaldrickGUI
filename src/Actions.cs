using System.Collections.Specialized;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace BaldrickGUI {
    internal class Actions {
        internal const string baldrick_path = "./baldrick.zip";
        internal static string? selected_local_csv_data_path = "";
        internal static string? selected_gsheets_csv_data_path = "./waypoints.csv";
        internal static async Task UpdateBaldrick(Label update_baldrick_info) {
            update_baldrick_info.Text = "";

            string repo_url = "https://api.github.com/repos/CalirDeminar/Baldrick/releases/latest";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            
            var release_data_response = await client.GetAsync(repo_url);

            var assetsList = new List<Dictionary<string, object>>();
            if (!release_data_response.IsSuccessStatusCode) {
                update_baldrick_info.Text = $"ERROR: HTTP Status Code {release_data_response.StatusCode.ToString()}";
                return;
            }
            else {
                string jsonString = await release_data_response.Content.ReadAsStringAsync();
                var releaseData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
                object? temp;
                releaseData.TryGetValue("assets", out temp);
                
                foreach (var element in ((JsonElement)temp).EnumerateArray()) {
                    Dictionary<string, object>? item = JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText());
                    assetsList.Add(item);
                }
            }

            foreach (var assetData in assetsList) {
                bool initiate_download = false;

                object? asset_filename;
                object? asset_url;
                object? asset_digest;

                assetData.TryGetValue("name", out asset_filename);
                assetData.TryGetValue("browser_download_url", out asset_url);
                assetData.TryGetValue("digest", out asset_digest);

                if (asset_filename.ToString() == "baldrick.zip") {
                    string hashString = "";
                    if (File.Exists(baldrick_path)) {
                        using (SHA256 sha256hash = SHA256.Create()) {
                            FileInfo fInfo = new FileInfo(baldrick_path);
                            using (var fileStream = fInfo.Open(FileMode.Open)) {
                                try {
                                    fileStream.Position = 0;
                                    byte[] hashValue = sha256hash.ComputeHash(fileStream);

                                    StringBuilder stringBuilder = new StringBuilder();
                                    foreach (byte b in hashValue) {
                                        stringBuilder.AppendFormat("{0:X2}", b);
                                    }
                                    hashString = $"sha256:{stringBuilder.ToString().ToLower()}";
                                }
                                catch (IOException e) {
                                    update_baldrick_info.Text = $"ERROR: {e.Message}";
                                }
                                catch (UnauthorizedAccessException e) {
                                    update_baldrick_info.Text = $"ERROR: {e.Message}";
                                }
                            }
                        }

                        if (asset_digest.ToString().ToLower() == hashString) {
                            initiate_download = false;
                            update_baldrick_info.Text = "baldrick.zip:  latest version (sha256)";
                        }
                        else {
                            initiate_download = true;
                        }
                    }
                    else {
                        initiate_download = true;
                    }

                    if (initiate_download) {
                        update_baldrick_info.Text = String.Format("Downloading {0}", asset_filename.ToString());
                        using (Stream contentStream = await client.GetStreamAsync(asset_url.ToString())) {
                            using (FileStream fileStream = new FileStream(asset_filename.ToString(), FileMode.Create, FileAccess.Write)) {
                                await contentStream.CopyToAsync(fileStream);
                            }
                        }
                        update_baldrick_info.Text = "Download complete.";
                    }
                }
            }
        }

        internal static bool LocalDataSelected(Label dataSource_info, Label select_data_source_info) {
            bool result = false;

            var dialog = new OpenFileDialog();
            dialog.Filter = "CSV files (*.csv)|*.csv";
            dialog.Title = "Select Baldrick compatible CSV";
            dialog.InitialDirectory = "./";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            var dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK) {
                selected_local_csv_data_path = dialog.FileName;

                dataSource_info.Text = $"File:\n{selected_local_csv_data_path}";
                result = true;
            }

            return result;
        }

        internal static async Task<bool> GoogleSheetsDataSelected(Label dataSource_info, Label select_data_source_info) {
            bool result = false;

            string url_string = Microsoft.VisualBasic.Interaction.InputBox("Enter a PUBLIC Google Sheets URL", "Data Entry");
            
            // @TODO input validation
            string topLeftCell = Microsoft.VisualBasic.Interaction.InputBox("Enter the TOP LEFT CELL", "Data Entry");
            Tuple<int, int>? tlc;
            try {
                tlc = Utilities.ParseTLC(topLeftCell);
            }
            catch (ArgumentException e) {
                select_data_source_info.Text = $"ERROR:  {e.Message}";
                return false;
            }
            catch (FormatException e) {
                select_data_source_info.Text = $"ERROR:  {e.Message}";
                return false;
            }

            try {
                Uri? uri = new Uri(url_string);
                NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);

                if (!uri.AbsoluteUri.Contains("docs.google.com/spreadsheets/d/")) {
                    select_data_source_info.Text = "Invalid Google Sheets URL";
                    return false;
                }

                string doc_id;
                try {
                    // example uri.AbsolutePath return:  /spreadsheets/d/1IEMC4d2dG72zHMpPuf-0l8f8ZalMVn0xe1XLOS03fLI/edit
                    doc_id = uri.AbsolutePath.Split("/")[3];
                }
                catch (IndexOutOfRangeException) {
                    select_data_source_info.Text = "Invalid Google Sheets URL";
                    return false;
                }

                string gid = queryParams.Get("gid");

                dataSource_info.Text = $"Doc ID:\t{doc_id}\nGID:\t{gid}\nCell:{topLeftCell}";

                // download the big CSV
                string csv_url = $"https://docs.google.com/spreadsheets/d/{doc_id}/export?format=csv&gid={gid}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                string raw_gsheet_path = "./raw_gsheet.csv";
                using (Stream contentStream = await client.GetStreamAsync(csv_url)) {
                    using (FileStream fileStream = new FileStream(raw_gsheet_path, FileMode.Create, FileAccess.Write)) {
                        await contentStream.CopyToAsync(fileStream);
                        select_data_source_info.Text = "Download complete.";
                    }
                }

                string first_row = "name,latd,latm,lats,longd,longm,longs,notes,tags";
                List<string> outputRows = new List<string>();
                outputRows.Add(first_row);
                foreach (var row in Utilities.ExtractDataFromCSV(raw_gsheet_path, tlc)) {
                    outputRows.Add(row);
                }

                File.WriteAllLines(selected_gsheets_csv_data_path, outputRows);


                result = true;
            }
            catch (UriFormatException) {
                select_data_source_info.Text = "ERROR:  Invalid URL";
                return false;
            }
            catch (ArgumentNullException) {
                select_data_source_info.Text = "ERROR:  Null URL";
                return false;
            }

            return result;
        }

        internal static async Task RunBaldrick(Label run_baldrick_info, RadioButton localRadioBtn, RadioButton gsheetRadioBtn) {
            // unzip baldrick
            run_baldrick_info.Text = "Extracting baldrick.zip";
            var temp_dir = "./temp";
            if (!Directory.Exists(temp_dir)) {
                Directory.CreateDirectory(temp_dir);
            }

            ZipFile.ExtractToDirectory(baldrick_path, temp_dir, true);

            // copy csv file
            string routes_path = $"{temp_dir}/routes";
            string target_csv;

            if (localRadioBtn.Checked) {
                target_csv = selected_local_csv_data_path;
            }
            
            else if (gsheetRadioBtn.Checked) {
                target_csv = selected_gsheets_csv_data_path;
            }

            else {
                return;  // should not get here
            }
            
            File.Copy(target_csv, $"{routes_path}/{Path.GetFileName(target_csv)}", true);
            run_baldrick_info.Text = "CSV File Copied";

            // exec baldrick
            var startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = Path.GetFullPath(temp_dir);
            startInfo.FileName = "baldrick.exe";
            string route_arg = Path.GetFileNameWithoutExtension(target_csv);
            startInfo.Arguments = $"{route_arg}";
            startInfo.UseShellExecute = true;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            startInfo.CreateNoWindow = false;

            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            // get results
            string results_path = $"{temp_dir}/{route_arg}/{route_arg}_bundle.zip";
            if (Path.Exists(results_path)) {
                File.Move(results_path, $"./{Path.GetFileName(results_path)}");
            }

            // cleanup
            Directory.Delete(temp_dir, true);
            run_baldrick_info.Text = "Run Complete.";
        }
    }
}
