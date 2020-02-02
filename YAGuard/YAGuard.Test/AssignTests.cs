using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace YAGuard.Test
{
    [TestClass]
    public class AssignTests
    {
        #region NotNull
        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("blah")]
        public void NotNull_ShouldSucceed(string goodValue)
        {
            string result = Assign.NotNull<string>(new { goodValue });
        }

        [TestMethod]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_ShouldFail(string badValue)
        {
            string result = Assign.NotNull<string>(new { badValue });
        }
        #endregion NotNull
    }
}
