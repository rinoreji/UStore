using Services.DataService;
using Services.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StorageService
{
    public interface IStorageHandler
    {
        bool IsAuthorized { get; }

        Task AuthorizeAsync();
        Task<IEnumerable<FileInfo>> GetFilesAsync();
    }
}
