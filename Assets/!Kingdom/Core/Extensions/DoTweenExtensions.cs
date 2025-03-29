using DG.Tweening;
using UnityEngine;

public static class DOTweenExtensions
{
	public static Tweener DOFadeIn(this SpriteRenderer renderer, float duration, bool isStartValueOverwritten = false)
	{
		renderer.enabled = true;
		if (isStartValueOverwritten)
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
		return renderer.DOFade(1, duration);
	}


	public static Tweener DOFadeOut(this SpriteRenderer renderer, float duration, bool isStartValueOverwritten = false)
	{
		if (isStartValueOverwritten)
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
		return renderer.DOFade(0, duration).OnComplete(() => renderer.enabled = false);
	}
}
