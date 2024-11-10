using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom
{
	public abstract class Interactable : MonoBehaviour
	{
		public abstract void Interact();
		
		public abstract bool IsInteractable();
	}
}