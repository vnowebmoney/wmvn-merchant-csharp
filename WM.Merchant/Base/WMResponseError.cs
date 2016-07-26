
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
    public class WMResponseError
    {

        #region Constants
        /// <summary>
        /// Success code constant
        /// </summary>
        public const int SUCCESS_CODE = 0;

        /// <summary>
        /// Server busy error code
        /// </summary>
        const int ERROR_SERVER_BUSY = 1000;

        /// <summary>
        /// Merchant validation failed code constant
        /// </summary>
        public const int PARTNER_VALIDATION_FAILED = 1001;
        public const int INVALID_PARAM = 1002;
        public const int POLICY_VIOLATION = 1003;
        public const int DUPLICATE_TRANSACTION = 1004;
        public const int TRANSACTION_LIMIT = 1005;
        public const int INVALID_PAYMENT_INFO = 1006;
        public const int UNAUTHENTICATED = 1007;
        public const int NOT_FOUND = 1008;
        public const int INVALID_CHECKSUM = 1009;
        public const string UNKNOWN_ERROR_MESSAGE = "Unknown error";
        #endregion

        /// <summary>
        /// Error code, message dictionary
        /// </summary>
        protected static Dictionary<int, WMResponseMessage> _errorCodes = null;

        /// <summary>
        /// Error code, message dictionary getter
        /// </summary>
        public static Dictionary<int, WMResponseMessage> ErrorCodes
        {
            get {
                if (_errorCodes == null)
                {
                    _errorCodes = getErrorCodes();
                }
                return _errorCodes;
            }
        }

        /// <summary>
        /// Return Error code, message dictionary
        /// </summary>
        protected static Dictionary<int, WMResponseMessage> getErrorCodes()
        {
            return new Dictionary<int, WMResponseMessage>()
            {
                { SUCCESS_CODE,
                    new WMResponseMessage() { Message = "Success" } },
                { ERROR_SERVER_BUSY,
                    new WMResponseMessage() { Message = "Server Busy" } },
                { PARTNER_VALIDATION_FAILED,
                    new WMResponseMessage() { Message = "Partner validation failed" } },
                { INVALID_PARAM,
                    new WMResponseMessage() { Message = "Invalid request parameter" } },
                { POLICY_VIOLATION,
                    new WMResponseMessage() { Message = "Policy Violation" } },
                { DUPLICATE_TRANSACTION,
                    new WMResponseMessage() { Message = "Duplicate transaction" } },
                { TRANSACTION_LIMIT,
                    new WMResponseMessage() { Message = "Transaction is out of limit" } },
                { INVALID_PAYMENT_INFO,
                    new WMResponseMessage() { Message = "Invalid payment information" } },
                { UNAUTHENTICATED,
                    new WMResponseMessage() { Message = "You have no permission to access webmoney merchant api server" } },
                { NOT_FOUND,
                    new WMResponseMessage() { Message = "Not found" } },
                { INVALID_CHECKSUM,
                    new WMResponseMessage() { Message = "Invalid checksum", UIMessage = "The response data is not requested from reliable source URL" } }
            };
        }

        /// <summary>
        /// Get Error Message from error code
        /// </summary>
        /// <param name="errorCode">Error Code</param>
        /// <returns>WM Response Message</returns>
        public static WMResponseMessage GetErrorMessage(int errorCode)
        {
            if (ErrorCodes.ContainsKey(errorCode))
            {
                return ErrorCodes[errorCode];
            }

            return new WMResponseMessage()
            {
                Message = UNKNOWN_ERROR_MESSAGE
            };
        }
    }
}
