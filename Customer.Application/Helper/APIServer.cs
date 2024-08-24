using Customer.DTOs.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Helper
{
    public class APIServer
    {
        private readonly IConfiguration _configuration;

        public APIServer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CallApiGet(string WebApiUrl, string EndPoint, string? apiKey, Dictionary<string, string> customHeaders = null)   // Call Server API 
        {

            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);


                client.BaseAddress = new Uri(WebApiUrl);

               
                if (apiKey != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);


                var res = await client.GetAsync($"{EndPoint}");

                return await res.Content.ReadAsStringAsync();

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<ResponseModel> CallApiPost(string WebApiUrl, string EndPoint, dynamic obj, string? apiKey, Dictionary<string, string> customHeaders = null)   // Call Server API 
        {
            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var _httpClient = new HttpClient(clientHandler);

                _httpClient.BaseAddress = new Uri(WebApiUrl);


                if (apiKey != null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);


                _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), UnicodeEncoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"api/{EndPoint}", stringContent);

                string responseJson = "";

                responseJson = response.Content.ReadAsStringAsync().Result;
                var customerJsonString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<object>(custome‌​rJsonString);

                return new ResponseModel
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode,
                    Data = deserialized
                };



            }
            catch (Exception ex)
            {
                string errormsg = "PostRequest Exception: " + ex.Message;


                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 403,
                    Message = (ex.Message.Contains("connection failed")) ? "Your IP Address not in a white List" : "some thing went wrong",
                    ExceptionMessage = "Error in call API " + ex.Message
                };
            }



        }
        public async Task<Stream> CallApiPostStreaming(string WebApiUrl, string EndPoint, dynamic obj, string? apiKey, Dictionary<string, string> customHeaders = null)   // Call Server API 
        {
            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var _httpClient = new HttpClient(clientHandler);

                _httpClient.BaseAddress = new Uri(WebApiUrl);


                if (apiKey != null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);


                _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), UnicodeEncoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"api/{EndPoint}", stringContent);


                var responseJson = response.Content.ReadAsStreamAsync().Result;

                return responseJson;

            }
            catch (Exception ex)
            {
                string errormsg = "PostRequest Exception: " + ex.Message;

                throw ex;

            }

        }

    }
}
