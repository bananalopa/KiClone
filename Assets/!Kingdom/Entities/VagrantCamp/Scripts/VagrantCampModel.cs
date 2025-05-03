using System;
using System.Collections.Generic;
using Kingdom.Peasants;
using ObservableCollections;
using R3;
using R3.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Entities
{
	[Serializable]
	public class VagrantCampModel
	{
		[SerializeField] private SerializableReactiveProperty<float> pastTimeSinceLastSpawn;
		[SerializeField] private SerializableReactiveProperty<bool> isMarkedForDestruction;
		[SerializeField] private SerializableReactiveProperty<List<VagrantPresenter>> vagrants;

		public bool IsMarkedForDestruction => isMarkedForDestruction.Value;

		public SerializableReactiveProperty<float> PastTimeSinceLastSpawn => pastTimeSinceLastSpawn;

		public SerializableReactiveProperty<List<VagrantPresenter>> Vagrants => vagrants;
		
	}
}