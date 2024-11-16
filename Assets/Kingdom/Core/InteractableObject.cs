using UnityEngine;
using UnityEngine.Serialization;
using System;
using R3;

namespace Kingdom
{
	public class InteractableObject : MonoBehaviour, IInteractable
	{
		[SerializeField] private IntReference interactionPrice;
		[SerializeField] SerializableReactiveProperty<bool> isInteractable = new ();
		Subject<Unit> onInteract = new(); 

		public Subject<Unit> OnInteract()
		{
			return onInteract;
		}

		public SerializableReactiveProperty<bool> IsInteractable()
		{
			return isInteractable;
		}

		public virtual void Interact()
		{
			if (isInteractable.Value)
				onInteract.OnNext(Unit.Default);
		}

		public int InteractionPrice()
		{
			return interactionPrice.Value;
		}
	}
}