using System.Configuration;
using System.Diagnostics.Contracts;

namespace MvcFlashMessages
{
    public class Config
    {
        private string innerCssClass;
        private readonly static Config instance = new Config();
        private string outerCssClass;

        private Config() { }

        public string InnerCssClass
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()), "Return value cannot be null, empty string, or white space.");
                if (innerCssClass == null)
                {
                    innerCssClass = ConfigurationManager.AppSettings["MvcFlashMessages/InnerCssClass"] ?? "flash-message";
                }                
                return innerCssClass;
            }
            set { innerCssClass = value; }
        }

        public static Config Instance
        {
            get
            {
                Contract.Ensures(Contract.Result<Config>() != null, "Instance is not null.");
                return instance;
            }
        }

        public string OuterCssClass
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()), "Return value cannot be null, empty string, or white space.");
                if (outerCssClass == null)
                {
                    outerCssClass = ConfigurationManager.AppSettings["MvcFlashMessages/OuterCssClass"] ?? "flash-messages";
                }
                return outerCssClass;
            }
            set { outerCssClass = value; }
        }
    }
}
