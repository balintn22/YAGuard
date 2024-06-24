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
                if (ex.Message != "Parameter may not be null (Parameter 'arg1')")
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

        private enum MyEnums
        {
            MyEnum1
        }

        private interface ITestInterface
        {
            string TestStringProperty { get; }
        }

        private class TestParentClass
        {
        }

        private class TestClass : TestParentClass, ITestInterface
        {
            public string TestStringProperty { get; } = "Bla";
            public string TestStringNullProperty { get; } = null;
            public MyEnums TestEnumProperty { get; } = MyEnums.MyEnum1;
        }

        [TestMethod]
        public void GuardExpressions_WhenExpressionContainsAnyProperty_HappyCase()
        {
            TestClass testClassInstance = new TestClass();

            Guard.AgainstNull(() => testClassInstance.TestStringProperty);

            Guard.AgainstUnsupportedValues(
                () => testClassInstance.TestStringProperty,
                new string[] { "Bla", "Blabla" });

            Guard.AgainstUnsupportedValues(
                () => testClassInstance.TestEnumProperty,
                new MyEnums[] { MyEnums.MyEnum1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GuardExpressions_WhenExpressionContainsAnyProperty_ValidationFailure_ShouldWorkAsExpected()
        {
            TestClass testClassInstance = new TestClass();

            Guard.AgainstNull(() => testClassInstance.TestStringNullProperty);
        }

        #region AgainstInvalidType Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GuardExpressions_AgainstInvalidType_ShouldFail_WhenTypeInvalid()
        {
            TestClass testClassInstance = new TestClass();

            Guard.AgainstInvalidType<string>(() => testClassInstance, "testClassInstance is not a string.");
        }

        [TestMethod]
        public void GuardExpressions_AgainstInvalidType_ShouldSucceed_WhenTypeIsExactMatch()
        {
            TestClass testClassInstance = new TestClass();

            TestClass result = Guard.AgainstInvalidType<TestClass>(() => testClassInstance);
        }

        [TestMethod]
        public void GuardExpressions_AgainstInvalidType_ShouldSucceed_WhenTypeIsDerivedFromRequiredType()
        {
            TestClass testClassInstance = new TestClass();

            TestParentClass result = Guard.AgainstInvalidType<TestParentClass>(() => testClassInstance, "testClassInstance is is derived from TestParentClass, the check should succeed.");
        }

        [TestMethod]
        public void GuardExpressions_AgainstInvalidType_ShouldSucceed_WhenTypeImplementsReuiredInterface()
        {
            TestClass testClassInstance = new TestClass();

            ITestInterface result = Guard.AgainstInvalidType<ITestInterface>(() => testClassInstance, "testClassInstance implements ITestInterface, the test should succeed.");
        }

        #endregion AgainstInvalidType Tests
    }
}
