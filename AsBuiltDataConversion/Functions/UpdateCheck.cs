using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    class UpdateCheck
    {

        public static bool HasInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public static string GetCurrentVersion()
        {
            return typeof(UpdateCheck).Assembly.GetName().Version.ToString();
        }

        public static void CheckForUpdates(Action<string, bool> logFunction)
        {
            if (HasInternetConnection())
            {
                logFunction("Checking for version newer than " + GetCurrentVersion(), true);

                using (var webClient = new WebClient())
                {
                    var mJsonString = webClient.DownloadString("http://myabudhabi.net/latest/version.php?v=" + GetCurrentVersion());
                    try
                    {
                        var mJson = JsonConvert.DeserializeObject<UpdateResponse>(mJsonString);
                        if (mJson.Status == 1)
                        {
                            logFunction("An update is available at the web address http://myabudhabi.net/latest", true);

                            var dlgSaveFile = new SaveFileDialog();
                            dlgSaveFile.Title = "Please select a location to download the file";
                            dlgSaveFile.Filter = "Executable files|*.exe";
                            dlgSaveFile.FileName = mJson.File.FileName;

                            if (MessageBox.Show("Would you like to download the update?", mJson.Message, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (dlgSaveFile.ShowDialog() != DialogResult.OK)
                                {
                                    logFunction("Please select a download location for the update", true);
                                }
                            }

                            logFunction("Downloading, please wait it may take some minutes...", true);
                            webClient.DownloadFile("http://myabudhabi.net/latest/files/" + mJson.File.FileName, dlgSaveFile.FileName);
                            logFunction("Download complete, saved file to: " + dlgSaveFile.FileName, true);
                            logFunction("Please exit the application and execute the setup file to upgrade", true);
                            logFunction("Operation complete", true);

                        }
                        else
                        {
                            logFunction("No update is available", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        logFunction("An error occured while parsing response from update service", false);
                        logFunction(ex.Message, true);
                    }
                }
            }
            else
            {
                logFunction("You do not appear to be connected to the Internet", true);
            }

        }

        public class SetupFile
        {
            public string FileName;
            public string Version;
            public string Newer;
            public string Size;
            public string Date;

            public SetupFile(
                string fileName,
                string version,
                string newer,
                string size,
                string date)
            {
                FileName = fileName;
                Version = version;
                Newer = newer;
                Size = size;
                Date = date;
            }
        }

        public class UpdateResponse
        {
            public int Status;
            public string Message;
            public SetupFile File;

            public UpdateResponse(
                int status,
                string message,
                SetupFile file)
            {
                Status = status;
                Message = message;
                File = file;
            }
        }


    }

}
