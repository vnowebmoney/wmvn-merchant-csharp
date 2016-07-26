using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

using WM.Merchant.Helpers;
using WM.Merchant.Base;
using WM.Merchant.Models;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Configuration;

namespace WM.Merchant
{
    public class WMService
    {
        #region Constants
        

        public const string SUCCESS_STATUS = "WM_SUCCESS";
        public const string FAILED_STATUS = "WM_FAILED";
        public const string WAITING_STATUS = "WM_WAITING";
        public const string CANCELED_STATUS = "WM_CANCELED";
        #endregion

        
        public string APIHOST_URL { get; set; }
        /// <summary>
        /// Merchant service config
        /// </summary>
        private static MerchantConfiguration _config = null;

        /// <summary>
        /// Get Merchant service config
        /// if config is null, loaded from config file
        /// </summary>
        public static MerchantConfiguration Config
        {
            get
            {
                if (_config == null)
                {
                    _config = (MerchantConfiguration)WebConfigurationManager.GetWebApplicationSection("wmMerchant");
                }

                return _config;
            }
        }
        /// <summary>
        /// Production mode (true) or sandbox mode (false)
        /// Default Sandbox mode
        /// </summary>
        public bool ProductionMode { set; get; }

        /// <summary>
        /// If this client using local debug
        /// This property is used by Webmoney developer only, so you don't need caring about this
        /// </summary>
        public bool IsLocalTest { get; set; }

        /// <summary>
        /// Merchant Passcode, or Public key, which is provided by Webmoney
        /// Used for identifying merchant profile, and to encrypt checksum
        /// </summary>
        public string Passcode { get; set; }

        /// <summary>
        /// Merchant Code, which is provided by Webmoney
        /// Used for identifying merchant profile, and to encrypt checksum
        /// </summary>
        public string MerchantCode { get; set; }
        /// <summary>
        /// Secret key, which is provided by Webmoney
        /// Used as key to hash checksum, this key is very important, don't show it to unrelated people
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// WMService constructor
        /// Load data from config
        /// </summary>
        /// <param name="settings"></param>
        public WMService()
        {
            this.APIHOST_URL = ConfigurationManager.AppSettings.Get("ApiHost");
            this.Passcode = Config.Service.Passcode;
            this.SecretKey = Config.Service.SecretKey;
            this.MerchantCode = Config.Service.MerchantCode;
            this.ProductionMode = Config.Service.ProductionMode;
            this.IsLocalTest = Config.Service.IsLocalTest;
        }

        /// <summary>
        /// Validate required Passcode and SecretKey properties
        /// </summary>
        protected void ValidateProperties()
        {
            if (this.Passcode == string.Empty)
            {
                throw new Exception("Webmoney Service Passcode is empty");
            }

            if (this.MerchantCode == string.Empty)
            {
                throw new Exception("Webmoney Service MerchantCode is empty");
            }

            if (this.SecretKey == string.Empty)
            {
                throw new Exception("Webmoney Service Secret Key is empty");
            }
        }

        /// <summary>
        /// Create destination URL for API request
        /// </summary>
        /// <param name="actionName">Action name of API controller</param>
        /// <returns></returns>
        public string CreateURL(string actionName)
        {
            return this.APIHOST_URL + "/" + actionName;
        }

        /// <summary>
        /// Validates checksum of result URL
        /// Compares checksum with the hash of transaction_id + status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>boolean if checksum is valid</returns>
        public string ValidateResultURL(string status)
        {
            this.ValidateProperties();
            var request = HttpContext.Current.Request;

            string transactionID = request.QueryString.Get("transaction_id");
            if (transactionID == string.Empty)
            {
                return "Empty transaction ID";
            }

            string checksum = request.QueryString.Get("checksum");
            if (checksum == string.Empty)
            {
                return "Empty checksum";
            }

            string plainMsg = transactionID + status + MerchantCode + Passcode;
            if (!SecurityHelper.CompareHMACHSA1(checksum, plainMsg, this.SecretKey))
            {
                return "Invalid checksum";
            }

            return "";
        }

        /// <summary>
        /// Validates checksum of success URL
        /// Compares checksum with the hash of transaction_id + "WM_SUCCESS"
        /// </summary>
        /// <returns>boolean if checksum is valid</returns>
        public string ValidateSuccessURL()
        {
            return this.ValidateResultURL(SUCCESS_STATUS);
        }

        /// <summary>
        /// Validates checksum of failed URL
        /// Compares checksum with the hash of transaction_id + "WM_FAILED"
        /// </summary>
        /// <returns>boolean if checksum is valid</returns>
        public string ValidateFailedURL()
        {
            return this.ValidateResultURL(FAILED_STATUS);
        }

        /// <summary>
        /// Validates checksum of Cancel URL
        /// Compares checksum with the hash of transaction_id + "WM_CANCELED"
        /// </summary>
        /// <returns>boolean if checksum is valid</returns>
        public string ValidateCanceledURL()
        {
            return this.ValidateResultURL(CANCELED_STATUS);
        }

        /// <summary>
        /// Create web request instance for send request to API
        /// </summary>
        /// <param name="uri">Destination URI</param>
        /// <returns>WebRequest instance</returns>
        public WebRequest CreateRequest(string uri)
        {
            this.ValidateProperties();

            var request = WebRequest.Create(uri) as HttpWebRequest;
            var httpRequest = HttpContext.Current.Request;

            request.ContentType = "application/json";
            request.Headers.Add("Authorization", this.Passcode);
            request.Headers.Add("X-Forwarded-Host", NetHelper.GetBaseURI(httpRequest.Url));
            request.Headers.Add("X-Forwarded-For", httpRequest.ServerVariables["LOCAL_ADDR"]);
            request.Referer = httpRequest.Url.AbsoluteUri;
            return request;
        }
        
        /// <summary>
        /// Sends Creating Order request to Webmoney merchant API
        /// </summary>
        /// <param name="model">Create Order Request model</param>
        /// <returns>Respones Handler</returns>
        public WMResponseHandler<CreateOrderResponse> CreateOrder(CreateOrderRequest model)
        {
            var httpRequest = HttpContext.Current.Request;
            string url = this.CreateURL("create-order");
            var request = this.CreateRequest(url);

            model.ClientIP = NetHelper.GetClientIPAddress();
            model.UserAgent = httpRequest.UserAgent;
            model.HashChecksum(this);

            string jsonText = JsonConvert.SerializeObject(model);
            string jsonResponse = NetHelper.HttpRequest(request, jsonText);

            var response = WMResponseHandler<CreateOrderResponse>.Load(jsonResponse);

            return response;
        }

        /// <summary>
        /// Sends Creating Order request to Webmoney merchant API
        /// </summary>
        /// <param name="model">Create Order Request model</param>
        /// <returns>Respones Handler</returns>
        public WMResponseHandler<ViewOrderResponse> ViewOrder(ViewOrderRequest model)
        {
            string url = this.CreateURL("view-order");
            var request = this.CreateRequest(url);

            model.HashChecksum(this);

            string jsonText = JsonConvert.SerializeObject(model);
            string jsonResponse = NetHelper.HttpRequest(request, jsonText);

            var response = WMResponseHandler<ViewOrderResponse>.Load(jsonResponse);

            return response;
        }
    }
}
