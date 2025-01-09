using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Share;

public abstract class RestClient
{
    #region Members
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    #endregion

    #region Constructor
    protected RestClient(ILogger logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;

        _jsonSerializerOptions = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
    }
    #endregion

    #region Protected
    public async Task<TResponse> GetAsync<TResponse>(string url, IQueryRequest? request = null, CancellationToken cancellationToken = default) where TResponse : class, IResponse, new()
    {
        TResponse? response = default!;

        try
        {
            //await PrepareTokens();

            var uri = (request is null) ? url : $"{url}{request.ToParameters()}";

            using var responseMessage = await _httpClient.GetAsync(uri, cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonSerializerOptions, cancellationToken);
            }
            else
            {
                response = new TResponse();
                response.AddError(Error(responseMessage));
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "RestService.GetAsync");

            response = new TResponse();
            response.AddError(exception, "Rest Client");
        }

        return response ?? new TResponse();
    }

    public async Task<TResponse> PostAsync<TResponse>(string url, object request, CancellationToken cancellationToken = default) where TResponse : class, IResponse, new()
    {
        TResponse? response;

        try
        {
            //await PrepareTokens();

            using var responseMessage = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonSerializerOptions, cancellationToken);
            }
            else
            {
                response = new TResponse();
                response.AddError(Error(responseMessage));
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "RestService.PostAsync");

            response = new TResponse();
            response.AddError(exception, "Rest Client");
        }

        return response ?? new TResponse();
    }

    public async Task<TResponse> PutAsync<TResponse>(string url, object request, CancellationToken cancellationToken = default) where TResponse : class, IResponse, new()
    {
        TResponse? response;

        try
        {
            //await PrepareTokens();

            using var responseMessage = await _httpClient.PutAsJsonAsync(url, request, cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonSerializerOptions, cancellationToken);
            }
            else
            {
                response = new TResponse();
                response.AddError(Error(responseMessage));
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "RestService.PutAsync");

            response = new TResponse();
            response.AddError(exception, "Rest Client");
        }

        return response ?? new TResponse();
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string url, IQueryRequest? request = null, CancellationToken cancellationToken = default) where TResponse : class, IResponse, new()
    {
        TResponse? response;

        try
        {
            //await PrepareTokens();

            using var responseMessage = await _httpClient.DeleteAsync(request is null ? url : $"{url}{request.ToParameters()}", cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonSerializerOptions, cancellationToken);
            }
            else
            {
                response = new TResponse();
                response.AddError(Error(responseMessage));
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "RestService.PutAsync");

            response = new TResponse();
            response.AddError(exception, "Rest Client");
        }

        return response ?? new TResponse();
    }
    #endregion

    #region Private        
    //private async Task PrepareTokens()
    //{
    //    if (_client.DefaultRequestHeaders.Authorization is null)
    //    {
    //        try
    //        {
    //            var tokenResult = await _accessTokenProvider.RequestAccessToken();
    //            if (tokenResult.TryGetToken(out AccessToken accessToken))
    //            {
    //                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            _logger.LogError(exception, $"{nameof(RestService)}.{nameof(PrepareTokens)}");
    //            throw;
    //        }
    //    }
    //}

    private static Error Error(HttpResponseMessage responseMessage)
    {
        var error = new Error
        {
            Message = responseMessage.ReasonPhrase ?? "Http Response error",
            Context = "Global.HttpException",
            ContextKey = responseMessage.StatusCode.ToString()
        };

        return error;
    }
    #endregion
}