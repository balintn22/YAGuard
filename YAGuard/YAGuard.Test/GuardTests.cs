using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YAGuard.Test
{
    [TestClass]
    public class GuardTests
    {
        #region NotNull
        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("blah")]
        public void AgainstNull_ShouldSucceed(object goodValue)
        {
            Guard.AgainstNull(new { goodValue });
        }

        [DataTestMethod]
        [DataRow(null)]
        public void AgainstNull(string blah)
        {
            try { Guard.AgainstNull(blah); }
            catch (ArgumentNullException ex) { ex.Message.Should().Be("Parameter may not be null\r\nParameter name: blah"); }
            catch { Assert.Fail(); }
        }

        #endregion NotNull
    }
}
