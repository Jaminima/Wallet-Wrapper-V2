using System;
using System.Collections.Generic;
using System.Text;

namespace Wallet_Wrapper_V2
{
    public class RPCDetails
    {
        #region Fields

        public string username = "testUname", password = "testPword", url = "http://localhost:4000/";

        #endregion Fields

        #region Constructors

        public RPCDetails(string _username, string _password, string _url)
        {
            this.username = _username;
            this.password = _password;
            this.url = _url;
        }

        #endregion Constructors

        #region Properties

        public string base64Auth
        {
            get
            {
                return Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")); ;
            }
        }

        #endregion Properties
    }
}