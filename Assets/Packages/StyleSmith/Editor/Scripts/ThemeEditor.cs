using System;
using System.Collections.Generic;
using StyleSmith.Runtime.Domain;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// [CustomEditor(typeof(Theme))]
public class ThemeEditor : Editor
{
    [SerializeField] private VisualTreeAsset visualTree;

    private VisualElement _root;
    private ListView _colorsListView;
    private ListView _typographiesListView;
    private Theme _theme;

    public override VisualElement CreateInspectorGUI()
    {
        if (visualTree == null)
        {
            _root = new VisualElement();

            var horizontalContainer = CreateWarningBox("UXML not loaded, please put on the editor script!");

            _root.Add(horizontalContainer);
            return _root;
        }

        _theme = target as Theme;
        _root = visualTree.CloneTree();

        _colorsListView = _root.Q<ListView>(nameof(Theme.Colors));
        _typographiesListView = _root.Q<ListView>(nameof(Theme.Typographies));

        _colorsListView.SetBinding(
            nameof(_colorsListView.itemsSource),
            new DataBinding { dataSourcePath = new PropertyPath(nameof(Theme.Colors)) }
        );

        _colorsListView.itemsSource = _theme.Colors;
        _typographiesListView.itemsSource = _theme.Typographies;

        SetupListView(_colorsListView, _theme.Colors);
        SetupListView(_typographiesListView, _theme.Typographies);

        return _root;
    }

    private static VisualElement CreateWarningBox(string message)
    {
        var horizontalContainer = new VisualElement();
        horizontalContainer.AddToClassList("unity-help-box");
        horizontalContainer.style.flexDirection = FlexDirection.Row;

        horizontalContainer.style.paddingTop = 4;
        horizontalContainer.style.paddingBottom = 4;
        horizontalContainer.style.paddingLeft = 8;
        horizontalContainer.style.paddingRight = 8;

        horizontalContainer.style.marginTop = 4;
        horizontalContainer.style.marginBottom = 4;

        horizontalContainer.Add(new Label(message));
        return horizontalContainer;
    }

    private void SetupListView<T>(ListView listView, OptionCollection<T> collection) where T : IOption, new()
    {
        listView.itemsSource = collection;

        listView.itemsAdded += OnItemsAdded(collection);
        listView.itemsRemoved += OnItemsRemoved(collection);

        listView.RefreshItems();
    }

    private Action<IEnumerable<int>> OnItemsAdded<T>(OptionCollection<T> collection)
        where T : IOption, new()
    {
        return indices =>
        {
            foreach (var index in indices)
            {
                var newItem = new T();
                if (newItem is ColorOption colorOption)
                {
                    colorOption.Name = $"Color {collection.Count + 1}";
                }
                else if (newItem is TypographyOption typographyOption)
                {
                    typographyOption.Name = $"Typography {collection.Count + 1}";
                }

                collection.Add(newItem);
            }

            EditorUtility.SetDirty(_theme);
        };
    }

    private Action<IEnumerable<int>> OnItemsRemoved<T>(OptionCollection<T> collection)
        where T : IOption, new()
    {
        return indices =>
        {
            var sortedIndices = new List<int>(indices);
            sortedIndices.Sort((a, b) => b.CompareTo(a));

            foreach (var index in sortedIndices)
            {
                if (index < collection.Count)
                {
                    var item = collection[index];
                    collection.Remove(item);
                }
            }

            EditorUtility.SetDirty(_theme);
        };
    }
}