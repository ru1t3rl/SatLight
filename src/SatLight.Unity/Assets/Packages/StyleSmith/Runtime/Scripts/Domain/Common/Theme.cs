using System.Collections.Generic;
using UnityEngine;

namespace StyleSmith.Runtime.Domain
{
    [CreateAssetMenu(fileName = "Theme", menuName = "StyleSmith/Theme", order = 0)]
    public partial class Theme : ScriptableObject
    {
        [field: SerializeField] public OptionCollection<ColorOption> Colors { get; set; } = new();

        [field: SerializeField] public OptionCollection<TypographyOption> Typographies { get; set; } = new();
    }
}