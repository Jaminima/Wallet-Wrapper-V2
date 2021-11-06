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

        public async void Send<T>(RPCAction action, Action<T, object> callback, object state = null)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), rpcDetails.url))
            {
                request.Headers.TryAddWithoutValidation("Authorization", $"Basic {rpcDetails.base64Auth}");

                request.Content = action.asContent;
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");

                HttpResponseMessage response = null;
                try
                {
                    response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    T data = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());

                    callback(data, state);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (HttpRequestException e)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(e.ToString());
                }
                catch (JsonException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        #endregion Methods
    }
}