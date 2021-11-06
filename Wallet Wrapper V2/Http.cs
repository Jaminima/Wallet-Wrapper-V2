using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wallet_Wrapper_V2
{
    public class Http
    {
        #region Fields

        private HttpClient httpClient;
        public readonly RPCDetails rpcDetails;

        #endregion Fields

        #region Constructors

        public Http(RPCDetails _rpc)
        {
            rpcDetails = _rpc;
            httpClient = new HttpClient();
        }

        #endregion Constructors

        #region Methods

        public async void Send<T>(RPCAction action, Action<T, object> callback, object state)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), rpcDetails.url))
            {
                request.Headers.TryAddWithoutValidation("Authorization", $"Basic {rpcDetails.base64Auth}");

                request.Content = action.asContent;
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain;");

                try
                {
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    T data = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());

                    callback(data, state);
                }
                catch (ArgumentNullException e)
                {
                }
                catch (InvalidOperationException e)
                {
                }
                catch (HttpRequestException e)
                {
                }
                catch (JsonException e)
                {
                }
            }
        }

        #endregion Methods
    }
}