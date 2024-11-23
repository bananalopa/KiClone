using System;
using UnityEngine;

namespace Kingdom.Entities
{
	[Serializable]
	public class VagrantCampSetting
	{
		[SerializeField] IntReference vagrantCapacity;
		[SerializeField] private FloatReference vagrantSpawnRate;
		[SerializeField] private FloatReference disappearAfterTreeNearbyCutTimeout;
		
		public IntReference VagrantCapacity => vagrantCapacity;

		public FloatReference VagrantSpawnRate => vagrantSpawnRate;

		public FloatReference DisappearAfterTreeNearbyCutTimeout => disappearAfterTreeNearbyCutTimeout;
	}
}