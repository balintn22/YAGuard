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
    public static class Assign
    {
        public static TValue NotNull<TValue>(TValue value)
        {
            Guard.AgainstNull(value, argName: ArgHelper.ArgName());
            return value;
        }
    }
}
