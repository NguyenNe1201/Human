using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Services;
using Google.Apis.Discovery;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;

using System.IO;
using System.Threading;
using System.Web.UI.WebControls;

namespace Human.Common
{
    public class CommonAPI
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";
        public string drive_api()
        {
            string textcontent = null;

            UserCredential credential;
            var CSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resource/Google_Drive_Api");
            using (var stream = new FileStream(Path.Combine(CSPath, "client_secret.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                // Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            // Define the folder ID and file name you want to retrieve       
            string folderId = "1D-v3q-1wlDPPcOKn7R70soVrsVQ91dXT";  // link trỏ tới folder chứa API_KEY.txt trong drive
            string fileName = "API_KEY.txt";  // API_KEY.txt chứa mã key bản quyền, ngày cấp, ngày hết hạn
            // Define parameters of request to list files
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Q = $"'{folderId}' in parents and name = '{fileName}' and mimeType='text/plain'";
            listRequest.Fields = "nextPageToken, files(id, name)";
            // Retrieve the file
            var file = listRequest.Execute().Files.FirstOrDefault();
            if (file != null)
            {
                // Download the content of the text file
                var fileContent = service.Files.Get(file.Id).Execute();
                var stream = new System.IO.MemoryStream();
                service.Files.Get(file.Id).Download(stream);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                using (var reader = new System.IO.StreamReader(stream))
                {
                      string Content = reader.ReadToEnd();
                      textcontent = Content;
                }
            }
            return textcontent;
        }
        public class ContentKeyInfo
        {
            public string KeyText_Api { get; set; }
            public string KeySHA256_Api { get; set; }
            public string DateStartText_Api { get; set; }
            public string DateEndText_Api { get; set; }
        }
        public ContentKeyInfo Get_content_key()
        {
          //  CommonAPI commonAPI = new CommonAPI();
            string content = drive_api();
            string[] part = content.Split('_');
            string key_text = part[0];
            var key_sha256 = CommonCheck.ComputeSHA256Hash(key_text);
            string date_start_text = part[1];
            string date_end_text = part[2];

            ContentKeyInfo info = new ContentKeyInfo
            {
                KeyText_Api = key_text,
                KeySHA256_Api = key_sha256,
                DateStartText_Api = date_start_text,
                DateEndText_Api = date_end_text
            };

            return info;
        }
    }
}