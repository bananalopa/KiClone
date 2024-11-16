using DG.Tweening;
using UnityEngine;

public static class DOTweenExtensions
{
	public static Tweener DOFadeIn(this SpriteRenderer renderer, float duration)
	{
		renderer.enabled = true;
		return renderer.DOFade(1, duration);
	}


	public static Tweener DOFadeOut(this SpriteRenderer renderer, float duration)
	{
		return renderer.DOFade(0, duration).OnComplete(() => renderer.enabled = false);
	}
}
