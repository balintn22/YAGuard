using System;
using System.Collections.Generic;
using System.Linq;

namespace YAGuard
{
    // This partial class implementation contains Guard methods that take a single,
    // plain method argument. They work well when
    //  - the method has a single argument
    //  - the guarded argument can be identified by its type
    // In all other cases, use the expression overloads, like Guard.AgainstNull( () => myArg )

    /// <summary>
    /// Static class to implement method argument validation.
    /// In case of validation errors, exceptions are thrown, that contain the name of the offending argument.
    /// Otherwise methods return the validated argument.
    /// </summary>
    /// <example>
    /// Guard.AgainstNull(myString);
    /// Will throw ArgumentNullException with argument name 'myString'.
    /// </example>
    public static partial class Guard
    {
        #region Generic Checks

        /// <summary>
        /// Validates an argument.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// <returns>In case the validation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T AgainstNull<T>(T arg, string argName = null, string message = null)
        {
            if (arg == null)
                throw new ArgumentNullException(argName ?? ArgHelper.ArgName(typeof(T)), message ?? "Parameter may not be null");

            return arg;
        }

        /// <summary>
        /// Validates a condition. Throws if condition is false.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        public static void AgainstCondition(bool condition, string argName = null, string message = null)
        {
            if (condition)
                throw new ArgumentException(message ?? $"Guard condition was not met.", argName);
        }

        /// <summary>
        /// Validates an argument against a set of acceptable values.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <param name="argName">The name of the argument to include in case of an exception. If omitted, the argumnet name in the exception will include an educated guess.</param>
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
                    argName ?? ArgHelper.ArgName(typeof(T)));
            }

            return arg;
        }

        #endregion Generic Checks


        #region String Checks

        /// <summary>
        /// Throws if the specified value is not a string representation of an Int32.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNonIntString(string arg, string argName = null)
        {
            if (!Int32.TryParse(arg, out int dummy))
                throw new ArgumentException(
                    $"'{arg}' is expected to be an integer.", argName ?? ArgHelper.ArgName(typeof(string)));

            return arg;
        }

        /// <summary>
        /// Throws if the specified value is null or an empty string.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNullOrEmptyString(
            string arg, string argName = null, string message = null)
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty string.", argName ?? ArgHelper.ArgName(typeof(string)));

            return arg;
        }

        /// <summary>
        /// Throws if the specified value is null, an empty string or whitespace.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNullOrWhiteSpaceString(
            string arg, string argName = null, string message = null)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or whitespace.", argName ?? ArgHelper.ArgName(typeof(string)));

            return arg;
        }

        /// <summary>
        /// Throws if the specified value is a string that is too long.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstLongString(
            string arg, int maxAcceptableLength, string argName = null, string message = null)
        {
            if (!string.IsNullOrWhiteSpace(arg) && (arg.Length > maxAcceptableLength))
                throw new ArgumentException(
                    message
                    ?? string.Format("String argument too long, {0} characters, max {1} allowed.", arg.Length, maxAcceptableLength),
                    argName ?? ArgHelper.ArgName(typeof(string)));

            return arg;
        }

        #endregion String Checks


        #region Collections

        /// <summary>
        /// Throws if the specified value is null or an empty collection.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static IEnumerable<T> AgainstNullOrEmptyCollection<T>(
            IEnumerable<T> arg, string argName = null, string message = null)
        {
            if ((arg == null) || (arg.Count() == 0))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty collection.", argName ?? ArgHelper.ArgName(typeof(IEnumerable<T>)));

            return arg;
        }

        #endregion Collections


        #region Int Checks

        /// <summary>
        /// Throws if the specified integer value is negative.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static Int64 AgainstNegativeInt(Int64 arg, string argName = null, string message = null)
        {
            if (arg < 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative.", argName ?? ArgHelper.ArgName(typeof(Int64)));

            return arg;
        }

        /// <summary>
        /// Throws if the specified integer value is zero or negative.
        /// This is the simplest form, but can only be used when the parameter being validated is the only such type in the list of method parameters. If there are more parameters of the same type, or you are validating anything but a parameter, use the expression form.</summary>
        /// </summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static Int64 AgainstNonPositiveInt(Int64 arg, string argName = null, string message = null)
        {
            if (arg <= 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative or zero.", argName ?? ArgHelper.ArgName(typeof(Int64)));

            return arg;
        }

        #endregion Int Checks


        #region Type Checks

        /// <summary>
        /// Validates an argument type.
        /// The test will succeed if the argument
        ///  - is of the specified type
        ///  - is derived from the specifeid type
        ///  - implements the specified interface
        /// <returns>In case the validation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static TRequired AgainstInvalidType<TRequired>(object arg, string argName = null, string message = null)
        {
            if (arg != null && !typeof(TRequired).IsAssignableFrom(arg.GetType()))
            {
                throw new ArgumentException(
                    message ?? $"Parameter must be of type {typeof(TRequired)} or assignable to it.",
                    argName ?? ArgHelper.ArgName(typeof(object)));
            }

            return (TRequired)arg;
        }

        #endregion Type Checks
    }
}
