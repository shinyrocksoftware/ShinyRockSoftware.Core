using Core.Model.ResponseModels;
using Base.Extension;
using Base.Constant;
using Microsoft.AspNetCore.WebUtilities;

namespace Core.Helper;

public static class RestHelper
{
    #region GET

    public static RestResponseModel<T> Get<T>(string url, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        Action(HttpMethod.Get, url, () => null, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static RestResponseModel<T> Get<T>(string url, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = Get<T>(url, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;

        return responseModel;
    }

    public static async Task<RestResponseModel<T>> GetAsync<T>(string url, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionAsync(HttpMethod.Get, url, () => null, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    #endregion

    #region POST

    #region Dictionary data

    public static void PostFormData(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        Action(HttpMethod.Post, url, () => GetHttpContent(WebConstants.HTTP_CONTENT_TYPE_WWW_FORM, data), requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PostFormData<T>(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        Action(HttpMethod.Post, url, () => GetHttpContent(WebConstants.HTTP_CONTENT_TYPE_WWW_FORM, data), requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static async Task<RestResponseModel<T>> PostFormDataAsync<T>(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionAsync(HttpMethod.Post, url, WebConstants.HTTP_CONTENT_TYPE_WWW_FORM, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static void PostJson(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        Action(HttpMethod.Post, url, () => GetHttpContent(WebConstants.HTTP_CONTENT_TYPE_JSON, data), requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PostJson<T>(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return Post<T>(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PostJson<T>(string url, IDictionary<string, string> data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = PostJson<T>(url, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task PostJsonAsync(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PostAsync(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, cancellationToken, requestParams, requestHeaders);
    }

    public static Task<RestResponseModel<T>> PostJsonAsync<T>(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PostAsync<T>(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, cancellationToken, requestParams, requestHeaders);
    }

    #endregion

    #region Object data

    public static void PostJson(string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        ActionJson(HttpMethod.Post, url, data, requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PostJson<T>(string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        ActionJson(HttpMethod.Post, url, data, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static RestResponseModel<T> PostJson<T>(string url, object data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = PostJson<T>(url, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task PostJsonAsync(string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PostJsonAsync<string>(url, data, cancellationToken, requestParams, requestHeaders);
    }

    public static async Task<RestResponseModel<T>> PostJsonAsync<T>(string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionJsonAsync(HttpMethod.Post, url, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    #endregion

    #endregion

    #region PUT

    #region Dictionary data

    public static void Put(string url, string contentType, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        Action(HttpMethod.Put, url, () => GetHttpContent(contentType, data), requestParams, requestHeaders);
    }

    public static RestResponseModel<T> Put<T>(string url, string contentType, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        Action(HttpMethod.Put, url, () => GetHttpContent(contentType, data), requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static RestResponseModel<T> Put<T>(string url, string contentType, IDictionary<string, string> data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = Put<T>(url, contentType, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task PutAsync(string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PutAsync<string>(url, contentType, data, cancellationToken, requestParams, requestHeaders);
    }

    public static async Task<RestResponseModel<T>> PutAsync<T>(string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionAsync(HttpMethod.Put, url, contentType, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static void PutJson(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        Action(HttpMethod.Put, url, () => GetHttpContent(WebConstants.HTTP_CONTENT_TYPE_JSON, data), requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PutJson<T>(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return Put<T>(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PutJson<T>(string url, IDictionary<string, string> data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = PutJson<T>(url, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task PutJsonAsync(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PutAsync(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, cancellationToken, requestParams, requestHeaders);
    }

    public static Task<RestResponseModel<T>> PutJsonAsync<T>(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PutAsync<T>(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, cancellationToken, requestParams, requestHeaders);
    }

    #endregion

    #region Object data

    public static void PutJson(string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        ActionJson(HttpMethod.Put, url, data, requestParams, requestHeaders);
    }

    public static RestResponseModel<T> PutJson<T>(string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        ActionJson(HttpMethod.Put, url, data, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static RestResponseModel<T> PutJson<T>(string url, object data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = PutJson<T>(url, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task PutJsonAsync(string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PutJsonAsync<string>(url, data, cancellationToken, requestParams, requestHeaders);
    }

    public static async Task<RestResponseModel<T>> PutJsonAsync<T>(string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionJsonAsync(HttpMethod.Put, url, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    #endregion

    #endregion

    #region DELETE

    #region Dictionary data

    public static void Delete(string url, string contentType, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        Action(HttpMethod.Delete, url, () => GetHttpContent(contentType, data), requestParams, requestHeaders);
    }

    public static RestResponseModel<T> Delete<T>(string url, string contentType, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        Action(HttpMethod.Delete, url, () => GetHttpContent(contentType, data), requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static RestResponseModel<T> Delete<T>(string url, string contentType, IDictionary<string, string> data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = Delete<T>(url, contentType, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task DeleteAsync(string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return DeleteAsync<string>(url, contentType, data, cancellationToken, requestParams, requestHeaders);
    }

    public static async Task<RestResponseModel<T>> DeleteAsync<T>(string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionAsync(HttpMethod.Delete, url, contentType, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static void DeleteJson(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        Action(HttpMethod.Delete, url, () => GetHttpContent(WebConstants.HTTP_CONTENT_TYPE_JSON, data), requestParams, requestHeaders);
    }

    public static RestResponseModel<T> DeleteJson<T>(string url, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return Delete<T>(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, requestParams, requestHeaders);
    }

    public static RestResponseModel<T> DeleteJson<T>(string url, IDictionary<string, string> data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = DeleteJson<T>(url, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task DeleteJsonAsync(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return DeleteAsync(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, cancellationToken, requestParams, requestHeaders);
    }

    public static Task<RestResponseModel<T>> DeleteJsonAsync<T>(string url, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return DeleteAsync<T>(url, WebConstants.HTTP_CONTENT_TYPE_JSON, data, cancellationToken, requestParams, requestHeaders);
    }

    #endregion

    #region Object data

    public static void DeleteJson(string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        ActionJson(HttpMethod.Delete, url, data, requestParams, requestHeaders);
    }

    public static RestResponseModel<T> DeleteJson<T>(string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        ActionJson(HttpMethod.Delete, url, data, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    public static RestResponseModel<T> DeleteJson<T>(string url, object data, out IDictionary<string, string> responseHeaders, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var responseModel = DeleteJson<T>(url, data, requestParams, requestHeaders);
        responseHeaders = responseModel.Headers;
        return responseModel;
    }

    public static Task DeleteJsonAsync(string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return DeleteJsonAsync<string>(url, data, cancellationToken, requestParams, requestHeaders);
    }

    public static async Task<RestResponseModel<T>> DeleteJsonAsync<T>(string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionJsonAsync(HttpMethod.Delete, url, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    #endregion

    #endregion

    #region Private

    private static void ActionJson(HttpMethod method, string url, object data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        ActionJson(method, url, () => Serialize(data), requestParams, requestHeaders, action);
    }

    private static void ActionJson(HttpMethod method, string url, Func<string> acquireDataStringFunc, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        Action(method, url, () => new StringContent(acquireDataStringFunc(), Encoding.UTF8, WebConstants.HTTP_CONTENT_TYPE_JSON), requestParams, requestHeaders, action);
    }

    private static void Action(HttpMethod method, string url, Func<HttpContent> acquireHttpContentFunc, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        using var httpClient = new HttpClient();
        using var content = acquireHttpContentFunc();

        var urlWithParams = requestParams == null
            ? url
            : QueryHelpers.AddQueryString(url, requestParams.ToDictionary(c => c.Key, c => c.Value.ToString()));

        if (requestHeaders != null)
        {
            foreach (var (key, value) in requestHeaders)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
            }
        }

        HttpResponseMessage httpResponseMessage;

        if (method == HttpMethod.Get)
        {
            httpResponseMessage = httpClient.GetAsync(urlWithParams, CancellationToken.None).Result;
        }
        else if (method == HttpMethod.Post)
        {
            httpResponseMessage = httpClient.PostAsync(urlWithParams, content, CancellationToken.None).Result;
        }
        else if (method == HttpMethod.Put)
        {
            httpResponseMessage = httpClient.PutAsync(urlWithParams, content, CancellationToken.None).Result;
        }
        else if (method == HttpMethod.Delete)
        {
            httpResponseMessage = httpClient.DeleteAsync(urlWithParams, CancellationToken.None).Result;
        }
        else
        {
            httpResponseMessage = null;
        }

        if (httpResponseMessage == null)
        {
            throw new("Unknown HTTP method");
        }

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var contentString = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var responseHeaders = httpResponseMessage.Headers.Concat(httpResponseMessage.Content.Headers);
            var headers = responseHeaders.ToDictionary(header => header.Key, header => header.Value.FirstOrDefault());

            action?.Invoke(httpResponseMessage, contentString, headers);
        }
        else
        {
            action?.Invoke(httpResponseMessage, null, null);
        }
    }

    private static async Task ActionAsync(HttpMethod method, string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        await ActionAsync(method, url, () => GetHttpContent(contentType, data), cancellationToken, requestParams, requestHeaders, action);
    }

    private static async Task ActionJsonAsync(HttpMethod method, string url, object data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        await ActionJsonAsync(method, url, () => Serialize(data), cancellationToken, requestParams, requestHeaders, action);
    }

    private static async Task ActionJsonAsync(HttpMethod method, string url, Func<string> acquireDataStringFunc, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        await ActionAsync(method, url, () => new StringContent(acquireDataStringFunc(), Encoding.UTF8, WebConstants.HTTP_CONTENT_TYPE_JSON), cancellationToken, requestParams, requestHeaders, action);
    }

    private static async Task ActionAsync(HttpMethod method, string url, Func<HttpContent> acquireHttpContentFunc, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null, Action<HttpResponseMessage, string, IDictionary<string, string>>? action = null)
    {
        using var httpClient = new HttpClient();
        using var content = acquireHttpContentFunc();

        var urlWithParams = requestParams == null
            ? url
            : QueryHelpers.AddQueryString(url, requestParams.ToDictionary(c => c.Key, c => c.Value.ToString()));

        if (requestHeaders != null)
        {
            foreach (var (key, value) in requestHeaders)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
            }
        }

        HttpResponseMessage httpResponseMessage;

        if (method == HttpMethod.Get)
        {
            httpResponseMessage = await httpClient.GetAsync(urlWithParams, cancellationToken);
        }
        else if (method == HttpMethod.Post)
        {
            httpResponseMessage = await httpClient.PostAsync(urlWithParams, content, cancellationToken);
        }
        else if (method == HttpMethod.Put)
        {
            httpResponseMessage = await httpClient.PutAsync(urlWithParams, content, cancellationToken);
        }
        else if (method == HttpMethod.Delete)
        {
            httpResponseMessage = await httpClient.DeleteAsync(urlWithParams, cancellationToken);
        }
        else
        {
            httpResponseMessage = null;
        }

        if (httpResponseMessage == null)
        {
            throw new("Unknown HTTP method");
        }

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var contentString = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
            var responseHeaders = httpResponseMessage.Headers.Concat(httpResponseMessage.Content.Headers);
            var headers = responseHeaders.ToDictionary(header => header.Key, header => header.Value.FirstOrDefault());

            action?.Invoke(httpResponseMessage, contentString, headers);
        }
        else
        {
            action?.Invoke(httpResponseMessage, null, null);
        }
    }

    private static RestResponseModel<T> Post<T>(string url, string contentType, IDictionary<string, string> data, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        Action(HttpMethod.Post, url, () => GetHttpContent(contentType, data), requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    private static Task PostAsync(string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        return PostAsync<string>(url, contentType, data, cancellationToken, requestParams, requestHeaders);
    }

    private static async Task<RestResponseModel<T>> PostAsync<T>(string url, string contentType, IDictionary<string, string> data, CancellationToken cancellationToken, IDictionary<string, object>? requestParams = null, IDictionary<string, string>? requestHeaders = null)
    {
        var result = default(RestResponseModel<T>);

        await ActionAsync(HttpMethod.Post, url, contentType, data, cancellationToken, requestParams, requestHeaders, (httpResponseMessage, contentString, responseHeaders) =>
        {
            result = GetModel<T>(httpResponseMessage, contentString, responseHeaders);
        });

        return result;
    }

    private static HttpContent GetHttpContent(string contentType, IDictionary<string, string> data)
    {
        HttpContent content = null;

        switch (contentType)
        {
            case WebConstants.HTTP_CONTENT_TYPE_WWW_FORM:
                content = new FormUrlEncodedContent(data);
                content.Headers.Clear();
                content.Headers.Add("Content-Type", contentType);
                break;
            case WebConstants.HTTP_CONTENT_TYPE_JSON:
                content = new StringContent(data == null ? string.Empty : data.ToJsonString(), Encoding.UTF8, contentType);
                break;
        }

        return content;
    }

    private static RestResponseModel<T> GetModel<T>(HttpResponseMessage httpResponseMessage, string contentString, IDictionary<string, string> responseHeaders)
    {
        return new()
        {
            Success = httpResponseMessage.IsSuccessStatusCode
            , Data = contentString == null ? default : Deserialize<T>(contentString)
            , Headers = responseHeaders
            , ReasonPhase = httpResponseMessage.ReasonPhrase
        };
    }

    private static string Serialize<T>(T data)
    {
        return typeof(T).IsValueType || typeof(T) == typeof(string)
            ? data.ToString()
            : data.Serialize();
    }

    private static T Deserialize<T>(string data)
    {
        return typeof(T).IsValueType || typeof(T) == typeof(string)
            ? (T)(object)data
            : data.Deserialize<T>();
    }

    #endregion
}