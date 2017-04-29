using System;
using UnityEngine;

public class UIMaskLayer : MonoBehaviour
{
	private void Start()
	{
		int num;
		int num2;
		UIConst.GetRealScreenSize(out num, out num2);
		RectTransform rectTransform = base.get_transform() as RectTransform;
		if (rectTransform != null)
		{
			rectTransform.set_sizeDelta(new Vector2((float)num, (float)num2));
		}
	}
}
