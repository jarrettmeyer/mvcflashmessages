using System;
using System.Configuration;
using System.Diagnostics.Contracts;

namespace MvcFlashMessages
{
    public class Config
    {
        private string _closeClickEvent;
        private string _innerCssClass;
        private readonly static Config _instance = new Config();
        private bool? _isClosable;
        private string _outerCssClass;

        private Config() { }

        public string CloseClickEvent
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()), "Return value cannot be null, empty string, or white space.");
                if (_closeClickEvent == null)
                {
                    _closeClickEvent = ConfigurationManager.AppSettings["MvcFlashMessages/CloseClickEvent"] ?? "(function(el){var parent=el.parentNode;parent.parentNode.removeChild(parent);})(this);";
                }
                return _closeClickEvent;
            }
            set { _closeClickEvent = value; }
        }

        public string InnerCssClass
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()), "Return value cannot be null, empty string, or white space.");
                if (_innerCssClass == null)
                {
                    _innerCssClass = ConfigurationManager.AppSettings["MvcFlashMessages/InnerCssClass"] ?? "flash-message";
                }                
                return _innerCssClass;
            }
            set { _innerCssClass = value; }
        }

        public static Config Instance
        {
            get
            {
                Contract.Ensures(Contract.Result<Config>() != null, "Instance is not null.");
                return _instance;
            }
        }

        public bool IsClosable
        {
            get
            {
                if (_isClosable == null)
                {
                    _isClosable = Convert.ToBoolean(ConfigurationManager.AppSettings["MvcFlashMessages/IsClosable"] ?? "false");
                }
                return _isClosable.Value;
            }
            set { _isClosable = value; }
        }

        public string OuterCssClass
        {
            get
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()), "Return value cannot be null, empty string, or white space.");
                if (_outerCssClass == null)
                {
                    _outerCssClass = ConfigurationManager.AppSettings["MvcFlashMessages/OuterCssClass"] ?? "flash-messages";
                }
                return _outerCssClass;
            }
            set { _outerCssClass = value; }
        }
    }
}
