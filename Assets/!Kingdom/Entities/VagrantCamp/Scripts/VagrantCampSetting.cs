using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Entities
{
	[Serializable]
	public class VagrantCampSetting
	{
		[SerializeField] IntReference vagrantCapacity;
		[FormerlySerializedAs("vagrantSpawnRate")] [SerializeField] private FloatReference vagrantSpawnRateInSec;
		[SerializeField] private FloatReference disappearAfterTreeNearbyCutTimeout;

		public IntReference VagrantCapacity => vagrantCapacity;

		public FloatReference VagrantSpawnRateInSec => vagrantSpawnRateInSec;

		public FloatReference DisappearAfterTreeNearbyCutTimeout => disappearAfterTreeNearbyCutTimeout;
	}
}