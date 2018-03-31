using Services.DataService;
using Services.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StorageService
{
    public abstract class StorageAccount : IEntity
    {
        public abstract string Provider { get; }
        public string DisplayName { get; set; }

        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{Provider} - {DisplayName} - {Id}";
        }

        protected abstract IStorageHandler GetStorageHandler();
        public abstract Task AuthorizeAsync();
        public abstract Task<IEnumerable<FileInfo>> GetFilesAsync();
    }
}
