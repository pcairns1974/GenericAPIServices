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
    public class GenericPutService:IGenericPutService
    {
        public readonly HttpClient _httpClient;
        public GenericPutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<TDTO> Update<TDTO, TId>(TDTO dto, TId id, string endpoint) where TDTO : class
        {
            var jsonPayload = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PutAsync($"{endpoint}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TDTO>(jsonResponse);
                }
                else
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    ErrorModelDTO errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(errorResponseContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException($"{typeof(TDTO).Name} not found");
                    }
                    else
                    {
                        throw new ApiException($"Could not update {typeof(TDTO).Name}: {errorModel?.ErrorMessage}", response.StatusCode);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("JSON serialization/deserialization error: " + jsonEx.Message);
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

        public async Task<TDTO> UpdateManyToMany<TDTO, TId1, TId2>(TDTO dto, TId1 id1, TId2 id2, string endpoint) where TDTO : class
        {
            var jsonPayload = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var url = $"{endpoint}/{Uri.EscapeDataString(id1.ToString())}/{Uri.EscapeDataString(id2.ToString())}";

            try
            {
                var response = await _httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TDTO>(jsonResponse);
                }
                else
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    ErrorModelDTO errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(errorResponseContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException($"{typeof(TDTO).Name} not found");
                    }
                    else
                    {
                        throw new ApiException($"Could not update {typeof(TDTO).Name}: {errorModel?.ErrorMessage}", response.StatusCode);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("JSON serialization/deserialization error: " + jsonEx.Message);
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
