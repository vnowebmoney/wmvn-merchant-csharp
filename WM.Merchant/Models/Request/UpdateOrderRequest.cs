
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WM.Merchant.Base;

namespace WM.Merchant.Models
{
    public class UpdateOrderRequest : WMRequestModel
    {
        [JsonProperty("transactionID")]
        public string TransactionID { set; get; }

        [JsonProperty("status")]
        public string status { get; set; }

        /// <summary>
        /// Return unique message to hash checksum
        /// </summary>
        /// <returns>string</returns>
        public override string HashMessage()
        {
            return this.TransactionID + this.status;
        }
    }
}
