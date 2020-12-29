using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace YAGuard
{
    public static class ArgHelper
    {
        /// <summary>
        /// Gets the name of an argument from a calling method from the call stack.
        /// In case there are multiple arguments of type T, returns a string with the names of all
        /// those arguments and a description.
        /// </summary>
        /// <param name="stackLevel">Specifies the level of the caller in the hierarchy. Optional, defaults to 2.</param>
        /// <param name="type">Specifies the argument type, to help find the correct argument.</param>
        /// <returns></returns>
        public static string ArgName(Type type, uint stackLevel = 2)
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame((int)stackLevel).GetMethod();
            ParameterInfo[] callingMethodParameters = callingMethod.GetParameters();

            // Try to identify the parameter
            if (callingMethodParameters.Length == 0)
                return "Couldn't determine the name of the parameter that is failing validation. You may be guarding a variable other than a method parameter or your code may be optimized. For correct results, use the expression syntax i.e. Guard.Against...( () => myArg);";

            if (callingMethodParameters.Length == 1)
                return callingMethodParameters[0].Name;

            IEnumerable<ParameterInfo> matchingParameters = callingMethodParameters
                .Where(x => x.ParameterType == type);

            if (matchingParameters.Count() == 1)
                return matchingParameters.First().Name;

            return $"One of {string.Join(", ", matchingParameters)}. To get an exact variable name in this case, use" +
                $" Guard method overloads that take an expression argument, like Guard.AgainstNull( () => myArg).";
        }
    }
}
