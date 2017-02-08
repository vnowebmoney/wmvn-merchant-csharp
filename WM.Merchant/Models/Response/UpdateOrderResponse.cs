using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WM.Merchant.Base;
using System.ComponentModel.DataAnnotations;

namespace WM.Merchant.Models
{
    public class UpdateOrderResponse
    {
        [JsonProperty("transactionID")]
        [Display(Name = "Transaction ID")]
        public string TransactionID { set; get; }

        [JsonProperty("mTransactionID")]
        public string mTransactionID { set; get; }

        [JsonProperty("invoiceID")]
        [Display(Name = "Invoice ID")]
        public string InvoiceID { set; get; }

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

        [JsonProperty("description")]
        [Display(Name = "Description")]
        public string Description { set; get; }

        [JsonProperty("totalAmount")]
        [Display(Name = "Total Amount")]
        public int TotalAmount { set; get; }



        protected Dictionary<string, string> _AdditionalInfo;

        [JsonProperty("addInfo")]
        [Display(Name = "Additional Information")]
        public Dictionary<string, string> AdditionalInfo {
            get {
                if (_AdditionalInfo == null)
                {
                    _AdditionalInfo = new Dictionary<string, string>();
                }
                return _AdditionalInfo;
            }
            set { _AdditionalInfo = value; }
        }

        [JsonProperty("status")]
        [Display(Name = "Status")]
        public string Status { set; get; }
    }


}
