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

        [TestMethod]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgainstNull_ShouldFail(object badValue)
        {
            Guard.AgainstNull(new { badValue });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgainstNull_ShouldFail_WhenIncorrectlyInvoked1()
        {
            Guard.AgainstNull("argument is not provided as a property of an anonymous object");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgainstNull_ShouldFail_WhenIncorrectlyInvoked2()
        {
            Guard.AgainstNull(new { one = "anonymous argument", two = "has too many properties" });
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("blah")]
        public void AgainstNullT_ShouldSucceed(string goodValue)
        {
            Guard.AgainstNull<string>(new { goodValue });
        }

        [TestMethod]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgainstNullT_ShouldFail(string badValue)
        {
            Guard.AgainstNull<string>(new { badValue });
        }

        // TODO: This test is questionable. Should it really fail, if the runtime type is correct?
        [TestMethod]
        [DataRow("string")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgainstNullT_ShouldFail(object badValue)
        {
            Guard.AgainstNull<string>(new { badValue });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgainstNullT_ShouldFail_WhenIncorrectlyInvoked1()
        {
            Guard.AgainstNull<string>("argument is not provided as a property of an anonymous object");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgainstNullT_ShouldFail_WhenIncorrectlyInvoked2()
        {
            Guard.AgainstNull<string>(new { one = "anonymous argument", two = "has too many properties" });
        }
        #endregion NotNull
    }
}
