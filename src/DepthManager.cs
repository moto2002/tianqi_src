using System;
using UnityEngine;
using UnityEngine.UI;

public class DepthManager
{
	public static void SetDepth(GameObject target, int order)
	{
		Canvas canvas = target.AddUniqueComponent<Canvas>();
		canvas.set_enabled(true);
		canvas.set_overrideSorting(true);
		canvas.set_sortingOrder(order);
	}

	public static void SetGraphicRaycaster(GameObject target)
	{
		GraphicRaycaster graphicRaycaster = target.AddMissingComponent<GraphicRaycaster>();
		graphicRaycaster.set_enabled(true);
		graphicRaycaster.set_blockingObjects(0);
		graphicRaycaster.set_ignoreReversedGraphics(true);
	}
}
