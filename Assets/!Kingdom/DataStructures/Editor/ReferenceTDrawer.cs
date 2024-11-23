using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

namespace Kingdom.Editor
{
	public class ReferenceTDrawer<T> : PropertyDrawer
	{
		private SerializedProperty scriptableObjectVariableSerializedProperty;
		private PropertyField valueTypeValue;
		private PropertyField valueProperty;
		private PropertyField scriptableObjectVariableProperty;
		SerializedProperty property;
		private SerializedProperty valueSerializedProperty;
		
		SerializedObject serializedObject;
		VisualElement container;
		
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			container = new VisualElement();
			this.property = property;
			container.style.alignContent = Align.Center;
			container.style.flexDirection = FlexDirection.Row;
			
			var labelText = new Label(property.name);
			container.Add(labelText);

			scriptableObjectVariableSerializedProperty = property.FindPropertyRelative("ScriptableObjectVariable");
			
			scriptableObjectVariableProperty = new PropertyField(scriptableObjectVariableSerializedProperty, typeof(T).Name);
			container.Add(scriptableObjectVariableProperty);

			valueTypeValue = new PropertyField(property.FindPropertyRelative("ValueTypeValue"),"Value");
			container.Add(valueTypeValue);
			
			//Refresh();
			
			//container.TrackPropertyValue(scriptableObjectVariableSerializedProperty, _ =>
			//{
				//Refresh();
			//});
			if (scriptableObjectVariableSerializedProperty.objectReferenceValue)
			{
				serializedObject = new SerializedObject(scriptableObjectVariableSerializedProperty.objectReferenceValue);
				valueSerializedProperty = serializedObject.FindProperty("Value");
				//container.Bind(serializedObject);
				valueProperty = new PropertyField(valueSerializedProperty, "Value");
				valueProperty.Bind(serializedObject);
				container.Add(valueProperty);
			}
			
			
			var isNull = scriptableObjectVariableSerializedProperty.objectReferenceValue == null;
			valueTypeValue.style.display = isNull ? DisplayStyle.Flex : DisplayStyle.None;
			if (valueProperty!=null)
				valueProperty.style.display = isNull ? DisplayStyle.None : DisplayStyle.Flex;
			
			return container;
		}

		void Refresh()
		{
			container.Unbind();
			if (scriptableObjectVariableSerializedProperty.objectReferenceValue )
			{
				serializedObject = new SerializedObject(scriptableObjectVariableSerializedProperty.objectReferenceValue);
				valueSerializedProperty = serializedObject.FindProperty("Value");
				container.Bind(serializedObject);
				
				valueProperty = new PropertyField(valueSerializedProperty, "Value");
				valueProperty.Bind(serializedObject);
				container.Add(valueProperty);
				
				
				
			}
			
			var isNull = scriptableObjectVariableSerializedProperty.objectReferenceValue == null;
			valueTypeValue.style.display = isNull ? DisplayStyle.Flex : DisplayStyle.None;
			if (valueProperty!=null)
				valueProperty.style.display = isNull ? DisplayStyle.None : DisplayStyle.Flex;

			//scriptableObjectVariableProperty.style.maxWidth = isNull ? 200 : int.MaxValue;
		}
		
	}
}


