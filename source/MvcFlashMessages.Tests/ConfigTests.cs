using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void Instance_is_not_null()
        {
            Assert.IsNotNull(Config.Instance);
        }
    }
}
