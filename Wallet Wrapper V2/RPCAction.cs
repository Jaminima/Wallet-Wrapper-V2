using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Wallet_Wrapper_V2
{
    public class RPCAction
    {
        //"{\"jsonrpc\": \"1.0\", \"id\": \"curltest\", \"method\": \"getblockchaininfo\", \"params\": []}"

        #region Properties

        private string paramStr
        {
            get
            {
                string s = "";
                foreach (object o in this._params)
                {
                    string t = "";
                    switch (o.GetType().ToString())
                    {
                        case "string":
                            t = $"'{o.ToString()}'";
                            break;

                        default:
                            t = $"{o.ToString()}";
                            break;
                    }
                    s += t + ",";
                }
                return s.Substring(0, s.Length - 1);
            }
        }

        #endregion Properties

        #region Fields

        public object[] _params = null;
        public string jsonrpc = "1.0", id = "defaultId", method = "getblockchaininfo";

        #endregion Fields

        #region Constructors

        public RPCAction(string _id, string _method, object[] __params)
        {
            this.id = _id;
            this.method = _method;
            this._params = __params;
        }

        #endregion Constructors

        public StringContent asContent
        {
            get
            {
                return new StringContent($"{{'jsonrpc': '{this.jsonrpc}', 'id': '{this.id}', 'method': '{this.method}', 'params': [{this.paramStr}]}}");
            }
        }
    }
}