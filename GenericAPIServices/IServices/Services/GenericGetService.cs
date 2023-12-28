using GenericAPIServices.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices.Services
{
    public class GenericGetService : IGenericGetService
    {
        public readonly HttpClient _httpClient;
        public GenericGetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //SAMPLES CALL FOR GET
        //var itemId = 123; 
        //var endpoint = "https://api.example.com/items";
        //var item = await Get<Item>(endpoint, itemId);

        //--------------SAMPLE 2 FOR GET---------------
        //var typeId = 456; // Example type ID
        //var itemId = "ABC123"; // Example item ID
        //var endpoint = "https://api.example.com/products";
        //var product = await Get<Product>(endpoint, typeId, itemId);

        //SAMPLE 3
        //var userId = 123; // Example user ID (int)
        //var orgId = Guid.NewGuid(); // Example organization ID (Guid)
        //var endpoint = "https://api.example.com/users";
        //var user = await Get<User>(endpoint, userId, orgId);
        public async Task<T> Get<T>(string endpoint, params object[] ids) where T : class
        {
            try
            {
                string url = $"{endpoint}/{string.Join("/", ids.Select(Uri.EscapeDataString))}";
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(content);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        throw new InvalidOperationException("The response was successful but did not contain any content.");
                    }
                }
                else
                {
                    ErrorModelDTO errorModel = null;
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        throw new ApiException("Error during response processing.", response.StatusCode, jsonEx);
                    }

                    if (errorModel == null)
                    {
                        throw new ApiException("An error occurred, but no additional error information is available.", response.StatusCode);
                    }

                    throw new ApiException(errorModel.ErrorMessage, response.StatusCode);
                }
            }
            catch (HttpRequestException httpRequestEx)
            {
                throw new ApiException("HTTP request error: " + httpRequestEx.Message, HttpStatusCode.ServiceUnavailable, httpRequestEx);
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("JSON deserialization error: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }
        public async Task<IEnumerable<T>> GetAll<T>(string endpoint) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        // Handle null result from deserialization
                        throw new InvalidOperationException("The response was successful but did not contain any content.");
                    }
                }
                else
                {
                    // Deserialize error response into a model, if possible
                    ErrorModelDTO errorModel = null;
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        try
                        {
                            errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                        }
                        catch (JsonException jsonEx)
                        {
                            // Log the JSON deserialization error
                            // ... logging code here ...
                            throw new ApiException("Error during response processing.", response.StatusCode, jsonEx);
                        }
                    }

                    var errorMessage = errorModel?.ErrorMessage ?? "An error occurred, but no additional error information is available.";
                    throw new ApiException(errorMessage, response.StatusCode);
                }
            }
            catch (HttpRequestException httpRequestEx)
            {
                // Handle HTTP request exceptions
                // ... logging code here ...
                throw new ApiException("HTTP request error: " + httpRequestEx.Message, HttpStatusCode.ServiceUnavailable, httpRequestEx);
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON deserialization exceptions
                // ... logging code here ...
                throw new InvalidOperationException("JSON deserialization error: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // ... logging code here ...
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }

        //SAMPLES FOR GET ALL BY IDs
        //var categoryId = 10; // Example category ID
        //var endpoint = "https://api.example.com/products";
        //var products = await GetAllByIds<Product>(endpoint, categoryId);

        //SAMPLE 2
        //var categoryId = 20; // Example category ID
        //var supplierId = 5;  // Example supplier ID
        //var endpoint = "https://api.example.com/items";
        //var items = await GetAllByIds<Item>(endpoint, categoryId, supplierId);

        //SAMPLE 3
        //var customerId = 123;        // Example customer ID (int)
        //var orderDate = "2023-12-01"; // Example order date (string)
        //var endpoint = "https://api.example.com/orders";
        //var orders = await GetAllByIds<Order>(endpoint, customerId, orderDate);
        public async Task<IEnumerable<T>> GetAllByIds<T>(string endpoint, params object[] ids) where T : class
        {
            try
            {
                string url = $"{endpoint}/{string.Join("/", ids.Select(Uri.EscapeDataString))}";
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        throw new InvalidOperationException("The response was successful but did not contain any content.");
                    }
                }
                else
                {
                    ErrorModelDTO errorModel = null;
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        try
                        {
                            errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                        }
                        catch (JsonException jsonEx)
                        {
                            throw new ApiException("Error during response processing.", response.StatusCode, jsonEx);
                        }
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
                throw new InvalidOperationException("JSON deserialization error: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }
        public async Task<int> GetCount<T>(string endpoint, Expression<Func<T, bool>> filter) where T : class
        {
            var queryParameters = new Dictionary<string, string>
    {
        { "filter", filter.ToString() }
    };

            var queryString = string.Join("&", queryParameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));

            try
            {
                var response = await _httpClient.GetAsync($"{endpoint}?{queryString}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var count = JsonConvert.DeserializeObject<int>(content);
                        return count;
                    }
                    catch (JsonException jsonEx)
                    {
                        // Handle JSON deserialization exception
                        throw new InvalidOperationException("JSON deserialization error: " + jsonEx.Message);
                    }
                }
                else
                {
                    ErrorModelDTO errorModel = null;
                    try
                    {
                        errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                    }
                    catch (JsonException jsonEx)
                    {
                        // Log the JSON deserialization error
                        // ... logging code here ...
                        throw new ApiException("Error during response processing.", response.StatusCode, jsonEx);
                    }

                    if (errorModel == null)
                    {
                        // Handle case where errorModel is null after deserialization attempt
                        throw new ApiException("An error occurred, but no additional error information is available.", response.StatusCode);
                    }

                    throw new ApiException(errorModel.ErrorMessage, response.StatusCode);
                }
            }
            catch (HttpRequestException httpRequestEx)
            {
                // Handle HTTP request exceptions
                // ... logging code here ...
                throw new ApiException("HTTP request error: " + httpRequestEx.Message, HttpStatusCode.ServiceUnavailable, httpRequestEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // ... logging code here ...
                throw new ApplicationException("Unexpected error: " + ex.Message, ex);
            }
        }

    }
}
