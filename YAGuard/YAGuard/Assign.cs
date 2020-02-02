namespace YAGuard
{
    /// <summary>
    /// Implements variable assignments with simple value checks.
    /// </summary>
    /// <example>
    /// public void MyFunc(string arg)
    /// {
    ///     string myString = Assign.NotNullString(arg);
    /// }</example>
    public static class Assign
    {
        public static TValue NotNull<TValue>(TValue value, string argName)
        {
            Guard.AgainstNull(value, argName);
            return value;
        }

        public static TValue NotNull<TValue>(object arg)
        {
            ArgHelper.CheckUsage<TValue>(arg);
            ArgHelper.AssertType(arg, typeof(string));
            return Assign.NotNull(ArgHelper.GetProp0Value<TValue>(arg), ArgHelper.GetProp0Name(arg));
        }
    }
}
