using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Entities
{
	public class VagrantCampPresenter : MonoBehaviour
	{
		[SerializeField] VagrantCampModel model;
		[SerializeField] VagrantCampView view;
		[SerializeField] VagrantCampSettingSo settingSo;

		VagrantCampSetting Setting => settingSo.Setting;
		//todo add save/load for spawner timer

		private void Start()
		{
			StartCoroutine(DoSpawnLoop());
			
		}

		IEnumerator DoSpawnLoop()
		{
			while (true)
			{
				if (model.PastTimeSinceLastSpawn.Value>Setting.VagrantSpawnRateInSec.Value)
					Spawn();

				yield return null;

			}
		}

		private void Spawn()
		{
			if (model.Vagrants.Value.Count>= Setting.VagrantCapacity.Value)
				return;

			model.PastTimeSinceLastSpawn.Value = 0;
			Debug.LogError("VagrantCamp Spawn Event!");
		}
	}
}