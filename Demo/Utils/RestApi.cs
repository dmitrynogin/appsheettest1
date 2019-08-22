﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Utils
{
    public abstract class RestApi
    {
        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new[] { new StringEnumConverter() }
        };

        protected RestApi(string baseAddress, HttpMessageHandler handler = null)
        {
            Client = handler == null ? new HttpClient() : new HttpClient(handler);
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> GetAsync<T>(string uri) =>
            await ParseJson<T>(await Client.GetAsync(uri));

        protected async Task<T> PostAsync<T>(string uri, object data = null) =>
            await ParseJson<T>(await Client.PostAsync(uri, JsonContent.From(data)));

        protected HttpClient Client { get; }

        async Task<T> ParseJson<T>(HttpResponseMessage response)
        {
            var s = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(s, Settings);
        }

        class JsonContent : StringContent
        {
            public static JsonContent From(object data) =>
                data == null ? null : new JsonContent(data);

            public JsonContent(object data)
                : base(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
            {
            }
        }
    }
}
