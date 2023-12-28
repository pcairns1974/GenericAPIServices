using GenericAPIServices.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices.Services
{
    public class GenericPostService:IGenericPostServices
    {
        public readonly HttpClient _httpClient;
        public GenericPostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Add<T>(T obj, string endpoint) where T : class
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdObject = JsonConvert.DeserializeObject<T>(responseContent);
                    if (createdObject != null)
                    {
                        return createdObject;
                    }
                    else
                    {
                        throw new InvalidOperationException("The response was successful but did not contain any content.");
                    }
                }
                else
                {
                    ErrorModelDTO errorModel = null;
                    var errorResponseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(errorResponseContent);
                    }
                    catch (JsonException jsonEx)
                    {
                        throw new ApiException("Error during response processing.", response.StatusCode, jsonEx);
                    }

                    var errorMessage = errorModel?.ErrorMessage ?? "An error occurred, but no additional error information is available.";
                    throw new ApiException(errorMessage, response.StatusCode);
                }
            }
            catch (HttpRequestException httpRequestEx)
            {
                throw new ApiException("HTTP request error: " + httpRequestEx.Message, HttpStatusCode.ServiceUnavailable, httpRequestEx);
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("JSON serialization/deserialization error: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }

    }
}
