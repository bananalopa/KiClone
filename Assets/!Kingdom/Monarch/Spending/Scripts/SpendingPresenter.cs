using System;
using Kingdom.Input;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Monarch
{
	public class SpendingPresenter : MonoBehaviour
	{
		[SerializeField] private SpendingModel spendingModel;
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
				Refund();
				currentInteractable = null;
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
			
			spendingModel.OnSpend.Subscribe(interactable =>
			{
				interactable?.Interact();
				spendingView.Hide();
			}).AddTo(this);
			
			spendingModel.OnGetRefund.Subscribe(amount =>
			{
				spendingView.ResetReservedCoins();
			}).AddTo(this);
			
			inputHandler.DownHold.Subscribe(isDownHold =>
			{
				if (isDownHold)
				{
					if (currentInteractable == null || !currentInteractable.IsInteractable().Value)
						return;
					
					spendingView.Show(currentInteractable.InteractionPrice(), currentInteractable.gameObject.transform.position);
					disposable = Observable.Interval(TimeSpan.FromSeconds(timeRequiredToSpendOneCoin.Value)).Subscribe(_ =>
					{
						if (currentInteractable == null || !currentInteractable.IsInteractable().Value)
							return;
						if (pouchPresenter.TryRemoveCoins())
						{
							spendingModel.AddCoinsToReserve(currentInteractable);
							spendingView.SetReservedCoins(spendingModel.CoinsReserve.Value);
						}
					}).AddTo(this);


				}
				else
				{
					disposable?.Dispose();
					Refund();
				}
			}).AddTo(this);
		}

		void Refund()
		{
			pouchPresenter.AddCoins(spendingModel.GetRefund());
		}
		
		private void OnDestroy()
		{
			disposable?.Dispose();
		}
	}
}