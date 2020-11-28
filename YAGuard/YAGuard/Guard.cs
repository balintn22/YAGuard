using System;
using System.Collections.Generic;
using System.Linq;

namespace YAGuard
{
    /// <summary>
    /// Static class to implement method argument validation.
    /// In case of validation errors, exceptions are thrown, that contain the name of the offending argument.
    /// </summary>
    /// <example>
    /// Guard.AgainstNull(myString);
    /// Will throw ArgumentNullException with argument name 'myString'.
    /// </example>
    public static class Guard
    {
        #region Generic Checks

        /// <summary>Validates an argument.</summary>
        /// <exception cref="ArgumentNullException"/>
        public static void AgainstNull(object arg, string argName = null, string message = null)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName ?? ArgHelper.ArgName(), message ?? "Parameter may not be null");
            }
        }

        /// <summary>Validates a condition. Throws if condiotion is false.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstCondition(bool condition, string argName = null, string message = null)
        {
            if (condition)
                throw new ArgumentException(message ?? $"Argument {argName ?? ArgHelper.ArgName()} did not satisfy condition.");
        }

        /// <summary>Validates an argument against a set of acceptable values.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstUnsupportedValues<T>(
            T argumentValue, IEnumerable<T> supportedValues, string argName = null, string message = null)
        {
            if ((supportedValues == null) || !supportedValues.Contains(argumentValue))
            {
                throw new ArgumentException(
                    message
                    ?? $"Argument value not supported. Supported values are {string.Join(", ", supportedValues)}",
                    argName ?? ArgHelper.ArgName());
            }
        }

        #endregion Generic Checks

        #region String Checks

        /// <summary>Throws if the specified value is not a string representation of an Int32.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstNonIntString(string shouldBeInt, string argName = null)
        {
            if (!Int32.TryParse(shouldBeInt, out int dummy))
                throw new ArgumentException(
                    $"'{shouldBeInt}' is expected to be an integer.", argName ?? ArgHelper.ArgName());
        }

        /// <summary>Throws if the specified value is null or an empty string.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstNullOrEmptyString(string argumentValue, string argName = null, string message = null)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty string.", argName ?? ArgHelper.ArgName());
        }

        /// <summary>Throws if the specified value is null, an empty string or whitespace.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstNullOrWhiteSpaceString(
            string argumentValue, string argName = null, string message = null)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or whitespace.", argName ?? ArgHelper.ArgName());
        }

        /// <summary>Throws if the specified value is a string that is too long.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstLongString(
            string argumentValue, int maxAcceptableLength, string argName = null, string message = null)
        {
            if (!string.IsNullOrWhiteSpace(argumentValue) && (argumentValue.Length > maxAcceptableLength))
                throw new ArgumentException(
                    message
                    ?? string.Format("String argument too long, {0} characters, max {1} allowed.", argumentValue.Length, maxAcceptableLength),
                    argName ?? ArgHelper.ArgName());
        }

        #endregion String Checks

        #region Collections

        /// <summary>Throws if the specified value is null or an empty collection.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstNullOrEmptyCollection<T>(
            IEnumerable<T> argumentValue, string argName = null, string message = null)
        {
            if ((argumentValue == null) || (argumentValue.Count() == 0))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty collection.", argName ?? ArgHelper.ArgName());
        }

        #endregion Collections

        #region Int Checks

        /// <summary>Throws if the specified integer value is negative.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstNegativeInt(Int64 argumentValue, string argName = null, string message = null)
        {
            if (argumentValue < 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative.", argName ?? ArgHelper.ArgName());
        }

        /// <summary>Throws if the specified integer value is zero or negative.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstNonPositiveInt(Int64 argumentValue, string argName = null, string message = null)
        {
            if (argumentValue <= 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative or zero.", argName ?? ArgHelper.ArgName());
        }

        #endregion Int Checks
    }
}
