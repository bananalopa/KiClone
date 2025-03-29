using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Monarch
{
	public class MonarchMainController : MonoBehaviour
	{
		[SerializeField] MonarchMoveController monarchMoveController;
		[SerializeField] MonarchInteractionController monarchInteractionController;
		[FormerlySerializedAs("approachableController")] [SerializeField] ApproachablesLookup approachablesLookup;
		[FormerlySerializedAs("pouchPresenter")] [SerializeField] MonarchPouchPresenter monarchPouchPresenter;
	}
}