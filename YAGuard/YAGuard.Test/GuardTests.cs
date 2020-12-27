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
            var actualValue = Guard.AgainstNull(goodValue);

            actualValue.Should().Be(goodValue);
        }

        [DataTestMethod]
        [DataRow(null)]
        public void AgainstNull_ShouldProduceExpectedException(string badValue)
        {
            try { Guard.AgainstNull(badValue); }
            catch (ArgumentNullException ex) { ex.Message.Should().Be("Parameter may not be null\r\nParameter name: badValue"); }
            catch { Assert.Fail(); }
        }

        [DataTestMethod]
        [DataRow(null)]
        public void AgainstNull_WithAlias_ShouldProduceExpectedException(string badValue)
        {
            try { Guard.AgainstNull(badValue, "myAlias"); }
            catch (ArgumentNullException ex) { ex.Message.Should().Be("Parameter may not be null\r\nParameter name: myAlias"); }
            catch { Assert.Fail(); }
        }

        #endregion NotNull
    }
}
