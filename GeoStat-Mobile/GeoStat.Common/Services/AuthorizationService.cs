using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using GeoStat.Common.Models;
using MvvmCross.Logging;
using System.Text;
using System.Net.Http.Headers;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly IMvxLog _log;
        private readonly ICredentialsStorage _storageService;
        private const string AuthUrl = "/api/account/auth/";
        private const string RegisterUrl = "/api/account/register/";

        public AuthorizationService(
            HttpClient client,
            IMvxLog log,
            ICredentialsStorage storageService)
        {
            _httpClient = client;
            _log = log;
            _storageService = storageService;
        }

        public async Task SendRequestForLogin(LoginModel loginModel)
        {
            var url = ConnectionString.BackendUri + AuthUrl;
            var response = await SendPostRequest(url, JsonConvert.SerializeObject(loginModel));

            await ProcessResponse(response);
        }

        public async Task SendRequestForRegister(RegisterModel registerModel)
        {
            var url = ConnectionString.BackendUri + RegisterUrl;
            var response = await SendPostRequest(url, JsonConvert.SerializeObject(registerModel));

            await ProcessResponse(response);
        }

        private async Task ProcessResponse(HttpResponseMessage message)
        {
            var json = await message.Content.ReadAsStringAsync();

            try
            {
                var authModel = JsonConvert.DeserializeObject<AuthModel>(json);
                _storageService.StoreCredentials(authModel);
            }
            catch (JsonSerializationException ex)
            {
                _log.Error(ex, "An error occurred during JSON parsing");
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

            try
            {
                var response = await _httpClient.SendAsync(httpMessage);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "An error occurred during authorization");
            }

            return null;
        }
    }
}
