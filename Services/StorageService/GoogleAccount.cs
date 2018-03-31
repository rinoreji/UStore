using LiteDB;
using Services.DataService;
using Services.DataType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.StorageService
{
    public class GoogleAccount : StorageAccount
    {
        private GoogleStorageHandler _storageHandler;
        private const string PROVIDER_IDENTIFIER = "GOOGLE";
        public override string Provider
        {
            get
            {
                return PROVIDER_IDENTIFIER;
            }
        }

        public string AccessToken { get; set; }

        [BsonIgnore]
        public bool IsAutorized
        {
            get
            {
                return GetStorageHandler().IsAuthorized;
            }
        }

        protected override IStorageHandler GetStorageHandler()
        {
            if (_storageHandler == null)
                _storageHandler = new GoogleStorageHandler(this, new UStoreRepository());

            return _storageHandler;
        }

        public override Task AuthorizeAsync()
        {
            return GetStorageHandler().AuthorizeAsync();
        }

        public override Task<IEnumerable<FileInfo>> GetFilesAsync()
        {
            return GetStorageHandler().GetFilesAsync();
        }
    }
}
