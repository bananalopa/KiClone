using UnityEngine;

namespace Kingdom.Monarch
{
	public class MonarchMainController : MonoBehaviour
	{
		[SerializeField] MonarchMoveController monarchMoveController;
		[SerializeField] MonarchInteractionController monarchInteractionController;
		[SerializeField] ClosestInteractableLookup closestInteractableLookup;
		[SerializeField] private Pouch pouch;
	}
}