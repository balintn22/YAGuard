using System;
using System.Collections.Generic;
using System.Linq;

namespace YAGuard
{
    /// <summary>
    /// Static class to implement method argument checking.
    /// </summary>
    /// <example>Guard.AgainstNull(argument, nameof(argument));</example>
    public static class Guard
    {
        #region Generic Checks

        public static void AgainstCondition(bool condition, string argName = null, string message = null)
        {
            if (condition)
                throw new ArgumentException(message ?? $"Argument {argName ?? ""} did not satisfy condition.");
        }

        public static void AgainstCondition(Func<bool> condition, string argName = null, string message = null)
        {
            if (!condition())
                throw new ArgumentException(message ?? $"Argument {argName ?? ""} did not satisfy condition.");
        }

        public static void AgainstNull(object argValue, string argName, string message = null)
        {
            if (argValue == null)
                throw new ArgumentNullException(argName, message ?? argName + " may not be null");
        }

        // TODO: Test
        public static void NotNull<TValue>(object arg)
        {
            ArgHelper.CheckUsage<TValue>(arg);
            Guard.AgainstNull(ArgHelper.GetProp0Value<TValue>(arg), ArgHelper.GetProp0Name(arg));
        }

        public static void AgainstUnsupportedValues<T>(T argumentValue, string argumentName, IEnumerable<T> supportedValues, string message = null)
        {
            if ((supportedValues == null) || !supportedValues.Contains(argumentValue))
                throw new ArgumentException(
                    message
                    ?? $"Argument value not supported. Supported values are {string.Join(", ", supportedValues)}",
                    argumentName);
        }

        #endregion Generic Checks

        #region String Checks

        public static void AgainstNonIntString(string shouldBeInt, string argumentName)
        {
            if (!Int32.TryParse(shouldBeInt, out int dummy))
                throw new ArgumentException($"'{shouldBeInt}' was exepcted to be an integer.", argumentName);
        }

        public static void AgainstNullOrEmptyString(string argumentValue, string argumentName, string message = null)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentException(message ?? "Parameter cannot be null or an empty string.", argumentName);
        }

        // TODO: Test
        public static void AgainstNullOrEmptyString(object arg)
        {
            ArgHelper.CheckUsage<string>(arg);
            ArgHelper.AssertType(arg, typeof(string));
            Guard.AgainstNullOrEmptyString(ArgHelper.GetProp0Value<string>(arg), ArgHelper.GetProp0Name(arg));
        }

        public static void AgainstNullOrWhiteSpaceString(string argumentValue, string argumentName, string message = null)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
                throw new ArgumentException(message ?? "Parameter cannot be null or whitespace.", argumentName);
        }

        // TODO: Test
        public static void AgainstNullOrWhiteSpaceString(object arg)
        {
            ArgHelper.CheckUsage<string>(arg);
            ArgHelper.AssertType(arg, typeof(string));
            Guard.AgainstNullOrWhiteSpaceString(ArgHelper.GetProp0Value<string>(arg), ArgHelper.GetProp0Name(arg));
        }

        public static void AgainstLongString(string argumentValue, string argumentName, int maxAcceptableLength, string message = null)
        {
            if (!string.IsNullOrWhiteSpace(argumentValue) && (argumentValue.Length > maxAcceptableLength))
                throw new ArgumentException(
                    message
                    ?? string.Format("String argument too long, {0} characters, max {1} allowed.", argumentValue.Length, maxAcceptableLength),
                    argumentName);
        }

        #endregion String Checks

        #region Collections

        public static void AgainstNullOrEmptyCollection<T>(IEnumerable<T> argumentValue, string argumentName, string message = null)
        {
            if ((argumentValue == null) || (argumentValue.Count() == 0))
                throw new ArgumentException(message ?? "Parameter cannot be null or an empty collection.", argumentName);
        }

        #endregion Collections

        #region Int Checks

        public static void AgainstNegativeInt(int argumentValue, string argumentName, string message = null)
        {
            if (argumentValue < 0)
                throw new ArgumentException(message ?? "Parameter cannot be negative.", argumentName);
        }

        public static void AgainstNonPositiveInt(int argumentValue, string argumentName, string message = null)
        {
            if (argumentValue <= 0)
                throw new ArgumentException(message ?? "Parameter cannot be negative or zero.", argumentName);
        }

        #endregion Int Checks
    }
}
