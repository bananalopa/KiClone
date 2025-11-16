using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UltimateParallax.Editor.UIToolkit
{
    public class LayerGroupElement : VisualElement
    {
        private const int margin = 5;

        private int groupIndex;
        private SerializedProperty prop;
        private SerializedObject serializedObject;

        public LayerGroupElement(SerializedObject _serializedObject, SerializedProperty _prop, int _index)
        {
            Setup(_serializedObject, _prop, _index);
        }

        private void Setup(SerializedObject _serializedObject, SerializedProperty _prop, int _index)
        {
            prop = _prop;
            serializedObject = _serializedObject;
            groupIndex = _index;

            Draw();
        }

        private void Draw()
        {
            Clear();

            GroupProps();

            var layers = prop.FindPropertyRelative("layers");

            var parentGroup = new VisualElement
            {
                style =
                {
                    marginBottom = margin,
                    marginLeft = margin,
                    marginRight = margin,
                    marginTop = margin
                }
            };

            var listView = new ListView
            {
                headerTitle = "Layers",
                showBorder = true,
                showAddRemoveFooter = false,
                showBoundCollectionSize = false,
                showFoldoutHeader = true,
                pickingMode = PickingMode.Position,
                reorderable = true,
                selectionType = SelectionType.None,
                bindingPath = layers.propertyPath,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                makeItem = MakeItem,
                bindItem = BindItem,
                reorderMode = ListViewReorderMode.Animated,
                style = { unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft) }
            };

            listView.Bind(serializedObject);

            parentGroup.Add(listView);

            Add(parentGroup);
        }

        private void BindItem(VisualElement ve, int itemIndex)
        {
            var layersProp = prop.FindPropertyRelative("layers");

            if (layersProp?.GetArrayElementAtIndex(itemIndex) == null ||
                layersProp.GetArrayElementAtIndex(itemIndex).objectReferenceValue == null)
                return;

            var propSo = new SerializedObject(layersProp.GetArrayElementAtIndex(itemIndex).objectReferenceValue);
            var layerElement = ve.Q<LayerElement>();
            layerElement.Setup(propSo);
            layerElement.groupUpClicked = () =>
            {
                if (groupIndex <= 0) 
                    return;
                
                var propVal = prop.FindPropertyRelative("layers").GetArrayElementAtIndex(itemIndex).boxedValue;
                prop.FindPropertyRelative("layers").DeleteArrayElementAtIndex(itemIndex);
                var newGroupProp = serializedObject.FindProperty("layerGroups").GetArrayElementAtIndex(groupIndex - 1).FindPropertyRelative("layers");
                newGroupProp.InsertArrayElementAtIndex(newGroupProp.arraySize);
                newGroupProp.GetArrayElementAtIndex(newGroupProp.arraySize - 1).boxedValue = propVal;
                serializedObject.ApplyModifiedProperties();
                Draw();
            };
            var layerSize = serializedObject.FindProperty("layerGroups").arraySize - 1;
            layerElement.groupDownClicked = () =>
            {
                if (groupIndex >= layerSize) 
                    return;
                
                var propVal = prop.FindPropertyRelative("layers").GetArrayElementAtIndex(itemIndex).boxedValue;
                prop.FindPropertyRelative("layers").DeleteArrayElementAtIndex(itemIndex);
                var newGroupProp = serializedObject.FindProperty("layerGroups").GetArrayElementAtIndex(groupIndex + 1).FindPropertyRelative("layers");
                newGroupProp.InsertArrayElementAtIndex(newGroupProp.arraySize);
                newGroupProp.GetArrayElementAtIndex(newGroupProp.arraySize - 1).boxedValue = propVal;
                serializedObject.ApplyModifiedProperties();
                Draw();
            };
        }

        private static VisualElement MakeItem()
        {
            return new LayerElement();
        }


        private void GroupProps()
        {
            Add(EditorUtils.BindProp("Group Name", prop.propertyPath + ".groupName", "groupName", prop.serializedObject));

            Add(GroupFieldButton("Group Parent", "Set Parent", prop, "groupParent", () =>
            {
                foreach (SerializedProperty layer in prop.FindPropertyRelative("layers"))
                {
                    var parallaxLayer = (UltimateParallaxLayer)layer.boxedValue;
                    parallaxLayer?.SetLayerParent(prop.FindPropertyRelative("groupParent").objectReferenceValue as Transform);
                }
            }));

            Add(EditorUtils.BindProp("Starting Layer", prop.propertyPath + ".startingLayerOrder", prop.serializedObject,
                val =>
                {
                    foreach (SerializedProperty layer in prop.FindPropertyRelative("layers"))
                    {
                        var parallaxLayer = (UltimateParallaxLayer)layer.boxedValue;
                        parallaxLayer?.SetParallaxLayer(val.changedProperty.intValue);
                    }
                }));

            Add(EditorUtils.BindProp("Z Offset", prop.propertyPath + ".zOffset", prop.serializedObject, val =>
            {
                foreach (SerializedProperty layer in prop.FindPropertyRelative("layers"))
                {
                    var parallaxLayer = (UltimateParallaxLayer)layer.boxedValue;
                    parallaxLayer?.SetZOffset(val.changedProperty.floatValue);
                }
            }));

            var sliderX = new Slider(-200, 200)
            {
                label = "Parallax Speed X",
                showMixedValue = true,
                showInputField = true,
                bindingPath = prop.propertyPath + ".parallaxSpeedX"
            };
            sliderX.Bind(prop.serializedObject);
            sliderX.RegisterValueChangedCallback(val =>
            {
                foreach (SerializedProperty layer in prop.FindPropertyRelative("layers"))
                {
                    var parallaxLayer = (UltimateParallaxLayer)layer.boxedValue;
                    parallaxLayer?.SetParallaxSpeedX(val.newValue);
                }
            });
            Add(sliderX);

            var sliderY = new Slider(-200, 200)
            {
                label = "Parallax Speed Y",
                showMixedValue = true,
                showInputField = true,
                bindingPath = prop.propertyPath + ".parallaxSpeedY"
            };
            sliderY.Bind(prop.serializedObject);
            sliderY.RegisterValueChangedCallback(val =>
            {
                foreach (SerializedProperty layer in prop.FindPropertyRelative("layers"))
                {
                    var parallaxLayer = (UltimateParallaxLayer)layer.boxedValue;
                    parallaxLayer?.SetParallaxSpeedY(val.newValue);
                }
            });
            Add(sliderY);
        }

        private static VisualElement GroupFieldButton(string label, string buttonLabel, SerializedProperty serializedProp, string propName, Action clickEvent)
        {
            var result = EditorUtils.FieldButtonGroup(
                EditorUtils.BindProp(
                    label,
                    serializedProp.propertyPath + "." + propName,
                    serializedProp.serializedObject));
            var setButton = new Button(clickEvent)
                { text = buttonLabel };

            result.buttonGroup.Add(setButton);

            return result.parentGroup;
        }
    }
}