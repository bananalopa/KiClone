using Kingdom;
using UnityEditor;

namespace Kingdom.Editor
{
	[CustomPropertyDrawer(typeof(IntReference), true)]
	public class IntReferenceDrawer : ReferenceTDrawer<IntVariable>
	{
		
	}
}
