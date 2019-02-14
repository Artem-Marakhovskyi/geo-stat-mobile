using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using GeoStat.Common.Models;
using MvvmCross.Logging;
using System.Text;

namespace GeoStat.Common.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IMvxLog _log;
        private readonly StorageService _storageService;

        public HttpService(
            HttpClient client,
            IMvxLog log,
            StorageService storageService)
        {
            _httpClient = client;
            _log = log;
            _storageService = storageService;
        }

        public async Task SendRequestForLogin(LoginModel loginModel)
        {
            var content = new
            {
                loginModel.Email,
                loginModel.Password
            };

            var url = ConnectionString.BackendUri + "/api/account/authorise/post";
            var response = await SendPostRequest(url, JsonConvert.SerializeObject(content)); //try?

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var authModel = JsonConvert.DeserializeObject<AuthModel>(json);
                _storageService.StoreCredentials(authModel);
            }
            catch (JsonSerializationException ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
            }
        }

        public async Task SendRequestForRegister(RegisterModel registerModel)
        {
            var content = new
            {
                registerModel.Email,
                registerModel.Password,
                registerModel.RepeatedPassword
            };

            var url = ConnectionString.BackendUri + "/api/account/register/post";
            var response = await SendPostRequest(url, JsonConvert.SerializeObject(content));

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var authModel = JsonConvert.DeserializeObject<AuthModel>(json);
                _storageService.StoreCredentials(authModel);
            }
            catch (JsonSerializationException ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
            }
        }

        private async Task<HttpResponseMessage> SendPostRequest(
            string uri,
            string json)
        {
            var httpMessage = new HttpRequestMessage(
                HttpMethod.Post,
                uri)
            {
                Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await _httpClient.SendAsync(httpMessage);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            return null;
        }
    }
}
