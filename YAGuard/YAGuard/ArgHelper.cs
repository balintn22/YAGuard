using System.Diagnostics;
using System.Reflection;

namespace YAGuard
{
    public static class ArgHelper
    {
        /// <summary>
        /// Gets the name of an argument from a calling method from the call stack.
        /// </summary>
        /// <param name="stackLevel">Specifies the level of the caller in the hierarchy</param>
        /// <returns></returns>
        public static string ArgName(uint stackLevel = 2)
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame((int)stackLevel).GetMethod();
            ParameterInfo[] callingMethodParameters = callingMethod.GetParameters();
            return callingMethodParameters[0].Name;
        }
    }
}
