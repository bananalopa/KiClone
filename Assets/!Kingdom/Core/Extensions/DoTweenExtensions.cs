using UnityEngine;
using PrimeTween;


public static class DOTweenExtensions
{
	public static Tween DOFadeIn(this SpriteRenderer renderer, float duration, bool isStartValueOverwritten = false)
	{
		renderer.enabled = true;
		if (isStartValueOverwritten)
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0); //memory allocation
		
		return Tween.Alpha(renderer, 1, duration, Ease.Default);
	}


	public static Tween DOFadeOut(this SpriteRenderer renderer, float duration, bool isStartValueOverwritten = false)
	{
		if (isStartValueOverwritten)
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1); //memory allocation
		
		return Tween.Alpha(renderer, 0, duration, Ease.Default).OnComplete(() => renderer.enabled = false); //memory allocation
	}
}
