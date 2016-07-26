
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using WM.Merchant.Helpers;
using WM.Merchant.Base;

namespace WM.Merchant
{
    public class WMResponseHandler<T>
    {

        #region Constants
        /// <summary>
        /// Success code constant
        /// </summary>
        const int SUCCESS_CODE = 0;
        /// <summary>
        /// Merchant validation failed code constant
        /// </summary>
        const int PARTNER_VALIDATION_FAILED = 1001;
        #endregion

        [JsonProperty("errorCode")]
        public int ErrorCode { set; get; }

        [JsonProperty("object")]
        public T Object { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("uiMessage")]
        public string UIMessage { set; get; }

        /// <summary>
        /// WMResponseHandler Constructor with error code and message
        /// Used for creating error response instance
        /// </summary>
        /// <param name="code">Response code</param>
        /// <param name="message">Response message</param>
        public WMResponseHandler(int code, string message)
        {
            ErrorCode = code;
            Message = message;
        }

        /// <summary>
        /// Check if response data has error 
        /// </summary>
        /// <returns></returns>
        public bool IsError()
        {
            return this.ErrorCode != 0;
        }

        /// <summary>
        /// Apply Error code to response handler
        /// </summary>
        /// <param name="errorCode">Error Code</param>
        public void ApplyError(int errorCode)
        {
            this.ErrorCode = errorCode;
            var errorMessage = WMResponseError.GetErrorMessage(errorCode);
            this.Message = errorMessage.Message;
            this.UIMessage = errorMessage.UIMessage;
            this.Object = default(T);
        }

        /// <summary>
        /// Loads response data from json text
        /// </summary>
        /// <param name="responseJson">Response text in json format</param>
        /// <returns>WMResponseHandler instance</returns>
        public static WMResponseHandler<T> Load(string responseJson)
        {
            WMResponseHandler<T> response = JsonConvert.DeserializeObject<WMResponseHandler<T>>(responseJson, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return response;
        }
    }
}
