using System;
using Wallet_Wrapper_V2;

namespace Example
{
    internal class Program
    {
        #region Methods

        private static async void blockChainInfoToConsole(object data, object state)
        {
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var http = new Http(new Login());

            var a = new RPCAction("test", "getblockchaininfo");

            http.Send<object>(a, blockChainInfoToConsole);

            Console.ReadLine();
        }

        #endregion Methods
    }
}