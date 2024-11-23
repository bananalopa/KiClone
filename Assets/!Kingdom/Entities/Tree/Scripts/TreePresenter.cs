using System;
using R3;
using UnityEngine;

namespace Kingdom.Entities
{
	public class TreePresenter: MonoBehaviour
	{
		[SerializeField] private TreeModel treeModel;
		[SerializeField] private TreeView treeView;
		[SerializeField] private InteractableObject interactableObject;
		[SerializeField] private TreeSettingSo treeSettingSo;
		private void Awake()
		{
			interactableObject.SetInteractionPrice(treeSettingSo.Setting.CutPrice);
		}

		private void Start()
		{
			interactableObject.OnInteract().Subscribe(_ =>
			{
				treeModel.State = TreeModel.TreeStateEnum.Marked;
				treeView.ShowMark();
				interactableObject.IsInteractable().Value = false;
			}).AddTo(this);
		}
	}
}