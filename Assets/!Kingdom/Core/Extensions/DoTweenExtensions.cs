using DG.Tweening;
using UnityEngine;

public static class DOTweenExtensions
{
	public static Tweener DOFadeIn(this SpriteRenderer renderer, float duration, bool isStartValueOverwritten = false)
	{
		renderer.enabled = true;
		if (isStartValueOverwritten)
			renderer.DOFade(0,0).Complete();
		return renderer.DOFade(1, duration);
	}


	public static Tweener DOFadeOut(this SpriteRenderer renderer, float duration, bool isStartValueOverwritten = false)
	{
		if (isStartValueOverwritten)
			renderer.DOFade(1,0).Complete();
		return renderer.DOFade(0, duration).OnComplete(() => renderer.enabled = false);
	}
}
