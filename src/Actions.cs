using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace BaldrickGUI {
    internal class Actions {
        internal static async Task UpdateBaldrick(Label update_baldrick_info) {
            update_baldrick_info.Text = "";

            string baldrick_path = "./baldrick.zip";
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
                object temp;
                releaseData.TryGetValue("assets", out temp);
                
                foreach (var element in ((JsonElement)temp).EnumerateArray()) {
                    Dictionary<string, object> item = JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText());
                    assetsList.Add(item);
                }
            }

            foreach (var assetData in assetsList) {
                bool initiate_download = false;

                object asset_filename;
                object asset_url;
                object asset_digest;

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

            select_data_source_info.Text = "ERROR:  Not implemented";

            return result;
        }

        internal static async Task<bool> GoogleSheetsDataSelected(Label dataSource_info, Label select_data_source_info) {
            bool result = false;

            string url_string = Microsoft.VisualBasic.Interaction.InputBox("Enter a PUBLIC Google Sheets URL", "Data Entry");
            
            // @TODO input validation
            string topLeftCell = Microsoft.VisualBasic.Interaction.InputBox("Enter the TOP LEFT CELL", "Data Entry");
            // @TODO parse TLC to 0 indexed Tuple<int,int> (row,column)

            try {
                Uri uri = new Uri(url_string);
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
                catch (IndexOutOfRangeException e) {
                    select_data_source_info.Text = "Invalid Google Sheets URL";
                    return false;
                }

                string gid = queryParams.Get("gid");

                dataSource_info.Text = $"DID:\t{doc_id}\nGID:\t{gid}\nTLC:{topLeftCell}";

                // download the big CSV
                string csv_url = $"https://docs.google.com/spreadsheets/d/{doc_id}/export?format=csv&gid={gid}";
                
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                using (Stream contentStream = await client.GetStreamAsync(csv_url)) {
                    using (FileStream fileStream = new FileStream("./raw_gsheet.csv", FileMode.Create, FileAccess.Write)) {
                        await contentStream.CopyToAsync(fileStream);
                        select_data_source_info.Text = "Download complete.";
                    }
                }

                // @TODO refactor so local and gsheet use same parsing function on csv
                List<string> outputRows = new List<string>();
                using (var reader = new StreamReader("./raw_gsheet.csv")) {
                    var raw_data = reader.ReadToEnd();
                    raw_data = raw_data.Replace("\r\n", "\n");  // remove CRLF nonsense
                    // @TODO use converted top left cell data for skips
                    var rows = raw_data.Split("\n").Skip(1);

                    foreach (var row in rows) {
                        // @TODO refactor out magic numbers
                        // @TODO use converted top left cell data for skips
                        var raw_row = row.Split(",").Skip(5).ToList().GetRange(0, 9);
                        var data = String.Join(",", raw_row).Replace("\"", "");
                        if (data.Contains("#VALUE!")) {
                            break;  // End of useful data,   Note:  Tailored to Castor's GSheet
                        }
                        outputRows.Add(data);
                    }
                }

                File.WriteAllLines("./waypoints.csv", outputRows);

                result = true;
            }
            catch (UriFormatException e) {
                select_data_source_info.Text = "ERROR:  Invalid URL";
                return false;
            }
            catch (ArgumentNullException e) {
                select_data_source_info.Text = "ERROR:  Null URL";
                return false;
            }

            return result;
        }

        internal static void RunBaldrick() {
            // unzip baldrick
            // exec baldrick
            // package results
            // cleanup
        }
    }
}
