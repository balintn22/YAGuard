using System;
using System.Collections.Generic;

namespace YAGuard
{
    /// <summary>
    /// Implements variable assignments with simple value checks.
    /// </summary>
    /// <example>
    /// string result = Assign.NotNull(string sourceString);
    /// When sourceString is not null, will set result to  the value of SourceString.
    /// When sourceString is null, will throw an ArgumentNullException, with parameter name set to 'sourceString'.
    /// </example>
     [Obsolete("Use the corresponding Guard statements instead.")]
    public static class Assign
    {
        #region Generic

        /// <summary>Returns the specified value if that is not null.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentNullException"/>
        public static TValue NotNull<TValue>(TValue value, string argName = null)
        {
            Guard.AgainstNull(value, argName: argName ?? ArgHelper.ArgName(typeof(TValue)));
            return value;
        }

        /// <summary>Returns the specified value if that is not null.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentNullException"/>
        public static TValue IfNotNull<TValue>(TValue value, string argName = null)
        {
            Guard.AgainstNull(value, argName: argName ?? ArgHelper.ArgName(typeof(TValue)));
            return value;
        }

        /// <summary>Returns the specified value if it is one of the collection provided. THrows otherwise.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static TValue IfOneOf<TValue>(TValue value, IEnumerable<TValue> supportedValues, string argName = null)
        {
            Guard.AgainstUnsupportedValues(value, supportedValues, argName: argName ?? ArgHelper.ArgName(typeof(TValue)));
            return value;
        }

        #endregion Generic


        #region Strings

        /// <summary>Returns the specified value if it is a string representation of an integer.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static string IfRepresentsAnInteger(string value, string argName = null)
        {
            Guard.AgainstNonIntString(value, argName: argName ?? ArgHelper.ArgName(typeof(string)));
            return value;
        }

        /// <summary>Returns the integer value of the specified value if is a string representation of an integer.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static int IfRepresentsAnIntegerAsInteger(string value, string argName = null)
        {
            Guard.AgainstNonIntString(value, argName: argName ?? ArgHelper.ArgName(typeof(string)));
            return int.Parse(value);
        }

        /// <summary>Returns the specified value if it is not null or empty.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static string IfNotNullOrEmpty(string value, string argName = null)
        {
            Guard.AgainstNullOrEmptyString(value, argName: argName ?? ArgHelper.ArgName(typeof(string)));
            return value;
        }

        /// <summary>Returns the specified value if it is not null or whitespace.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static string IfNotNullOrWhiteSpace(string value, string argName = null)
        {
            Guard.AgainstNullOrWhiteSpaceString(value, argName: argName ?? ArgHelper.ArgName(typeof(string)));
            return value;
        }

        /// <summary>Returns the specified string if it is at most the specified length.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static string IfNotTooLong(string value, int maxAcceőtableLength, string argName = null)
        {
            Guard.AgainstLongString(value, maxAcceőtableLength, argName: argName ?? ArgHelper.ArgName(typeof(string)));
            return value;
        }

        #endregion Strings


        #region Collections

        /// <summary>Returns the specified collection if it is not null or empty.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static TColl IfNotNullOrEmpty<TColl, TItem>(TColl value, string argName = null)
            where TColl : IEnumerable<TItem>
        {
            Guard.AgainstNullOrEmptyCollection(value, argName: argName ?? ArgHelper.ArgName(typeof(TColl)));
            return value;
        }

        #endregion Collections


        #region Integers

        /// <summary>Returns the specified int value if it is non-negative.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static int IfNonNegative(int value, string argName = null)
        {
            Guard.AgainstNegativeInt(value, argName: argName ?? ArgHelper.ArgName(typeof(int)));
            return value;
        }

        /// <summary>Returns the specified int value if it is positive.</summary>
        /// <param name="argName">If specified and a validation error occurs, the resulting exception will contain this string as the name of the argument, instead of the name of the validated argument.</param>
        /// <exception cref="ArgumentException"/>
        public static int IfPositive(int value, string argName = null)
        {
            Guard.AgainstNonPositiveInt(value, argName: argName ?? ArgHelper.ArgName(typeof(int)));
            return value;
        }

        #endregion Integers
    }
}
