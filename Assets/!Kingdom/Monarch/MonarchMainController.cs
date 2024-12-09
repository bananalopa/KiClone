using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Monarch
{
	public class MonarchMainController : MonoBehaviour
	{
		[SerializeField] MonarchMoveController monarchMoveController;
		[SerializeField] MonarchInteractionController monarchInteractionController;
		[SerializeField] ApproachableController approachableController;
		[FormerlySerializedAs("pouchPresenter")] [SerializeField] MonarchPouchPresenter monarchPouchPresenter;
	}
}