using System;
using System.Collections.Generic;
using System.Linq;

namespace YAGuard
{
    /// <summary>
    /// Static class to implement method argument validation.
    /// In case of validation errors, exceptions are thrown, that contain the name of the offending argument.
    /// Otherwise methods return the validated argument.
    /// </summary>
    /// <example>
    /// Guard.AgainstNull(myString);
    /// Will throw ArgumentNullException with argument name 'myString'.
    /// </example>
    public static class Guard
    {
        #region Generic Checks

        /// <summary>Validates an argument.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T AgainstNull<T>(T arg, string argName = null, string message = null)
        {
            if (arg == null)
                throw new ArgumentNullException(argName ?? ArgHelper.ArgName(), message ?? "Parameter may not be null");

            return arg;
        }

        /// <summary>Validates a condition. Throws if condiotion is false.</summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstCondition(bool condition, string argName = null, string message = null)
        {
            if (condition)
                throw new ArgumentException(message ?? $"Argument {argName ?? ArgHelper.ArgName()} did not satisfy condition.");
        }

        /// <summary>Validates an argument against a set of acceptable values.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static T AgainstUnsupportedValues<T>(
            T arg, IEnumerable<T> supportedValues, string argName = null, string message = null)
        {
            if ((supportedValues == null) || !supportedValues.Contains(arg))
            {
                throw new ArgumentException(
                    message
                    ?? $"Argument value not supported. Supported values are {string.Join(", ", supportedValues)}",
                    argName ?? ArgHelper.ArgName());
            }

            return arg;
        }

        #endregion Generic Checks

        #region String Checks

        /// <summary>Throws if the specified value is not a string representation of an Int32.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNonIntString(string arg, string argName = null)
        {
            if (!Int32.TryParse(arg, out int dummy))
                throw new ArgumentException(
                    $"'{arg}' is expected to be an integer.", argName ?? ArgHelper.ArgName());

            return arg;
        }

        /// <summary>Throws if the specified value is null or an empty string.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNullOrEmptyString(
            string arg, string argName = null, string message = null)
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty string.", argName ?? ArgHelper.ArgName());

            return arg;
        }

        /// <summary>Throws if the specified value is null, an empty string or whitespace.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNullOrWhiteSpaceString(
            string arg, string argName = null, string message = null)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or whitespace.", argName ?? ArgHelper.ArgName());

            return arg;
        }

        /// <summary>Throws if the specified value is a string that is too long.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstLongString(
            string arg, int maxAcceptableLength, string argName = null, string message = null)
        {
            if (!string.IsNullOrWhiteSpace(arg) && (arg.Length > maxAcceptableLength))
                throw new ArgumentException(
                    message
                    ?? string.Format("String argument too long, {0} characters, max {1} allowed.", arg.Length, maxAcceptableLength),
                    argName ?? ArgHelper.ArgName());

            return arg;
        }

        #endregion String Checks

        #region Collections

        /// <summary>Throws if the specified value is null or an empty collection.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static IEnumerable<T> AgainstNullOrEmptyCollection<T>(
            IEnumerable<T> arg, string argName = null, string message = null)
        {
            if ((arg == null) || (arg.Count() == 0))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty collection.", argName ?? ArgHelper.ArgName());

            return arg;
        }

        #endregion Collections

        #region Int Checks

        /// <summary>Throws if the specified integer value is negative.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static Int64 AgainstNegativeInt(Int64 arg, string argName = null, string message = null)
        {
            if (arg < 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative.", argName ?? ArgHelper.ArgName());

            return arg;
        }

        /// <summary>Throws if the specified integer value is zero or negative.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static Int64 AgainstNonPositiveInt(Int64 arg, string argName = null, string message = null)
        {
            if (arg <= 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative or zero.", argName ?? ArgHelper.ArgName());

            return arg;
        }

        #endregion Int Checks
    }
}
