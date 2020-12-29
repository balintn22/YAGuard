using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YAGuard.Test
{
    [TestClass]
    [Obsolete("Assign is obsolete, remove these tests when Assign is removed.")]
    public class AssignTests
    {
        #region NotNull

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("blah")]
        public void NotNull_ShouldSucceed(string goodValue)
        {
            string result = Assign.NotNull<string>(goodValue);
            result.Should().Be(goodValue);
        }

        // Note: When compiled for Release, this method is optimized, and parameter
        // name resolution fails, prompting the developer to use the expression form.
        [TestMethod]
        [DataRow(null)]
        public void NotNull_ShouldFail(string badValue)
        {
            try { string result = Assign.NotNull<string>(badValue); }
            catch (ArgumentNullException ex) { ex.Message.Should().Be("Parameter may not be null\r\nParameter name: badValue"); }
            catch { Assert.Fail(); }
        }

        #endregion NotNull
    }
}
