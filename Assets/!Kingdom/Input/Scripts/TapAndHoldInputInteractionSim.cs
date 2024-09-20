using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kingdom.Input
{
	public class TapAndHoldInputInteractionSim : MonoBehaviour
	{
		[SerializeField] private InputActionReference tapAndHoldRef;
		[SerializeField] private float tapToHoldTimeout = .2f;
		[SerializeField] private bool isLoggingEnabled;
		
		private float tapTime;
		private bool isHoldStartedInTime;
		private StateEnum state = StateEnum.reset;
		public Action OnStarted;
		public Action OnPerformed;
		public Action OnPerformEnded;
		public Action OnCancelled;
		
		private void Start()
		{
			tapAndHoldRef.action.started += (context) =>
			{
				if (context.interaction is TapInteraction)
				{
					state = StateEnum.started;
					Logging("started");
					OnStarted?.Invoke();
				}
				
				if (context.interaction is HoldInteraction && (tapTime + tapToHoldTimeout > Time.unscaledTime))
				{
					isHoldStartedInTime = true;
					Logging("HoldIsStartedInTime");
				}
			};
			
			tapAndHoldRef.action.performed += (context) =>
			{
				if (context.interaction is TapInteraction)
					tapTime = Time.unscaledTime;
				

				if (context.interaction is HoldInteraction && isHoldStartedInTime)
				{
					isHoldStartedInTime = false;
					state = StateEnum.performed;
					Logging("performed");
					OnPerformed?.Invoke();
				}

				if (context.interaction is PressInteraction && state == StateEnum.performed)
				{
					state = StateEnum.reset;
					isHoldStartedInTime = false;
					Logging("onPerformEnded");
					OnPerformEnded?.Invoke();
				}
				
			};
			
			
			tapAndHoldRef.action.canceled += (context) =>
			{
				if (state != StateEnum.reset)
					return;
				state = StateEnum.reset;
				Logging("reset");
				isHoldStartedInTime = false;
				OnCancelled?.Invoke();
			};
			
			
		}
        

		void Logging(string str)
		{
			if (!isLoggingEnabled)
				return;
			Debug.Log($"TapAndHoldInputInteractionSim: {str}");
		}

		enum StateEnum
		{
			reset,
			waiting,
			started,
			performed
		}
	}
}