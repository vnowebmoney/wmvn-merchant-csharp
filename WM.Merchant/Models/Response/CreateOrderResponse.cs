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
    public class CreateOrderResponse
    {
        [JsonProperty("transactionID")]
        [Display(Name = "Transaction ID")]
        public string TransactionID { set; get; }

        [JsonProperty("redirectURL")]
        [Display(Name = "Redirect URL")]
        public string RedirectURL { set; get; }
    }


}
