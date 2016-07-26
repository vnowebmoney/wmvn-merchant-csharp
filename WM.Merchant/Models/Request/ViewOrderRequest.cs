
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
    public class ViewOrderRequest : WMRequestModel
    {
       
        [JsonProperty("mTransactionID")]
        [Display(Name = "Merchant Transaction ID")]
        public string MerchantTransactionID { set; get; }

        /// <summary>
        /// Return unique message to hash checksum
        /// </summary>
        /// <returns>string</returns>
        public override string HashMessage()
        {
            return this.MerchantTransactionID;
        }
    }
}
