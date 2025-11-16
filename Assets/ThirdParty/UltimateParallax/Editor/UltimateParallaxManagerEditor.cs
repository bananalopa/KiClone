using System.Collections.Generic;
using System.Linq;
using UltimateParallax.Editor.UIToolkit;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UltimateParallax.Editor
{
    [CustomEditor(typeof(UltimateParallaxManager))]
    public class UltimateParallaxManagerEditor : UnityEditor.Editor
    {
        private VisualElement groupContainer;
        private VisualElement root;

        private int startingLayer;
        private float startingOffset;

        [MenuItem("Tools/UltimateParallax/Create UltimateParallax Manager")]
        [MenuItem("GameObject/Tools/UltimateParallax/Create UltimateParallax Manager")]
        public static void CreateUltimateParallaxManager()
        {
            var go = new GameObject("UltimateParallaxManager");
            go.AddComponent<UltimateParallaxManager>();
        }

        [MenuItem("Tools/UltimateParallax/Add UltimateParallax Layer")]
        public static void AddUltimateParallaxLayer()
        {
            if (Selection.gameObjects.Length == 0)
                return;

            foreach (var go in Selection.gameObjects)
            {
                if (go.GetComponent<UltimateParallaxLayer>() == null)
                {
                    go.AddComponent<UltimateParallaxLayer>();
                }
            }

            Undo.RecordObjects(Selection.gameObjects, "Setup UltimateParallax Layer");
        }

        [MenuItem("Tools/UltimateParallax/Setup Repeating Sprite")]
        public static void SetupRepeatingSprite()
        {
            if (Selection.gameObjects.Length == 0)
                return;

            foreach (var go in Selection.gameObjects)
            {
                if (go.GetComponent<SpriteRenderer>() == null)
                    continue;

                var repeatingSprite = go.GetComponent<UltimateParallaxRepeatingSprite>();
                
                if (repeatingSprite != null) 
                    continue;
                
                repeatingSprite = go.AddComponent<UltimateParallaxRepeatingSprite>();
                repeatingSprite.InitialSetup();
            }

            Undo.RecordObjects(Selection.gameObjects, "Setup Repeating Sprite");
        }


        public override VisualElement CreateInspectorGUI()
        {
            root?.Clear();

            root = new VisualElement();
            groupContainer = new VisualElement();

            root.Add(new VisualElement
            {
                style = { height = 10 }
            });

            BuildSettings();

            CheckLayerGroups();

            root.Add(new VisualElement
            {
                style = { height = 10 }
            });
            BuildGroupContainer();

            root.Add(groupContainer);

            BuildButtons();

            return root;
        }

        private void BuildSettings()
        {
            root.Add(new PropertyField(serializedObject.FindProperty("targetCamera")));
            root.Add(new PropertyField(serializedObject.FindProperty("previewInEditMode")));
            root.Add(new PropertyField(serializedObject.FindProperty("parallaxScale")));
        }

        private void BuildGroupContainer()
        {
            groupContainer?.Clear();

            var layers = serializedObject.FindProperty("layerGroups");

            for (var i = 0; i < layers.arraySize; i++)
            {
                var element = layers.GetArrayElementAtIndex(i);
                var foldoutProp = element.FindPropertyRelative("foldout");
                var foldoutVal = element.FindPropertyRelative("foldout").boolValue;
                var foldout = new Foldout
                {
                    text = element.FindPropertyRelative("groupName").stringValue,
                    value = foldoutVal,
                    toggleOnLabelClick = true,
                    style =
                    {
                        unityFontStyleAndWeight = FontStyle.Bold,
                        unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft),
                        flexGrow = 1
                    }
                };
                foldout.RegisterValueChangedCallback(evt =>
                {
                    if (evt.previousValue == evt.newValue)
                        return;

                    foldoutProp.boolValue = evt.newValue;
                    element.serializedObject.ApplyModifiedProperties();
                });

                var groupElement = new LayerGroupElement(serializedObject, element, i);
                groupElement.Bind(serializedObject);
                groupElement.Q<PropertyField>("groupName").RegisterValueChangeCallback(evt =>
                {
                    foldout.text = evt.changedProperty.stringValue;
                });
                foldout.Add(groupElement);

                var buttonGroup = new VisualElement
                {
                    style =
                    {
                        alignSelf = new StyleEnum<Align>(Align.FlexEnd),
                        flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)
                    }
                };

                var index = i;
                var upGroupButton = new Button(() =>
                {
                    if (index <= 0) 
                        return;
                    
                    layers.MoveArrayElement(index, index - 1);
                    serializedObject.ApplyModifiedProperties();
                    BuildGroupContainer();
                })
                {
                    name = "upGroupButton",
                    text = "\u25b2",
                    style =
                    {
                        width = 25,
                        paddingRight = 0
                    }
                };
                var downGroupButton = new Button(() =>
                {
                    if (index >= layers.arraySize - 1) 
                        return;
                    
                    layers.MoveArrayElement(index, index + 1);
                    serializedObject.ApplyModifiedProperties();
                    BuildGroupContainer();
                })
                {
                    name = "downGroupButton",
                    text = "\u25bc",
                    style =
                    {
                        width = 25,
                        paddingRight = 0
                    }
                };

                buttonGroup.Add(upGroupButton);
                buttonGroup.Add(downGroupButton);

                foldout.Q<Toggle>().Add(buttonGroup);


                var removeButton = new Button(() =>
                {
                    if (!EditorUtility.DisplayDialog(
                            "Delete group " + element.FindPropertyRelative("groupName").stringValue,
                            "Are you sure?!",
                            "Delete",
                            "Cancel"))
                    {
                        return;
                    }

                    layers.DeleteArrayElementAtIndex(index);
                    layers.serializedObject.ApplyModifiedProperties();
                    BuildGroupContainer();
                })
                {
                    text = "Remove Group " + element.FindPropertyRelative("groupName").stringValue,
                    style =
                    {
                        backgroundColor = new StyleColor(new Color(0.3f, 0, 0))
                    }
                };
                foldout.Add(removeButton);

                groupContainer.Add(foldout);
            }

            var addGroupButton = new Button(() =>
            {
                layers.InsertArrayElementAtIndex(layers.arraySize);
                layers.GetArrayElementAtIndex(layers.arraySize - 1).boxedValue = new UltimateParallaxLayerGroup();
                layers.serializedObject.ApplyModifiedProperties();
                BuildGroupContainer();
            })
            {
                text = "Add Group",
                style =
                {
                    backgroundColor = new StyleColor(new Color(0, 0.3f, 0)),
                    marginTop = 10,
                    marginBottom = 20
                }
            };

            groupContainer.Add(addGroupButton);
        }

        private void BuildButtons()
        {
            var findLayersButton = new Button
            {
                text = "Find UltimateParallax Layers"
            };
            findLayersButton.clicked += () => { FindLayers(false); };
            root.Add(findLayersButton);

            var findClearLayersButton = new Button
            {
                text = "Clear & Find UltimateParallax Layers"
            };
            findClearLayersButton.clicked += () => { FindLayers(true); };
            root.Add(findClearLayersButton);

            var groupsFromSelectionButton = new Button
            {
                text = "Set Groups From Selection",
                style =
                {
                    backgroundColor = new Color(.3f, 0, 0)
                }
            };
            groupsFromSelectionButton.clicked += SetGroupsFromSelection;
            root.Add(groupsFromSelectionButton);
        }

        private void SetGroupsFromSelection()
        {
            if (!EditorUtility.DisplayDialog(
                    "Are you sure?!",
                    "This will clear the current groups",
                    "Clear current groups and Find new groups",
                    "Cancel"))
                return;

            var manager = target as UltimateParallaxManager;

            if (manager == null) return;

            var groups = new List<UltimateParallaxLayerGroup>();

            foreach (var selected in Selection.gameObjects)
            {
                var group = new UltimateParallaxLayerGroup
                {
                    groupName = selected.name,
                    groupParent = selected.transform
                };

                int? layerIndex = null;
                foreach (Transform child in selected.transform)
                {
                    var parallaxLayer = child.GetComponent<UltimateParallaxLayer>();

                    parallaxLayer ??= child.gameObject.AddComponent<UltimateParallaxLayer>();

                    var childSprite = child.GetComponentInChildren<SpriteRenderer>();
                    if (childSprite != null && layerIndex == null)
                    {
                        layerIndex = childSprite.sortingOrder;
                    }
                    
                    group.layers.Add(parallaxLayer);
                }

                if (layerIndex != null)
                {
                    group.startingLayerOrder = layerIndex.Value;
                    group.zOffset = layerIndex.Value * 0.5f;
                    foreach (var layer in group.layers)
                    {
                        layer.SetParallaxLayer(layerIndex.Value);
                        layer.SetZOffset(layerIndex.Value * 0.5f);
                    }
                }

                groups.Add(group);
            }

            manager.layerGroups.Clear();

            manager.layerGroups = groups;

            serializedObject.ApplyModifiedProperties();
            BuildGroupContainer();
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        private void CheckLayerGroups()
        {
            var manager = target as UltimateParallaxManager;

            if (manager == null)
                return;

            manager.layerGroups ??= new List<UltimateParallaxLayerGroup>();

            if (manager.layerGroups.Count == 0)
            {
                manager.layerGroups.Add(new UltimateParallaxLayerGroup());
            }
        }

        private void FindLayers(bool clear)
        {
            var manager = target as UltimateParallaxManager;

            if (manager == null)
                return;

            if (clear)
            {
                foreach (var layerGroup in manager.layerGroups)
                {
                    layerGroup.layers.Clear();
                }
            }
            
            var ultimateParallaxLayers = new List<UltimateParallaxLayer>();
            
            foreach (var layerGroup in manager.layerGroups.Where(layerGroup => layerGroup.layers?.Count > 0))
            {
                ultimateParallaxLayers.AddRange(layerGroup.layers);
            }

            foreach (var layer in FindObjectsByType<UltimateParallaxLayer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (ultimateParallaxLayers.Contains(layer) == false)
                {
                    manager.layerGroups[0].layers.Add(layer);
                }
            }
            serializedObject.ApplyModifiedProperties();
            BuildGroupContainer();
        }
    }
}