
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
    public class CreateOrderRequest : WMRequestModel
    {
        public const string GENDER_MALE = "M";
        public const string GENDER_FEMALE = "F";

        [JsonProperty("mTransactionID")]
        [Display(Name = "Merchant Transaction ID")]
        public string MerchantTransactionID { set; get; }

        [JsonProperty("custName")]
        [Display(Name = "Customer Name")]
        public string CustomerName { set; get; }

        [JsonProperty("custAddress")]

        [Display(Name = "Customer Address")]
        public string CustomerAddress { set; get; }

        [JsonProperty("custEmail")]
        [Display(Name = "Customer Email")]
        public string CustomerEmail { set; get; }
        [JsonProperty("custGender")]
        [Display(Name = "Customer Gender")]
        public string CustomerGender { set; get; }

        [JsonProperty("custPhone")]
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { set; get; }

        [JsonProperty("custPurse")]
        [Display(Name = "Customer Purse")]
        public string CustomerPurse { set; get; }

        [JsonProperty("description")]
        [Display(Name = "Description")]
        public string Description { set; get; }

        [JsonProperty("totalAmount")]
        [Display(Name = "Total Amount")]
        public int TotalAmount { set; get; }

        [JsonProperty("cancelURL")]
        [Display(Name = "Cancel URL")]
        public string CancelURL { set; get; }

        [JsonProperty("resultURL")]
        [Display(Name = "Result URL")]
        public string ResultURL { set; get; }

        [JsonProperty("errorURL")]
        [Display(Name = "Error URL")]
        public string ErrorURL { set; get; }

        [JsonProperty("clientIP")]
        [Display(Name = "Client IP")]
        public string ClientIP { set; get; }

        [JsonProperty("userAgent")]
        [Display(Name = "User Agent")]
        public string UserAgent { set; get; }

        [JsonProperty("addInfo")]
        [Display(Name = "Additional Information")]
        public Dictionary<string, string> AdditionalInfo { get; set; }

        /// <summary>
        /// Return unique message to hash checksum
        /// </summary>
        /// <returns>string</returns>
        public override string HashMessage()
        {
            //this.CustomerPurse = "84898451889";
            return this.MerchantTransactionID + this.TotalAmount + this.CustomerName + this.CustomerAddress
                + this.CustomerGender + this.CustomerEmail + this.CustomerPhone + this.CustomerPurse + this.ResultURL
                + this.Description + this.ClientIP + this.UserAgent;
        }
    }
}
