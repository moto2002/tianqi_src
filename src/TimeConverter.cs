using System;

public class TimeConverter
{
	private const int ID2SHOW_SECOND = 506056;

	private const int ID2SHOW_MINUTE = 506057;

	private const int ID2SHOW_HOUR = 506058;

	private const int ID2SHOW_DAY = 506059;

	public const string HHMMSS_SIGN = ":";

	public const int ID_DAY = 509000;

	public const int ID_HOUR = 509001;

	public const int ID_MINUTE = 509002;

	public const int ID_SECOND = 509003;

	private static string daysStr = string.Empty;

	private static string hoursStr = string.Empty;

	private static string minutesStr = string.Empty;

	private static string secondsStr = string.Empty;

	public static string ShowTime4Second(int second)
	{
		string result = string.Empty;
		if (second < 60)
		{
			result = GameDataUtils.GetChineseContent(506056, false);
		}
		else if (second < 3600)
		{
			int num = second / 60;
			result = string.Format(GameDataUtils.GetChineseContent(506057, false), num);
		}
		else if (second < 86400)
		{
			int num2 = second / 3600;
			result = string.Format(GameDataUtils.GetChineseContent(506058, false), num2);
		}
		else
		{
			int num3 = second / 86400;
			result = string.Format(GameDataUtils.GetChineseContent(506059, false), num3);
		}
		return result;
	}

	public static string ChangeSecsToString(int secs)
	{
		int num = secs / 60;
		int num2 = secs % 60;
		string text;
		if (num2 < 10)
		{
			text = "0" + num2;
		}
		else
		{
			text = num2.ToString();
		}
		string text2;
		if (num < 10)
		{
			text2 = "0" + num;
		}
		else
		{
			text2 = num.ToString();
		}
		return text2 + " : " + text;
	}

	private static string GetDHHMM_Chinese(int totalSeconds)
	{
		TimeConverter.daysStr = string.Empty;
		TimeConverter.hoursStr = string.Empty;
		TimeConverter.minutesStr = string.Empty;
		TimeConverter.secondsStr = string.Empty;
		int num = totalSeconds / 86400;
		int num2 = totalSeconds % 86400 / 3600;
		int num3 = totalSeconds % 3600 / 60;
		if (num > 0)
		{
			TimeConverter.daysStr = num + GameDataUtils.GetChineseContent(509000, false);
			TimeConverter.hoursStr = num2.ToString("D2") + GameDataUtils.GetChineseContent(509001, false);
			TimeConverter.minutesStr = num3.ToString("D2") + GameDataUtils.GetChineseContent(509002, false);
		}
		else if (num2 > 0)
		{
			TimeConverter.hoursStr = num2 + GameDataUtils.GetChineseContent(509001, false);
			TimeConverter.minutesStr = num3.ToString("D2") + GameDataUtils.GetChineseContent(509002, false);
		}
		else
		{
			TimeConverter.minutesStr = num3 + GameDataUtils.GetChineseContent(509002, false);
		}
		return TimeConverter.daysStr + TimeConverter.hoursStr + TimeConverter.minutesStr;
	}

	private static string GetHHMMSS(int totalSeconds)
	{
		int num = totalSeconds / 3600;
		int num2 = totalSeconds % 3600 / 60;
		int num3 = totalSeconds % 60;
		TimeConverter.hoursStr = ((num <= 100) ? num.ToString("D2") : num.ToString());
		TimeConverter.minutesStr = num2.ToString("D2");
		TimeConverter.secondsStr = num3.ToString("D2");
		return string.Concat(new string[]
		{
			TimeConverter.hoursStr,
			":",
			TimeConverter.minutesStr,
			":",
			TimeConverter.secondsStr
		});
	}

	private static string GetMMSS(int totalSeconds)
	{
		int num = totalSeconds / 60;
		int num2 = totalSeconds % 60;
		TimeConverter.minutesStr = ((num < 100) ? num.ToString("D2") : num.ToString());
		TimeConverter.secondsStr = num2.ToString("D2");
		return TimeConverter.minutesStr + ":" + TimeConverter.secondsStr;
	}

	private static string GetMMSS_Chinese(int totalSeconds)
	{
		int num = totalSeconds / 60;
		int num2 = totalSeconds % 60;
		TimeConverter.minutesStr = ((num < 100) ? num.ToString("D2") : num.ToString());
		TimeConverter.secondsStr = num2.ToString("D2");
		return TimeConverter.minutesStr + GameDataUtils.GetChineseContent(509002, false) + TimeConverter.secondsStr + GameDataUtils.GetChineseContent(509003, false);
	}

	private static string GetHHMM_Chinese(int totalSeconds, bool isMNoZero)
	{
		TimeConverter.hoursStr = string.Empty;
		TimeConverter.minutesStr = string.Empty;
		int num = totalSeconds % 86400 / 3600;
		int num2 = totalSeconds % 3600 / 60;
		if (num > 0)
		{
			TimeConverter.hoursStr = num + GameDataUtils.GetChineseContent(509001, false);
			if (!isMNoZero || num2 != 0)
			{
				TimeConverter.minutesStr = num2.ToString("D2") + GameDataUtils.GetChineseContent(509002, false);
			}
		}
		else
		{
			TimeConverter.minutesStr = num2 + GameDataUtils.GetChineseContent(509002, false);
		}
		return TimeConverter.hoursStr + TimeConverter.minutesStr;
	}

	private static string GetSECOND(int totalSeconds)
	{
		return totalSeconds.ToString();
	}

	private static string GetMDHHMM(DateTime dateTime)
	{
		return string.Format("{0}月{1}日{2}:{3:D2}", new object[]
		{
			dateTime.get_Month(),
			dateTime.get_Day(),
			dateTime.get_Hour(),
			dateTime.get_Minute()
		});
	}

	public static string GetTime(int totalSeconds, TimeFormat timeFormat)
	{
		switch (timeFormat)
		{
		case TimeFormat.DHHMM_Chinese:
			return TimeConverter.GetDHHMM_Chinese(totalSeconds);
		case TimeFormat.HHMMSS:
			return TimeConverter.GetHHMMSS(totalSeconds);
		case TimeFormat.MMSS:
			return TimeConverter.GetMMSS(totalSeconds);
		case TimeFormat.MMSS_Chinese:
			return TimeConverter.GetMMSS_Chinese(totalSeconds);
		case TimeFormat.SECOND:
			return TimeConverter.GetSECOND(totalSeconds);
		case TimeFormat.HHMM_Chinese:
			return TimeConverter.GetHHMM_Chinese(totalSeconds, false);
		case TimeFormat.HHMM_Chinese_MNoZero:
			return TimeConverter.GetHHMM_Chinese(totalSeconds, true);
		}
		return string.Empty;
	}

	public static string GetTime(DateTime dateTime, TimeFormat timeFormat)
	{
		if (timeFormat != TimeFormat.MDHHMM)
		{
			return string.Empty;
		}
		return TimeConverter.GetMDHHMM(dateTime);
	}
}
