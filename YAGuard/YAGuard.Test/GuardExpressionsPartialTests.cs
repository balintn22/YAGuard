using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YAGuard.Test
{
    [TestClass]
    public class GuardExpressionsPartialTests
    {
        [DataTestMethod]
        [DataRow(null)]
        public void GuardExpressions_AgainstNull_NullValue_ShouldThrowExpected(string arg1)
        {
            try { Guard.AgainstNull(() => arg1); }
            catch (ArgumentNullException ex)
            {
                if (ex.ParamName != "arg1")
                    Assert.Fail($"An exception with an incorrect ParameterName ({ex.ParamName}) was thrown.");
                if (ex.Message != "Parameter may not be null\r\nParameter name: arg1")
                    Assert.Fail("An exception with an incorrect message was thrown.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception, other than the expected one was thrown.");
            }
        }

        [DataTestMethod]
        [DataRow("not null")]
        public void GuardExpressions_AgainstNull_NonNullValue_ShouldBeReturned(string arg1)
        {
            string value = Guard.AgainstNull(() => arg1);

            value.Should().Be("not null");
        }
    }
}
