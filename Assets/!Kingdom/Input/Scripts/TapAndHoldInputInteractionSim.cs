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
	public class TapAndHoldInputInteractionSim : InputInteraction
	{
		//todo: refactor it before use
		[SerializeField] private InputActionReference tapAndHoldRef;
		[SerializeField] private float tapToHoldTimeout = .2f;
		[SerializeField] private float resetTimeout = 1f;
		private float startTime;
		private float tapTime;
		private bool isHoldStartedInTime;
		
		private StateEnum state = StateEnum.reset;
		
		private void Start()
		{
			tapAndHoldRef.action.started += (context) =>
			{
				if (context.interaction is TapInteraction)
				{
					if (state == StateEnum.started)
						return;
					state = StateEnum.started;
					Start(typeof(TapAndHoldInputInteractionSim).Name);
					startTime = Time.unscaledTime;
				}
				
				if (context.interaction is HoldInteraction && (tapTime + tapToHoldTimeout > Time.unscaledTime))
					isHoldStartedInTime = true;
			};
			
			tapAndHoldRef.action.performed += (context) =>
			{
				if (context.interaction is TapInteraction)
					tapTime = Time.unscaledTime;
				
				if (context.interaction is HoldInteraction && isHoldStartedInTime)
				{
					isHoldStartedInTime = false;
					state = StateEnum.performed;
					Perform(typeof(TapAndHoldInputInteractionSim).Name);
				}

				if (context.interaction is PressInteraction && state == StateEnum.performed)
				{
					state = StateEnum.reset;
					isHoldStartedInTime = false;
					PerformEnd(typeof(TapAndHoldInputInteractionSim).Name);
				}
				
				
			};
			
			tapAndHoldRef.action.canceled += (context) =>
			{
				if (state != StateEnum.reset)
					return;
				state = StateEnum.reset;
				isHoldStartedInTime = false;
				StartCancel(typeof(TapAndHoldInputInteractionSim).Name);
			};
			
		}

		private void Update()
		{
			if (state != StateEnum.performed && Time.unscaledTime > startTime + resetTimeout)
			{
				if (state == StateEnum.reset)
					return;
				state = StateEnum.reset;
				isHoldStartedInTime = false;
				StartCancel(typeof(TapAndHoldInputInteractionSim).Name);
			}
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