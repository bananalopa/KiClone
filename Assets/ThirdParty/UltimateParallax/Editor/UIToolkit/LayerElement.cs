using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UltimateParallax.Editor.UIToolkit
{
    public class LayerElement : VisualElement
    {
        public Action groupDownClicked;

        public Action groupUpClicked;
        private SerializedObject serializedObject;

        public void Setup(SerializedObject _serializedObject)
        {
            Clear();

            serializedObject = _serializedObject;

            var foldout = new Foldout
            {
                text = _serializedObject.targetObject.name,
                toggleOnLabelClick = true,
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter),
                    flexGrow = 1
                },
                value = serializedObject.FindProperty("foldout").boolValue
            };

            foldout.RegisterValueChangedCallback(evt =>
            {
                serializedObject.FindProperty("foldout").boolValue = evt.newValue;
                serializedObject.ApplyModifiedProperties();
            });

            var buttonGroup = new VisualElement
            {
                style =
                {
                    alignSelf = new StyleEnum<Align>(Align.FlexEnd),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)
                }
            };

            var layerUpButton = new Button(() =>
                {
                    var layer = (UltimateParallaxLayer)_serializedObject.targetObject;
                    layer.SetParallaxLayer(layer.parallaxLayer + 1);
                })
                { text = "+", style = { width = 25, paddingRight = 0 } };

            var layerDownButton = new Button(() =>
                {
                    var layer = (UltimateParallaxLayer)_serializedObject.targetObject;
                    layer.SetParallaxLayer(layer.parallaxLayer - 1);
                })
                { text = "-", style = { width = 25, paddingRight = 0 } };

            var upGroupButton = new Button(() => { groupUpClicked?.Invoke(); })
            {
                name = "upGroupButton",
                text = "\u25b2",
                style =
                {
                    width = 25,
                    paddingRight = 0
                }
            };
            var downGroupButton = new Button(() => { groupDownClicked?.Invoke(); })
            {
                name = "downGroupButton",
                text = "\u25bc",
                style =
                {
                    width = 25,
                    paddingRight = 0
                }
            };

            buttonGroup.Add(layerUpButton);
            buttonGroup.Add(layerDownButton);

            buttonGroup.Add(new Label("|") { style = { width = 10 } });

            buttonGroup.Add(upGroupButton);
            buttonGroup.Add(downGroupButton);

            foldout.Q<Toggle>().Add(buttonGroup);


            foldout.Add(EditorUtils.BindProp("SpeedX", "parallaxSpeedX", _serializedObject));

            foldout.Add(EditorUtils.BindProp("SpeedY", "parallaxSpeedY", _serializedObject));

            foldout.Add(EditorUtils.BindProp("Layer", "parallaxLayer", _serializedObject, _ =>
            {
                if (_serializedObject.targetObject is UltimateParallaxLayer UltimateParallaxLayer)
                    UltimateParallaxLayer.ApplyParallaxLayer();
            }));

            Add(foldout);
        }
    }
}