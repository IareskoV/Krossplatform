using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Lab6.Entities;
using Newtonsoft.Json;

namespace Lab6
{
    public class DatabaseHandler<T>
    {
        private readonly HttpClient httpClient;
        private string host = "http://localhost:4000/api/";

        public DatabaseHandler(HttpClient client, Tables table)
        {
            httpClient = client;
            host += table.ToString().ToLower() + "/";
        }

        public async Task<T> Create(T entity)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, host)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8)
            };
            message.Content.Headers.Remove("Content-Type");
            message.Content.Headers.Add("Content-Type", "application/json");
            var res= await httpClient.SendAsync(message);
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());

        }

        public async Task<T> Update(T entity)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, host)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8)
            };
            message.Content.Headers.Remove("Content-Type");
            message.Content.Headers.Add("Content-Type", "application/json");
            var res = await httpClient.SendAsync(message);
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());

        }

        public async Task<bool> Delete(object id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, host + id)
            {
            };
            await httpClient.SendAsync(message);
            return true;
        }
        public async Task<T> Get(object id)
        {
            return JsonConvert.DeserializeObject<T>(await httpClient.GetStringAsync(host + id));
        }
        public async Task<List<T>> GetList()
        {
            return JsonConvert.DeserializeObject<List<T>>(await httpClient.GetStringAsync(host));
        }

    }

    public enum Tables
    {
        Assets = 0,
        AssetsLifeCycleEvents = 1,
        LifeCyclePhases = 2,
        Locations = 3,
        RefAssetCategories = 4,
        RefAssetSuperTypes = 5,
        RefAssetTypes = 6,
        RefSizes = 7,
        RefStatuses = 8,
        ResponsibleParties = 9


    }
}
