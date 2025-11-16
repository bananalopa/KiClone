using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UltimateParallax.Editor
{
    public static class EditorUtils
    {
        [MenuItem("Assets/Tools/UltimateParallax/Create Scene Objects from Selection")]
        public static void CreateSpritesFromAssets()
        {
            var assets = Selection.objects;
            foreach (var asset in assets)
            {
                var loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(asset));
                if (loadedSprite == null)
                    continue;

                var go = new GameObject(loadedSprite.name);
                var sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = loadedSprite;
                // var go = Object.Instantiate(loadedSprite);
            }
        }

        [MenuItem("Tools/UltimateParallax/Utils/Add Empty Parent to Selection")]
        public static void AddEmptyParentToSelection()
        {
            foreach (var selected in Selection.gameObjects)
            {
                var newParent = new GameObject(selected.name);
                if (selected.transform.parent != null)
                    newParent.transform.SetParent(selected.transform.parent);
                selected.transform.SetParent(newParent.transform);
            }
        }

        [MenuItem("Tools/UltimateParallax/Utils/Set Sprite Order Ascending")]
        public static void SetSpriteOrderAscending()
        {
            int? startingLayer = null;
            foreach (var selected in Selection.gameObjects)
                if (selected.TryGetComponent(out SpriteRenderer sr))
                {
                    if (startingLayer == null)
                        startingLayer = sr.sortingOrder;
                    else
                        sr.sortingOrder = startingLayer.Value;
                    startingLayer++;
                }
        }

        [MenuItem("Tools/UltimateParallax/Utils/Set Sprite Order Descending")]
        public static void SetSpriteOrderDescending()
        {
            int? startingLayer = null;
            foreach (var selected in Selection.gameObjects)
                if (selected.TryGetComponent(out SpriteRenderer sr))
                {
                    if (startingLayer == null)
                        startingLayer = sr.sortingOrder;
                    else
                        sr.sortingOrder = startingLayer.Value;
                    startingLayer--;
                }
        }

        public static PropertyField BindProp(string label, string binding, SerializedObject serializedObject,
            EventCallback<SerializedPropertyChangeEvent> onValueChanged = null)
        {
            return BindProp(label, binding, null, serializedObject, onValueChanged);
        }
        
        public static PropertyField BindProp(string label, string binding, string name, SerializedObject serializedObject,
            EventCallback<SerializedPropertyChangeEvent> onValueChanged = null)
        {
            var propertyField = new PropertyField
            {
                label = label,
                style =
                {
                    flexGrow = 1,
                    unityTextAlign = TextAnchor.MiddleLeft
                }
            };
            if(name != null)
            {
                propertyField.name = name;
            }
            propertyField.bindingPath = binding;
            propertyField.Bind(serializedObject);
            propertyField.RegisterValueChangeCallback(onValueChanged);
            return propertyField;
        }

        public static (VisualElement parentGroup,VisualElement buttonGroup) FieldButtonGroup(VisualElement fieldElement)
        {
            var layerGroup = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            layerGroup.Add(fieldElement);

            var buttonGroup = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };

            layerGroup.Add(buttonGroup);

            return (layerGroup, buttonGroup);
        }
    }
}