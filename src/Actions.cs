using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BaldrickGUI {
    internal class Actions {
        internal static async Task UpdateBaldrick(Label update_baldrick_info) {
            update_baldrick_info.Text = "";

            string baldrick_path = "./baldrick.zip";
            string repo_url = "https://api.github.com/repos/CalirDeminar/Baldrick/releases/latest";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            
            var release_data_response = await client.GetAsync(repo_url);

            object assets_url;
            if (release_data_response.IsSuccessStatusCode) {
                string jsonString = await release_data_response.Content.ReadAsStringAsync();
                var releaseData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
                releaseData.TryGetValue("assets_url", out assets_url);
            }
            else {
                update_baldrick_info.Text = $"ERROR: HTTP Status Code {release_data_response.StatusCode.ToString()}";
                return;
            }

            var assets_data_response = await client.GetAsync(assets_url.ToString());
            
            if (assets_data_response.IsSuccessStatusCode) {
                string jsonString = await assets_data_response.Content.ReadAsStringAsync();
                var assetsList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonString);

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
            else {
                // @TODO error message dialog box
                throw new NotImplementedException();
            }
        }

        internal static void GoogleSheetsDataSelected() {
            throw new NotImplementedException();
        }

        internal static void LocalDataSelected() {
            throw new NotImplementedException();
        }

        internal static void RunBaldrick() {
            throw new NotImplementedException();
        }
    }
}
