
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
    public class WMResponseMessage
    {
        /// <summary>
        /// Detail Message for developer
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Detail Message for user
        /// </summary>
        public string UIMessage { get; set; }
    }
}
