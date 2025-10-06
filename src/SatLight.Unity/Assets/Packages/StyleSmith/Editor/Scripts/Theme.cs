using UnityEngine;

namespace StyleSmith.Runtime.Domain
{
	public partial class Theme
	{
		[SerializeField]
		private string enumGenerationPath = "Assets/Scripts/StyleSmith/Runtime/Domain/Enums/";

		public string EnumGenerationPath
		{
			get => enumGenerationPath;
			set => enumGenerationPath = value;
		}
	}
}