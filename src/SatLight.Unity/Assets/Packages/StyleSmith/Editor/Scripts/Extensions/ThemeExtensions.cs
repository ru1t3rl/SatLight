using System.Globalization;
using System.IO;
using System.Linq;
using StyleSmith.Runtime.Domain;
using UnityEditor;

namespace StyleSmith.Editor.Extensions
{
	public static class ThemeExtensions
	{
		public static void GenerateEnums(this Theme theme)
		{
			Directory.CreateDirectory(theme.EnumGenerationPath);

			string colorEnum = GenerateEnumString(nameof(Theme.Colors), theme.Colors.Select(c => c.Name).ToArray());
			string typographyEnum = GenerateEnumString(
				nameof(Theme.Typographies),
				theme.Typographies.Select(t => t.Name).ToArray()
			);

			WriteToFile(Path.Combine(theme.EnumGenerationPath, "Colors.cs"), colorEnum);
			WriteToFile(Path.Combine(theme.EnumGenerationPath, "Typographies.cs"), typographyEnum);
		}

		private static void WriteToFile(string path, string content)
		{
			File.WriteAllText(path, content);
			AssetDatabase.Refresh();
		}

		private static string GenerateEnumString(string name, string[] values)
		{
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			string[] formattedValues = values
				.Where(v => !string.IsNullOrWhiteSpace(v) && !string.IsNullOrEmpty(v))
				.Select(v => v.Trim())
				.Select(v => textInfo.ToTitleCase(v))
				.ToArray();

			string t = $@"
namespace {nameof(StyleSmith)}.{nameof(Runtime)}.{nameof(Runtime.Domain)}.{nameof(Runtime.Domain.Enums)}
{{
	public enum {name}
	{{
		{string.Join(",\n", formattedValues)}			
	}} 	
}}			         			            
";
			return t;
		}
	}
}