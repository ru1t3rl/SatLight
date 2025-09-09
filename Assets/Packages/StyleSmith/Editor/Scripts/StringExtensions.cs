namespace StyleSmith.Editor
{
    public static class StringExtensions
    {
        public static string AsBackingField(this string propertyName) =>
            $"<{propertyName}>k__BackingField";
    }
}