using System;
using UnityEngine;
using UnityEngine.UI;

public class ListShifts
{
	public const int Move2Next = 1;

	public const int Move2Previous = 2;

	public const int Move2First = 3;

	public const int Move2Last = 4;

	public const int Move2Page = 5;

	public const int Move2Index = 6;

	public static Vector3 GetShift(int shiftType, int arg = 0, bool bRightNow = false)
	{
		if (bRightNow)
		{
			return new Vector3((float)shiftType, (float)arg, 1f);
		}
		return new Vector3((float)shiftType, (float)arg, 0f);
	}

	public static void ResetShift(ref Vector3 shift)
	{
		shift = Vector3.get_zero();
	}

	public static void SetShift(ScrollRectCustom src, object arg)
	{
		if (arg == null)
		{
			return;
		}
		if (src == null)
		{
			return;
		}
		Vector3 action = (Vector3)arg;
		bool bRightNow = action.z == 1f;
		switch ((int)action.x)
		{
		case 1:
			src.Move2Next();
			break;
		case 2:
			src.Move2Previous();
			break;
		case 3:
			src.Move2First(bRightNow);
			src.OnHasBuilt = delegate
			{
				src.OnHasBuilt = null;
				src.Move2First(bRightNow);
			};
			break;
		case 4:
			src.Move2Last(bRightNow);
			src.OnHasBuilt = delegate
			{
				src.OnHasBuilt = null;
				src.Move2Last(bRightNow);
			};
			break;
		case 5:
			src.Move2Page((int)action.y, bRightNow);
			src.OnHasBuilt = delegate
			{
				if (src.GetPageNum() > (int)action.y)
				{
					src.OnHasBuilt = null;
					src.Move2Page((int)action.y, bRightNow);
				}
			};
			break;
		case 6:
			src.Move2Index((int)action.y, bRightNow);
			src.OnHasBuilt = delegate
			{
				src.OnHasBuilt = null;
				src.Move2Index((int)action.y, bRightNow);
			};
			break;
		}
	}
}
