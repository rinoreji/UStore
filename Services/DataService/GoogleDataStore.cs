using Google.Apis.Json;
using Google.Apis.Util.Store;
using Services.StorageService;
using System;
using System.Threading.Tasks;

namespace Services.DataService
{
    public class GoogleDataStore : IDataStore
    {
        private static readonly Task CompletedTask = Task.FromResult(0);
        private readonly GoogleAccount _account;
        private readonly IRepository _repository;

        public GoogleDataStore(GoogleAccount account, IRepository repository)
        {
            _account = account;
            _repository = repository;
        }

        public Task ClearAsync()
        {
            _account.AccessToken = string.Empty;
            _repository.Store(_account);

            return CompletedTask;
        }

        public Task DeleteAsync<T>(string key)
        {
            return ClearAsync();
        }

        public Task<T> GetAsync<T>(string key)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            try
            {
                tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(_account.AccessToken));
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            var serialized = NewtonsoftJsonSerializer.Instance.Serialize(value);
            _account.AccessToken = serialized;
            _repository.Store(_account);

            return CompletedTask;
        }
    }
}
