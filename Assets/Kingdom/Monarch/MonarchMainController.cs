using UnityEngine;

namespace Kingdom
{
	public class MonarchMainController : MonoBehaviour
	{
		[SerializeField] MonarchMoveController monarchMainController;
		[SerializeField] MonarchInteractionController monarchInteractionController;
		[SerializeField] ClosestInteractableLookup closestInteractableLookup;
	}
}