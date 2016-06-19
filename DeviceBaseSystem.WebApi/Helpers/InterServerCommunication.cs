using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace Anatoli.Business.Helpers
{
    public class InterServerCommunication
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        private string InternalUsername = "AnatoliMobileApp";
        private string InternalPassword = "Anatoli@App@Vn";

        private TimeSpan TokenExpireTimeSpan = new TimeSpan();
        private string OwnerKey { get; set; }
        private string DataOwnerKey { get; set; }

        private static InterServerCommunication instance = null;
        private TokenResponse OAuthResult;
        private InterServerCommunication(){
            OwnerKey = "";
            DataOwnerKey = "";
        }
        public static InterServerCommunication Instance
        {
            get
            {
                if (instance == null)
                    instance = new InterServerCommunication();
                return instance;
            }
        }

        public string GetInternalServerToken(string ownerKey, string dataOwnerKey)
        {
            try
            {
                if ((OwnerKey == null || OwnerKey == "") && (ownerKey == null || ownerKey == ""))
                {
                    log.Fatal("Invalid owner in internal communication");
                    throw new Exception("Invalid App Owner");
                }
                else if (ownerKey != null && ownerKey != "" && ownerKey != OwnerKey)
                {
                    OwnerKey = ownerKey;
                    OAuthResult = null;
                }
                
                if ((DataOwnerKey == null || DataOwnerKey == "") && (dataOwnerKey == null || dataOwnerKey == ""))
                {
                    log.Fatal("Invalid owner in internal communication");
                    throw new Exception("Invalid App Owner");
                }
                else if (dataOwnerKey != null && dataOwnerKey != "" && dataOwnerKey != DataOwnerKey)
                {
                    DataOwnerKey = dataOwnerKey;
                    OAuthResult = null;
                }

                if (OAuthResult == null)
                {
                    var client = new HttpClient();
                    var oauthClient = new OAuth2Client(new Uri(ConfigurationManager.AppSettings["InternalServer"] + "/oauth/token"));

                    client.Timeout = TimeSpan.FromMinutes(2);
                    OAuthResult = oauthClient.RequestResourceOwnerPasswordAsync(InternalUsername, InternalPassword, OwnerKey+","+DataOwnerKey).Result;
                    {
                        if (OAuthResult.AccessToken == null || OAuthResult.AccessToken == "")
                        {
                            OAuthResult = null;
                            log.Fatal("Can not login into internal servers");
                            throw new Exception("Internal communication failed");
                        }
                    }
                }

                return OAuthResult.AccessToken;
            }
            catch (Exception ex)
            {
                log.Error("Can not login to internal server", ex);
                throw ex;
            }
        }



    }
}
