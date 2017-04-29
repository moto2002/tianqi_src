using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TextStyleManager
{
	public static void SetTextStyle(Transform text, int styleID)
	{
		if (text.GetComponent<Text>() == null)
		{
			return;
		}
		WenZiYangShi wenZiYangShi = DataReader<WenZiYangShi>.Get(styleID);
		if (wenZiYangShi != null)
		{
			Outline outline = text.get_gameObject().AddMissingComponent<Outline>();
			outline.set_effectColor(new Color((float)wenZiYangShi.outLineColor.get_Item(0) / 255f, (float)wenZiYangShi.outLineColor.get_Item(1) / 255f, (float)wenZiYangShi.outLineColor.get_Item(2) / 255f, (float)wenZiYangShi.outLineColor.get_Item(3) / 255f));
			outline.set_effectDistance(new Vector2((float)wenZiYangShi.outLineWidth.get_Item(0), (float)wenZiYangShi.outLineWidth.get_Item(1)));
			Shadow shadow = text.get_gameObject().AddMissingComponent<Shadow>();
			shadow.set_effectColor(new Color((float)wenZiYangShi.shadowColor.get_Item(0) / 255f, (float)wenZiYangShi.shadowColor.get_Item(1) / 255f, (float)wenZiYangShi.shadowColor.get_Item(2) / 255f, (float)wenZiYangShi.shadowColor.get_Item(3) / 255f));
			shadow.set_effectDistance(new Vector2((float)wenZiYangShi.shadowOffset.get_Item(0), (float)wenZiYangShi.shadowOffset.get_Item(1)));
		}
	}
}
