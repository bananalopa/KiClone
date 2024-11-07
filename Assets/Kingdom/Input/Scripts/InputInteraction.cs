using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;
using Zenject;

namespace Kingdom.Input
{
	public abstract class InputInteraction : MonoBehaviour
	{
		public event Action OnStart;
		public event Action OnStartCancel;
		public event Action OnPerform;
		public event Action OnPerformEnd;
		
		[SerializeField] InteractionStateEnum interactionState;

		[SerializeField] private bool isLoggingEnabled;
		public InteractionStateEnum InteractionState => interactionState;
		
		public enum InteractionStateEnum
		{
			None,
			Started,
			Performing
		}

		protected void Start(string interactionName)
		{
			Logging(interactionName, "Start");
			interactionState = InteractionStateEnum.Started;
			OnStart?.Invoke();
		}
		
		protected void StartCancel(string interactionName)
		{
			Logging(interactionName, "StartCancel");
			interactionState = InteractionStateEnum.None;
			OnStartCancel?.Invoke();
		}
		
		protected void Perform(string interactionName)
		{
			Logging(interactionName, "Perform");
			interactionState = InteractionStateEnum.Performing;
			OnPerform?.Invoke();
		}
		
		protected void PerformEnd(string interactionName)
		{
			Logging(interactionName, "PerformEnd");
			interactionState = InteractionStateEnum.None;
			OnPerformEnd?.Invoke();
		}
		
		void Logging(string interactionName, string interactionState)
		{
			if (!isLoggingEnabled)
				return;
			Debug.Log($"{interactionName}: {interactionState}");
		}
	}
}