using System;
using Kingdom.Input;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Monarch
{
	public class SpendingPresenter : MonoBehaviour
	{
		[SerializeField] SpendingModel spendingModel;
		[SerializeField] private SpendingView spendingView;
		[SerializeField] private FloatReference timeRequiredToSpendOneCoin;
		
		MonarchInteractionController monarchInteractionController;
		private PouchPresenter pouchPresenter;
		private InputHandler inputHandler;
		private IInteractable currentInteractable;
		private IDisposable disposable;
			
		[Inject]
		private void Construct(PouchPresenter pouchPresenter,
			InputHandler inputHandler,
			MonarchInteractionController monarchInteractionController)
		{
			this.pouchPresenter = pouchPresenter;
			this.inputHandler = inputHandler;
			this.monarchInteractionController = monarchInteractionController;
		}

		private void Start()
		{
			monarchInteractionController.OnInteractableChange.Subscribe(interactable =>
			{
				if (interactable == null)
				{
					spendingView.Hide();
				}
				else
				{
					spendingView.Show(interactable.InteractionPrice(), interactable.gameObject.transform.position);
					currentInteractable = interactable;
				}
			}).AddTo(this);
			
			
			inputHandler.DownHold.Subscribe(isDownHold =>
			{
				if (isDownHold)
				{
					if (currentInteractable == null)
						return;
					spendingModel = new SpendingModel(currentInteractable);
					disposable = spendingModel.OnSpend.Subscribe(interactable =>
					{
						
						interactable.Interact();
					});
					spendingView.Show(currentInteractable.InteractionPrice(), currentInteractable.gameObject.transform.position);
					Observable.Interval(TimeSpan.FromSeconds(timeRequiredToSpendOneCoin.Value)).Subscribe(_ =>
					{
						
					});
					
					
				}
				else
				{
					currentInteractable = null;

					disposable.Dispose();
					if (spendingModel == null)
						return;
					pouchPresenter.AddCoins(spendingModel.GetRefund());
					spendingModel?.Dispose();
					spendingModel = null;
				}
			}).AddTo(this);
		}

		private void OnDestroy()
		{
			disposable?.Dispose();
		}
	}
}