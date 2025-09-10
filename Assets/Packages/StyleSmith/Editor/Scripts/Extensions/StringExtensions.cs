namespace StyleSmith.Editor.Extensions
{
    public static class StringExtensions
    {
        public static string AsBackingField(this string propertyName) =>
            $"<{propertyName}>k__BackingField";
    }
}