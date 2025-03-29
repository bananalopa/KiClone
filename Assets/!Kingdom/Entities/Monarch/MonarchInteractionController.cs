using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Monarch
{
	public class MonarchInteractionController : MonoBehaviour
	{
		public Subject<IInteractable> OnInteractableChange = new();
		
		ApproachablesLookup approachablesLookup;
		IInteractable lastInteractable;

		[Inject]
		void Construct(ApproachablesLookup approachablesLookup)
		{
			this.approachablesLookup = approachablesLookup;
		}
		
		private void Start()
		{
			approachablesLookup.OnApproachablesChange.Subscribe(approachables =>
			{
				IInteractable interactable = approachables.
					Select(x => x.GetComponent<IInteractable>()).
					WhereNotNull().
					FirstOrDefault(interactable => interactable.IsInteractable().Value);
				
				if (interactable!=lastInteractable)
					OnInteractableChange.OnNext(interactable);
				
				lastInteractable = interactable;
			}).AddTo(this);
		}

		public IInteractable GetClosestInteractable()
		{
			return lastInteractable;
		}
	}
}
