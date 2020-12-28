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


        #region Guard method with multiple arguments

        [DataTestMethod]
        [DataRow(null, "not null")]
        public void AgainstNull_ForMethodWithMultipleArguments_1stArgShouldBeHandledCorrectly(
            string arg1, object arg2)
        {
            try { Guard.AgainstNull(arg1); }
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
        [DataRow("not null", null)]
        public void AgainstNull_ForMethodWithMultipleArguments_2ndArgShouldBeHandledCorrectly(
            object arg1, string arg2)
        {
            try { Guard.AgainstNull(arg2); }
            catch (ArgumentNullException ex)
            {
                if (ex.ParamName != "arg2")
                    Assert.Fail($"An exception with an incorrect ParameterName ({ex.ParamName}) was thrown.");
                if (ex.Message != "Parameter may not be null\r\nParameter name: arg2")
                    Assert.Fail("An exception with an incorrect message was thrown.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception, other than the expected one was thrown.");
            }
        }

        #endregion Guard method with multiple arguments


        #region Guard Properties

        public string TestProperty;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgainstNull_WithProperty()
        {
            TestProperty = null;
            Guard.AgainstNull(TestProperty);
        }

        #endregion Guard Properties
    }
}
