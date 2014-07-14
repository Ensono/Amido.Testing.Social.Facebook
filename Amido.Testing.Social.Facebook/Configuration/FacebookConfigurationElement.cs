using System.Configuration;

namespace Amido.Testing.Social.Facebook.Configuration
{
    public class FacebookConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("appId", IsKey = true, IsRequired = true)]
        public string AppId
        {
            get
            {
                return this["appId"].ToString();
            }
            set
            {
                this["appId"] = value;
            }
        }

        [ConfigurationProperty("secret", IsRequired = true)]
        public string Secret
        {
            get
            {
                return this["secret"].ToString();
            }
            set
            {
                this["secret"] = value;
            }
        }
    }
}