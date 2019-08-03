using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CJ.Core.Caching
{
    public class RedisCacheManager:ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public string Get<T>(string key)
        {
            return GetAsync(key).Result;
        }
        public  void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            Set(key, value, null);
        }
        public async void Set<T>(string key, T value,TimeSpan? ts)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await SetAsync(key, value, ts);
        }

        public async void Refresh(string key)
        {
            await RefreshAsync(key);
        }

        public async void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            await RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ts,
            CancellationToken token = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            
            var jsonValue = JsonConvert.SerializeObject(value);

            var option = new DistributedCacheEntryOptions();
            if(ts!=null)
            {
                option.AbsoluteExpirationRelativeToNow = ts;
            }
            byte[] bt = Encoding.UTF8.GetBytes(jsonValue);
            await _distributedCache.SetAsync(key, bt, option,token);
        }

        public async Task<string> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            token.ThrowIfCancellationRequested();
            var  bt= await _distributedCache.GetAsync(key, token);
            return  Encoding.UTF8.GetString(bt);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await _distributedCache.RemoveAsync(key, token);
        }
        public async Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            await _distributedCache.RefreshAsync(key, token);
        }
    }
}