using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Services.DataService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.StorageService
{
    //https://www.googleapis.com/drive/v3/files
    public class GoogleStorageHandler : IStorageHandler
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        readonly string[] Scopes = { DriveService.Scope.Drive };
        const string ApplicationName = "UStore";
        private DriveService _service;
        private GoogleAccount _account;
        private IRepository _repository;

        public GoogleStorageHandler(GoogleAccount account, IRepository repository)
        {
            _account = account; _repository = repository;
        }

        public bool IsAuthorized
        {
            get
            {
                return _service != null;
            }
        }

        public Task AuthorizeAsync()
        {
            try
            {
                var path = @"client_secret.json";
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new GoogleDataStore(_account, _repository)).Result;

                        // Create Drive API service.
                        _service = new DriveService(new BaseClientService.Initializer()
                            {
                                HttpClientInitializer = credential,
                                ApplicationName = ApplicationName,
                            });
                    logger.Debug(_service.About);
                }
            }
            catch (Exception exp)
            {
                logger.Error(exp.Message);
                throw;
            }

            return Task.FromResult(0);
        }

        public Task<IEnumerable<DataType.FileInfo>> GetFilesAsync()
        {
            var tcs = new TaskCompletionSource<IEnumerable<DataType.FileInfo>>();
            try
            {
                FilesResource.ListRequest listRequest = _service.Files.List();
                listRequest.PageSize = 10;
                listRequest.Fields = "nextPageToken, files(id, name)";

                // List files.
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
                var fileInfos = new List<DataType.FileInfo>();

                fileInfos.AddRange(files.Select(f => new DataType.FileInfo() { FileName = f.Name, FileId = f.Id }));
                tcs.SetResult(fileInfos);
            }
            catch (Exception exp)
            {
                logger.Error(exp.Message);
                tcs.SetException(exp);
            }

            return tcs.Task;
        }
    }
}
