using System;
using Wallet_Wrapper_V2;
using System.Text.Json;

namespace Example
{
    internal class Program
    {
        #region Methods

        private static async void blockChainInfoToConsole(JsonElement data, object state)
        {
            decimal loaded = data.GetProperty("result").GetProperty("verificationprogress").GetDecimal();
            loaded = Math.Round(loaded, 2) * 100;

            long blockCount = data.GetProperty("result").GetProperty("blocks").GetInt64();

            Console.WriteLine($"{loaded}% Loaded of {blockCount} Blocks");
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var http = new Http(new Login());

            var a = new RPCAction("test", "getblockchaininfo");

            http.Send(a, blockChainInfoToConsole);

            Console.ReadLine();
        }

        #endregion Methods
    }
}