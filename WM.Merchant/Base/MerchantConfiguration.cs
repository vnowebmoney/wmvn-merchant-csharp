using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Merchant
{
    public sealed class MerchantConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("wmService")]
        public ServiceElement Service
        {
            get
            {
                return (ServiceElement)this["wmService"];
            }
            set
            {
                this["wmService"] = value;
            }
        }

        public class ServiceElement : ConfigurationElement
        {
            [ConfigurationProperty("passcode", IsRequired = true)]
            public String Passcode
            {
                get
                {
                    return (String)this["passcode"];
                }
                set
                {
                    this["passcode"] = value;
                }
            }

            [ConfigurationProperty("merchantCode", IsRequired = true)]
            public String MerchantCode
            {
                get
                {
                    return (String)this["merchantCode"];
                }
                set
                {
                    this["merchantCode"] = value;
                }
            }

            [ConfigurationProperty("secretKey", IsRequired = true)]
            public String SecretKey
            {
                get
                {
                    return (String)this["secretKey"];
                }
                set
                {
                    this["secretKey"] = value;
                }
            }

            [ConfigurationProperty("productionMode", IsRequired = false, DefaultValue = false)]
            public Boolean ProductionMode
            {
                get
                {
                    return (Boolean)this["productionMode"];
                }
                set
                {
                    this["productionMode"] = value;
                }
            }

            [ConfigurationProperty("isLocalTest", IsRequired = false, DefaultValue = false)]
            public Boolean IsLocalTest
            {
                get
                {
                    return (Boolean)this["isLocalTest"];
                }
                set
                {
                    this["isLocalTest"] = value;
                }
            }
        }
    }
}
