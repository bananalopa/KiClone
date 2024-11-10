using System;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class WallSetting
	{
		[SerializeField] private int rebuildingPrice;
		[SerializeField] private int baseHp;
		[SerializeField] private int blessedHp;

		[SerializeField] Sprite damagedSprite;
		[SerializeField] Sprite destroyedSprite;
		
		
		public int RebuildingPrice => rebuildingPrice;
		public int BaseHp => baseHp;
		public int BlessedHp => blessedHp;

		public Sprite DamagedSprite => damagedSprite;

		public Sprite DestroyedSprite => destroyedSprite;
	}
}