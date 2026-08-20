using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services
{
    public interface ILocalStorageService 
    {
        Task SetValue<T>(string key, T value);
        Task<T> GetValue<T>(string key);
    }


    public class LocalStorageService: ILocalStorageService
    {
        public async Task SetValue<T>(string key, T value)
        {
            if (value == null)
            {
                SecureStorage.Default.Remove(key);
            }
            else
            {
                await SecureStorage.Default.SetAsync(key, JsonConvert.SerializeObject(value));
            }
        }
        public async Task<T> GetValue<T>(string key)
        {
            string value = await SecureStorage.Default.GetAsync(key);
            if (value == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

    }
}
