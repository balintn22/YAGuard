using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YAGuard
{
    // This partial class implementation contains Guard methods taking expression arguments, like
    //  Guard.AgainstNull( () => myArg);

    public static partial class Guard
    {
        #region Generic checks

        // Based on http://www.minddriven.de/index.php/technology/dot-net/c-sharp/efficient-expression-values
        public static T AgainstNull<T>(Expression<Func<T>> argExp, string message = null)
        {
            T argValue = GetArgValue(argExp);

            if (argValue == null)
                throw new ArgumentNullException(GetArgName(argExp), "Parameter may not be null");

            return argValue;
        }

        public static T AgainstUnsupportedValues<T>(
            Expression<Func<T>> argExp, IEnumerable<T> supportedValues, string message = null)
        {
            T argValue = GetArgValue(argExp);

            if ((supportedValues == null) || !supportedValues.Contains(argValue))
            {
                throw new ArgumentException(
                    message
                    ?? $"Argument value not supported. Supported values are {string.Join(", ", supportedValues)}",
                    GetArgName(argExp));
            }

            return argValue;
        }

        #endregion Generic checks


        #region String Checks

        /// <summary>Throws if the specified value is not a string representation of an Int32.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNonIntString(Expression<Func<string>> argExp)
        {
            string argValue = GetArgValue(argExp);

            if (!Int32.TryParse(argValue, out int dummy))
                throw new ArgumentException(
                    $"'{argValue}' is expected to be an integer.", GetArgName(argExp));

            return argValue;
        }

        /// <summary>Throws if the specified value is null or an empty string.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNullOrEmptyString(Expression<Func<string>> argExp, string message = null)
        {
            string argValue = GetArgValue(argExp);

            if (string.IsNullOrEmpty(argValue))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty string.", GetArgName(argExp));

            return argValue;
        }

        /// <summary>Throws if the specified value is null, an empty string or whitespace.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstNullOrWhiteSpaceString(Expression<Func<string>> argExp, string message = null)
        {
            string argValue = GetArgValue(argExp);

            if (string.IsNullOrWhiteSpace(argValue))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or whitespace.", GetArgName(argExp));

            return argValue;
        }

        /// <summary>Throws if the specified value is a string that is too long.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static string AgainstLongString(
            Expression<Func<string>> argExp, int maxAcceptableLength, string message = null)
        {
            string argValue = GetArgValue(argExp);

            if (!string.IsNullOrWhiteSpace(argValue) && (argValue.Length > maxAcceptableLength))
                throw new ArgumentException(
                    message
                    ?? string.Format("String argument too long, {0} characters, max {1} allowed.", argValue.Length, maxAcceptableLength),
                    GetArgName(argExp));

            return argValue;
        }

        #endregion String Checks


        #region Collections

        /// <summary>Throws if the specified value is null or an empty collection.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static IEnumerable<T> AgainstNullOrEmptyCollection<T>(
            Expression<Func<IEnumerable<T>>> argExp, string message = null)
        {
            IEnumerable<T> argValue = GetArgValue(argExp);

            if ((argValue == null) || (argValue.Count() == 0))
                throw new ArgumentException(
                    message ?? "Parameter cannot be null or an empty collection.", GetArgName(argExp));

            return argValue;
        }

        #endregion Collections


        #region Int Checks

        /// <summary>Throws if the specified integer value is negative.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static Int64 AgainstNegativeInt(Expression<Func<Int64>> argExp, string message = null)
        {
            Int64 argValue = GetArgValue(argExp);

            if (argValue < 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative.", GetArgName(argExp));

            return argValue;
        }

        /// <summary>Throws if the specified integer value is zero or negative.</summary>
        /// <returns>In case the valisation succeeds, returns the argument value.</returns>
        /// <exception cref="ArgumentException"/>
        public static Int64 AgainstNonPositiveInt(Expression<Func<Int64>> argExp, string message = null)
        {
            Int64 argValue = GetArgValue(argExp);

            if (argValue <= 0)
                throw new ArgumentException(
                    message ?? "Parameter cannot be negative or zero.", GetArgName(argExp));

            return argValue;
        }

        #endregion Int Checks


        #region Helpers

        private static T GetArgValue<T>(Expression<Func<T>> argExp)
        {
            // Based on https://stackoverflow.com/questions/2616638/access-the-value-of-a-member-expression
            MemberExpression right = (MemberExpression)argExp.Body;
            T value = (T)Expression.Lambda(right).Compile().DynamicInvoke();
            return value;
        }

        private static string GetArgName<T>(Expression<Func<T>> argExp) =>
            ((MemberExpression) argExp.Body).Member.Name;

        #endregion Helpers
    }
}
