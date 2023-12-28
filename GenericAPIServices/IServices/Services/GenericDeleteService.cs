using GenericAPIServices.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices.Services
{
    public class GenericDeleteService:IGenericDeleteService
    {
        public readonly HttpClient _httpClient;
        public GenericDeleteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> DeleteManyToManyRecord<TId1, TId2>(TId1 parentId, TId2 childId, string endpoint)
        {
            try
            {
                var url = $"{endpoint}/{Uri.EscapeDataString(parentId.ToString())}/{Uri.EscapeDataString(childId.ToString())}";
                var response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    ErrorModelDTO errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(errorResponseContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException("Resource not found");
                    }
                    else
                    {
                        throw new ApiException($"Error occurred during deletion: {errorModel?.ErrorMessage}", response.StatusCode);
                    }
                }

                return response;
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("JSON deserialization error: " + jsonEx.Message);
            }
            catch (HttpRequestException httpRequestEx)
            {
                throw new ApiException("HTTP request error: " + httpRequestEx.Message, HttpStatusCode.ServiceUnavailable, httpRequestEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }
        public async Task<HttpResponseMessage> Delete<TId>(TId id, string endpoint)
        {
            try
            {
                var url = $"{endpoint}/{Uri.EscapeDataString(id.ToString())}";
                var response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    ErrorModelDTO errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(errorResponseContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException("Resource not found");
                    }
                    else
                    {
                        throw new ApiException($"Error occurred during deletion: {errorModel?.ErrorMessage}", response.StatusCode);
                    }
                }

                return response;
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("JSON deserialization error: " + jsonEx.Message);
            }
            catch (HttpRequestException httpRequestEx)
            {
                throw new ApiException("HTTP request error: " + httpRequestEx.Message, HttpStatusCode.ServiceUnavailable, httpRequestEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }

    }
}
