
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using WM.Merchant.Helpers;

namespace WM.Merchant.Base
{
    public abstract class WMRequestModel 
    {

        /// <summary>
        /// Checksum string, need to confirm request and response between client and server 
        /// </summary>
        [Display(Name = "Checksum")]
        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        /// <summary>
        /// Return unique message to hash checksum
        /// </summary>
        /// <returns>string</returns>
        public abstract string HashMessage();

        /// <summary>
        /// Hashes checksum value with passcode and secret
        /// </summary>
        /// <param name="passcode">Public passcode which is provided by Webmoney</param>
        /// <param name="secret">Secret key which is provided by Webmoney</param>
        public void HashChecksum(WMService service)
        {
            string message = this.HashMessage() + service.MerchantCode + service.Passcode;
            Checksum = SecurityHelper.HMACHSA1(message, service.SecretKey);
        }
    }
}
