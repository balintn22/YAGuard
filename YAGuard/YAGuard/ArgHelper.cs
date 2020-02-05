using System;

namespace YAGuard
{
    /// <summary>
    /// Helps carry aout assign tests.
    /// </summary>
    internal static class ArgHelper
    {
        /// <summary>Gets the name of the first property of an anonymous variable.</summary>
        /// <arg>Is an anonymous object, with a single property.</arg>
        internal static string GetProp0Name(object arg)
        {
            Guard.AgainstNull(arg, nameof(arg));
            return arg.GetType().GetProperties()[0].Name;
        }

        /// <summary>Gets the type of the first property of an anonymous variable.</summary>
        /// <arg>Is an anonymous object, with a single property.</arg>
        internal static Type GetProp0Type(object arg)
        {
            Guard.AgainstNull(arg, nameof(arg));
            return arg.GetType().GetProperties()[0].PropertyType;
        }

        /// <summary>Verifies that the first property of an anonymous variable is that of an expected type.</summary>
        /// <arg>Is an anonymous object, with a single property.</arg>
        internal static void AssertType(object arg, Type expectedType)
        {
            Guard.AgainstNull(arg, nameof(arg));
            Type actualType = GetProp0Type(arg);
            if (actualType != expectedType)
                throw new ArgumentException($"Invalid type '{actualType.Name}'. '{expectedType.Name}' expected.");
        }

        /// <summary>Gets the value of the first property of an anonymous variable.</summary>
        /// <arg>Is an anonymous object, with a single property.</arg>
        internal static TValue GetProp0Value<TValue>(object arg)
        {
            Guard.AgainstNull(arg, nameof(arg));
            //AssertType(arg, typeof(TValue));
            return (TValue)arg.GetType().GetProperties()[0].GetValue(arg);
        }

        /// <summary>Checks that the argument of an Assign call meets usage requirements, so that usage in code is correct.</summary>
        /// <arg>Is an anonymous object, with a single property.</arg>
        internal static void CheckUsage<TValue>(object arg)
        {
            const string usage = "string val = Assign.NotNull<string>(new {arg});";

            Guard.AgainstNull(arg, nameof(arg));

            // Check that arg is of an anonymous type
            if (!arg.GetType().Name.Contains("AnonymousType"))
                throw new ArgumentException($"Invalid use of SafeAssign.Assign(). Pass argument to be tested and assigned as an anonymous object. Usage example:\n{usage}");

            var props = arg.GetType().GetProperties();

            // Check that arg has a single property
            if (props.Length != 1)
                throw new ArgumentException($"Invalid use of SafeAssign.Assign(). The anonymous object argument should have a single property, containing the argument to test and return. Usage example:\n{usage}");

            // Check that the single property of arg is of type TValue
            //if (props[0].PropertyType != typeof(TValue))
            //    throw new ArgumentException($"Invalid use of SafeAssign.Assign(). The anonymous object argument type {props[0].PropertyType.Name} doesn't match the expected result type {nameof(TValue)}. Usage example:\n{usage}");
        }
    }
}
