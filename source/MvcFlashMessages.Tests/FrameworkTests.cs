using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace MvcFlashMessages
{
    /// <summary>
    /// Framework tests are exactly that - tests for the .NET framework. It is a
    /// sandbox for making sure that the .NET framework is going to perform a certain
    /// way under certain circumstances.
    /// </summary>
    [TestFixture]
    public class FrameworkTests
    {
        [Test]
        public void Adding_a_null_collection_to_a_list_throws_an_exception()
        {
            var list = new List<int>();
            var ex = Assert.Throws<ArgumentNullException>(() => list.AddRange(null));
            Debug.WriteLine(ex);
        }
    }
}
