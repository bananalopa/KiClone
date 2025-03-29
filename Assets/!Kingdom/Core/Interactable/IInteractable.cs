using R3;
using UnityEngine;

namespace Kingdom
{
	public interface IInteractable
	{
		public SerializableReactiveProperty<bool> IsInteractable();
		public Subject<Unit> OnInteract();
		public void Interact();
		public GameObject gameObject { get ; }

		public int InteractionPrice();

	}
}