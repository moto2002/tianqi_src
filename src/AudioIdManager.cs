using GameData;
using System;
using System.Collections.Generic;

public class AudioIdManager
{
	public const int SNORMAL = 10033;

	public const int S1 = 10001;

	public const int S2 = 10002;

	public const int S3 = 10003;

	public const int S4 = 10004;

	public const int S5 = 10008;

	public const int S6 = 10009;

	public const int S7 = 10010;

	public const int S8 = 10011;

	public const int S9 = 10012;

	public const int S10 = 10013;

	public const int S11 = 10018;

	public const int S12 = 10021;

	public const int S13 = 10022;

	public const int S14 = 10024;

	public const int S15 = 10026;

	public const int S16 = 10027;

	public const int S17 = 10028;

	public const int S18 = 10029;

	public const int S19 = 10030;

	public const int S20 = 10031;

	public const int S21 = 10041;

	public const int S22 = 10052;

	public const int S23 = 10037;

	public const int S24 = 10039;

	private const int UI_DEFAULT_AUDIO_ID = -1;

	private static List<Audio2UI> _Audio2UIs;

	private static List<Audio2UI> Audio2UIs
	{
		get
		{
			if (AudioIdManager._Audio2UIs == null)
			{
				AudioIdManager._Audio2UIs = DataReader<Audio2UI>.DataList;
			}
			return AudioIdManager._Audio2UIs;
		}
	}

	public static int GetAudioId(string uiName, string widgetName)
	{
		int result = -1;
		for (int i = 0; i < AudioIdManager.Audio2UIs.get_Count(); i++)
		{
			Audio2UI audio2UI = AudioIdManager.Audio2UIs.get_Item(i);
			if (!string.IsNullOrEmpty(uiName) && audio2UI.widgetId > 0)
			{
				UIWidgetTable uIWidgetTable = DataReader<UIWidgetTable>.Get(audio2UI.widgetId);
				if (uIWidgetTable != null)
				{
					UINameTable uINameTable = DataReader<UINameTable>.Get(uIWidgetTable.uiId);
					if (uINameTable != null && uINameTable.name.Equals(uiName) && uIWidgetTable.widgetName.Equals(widgetName))
					{
						result = audio2UI.audioId;
					}
				}
			}
		}
		return result;
	}
}
