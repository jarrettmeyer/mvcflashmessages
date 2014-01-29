using System;
using System.Configuration;
using System.Diagnostics.Contracts;

namespace MvcFlashMessages
{
    public class Config
    {
        private string closeClickEvent;
        private string innerCssClass;
        private readonly static Config instance = new Config();
        private bool? isClosable;
        private string outerCssClass;

        private Config() { }

        public string CloseClickEvent
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()), "Return value cannot be null, empty string, or white space.");
                if (closeClickEvent == null)
                {
                    closeClickEvent = ConfigurationManager.AppSettings["MvcFlashMessages/CloseClickEvent"] ?? "(function(el){var parent=el.parentNode;parent.parentNode.removeChild(parent);})(this);";
                }
                return closeClickEvent;
            }
            set { closeClickEvent = value; }
        }

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

        public bool IsClosable
        {
            get
            {
                if (isClosable == null)
                {
                    isClosable = Convert.ToBoolean(ConfigurationManager.AppSettings["MvcFlashMessages/IsClosable"] ?? "false");
                }
                return isClosable.Value;
            }
            set { isClosable = value; }
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
