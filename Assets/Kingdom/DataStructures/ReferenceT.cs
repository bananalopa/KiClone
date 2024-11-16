using System;
using UnityEngine.Serialization;

namespace Kingdom
{
	[Serializable]
	public class ReferenceT<T>
	{
		public T ValueTypeValue;
		public VariableT<T> ScriptableObjectVariable;
		public T Value => ScriptableObjectVariable ? ScriptableObjectVariable.Value : ValueTypeValue;
	}
}