using UnityEngine;
using Zenject;

namespace Kingdom.Entities
{
	public class VagrantCampView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer campRenderer;
		
		private SharedSettings sharedSettings;
		
		[Inject]
		void Construct(SharedSettings sharedSettings)
		{
			this.sharedSettings = sharedSettings;
		}

		public void DestroyCamp()
		{
			campRenderer.DOFadeOut(sharedSettings.FadeTime);
		}
	}
}